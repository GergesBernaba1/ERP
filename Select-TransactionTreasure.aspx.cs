using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_TransactionTreasure : System.Web.UI.Page
{
    databaseaccesslayer DataObj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=10 and IsVaild=1";
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
            if (txtName.Text != "" && txtDateFrom.Text.Trim() == "" && txtDateTo.Text.Trim() == "")
            {
                string sql = @"select top(50)TypeOfOperation as نوع_العملية,Amount as المبلغ,DateOfOperation تاريخ_العملية from TransactionOfTreasure where TypeOfOperation like '%" + txtName.Text + "%' order by DateOfOperation desc";
                grdSelect.DataSource = DataObj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
            else if (txtName.Text != "" && txtDateFrom.Text.Trim() != "" && txtDateTo.Text.Trim() != "")
            {
                string sql = @"select top(50)TypeOfOperation as نوع_العملية,Amount as المبلغ,DateOfOperation تاريخ_العملية from TransactionOfTreasure where DateOfOperation between '" + txtDateFrom.Text + "' and '" + txtDateTo.Text + "' and TypeOfOperation like '%" + txtName.Text + "%' order by DateOfOperation desc";
                grdSelect.DataSource = DataObj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
            else if (txtDateFrom.Text.Trim() != "" && txtDateTo.Text.Trim() != "")
            {
                string sql = @"select top(50)TypeOfOperation as نوع_العملية,Amount as المبلغ,DateOfOperation تاريخ_العملية from TransactionOfTreasure where DateOfOperation between '" + txtDateFrom.Text + "' and '" + txtDateTo.Text + "' order by DateOfOperation desc";
                grdSelect.DataSource = DataObj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
            else
            {
                string sql = @"select top(50)TypeOfOperation as نوع_العملية,Amount as المبلغ,DateOfOperation تاريخ_العملية from TransactionOfTreasure order by DateOfOperation desc";
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
    protected void txtName_TextChanged(object sender, EventArgs e)
    {
        BindgrdTrades();
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