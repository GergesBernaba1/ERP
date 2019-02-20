using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportBackSaleDetail : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=66 and IsVaild=1";
                DataTable dt1 = dataobj.Selectdatatable(sql1);
                if (dt1.Rows.Count == 1)
                {
                    addValidationGroup();
                    string sql = @"select top(50)Code from BackSales order by DateOfSale desc";
                    drpType.DataSource = dataobj.Selectdatatable(sql);
                    drpType.DataTextField = "Code";
                    drpType.DataValueField = "Code";
                    drpType.DataBind();
                    drpType.Items.Insert(0, "اختار");
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            div.Visible = true;
            string sql = @"ReportBackSaleDetails '" + drpType.SelectedItem.Value + "'";
            System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            ReportDocument reportdocument = new ReportDocument();
            reportdocument.Load(Server.MapPath("Reports/ReportBackDetailsSales.rpt"));
            reportdocument.DataSourceConnections[0].SetConnection(".", "ERPsystem", true);
            reportdocument.SetDataSource(dataobj.Selectdatatable(sql));
            reportdocument.SetParameterValue("@code", drpType.SelectedItem.Value);
            CrystalReportViewer1.ReportSource = reportdocument;

        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
}