using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_EditWPDisposedCasesAnalysis : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            string multiCharString = Request.QueryString.ToString();
            string[] multiArray = multiCharString.Split(new Char[] { '=', '&' });
            string CaseID = obj.Decrypt(Request.QueryString["CaseID"].ToString());
            string Uniqueno = obj.Decrypt(Request.QueryString["UniqueNO"].ToString());
            string CaseType = obj.Decrypt(Request.QueryString["CaseType"].ToString());
            ViewState["ID"] = CaseID;
            ViewState["UniqueNO"] = Uniqueno;
            ViewState["CaseType"] = CaseType;
            ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
            ViewState["Office_Id"] = Session["Office_Id"].ToString();
            BindCourtName();
            FillCasetype();
            FillYear();
            FillCaseSubject();
            BindDetails(sender, e);


        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void FillYear()
    {
        ddlCaseYear.Items.Clear();
        for (int i = 1950; i <= DateTime.Now.Year; i++)
        {
            ddlCaseYear.Items.Add(i.ToString());
        }
        ddlCaseYear.Items.Insert(0, new ListItem("Select", "0"));

    }
    protected void BindCourtName()
    {
        try
        {
            ddlCourtType.Items.Clear();
            DataSet dsCourt = obj.ByProcedure("USP_Legal_Select_CourtType", new string[] { }, new string[] { }, "dataset");
            if (dsCourt != null && dsCourt.Tables[0].Rows.Count > 0)
            {
                ddlCourtType.DataTextField = "CourtTypeName";
                ddlCourtType.DataValueField = "CourtType_ID";
                ddlCourtType.DataSource = dsCourt;
                ddlCourtType.DataBind();

            }
            ddlCourtType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlCourtType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlCourtLocation.Items.Clear();
            DataSet dsCourt = obj.ByDataSet("select  CT.District_Id, District_Name  from tbl_LegalCourtType CT INNER Join Mst_District DM on DM.District_ID = CT.District_Id where CourtType_ID = " + ddlCourtType.SelectedValue);
            if (dsCourt != null && dsCourt.Tables[0].Rows.Count > 0)
            {
                ddlCourtLocation.DataTextField = "District_Name";
                ddlCourtLocation.DataValueField = "District_Id";
                ddlCourtLocation.DataSource = dsCourt;
                ddlCourtLocation.DataBind();
            }
            ddlCourtLocation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillCasetype()
    {
        try
        {
            ddlCasetype.Items.Clear();
            ds = obj.ByDataSet("select Casetype_ID, Casetype_Name from  tbl_Legal_Casetype");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCasetype.DataTextField = "Casetype_Name";
                ddlCasetype.DataValueField = "Casetype_ID";
                ddlCasetype.DataSource = ds;
                ddlCasetype.DataBind();
            }
            ddlCasetype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillCaseSubject()
    {
        try
        {
            ddlCaseSubject.Items.Clear();
            DataSet ds_SC = obj.ByDataSet("SELECT CaseSubject, CaseSubjectID FROM tbl_LegalMstCaseSubject");
            if (ds_SC != null && ds_SC.Tables[0].Rows.Count > 0)
            {
                ddlCaseSubject.DataTextField = "CaseSubject";
                ddlCaseSubject.DataValueField = "CaseSubjectID";
                ddlCaseSubject.DataSource = ds_SC;
                ddlCaseSubject.DataBind();
            }
            ddlCaseSubject.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }

    protected void FillSubSubjectID()
    {
        try
        {
            ddlCaseSubSubject.Items.Clear();
            DataSet DsSubs = obj.ByDataSet("select CaseSubSubj_Id, CaseSubSubject from tbl_CaseSubSubjectMaster where CaseSubjectID=" + ddlCaseSubject.SelectedValue);
            if (DsSubs != null && DsSubs.Tables[0].Rows.Count > 0)
            {
                ddlCaseSubSubject.DataTextField = "CaseSubSubject";
                ddlCaseSubSubject.DataValueField = "CaseSubSubj_Id";
                ddlCaseSubSubject.DataSource = DsSubs;
                ddlCaseSubSubject.DataBind();
            }
            ddlCaseSubSubject.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void BindDetails(object sender, EventArgs e)
    {
        try
        {
            DataSet ds1 = new DataSet();
            //lblFlag.Text = "";
            ds1 = obj.ByProcedure("USP_Select_NewCaseRegis", new string[] { "Case_ID" }
                , new string[] { ViewState["ID"].ToString() }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                lblCaseNo.Text = ds1.Tables[0].Rows[0]["CaseNo"].ToString();
                //lblFlag.Text = ds1.Tables[0].Rows[0]["Flag"].ToString();
                txtPetitionerName.Text = ds1.Tables[3].Rows[0]["PetitionerName"].ToString();
                txtRespondentName.Text = ds1.Tables[1].Rows[0]["RespondentName"].ToString();
                txtCaseDisposeType.Text = ds1.Tables[6].Rows[0]["CaseDisposeType"].ToString();
                txtOrderWithDirection.Text = ds1.Tables[6].Rows[0]["OrderWithDirection"].ToString();
                txtCaseDisposal_Date.Text = ds1.Tables[6].Rows[0]["CaseDisposal_Date"].ToString();
                txtDateAfter90Days.Text = ds1.Tables[6].Rows[0]["DateAfter90Days"].ToString();
                txtRemainingDays.Text = ds1.Tables[6].Rows[0]["RemainingDays"].ToString();
                txtOrderSummary.Text = ds1.Tables[6].Rows[0]["OrderSummary"].ToString();
                BindCourtName();
                ddlCourtType.ClearSelection();
                if (ddlCourtType.Items.Count > 0)
                {

                    ddlCourtType.Items.FindByValue(ds1.Tables[0].Rows[0]["CourtType_Id"].ToString().Trim()).Selected = true; ddlCourtType.Enabled = false;
                }
                ddlCasetype.ClearSelection();
                FillCasetype();
                if (ds1.Tables[0].Rows[0]["Casetype_ID"].ToString() != "")
                {
                    ddlCasetype.Items.FindByValue(ds1.Tables[0].Rows[0]["Casetype_ID"].ToString()).Selected = true;
                    ddlCasetype.Enabled = false;

                }
                ddlCaseYear.ClearSelection();
                FillYear();
                if (ddlCaseYear.Items.Count > 0) ddlCaseYear.Items.FindByText(ds1.Tables[0].Rows[0]["CaseYear"].ToString().Trim()).Selected = true; ddlCaseYear.Enabled = false;

                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["CourtLocation_Id"].ToString()))
                {
                    ddlCourtLocation.ClearSelection();
                    ddlCourtType_SelectedIndexChanged(sender, e);
                    ddlCourtLocation.Items.FindByValue(ds1.Tables[0].Rows[0]["CourtLocation_Id"].ToString()).Selected = true;
                    ddlCourtLocation.Enabled = false;
                }
                ddlCaseSubject.Enabled = false;
                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["CaseSubjectID"].ToString()))
                {
                    ddlCaseSubject.ClearSelection();
                    ddlCaseSubject.Items.FindByValue(ds1.Tables[0].Rows[0]["CaseSubjectID"].ToString().Trim()).Selected = true;

                }
                ddlCaseSubSubject.Enabled = false;
                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["CaseSubSubj_Id"].ToString()))
                {
                    ddlCaseSubject_SelectedIndexChanged(sender, e);
                    ddlCaseSubSubject.ClearSelection();


                    int SubSubject = ReutrnSubSubjectID().AsEnumerable().Where(r => r.Field<int>("CaseSubSubj_Id") == Convert.ToInt32(ds1.Tables[0].Rows[0]["CaseSubSubj_Id"])).Select(y => y.Field<int>("CaseSubSubj_Id")).FirstOrDefault();
                    if (SubSubject != 0)
                    {
                        if (SubSubject == Convert.ToInt32(ds1.Tables[0].Rows[0]["CaseSubSubj_Id"]))
                        {
                            ddlCaseSubject_SelectedIndexChanged(sender, e);
                            ddlCaseSubSubject.ClearSelection();
                            ddlCaseSubSubject.Items.FindByValue(ds1.Tables[0].Rows[0]["CaseSubSubj_Id"].ToString()).Selected = true;
                        }
                    }
                    else
                    {
                        ddlCaseSubSubject.DataBind();
                    }

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    public DataTable ReutrnSubSubjectID()
    {
        try
        {
            Helper SubSubject = new Helper();
            ddlCaseSubSubject.Items.Clear();

            //   DataTable dtOic = oic.GetOIC(ddlCourtType.SelectedValue) as DataTable;
            DataSet DsSubsubject = obj.ByDataSet("select CaseSubSubj_Id, CaseSubSubject from tbl_CaseSubSubjectMaster where CaseSubjectID=" + ddlCaseSubject.SelectedValue);
            DataTable dt = new DataTable();
            if (DsSubsubject != null && DsSubsubject.Tables[0].Rows.Count > 0)
                dt = DsSubsubject.Tables[0];
            return dt;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            return null;
        }
    }
    protected void ddlCaseSubject_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            FillSubSubjectID();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }

    protected void ddlfavGovtAga_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlfavGovtAga.SelectedValue == "2")
            {
                DivFirst.Visible = true;
            }
            else
            {
                DivFirst.Visible = false;
            }

            DivSecond.Visible = false;
            Divthird.Visible = false; Divfourth.Visible = false;
            DivSecond.Visible = false; Divthird.Visible = false;
            Divfourth.Visible = false; Divfifth.Visible = false;
            Divsixth.Visible = false; Divseventh.Visible = false;
            Diveighth.Visible = false; Divninth.Visible = false;
            Divtenth.Visible = false; Diveleventh.Visible = false;

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void ddlAgainstGovt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAgainstGovt.SelectedValue == "1")
        {
            DivSecond.Visible = true; Divthird.Visible = true;
            Divfourth.Visible = false; Divfifth.Visible = false;
            Divsixth.Visible = false; Divseventh.Visible = false;
            Diveighth.Visible = false; Divninth.Visible = false;
            Divtenth.Visible = false; Diveleventh.Visible = false;
        }
        else if (ddlAgainstGovt.SelectedValue == "2")
        {
            Divfourth.Visible = true; Divfifth.Visible = true;
            Divsixth.Visible = true; Divseventh.Visible = true;
            Diveighth.Visible = true; Divninth.Visible = true;
            Divtenth.Visible = true; Diveleventh.Visible = true;
            DivSecond.Visible = false; Divthird.Visible = false;
        }
        else
        {
            DivSecond.Visible = false; Divthird.Visible = false;
            Divfourth.Visible = false; Divfifth.Visible = false;
            Divsixth.Visible = false; Divseventh.Visible = false;
            Diveighth.Visible = false; Divninth.Visible = false;
            Divtenth.Visible = false; Diveleventh.Visible = false;
        }


    }

    protected void txtWAapplicationdatefromlegaldept_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime waApplicationDateAtDPI, waApplicationDateFromLegalDept;

            // Parse the dates from the textboxes
            bool isDPIValid = DateTime.TryParseExact(txtWAapplicationdateatDPI.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out waApplicationDateAtDPI);
            bool isLegalDeptValid = DateTime.TryParseExact(txtWAapplicationdatefromlegaldept.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out waApplicationDateFromLegalDept);

            if (isDPIValid && isLegalDeptValid)
            {
                // Calculate the difference in days
                TimeSpan dateDifference = waApplicationDateAtDPI - waApplicationDateFromLegalDept;
                int totalDays = Math.Abs(dateDifference.Days);

                // Display the result in a label or another control
                txtDaystakenforWAPermission.Text = Convert.ToString(totalDays);
            }
            else
            {
                txtDaystakenforWAPermission.Text = "Please enter valid dates.";
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void txtWANumberandallotteddatefromHC_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime waApplicationDateAtDPI, WANumberandallotteddatefromHC;

            // Parse the dates from the textboxes
            bool isDPIValid = DateTime.TryParseExact(txtWAapplicationdatefromlegaldept.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out waApplicationDateAtDPI);
            bool isWANumberandallotteddatefromHCValid = DateTime.TryParseExact(txtWANumberandallotteddatefromHC.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out WANumberandallotteddatefromHC);

            if (isDPIValid && isWANumberandallotteddatefromHCValid)
            {
                // Calculate the difference in days
                TimeSpan dateDifference = waApplicationDateAtDPI - WANumberandallotteddatefromHC;
                int totalDays = Math.Abs(dateDifference.Days);

                // Display the result in a label or another control
                txtDaystakentofiletheapplication.Text = Convert.ToString(totalDays);
            }
            else
            {
                txtDaystakentofiletheapplication.Text = "Please enter valid dates.";
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void txtWAapplicationfillingdateatHC_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime WANumberandallotteddatefromHC, WAapplicationfillingdateatHC;

            // Parse the dates from the textboxes
            bool isDPIValid = DateTime.TryParseExact(txtWANumberandallotteddatefromHC.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out WANumberandallotteddatefromHC);
            bool isWAapplicationfillingdateatHCValid = DateTime.TryParseExact(txtWAapplicationfillingdateatHC.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out WAapplicationfillingdateatHC);

            if (isDPIValid && isWAapplicationfillingdateatHCValid)
            {
                // Calculate the difference in days
                TimeSpan dateDifference = WANumberandallotteddatefromHC - WAapplicationfillingdateatHC;
                int totalDays = Math.Abs(dateDifference.Days);

                // Display the result in a label or another control
                txtFileWAafterrecperfromgovt.Text = Convert.ToString(totalDays);
            }
            else
            {
                txtFileWAafterrecperfromgovt.Text = "Please enter valid dates.";
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnCaseDisposalAnalysis_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            string Complydate = txtComplydate.Text != "" ? Convert.ToDateTime(txtComplydate.Text, cult).ToString("yyyy/MM/dd") : "";
            string WAapplicationdateatDPI = txtWAapplicationdateatDPI.Text != "" ? Convert.ToDateTime(txtWAapplicationdateatDPI.Text, cult).ToString("yyyy/MM/dd") : "";
            string WAapplicationdatefromlegaldept = txtWAapplicationdatefromlegaldept.Text != "" ? Convert.ToDateTime(txtWAapplicationdatefromlegaldept.Text, cult).ToString("yyyy/MM/dd") : "";
            string WANumberandallotteddatefromHC = txtWANumberandallotteddatefromHC.Text != "" ? Convert.ToDateTime(txtWANumberandallotteddatefromHC.Text, cult).ToString("yyyy/MM/dd") : "";
            string WAapplicationfillingdateatHC = txtWAapplicationfillingdateatHC.Text != "" ? Convert.ToDateTime(txtWAapplicationfillingdateatHC.Text, cult).ToString("yyyy/MM/dd") : "";

            ds = obj.ByProcedure("Usp_UpdateWPDisposedCasesAnalysis", new string[] { "UniqueNo", "Case_Id", "favourofGovtorAgainstGovtID", "AgainstGovt", "Complydate", "PresentStatusRemark",
                "WAapplicationdateatDPI", "WAapplicationdatefromlegaldept",
                "WAapplicationfillingdateatHC", "WANumberandallotteddatefromHC",
                "DaystakenforWAPermission", "Daystakentofiletheapplication", "FileWAafterrecperfromgovt", "Remark" },
                                     new string[] { ViewState["UniqueNO"].ToString(), ViewState["ID"].ToString(), ddlfavGovtAga.SelectedValue, ddlAgainstGovt.SelectedValue,
                                         Complydate, txtPresentStatusRemark.Text.Trim(),
                                         WAapplicationdateatDPI, WAapplicationdatefromlegaldept, WANumberandallotteddatefromHC, WAapplicationfillingdateatHC,
                                         txtDaystakenforWAPermission.Text, txtDaystakentofiletheapplication.Text, txtFileWAafterrecperfromgovt.Text,txtRemark.Text }, "Update");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    BindDetails(sender, e);
                    Clear();
                    ddlfavGovtAga_SelectedIndexChanged(sender, e);
                    ddlAgainstGovt_SelectedIndexChanged(sender, e);

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                }
                else
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void Clear()
    {
        ddlfavGovtAga.ClearSelection();
        ddlAgainstGovt.ClearSelection();
        txtComplydate.Text = "";
        txtPresentStatusRemark.Text = "";
        txtWAapplicationdateatDPI.Text = "";
        txtWAapplicationdatefromlegaldept.Text = "";
        txtWANumberandallotteddatefromHC.Text = "";
        txtWAapplicationfillingdateatHC.Text = "";
        txtDaystakenforWAPermission.Text = "";
        txtDaystakentofiletheapplication.Text = "";
        txtFileWAafterrecperfromgovt.Text = "";
        txtRemark.Text = "";
    }

}