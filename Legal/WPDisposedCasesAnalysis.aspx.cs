using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Legal_WPDisposedCasesAnalysis : System.Web.UI.Page
{

    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                FillCourt();
                FillCasetype();
                FillYear();
                ViewState["OIC_ID"] = Session["OICMaster_ID"];
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx", false);
        }
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string OIC = "";
            GrdWPDisposedCasesAnalysis.DataSource = null;
            GrdWPDisposedCasesAnalysis.DataBind();
            if (Session["Role_ID"].ToString() == "3")
            {
                ds = obj.ByProcedure("USP_GetWPDisposedCasesAnalysisDetail", new string[] { "flag", "CourtType_Id", "Casetype_ID", "OICMaster_Id", "Year" },
                    new string[] { "1", ddlCourtName.SelectedValue, ddlCasetype.SelectedItem.Value, Session["OICMaster_ID"].ToString(), ddlCaseYear.SelectedItem.Text }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "1")
            {
                ds = obj.ByProcedure("USP_GetWPDisposedCasesAnalysisDetail", new string[] { "flag", "CourtType_Id", "Casetype_ID", "Year" },
                    new string[] { "2", ddlCourtName.SelectedValue, ddlCasetype.SelectedItem.Value, ddlCaseYear.SelectedItem.Text }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "4")
            {
                ds = obj.ByProcedure("USP_GetWPDisposedCasesAnalysisDetail", new string[] { "flag", "CourtType_Id", "Casetype_ID", "District_ID", "Year" },
                    new string[] { "3", ddlCourtName.SelectedValue, ddlCasetype.SelectedItem.Value, Session["District_Id"].ToString(), ddlCaseYear.SelectedItem.Text }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "2")
            {
                ds = obj.ByProcedure("USP_GetWPDisposedCasesAnalysisDetail", new string[] { "flag", "CourtType_Id", "Casetype_ID", "Division_ID", "Year" },
                    new string[] { "4", ddlCourtName.SelectedValue, ddlCasetype.SelectedItem.Value, Session["Division_Id"].ToString(), ddlCaseYear.SelectedItem.Text }, "dataset");
            }
            else if (Session["Role_ID"].ToString() == "5")
            {
                ds = obj.ByProcedure("USP_GetWPDisposedCasesAnalysisDetail", new string[] { "flag", "CourtType_Id", "Casetype_ID", "CourtLocation_Id", "Year" },
                    new string[] { "5", ddlCourtName.SelectedValue, ddlCasetype.SelectedItem.Value, Session["District_Id"].ToString(), ddlCaseYear.SelectedItem.Text }, "dataset");
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                GrdWPDisposedCasesAnalysis.DataSource = ds;
                GrdWPDisposedCasesAnalysis.DataBind();
            }
            else
            {
                GrdWPDisposedCasesAnalysis.DataSource = null;
                GrdWPDisposedCasesAnalysis.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
        finally
        {
            Datatable();
        }
    }
    protected void Datatable()
    {
        if (GrdWPDisposedCasesAnalysis.Rows.Count > 0)
        {
            GrdWPDisposedCasesAnalysis.HeaderRow.TableSection = TableRowSection.TableHeader;
            GrdWPDisposedCasesAnalysis.UseAccessibleHeader = true;
        }
    }

    protected void GrdWPDisposedCasesAnalysis_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            lblMsg.Text = "";
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblUniqueNo = (Label)row.FindControl("lblUniqueNo");
            string ID = obj.Encrypt(e.CommandArgument.ToString());
            string UniqueNO = obj.Encrypt(lblUniqueNo.Text);
            string CourtId = obj.Encrypt(ddlCourtName.SelectedValue);
            string Caseyear = obj.Encrypt(ddlCaseYear.SelectedItem.Text);
            string CaseType = obj.Encrypt(ddlCasetype.SelectedItem.Text);

                Response.Redirect("~/Legal/EditWPDisposedCasesAnalysis.aspx?CaseID=" + ID + "&UniqueNO=" + UniqueNO + "&CourtId=" + CourtId + "&Caseyear=" + Caseyear + "&CaseType=" + CaseType , false);
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}