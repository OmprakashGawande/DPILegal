using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_ZoneMaster : System.Web.UI.Page
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
                FillGrid();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void FillGrid()
    {
        try
        {
            ds = obj.ByProcedure("USP_Select_ZoneMaster", new string[] { }
                    , new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GrdZoneMaster.DataSource = ds;
                GrdZoneMaster.DataBind();
            }
            else
            {
                GrdZoneMaster.DataSource = null;
                GrdZoneMaster.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnSave.Text == "Save")
                {
                    //ds = obj.ByProcedure("USP_Insert_ZoneMaster", new string[] { "ZoneName", "ZoneCode", "CreatedBy", "CreatedByIP", "Office_Id" }
                    //, new string[] { txtZoneName.Text.Trim(), txtZoneCode.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Office_Id"].ToString() }, "dataset");
                    ds = obj.ByProcedure("USP_Insert_ZoneMaster", new string[] { "ZoneName", "CreatedBy", "CreatedByIP", "Office_Id" }
                         , new string[] { txtZoneName.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Office_Id"].ToString() }, "dataset");
                }
                else if (btnSave.Text == "Update" && ViewState["ZoneID"].ToString() != "" && ViewState["ZoneID"].ToString() != null)
                {
                    //ds = obj.ByProcedure("USP_Update_ZoneMaster", new string[] { "ZoneName", "ZoneCode", "LastUpdatedBy", "LastUpdatedByIP", "Office_Id", "Zone_ID" }
                    //, new string[] { txtZoneName.Text.Trim(), txtZoneCode.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Office_Id"].ToString(), ViewState["ZoneID"].ToString() }, "dataset");
                    ds = obj.ByProcedure("USP_Update_ZoneMaster", new string[] { "ZoneName", "LastUpdatedBy", "LastUpdatedByIP", "Office_Id", "Zone_ID" }
                                        , new string[] { txtZoneName.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Office_Id"].ToString(), ViewState["ZoneID"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        FillGrid();
                        btnSave.Text = "Save";
                        txtZoneName.Text = "";
                        txtZoneCode.Text = "";
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
    protected void GrdZoneMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditDetails")
            {
                lblMsg.Text = "";
                ViewState["ZoneID"] = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblZoneName = (Label)row.FindControl("lblZoneName");
                Label lblZoneCode = (Label)row.FindControl("lblZoneCode");
                txtZoneName.Text = lblZoneName.Text;
                //txtZoneCode.Text = lblZoneCode.Text;
                ViewState["ZoneID"] = e.CommandArgument;
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
}