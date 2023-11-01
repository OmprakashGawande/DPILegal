using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_DesignationMaster : System.Web.UI.Page
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
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                BindGrid();
                FillOfficetypeName();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }

    protected void BindGrid()
    {
        try
        {
            ds = obj.ByProcedure("USP_Select_DesignationMaster", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrdDesignation.DataSource = ds;
                GrdDesignation.DataBind();
            }
            else
            {
                GrdDesignation.DataSource = null;
                GrdDesignation.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }

    protected void FillOfficetypeName()
    {
        try
        {
            ddlOfficetypename.Items.Clear();
            ds = obj.ByDataSet("select OfficeType_Id, OfficeType_Name from tblOfficeTypeMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficetypename.DataTextField = "OfficeType_Name";
                ddlOfficetypename.DataValueField = "OfficeType_Id";
                ddlOfficetypename.DataSource = ds;
                ddlOfficetypename.DataBind();
            }
            ddlOfficetypename.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (Page.IsValid)
            {
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_Insert_DesignationMaster", new string[] { "OfficeType_Id", "Office_Id", "DesignationName", "CreatedBy", "CreatedByIP" }
                        , new string[] { ddlOfficetypename.SelectedValue, ddlOfficeName.SelectedValue, txtDeDesignation.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["ID"].ToString() != "" && ViewState["ID"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_Update_Designationmaster", new string[] { "OfficeType_Id", "Office_Id", "DesignationName", "LastUpdatedBy", "LastUpdatedByIp", "DesignationID" }
                        , new string[] { ddlOfficetypename.SelectedValue, ddlOfficeName.SelectedValue, txtDeDesignation.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["ID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtDeDesignation.Text = "";
                        BindGrid();
                        btnSave.Text = "Save";
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-check", "alert-warning", "Warning !", ErrMsg);
                        txtDeDesignation.Text = "";
                    }

                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
    protected void GrdDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GrdDesignation.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
    protected void GrdDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ViewState["ID"] = "";
            if (e.CommandName == "EditDetails")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblDesignationName = (Label)row.FindControl("lblDesignationName");
                Label lblID = (Label)row.FindControl("lblID");
                Label lblOfficetypeID = (Label)row.FindControl("lblOfficetypeID");
                Label lblOfficeID = (Label)row.FindControl("lblOfficeID");
                btnSave.Text = "Update";
                ViewState["ID"] = e.CommandArgument;
                txtDeDesignation.Text = lblDesignationName.Text;
                ddlOfficetypename.ClearSelection();
                ddlOfficetypename.Items.FindByValue(lblOfficetypeID.Text).Selected = true;
                ddlOfficetypename_SelectedIndexChanged( sender,  e);
                ddlOfficeName.ClearSelection();
                ddlOfficeName.Items.FindByValue(lblOfficetypeID.Text).Selected = true;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-dander", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddlOfficetypename_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlOfficeName.Items.Clear();
            ds = obj.ByProcedure("USP_legal_select_OfficeName", new string[] { "OfficeType_Id" },
                new string[] { ddlOfficetypename.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlOfficeName.DataTextField = "OfficeName";
                ddlOfficeName.DataValueField = "Office_Id";
                ddlOfficeName.DataSource = ds;
                ddlOfficeName.DataBind();
            }
            ddlOfficeName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
}