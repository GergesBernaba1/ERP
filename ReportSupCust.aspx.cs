using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

public partial class ReportSupCust : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=56 and IsVaild=1";
                DataTable dt1 = dataobj.Selectdatatable(sql1);
                if (dt1.Rows.Count == 1)
                {
                    drpType.Items.Insert(0, "اختار");
                    drpType.Items.Insert(1, "عملاء");
                    drpType.Items.Insert(2, "موردين");
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpType.SelectedIndex == 1)
            {
                Response.Redirect("ReportAllCustomers.aspx");
            }
            else if (drpType.SelectedIndex == 2)
            {
                Response.Redirect("ReportAllSuppliers.aspx");
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    //private void Report(string sql, string path)
    //{
    //    string sqls = sql;
    //    System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
    //    ReportDocument reportdocument = new ReportDocument();
    //    reportdocument.Load(Server.MapPath(path));

    //    reportdocument.DataSourceConnections[0].SetConnection(".", "ERPsystem", true);
    //    reportdocument.SetDataSource(dataobj.Selectdatatable(sqls));
    //    reportdocument.SetParameterValue("@Type", true);
    //    reportdocument.PrintOptions.PrinterName = printDocument.PrinterSettings.PrinterName;
    //    reportdocument.PrintToPrinter(1, true, 1, 100);
    //}
}