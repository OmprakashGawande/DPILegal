using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_EditDisposeCase : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    string oldOICDoc = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {

                string multiCharString = Request.QueryString.ToString();
                string[] multiArray = multiCharString.Split(new Char[] { '=', '&' });
                //string CaseID = Decrypt(HttpUtility.UrlDecode(multiArray[1]));
                string CaseID = obj.Decrypt(Request.QueryString["CaseID"].ToString());
                string Uniqueno = obj.Decrypt(Request.QueryString["UniqueNO"].ToString());
                string CaseType = obj.Decrypt(Request.QueryString["CaseType"].ToString());
                //string Uniqueno = Decrypt(HttpUtility.UrlDecode(multiArray[3]));

                divReplyDate.Visible = false;
                divReplyRemark.Visible = false;
                ViewState["ID"] = CaseID;
                ViewState["UniqueNO"] = Uniqueno;
                ViewState["CaseType"] = CaseType;
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                // ViewState["CaseType"] = Session["CaseType"].ToString();
                Session["PAGETOKEN"] = Server.UrlEncode(System.DateTime.Now.ToString());
                FillOldCaseYear();
                FillYear();
                BindCourtName();
                BindDisposalType();
                FillParty();
                FillCaseSubject();
                CaseDisposeStatus(); // by deafult Case Dispose on NO text.
                FillDesignation();
                BindOfficeType();
                FillCasetype();
                FillDepartment();

                BindDetails(sender, e);
                ManagVisiblity();
                fillWPRemainingdays();
                //FieldViewOldCaseDtl.Visible = false;
                BindddlCaseDisposd_yes();
                FillOrderWithDirection();
                FillSection();
                if (CaseType == "1")
                {
                    Div_PetitionerAdv.Visible = false;
                    FieldViewDeptAdvDtl.Visible = false;
                }
                else
                {
                    Div_PetitionerAdv.Visible = true;
                    FieldViewDeptAdvDtl.Visible = true;
                }


            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }

    }
    protected void CaseDisposeStatus() // Case Dispose By Default On NO condtiton
    {
        foreach (ListItem item in rdCaseDispose.Items)
        {
            if (item.Text.Contains("No"))
            {
                item.Selected = true;
                break;
            }
            caseDisposeYes.Visible = false;
            OrderBy1.Visible = false;
            OrderBy2.Visible = false;
            HearingDtl_CaseDispose.Visible = false;
            //CimplianceSt_Div.Visible = false;
        }
    }
    protected void fillWPRemainingdays()
    {
        try
        {
            ds = obj.ByProcedure("Usp_WPDisposedCase_Get", new string[] { "Case_ID" }, new string[] { ViewState["ID"].ToString() }, "Dataset");

            if (ds != null)
            {
                lblremainingdays.Text = ds.Tables[0].Rows[0]["DateAfter90Days"].ToString();
                lblcasedisposeldate.Text = ds.Tables[0].Rows[0]["CaseDisposal_Date"].ToString();
                if (ds.Tables[1].Rows[0]["RemainingDays"].ToString() == "")
                {
                    lblTotalDays.Text = "NULL";
                }
                else
                {
                    lblTotalDays.Text = ds.Tables[1].Rows[0]["RemainingDays"].ToString();

                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillDitrict()
    {
        try
        {
            ddlDistrict.Items.Clear(); ddlPetitionerPresentDistrict.Items.Clear();
            ds = obj.ByDataSet("select DM.District_ID, District_Name from  Mst_District DM inner join tbl_DistrictCourtMaping_Mst CMM on DM.District_ID=CMM.District_ID " +
            "where CMM.CourtName_ID=" + ddlCourtType.SelectedValue + "order by District_ID asc");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDistrict.DataTextField = "District_Name";
                ddlDistrict.DataValueField = "District_ID";
                ddlDistrict.DataSource = ds;
                ddlDistrict.DataBind();
            }
            ddlPetitionerPresentDistrict.DataTextField = "District_Name";
            ddlPetitionerPresentDistrict.DataValueField = "District_ID";
            ddlPetitionerPresentDistrict.DataSource = ds;
            ddlPetitionerPresentDistrict.DataBind();
            ddlPetitionerPresentDistrict.Items.Insert(0, new ListItem("Select", "0"));
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    private void ManagVisiblity()
    {
        try
        {
            divCaseRelate.Visible = false;
            if (Session["Role_ID"].ToString() == "1")
            {

                ddlCaseSubject.Enabled = true;
                ddlCaseSubSubject.Enabled = true;
                //ddlDepartment.Enabled = true;
                ddlOicName.Enabled = true;
                ddlHOD.Enabled = true;
                txtOicMobileNo.Enabled = true;
                txtOicEmailId.Enabled = true;
                ddlHighprioritycase.Enabled = true;
                ddlParty.Enabled = true;
                divRealtion.Visible = true;


            }
            else if (Session["Role_ID"].ToString() == "3")
            {
                ddlCaseSubject.Enabled = false;
                ddlCaseSubSubject.Enabled = false;
                //ddlDepartment.Enabled = false;
                ddlOicName.Enabled = false;
                txtOicMobileNo.Enabled = false;
                ddlHOD.Enabled = false;
                txtOicEmailId.Enabled = false;
                ddlHighprioritycase.Enabled = false;
                ddlParty.Enabled = false;
                txtPetiName.Enabled = false;
                ddlPetiDesigNation.Enabled = false;
                txtPetiMobileNo.Enabled = false;
                txtPetiAddRess.Enabled = false;
                divRealtion.Visible = false;
                ddlDistrict.Enabled = false;
            }
            else if (Session["Role_ID"].ToString() == "4")
            {
                ddlOicName.Enabled = true;
                ddlDistrict.Enabled = false;
                //ddlDepartment.Enabled = false;
                divCaseRelate.Visible = false;
                divRealtion.Visible = false;
                ddlCaseSubject.Enabled = false;
                ddlCaseSubSubject.Enabled = false;
                ddlHOD.Enabled = false;
                ddlParty.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["UPAGETOKEN"] = Session["PAGETOKEN"];
    }
    protected void BindDisposalType()
    {
        try
        {
            ddlDisponsType.Items.Clear();
            ds = obj.ByDataSet("select CaseDisposeType_Id, CaseDisposeType from tbl_LegalCaseDisposeType");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDisponsType.DataTextField = "CaseDisposeType";
                ddlDisponsType.DataValueField = "CaseDisposeType_Id";
                ddlDisponsType.DataSource = ds;
                ddlDisponsType.DataBind();
            }
            ddlDisponsType.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void BindOfficeType()
    {
        try
        {
            ddlResOfficetypeName.Items.Clear();
            ds = obj.ByProcedure("USP_Select_Officetype", new string[] { }, new string[] { }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlResOfficetypeName.DataTextField = "OfficeType_Name";
                ddlResOfficetypeName.DataValueField = "OfficeType_Id";
                ddlResOfficetypeName.DataSource = ds;
                ddlResOfficetypeName.DataBind();
            }
            ddlResOfficetypeName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    protected void FillOicName()
    {
        try
        {
            Helper oic = new Helper();
            ddlOicName.Items.Clear();
            DataTable dtOic = oic.GetOIC(ddlCourtType.SelectedValue) as DataTable;
            if (dtOic != null && dtOic.Rows.Count > 0)
            {
                ddlOicName.DataTextField = "OICName";
                ddlOicName.DataValueField = "OICMaster_ID";
                ddlOicName.DataSource = dtOic;
                ddlOicName.DataBind();
            }
            ddlOicName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
    private void FillDepartment()
    {
        try
        {
            ddlHOD.Items.Clear(); ddlResDepartment.Items.Clear();
            DataSet dsDepart = obj.ByDataSet("select Dept_ID,Dept_Name from tblDepartmentMaster where Isactive=1");
            DataSet dsHOD = obj.ByDataSet("select HOD_Id,HodName from tblHODMaster where Isactive=1");
            if (dsDepart.Tables.Count > 0 && dsDepart.Tables[0].Rows.Count > 0)
            {
                ddlHOD.DataSource = dsHOD.Tables[0];
                ddlHOD.DataTextField = "HodName";
                ddlHOD.DataValueField = "HOD_Id";
                ddlHOD.DataBind();

                //ddlDepartment.DataSource = dsDepart.Tables[0];
                //ddlDepartment.DataTextField = "Dept_Name";
                //ddlDepartment.DataValueField = "Dept_ID";
                //ddlDepartment.DataBind();
                //ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
                ddlResDepartment.DataSource = dsDepart.Tables[0];
                ddlResDepartment.DataTextField = "Dept_Name";
                ddlResDepartment.DataValueField = "Dept_ID";
                ddlResDepartment.DataBind();
                ddlResDepartment.Items.Insert(0, new ListItem("Select", "0"));

            }
            else
            {
                ddlHOD.DataSource = null;
                ddlHOD.DataBind();
                ddlHOD.Items.Insert(0, new ListItem("Select", "0"));
                //ddlDepartment.DataSource = null;
                //ddlDepartment.DataBind();
                //ddlDepartment.Items.Insert(0, new ListItem("Select", "0"));
                ddlResDepartment.DataSource = null;
                ddlResDepartment.DataBind();
                ddlResDepartment.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillOldCaseYear()
    {
        try
        {
            ddloldCaseYear.Items.Clear();
            DataSet dsCase = obj.ByDataSet("with yearlist as (select 1950 as year union all select yl.year + 1 as year from yearlist yl where yl.year + 1 <= YEAR(GetDate())) select year from yearlist order by year");
            if (dsCase.Tables.Count > 0 && dsCase.Tables[0].Rows.Count > 0)
            {
                ddloldCaseYear.DataSource = dsCase.Tables[0];
                ddloldCaseYear.DataTextField = "year";
                ddloldCaseYear.DataValueField = "year";
                ddloldCaseYear.DataBind();
                ddloldCaseYear.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddloldCaseYear.DataSource = null;
                ddloldCaseYear.DataBind();
                ddloldCaseYear.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void BiunOicName()
    {
        try
        {
            Helper oic = new Helper();
            ddlOicName.Items.Clear();
            DataTable dtOic = oic.GetOIC(ddlCourtType.SelectedValue) as DataTable;
            if (dtOic != null && dtOic.Rows.Count > 0)
            {
                ddlOicName.DataTextField = "OICName";
                ddlOicName.DataValueField = "OICMaster_ID";
                ddlOicName.DataSource = dtOic;
                ddlOicName.DataBind();
            }
            ddlOicName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void BindCourtName()
    {
        try
        {
            ddlCourtType.Items.Clear(); ddloldCaseCourt.Items.Clear();
            DataSet dsCourt = obj.ByProcedure("USP_Legal_Select_CourtType", new string[] { }, new string[] { }, "dataset");
            if (dsCourt != null && dsCourt.Tables[0].Rows.Count > 0)
            {
                ddlCourtType.DataTextField = "CourtTypeName";
                ddlCourtType.DataValueField = "CourtType_ID";
                ddlCourtType.DataSource = dsCourt;
                ddlCourtType.DataBind();

                ddloldCaseCourt.DataTextField = "CourtTypeName";
                ddloldCaseCourt.DataValueField = "CourtType_ID";
                ddloldCaseCourt.DataSource = dsCourt;
                ddloldCaseCourt.DataBind();
            }
            ddlCourtType.Items.Insert(0, new ListItem("Select", "0"));
            ddloldCaseCourt.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
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
    protected void FillParty()
    {
        try
        {
            ddlParty.Items.Clear();
            Helper hlp = new Helper();
            DataTable dtParty = hlp.GetPartyName() as DataTable;
            if (dtParty != null && dtParty.Rows.Count > 0)
            {
                ddlParty.DataValueField = "Party_ID";
                ddlParty.DataTextField = "PartyName";
                ddlParty.DataSource = dtParty;
                ddlParty.DataBind();
            }
            ddlParty.Items.Insert(0, new ListItem("Select", "0"));
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

            ddlPetiDesigNation.Items.Clear(); ddlResDesig.Items.Clear();
            ds = obj.ByDataSet("SELECT Designation_Id ,Designation_Name FROM tblDesignationMaster ORDER BY Designation_Name ASC");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlPetiDesigNation.DataTextField = "Designation_Name";
                ddlPetiDesigNation.DataValueField = "Designation_Id";
                ddlPetiDesigNation.DataSource = ds;
                ddlPetiDesigNation.DataBind();

                ddlResDesig.DataTextField = "Designation_Name";
                ddlResDesig.DataValueField = "Designation_Id";
                ddlResDesig.DataSource = ds;
                ddlResDesig.DataBind();
            }
            ddlResDesig.Items.Insert(0, new ListItem("Select", "0"));
            ddlPetiDesigNation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillCasetype()
    {
        try
        {
            ddlCasetype.Items.Clear();
            ddloldCasetype.Items.Clear();
            ds = obj.ByDataSet("select Casetype_ID, Casetype_Name from  tbl_Legal_Casetype");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCasetype.DataTextField = "Casetype_Name";
                ddlCasetype.DataValueField = "Casetype_ID";
                ddlCasetype.DataSource = ds;
                ddlCasetype.DataBind();
                // For old Case type
                ddloldCasetype.DataTextField = "Casetype_Name";
                ddloldCasetype.DataValueField = "Casetype_ID";
                ddloldCasetype.DataSource = ds;
                ddloldCasetype.DataBind();
            }
            ddlCasetype.Items.Insert(0, new ListItem("Select", "0"));
            ddloldCasetype.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillOrderWithDirection()
    {
        try
        {
            ddlOrderWith.ClearSelection();
            DataSet dsHod = obj.ByDataSet("select OrderWithDirection_ID,OrderWithDirection from tbl_OrderWithDirection");
            if (dsHod.Tables[0].Rows.Count > 0)
            {
                ddlOrderWith.DataTextField = "OrderWithDirection";
                ddlOrderWith.DataValueField = "OrderWithDirection_ID";
                ddlOrderWith.DataSource = dsHod;
                ddlOrderWith.DataBind();
                ddlOrderWith.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void BindddlCaseDisposd_yes()
    {
        try
        {

            ddlCaseDisposdType_First.Items.Clear();
            ds = obj.ByDataSet("select CaseDisposeType_Id, CaseDisposeType from tbl_LegalCaseDisposeType");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlCaseDisposdType_First.DataTextField = "CaseDisposeType";
                ddlCaseDisposdType_First.DataValueField = "CaseDisposeType_Id";
                ddlCaseDisposdType_First.DataSource = ds;
                ddlCaseDisposdType_First.DataBind();
            }
            ddlCaseDisposdType_First.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    public DataTable ReutrnSubSubjectID()
    {
        try
        {
            Helper SubSubject = new Helper();
            ddlCaseSubSubject.Items.Clear();
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
    protected void FillSection()
    {
        try
        {
            lblMsg.Text = "";
            ddlDesSection.Items.Clear();
            DataSet ds = obj.ByDataSet("select DISTINCT Section from tblDesignationMaster where Isactive = 1 and Section is not null");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDesSection.DataTextField = "Section";
                ddlDesSection.DataValueField = "Section";
                ddlDesSection.DataSource = ds;
                ddlDesSection.DataBind();
            }
            ddlDesSection.Items.Insert(0, new ListItem("Select", "0"));

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
            lblFlag.Text = "";
            ds1 = obj.ByProcedure("USP_Select_NewCaseRegis", new string[] { "Case_ID" }
                , new string[] { ViewState["ID"].ToString() }, "dataset");
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                lblCaseNo.Text = ds1.Tables[0].Rows[0]["CaseNo"].ToString();
                txtCaseDetail.Text = ds1.Tables[0].Rows[0]["CaseDetail"].ToString();
                txtCaseRegistrationDate.Text = ds1.Tables[0].Rows[0]["CaseRegDate"].ToString();
                lblFlag.Text = ds1.Tables[0].Rows[0]["Flag"].ToString();
                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["Flag"].ToString()))
                {
                    if (ds1.Tables[0].Rows[0]["Flag"].ToString() == "Excel")
                    {
                        FirstHearingDisposd.Visible = true;
                        lblFlag.Text = "Uploaded by Excel Sheet";
                    }
                    else
                    {
                        FirstHearingDisposd.Visible = false;
                    }
                    if (ds1.Tables[6].Rows[0]["CaseDisposal_Status"].ToString() == "")
                    {
                        FirstHearingDisposd.Visible = true;
                    }
                    else
                    {
                        FirstHearingDisposd.Visible = false;
                    }
                }
                if (ds1.Tables[0].Rows[0]["Casetype_ID"].ToString() == "1")
                {
                    DivOICName.Visible = false; DivOICMobile.Visible = false; DivOICEmail.Visible = false; DivOICOrderNu.Visible = false; div1.Visible = false;
                    DovOICOrderDoc.Visible = false; FieldViewHearingDtl.Visible = false;
                }
                else
                {
                    DivOICName.Visible = true;
                    DivOICMobile.Visible = true;
                    DivOICEmail.Visible = true; DivOICOrderNu.Visible = true; div1.Visible = true; DovOICOrderDoc.Visible = true; FieldViewHearingDtl.Visible = true;
                }

                BindCourtName();
                ddlCourtType.ClearSelection();
                if (ddlCourtType.Items.Count > 0)
                    ddlCourtType.Items.FindByValue(ds1.Tables[0].Rows[0]["CourtType_Id"].ToString().Trim()).Selected = true; ddlCourtType.Enabled = false;


                if (ds1.Tables[0].Rows[0]["Casetype_ID"].ToString() != "")
                    ddlCasetype.ClearSelection();
                ddlCasetype.Items.FindByValue(ds1.Tables[0].Rows[0]["Casetype_ID"].ToString()).Selected = true;
                ddlCasetype.Enabled = false;

                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["CourtLocation_Id"].ToString()))
                {
                    ddlCourtLocation.ClearSelection();
                    ddlCourtType_SelectedIndexChanged(sender, e);
                    ddlCourtLocation.Items.FindByValue(ds1.Tables[0].Rows[0]["CourtLocation_Id"].ToString()).Selected = true;
                    ddlCourtLocation.Enabled = false;
                }
                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["CaseSubSubj_Id"].ToString()))
                {
                    ddlCaseSubject_SelectedIndexChanged(sender, e);
                    ddlCaseSubSubject.ClearSelection();

                    int OICId = ReutrnSubSubjectID().AsEnumerable().Where(r => r.Field<int>("CaseSubSubj_Id") == Convert.ToInt32(ds1.Tables[0].Rows[0]["CaseSubSubj_Id"])).Select(y => y.Field<int>("CaseSubSubj_Id")).FirstOrDefault();
                    if (OICId != 0)
                    {
                        if (OICId == Convert.ToInt32(ds1.Tables[0].Rows[0]["CaseSubSubj_Id"]))
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

                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["Department_Id"].ToString()))
                {
                    //ddlDepartment.Items.Clear();
                    ViewState["Department"] = ds1.Tables[0].Rows[0]["Department_Id"].ToString();
                    //ddlDepartment.Items.FindByValue(ds1.Tables[0].Rows[0]["Department_Id"].ToString()).Selected = true;
                }
                ddlHOD.ClearSelection();
                for (int i = 0; i < ds1.Tables[8].Rows.Count; i++)
                {
                    foreach (ListItem item in ddlHOD.Items)
                    {
                        if (ds1.Tables[8].Rows[i]["Hod_id1"].ToString() == item.Value)
                        {
                            item.Selected = true;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["CaseBelongto_Id"].ToString()))
                {
                    ddlCaseBelongto.ClearSelection();
                    ddlCaseBelongto.Items.FindByValue(ds1.Tables[0].Rows[0]["CaseBelongto_Id"].ToString().Trim()).Selected = true;
                    ddlCaseBelogto_SelectedIndexChanged(sender, e);
                }

                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["Party_Id"].ToString()))
                {
                    ddlParty.ClearSelection();
                    ddlParty.Items.FindByValue(ds1.Tables[0].Rows[0]["Party_Id"].ToString().Trim()).Selected = true;
                }
                FillOicName();
                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["OICMaster_Id"].ToString()))
                {
                    ddlOicName.ClearSelection();
                    ddlOicName.Items.FindByValue(ds1.Tables[0].Rows[0]["OICMaster_Id"].ToString().Trim()).Selected = true;
                    ddlOicName_SelectedIndexChanged(sender, e);
                    ddlCaserRelated.Items.FindByValue("1").Selected = true;
                }

                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["HighPriorityCase_Status"].ToString()))
                {
                    ddlHighprioritycase.ClearSelection();
                    ddlHighprioritycase.Items.FindByText(ds1.Tables[0].Rows[0]["HighPriorityCase_Status"].ToString()).Selected = true;
                }
                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["CaseReplyStatus"].ToString()))
                {
                    ddlCaseReply.ClearSelection();
                    ddlCaseReply.Items.FindByValue(ds1.Tables[0].Rows[0]["CaseReplyStatus"].ToString()).Selected = true;
                    if (ds1.Tables[0].Rows[0]["CaseReplyStatus"].ToString() == "1")
                    {
                        divReplyDate.Visible = true;
                    }
                }
                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["CaseReplyDate"].ToString()))
                {
                    txtReplyDate.Text = ds1.Tables[0].Rows[0]["CaseReplyDate"].ToString();
                }
                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["CaseReplyRemark"].ToString()))
                {
                    txtReplyCaseRemark.Text = ds1.Tables[0].Rows[0]["CaseReplyRemark"].ToString();
                }
                ddlCaseYear.ClearSelection();
                if (ddlCaseYear.Items.Count > 0) ddlCaseYear.Items.FindByText(ds1.Tables[0].Rows[0]["CaseYear"].ToString().Trim()).Selected = true; ddlCaseYear.Enabled = false;

                if (ds1.Tables[1].Rows.Count > 0) GrdRespondentDtl.DataSource = ds1.Tables[1]; GrdRespondentDtl.DataBind();
                if (ds1.Tables[2].Rows.Count > 0) GrdHearingDtl.DataSource = ds1.Tables[2]; GrdHearingDtl.DataBind();
                if (ds1.Tables[3].Rows.Count > 0) GrdPetiDtl.DataSource = ds1.Tables[3]; GrdPetiDtl.DataBind();
                if (ds1.Tables[4].Rows.Count > 0) GrdCaseDocument.DataSource = ds1.Tables[4]; GrdCaseDocument.DataBind();
                if (ds1.Tables[5].Rows.Count > 0) GrdDeptAdvDtl.DataSource = ds1.Tables[5]; GrdDeptAdvDtl.DataBind();
                //if (ds1.Tables[6].Rows.Count > 0) GrdCaseDispose.DataSource = ds1.Tables[6]; GrdCaseDispose.DataBind(); DisposalStatus.Visible = false;
                //if (ds1.Tables[7].Rows.Count > 0) GrdOldCaseDtl.DataSource = ds1.Tables[7]; GrdOldCaseDtl.DataBind();
                if (ds1.Tables[7].Rows.Count > 0)
                {
                    ddlAskForOldCase.SelectedValue = "1";
                    ddlAskForOldCase_SelectedIndexChanged(this, EventArgs.Empty);
                    GrdOldCaseDtl.DataSource = ds1.Tables[7];
                    GrdOldCaseDtl.DataBind();
                }
                else
                {
                    ddlAskForOldCase.SelectedValue = "0";
                    ddlAskForOldCase_SelectedIndexChanged(this, EventArgs.Empty);
                    GrdOldCaseDtl.DataSource = ds1.Tables[7];
                    GrdOldCaseDtl.DataBind();
                }
                if (ds1.Tables[8].Rows.Count > 0) GrdPetiAdv.DataSource = ds1.Tables[9]; GrdPetiAdv.DataBind();
                if (!string.IsNullOrEmpty(ds1.Tables[6].Rows[0]["CaseDisposal_Status"].ToString()))
                {
                    GrdCaseDispose.DataSource = ds1.Tables[6]; GrdCaseDispose.DataBind(); DisposalStatus.Visible = false;
                    Fieldset_CaseDispose.Visible = true;
                }
                else
                {
                    Fieldset_CaseDispose.Visible = false;
                }

                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["OICOrderNumber"].ToString()))
                {
                    txtOICcaseNumber.Text = ds1.Tables[0].Rows[0]["OICOrderNumber"].ToString();

                }
                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["OICOrderDate"].ToString()))
                {
                    txtOICDate.Text = ds1.Tables[0].Rows[0]["OICOrderDate"].ToString();
                }
                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["OICOrderDoc"].ToString()))
                {
                    string Link = ds1.Tables[0].Rows[0]["OICOrderDoc"].ToString();

                    string lbldoc = "OICOrderDoc/" + Link; ;
                    ViewState["oldOICDoc"] = Link;
                    hyperlinkOICdoc.NavigateUrl = lbldoc;
                    hyperlinkOICdoc.Visible = true; ;
                }
                else
                {
                    ViewState["oldOICDoc"] = "NA";
                    hyperlinkOICdoc.Visible = false;
                }
                // Intirm Order DiV
                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["IntrimOrderDate"].ToString()))
                {
                    ddlIntrimOrder.ClearSelection();
                    ddlIntrimOrder.Items.FindByValue("1").Selected = true;
                    Div_IntrimOrderYesNO.Visible = true;
                    txtIntirmOrderDate.Text = ds1.Tables[0].Rows[0]["IntrimOrderDate"].ToString();
                    btnIntrimOrder.Text = "Update";
                    txtIntrimOrderEnddate.Text = ds1.Tables[0].Rows[0]["IntrimOrderEndDate"].ToString();
                }
                if (ds1.Tables[0].Rows[0]["IntrimOrderSummary"].ToString() != "") txtIntrimOrderSummary.Text = ds1.Tables[0].Rows[0]["IntrimOrderSummary"].ToString();
                if (ds1.Tables[0].Rows[0]["IntrimOrderTimeline"].ToString() != "") txtIntrimTimeline.Text = ds1.Tables[0].Rows[0]["IntrimOrderTimeline"].ToString();
                if (ds1.Tables[0].Rows[0]["IntrimOrderAnyPrevPP"].ToString() != "") txtIntrimPrevPP.Text = ds1.Tables[0].Rows[0]["IntrimOrderAnyPrevPP"].ToString();

                ddlDistrict.Items.Clear();
                FillDitrict();
                if (!string.IsNullOrEmpty(ds1.Tables[0].Rows[0]["District_ID"].ToString()))
                {
                    ddlDistrict.ClearSelection();
                    string result = ds1.Tables[0].Rows[0]["District_ID"].ToString();
                    ddlDistrict.Items.FindByValue(result).Selected = true;
                }

            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void ddlOrderWith_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            div_Compliance_AnyRejoinder.Visible = true;
            Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
            if (ddlOrderWith.SelectedIndex > 0)
            {
                OrderByFirst1.Visible = true;
                OrderBy2First.Visible = true;
                HearingDtl_CaseDisposeFirst.Visible = true;

                OrderSummary_DivFirst.Visible = true;
                div_Compliance_AnyRejoinder.Visible = true;
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
                // CimplianceSt_Div.Visible = true;
            }
            else
            {
                // CimplianceSt_Div.Visible = false;DivOrderTimeline.Visible = false;
                OrderByFirst1.Visible = false;
                OrderBy2First.Visible = false;
                HearingDtl_CaseDisposeFirst.Visible = false;
                OrderSummary_DivFirst.Visible = false;
                div_Compliance_AnyRejoinder.Visible = false;
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
            }
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
    protected void ddlCaseSubject_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void ddlOicName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet dsOic = obj.ByDataSet("select * from tblOICMaster where OICMaster_Id = " + ddlOicName.SelectedValue);
            if (dsOic.Tables[0].Rows.Count > 0)
            {
                txtOicMobileNo.Text = dsOic.Tables[0].Rows[0]["OICMobileNo"].ToString();
                txtOicEmailId.Text = dsOic.Tables[0].Rows[0]["OICEmailID"].ToString();
            }
            else
            {
                txtOicMobileNo.Text = "";
                txtOicEmailId.Text = "";

            }
        }
        catch (Exception ex)
        {

            ErrorLogCls.SendErrorToText(ex);
        }
    }
    //Respondent Dtl 
    protected void ddlResOfficetypeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            ddlResOfficeName.Items.Clear();
            ds = obj.ByProcedure("USP_legal_select_OfficeName", new string[] { "OfficeType_Id" }
                , new string[] { ddlResOfficetypeName.SelectedValue }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlResOfficeName.DataTextField = "OfficeName";
                ddlResOfficeName.DataValueField = "Office_Id";
                ddlResOfficeName.DataSource = ds;
                ddlResOfficeName.DataBind();
            }
            ddlResOfficeName.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlDisponsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            OrderBy1.Visible = false;
            OrderBy2.Visible = false;
            if (ddlDisponsType.SelectedIndex > 0)
            {
                OrderBy1.Visible = true;
                OrderBy2.Visible = true;
                HearingDtl_CaseDispose.Visible = true;
                DivOrderTimeline.Visible = true;
                OrderSummary_Div.Visible = true;
                if (ddlDisponsType.SelectedValue == "2")
                {
                    ddlCompliaceSt.ClearSelection();
                    CimplianceSt_Div.Visible = true;
                    //RequiredFieldValidator6.Enabled = true;
                }
            }
            else
            {
                HearingDtl_CaseDispose.Visible = false;
                OrderBy1.Visible = false;
                OrderBy2.Visible = false;
                DivOrderTimeline.Visible = false;
                OrderSummary_Div.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddloldCaseCourt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddloldCourtLoca_Id.Items.Clear();
            DataSet dsCourt = obj.ByDataSet("select  CT.District_Id, District_Name  from tbl_LegalCourtType CT INNER Join Mst_District DM on DM.District_ID = CT.District_Id where CourtType_ID = " + ddloldCaseCourt.SelectedValue);
            if (dsCourt != null && dsCourt.Tables[0].Rows.Count > 0)
            {
                ddloldCourtLoca_Id.DataTextField = "District_Name";
                ddloldCourtLoca_Id.DataValueField = "District_Id";
                ddloldCourtLoca_Id.DataSource = dsCourt;
                ddloldCourtLoca_Id.DataBind();
            }
            ddloldCourtLoca_Id.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    protected void ddlCaseReply_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCaseReply.SelectedValue == "1")
            {
                divReplyDate.Visible = true;
                divReplyRemark.Visible = true;
            }
            else
            {
                divReplyDate.Visible = false;
                divReplyRemark.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlCaserRelated_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDeleteCase.ClearSelection();
        if (ddlCaserRelated.SelectedValue == "2")
        {
            divCaseRelate.Visible = true;
        }
        else
        {
            divCaseRelate.Visible = false;
        }
    }
    protected void ddlDeleteCase_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDeleteCase.SelectedValue == "1")
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "ValidatePage()", true);
            ddlDeleteCase.ClearSelection();
        }
        else
        {

        }

    }
    protected void ddlResDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {  // This Event Made by Omprakash on 09/05/24 For HOD Field.
            ddlHodName.Items.Clear();
            DataSet dsHod = obj.ByDataSet("select HOD_ID, HodName from tblHODMaster where Dept_Id =" + ddlResDepartment.SelectedValue);
            if (dsHod.Tables[0].Rows.Count > 0)
            {
                ddlHodName.DataTextField = "HodName";
                ddlHodName.DataValueField = "HOD_ID";
                ddlHodName.DataSource = dsHod;
                ddlHodName.DataBind();
                ddlHodName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlAskForOldCase_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtoldCaseNo.Text = "";
            ddloldCaseYear.ClearSelection();
            ddloldCasetype.ClearSelection();
            ddloldCaseCourt.ClearSelection();
            ddloldCourtLoca_Id.ClearSelection();
            if (ddlAskForOldCase.SelectedValue == "1")
            {
                FieldViewOldCaseDtl.Visible = true;
            }
            else
            {
                FieldViewOldCaseDtl.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void rdCaseDispose_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            CaseDisposd_Yes.Visible = false;
            if (rdCaseDispose.SelectedValue == "1")
            {
                CaseDisposd_Yes.Visible = true; OrderWithDir_Div.Visible = false;
                Div_RejoinderNo.Visible = false; Div_RejoinderDate.Visible = false; Div_FileRejoinder.Visible = false; Div_RejoinderRemark.Visible = false;
                divAdditionalReturn.Visible = false;
                Div_CompliancNo.Visible = false; Div_CompliancDate.Visible = false; Div_CompliancDoc.Visible = false;
                Div_ComplianceRemark.Visible = false; div_Compliance_AnyRejoinder.Visible = false;
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
                //divAdditionalReturn.Visible = false;
                btnCaseDisposeFirst.Text = "Update";
            }
            else if (rdCaseDispose.SelectedValue == "2")
            {
                // divAdditionalReturn.Visible = true; CimplianceSt_Div.Visible = false;DivOrderTimeline.Visible = false;
                CaseDisposd_Yes.Visible = false; OrderByFirst1.Visible = false; HearingDtl_CaseDisposeFirst.Visible = false;
                OrderBy2First.Visible = false; ddlCaseDisposdType_First.ClearSelection(); OrderSummary_DivFirst.Visible = false;
                Div_RejoinderNo.Visible = false; Div_RejoinderDate.Visible = false; Div_FileRejoinder.Visible = false; Div_RejoinderRemark.Visible = false;
                divAdditionalReturn.Visible = false; OrderWithDir_Div.Visible = false;
                Div_CompliancNo.Visible = false; Div_CompliancDate.Visible = false; Div_CompliancDoc.Visible = false;
                Div_ComplianceRemark.Visible = false; div_Compliance_AnyRejoinder.Visible = false;
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
            }
            else
            {
                CaseDisposd_Yes.Visible = false;
                //divAdditionalReturn.Visible = false; DivOrderTimeline.Visible = false;
                OrderByFirst1.Visible = false; OrderBy2First.Visible = false; //CimplianceSt_Div.Visible = false;
                ddlCaseDisposdType_First.ClearSelection();
                Div_RejoinderNo.Visible = false; Div_RejoinderDate.Visible = false; Div_FileRejoinder.Visible = false; Div_RejoinderRemark.Visible = false;
                divAdditionalReturn.Visible = false; OrderWithDir_Div.Visible = false;
                Div_CompliancNo.Visible = false; Div_CompliancDate.Visible = false; Div_CompliancDoc.Visible = false;
                Div_ComplianceRemark.Visible = false; div_Compliance_AnyRejoinder.Visible = false;
                //txtAdditionalReturn.Text = "";
                HearingDtl_CaseDisposeFirst.Visible = false; OrderSummary_DivFirst.Visible = false; btnCaseDisposeFirst.Text = "Update";
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlCaseDisposd_yes_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            OrderByFirst1.Visible = false;
            OrderBy2First.Visible = false;
            if (ddlCaseDisposdType_First.SelectedIndex > 0)
            {
                OrderWithDir_Div.Visible = false;
                OrderByFirst1.Visible = true;
                OrderBy2First.Visible = true;
                HearingDtl_CaseDisposeFirst.Visible = true;
                //DivOrderTimeline.Visible = true;
                OrderSummary_DivFirst.Visible = true;
                ddlOrderWith.ClearSelection();
                div_Compliance_AnyRejoinder.Visible = false;
                // CimplianceSt_Div.Visible = true;
                if (ddlCaseDisposdType_First.SelectedValue == "2")
                {
                    OrderByFirst1.Visible = false; OrderBy2First.Visible = false; HearingDtl_CaseDisposeFirst.Visible = false;
                    OrderSummary_DivFirst.Visible = false; OrderWithDir_Div.Visible = true; div_Compliance_AnyRejoinder.Visible = false;
                    /* ddlCompliaceSt.ClearSelection(); CimplianceSt_Div.Visible = false;DivOrderTimeline.Visible = false;*/
                    ddlOrderWith.ClearSelection();
                }
            }
            else
            {
                OrderWithDir_Div.Visible = false; HearingDtl_CaseDisposeFirst.Visible = false; OrderByFirst1.Visible = false; OrderBy2First.Visible = false;
                OrderSummary_DivFirst.Visible = false; //CimplianceSt_Div.Visible = false;DivOrderTimeline.Visible = false;
                Div_RejoinderNo.Visible = false; Div_RejoinderDate.Visible = false; Div_FileRejoinder.Visible = false; Div_RejoinderRemark.Visible = false;
                divAdditionalReturn.Visible = false; Div_CompliancNo.Visible = false; Div_CompliancDate.Visible = false; Div_CompliancDoc.Visible = false;
                Div_ComplianceRemark.Visible = false; div_Compliance_AnyRejoinder.Visible = false; ddlOrderWith.ClearSelection();
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlAnyRejoinder_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAnyRejoinder.SelectedValue == "1")
            {
                Div_RejoinderNo.Visible = true; Div_RejoinderDate.Visible = true; Div_FileRejoinder.Visible = true; Div_RejoinderRemark.Visible = true;
                divAdditionalReturn.Visible = true;
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
                rfvRejNo.Enabled = true; rfvRejDate.Enabled = true; rfvRejRemark.Enabled = true;

            }
            else
            {
                Div_RejoinderNo.Visible = false; Div_RejoinderDate.Visible = false; Div_FileRejoinder.Visible = false; Div_RejoinderRemark.Visible = false;
                divAdditionalReturn.Visible = false; rfvRejNo.Enabled = false; rfvRejDate.Enabled = false; rfvRejRemark.Enabled = false;
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlAdditionalReturn_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlAdditionalReturn.SelectedValue == "1")
            {
                Div_AdditionalNo.Visible = true; Div_AdditionalDate.Visible = true; Div_AdditionalDoc.Visible = true; Div_AdditionalRemar.Visible = true;
                rfvAdditionalNo.Enabled = true; rfvAdditionalDate.Enabled = true; rfvadditionalRemark.Enabled = true;
            }
            else
            {
                Div_AdditionalNo.Visible = false; Div_AdditionalDate.Visible = false; Div_AdditionalDoc.Visible = false; Div_AdditionalRemar.Visible = false;
                rfvAdditionalNo.Enabled = false; rfvAdditionalDate.Enabled = false; rfvadditionalRemark.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlCaseBelogto_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCaseBelongto.SelectedIndex > 0)
            {
                divDistrictLocation.Visible = true;
            }
            else divDistrictLocation.Visible = false;
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void ddlIntrimOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (ddlIntrimOrder.SelectedValue == "1") Div_IntrimOrderYesNO.Visible = true;
            else Div_IntrimOrderYesNO.Visible = false;

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void btnSaveDoc_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                lblMsg.Text = "";
                if (ViewState["AddNewCaseDoc"] == "")
                    ViewState["AddNewCaseDoc"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FileCaseDoc.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FileCaseDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FileCaseDoc.PostedFile.ContentLength > 2097152) // 2 MB
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FileCaseDoc.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "NewCase-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/AddNewCaseCourtDoc/"), strFileName);
                        FileCaseDoc.SaveAs(path);

                        ViewState["AddNewCaseDoc"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Format.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 200 KB \\n"; }

                if (errormsg == "")
                {
                    if (btnSaveDoc.Text == "Save")
                    {

                        ds = obj.ByProcedure("USP_InsertUpdate_DocsForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "Doc_Name", "Doc_Path", "CreatedBy", "CreatedByIP" }
                            , new string[] { "1", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), txtDocumentName.Text.Trim(), ViewState["AddNewCaseDoc"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    else if (btnSaveDoc.Text == "Update" && ViewState["DocID"].ToString().ToString() != "" && ViewState["DocID"].ToString() != null)
                    {
                        if (ViewState["DocPath"] != ViewState["AddNewCaseDoc"])
                        {
                            string path = Path.Combine(Server.MapPath("../Legal/AddNewCaseCourtDoc/"), ViewState["DocPath"].ToString());
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                        }

                        ds = obj.ByProcedure("USP_InsertUpdate_DocsForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "CaseDoc_ID", "Doc_Name", "Doc_Path", "LastupdatedBy", "LastupdatedByIp" }
                            , new string[] { "2", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), ViewState["DocID"].ToString(), txtDocumentName.Text.Trim(), ViewState["AddNewCaseDoc"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            lblMsg.Text = obj.Alert("fa-ban", "alert-success", "Thanks !", ErrMsg);
                            txtDocumentName.Text = "";
                            ViewState["AddNewCaseDoc"] = "";
                            BindDetails(sender, e);
                            btnSaveDoc.Text = "Save";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                        }
                        else
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                    }
                }
                else
                {
                    ViewState["AddNewCaseDoc"] = "";
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                }
            }
            catch (Exception ex)
            {
                //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
                ErrorLogCls.SendErrorToText(ex);
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                ViewState["fuOICDocument"] = "";

                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                string OICDocument = "";
                lblMsg.Text = "";
                string HODId = "";
                foreach (ListItem item in ddlHOD.Items)
                {
                    if (item.Selected)
                    {
                        HODId += item.Value + ",";
                    }
                }
                if (btnUpdate.Text == "Update" && ViewState["ID"].ToString() != null && ViewState["ID"].ToString() != "")
                {
                    if (fuOICDocument.HasFile)
                    {
                        string fileExt = System.IO.Path.GetExtension(fuOICDocument.FileName).Substring(1);
                        string[] supportedTypes = { "PDF", "pdf" };
                        if (!supportedTypes.Contains(fileExt))
                        {
                            DocFailedCntExt += 1;
                        }
                        else if (fuOICDocument.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                        {
                            DocFailedCntSize += 1;
                        }
                        else
                        {
                            strFileName = fuOICDocument.FileName.ToString();
                            strExtension = Path.GetExtension(strFileName);
                            strTimeStamp = DateTime.Now.ToString();
                            strTimeStamp = strTimeStamp.Replace("/", "-");
                            strTimeStamp = strTimeStamp.Replace(" ", "-");
                            strTimeStamp = strTimeStamp.Replace(":", "-");
                            string strName = Path.GetFileNameWithoutExtension(strFileName);
                            strFileName = strName + "OICOrderDoc-" + strTimeStamp + strExtension;
                            string path = Path.Combine(Server.MapPath("~/Legal/OICOrderDoc"), strFileName);
                            fuOICDocument.SaveAs(path);

                            OICDocument = strFileName;
                            path = "";
                            strFileName = "";
                            strName = "";
                        }
                    }
                    if (OICDocument == "")
                    {
                        OICDocument = ViewState["oldOICDoc"].ToString();
                    }
                    string errormsg = "";
                    if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Format.\\n"; }
                    if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 200 KB \\n"; }

                    if (errormsg == "")
                    {
                        string OICDate = txtOICDate.Text != "" ? Convert.ToDateTime(txtOICDate.Text, cult).ToString("yyyy/MM/dd") : "";
                        string ReplyDate = txtReplyDate.Text != "" ? Convert.ToDateTime(txtReplyDate.Text, cult).ToString("yyyy/MM/dd") : "";
                        string CaseRegDate = txtCaseRegistrationDate.Text.Trim() != "" ? Convert.ToDateTime(txtCaseRegistrationDate.Text, cult).ToString("yyyy/MM/dd") : null;
                        ds = obj.ByProcedure("DisposecaseDetailUpdate",
                              new string[] {"HOD_Id","CaseBelongto_Id","CaseSubject_Id", "CaseSubSubj_Id",
                                      "OICMaster_Id","OICOrderNumber","OICOrderDate","OICOrderDoc","District_ID" ,"Party_Id","CaseRegDate",
                                      "HighPriorityCase_Status", "CaseDetail", "Case_ID",
                                      "CaseReplyStatus","CaseReplyDate","CaseReplyRemark",
                                      "UniqueNo", "LastupdatedBy", "LastupdatedByIP" }
                            , new string[] { HODId,ddlCaseBelongto.SelectedValue,ddlCaseSubject.SelectedValue, ddlCaseSubSubject.SelectedValue,
                            ddlOicName.SelectedValue,txtOICcaseNumber.Text.Trim(),OICDate,OICDocument, ddlDistrict.SelectedValue,ddlParty.SelectedValue,CaseRegDate,
                            ddlHighprioritycase.SelectedItem.Text.Trim(), txtCaseDetail.Text.Trim(), ViewState["ID"].ToString(),
                            ddlCaseReply.SelectedValue,ReplyDate,txtReplyCaseRemark.Text.Trim(),
                            ViewState["UniqueNO"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    else
                    {
                        ViewState["fuOICDocument"] = "";
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                    }
                }
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            ////lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);

                            ddlCaseSubject.ClearSelection();
                            ddlCaseSubSubject.ClearSelection();
                            ddlOicName.ClearSelection();
                            ddlParty.ClearSelection();
                            ddlHighprioritycase.ClearSelection();
                            txtCaseDetail.Text = "";
                            BindDetails(sender, e);

                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                            ViewState["oldOICDoc"] = "";
                        }
                        else
                        {
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                            // ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                        }
                    }
                    //if (ds.Tables[1].Rows[0]["EStatus"].ToString() == "Send")
                    //{
                    //    try
                    //    {
                    //        if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["OICEmailID"].ToString()))
                    //        {
                    //            string EmailBodyHTMLPath = Server.MapPath("~/HtmlTemplete/OIC_Email_Templete.html");
                    //            System.IO.StreamReader objReader;
                    //            //objReader = new StreamReader(System.IO.Directory.GetCurrentDirectory() + "\\intel\\main.html");
                    //            objReader = new StreamReader(EmailBodyHTMLPath);
                    //            string content = objReader.ReadToEnd();
                    //            objReader.Close();
                    //            byte[] pdfBuffer = null;
                    //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    //            {

                    //                content = content
                    //                   .Replace("{{OICName}}", ds.Tables[1].Rows[0]["OICName"].ToString())
                    //                   .Replace("{{CaseNo}}", ds.Tables[1].Rows[0]["FilingNo"].ToString())
                    //                   .Replace("{{OICOrderNumber}}", ds.Tables[1].Rows[0]["OICOrderNumber"].ToString())
                    //                   .Replace("{{OICOrderDate}}", ds.Tables[1].Rows[0]["OICOrderDate"].ToString())
                    //                   .Replace("{{OICDocument}}", ds.Tables[1].Rows[0]["OICOrderDoc"].ToString());
                    //                string fileName = ds.Tables[1].Rows[0]["OICOrderDoc"].ToString();
                    //                string AttachedEmailHTMLPath = Server.MapPath("~/HtmlTemplete/OIC_Email_Templete.html");
                    //                SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                    //                using (MailMessage mm = new MailMessage(smtpSection.From, ds.Tables[1].Rows[0]["OICEmailID"].ToString()))
                    //                {
                    //                    mm.Subject = "OIC नियुक्ति आदेश -" + ds.Tables[1].Rows[0]["FilingNo"].ToString();
                    //                    mm.Body = content;
                    //                    if (ds.Tables[1].Rows[0]["OICOrderDoc"].ToString() != "")
                    //                    {
                    //                        Attachment attachment = new System.Net.Mail.Attachment(Server.MapPath("OICOrderDoc/" + fileName));
                    //                        mm.Attachments.Add(attachment);

                    //                    }
                    //                    mm.IsBodyHtml = true;
                    //                    // mm.CC.Add(CC);
                    //                    //////  mm.Attachments.Add(new Attachment(new MemoryStream(pdfBuffer), "OIC(" + ObjEC.OIC_Name + "_Mapped_With_Case_No_)" + ObjEC.Case_Number + ".pdf"));
                    //                    SmtpClient smtp = new SmtpClient();
                    //                    smtp.Host = smtpSection.Network.Host;
                    //                    smtp.EnableSsl = smtpSection.Network.EnableSsl;
                    //                    NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    //                    smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                    //                    smtp.Credentials = networkCred;
                    //                    smtp.Port = smtpSection.Network.Port;
                    //                    smtp.Send(mm);

                    //                    //obj.ByTextQuery("insert into tblManageResetPassword(UserId, UserEmail, Isactive,ResetPasswordLink) values(" + ds.Tables[0].Rows[0]["userid"].ToString() + ",'" + ds.Tables[0].Rows[0]["UserEmail"].ToString() + "',1, '" + RPurl.ToString() + "')");
                    //                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alert('Email sent Please Check your mail');", true);
                    //                }
                    //            }
                    //            //else
                    //            //{
                    //            //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alertMessage", "alert('Please Enter Valid Email Address');", true);
                    //            //}
                    //        }

                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        ErrorLogCls.SendErrorToText(ex);
                    //    }
                    //}

                }


            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnPetitioner_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet dspeti = new DataSet();
            if (btnPetitioner.Text == "Save")
            {
                dspeti = obj.ByProcedure("USP_InsertUpdate_PetiDtlForCaseRegi", new string[] { "flag", "UniqueNo", "Case_Id", "PetitionerName", "Designation_Id", "PetitionerMobileNo", "PetitionerAddress", "District_ID", "Section", "CreatedBy", "CreatedByIP", "PetitionerRemark" }
                    , new string[] { "1", ViewState["UniqueNO"].ToString(), ViewState["ID"].ToString(), txtPetiName.Text.Trim(), ddlPetiDesigNation.SelectedValue, txtPetiMobileNo.Text.Trim(), txtPetiAddRess.Text.Trim(), ddlPetitionerPresentDistrict.SelectedValue, ddlDesSection.SelectedItem.Text, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), txtPetitionerRemark.Text.Trim() }, "dataset");
            }
            else if (btnPetitioner.Text == "Update")
            {
                dspeti = obj.ByProcedure("USP_InsertUpdate_PetiDtlForCaseRegi", new string[] { "flag", "UniqueNo", "Petitioner_Id", "PetitionerName", "Designation_Id", "PetitionerMobileNo", "PetitionerAddress", "District_ID", "Section", "LastupdatedBy", "LastupdatedByIP", "PetitionerRemark" }
                  , new string[] { "2", ViewState["UniqueNO"].ToString(), ViewState["Petitioner_ID"].ToString(), txtPetiName.Text.Trim(), ddlPetiDesigNation.SelectedValue, txtPetiMobileNo.Text.Trim(), txtPetiAddRess.Text.Trim(), ddlPetitionerPresentDistrict.SelectedValue, ddlDesSection.SelectedItem.Text, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), txtPetitionerRemark.Text.Trim() }, "dataset");
            }
            if (dspeti != null && dspeti.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = dspeti.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (dspeti.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    txtPetiName.Text = "";
                    ddlPetiDesigNation.ClearSelection();
                    txtPetiMobileNo.Text = "";
                    txtPetiAddRess.Text = "";
                    ddlPetitionerPresentDistrict.ClearSelection();
                    txtPetitionerRemark.Text = "";
                    ddlDesSection.ClearSelection();
                    btnPetitioner.Text = "Save";
                    BindDetails(sender, e);
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
    protected void btnRespondent_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet dsres = new DataSet();
            if (btnRespondent.Text == "Save")
            {
                dsres = obj.ByProcedure("USP_InsertUpdate_RespondentForCaseRegis", new string[] { "flag", "HOD_Id", "UniqueNo", "Case_Id", "Officetype_Id", "Office_Id", "Designation_Id", "RespondentName", "RespondentMobileNo", "Address", "Department_Id", "RespondentRemark", "CreatedBy", "CreatedByIP" }
                    , new string[] { "1", ddlHodName.SelectedValue, ViewState["UniqueNO"].ToString(), ViewState["ID"].ToString(), ddlResOfficetypeName.SelectedValue, ddlResOfficeName.SelectedValue, ddlResDesig.SelectedValue, txtResName.Text.Trim(), txtResMobileNo.Text.Trim(), txtResAddress.Text.Trim(), ddlResDepartment.SelectedValue, txtRespondentRemark.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
            }
            else if (btnRespondent.Text == "Update")
            {
                dsres = obj.ByProcedure("USP_InsertUpdate_RespondentForCaseRegis", new string[] { "flag", "HOD_Id", "UniqueNo", "Respondent_ID", "Officetype_Id", "Office_Id", "Designation_Id", "RespondentName", "RespondentMobileNo", "Address", "Department_Id", "RespondentRemark", "LastupdatedBy", "LastupdatedByIP" }
                  , new string[] { "2", ddlHodName.SelectedValue, ViewState["UniqueNO"].ToString(), ViewState["RespondentID"].ToString(), ddlResOfficetypeName.SelectedValue, ddlResOfficeName.SelectedValue, ddlResDesig.SelectedValue, txtResName.Text.Trim(), txtResMobileNo.Text.Trim(), txtResAddress.Text.Trim(), ddlResDepartment.SelectedValue, txtRespondentRemark.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
            }
            if (dsres != null && dsres.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = dsres.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (dsres.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    txtResName.Text = "";
                    ddlResOfficetypeName.ClearSelection();
                    txtResMobileNo.Text = "";
                    txtResAddress.Text = "";
                    ddlResDepartment.ClearSelection();
                    ddlResDesig.ClearSelection();
                    ddlResOfficeName.ClearSelection();
                    btnRespondent.Text = "Save";
                    txtRespondentRemark.Text = "";
                    ddlHodName.ClearSelection();
                    BindDetails(sender, e);
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
    protected void btnDeptAdvocate_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet dsdept = new DataSet();
            if (btnDeptAdvocate.Text == "Save")
            {
                dsdept = obj.ByProcedure("USP_InsertUpdate_DeptAdvForCaseRegis", new string[] { "flag", "UniqueNo", "Case_Id", "DeptAdvName", "DeptAdvMobileNo", "CreatedBy", "CreatedByIP" }
                    , new string[] { "1", ViewState["UniqueNO"].ToString(), ViewState["ID"].ToString(), txtDeptAdvocateName.Text.Trim(), txtDeptAdvocateMobileNo.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
            }
            else if (btnDeptAdvocate.Text == "Update")
            {
                dsdept = obj.ByProcedure("USP_InsertUpdate_DeptAdvForCaseRegis", new string[] { "flag", "UniqueNo", "DeptAdv_Id", "DeptAdvName", "DeptAdvMobileNo", "LastupdatedBy", "LastupdatedByIP" }
                  , new string[] { "2", ViewState["UniqueNO"].ToString(), ViewState["DeptAdv_Id"].ToString(), txtDeptAdvocateName.Text.Trim(), txtDeptAdvocateMobileNo.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
            }
            if (dsdept != null && dsdept.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = dsdept.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (dsdept.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                    txtDeptAdvocateName.Text = "";
                    txtDeptAdvocateMobileNo.Text = "";
                    btnDeptAdvocate.Text = "Save";
                    BindDetails(sender, e);
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
    // Hearing Dtl
    protected void btnAddHeairng_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                lblMsg.Text = "";
                ViewState["HearingDoc"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FileHearingDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(FileHearingDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FileHearingDoc.PostedFile.ContentLength > 2097152) // 2 MB
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FileHearingDoc.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "Hearing-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/HearingDoc/"), strFileName);
                        FileHearingDoc.SaveAs(path);

                        ViewState["HearingDoc"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Format.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 200 KB \\n"; }

                if (errormsg == "")
                {
                    string NextHearingDate = txtNextHearingDate.Text != "" ? Convert.ToDateTime(txtNextHearingDate.Text, cult).ToString("yyyy/MM/dd") : "";

                    if (btnAddHeairng.Text == "Save")
                    {
                        ds = obj.ByProcedure("USP_InsertUpdate_HearingForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "NextHearingDate", "HearingDoc", "CreatedBy", "CreatedByIP" }
                            , new string[] { "1", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), NextHearingDate, ViewState["HearingDoc"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    else if (btnAddHeairng.Text == "Update" && ViewState["Hearing_Id"].ToString().ToString() != "" && ViewState["Hearing_Id"].ToString() != null)
                    {
                        if (ViewState["EditDocPath"] != ViewState["HearingDoc"])
                        {
                            string path = Path.Combine(Server.MapPath("../Legal/HearingDoc/"), ViewState["EditDocPath"].ToString());
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }
                        }
                        ds = obj.ByProcedure("USP_InsertUpdate_HearingForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "NextHearing_ID", "NextHearingDate", "HearingDoc", "LastupdatedBy", "LastupdatedByIp" }
                            , new string[] { "2", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), ViewState["Hearing_Id"].ToString(), NextHearingDate, ViewState["HearingDoc"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");


                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                            txtDocumentName.Text = "";
                            txtNextHearingDate.Text = "";
                            ViewState["HearingDoc"] = "";
                            BindDetails(sender, e);
                            btnSaveDoc.Text = "Save";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                        }
                        else
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                    }
                }
                else
                {
                    ViewState["HearingDoc"] = "";
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                }
            }
            catch (Exception ex)
            {
                ErrorLogCls.SendErrorToText(ex);
            }
        }
    }
    protected void btnCaseDispose_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                lblMsg.Text = "";

                ViewState["DisposeDOC"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FielUpcaseDisposeOrderDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(FielUpcaseDisposeOrderDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FielUpcaseDisposeOrderDoc.PostedFile.ContentLength > 2097152) // 2 MB 
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {

                        strFileName = FielUpcaseDisposeOrderDoc.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "-CaseDispose-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("../Legal/DisposalDocs/"), strFileName);
                        FielUpcaseDisposeOrderDoc.SaveAs(path);

                        ViewState["DisposeDOC"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Format.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 200 KB \\n"; }

                if (errormsg == "")
                {
                    if (btnCaseDispose.Text == "Update")
                    {
                        ds = obj.ByProcedure("USP_UpdateCaseDisOrderByDirec", new string[] { "Compliance_Status", "OrderSummary", "LastupdatedBy", "LastupdatedByIp", "Case_ID" }
                        , new string[] { ddlCompliaceSt.SelectedValue, txtorderSummary.Text.Trim(), Session["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Dispose_ID"].ToString() }, "dataset");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                            txtOrderimpletimeline.Text = "";
                            ddlDisponsType.ClearSelection();
                            ddlCompliaceSt.ClearSelection();
                            txtCaseDisposeDate.Text = "";
                            ViewState["DisposeDOC"] = "";
                            BindDetails(sender, e);
                            btnCaseDispose.Text = "Disposal";
                            caseDisposeYes.Visible = false;
                            ddlDisponsType.ClearSelection();
                            HearingDtl_CaseDispose.Visible = false;
                            OrderSummary_Div.Visible = false;
                            CimplianceSt_Div.Visible = false;
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                        }
                        else
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                    }
                }
                else
                {
                    ViewState["DisposeDOC"] = "";
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                }
            }
        }

        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    // Old Case No. Dtl
    protected void btnOldCase_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                ViewState["FU1"] = "";
                ViewState["FU2"] = "";
                ViewState["FU3"] = "";
                ViewState["FU4"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FU1.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(FU1.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FU1.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FU1.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "OldCaseDoc-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("~/Legal/OldCaseDocument"), strFileName);
                        FU1.SaveAs(path);

                        ViewState["FU1"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                if (FU2.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FU2.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FU2.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FU2.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "OldCaseDoc-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("~/Legal/OldCaseDocument"), strFileName);
                        FU2.SaveAs(path);

                        ViewState["FU2"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                if (FU3.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FU3.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FU3.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FU3.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "OldCaseDoc-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("~/Legal/OldCaseDocument"), strFileName);
                        FU3.SaveAs(path);

                        ViewState["FU3"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                if (FU4.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(FU4.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    else if (FU4.PostedFile.ContentLength > 204800) // 200 KB = 1024 * 200
                    {
                        DocFailedCntSize += 1;
                    }
                    else
                    {
                        strFileName = FU4.FileName.ToString();
                        strExtension = Path.GetExtension(strFileName);
                        strTimeStamp = DateTime.Now.ToString();
                        strTimeStamp = strTimeStamp.Replace("/", "-");
                        strTimeStamp = strTimeStamp.Replace(" ", "-");
                        strTimeStamp = strTimeStamp.Replace(":", "-");
                        string strName = Path.GetFileNameWithoutExtension(strFileName);
                        strFileName = strName + "Hearing-" + strTimeStamp + strExtension;
                        string path = Path.Combine(Server.MapPath("~/Legal/OldCaseDocument"), strFileName);
                        FU4.SaveAs(path);

                        ViewState["FU4"] = strFileName;
                        path = "";
                        strFileName = "";
                        strName = "";
                    }
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Format.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 200 KB \\n"; }

                if (errormsg == "")
                {

                    if (btnOldCase.Text == "Save")
                    {
                        if (txtoldCaseNo.Text != "")
                        {
                            if (FU1.HasFile)// Insert data into oldCase Record table
                            {
                                ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink",
                                           "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                                 new string[] { "1", ViewState["ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text,  ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "Case Details", ViewState["FU1"].ToString(), ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCasetype.SelectedValue,
                               ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                            }
                            if (FU2.HasFile)
                            {
                                ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink", "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                                   new string[] { "1", ViewState["ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "Description Of Proceedings", ViewState["FU2"].ToString(), ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCasetype.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");

                            }
                            if (FU3.HasFile)
                            {
                                ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink", "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                                   new string[] { "1", ViewState["ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "Decision", ViewState["FU3"].ToString(), ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCasetype.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");

                            }
                            if (FU4.HasFile)
                            {
                                ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "OldCaseDocName", "DocLink", "CourtDistLoca_Id", "CourtType_Id", "Casetype_Id", "CreatedBy", "CreatedByIP" },
                                   new string[] { "1", ViewState["ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, "Other", ViewState["FU4"].ToString(), ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ddloldCasetype.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                            }
                            if (ViewState["FU1"].ToString() == "" && ViewState["FU2"].ToString() == "" && ViewState["FU3"].ToString() == "" && ViewState["FU4"].ToString() == "")
                            {
                                ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "Case_Id", "oldCaseNo", "oldCaseYear", "OldCasetype", "OldCourt_Id", "CourtDistLoca_Id", "CourtType_Id", "CreatedBy", "CreatedByIP", "Casetype_Id" },
                                                                                new string[] { "1", ViewState["ID"].ToString(), txtoldCaseNo.Text.Trim(), ddloldCaseYear.SelectedItem.Text, ddloldCasetype.SelectedItem.Text, ddloldCaseCourt.SelectedItem.Text, ddloldCourtLoca_Id.SelectedValue, ddloldCaseCourt.SelectedValue, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ddlCasetype.SelectedValue }, "dataset");
                            }
                        }
                    }
                    else if (btnOldCase.Text == "Update" && ViewState["OldCase_Id"] != "")
                    {
                        ds = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "Id", "UniqueNo", "oldCaseYear", "CourtType_Id", "Court", "CourtDistLoca_Id", "Casetype_Id", "OldCasetype", "LastupdatedBy", "LastupdatedByIP" }
                            , new string[] { "2", ViewState["OldCase_Id"].ToString(), ViewState["UniqueNO"].ToString(), ddloldCaseYear.SelectedItem.Text, ddloldCaseCourt.SelectedValue, ddloldCaseCourt.SelectedItem.Text, ddloldCourtLoca_Id.SelectedValue, ddloldCasetype.SelectedValue, ddloldCasetype.SelectedItem.Text, ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                            txtoldCaseNo.Text = "";
                            ddloldCaseYear.ClearSelection();
                            ddloldCasetype.ClearSelection();
                            ddloldCourtLoca_Id.ClearSelection();
                            ddloldCaseCourt.ClearSelection();
                            ViewState["FU1"] = ""; ViewState["FU2"] = ""; ViewState["FU3"] = ""; ViewState["FU4"] = "";
                            BindDetails(sender, e);
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + ErrMsg + "', 'success')", true);
                        }
                        else ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                    }
                }
                else
                {
                    ViewState["FU1"] = ""; ViewState["FU2"] = ""; ViewState["FU3"] = ""; ViewState["FU4"] = "";
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    // Petitioner Advocate Dtl
    protected void btnPetiAdvSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnPetiAdvSave.Text == "Save")
                {
                    ds = obj.ByProcedure("USP_InsertUpdate_PetiAdvForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "PetiAdv_Name", "PetiAdv_MobileNo", "CreatedBy", "CreatedByIP" }
                        , new string[] { "1", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), txtPetiAdvocateName.Text.Trim(), txtPetiAdvocateMobileNo.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnPetiAdvSave.Text == "Update" && ViewState["PetiAdv_Id"].ToString().ToString() != "" && ViewState["PetiAdv_Id"].ToString() != null)
                {
                    ds = obj.ByProcedure("USP_InsertUpdate_PetiAdvForCaseRegis", new string[] { "flag", "Case_ID", "UniqueNo", "PetiAdv_Id", "PetiAdv_Name", "PetiAdv_MobileNo", "LastupdatedBy", "LastupdatedByIP" }
                        , new string[] { "2", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), ViewState["PetiAdv_Id"].ToString(), txtPetiAdvocateName.Text.Trim(), txtPetiAdvocateMobileNo.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        txtPetiAdvocateName.Text = "";
                        txtPetiAdvocateMobileNo.Text = "";
                        ViewState["PetiAdv_Id"] = "";
                        BindDetails(sender, e);
                        btnPetiAdvSave.Text = "Save";
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
    protected void btnYes_Click(object sender, EventArgs e)
    {
        if (ddlCaserRelated.SelectedValue == "2")
        {
            ds = obj.ByProcedure("USP_Update_CaseRegisDtl",
           new string[] { "flag", "Case_ID", "UniqueNo", "LastupdatedBy", "LastupdatedByIP" }
                          , new string[] { "3", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    Response.Redirect("WPCaseList.aspx");
                }
                else ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
            }

        }



    }
    protected void btnCaseDisposeFirst_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {

                lblMsg.Text = "";
                ViewState["DisposeDOC"] = "";
                ViewState["ComplianceDOC"] = "";
                ViewState["RejoinderDOC"] = "";
                ViewState["AdditionalDOC"] = "";
                int DocFailedCntExt = 0;
                int DocFailedCntSize = 0;
                string strFileName = "";
                string strExtension = "";
                string strTimeStamp = "";
                if (FielUpcaseDisposeOrderDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(FielUpcaseDisposeOrderDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    //else if (FielUpcaseDisposeOrderDoc.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                    //{
                    //    DocFailedCntSize += 1;
                    //}
                    //else
                    //{

                    strFileName = FielUpcaseDisposeOrderDoc.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "-CaseDispose-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/DisposalDocs/"), strFileName);
                    FielUpcaseDisposeOrderDoc.SaveAs(path);

                    ViewState["DisposeDOC"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                    //}
                }
                if (ComplianceDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(ComplianceDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    //else if (FielUpcaseDisposeOrderDoc.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                    //{
                    //    DocFailedCntSize += 1;
                    //}
                    //else
                    //{

                    strFileName = ComplianceDoc.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "-CaseDispose-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/DisposalDocs/"), strFileName);
                    ComplianceDoc.SaveAs(path);

                    ViewState["ComplianceDOC"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                    //}
                }
                if (RejoinderDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(RejoinderDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    //else if (FielUpcaseDisposeOrderDoc.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                    //{
                    //    DocFailedCntSize += 1;
                    //}
                    //else
                    //{

                    strFileName = RejoinderDoc.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "-CaseDispose-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/DisposalDocs/"), strFileName);
                    RejoinderDoc.SaveAs(path);

                    ViewState["RejoinderDOC"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                    //}
                }
                if (AdditionalDoc.HasFile)     // CHECK IF ANY FILE HAS BEEN SELECTED.
                {

                    string fileExt = System.IO.Path.GetExtension(AdditionalDoc.FileName).Substring(1);
                    string[] supportedTypes = { "PDF", "pdf" };
                    if (!supportedTypes.Contains(fileExt))
                    {
                        DocFailedCntExt += 1;
                    }
                    //else if (FielUpcaseDisposeOrderDoc.PostedFile.ContentLength > 5120) // 5 MB = 1024 * 5
                    //{
                    //    DocFailedCntSize += 1;
                    //}
                    //else
                    //{

                    strFileName = AdditionalDoc.FileName.ToString();
                    strExtension = Path.GetExtension(strFileName);
                    strTimeStamp = DateTime.Now.ToString();
                    strTimeStamp = strTimeStamp.Replace("/", "-");
                    strTimeStamp = strTimeStamp.Replace(" ", "-");
                    strTimeStamp = strTimeStamp.Replace(":", "-");
                    string strName = Path.GetFileNameWithoutExtension(strFileName);
                    strFileName = strName + "-CaseDispose-" + strTimeStamp + strExtension;
                    string path = Path.Combine(Server.MapPath("../Legal/DisposalDocs/"), strFileName);
                    AdditionalDoc.SaveAs(path);

                    ViewState["AdditionalDOC"] = strFileName;
                    path = "";
                    strFileName = "";
                    strName = "";
                    //}
                }
                string errormsg = "";
                if (DocFailedCntExt > 0) { errormsg += "Only upload Document in( PDF) Formate.\\n"; }
                if (DocFailedCntSize > 0) { errormsg += "Uploaded Document size should be less than 5 MB \\n"; }

                if (errormsg == "")
                {
                    if (btnCaseDisposeFirst.Text == "Update")
                    {
                        //if (ViewState["Department"].ToString() == "NA")
                        //{
                        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', 'Fill Case Details', 'info')", true);
                        //}
                        //else
                        //{
                        string DisposalDate = txtCaseDisposeDateFirst.Text != "" ? Convert.ToDateTime(txtCaseDisposeDateFirst.Text, cult).ToString("yyyy/MM/dd") : "";
                        string ComplianceDate = txtCompianceDate.Text != "" ? Convert.ToDateTime(txtCompianceDate.Text, cult).ToString("yyyy/MM/dd") : "";
                        string RejoinderDate = txtRejoinderDate.Text != "" ? Convert.ToDateTime(txtRejoinderDate.Text, cult).ToString("yyyy/MM/dd") : "";
                        string AdditionalDate = txtAdditionalDate.Text != "" ? Convert.ToDateTime(txtAdditionalDate.Text, cult).ToString("yyyy/MM/dd") : "";
                        ds = obj.ByProcedure("USP_Update_CaseRegisDtl", new string[] { "flag", "Case_ID", "UniqueNo", "CaseDisposal_Status",
                                "CaseDisposalType_Id", "CaseDisposal_Date", "CaseDisposal_Doc", "OrderSummary","ComplianceNo","ComplianceDate",
                                "ComplianceDoc","ComplianceRemark","AnyRejoinder_ID","RejoinderNo","RejoinderDate","RejoinderDoc","RejoinderRemark","AdditionalReturn_ID","AdditionalNo"
                                ,"AdditionalDate","AdditionalDoc","AdditionalRemark","OrderWithDirection_ID","LastupdatedBy", "LastupdatedByIP" }
                            , new string[] { "2", ViewState["ID"].ToString(), ViewState["UniqueNO"].ToString(), rdCaseDispose.SelectedItem.Text,
                                    ddlCaseDisposdType_First.SelectedValue, DisposalDate,  ViewState["DisposeDOC"].ToString(), txtorderSummaryFirst.Text.Trim(),
                                    txtComplianceNo.Text.Trim(),ComplianceDate, ViewState["ComplianceDOC"].ToString(),txtComplianceRemark.Text.Trim(),
                                    ddlAnyRejoinder.SelectedValue,txtRejoinderNo.Text.Trim(),RejoinderDate,ViewState["RejoinderDOC"].ToString(),txtRejoinderRemark.Text.Trim(),
                                    ddlAdditionalReturn.SelectedValue,txtAdditionalNo.Text.Trim(),AdditionalDate,ViewState["AdditionalDOC"].ToString(),txtAdditionalRemar.Text.Trim(),
                                    ddlOrderWith.SelectedValue,ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                        //}
                    }
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string ErrMsg = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                            {
                                //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                                //txtOrderimpletimeline.Text = "";

                                rdCaseDispose.ClearSelection();
                                ddlDisponsType.ClearSelection();
                                txtCaseDisposeDate.Text = "";
                                ViewState["DisposeDOC"] = "";
                                BindDetails(sender, e);
                                btnCaseDisposeFirst.Text = "Update";
                                rdCaseDispose_SelectedIndexChanged(sender, e);
                                FirstHearingDisposd.Visible = false;
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Alert!', '" + "Case Update Successfully" + "', 'success')", true);
                                fillWPRemainingdays();
                            }
                            else
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Warning!','" + ErrMsg + "' , 'warning')", true);
                        }

                    }
                }
                else
                {
                    ViewState["DisposeDOC"] = "";
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertMessage", "alert('Please Select \\n " + errormsg + "')", true);
                }
            }
        }

        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnBackPage_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Legal/WPCaseList.aspx?CourtId=" + Request.QueryString["CourtId"].ToString() + "&Caseyear=" + Request.QueryString["Caseyear"].ToString() + "&CaseType=" + Request.QueryString["CaseType"].ToString() + "&CaseStatus=" + Request.QueryString["CaseStatus"].ToString(), false);
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void btnIntrimOrder_Click(object sender, EventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            DataSet dsintrim = new DataSet();
            string IntrimOrderStartDate = txtIntirmOrderDate.Text.Trim() != "" ? Convert.ToDateTime(txtIntirmOrderDate.Text, cult).ToString("yyyy/MM/dd") : null;
            string IntrimOrderEndDate = txtIntrimOrderEnddate.Text.Trim() != "" ? Convert.ToDateTime(txtIntrimOrderEnddate.Text, cult).ToString("yyyy/MM/dd") : null;
            dsintrim = obj.ByProcedure("USP_InserUpdateIntrimOrder", new string[] { "IntrimOrderStartDate", "IntrimOrderEndDate", "IntrimOrderTimeline", "IntrimOrderSummary", "IntrimOrderAnyPrevPP", "CreatedBy", "CreatedByIP", "Case_ID" },
                new string[] { IntrimOrderStartDate, IntrimOrderEndDate, txtIntrimTimeline.Text.Trim(), txtIntrimOrderSummary.Text.Trim(), txtIntrimPrevPP.Text.Trim(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["ID"].ToString() }, "dataset");
            if (dsintrim.Tables[0].Rows.Count > 0)
            {
                if (dsintrim.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                {
                    lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", dsintrim.Tables[0].Rows[0]["ErrMsg"].ToString());
                    txtIntirmOrderDate.Text = ""; txtIntrimTimeline.Text = ""; txtIntrimOrderSummary.Text = ""; txtIntrimOrderEnddate.Text = "";
                    btnIntrimOrder.Text = "Save";
                    BindDetails(sender, e);
                }
                else lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", dsintrim.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
            else lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", dsintrim.Tables[0].Rows[0]["ErrMsg"].ToString());
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }

    protected void GrdPetiDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (Session["Emp_Id"].ToString() != "1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You are not aligible to edit')", true);
            }
            else
            {


                if (e.CommandName == "EditRecord")
                {
                    ViewState["Petitioner_ID"] = "";
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    Label lblUniqNO = (Label)row.FindControl("lblUniqNO");
                    Label lblPetitionerName = (Label)row.FindControl("lblPetitionerName");
                    Label lblDesignation_Id = (Label)row.FindControl("lblDesignation_Id");
                    Label lblPetitionermobileNo = (Label)row.FindControl("lblPetitionermobileNo");
                    Label lblAddress = (Label)row.FindControl("lblAddress");
                    Label lblPresentpostDisID = (Label)row.FindControl("lblPresentpostDisID");
                    Label lblDesSection = (Label)row.FindControl("lblDesSection");
                    Label lblRemark = (Label)row.FindControl("lblRemark");
                    ViewState["Petitioner_ID"] = e.CommandArgument;
                    hdnUniqueNo.Value = lblUniqNO.Text;
                    txtPetiName.Text = lblPetitionerName.Text;
                    //txtPetiName.Enabled = false;
                    txtPetiMobileNo.Text = lblPetitionermobileNo.Text;
                    txtPetiAddRess.Text = lblAddress.Text;
                    if (!string.IsNullOrEmpty(lblDesignation_Id.Text))
                    {
                        ddlPetiDesigNation.ClearSelection();
                        ddlPetiDesigNation.Items.FindByValue(lblDesignation_Id.Text).Selected = true;
                    }
                    if (!string.IsNullOrEmpty(lblPresentpostDisID.Text))
                    {
                        ddlPetitionerPresentDistrict.ClearSelection();
                        ddlPetitionerPresentDistrict.Items.FindByValue(lblPresentpostDisID.Text).Selected = true;
                    }
                    if (!string.IsNullOrEmpty(lblDesSection.Text))
                    {
                        ddlDesSection.ClearSelection();
                        FillSection();
                        ddlDesSection.Items.FindByText(lblDesSection.Text).Selected = true;
                    }
                    txtPetitionerRemark.Text = !string.IsNullOrEmpty(lblRemark.Text) ? lblRemark.Text : null;
                    btnPetitioner.Text = "Update";
                }
                if (e.CommandName == "DeleteRecord")
                {
                    GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    DataSet disable = obj.ByProcedure("USP_InsertUpdate_PetiDtlForCaseRegi", new string[] { "flag", "UniqueNo", "Petitioner_Id", "LastIsactiveBy", "LastIsactiveByIP" }
                        , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                    if (disable != null && disable.Tables[0].Rows.Count > 0)
                    {
                        string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                        if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                        {
                            //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                            BindDetails(sender, e);
                        }
                    }
                    else lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdRespondentDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditRecord")
            {
                lblMsg.Text = "";
                ViewState["ResponderID"] = "";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblOfficetype_ID = (Label)row.FindControl("lblOfficetype_ID");
                Label lblOffice_Id = (Label)row.FindControl("lblOffice_Id");
                Label lblDesignation_Id = (Label)row.FindControl("lblDesignation_Id");
                Label lblResName = (Label)row.FindControl("lblRespondentName");
                Label lblResMobileNo = (Label)row.FindControl("lblRespondentNo");
                Label lblDepartent = (Label)row.FindControl("lblDepartment_Id");
                Label lblAddress = (Label)row.FindControl("lblAddress");
                Label lblHod_Id = (Label)row.FindControl("lblHod_Id");
                Label lblRespondendDetailsasperHighCourtsite = (Label)row.FindControl("lblRespondendDetailsasperHighCourtsite");
                ViewState["RespondentID"] = e.CommandArgument;
                btnRespondent.Text = "Update";


                txtResName.Text = !string.IsNullOrEmpty(lblResName.Text) ? lblResName.Text : null;
                txtResMobileNo.Text = !string.IsNullOrEmpty(lblResMobileNo.Text) ? lblResMobileNo.Text : null;
                txtResAddress.Text = !string.IsNullOrEmpty(lblAddress.Text) ? lblAddress.Text : null;
                txtRespondentRemark.Text = !string.IsNullOrEmpty(lblRespondendDetailsasperHighCourtsite.Text) ? lblRespondendDetailsasperHighCourtsite.Text : null;
                if (!string.IsNullOrEmpty(lblDesignation_Id.Text))
                {
                    ddlResDesig.ClearSelection();
                    ddlResDesig.Items.FindByValue(lblDesignation_Id.Text).Selected = true;
                }
                if (!string.IsNullOrEmpty(lblOfficetype_ID.Text))
                {
                    ddlResOfficetypeName.ClearSelection();
                    ddlResOfficetypeName.Items.FindByValue(lblOfficetype_ID.Text).Selected = true;
                }
                if (!string.IsNullOrEmpty(lblDepartent.Text))
                {
                    ddlResDepartment.ClearSelection();
                    ddlResDepartment.Items.FindByValue(lblDepartent.Text).Selected = true;
                }
                if (!string.IsNullOrEmpty(lblHod_Id.Text))
                {
                    ddlResDepartment_SelectedIndexChanged(sender, e);
                    ddlHodName.ClearSelection();
                    ddlHodName.Items.FindByValue(lblHod_Id.Text).Selected = true;
                }
                if (!string.IsNullOrEmpty(lblOffice_Id.Text))
                {
                    ddlResOfficetypeName_SelectedIndexChanged(sender, e);
                    ddlResOfficeName.ClearSelection();
                    ddlResOfficeName.Items.FindByValue(lblOffice_Id.Text).Selected = true;
                }
            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_RespondentForCaseRegis", new string[] { "flag", "UniqueNo", "Respondent_ID", "LastIsactiveBy", "LastIsactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }

    }
    protected void GrdDeptAdvDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditRecord")
            {
                lblMsg.Text = "";
                ViewState["DeptAdv_Id"] = "";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblAdvocateName = (Label)row.FindControl("lblAdvocateName");
                Label lblMobileNo = (Label)row.FindControl("lblMobileNo");
                if (lblMobileNo.Text != "")
                    txtDeptAdvocateMobileNo.Text = lblMobileNo.Text;
                if (lblAdvocateName.Text != "")
                    txtDeptAdvocateName.Text = lblAdvocateName.Text;
                ViewState["DeptAdv_Id"] = e.CommandArgument;
                btnDeptAdvocate.Text = "Update";

            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_DeptAdvForCaseRegis", new string[] { "flag", "UniqueNo", "DeptAdv_Id", "LastIsactiveBy", "LastIsactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdCaseDocument_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["DocID"] = "";
            if (e.CommandName == "EditRecord")
            {
                ViewState["DocPath"] = "";
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblDocPath = (Label)row.FindControl("lblDocPath");
                Label lblDocName = (Label)row.FindControl("lblDocName");
                if (lblDocPath.Text != "")
                {
                    ViewState["DocPath"] = lblDocPath.Text;
                    ViewState["AddNewCaseDoc"] = lblDocPath.Text;
                    RfvUploadDoc.Enabled = false;
                }
                if (lblDocName.Text != "")
                    txtDocumentName.Text = lblDocName.Text;
                ViewState["DocID"] = e.CommandArgument;
                btnSaveDoc.Text = "Update";

            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_DocsForCaseRegis", new string[] { "flag", "UniqueNo", "CaseDoc_ID", "LastIsactiveBy", "LastIsactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            //lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", ex.Message.ToString());
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdHearingDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["Hearing_Id"] = "";
            if (e.CommandName == "EditRecord")
            {
                ViewState["EditDocPath"] = "";
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblHearingDate = (Label)row.FindControl("lblHearingDate");
                Label lblHearingDocPath = (Label)row.FindControl("lblHearingDocPath");
                if (lblHearingDocPath.Text != "")
                {
                    ViewState["EditDocPath"] = lblHearingDocPath.Text;
                    ViewState["HearingDoc"] = lblHearingDocPath.Text;
                    //rfvhearingFile.Enabled = false;
                }
                if (lblHearingDate.Text != "")
                    txtNextHearingDate.Text = lblHearingDate.Text;
                ViewState["Hearing_Id"] = e.CommandArgument;
                btnAddHeairng.Text = "Update";

            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_HearingForCaseRegis", new string[] { "flag", "UniqueNo", "NextHearing_ID", "LastIsactiveBy", "LastIsactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdCaseDispose_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            if (e.CommandName == "EditCase")
            {
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblDisposaltype_Id = (Label)row.FindControl("lblDisposaltype_Id");
                Label lblComplianseSts_ID = (Label)row.FindControl("lblComplianseSts_ID");
                Label lblorderSummary = (Label)row.FindControl("lblorderSummary");
                if (lblDisposaltype_Id.Text != "")
                {
                    caseDisposeYes.Visible = true;
                    ddlDisponsType.ClearSelection();
                    ddlDisponsType.Items.FindByValue(lblDisposaltype_Id.Text).Selected = true;
                }
                if (lblorderSummary.Text != "") txtorderSummary.Text = lblorderSummary.Text;
                if (lblDisposaltype_Id.Text == "2")
                {
                    if (lblComplianseSts_ID.Text != "")
                    {

                        ddlDisponsType.Enabled = false;
                        ddlCompliaceSt.ClearSelection();
                        ddlCompliaceSt.Items.FindByValue(lblComplianseSts_ID.Text).Selected = true; CimplianceSt_Div.Visible = true;
                        HearingDtl_CaseDispose.Visible = true;
                        OrderSummary_Div.Visible = true;
                    }
                }
                ViewState["Dispose_ID"] = e.CommandArgument.ToString();
                btnCaseDispose.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdOldCaseDtl_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["OldCase_Id"] = "";
            if (e.CommandName == "EditRecord")
            {
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblOldCaseNo = (Label)row.FindControl("lblOldCaseNo");
                Label lblOldCaseYear = (Label)row.FindControl("lblOldCaseYear");
                Label lblOldCasetype_Id = (Label)row.FindControl("lblOldCasetype_Id");
                Label lblOldCourt_Id = (Label)row.FindControl("lblOldCourt_Id");
                Label lblOldCourtLoca_Id = (Label)row.FindControl("lblOldCourtLoca_Id");
                Label lblOldDocName = (Label)row.FindControl("lblOldDocName");

                ViewState["OldCase_Id"] = e.CommandArgument;
                btnOldCase.Text = "Update";
                if (lblOldCaseNo.Text != "") txtoldCaseNo.Text = lblOldCaseNo.Text;
                if (lblOldCaseYear.Text != "") ddloldCaseYear.ClearSelection(); ddloldCaseYear.Items.FindByValue(lblOldCaseYear.Text).Selected = true;
                if (lblOldCourt_Id.Text != "") ddloldCaseCourt.ClearSelection(); ddloldCaseCourt.Items.FindByValue(lblOldCourt_Id.Text).Selected = true;
                if (lblOldCasetype_Id.Text != "") ddloldCasetype.ClearSelection(); ddloldCasetype.Items.FindByValue(lblOldCasetype_Id.Text).Selected = true;
                if (lblOldCourtLoca_Id.Text != "")
                {
                    ddloldCaseCourt_SelectedIndexChanged(sender, e);
                    ddloldCourtLoca_Id.ClearSelection();

                    ddloldCourtLoca_Id.Items.FindByValue(lblOldCourtLoca_Id.Text).Selected = true;
                }
                Div_Doc1.Visible = false; Div_Doc2.Visible = false; Div_Doc3.Visible = false; Div_Doc4.Visible = false;
            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_Insert_OldCaseEntry", new string[] { "flag", "UniqueNo", "Id", "LastisactiveBy", "LastisactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                        btnOldCase.Text = "Save";
                        Div_Doc1.Visible = true; Div_Doc2.Visible = true; Div_Doc3.Visible = true; Div_Doc4.Visible = true;

                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdPetiAdv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            ViewState["PetiAdv_Id"] = "";
            if (e.CommandName == "EditRecord")
            {
                lblMsg.Text = "";
                GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                Label lblPetiAdvocatename = (Label)row.FindControl("lblPetiAdvocatename");
                Label lblPetiAdvocatMObile = (Label)row.FindControl("lblPetiAdvocatMObile");


                ViewState["PetiAdv_Id"] = e.CommandArgument;
                btnPetiAdvSave.Text = "Update";
                if (lblPetiAdvocatename.Text != "") txtPetiAdvocateName.Text = lblPetiAdvocatename.Text;
                if (lblPetiAdvocatMObile.Text != "") txtPetiAdvocateMobileNo.Text = lblPetiAdvocatMObile.Text;
            }
            else if (e.CommandName == "DeleteRecord")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                DataSet disable = obj.ByProcedure("USP_InsertUpdate_PetiAdvForCaseRegis", new string[] { "flag", "UniqueNo", "PetiAdv_Id", "LastisactiveBy", "LastisactiveByIP" }
                   , new string[] { "3", ViewState["UniqueNO"].ToString(), e.CommandArgument.ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                if (disable != null && disable.Tables[0].Rows.Count > 0)
                {
                    string ErrMsg = disable.Tables[0].Rows[0]["ErrMsg"].ToString();
                    if (disable.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        //lblMsg.Text = obj.Alert("fa-check", "alert-success", "Thanks !", ErrMsg);
                        BindDetails(sender, e);
                    }
                }
                else
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry !", disable.Tables[0].Rows[0]["ErrMsg"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void GrdCaseDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink Link = (HyperLink)e.Row.FindControl("hyperViewLink");
                HyperLink DocWithFolderPath = (HyperLink)e.Row.FindControl("hyperViewDoc");
                Label lbldoc = (Label)e.Row.FindControl("lblDocPath");
                int coul = GrdCaseDocument.Columns.Count;

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
}