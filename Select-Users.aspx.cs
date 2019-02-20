using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_Users : System.Web.UI.Page
{
    databaseaccesslayer DataObj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try{
            string sql = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=2 and IsVaild=1";
            DataTable dt = DataObj.Selectdatatable(sql);
            if (dt.Rows.Count != 1)
            {
                Response.Redirect("MainPage.aspx");
            }
            BindgrdTrades();
            }
            catch (Exception)
            { Response.Redirect("MainPage.aspx"); }
        }
    }
    private void BindgrdTrades()
    {
        try
        {
            string sql = @"select Name as الاسم,UserName as اسم_المستخدم,Mobil as الموبيل,Address as العنوان from Users";
            grdSelect.DataSource = DataObj.Selectdatatable(sql);
            grdSelect.DataBind();
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
            Response.Redirect("Update-UserDetails.aspx?User=" + row.Cells[2].Text);
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