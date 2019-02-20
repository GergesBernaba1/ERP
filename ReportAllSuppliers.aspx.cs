using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportAllSuppliers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        databaseaccesslayer dataobj = new databaseaccesslayer();
        if (!IsPostBack)
        {
            try
            {
                string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=56 and IsVaild=1";
                DataTable dt1 = dataobj.Selectdatatable(sql1);
                if (dt1.Rows.Count == 1)
                {
                    string sql = @"select * from CustomerAndSupplier where Supplier=1";
                    string path = "Reports/ReportSupplier.rpt";
                    string sqls = sql;
                    System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                    ReportDocument reportdocument = new ReportDocument();
                    reportdocument.Load(Server.MapPath(path));
                    reportdocument.DataSourceConnections[0].SetConnection(".", "ERPsystem", true);
                    reportdocument.SetDataSource(dataobj.Selectdatatable(sqls));
                    reportdocument.SetParameterValue("@Type", true);
                    CrystalReportViewer1.ReportSource = reportdocument;
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