using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

public partial class ReportOneUser : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=53 and IsVaild=1";
                DataTable dt1 = dataobj.Selectdatatable(sql1);
                if (dt1.Rows.Count == 1)
                {
                    addValidationGroup();
                    string sql = @"select Id,UserName from Users ";
                    drpName.DataSource = dataobj.Selectdatatable(sql);
                    drpName.DataTextField = "UserName";
                    drpName.DataValueField = "Id";
                    drpName.DataBind();
                    drpName.Items.Insert(0, "الاسم");
                    drpType.Items.Insert(0, "اختار");
                    drpType.Items.Insert(1, "المشتريات");
                    drpType.Items.Insert(2, "مرتجع المشتريات");
                    drpType.Items.Insert(3, "المبيعات");
                    drpType.Items.Insert(4, "مرتجع المبيعات");
                    drpType.Items.Insert(5, "المصروفات");
                    drpType.Items.Insert(6, "الايرادات");
                    drpType.Items.Insert(7, "مبيعات العروض");
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
    private void addValidationGroup()
    {

        foreach (Control ctrl in UpdatePanel1.Controls[0].Controls)
        {

            TextBox txtbx = ctrl as TextBox;
            if (txtbx != null)
            {
                txtbx.Attributes["ValidationGroup"] = txtbx.ValidationGroup;
            }

            DropDownList drp = ctrl as DropDownList;
            if (drp != null)
            {
                drp.Attributes["ValidationGroup"] = drp.ValidationGroup;
            }
        }
    }
    private void Report(string sql,string path)
    {
        try
        {
            //string sql1 = sql;
            //System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            //ReportDocument reportdocument = new ReportDocument();
            //reportdocument.Load(Server.MapPath(path));

            //reportdocument.DataSourceConnections[0].SetConnection(".", "ERPsystem", true);
            //reportdocument.SetDataSource(dataobj.Selectdatatable(sql1));
            //reportdocument.SetParameterValue("@userId", drpName.SelectedItem.Value);
            //reportdocument.SetParameterValue("@datefrom", DateTime.Parse(txtDateFrom.Text));
            //reportdocument.SetParameterValue("@dateto", DateTime.Parse(txtDateTo.Text));
            //CrystalReportViewer1.ReportSource = reportdocument;
            //div.Visible = true;
            //reportdocument.PrintOptions.PrinterName = printDocument.PrinterSettings.PrinterName;
            //reportdocument.PrintToPrinter(1, true, 1, 100);
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpName.SelectedIndex > 0)
            {
                Session["UserReportId"] = drpName.SelectedItem.Value;
                Session["DateFrom"] = txtDateFrom.Text;
                Session["DateTo"] = txtDateTo.Text;
                if (drpType.SelectedIndex == 1)
                {
                    Response.Redirect("ReportUserPruches.aspx");
                }
                else if (drpType.SelectedIndex == 2)
                {
                    Response.Redirect("ReportUserBackPruches.aspx");
                }
                else if (drpType.SelectedIndex == 3)
                {
                    Response.Redirect("ReportUserSales.aspx");
                }
                else if (drpType.SelectedIndex == 4)
                {
                    Response.Redirect("ReportUserBackSales.aspx");
                }
                else if (drpType.SelectedIndex == 5)
                {
                    Response.Redirect("ReportUserExpenses.aspx");
                }
                else if (drpType.SelectedIndex == 6)
                {
                    Response.Redirect("ReportUserComingMoney.aspx");
                }
                else if (drpType.SelectedIndex == 7)
                {
                    Response.Redirect("ReportUserSalesOver.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
}