using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_ItemsCount : System.Web.UI.Page
{
    databaseaccesslayer DataObj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=21 and IsVaild=1";
                DataTable dt = DataObj.Selectdatatable(sql1);
                if (dt.Rows.Count == 1)
                {
                    string sql = @"select Code,StoreName from Store";
                    drpStore.DataSource = DataObj.Selectdatatable(sql);
                    drpStore.DataValueField = "Code";
                    drpStore.DataTextField = "StoreName";
                    drpStore.DataBind();
                    drpStore.Items.Insert(0, "اختار المخزن");
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
            if (drpStore.SelectedIndex > 0 && txtName.Text == "")
            {
                string sql = @"select BarCode as الباركود,ItemName as اسم_الصنف,(select SUM(Quantity) from TransactionOfStore where ItemId=ii.Code and Store_Id=i.StoreCode) as الكمية from ItemStore i left join Items ii on i.ItemCode=ii.Code where i.StoreCode='"+drpStore.SelectedItem.Value+"'";
                grdSelect.DataSource = DataObj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
            else if (drpStore.SelectedIndex > 0 && txtName.Text!="")
            {
                string sql = @"select BarCode as الباركود,ItemName as اسم_الصنف,(select SUM(Quantity) from TransactionOfStore where ItemId=ii.Code and Store_Id=i.StoreCode) as الكمية from ItemStore i left join Items ii on i.ItemCode=ii.Code where i.StoreCode='" + drpStore.SelectedItem.Value + "' and ItemName like '%"+txtName.Text+"%'";
                grdSelect.DataSource = DataObj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
            else
            {
                grdSelect.DataSource =null;
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
    protected void drpStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindgrdTrades();
        txtName.Text = string.Empty;
    }
    protected void txtName_TextChanged(object sender, EventArgs e)
    {
        BindgrdTrades();
    }
}