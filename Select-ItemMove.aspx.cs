using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_ItemMove : System.Web.UI.Page
{
    databaseaccesslayer DataObj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=20 and IsVaild=1";
                DataTable dt = DataObj.Selectdatatable(sql);
                if (dt.Rows.Count == 1)
                {
                    BindgrdTrades();
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
    private void BindgrdTrades()
    {
        try
        {
            if (txtDateFrom.Text.Trim() != "" && txtDateTo.Text.Trim() != "")
            {
                string sql = "select o.Code as كود_العملية,s.StoreName as من_مخزن,ss.StoreName as الي_مخزن,DateOfOperation as التاريخ,Notes as ملاحظات from [dbo].[MoveItemStores] o left join Store s on o.StoreFrom=s.Code left join store ss on o.StoreTo=ss.Code  where DateOfOperation between '" + txtDateFrom.Text + "' and '" + txtDateTo.Text + "'";
                grdSelect.DataSource = DataObj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
            else
            {
                string sql = "select o.Code as كود_العملية,s.StoreName as من_مخزن,ss.StoreName as الي_مخزن,DateOfOperation as التاريخ,Notes as ملاحظات from [dbo].[MoveItemStores] o left join Store s on o.StoreFrom=s.Code left join store ss on o.StoreTo=ss.Code ";
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
            Response.Redirect("Select-ItemMoveDetails.aspx?code=" + row.Cells[1].Text);
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