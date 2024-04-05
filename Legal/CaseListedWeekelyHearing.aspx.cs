using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_CaseListedWeekelyHearing : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                GetCaseType();
                FillCourt();
            }
        }
        else
        { Response.Redirect("/Login.aspx", false); }
    }
    protected void FillCourt()
    {
        try
        {
            ddlCourtName.Items.Clear();
            Helper court = new Helper();
            DataTable dtCourt = new DataTable();
            if (Session["Role_ID"].ToString() == "5")// Court.
            {
                string District_Id = Session["District_Id"].ToString();
                dtCourt = court.GetCourtForCourt(District_Id) as DataTable;
            }
            else if (Session["Role_ID"].ToString() == "4")// District Office.
            {
                string District_Id = Session["District_Id"].ToString();
                dtCourt = court.GetCourtForCourt(District_Id) as DataTable;

            }
            else if (Session["Role_ID"].ToString() == "3")// OIC Login
            {
                string District_Id = Session["District_Id"].ToString();
                dtCourt = court.GetCourtForCourt(District_Id) as DataTable;
            }
            else if (Session["Role_ID"].ToString() == "2")// Division Office.
            {
                string Division_Id = Session["Division_Id"].ToString();
                dtCourt = court.GetCourtByDivision(Division_Id) as DataTable;

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

    #region Get Case Type
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
    #endregion
    #region Bind Grid
    protected void BindGrid()
    {
        try
        {
            string strFromDate = string.Empty;
            string strToDate = string.Empty;
            if (txtFromDate.Text != "")
            {
                string A_Date = txtFromDate.Text; // From Database
                DateTime sdate = DateTime.ParseExact(A_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                strFromDate = sdate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            }
            if (txttodate.Text != "")
            {
                string A_Date = txttodate.Text; // From Database
                DateTime sdate = DateTime.ParseExact(A_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                strToDate = sdate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            }

            string OIC_ID = Session["OICMaster_ID"] != null ? Session["OICMaster_ID"].ToString() : null;
            ds = obj.ByProcedure("Usp_CaseListedWeekelyHearing", new string[] { "flag", "Casetype_ID", "CourtType_Id",  "OICMaster_Id", "NFromDate", "NToDate" },
                new string[] { "1", ddlCaseType.SelectedItem.Value, ddlCourtName.SelectedItem.Value, OIC_ID , strFromDate , strToDate }, "dataset");
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdWeekelyWiseCasedtl.DataSource = ds;
                grdWeekelyWiseCasedtl.DataBind();
                if (grdWeekelyWiseCasedtl.Rows.Count > 0)
                {
                    grdWeekelyWiseCasedtl.HeaderRow.TableSection = TableRowSection.TableHeader;
                    grdWeekelyWiseCasedtl.UseAccessibleHeader = true;
                }
            }
            else
            {
                grdWeekelyWiseCasedtl.DataSource = null;
                grdWeekelyWiseCasedtl.DataBind();
            }
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
            ds = new DataSet();
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
    #endregion
    #region Row Command
    protected void grdSubjectWiseCasedtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "ViewDtl")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                string ID = HttpUtility.UrlEncode(Encrypt(e.CommandArgument.ToString()));
                string page_ID = HttpUtility.UrlEncode(Encrypt("1"));
                string CaseID = HttpUtility.UrlEncode(Encrypt("CaseID"));
                string pageID = HttpUtility.UrlEncode(Encrypt("pageID"));
                Label lblHearingNext = (Label)row.FindControl("lblHearingNext");
                string Hearingdate = HttpUtility.UrlEncode(Encrypt(lblHearingNext.Text));

                Response.Redirect("~/Legal/ActionWeekelyHearing.aspx?" + CaseID + "=" + ID + "&" + pageID + "=" + page_ID + "&lblHearingNext=" + Hearingdate, false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    private string Encrypt(string clearText)
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
}