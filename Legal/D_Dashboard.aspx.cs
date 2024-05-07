using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_D_Dashboard : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!Page.IsPostBack)
            {
                FillCourt();
                FillCasetype();
                
                if (ddlCourt.SelectedValue == "0")
                {
                    btnSearch_Click(sender, e);
                }

            }
        }
        else
        {
            Response.Redirect("../Login.aspx", false);
        }

        //fillChart();
    }
    protected void FillCourt()
    {
        try
        {
            ddlCourt.Items.Clear();
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
            else if (Session["Role_ID"].ToString() == "3")// District Office.
            {
                string District_Id = Session["District_Id"].ToString();
                dtCourt = court.GetCourtForCourt(District_Id) as DataTable;
            }
            else dtCourt = court.GetCourt() as DataTable;
            if (dtCourt.Rows.Count > 0)
            {
                ddlCourt.DataValueField = "CourtType_ID";
                ddlCourt.DataTextField = "CourtTypeName";
                ddlCourt.DataSource = dtCourt;
                ddlCourt.DataBind();
                ddlCourt.Items.Insert(0, new ListItem("All", "0"));
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
            Helper HP = new Helper();
            DataTable dt = HP.GetCasetype() as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlCaseType.DataValueField = "Casetype_ID";
                ddlCaseType.DataTextField = "Casetype_Name";
                ddlCaseType.DataSource = dt;
                ddlCaseType.DataBind();
            }
            ddlCaseType.Items.Insert(0, new ListItem("All", "0"));
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void fillChart()
    {
        try
        {
            StringBuilder HTML = new StringBuilder();

            //DataSet ds = obj.ByDataSet("select COUNT(Case_ID) as CaseCount,CaseYear from tblLegalCaseRegistration where CourtType_Id = " + ddlCourt.SelectedValue + " and CaseYear is not null group by CaseYear order by CaseYear DESC");

            ds = obj.ByProcedure("Usp_GetTotalCountD_Dashboard", new string[] { "CourtType_Id", "Casetype_ID", "CaseStatus" }, new string[] { ddlCourt.SelectedValue, ddlCaseType.SelectedValue, ddlCaseStatus.SelectedItem.Text }, "GetCount");

            HTML.Append("<script>");
            HTML.Append("Highcharts.chart('container', {");
            HTML.Append("chart:");
            HTML.Append("{");
            HTML.Append("type: 'column'");
            HTML.Append("},");
            HTML.Append("title:");
            HTML.Append("{");
            HTML.Append("text: ''");
            HTML.Append("},");
            HTML.Append("subtitle:");
            HTML.Append("{");
            HTML.Append("text:null");
            HTML.Append("},");
            HTML.Append("xAxis:");
            HTML.Append("{");
            HTML.Append("type: 'category',");
            HTML.Append("labels:");
            HTML.Append("{");
            HTML.Append("autoRotation: [-45, -90],");
            HTML.Append("style:");
            HTML.Append("{");
            HTML.Append("fontSize: '13px',");
            HTML.Append("fontFamily: 'Verdana, sans-serif'");
            HTML.Append("}");
            HTML.Append("}");
            HTML.Append("},");
            HTML.Append("yAxis:");
            HTML.Append("{");
            HTML.Append("min: 0,");
            HTML.Append("title:");
            HTML.Append("{");
            HTML.Append("text: 'Total Case Count'");
            HTML.Append("}");
            HTML.Append("},");
            HTML.Append("legend:");
            HTML.Append("{");
            HTML.Append("enabled: false");
            HTML.Append("},");
            HTML.Append("tooltip:");
            HTML.Append("{");
            HTML.Append("pointFormat: 'Total Case: <b>{point.y:.f} </b>'");
            HTML.Append("},");
            HTML.Append("series:");
            HTML.Append("[{");
            HTML.Append("name: 'Population',");
            HTML.Append("colors: [");
            HTML.Append("'#9b20d9', '#9215ac', '#861ec9', '#B34A33', '#7010f9', '#33B384',");
            HTML.Append("'#33A2B3', '#5b30e7', '#A933B3', '#4c46db', '#B3337D', '#3e5ccf',");
            HTML.Append("'#3667c9', '#2f72c3', '#277dbd', '#1f88b7', '#1693b1', '#0a9eaa',");
            HTML.Append("'#03c69b', '#00f194', '#00f1cd', '#a63d6b'");
            HTML.Append("],");
            HTML.Append("colorByPoint: true,");
            HTML.Append("groupPadding: 0,");
            HTML.Append("data: [");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                HTML.Append("['" + ds.Tables[0].Rows[i]["CaseYear"].ToString() + "', " + ds.Tables[0].Rows[i]["CaseCount"].ToString() + "],");
            }
            //HTML.Append("['2000', 37.33],");
            HTML.Append("],");
            HTML.Append("dataLabels:");
            HTML.Append("{");
            HTML.Append("enabled: true,");
            HTML.Append("rotation: -90,");
            HTML.Append("color: '#FFFFFF',");
            HTML.Append("inside: true,");
            HTML.Append("verticalAlign: 'top',");
            HTML.Append("format: '{point.y:.f}',");
            HTML.Append("y: 10,");
            HTML.Append("style:");
            HTML.Append("{");
            HTML.Append("fontSize: '13px',");
            HTML.Append("fontFamily: 'Verdana, sans-serif'");
            HTML.Append("}");
            HTML.Append("}");
            HTML.Append("}]");
            HTML.Append("});");
            HTML.Append("</script>");
            divscript.InnerHtml = HTML.ToString();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
    protected void FillChart2()
    {
        try
        {

            ds = obj.ByProcedure("Usp_GetTotalCountD_Dashboard", new string[] { "CourtType_Id", "Casetype_ID", "CaseStatus" }, new string[] { ddlCourt.SelectedValue, ddlCaseType.SelectedValue, ddlCaseStatus.SelectedItem.Text }, "GetCount");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<script>");
            sb.AppendLine("(function (H) {");
            sb.AppendLine("H.seriesTypes.pie.prototype.animate = function (init) {");
            sb.AppendLine("const series = this,");
            sb.AppendLine("chart = series.chart,");
            sb.AppendLine("points = series.points,");
            sb.AppendLine("{");
            sb.AppendLine("animation");
            sb.AppendLine("} = series.options,");
            sb.AppendLine("{");
            sb.AppendLine("startAngleRad");
            sb.AppendLine("} = series;");
            sb.AppendLine("function fanAnimate(point, startAngleRad) {");
            sb.AppendLine("const graphic = point.graphic,");
            sb.AppendLine("args = point.shapeArgs;");
            sb.AppendLine("if (graphic && args) {");
            sb.AppendLine("graphic");
            sb.AppendLine("// Set inital animation values");
            sb.AppendLine(".attr({");
            sb.AppendLine("start: startAngleRad,");
            sb.AppendLine("end: startAngleRad,");
            sb.AppendLine("opacity: 1");
            sb.AppendLine("})");
            sb.AppendLine("// Animate to the final position");
            sb.AppendLine(".animate({");
            sb.AppendLine("start: args.start,");
            sb.AppendLine("end: args.end");
            sb.AppendLine("}, {");
            sb.AppendLine("duration: animation.duration / points.length");
            sb.AppendLine("}, function () {");
            sb.AppendLine("// On complete, start animating the next point");
            sb.AppendLine("if (points[point.index + 1]) {");
            sb.AppendLine("fanAnimate(points[point.index + 1], args.end);");
            sb.AppendLine("}");
            sb.AppendLine("// On the last point, fade in the data labels, then");
            sb.AppendLine("// apply the inner size");
            sb.AppendLine("if (point.index === series.points.length - 1) {");
            sb.AppendLine("series.dataLabelsGroup.animate({");
            sb.AppendLine("opacity: 1");
            sb.AppendLine("},");
            sb.AppendLine("void 0,");
            sb.AppendLine("function () {");
            sb.AppendLine("points.forEach(point => {");
            sb.AppendLine("point.opacity = 1;");
            sb.AppendLine("});");
            sb.AppendLine("series.update({");
            sb.AppendLine("enableMouseTracking: true");
            sb.AppendLine("}, false);");
            sb.AppendLine("chart.update({");
            sb.AppendLine("plotOptions: {");
            sb.AppendLine("pie: {");
            sb.AppendLine("innerSize: '40%',");
            sb.AppendLine("borderRadius: 8");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("});");
            sb.AppendLine("});");
            sb.AppendLine("}");
            sb.AppendLine("});");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("if (init) {");
            sb.AppendLine("// Hide points on init");
            sb.AppendLine("points.forEach(point => {");
            sb.AppendLine("point.opacity = 0;");
            sb.AppendLine("});");
            sb.AppendLine("} else {");
            sb.AppendLine("fanAnimate(points[0], startAngleRad);");
            sb.AppendLine("}");
            sb.AppendLine("};");
            sb.AppendLine("}(Highcharts));");
            sb.AppendLine("Highcharts.chart('container2', {");
            sb.AppendLine("chart: {");
            sb.AppendLine("type: 'pie'");
            sb.AppendLine("},");
            sb.AppendLine("title: {");
            sb.AppendLine("text: '',");
            sb.AppendLine("align: 'left'");
            sb.AppendLine("},");
            sb.AppendLine("subtitle: {");
            sb.AppendLine("text: '',");
            sb.AppendLine("align: 'left'");
            sb.AppendLine("},");
            sb.AppendLine("tooltip: {");
            sb.AppendLine("pointFormat: ''");
            sb.AppendLine("},");
            sb.AppendLine("accessibility: {");
            sb.AppendLine("point: {");
            sb.AppendLine("valueSuffix: ''");
            sb.AppendLine("}");
            sb.AppendLine("},");
            sb.AppendLine("plotOptions: {");
            sb.AppendLine("pie: {");
            sb.AppendLine("allowPointSelect: true,");
            sb.AppendLine("borderWidth: 2,");
            sb.AppendLine("cursor: 'pointer',");
            sb.AppendLine("dataLabels: {");
            sb.AppendLine("enabled: true,");
            sb.AppendLine("format: '<b>{point.name}</b><br>{y}',");
            sb.AppendLine("distance: 20");
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("},");
            sb.AppendLine("series: [{");
            sb.AppendLine("animation: {");
            sb.AppendLine("duration: 1000");
            sb.AppendLine("},");
            sb.AppendLine("colorByPoint: true,");
            sb.AppendLine("data: [");
            //sb.AppendLine("{name: 'Disposed',");
            //sb.AppendLine("y: 2000");
            //sb.AppendLine("},");


            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
        

                sb.AppendLine("{name: '" + ds.Tables[1].Rows[i]["CaseStatus"].ToString() + "',");
                sb.AppendLine("y: " + ds.Tables[1].Rows[i]["caseCount"].ToString() + "");
                sb.AppendLine("},");
            }

          

            sb.AppendLine("]");
            sb.AppendLine("}]");
            sb.AppendLine("});");
            sb.AppendLine("</script>");
            divscript2.InnerHtml = sb.ToString();
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
            fillChart();
            FillChart2();
        }
        catch (Exception ex)
        {
            ErrorLogCls.SendErrorToText(ex);
        }
    }
}