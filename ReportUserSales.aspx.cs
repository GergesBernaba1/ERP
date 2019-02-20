using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportUserSales : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=53 and IsVaild=1";
            DataTable dt1 = dataobj.Selectdatatable(sql1);
            if (dt1.Rows.Count == 1)
            {
                string sql = @"UserSales " + Session["UserReportId"] + ",'" + DateTime.Parse(Session["DateFrom"].ToString()) + "','" + DateTime.Parse(Session["DateTo"].ToString()) + "'";
                string path = "Reports/ReportUserSales.rpt";
                System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                ReportDocument reportdocument = new ReportDocument();
                reportdocument.Load(Server.MapPath(path));

                reportdocument.DataSourceConnections[0].SetConnection(".", "ERPsystem", true);
                reportdocument.SetDataSource(dataobj.Selectdatatable(sql));
                reportdocument.SetParameterValue("@userId", Session["UserReportId"]);
                reportdocument.SetParameterValue("@datefrom", DateTime.Parse(Session["DateFrom"].ToString()));
                reportdocument.SetParameterValue("@dateto", DateTime.Parse(Session["DateTo"].ToString()));
                CrystalReportViewer1.ReportSource = reportdocument;
            }
            else
            {
                Response.Redirect("MainPage.aspx");
            }
        }
        catch { Response.Redirect("MainPage.aspx"); }
    }
}