using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_Stores : System.Web.UI.Page
{
    databaseaccesslayer DataObj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=16 and IsVaild=1";
                DataTable dt1 = DataObj.Selectdatatable(sql1);
                if (dt1.Rows.Count == 1)
                {
                    BindgrdTrades();
                    grdSelect.DataBind();
                }
                else
                {
                    Response.Redirect("MainPage.aspx");
                }
            }
            catch (Exception) { Response.Redirect("MainPage.aspx"); }
        }
    }
    private void BindgrdTrades()
    {
        try
        {
            if (RadioCustomer.SelectedValue == "نشط")
            {
                string sql = @"select Code as الكود,StoreName as اسم_المخزن,Responsibility as المسئول,StoreTel as تليفون_المخزن,mobil as تليفون_المسئول,StoreAddress as عنوان_المخزن from Store where Status=1";
                grdSelect.DataSource = DataObj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
            else if (RadioCustomer.SelectedValue == "غير نشط")
            {
                string sql = @"select Code as الكود,StoreName as اسم_المخزن,Responsibility as المسئول,StoreTel as تليفون_المخزن,mobil as تليفون_المسئول,StoreAddress as عنوان_المخزن from Store where Status=0";
                grdSelect.DataSource = DataObj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
            else
            {
                string sql = @"select Code as الكود,StoreName as اسم_المخزن,Responsibility as المسئول,StoreTel as تليفون_المخزن,mobil as تليفون_المسئول,StoreAddress as عنوان_المخزن from Store";
                grdSelect.DataSource = DataObj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
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
    protected void RadioCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindgrdTrades();
    }
    protected void grdSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = grdSelect.SelectedRow;
            Response.Redirect("Update-Store.aspx?code=" + row.Cells[1].Text);
        }
        catch (Exception ex)
        {
            DataObj.Alert(ex.Message, this);
        }
    }
}