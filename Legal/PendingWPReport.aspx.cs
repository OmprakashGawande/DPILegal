﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mis_Legal_PendingWPReport : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                FillCasetype();
                FillYear();
                ViewState["OIC_ID"] = Session["OICMaster_ID"];
            }
        }
        else
        {
            Response.Redirect("~/Legal/Login.aspx", false);
        }
    }
    #region Fill Year
    protected void FillYear()
    {
        ddlCaseYear.Items.Clear();
        DataSet dsCase = obj.ByDataSet("with yearlist as (select 1950 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year desc");
        if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
        {
            ddlCaseYear.DataSource = dsCase.Tables[0];
            ddlCaseYear.DataTextField = "year";
            ddlCaseYear.DataValueField = "year";
            ddlCaseYear.DataBind();
        }
        ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));

    }
    #endregion
    #region Fill Case type
    protected void FillCasetype()
    {
        try
        {
            Helper hl = new Helper();
            DataTable dt = hl.GetCasetype() as DataTable;
            if (dt.Rows.Count > 0)
            {
                ddlCasetype.DataTextField = "Casetype_Name";
                ddlCasetype.DataValueField = "Casetype_ID";
                ddlCasetype.DataSource = dt;
                ddlCasetype.DataBind();
            }
            ddlCasetype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Btn Search
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string OIC = "";
                lblMsg.Text = "";
                GrdPendingReport.DataSource = null;
                GrdPendingReport.DataBind();
                if (Session["Role_ID"].ToString() == "4")
                {
                    string District_Id = Session["District_Id"].ToString();
                    ds = obj.ByProcedure("USP_GetWPPendingRpt", new string[] { "CaseYear", "Casetype_ID", "District_Id","flag" }
                        , new string[] { ddlCaseYear.SelectedValue, ddlCasetype.SelectedValue, District_Id,"2" }, "dataset");
                }
                else if (Session["Role_ID"].ToString() == "2")
                {
                     string Division_Id = Session["Division_Id"].ToString();
                     ds = obj.ByProcedure("USP_GetWPPendingRpt", new string[] { "CaseYear", "Casetype_ID", "Division_Id", "flag" }
                        , new string[] { ddlCaseYear.SelectedValue, ddlCasetype.SelectedValue, Division_Id, "3" }, "dataset");
                }
                else if (Session["Role_ID"].ToString() == "5")
                {
                    string District_Id = Session["District_Id"].ToString();
                    ds = obj.ByProcedure("USP_GetWPPendingRpt", new string[] { "CaseYear", "Casetype_ID", "CourtLocation_Id", "flag" }
                       , new string[] { ddlCaseYear.SelectedValue, ddlCasetype.SelectedValue, District_Id, "4" }, "dataset");
                }
                else
                {
                    if (Session["OICMaster_ID"] != "" && Session["OICMaster_ID"] != null)
                        OIC = Session["OICMaster_ID"].ToString();
                    ds = obj.ByProcedure("USP_GetWPPendingRpt", new string[] { "CaseYear", "Casetype_ID", "OICMaster_Id","flag" }
                        , new string[] { ddlCaseYear.SelectedValue, ddlCasetype.SelectedValue, OIC ,"1"}, "dataset");
                }
              
                if (ds != null )
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        GrdPendingReport.DataSource = ds;
                        GrdPendingReport.DataBind();
                        GrdPendingReport.HeaderRow.TableSection = TableRowSection.TableHeader;
                        GrdPendingReport.UseAccessibleHeader = true;
                    }
                    
                   
                }
                else
                {
                    GrdPendingReport.DataSource = null;
                    GrdPendingReport.DataBind();
                }
               
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
        finally { ds.Clear(); }
    }
    #endregion
    #region Row Command
    protected void GrdPendingReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewDetail")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string ID = e.CommandArgument.ToString();
                string pageID = "1";
                Response.Redirect("~/Legal/ViewWPPendingCaseDetail.aspx?CaseID=" + ID + "&pageID=" + pageID, false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
}