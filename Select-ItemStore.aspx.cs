using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_ItemStore : System.Web.UI.Page
{
    databaseaccesslayer DataObj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=23 and IsVaild=1";
                DataTable dt = DataObj.Selectdatatable(sql);
                if (dt.Rows.Count == 1)
                {
                    string sqlStore = @"select Code,StoreName from Store";
                    drpselect.DataSource = DataObj.Selectdatatable(sqlStore);
                    drpselect.DataValueField = "Code";
                    drpselect.DataTextField = "StoreName";
                    drpselect.DataBind();
                    drpselect.Items.Insert(0, "اختار المخزن");
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
            string sql = @" select BarCode as الباركود,ItemName as اسم_الصنف,Notes as ملاحظات from ItemStore ii left join Items i on ii.ItemCode=i.Code left join Store s on ii.StoreCode=s.Code where StoreCode='" + drpselect.SelectedItem.Value + "'";
            grdSelect.DataSource = DataObj.Selectdatatable(sql);
            grdSelect.DataBind();
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
    protected void drpselect_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void grdSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=24 and IsVaild=1";
            DataTable dt = DataObj.Selectdatatable(sql1);
            if (dt.Rows.Count == 1)
            {
                GridViewRow row = grdSelect.SelectedRow;
                string sql = @"DELETE FROM [dbo].[ItemStore] WHERE [StoreCode]='" + drpselect.SelectedItem.Value + "' and [ItemCode]=(select Code from Items where BarCode='" + row.Cells[1].Text + "')";
                DataObj.Execute(sql);
                drpselect.Text = "اختار المخزن";
                grdSelect.DataSource = null;
                grdSelect.DataBind();
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