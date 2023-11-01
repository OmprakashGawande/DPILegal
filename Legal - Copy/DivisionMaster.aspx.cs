using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_DivisionMaster : System.Web.UI.Page
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
                FillZone();
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }

    #region Fill Zone
    protected void FillZone()
    {
        try
        {
            ddlzone.Items.Clear();
            ds = obj.ByProcedure("USP_SelectZoneForCircleMaster", new string[] { }
                    , new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlzone.DataTextField = "ZoneName";
                ddlzone.DataValueField = "Zone_ID";
                ddlzone.DataSource = ds;
                ddlzone.DataBind();
            }
            ddlzone.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion

    #region Fill GridView
    protected void FillGrid()
    {
        try
        {
            ds = obj.ByProcedure("USP_Select_DivisionMaster", new string[] { }
                   , new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrddivisionMst.DataSource = ds;
                GrddivisionMst.DataBind();
            }
            else
            {
                GrddivisionMst.DataSource = null;
                GrddivisionMst.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    #endregion
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnSave.Text == "Save")
                {
                    //ds = obj.ByProcedure("USP_Insert_DivisionMaster", new string[] { "Division_Name", "Division_Code", "Zone_Id", "Circle_Id", "Office_Id", "CreatedBy", "CreatedByIP" }
                    //, new string[] { txtDivisionName.Text.Trim(), txtDivisionCode.Text.Trim(), ddlzone.SelectedValue, ddlCircleName.SelectedValue, ViewState["Office_Id"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");

                    ds = obj.ByProcedure("USP_Insert_DivisionMaster", new string[] { "Division_Name", "Zone_Id", "Circle_Id", "Office_Id", "CreatedBy", "CreatedByIP" }
                    , new string[] { txtDivisionName.Text.Trim(), ddlzone.SelectedValue, ddlCircleName.SelectedValue, ViewState["Office_Id"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["DivisionID"].ToString() != "" && ViewState["DivisionID"].ToString() != null)
                {
                    //ds = obj.ByProcedure("USP_Upate_DivisionMaster", new string[] { "Division_Name", "Division_Code", "Zone_Id", "Circle_Id", "Office_Id", "LastUpdatedBy", "LastUpdatedByIP", "Division_ID" }
                    //, new string[] { txtDivisionName.Text.Trim(), txtDivisionCode.Text.Trim(), ddlzone.SelectedValue, ddlCircleName.SelectedValue, ViewState["Office_Id"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["DivisionID"].ToString() }, "dataset");

                    ds = obj.ByProcedure("USP_Upate_DivisionMaster", new string[] { "Division_Name", "Zone_Id", "Circle_Id", "Office_Id", "LastUpdatedBy", "LastUpdatedByIP", "Division_ID" }
                    , new string[] { txtDivisionName.Text.Trim(), ddlzone.SelectedValue, ddlCircleName.SelectedValue, ViewState["Office_Id"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["DivisionID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        FillGrid();
                        btnSave.Text = "Save";
                        ddlzone.ClearSelection();
                        ddlCircleName.ClearSelection();
                        txtDivisionName.Text = "";
                        txtDivisionCode.Text = "";
                        ViewState["DivisionID"] = "";
                        lblMsg.Text = obj.Alert("fa-ban", "alert-success", "Thanks !", ErrMsg);
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrMsg);
                    }
                }
                else
                {
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Warning !", ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                }
               
               
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void GrddivisionMst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if(e.CommandName == "EditDetails")
            {
                ViewState["DivisionID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblZoneID = (Label)row.FindControl("lblZoneID");
                Label lblCircleID = (Label)row.FindControl("lblCircleID");
                Label lblDivisionName = (Label)row.FindControl("lblDivisionName");
                Label lblDivisionCode = (Label)row.FindControl("lblDivisionCode");
                txtDivisionName.Text = lblDivisionName.Text;
                txtDivisionCode.Text = lblDivisionCode.Text;
                ddlzone.ClearSelection();
                ddlzone.Items.FindByValue(lblZoneID.Text).Selected = true;
                ddlzone_SelectedIndexChanged(sender,  e);
                ddlCircleName.ClearSelection();
                ddlCircleName.Items.FindByValue(lblCircleID.Text).Selected = true;
                ViewState["DivisionID"] = e.CommandArgument;
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void GrddivisionMst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GrddivisionMst.PageIndex = e.NewPageIndex;
            FillGrid();
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ddlzone_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlCircleName.Items.Clear();
            ds = obj.ByProcedure("USP_Select_CircleMaster", new string[] { "flag", "Zone_ID" }
                    , new string[] { "2", ddlzone.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCircleName.DataTextField = "CirlceName";
                ddlCircleName.DataValueField = "Circle_ID";
                ddlCircleName.DataSource = ds;
                ddlCircleName.DataBind();
            }
            ddlCircleName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}