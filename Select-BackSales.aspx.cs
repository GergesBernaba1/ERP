using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_BackSales : System.Web.UI.Page
{
    databaseaccesslayer DataObj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=43 and IsVaild=1";
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
            if (Radio.SelectedValue == "تعامل نقدي")
            {
                if (txtDateFrom.Text.Trim() != "" && txtDateTo.Text.Trim() != "")
                {
                    string sql = "select p.[Code] as الكود,[Supplier_Id] as المورد,[DateOfSale] as التاريخ,[AddMoney] as الاضافة,[Discount]as الخصم,[Total] as المبلغ,s.StoreName as المخزن,[Notes] as ملاحظات from [dbo].[BackSales] p left join Store s on p.Store_Id=s.Code where [Supplier_Id] ='تعامل نقدي'  and [DateOfSale] between '" + txtDateFrom.Text + "' and '" + txtDateTo.Text + "'";
                    grdSelect.DataSource = DataObj.Selectdatatable(sql);
                    grdSelect.DataBind();
                }
                else
                {
                    string sql = "select p.[Code] as الكود,[Supplier_Id] as المورد,[DateOfSale] as التاريخ,[AddMoney] as الاضافة,[Discount]as الخصم,[Total] as المبلغ,s.StoreName as المخزن,[Notes] as ملاحظات from [dbo].[BackSales] p left join Store s on p.Store_Id=s.Code where [Supplier_Id] ='تعامل نقدي'";
                    grdSelect.DataSource = DataObj.Selectdatatable(sql);
                    grdSelect.DataBind();
                }
            }
            else
            {
                if (txtDateFrom.Text.Trim() != "" && txtDateTo.Text.Trim() != "")
                {
                    string sql = "select p.[Code] as الكود,[Supplier_Id] as المورد,[DateOfSale] as التاريخ,[AddMoney] as الاضافة,[Discount]as الخصم,[Total] as المبلغ,s.StoreName as المخزن,[Notes] as ملاحظات from [dbo].[BackSales] p left join Store s on p.Store_Id=s.Code where [Supplier_Id] !='تعامل نقدي'  and [DateOfSale] between '" + txtDateFrom.Text + "' and '" + txtDateTo.Text + "'";
                    grdSelect.DataSource = DataObj.Selectdatatable(sql);
                    grdSelect.DataBind();
                }
                else
                {
                    string sql = "select p.[Code] as الكود,[Supplier_Id] as المورد,[DateOfSale] as التاريخ,[AddMoney] as الاضافة,[Discount]as الخصم,[Total] as المبلغ,s.StoreName as المخزن,[Notes] as ملاحظات from [dbo].[BackSales] p left join Store s on p.Store_Id=s.Code where [Supplier_Id] !='تعامل نقدي'";
                    grdSelect.DataSource = DataObj.Selectdatatable(sql);
                    grdSelect.DataBind();
                }
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
            Response.Redirect("Select-BackSaleDetails.aspx?code=" + row.Cells[1].Text);
        }
        catch (Exception ex)
        {
            DataObj.Alert(ex.Message, this);
        }
    }
    protected void RadioCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindgrdTrades();
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