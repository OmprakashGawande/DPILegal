using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;

public partial class Legal_RespondentWiseCaseRpt : System.Web.UI.Page
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
                //FillDitrict();
                FillYear();
                FillDesignation();
                GetCaseType();
                FillCourt();
            }
        }
        else Response.Redirect("/Login.aspx", false);
    }

    protected void FillYear()
    {
        try
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
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    private void GetCaseType()
    {
        try
        {
            ds = new DataSet();
            ds = obj.ByDataSet("select * from tbl_Legal_Casetype");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCaseType.DataSource = ds.Tables[0];
                ddlCaseType.DataTextField = "Casetype_Name";
                ddlCaseType.DataValueField = "Casetype_ID";
                ddlCaseType.DataBind();
                ddlCaseType.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCaseType.DataSource = null;
                ddlCaseType.DataBind();
                ddlCaseType.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillCourt()
    {
        try
        {
            ddlCourtName.Items.Clear();
            Helper court = new Helper();
            DataTable dtCourt = new DataTable();
            if (Session["Role_ID"].ToString() == "5")// JD Legal.
            {
                string District_Id = Session["District_Id"].ToString();
                dtCourt = court.GetCourtForCourt(District_Id) as DataTable;
            }
            else if (Session["Role_ID"].ToString() == "4")// District Office.
            {
                string District_Id = Session["District_Id"].ToString();
                dtCourt = court.GetCourtForCourt(District_Id) as DataTable;

            }
            else dtCourt = court.GetCourt() as DataTable;
            if (dtCourt.Rows.Count > 0)
            {
                ddlCourtName.DataValueField = "CourtType_ID";
                ddlCourtName.DataTextField = "CourtTypeName";
                ddlCourtName.DataSource = dtCourt;
                ddlCourtName.DataBind();
                ddlCourtName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillDesignation()
    {
        try
        {

            ddlDesigNation.Items.Clear();
            ds = obj.ByDataSet("SELECT Designation_Id ,Designation_Name FROM tblDesignationMaster ORDER BY Designation_Name ASC");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {


                ddlDesigNation.DataTextField = "Designation_Name";
                ddlDesigNation.DataValueField = "Designation_Id";
                ddlDesigNation.DataSource = ds;
                ddlDesigNation.DataBind();
            }
            ddlDesigNation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BindGrid()
    {
        try
        {
            //string FromDate = !string.IsNullOrEmpty(txtFromDate.Text) ? Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd") : "";
            //string Todate = !string.IsNullOrEmpty(txttodate.Text) ? Convert.ToDateTime(txttodate.Text, cult).ToString("yyyy/MM/dd") : "";
            string OICID = Session["OICMaster_ID"] != null ? Session["OICMaster_ID"].ToString() : null;
            //if (Session["Role_ID"].ToString() == "4")
            //{
            //    ds = obj.ByProcedure("USP_RespondentWIseCaseList", new string[] { "flag", "Casetype_ID", "Designation_Id", "OICMaster_Id" },
            //       new string[] { "2", ddlCaseType.SelectedItem.Value, ddlPetiDesigNation.SelectedItem.Value, OICID }, "dataset");
            //}
            //else if (Session["Role_ID"].ToString() == "5")
            //{
            //    ds = obj.ByProcedure("USP_RespondentWIseCaseList", new string[] { "flag", "Casetype_ID", "Designation_Id", "OICMaster_Id" },
            //      new string[] { "3", ddlCaseType.SelectedItem.Value, ddlPetiDesigNation.SelectedItem.Value, OICID }, "dataset");
            //}
            //else
            //{
            ds = obj.ByProcedure("USP_RespondentWIseCaseList", new string[] { "flag", "Casetype_ID", "CourtType_Id", "Designation_Id", "OICMaster_Id", "CaseYear", "District_ID", "Respondent_ID" },
                                                               new string[] { "1", ddlCaseType.SelectedValue, ddlCourtName.SelectedValue, ddlDesigNation.SelectedValue, OICID, ddlCaseYear.SelectedValue,ddlDistrict.SelectedValue,ddlRespondent.SelectedValue }, "dataset");
            //}

            if (ds.Tables[0].Rows.Count > 0)
            {
                grdSubjectWiseCasedtl.DataSource = ds;
                grdSubjectWiseCasedtl.DataBind();
                grdSubjectWiseCasedtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdSubjectWiseCasedtl.UseAccessibleHeader = true;
            }
            else
            {
                grdSubjectWiseCasedtl.DataSource = null;
                grdSubjectWiseCasedtl.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                BindGrid();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void grdSubjectWiseCasedtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "ViewDtl")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                grdSubjectWiseCasedtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                grdSubjectWiseCasedtl.UseAccessibleHeader = true;
                string ID = HttpUtility.UrlEncode(Encrypt(e.CommandArgument.ToString()));
                string pageID = HttpUtility.UrlEncode(Encrypt("pageID"));
                string page_ID = HttpUtility.UrlEncode(Encrypt("8"));
                string CaseID = HttpUtility.UrlEncode(Encrypt("CaseID"));
                Response.Redirect("../Legal/ViewWPPendingCaseDetail.aspx?" + CaseID + "=" + ID + "&" + pageID + "=" + page_ID, false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    public string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }



    protected void ddlDesigNation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlRespondent.Items.Clear();
            ds = obj.ByDataSet("select Respondent_ID ,RespondentName from tbl_LegalCaseRespondentDetail where Isactive = 1 and Designation_Id = " + ddlDesigNation.SelectedValue + "");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                ddlRespondent.DataTextField = "RespondentName";
                ddlRespondent.DataValueField = "Respondent_ID";
                ddlRespondent.DataSource = ds;
                ddlRespondent.DataBind();
            }
            ddlRespondent.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlCourt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlDistrict.Items.Clear();
            DataSet ds1 = new DataSet();
            ds1 = obj.ByProcedure("USP_Legal_Select_CourtType", new string[] { "flag", "CourtName_ID" }, new string[] { "2", ddlCourtName.SelectedValue }, "dataset");
            if (ds1 != null && ds1.Tables[1].Rows.Count > 0)
            {
                ddlDistrict.DataValueField = "District_ID";
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataSource = ds1.Tables[1];
                ddlDistrict.DataBind();
                if (Session["Role_ID"].ToString() == "4")// District Office.
                {
                    string District_Id = Session["District_Id"].ToString();
                    ddlDistrict.ClearSelection();
                    ddlDistrict.Items.FindByValue(District_Id).Selected = true;

                    // ddlDistrict.Attributes.Add("disabled", "disable");
                    ddlDistrict.Enabled = false;
                }
                else if (Session["Role_ID"].ToString() == "3")//OIC Login
                {
                    string District_Id = Session["District_Id"].ToString();
                    ddlDistrict.ClearSelection();
                    ddlDistrict.Items.FindByValue(District_Id).Selected = true;
                    //ddlDistrict.Attributes.Add("disabled", "disable");
                    ddlDistrict.Enabled = false;
                }
            }
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }


    }
}
