﻿using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Legal_ActionWeekelyHearing : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
            {

                if (!IsPostBack)
                {

                    string multiCharString = Request.QueryString.ToString();
                    string[] multiArray = multiCharString.Split(new Char[] { '=', '&' });
                    string CaseID = Decrypt(HttpUtility.UrlDecode(multiArray[1]));
                    string PageID = Decrypt(HttpUtility.UrlDecode(multiArray[3]));
                    string ghgh = multiArray[5].ToString();
                    string lblHearingNext = Decrypt(HttpUtility.UrlDecode(ghgh));
                    ViewState["Page"] = PageID.ToString();


                    txtHearingDate.Text = lblHearingNext;
                    DateTime today = DateTime.Today;
                    DateTime dat = Convert.ToDateTime(txtHearingDate.Text, cult);
                    DateTime datecrent  = Convert.ToDateTime(today, cult);

                    if (datecrent >= dat)
                    {
                        Div_Appeal_YesNo.Visible = true;
                    }
                    else
                    {
                        Div_Appeal_YesNo.Visible = false;
                    }
                    txtHearingDate.Enabled = false;
                    ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                    if (!string.IsNullOrEmpty(CaseID))
                    {
                        //ViewState["CaseID"] = CaseID;
                        ViewState["ID"] = CaseID;
                        BindCaseDetail();
                        fillAppealGrid();
                        txtOICDate.Attributes.Add("readonly", "readonly");
                        if (PageID == "2" || PageID == "4")
                        {
                            dvOrderSummary.Visible = true;
                            dvCaseDisposalType.Visible = true;
                            Compilance_Div.Visible = true;
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("../Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #region Bind Case Detail
    protected void BindCaseDetail()
    {
        try
        {
            // lblMsg.Text = "";

            GrdResponderDtl.DataSource = null;
            GrdResponderDtl.DataBind();
            GrdHearingDtl.DataSource = null;
            GrdHearingDtl.DataBind();

            ds = obj.ByProcedure("USP_ViewWPPendingCaseFullDtlRpt", new string[] { "Case_ID" }
                , new string[] { ViewState["ID"].ToString() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CaseNo"].ToString() != "") lblCaseNo.Text = ds.Tables[0].Rows[0]["CaseNo"].ToString(); //else lblCaseNo.Text = "NA";
                if (ds.Tables[0].Rows[0]["CaseYear"].ToString() != "") txtCaseYear.Text = ds.Tables[0].Rows[0]["CaseYear"].ToString(); //else txtCaseYear.Text = "NA";
                if (ds.Tables[0].Rows[0]["CourtTypeName"].ToString() != "") txtCourtType.Text = ds.Tables[0].Rows[0]["CourtTypeName"].ToString();// else txtCourtType.Text = "NA";
                if (ds.Tables[0].Rows[0]["District_Name"].ToString() != "") txtCourtLocation.Text = ds.Tables[0].Rows[0]["District_Name"].ToString(); //else txtCourtLocation.Text = "NA";
                if (ds.Tables[0].Rows[0]["PartyName"].ToString() != "") txtParty.Text = ds.Tables[0].Rows[0]["PartyName"].ToString(); //else txtParty.Text = "NA";
                if (ds.Tables[0].Rows[0]["Casetype_Name"].ToString() != "") txtCasetype.Text = ds.Tables[0].Rows[0]["Casetype_Name"].ToString();// else txtCasetype.Text = "NA";
                if (ds.Tables[0].Rows[0]["CaseSubject"].ToString() != "") txtCaseSubject.Text = ds.Tables[0].Rows[0]["CaseSubject"].ToString();// else txtCaseSubject.Text = "NA";
                if (ds.Tables[0].Rows[0]["CaseSubSubject"].ToString() != "") txtCaseSubSubject.Text = ds.Tables[0].Rows[0]["CaseSubSubject"].ToString();// else txtCaseSubSubject.Text = "NA";
                if (ds.Tables[0].Rows[0]["OICNAME"].ToString() != "") txtOicName.Text = ds.Tables[0].Rows[0]["OICNAME"].ToString(); //else txtOicName.Text = "NA";
                if (ds.Tables[0].Rows[0]["OICMobileNO"].ToString() != "") txtOicMobileNo.Text = ds.Tables[0].Rows[0]["OICMobileNO"].ToString(); //else txtOicMobileNo.Text = "NA";
                if (ds.Tables[0].Rows[0]["OICEmailID"].ToString() != "") txtOicEmailId.Text = ds.Tables[0].Rows[0]["OICEmailID"].ToString(); //else txtOicEmailId.Text = "NA";
                if (ds.Tables[0].Rows[0]["HighPriorityCase_Status"].ToString() != "") txtHighprioritycase.Text = ds.Tables[0].Rows[0]["HighPriorityCase_Status"].ToString(); //else txtHighprioritycase.Text = "NA";
                if (ds.Tables[0].Rows[0]["CaseDetail"].ToString() != "") txtCaseDetail.Text = ds.Tables[0].Rows[0]["CaseDetail"].ToString(); ///else txtCaseDetail.Text = "NA";
                if (ds.Tables[0].Rows[0]["CaseDisposeType"].ToString() != "") { txtcasedisposaltype.Text = ds.Tables[0].Rows[0]["CaseDisposeType"].ToString(); dvCaseDisposalType.Visible = true; } ///else txtcasedisposaltype.Text = "NA";
                if (ds.Tables[0].Rows[0]["OrderSummary"].ToString() != "") { txtOrderSummary.Text = ds.Tables[0].Rows[0]["OrderSummary"].ToString(); dvOrderSummary.Visible = true; }///else txtOrderSummary.Text = "NA";
                if (ds.Tables[0].Rows[0]["Compliance_Status"].ToString() != "") { txtComplianceStatus.Text = ds.Tables[0].Rows[0]["Compliance_Status"].ToString(); Compilance_Div.Visible = true; }// else txtComplianceStatus.Text = "NA";
                if (ds.Tables[0].Rows[0]["CaseStatus"].ToString() != "") txtCaseStatus.Text = ds.Tables[0].Rows[0]["CaseStatus"].ToString();
                txtIntrimOrderEnddate.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["IntrimOrderEndDate"].ToString()) ? ds.Tables[0].Rows[0]["IntrimOrderEndDate"].ToString() : "";
                txtIntirmOrderDate.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["IntrimOrderStartDate"].ToString()) ? ds.Tables[0].Rows[0]["IntrimOrderStartDate"].ToString() : "";
                txtIntrimTimeline.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["IntrimOrderTimeline"].ToString()) ? ds.Tables[0].Rows[0]["IntrimOrderTimeline"].ToString() : "";
                txtIntrimPrevPP.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["IntrimOrderAnyPrevPP"].ToString()) ? ds.Tables[0].Rows[0]["IntrimOrderAnyPrevPP"].ToString() : "";
                txtIntrimOrderSummary.Text = !string.IsNullOrEmpty(ds.Tables[0].Rows[0]["IntrimOrderSummary"].ToString()) ? ds.Tables[0].Rows[0]["IntrimOrderSummary"].ToString() : "";
                string PolicyMeter = ds.Tables[0].Rows[0]["PolicyMeterStatus"].ToString();
                txtPolicymetersts.Text = !string.IsNullOrEmpty(PolicyMeter) ? PolicyMeter : null;
                if (ds.Tables[0].Rows[0]["OICOrderDate"].ToString() != "")
                {
                    txtOICDate.Text = ds.Tables[0].Rows[0]["OICOrderDate"].ToString();
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["CaseStatus"].ToString() == "Dispose")
                    {
                        divCsaeDisposal.Visible = true;
                        grdCaseDisposal.DataSource = ds.Tables[0]; grdCaseDisposal.DataBind();
                    }
                }
                if (ds.Tables[1].Rows.Count > 0) { GrdPetitioner.DataSource = ds.Tables[1]; GrdPetitioner.DataBind(); }
                if (ds.Tables[2].Rows.Count > 0) { GrdPetiAdv.DataSource = ds.Tables[2]; GrdPetiAdv.DataBind(); }
                if (ds.Tables[3].Rows.Count > 0) { GrdDeptAdv.DataSource = ds.Tables[3]; GrdDeptAdv.DataBind(); }
                if (ds.Tables[4].Rows.Count > 0) { GrdResponderDtl.DataSource = ds.Tables[4]; GrdResponderDtl.DataBind(); }
                if (ds.Tables[5].Rows.Count > 0) { GrdDocument.DataSource = ds.Tables[5]; GrdDocument.DataBind(); }
                if (ds.Tables[6].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(ds.Tables[6].Rows[0]["NextHearing_ID"].ToString())) { GrdHearingDtl.DataSource = ds.Tables[6]; GrdHearingDtl.DataBind(); }
                }
                if (ds.Tables[7].Rows.Count > 0) { GrdOldCaseDtl.DataSource = ds.Tables[7]; GrdOldCaseDtl.DataBind(); }
                if (ds.Tables[8].Rows.Count > 0) { grvReturnFileAppeal.DataSource = ds.Tables[8]; grvReturnFileAppeal.DataBind(); } //Add New Grid On Date  09/05/2023 By Bhanu 
            }
            else
            {
                GrdResponderDtl.DataSource = null;
                GrdResponderDtl.DataBind();
                GrdHearingDtl.DataSource = null;
                GrdHearingDtl.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Row Command
    protected void GrdDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink Link = (HyperLink)e.Row.FindControl("hyperLink");
                HyperLink DocWithFolderPath = (HyperLink)e.Row.FindControl("lnkDocPath");
                Label lbldoc = (Label)e.Row.FindControl("lbldoc");

                string name = lbldoc.Text;
                name.StartsWith("https");
                if (name.StartsWith("https") == true)
                    Link.Visible = true;
                else DocWithFolderPath.Visible = true;

            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    #region Btn Back
    protected void lbkBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Page"].ToString() == "1") Response.Redirect("../Legal/CaseListedWeekelyHearing.aspx", false); //Pendig Rpt
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    #endregion
    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    protected void btnCaselisted_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnCaselisted.Text == "Update")
            {
                string NextHearingDate = txtHearingDate.Text != "" ? Convert.ToDateTime(txtHearingDate.Text, cult).ToString("yyyy/MM/dd") : "";


                ds = obj.ByProcedure("Usp_ActionWeekelyHearing", new string[] { "flag","Case_ID", "NextHearing", "Caselisted", "RemarkFromDEO_OIC_Nodal", "DEO_ActionRequired", "RemarkfromJD",
                                                                                 "JD_ActionRequired", "RemarkfromHOD_DPI", "HOD_ActionRequired" ,"CreatedBy", "CreatedByIP" }
               , new string[] { "1",ViewState["ID"].ToString(), NextHearingDate ,ddlcaselisted.SelectedItem.Text, txtRemarkofDEO.Text.Trim(), txtDEOActionRequired.Text.Trim(),
                                                                     txtRemarkfromJD.Text.Trim(), txtJDActionRequired.Text.Trim(), txtRemarkfromHOD_DPI.Text.Trim() ,txtActionRequiredHOD_DPI.Text.Trim()
                                                                     ,ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        ddlcaselisted.ClearSelection();
                        txtRemarkofDEO.Text = "";
                        txtRemarkfromJD.Text = "";
                        txtJDActionRequired.Text = "";
                        txtRemarkfromHOD_DPI.Text = "";
                        txtDEOActionRequired.Text = "";
                        txtActionRequiredHOD_DPI.Text = "";
                        fillAppealGrid();
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                    }
                    else
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void fillAppealGrid()
    {
        try
        {
            GRVAppeal.DataSource = null;
            GRVAppeal.DataBind();
            ds = obj.ByProcedure("Usp_ActionWeekelyHearing", new string[] { "flag", "Case_ID" }
                , new string[] { "2", ViewState["ID"].ToString() }, "dataset");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                GRVAppeal.DataSource = ds.Tables[0];
                GRVAppeal.DataBind();
                Div_Appeal_YesNo.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}