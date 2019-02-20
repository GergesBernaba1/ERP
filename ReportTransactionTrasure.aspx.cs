﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;

public partial class ReportTransactionTrasure : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=55 and IsVaild=1";
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
            if (drpName.SelectedIndex>0)
            {
                div.Visible = true;
                string sql = @"ReportTranTreasure " + drpName.SelectedItem.Value + ",'" + DateTime.Parse(txtDateFrom.Text) + "','" + DateTime.Parse(txtDateTo.Text) + "'";
                    System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                    ReportDocument reportdocument = new ReportDocument();
                    reportdocument.Load(Server.MapPath("Reports/ReportTranTreasure.rpt"));

                    reportdocument.DataSourceConnections[0].SetConnection(".", "ERPsystem", true);
                    reportdocument.SetDataSource(dataobj.Selectdatatable(sql));
                    reportdocument.SetParameterValue("@userId", drpName.SelectedItem.Value);
                    reportdocument.SetParameterValue("@datefrom", DateTime.Parse(txtDateFrom.Text));
                    reportdocument.SetParameterValue("@dateto", DateTime.Parse(txtDateTo.Text));
                    CrystalReportViewer1.ReportSource = reportdocument;
                    //reportdocument.PrintOptions.PrinterName = printDocument.PrinterSettings.PrinterName;
                    //reportdocument.PrintToPrinter(1, true, 1, 100);
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
}