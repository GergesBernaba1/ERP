using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

public partial class ReportStore : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=58 and IsVaild=1";
                DataTable dt1 = dataobj.Selectdatatable(sql1);
                if (dt1.Rows.Count == 1)
                {
                    string sql = @"select * from Store";
                    System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                    ReportDocument reportdocument = new ReportDocument();
                    reportdocument.Load(Server.MapPath("Reports/ReportStore.rpt"));
                    reportdocument.DataSourceConnections[0].SetConnection(".", "ERPsystem", true);
                    reportdocument.SetDataSource(dataobj.Selectdatatable(sql));
                    CrystalReportViewer1.ReportSource = reportdocument;
                    //reportdocument.PrintOptions.PrinterName = printDocument.PrinterSettings.PrinterName;
                    //reportdocument.PrintToPrinter(1, true, 1, 100);
                    //Response.Redirect("MainReportStores.aspx");
                }
                else
                {
                    Response.Redirect("MainPage.aspx");
                }

            }
            catch (Exception)
            {
                Response.Redirect("MainPage.aspx");
            }

        }
    }
}