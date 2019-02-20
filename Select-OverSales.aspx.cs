using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_OverSales : System.Web.UI.Page
{
    databaseaccesslayer DataObj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=41 and IsVaild=1";
                DataTable dt1 = DataObj.Selectdatatable(sql1);
                if (dt1.Rows.Count == 1)
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
            if (txtDateFrom.Text.Trim() != "" && txtDateTo.Text.Trim() != "")
            {
                string sql = "select Code as الكود,NameOfOver as الاسم,Amount as المبلغ,DateFrom as الفترة_من,DateTo as الي,Notes as ملاحظات from OverSales where DateFrom between '" + txtDateFrom.Text + "' and '" + txtDateTo.Text + "'";
                grdSelect.DataSource = DataObj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
            else
            {
                string sql = "select Code as الكود,NameOfOver as الاسم,Amount as المبلغ,DateFrom as الفترة_من,DateTo as الي,Notes as ملاحظات from OverSales";
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
    protected void grdSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = grdSelect.SelectedRow;
            Response.Redirect("Select-OverDetails.aspx?code=" + row.Cells[1].Text);
        }
        catch (Exception ex)
        {
            DataObj.Alert(ex.Message, this);
        }
    }
    protected void txtDateFrom_TextChanged(object sender, EventArgs e)
    {
        BindgrdTrades();
    }
    protected void txtDateTo_TextChanged(object sender, EventArgs e)
    {
        BindgrdTrades();
    }
}