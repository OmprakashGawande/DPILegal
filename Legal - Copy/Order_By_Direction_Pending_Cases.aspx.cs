using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Legal_Order_By_Drection_Pending_Cases : System.Web.UI.Page
{
    DataSet dsCase = null;
    DataTable dtCase = null;
    APIProcedure obj = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_ID"] != null)
        {
            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(Request.QueryString["CaseType"]))
                {
                    BindGrid(Request.QueryString["CaseType"]);
                    spnCaseType.InnerHtml = Request.QueryString["CaseType"] + " Case Type Details";
                }
            }

        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

    }

    //private void GetCourt()
    //{
    //    try
    //    {
    //        dsCase = obj.ByDataSet("select distinct Court from tbl_OrderByDirectionPendingCase order by Court");
    //        if (dsCase.Tables[0].Rows.Count > 0)
    //        {
    //            ddlCourt.DataSource = dsCase;
    //            ddlCourt.DataTextField = "Court";
    //            ddlCourt.DataValueField = "Court";
    //            ddlCourt.DataBind();
    //            ddlCourt.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //        else
    //        {
    //            ddlYear.DataSource = null;
    //            ddlYear.DataBind();
    //            ddlYear.Items.Insert(0, new ListItem("Select", "0"));
    //        }
    //    }
    //    catch (Exception)
    //    {
    //    }

    //}
    protected void BindGrid(string CaseType)
    {
        try
        {
            dsCase = obj.ByDataSet("select distinct UniqueNo,FilingNo,Court,Petitioner,Respondent,RespondentOffice,OICId,OICMobileNo,CaseSubjectId,Remarks,HearingDate,CaseNo,OrderComplianceDate,IsComplaince,CaseSubSubjectId,IsOrderByDirection from tbl_OrderByDirectionPendingCase where CaseType='" + Convert.ToString(CaseType) + "'");
            if (dsCase.Tables[0].Rows.Count > 0)
            {
                ViewState["dt"] = null;
                ViewState["dt"] = dsCase.Tables[0];
                grdCaseTypeDetail.DataSource = dsCase.Tables[0];
                grdCaseTypeDetail.DataBind();


            }
            else
            {
                grdCaseTypeDetail.DataSource = null;
                grdCaseTypeDetail.DataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No record found')", true);
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void grdCaseTypeDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void grdCaseTypeDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdCaseTypeDetail.EditIndex = e.NewEditIndex;
        BindGrid(Request.QueryString["CaseType"]);
        //bindGridData();
    }
    public string convertQuotes(string str)
    {
        return str.Replace("'", "''");
    }


    protected void grdCaseTypeDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        HiddenField hdnUId = (HiddenField)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("hdnUId");
        HiddenField hdnCaseNo = (HiddenField)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("hdnCaseNo");
        TextBox txtOICMobileNo = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtOICMobileNo");
        TextBox txtCaseSubSubjectId = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtCaseSubSubjectId");
        TextBox txtRespondentOffice = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtRespondentOffice");
        // TextBox txtHearingDate = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtHearingDate");
        TextBox txtOrderComplianceDate = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtOrderComplianceDate");
        TextBox txtRemarks = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtRemarks");
        DropDownList ddlOICName = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlOICName");
        DropDownList ddlCaseSubject = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlCaseSubject");
        DropDownList ddlIsComplaince = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlIsComplaince");
        DropDownList ddlIsOrderByDirection = (DropDownList)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("ddlIsOrderByDirection");
        TextBox txtRespondent = (TextBox)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("txtRespondent");
        string strQuery = "update tbl_OrderByDirectionPendingCase set RespondentOffice='" + convertQuotes(txtRespondentOffice.Text.Trim()) + "',OICId=" + ddlOICName.SelectedItem.Value
            + ",CaseSubjectId=" + ddlCaseSubject.SelectedItem.Value + ",Remarks='" + convertQuotes(txtRemarks.Text.Trim()) + "',OICMobileNo='" + convertQuotes(txtOICMobileNo.Text.Trim());
        //if (!string.IsNullOrEmpty(txtHearingDate.Text)) strQuery += "',HearingDate='" + Convert.ToDateTime(txtHearingDate.Text, cult).ToString("yyyy/MM/dd");
        if (!string.IsNullOrEmpty(txtOrderComplianceDate.Text)) strQuery += "',OrderComplianceDate='" + Convert.ToDateTime(txtOrderComplianceDate.Text, cult).ToString("yyyy/MM/dd");
        if (ddlIsComplaince.SelectedIndex > 0) strQuery += "',IsComplaince='" + ddlIsComplaince.SelectedItem.Text;
        if (ddlIsOrderByDirection.SelectedIndex > 0) strQuery += "',IsOrderByDirection='" + ddlIsOrderByDirection.SelectedItem.Text;
        if (!string.IsNullOrEmpty(txtCaseSubSubjectId.Text)) strQuery += "',CaseSubSubjectId='" + txtCaseSubSubjectId.Text;

        strQuery += "',Respondent='" + convertQuotes(txtRespondent.Text.Trim()) + "' where UniqueNo='" + Convert.ToString(hdnUId.Value) + "'";
        obj.ByTextQuery(strQuery);
        grdCaseTypeDetail.EditIndex = -1;
        BindGrid(Request.QueryString["CaseType"]);

        ////HiddenField hdnOICId = (HiddenField)grdCaseTypeDetail.Rows[e.RowIndex].FindControl("hdnOICId");
        //dsCase = obj.ByDataSet("select a.OICName,a.OICEmailID,a.OICMobileNo,b.CirlceName,c.ZoneName,d.OfficeName,e.UserType_Name designation,f.Division_Name from tblOICMaster a " +
        //    "inner join tblCircleMaster b on b.Circle_ID = a.Circle_ID " +
        //    "inner join tblZoneMaster c on c.Zone_ID = a.Zone_ID " +
        //    "inner join  tblOfficeMaster  d on d.Office_Id = a.Office_ID " +
        //    "inner join tblLegal_UserTypeMaster e on e.UserType_Id = a.DesignationID " +
        //    "inner join tblDivisionMaster f on f.Division_ID = a.Division_ID " +
        //    "where OICMaster_ID = " + Convert.ToInt32(ddlOICName.SelectedItem.Value));
        //DataTable dt = (DataTable)dsCase.Tables[0];


        //if (dsCase.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["OICEmailID"].ToString()))
        //{

        //    EmailContent1 ObjEC = new EmailContent1();
        //    ObjEC.OIC_Name = dt.Rows[0]["OICName"].ToString();
        //    ObjEC.OIC_Mobile = dt.Rows[0]["OICMobileNo"].ToString();
        //    ObjEC.OIC_Email = dt.Rows[0]["OICEmailID"].ToString();
        //    ObjEC.OIC_Circle = dt.Rows[0]["CirlceName"].ToString();
        //    ObjEC.OIC_Designation = dsCase.Tables[0].Rows[0]["designation"].ToString();
        //    ObjEC.OIC_Office = dsCase.Tables[0].Rows[0]["OfficeName"].ToString();
        //    ObjEC.OIC_Zone = dsCase.Tables[0].Rows[0]["ZoneName"].ToString();
        //    ObjEC.OIC_Division = dsCase.Tables[0].Rows[0]["Division_Name"].ToString();

        //    ObjEC.Petitioner = grdCaseTypeDetail.Rows[e.RowIndex].Cells[4].Text;
        //    ObjEC.respondent = txtRespondent.Text.Trim();
        //    ObjEC.Case_Number = hdnCaseNo.Value;
        //    ObjEC.Curr_Date = DateTime.Now.ToString("dd-MM-yyyy");
        //    ObjEC.Curr_Year = DateTime.Now.Year.ToString();
        //    ObjEC.Court_Name = grdCaseTypeDetail.Rows[e.RowIndex].Cells[3].Text;
        //    //sendmail(ObjEC, "sfatech.bot@gmail.com");
        //}
        //else
        //{
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alertMessage()", true);
        //}
    }

   
    //private void sendmail(EmailContent1 ObjEC, string CC)
    //{
    //    try
    //    {

    //        string EmailBodyHTMLPath = Server.MapPath("~/HtmlTemplete/Mail_Body_For_OIC.html");

    //        System.IO.StreamReader objReader;
    //        //objReader = new StreamReader(System.IO.Directory.GetCurrentDirectory() + "\\intel\\main.html");
    //        objReader = new StreamReader(EmailBodyHTMLPath);
    //        string content = objReader.ReadToEnd();
    //        objReader.Close();
    //        content = content
    //           .Replace("{{OIC_Name}}", ObjEC.OIC_Name)
    //           .Replace("{{Curr_Year}}", ObjEC.Curr_Year)
    //           .Replace("{{Curr_Date}}", ObjEC.Curr_Date)
    //           .Replace("{{OIC_Desg}}", ObjEC.OIC_Designation)
    //           .Replace("{{Petitioner}}", ObjEC.Petitioner)
    //           .Replace("{{respondent}}", ObjEC.respondent)
    //           .Replace("{{OIC_Office}}", ObjEC.OIC_Office)
    //           .Replace("{{Case_Number}}", ObjEC.Case_Number);

    //        string EmailAttachementHTMLPath = Server.MapPath("~/HtmlTemplete/OIC_Email_Templete.html");

    //        objReader = new StreamReader(EmailAttachementHTMLPath);
    //        string Att_content = objReader.ReadToEnd();
    //        objReader.Close();
    //        Att_content = Att_content
    //           .Replace("{{OIC_Name}}", ObjEC.OIC_Name)
    //           .Replace("{{Curr_Year}}", ObjEC.Curr_Year)
    //           .Replace("{{Curr_Date}}", ObjEC.Curr_Date)
    //           .Replace("{{OIC_Desg}}", ObjEC.OIC_Designation)
    //           .Replace("{{Petitioner}}", ObjEC.Petitioner)
    //           .Replace("{{respondent}}", ObjEC.respondent)
    //           .Replace("{{OIC_Office}}", ObjEC.OIC_Office)
    //           .Replace("{{Case_Number}}", ObjEC.Case_Number)
    //           .Replace("{{Court_Name}}", ObjEC.Court_Name)
    //           .Replace("{{Zone_Name_of_OIC}}", ObjEC.OIC_Zone)
    //           .Replace("{{Circle_Name_of_OIC}}", ObjEC.OIC_Circle)
    //           .Replace("{{Designation}}", ObjEC.OIC_Designation);


    //        //  string AttachedEmailHTMLPath = Server.MapPath("~/HtmlTemplete/OIC_Email_Templete.html");
    //        SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
    //        using (MailMessage mm = new MailMessage(smtpSection.From, ObjEC.OIC_Email.Trim()))
    //        {

    //            #region

    //            // create the HTML to PDF converter
    //            HiQPdf.HtmlToPdf htmlToPdfConverter = new HiQPdf.HtmlToPdf();


    //            // htmlToPdfConverter.BrowserWidth = ;


    //            //if (textBoxBrowserHeight.Text.Length > 0)
    //            //    htmlToPdfConverter.BrowserHeight = int.Parse(textBoxBrowserHeight.Text);


    //            //htmlToPdfConverter.HtmlLoadedTimeout = ;

    //            // set PDF page size and orientation
    //            htmlToPdfConverter.Document.PageSize = HiQPdf.PdfPageSize.A4;
    //            htmlToPdfConverter.Document.PageOrientation = HiQPdf.PdfPageOrientation.Portrait;

    //            // set the PDF standard used by the document
    //            htmlToPdfConverter.Document.PdfStandard = HiQPdf.PdfStandard.PdfA;

    //            // set PDF page margins
    //            htmlToPdfConverter.Document.Margins = new HiQPdf.PdfMargins(10);

    //            // set whether to embed the true type font in PDF
    //            htmlToPdfConverter.Document.FontEmbedding = true;

    //            // set triggering mode; for WaitTime mode set the wait time before convert
    //            //switch (dropDownListTriggeringMode.SelectedValue)
    //            //{
    //            //    case "Auto":
    //            htmlToPdfConverter.TriggerMode = HiQPdf.ConversionTriggerMode.Auto;
    //            //        break;
    //            //    case "WaitTime":
    //            //        htmlToPdfConverter.TriggerMode = ConversionTriggerMode.WaitTime;
    //            //        htmlToPdfConverter.WaitBeforeConvert = int.Parse(textBoxWaitTime.Text);
    //            //        break;
    //            //    case "Manual":
    //            //        htmlToPdfConverter.TriggerMode = ConversionTriggerMode.Manual;
    //            //        break;
    //            //    default:
    //            //        htmlToPdfConverter.TriggerMode = ConversionTriggerMode.Auto;
    //            //        break;
    //            //}

    //            // set header and footer
    //            //  SetHeader(htmlToPdfConverter.Document);
    //            // SetFooter(htmlToPdfConverter.Document);

    //            // set the document security
    //            //  htmlToPdfConverter.Document.Security.OpenPassword = textBoxOpenPassword.Text;
    //            htmlToPdfConverter.Document.Security.AllowPrinting = true;

    //            // set the permissions password too if an open password was set
    //            if (htmlToPdfConverter.Document.Security.OpenPassword != null && htmlToPdfConverter.Document.Security.OpenPassword != String.Empty)
    //                htmlToPdfConverter.Document.Security.PermissionsPassword = htmlToPdfConverter.Document.Security.OpenPassword + "_admin";

    //            // convert HTML to PDF
    //            byte[] pdfBuffer = null;

    //            //if (radioButtonConvertUrl.Checked)
    //            //{
    //            //    // convert URL to a PDF memory buffer
    //            //    string url = textBoxUrl.Text;

    //            //    pdfBuffer = htmlToPdfConverter.ConvertUrlToMemory(url);
    //            //}
    //            //else
    //            //{
    //            // convert HTML code
    //            string htmlCode = Att_content;
    //            string baseUrl = "";

    //            // convert HTML code to a PDF memory buffer
    //            pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);
    //            //}

    //            // inform the browser about the binary data format
    //            HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

    //            // let the browser know how to open the PDF document, attachment or inline, and the file name
    //            HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("{0}; filename=HtmlToPdf.pdf; size={1}",
    //                false ? "inline" : "attachment", pdfBuffer.Length.ToString()));

    //            // write the PDF buffer to HTTP response
    //            HttpContext.Current.Response.BinaryWrite(pdfBuffer);

    //            // call End() method of HTTP response to stop ASP.NET page processing


    //            #endregion

    //            mm.Subject = "OIC Successfully Mapped";
    //            mm.Body = content;
    //            mm.IsBodyHtml = true;
    //            mm.CC.Add(CC);
    //            mm.Attachments.Add(new Attachment(new MemoryStream(pdfBuffer), "OIC(" + ObjEC.OIC_Name + "_Mapped_With_Case_No_)" + ObjEC.Case_Number + ".pdf"));
    //            SmtpClient smtp = new SmtpClient();
    //            smtp.Host = smtpSection.Network.Host;
    //            smtp.EnableSsl = smtpSection.Network.EnableSsl;
    //            NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
    //            smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
    //            smtp.Credentials = networkCred;
    //            smtp.Port = smtpSection.Network.Port;
    //            smtp.Send(mm);
    //            HttpContext.Current.Response.End();
    //            Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alert('Email sent.');", true);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //}

    private void bindGridData()
    {
        if (ViewState["dt"] != null)
        {
            dtCase = (DataTable)ViewState["dt"];
            grdCaseTypeDetail.DataSource = dtCase;
            grdCaseTypeDetail.DataBind();
        }
    }

    protected void grdCaseTypeDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdCaseTypeDetail.EditIndex = -1;
        BindGrid(Request.QueryString["CaseType"]);
    }



    protected void grdCaseTypeDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        int OICId = 0;
        int CaseSubjectId = 0;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) <= 0)
            {
                Label lblOICName = e.Row.FindControl("lblOICName") as Label;
                Label lblOICMobileNo = e.Row.FindControl("lblOICMobileNo") as Label;
                Label lblCaseSubjectId = e.Row.FindControl("lblCaseSubjectId") as Label;
                Label lblCaseSubSubjectId = e.Row.FindControl("lblCaseSubSubjectId") as Label;

                if (lblOICName.Text != "" && lblOICMobileNo.Text != "" && lblOICName.Text != "0" )
                {
                    OICId = Convert.ToInt32(lblOICName.Text);
                    dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where OICMaster_ID=" + OICId + " and Isactive=1");
                    if (dsCase.Tables[0].Rows.Count > 0)
                    {
                        lblOICName.Text = dsCase.Tables[0].Rows[0]["OICName"].ToString();
                        lblOICMobileNo.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
                    }
                }
                if (lblCaseSubjectId.Text != "" && lblCaseSubjectId.Text != "0")
                {
                    CaseSubjectId = Convert.ToInt32(lblCaseSubjectId.Text);
                    dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubject From tbl_LegalMstCaseSubject where CaseSubjectID=" + CaseSubjectId);
                    if (dsCase.Tables[0].Rows.Count > 0)
                    {
                        lblCaseSubjectId.Text = dsCase.Tables[0].Rows[0]["CaseSubject"].ToString();
                    }
                }
                if (lblCaseSubSubjectId.Text != "0" && lblCaseSubSubjectId.Text != "")
                {
                    HiddenField hdnUId = e.Row.FindControl("hdnUId") as HiddenField;
                    dsCase = obj.ByDataSet("select CaseSubSubject from tbl_CaseSubSubjectMaster where CaseSubSubjId=" + Convert.ToInt32(lblCaseSubSubjectId.Text));
                    if (dsCase.Tables.Count > 0 && !string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["CaseSubSubject"].ToString()) )
                    {
                        lblCaseSubSubjectId.Text = dsCase.Tables[0].Rows[0]["CaseSubSubject"].ToString();
                    }
                }
            }
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {

                HiddenField hdnUId = e.Row.FindControl("hdnUId") as HiddenField;
                dsCase = obj.ByDataSet("select distinct UniqueNo,FilingNo,Court,Petitioner,Respondent,RespondentOffice,OICId,OICMobileNo,CaseSubjectId,Remarks,IsComplaince,CaseSubSubjectId,IsOrderByDirection from tbl_OrderByDirectionPendingCase where CaseType='" + Convert.ToString(Request.QueryString["CaseType"]) + "' and UniqueNo='" + hdnUId.Value + "'");
                if (dsCase.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["OICId"].ToString()) && !string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["CaseSubjectId"].ToString()))
                {
                    OICId = Convert.ToInt32(dsCase.Tables[0].Rows[0]["OICId"]);
                    CaseSubjectId = Convert.ToInt32(dsCase.Tables[0].Rows[0]["CaseSubjectId"]);
                    DropDownList ddlIsComplaince = e.Row.FindControl("ddlIsComplaince") as DropDownList;
                    DropDownList ddlIsOrderByDirection = e.Row.FindControl("ddlIsOrderByDirection") as DropDownList;
                    if (!string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["IsComplaince"].ToString()))
                        ddlIsComplaince.Items.FindByValue(dsCase.Tables[0].Rows[0]["IsComplaince"].ToString()).Selected = true;
                    if (!string.IsNullOrEmpty(dsCase.Tables[0].Rows[0]["IsOrderByDirection"].ToString()))
                        ddlIsOrderByDirection.Items.FindByValue(dsCase.Tables[0].Rows[0]["IsOrderByDirection"].ToString()).Selected = true;
                }

                //int index = e.Row.RowIndex;
                Label lblOICName = e.Row.FindControl("lblOICName") as Label;


                // TextBox txtOICMobileNo = e.Row.FindControl("txtOICMobileNo") as TextBox;
                //dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where Isactive=1");
                //if (dsCase.Tables[0].Rows.Count > 0)
                //{
                //    txtOICMobileNo.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
                //}

                DropDownList ddlOICName = e.Row.FindControl("ddlOICName") as DropDownList;


                dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where Isactive=1");
                if (dsCase.Tables[0].Rows.Count > 0)
                {
                    ddlOICName.DataSource = dsCase.Tables[0];
                    ddlOICName.DataTextField = "OICName";
                    ddlOICName.DataValueField = "OICMaster_ID";
                    ddlOICName.DataBind();
                    ddlOICName.Items.Insert(0, new ListItem("Select", "0"));
                    ddlOICName.Items.FindByValue(OICId.ToString()).Selected = true;
                }
                else
                {
                    ddlOICName.DataSource = null;
                    ddlOICName.DataBind();
                    ddlOICName.Items.Insert(0, new ListItem("Select", "0"));
                }


                DropDownList ddlCaseSubject = (DropDownList)e.Row.FindControl("ddlCaseSubject");
                dsCase = obj.ByDataSet("select CaseSubjectID,CaseSubject From tbl_LegalMstCaseSubject");
                if (dsCase.Tables[0].Rows.Count > 0)
                {

                    ddlCaseSubject.DataSource = dsCase.Tables[0];
                    ddlCaseSubject.DataTextField = "CaseSubject";
                    ddlCaseSubject.DataValueField = "CaseSubjectID";
                    ddlCaseSubject.DataBind();
                    ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
                    ddlCaseSubject.Items.FindByValue(CaseSubjectId.ToString()).Selected = true;
                }
                else
                {
                    ddlCaseSubject.DataSource = null;
                    ddlCaseSubject.DataBind();
                    ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
                }

                ///TextBox txtCaseSubSubjectId = (TextBox)e.Row.FindControl("txtCaseSubSubjectId");
                //dsCase = obj.ByDataSet("select * from tbl_CaseSubSubjectMaster where CaseSubSubjId" + Convert.ToInt32(dsCase.Tables[0].Rows[0]["CaseSubSubjectId"]));
                //txtCaseSubSubjectId.Text = dsCase.Tables[0].Rows[0]["CaseSubSubject"].ToString();




            }
        }
    }

    protected void ddlOICName_TextChanged(object sender, EventArgs e)
    {
        GridViewRow grow = (GridViewRow)((Control)sender).NamingContainer;
        DropDownList ddlOICName = (DropDownList)grow.FindControl("ddlOICName");
        DropDownList ddl_state = (DropDownList)grow.FindControl("ddl_state");
        int OICId = Convert.ToInt32(ddlOICName.SelectedValue);
        // Label lblOICMobileNo = (Label)gvRow.FindControl("lblOICMobileNo");
        TextBox txtOICMobileNo = grow.FindControl("txtOICMobileNo") as TextBox;

        dsCase = obj.ByDataSet("select OICMaster_ID,OICName,OICEmailID,OICMobileNo,Office_ID,Zone_ID,Circle_ID,Division_ID from tblOICMaster where OICMaster_ID=" + OICId);
        if (dsCase.Tables[0].Rows.Count > 0)
        {
            txtOICMobileNo.Text = dsCase.Tables[0].Rows[0]["OICMobileNo"].ToString();
        }
    }

    protected void grdCaseTypeDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCaseTypeDetail.PageIndex = e.NewPageIndex;
        BindGrid(Request.QueryString["CaseType"]);
    }
}

