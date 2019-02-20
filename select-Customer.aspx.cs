using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class select_Customer : System.Web.UI.Page
{
    databaseaccesslayer DataObj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=26 and IsVaild=1";
                DataTable dt = DataObj.Selectdatatable(sql);
                if (dt.Rows.Count == 1)
                {
                    BindgrdTrades();
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
    private void BindgrdTrades()
    {
        try
        {
            if (RadioCustomer.SelectedValue == "نشط")
            {
                string sql = @"select Code as الكود,Name as الاسم,InitialBalance as الحساب_الحالي,Mobil as موبيل1,mobil1 as موبيل2,Mobil2 as موبيل3 from CustomerAndSupplier  where Customer=1 and Status=1";
                grdSelect.DataSource = DataObj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
            else if (RadioCustomer.SelectedValue == "غير نشط")
            {
                string sql = @"select Code as الكود,Name as الاسم,InitialBalance as الحساب_الحالي,Mobil as موبيل1,mobil1 as موبيل2,Mobil2 as موبيل3 from CustomerAndSupplier  where Customer=1 and Status=0";
                grdSelect.DataSource = DataObj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
            else
            {
                string sql = @"select Code as الكود,Name as الاسم,InitialBalance as الحساب_الحالي,Mobil as موبيل1,mobil1 as موبيل2,Mobil2 as موبيل3 from CustomerAndSupplier  where Customer=1";
                grdSelect.DataSource = DataObj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
        }
        catch (Exception ex)
        {
            DataObj.Alert(ex.Message, this);
        }
    }
    protected void RadioCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindgrdTrades();
    }
    protected void grdSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = grdSelect.SelectedRow;
            Response.Redirect("Update-CustAndSup.aspx?code=" + row.Cells[1].Text);
        }
        catch (Exception ex)
        {
            DataObj.Alert(ex.Message, this);
        }
    }
    protected void grdSelect_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSelect.PageIndex = e.NewPageIndex;
        BindgrdTrades();
    }
}