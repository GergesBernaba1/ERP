using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_ItemMoveDetails : System.Web.UI.Page
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
                    if (Page.Request.QueryString["code"] != "" && Page.Request.QueryString["code"] != null)
                    {
                        BindgrdTrades();
                    }
                    else
                    {
                        Response.Redirect("Select-ItemMove.aspx");
                    }
                    grdSelect.DataBind();
                }
                else
                {
                    Response.Redirect("Select-ItemMove.aspx");
                }
            }
            catch (Exception)
            {
                Response.Redirect("Select-ItemMove.aspx");
            }
        }
    }
    private void BindgrdTrades()
    {
        try
        {
            string sql = @"select i.BarCode as الباركود,i.ItemName as اسم_الصنف,o.Quantity as الكمية,Type As النوع from MoveItemStoresDetails o left join Items i on o.ItemCode=i.Code where MoveCode='" + Request.QueryString["code"].ToString() + "'";
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        BindgrdTrades();
    }
}