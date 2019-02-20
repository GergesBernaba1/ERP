using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Add_ItemStores : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=22 and IsVaild=1";
                DataTable dt = dataobj.Selectdatatable(sql);
                if (dt.Rows.Count == 1)
                {
                    string sqlStore = @"select Code,StoreName from Store";
                    drpSelect.DataSource = dataobj.Selectdatatable(sqlStore);
                    drpSelect.DataValueField = "Code";
                    drpSelect.DataTextField = "StoreName";
                    drpSelect.DataBind();
                    drpSelect.Items.Insert(0, "اختار المخزن");
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
            if (drpSelect.SelectedIndex > 0)
            {
                string sql = @"select BarCode as الباركود,ItemName as اسم_الصنف from Items  where ItemName like '%" + txtName.Text + "%'";
                grdSelect.DataSource = dataobj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
            else
            {
                grdSelect.DataSource = null;
                grdSelect.DataBind();
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void grdSelect_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSelect.PageIndex = e.NewPageIndex;
        BindgrdTrades();
    }
    protected void drpSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindgrdTrades();
    }
    protected void txtName_TextChanged(object sender, EventArgs e)
    {
        BindgrdTrades();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < grdSelect.Rows.Count; i++)
                    {
                        CheckBox c = (grdSelect.Rows[i].Cells[0].FindControl("btnCheckAll") as CheckBox);

                        if (c.Checked == true)
                        {
                            string checksql = @"select * from ItemStore where ItemCode=(select Code from Items where BarCode='" + grdSelect.Rows[i].Cells[1].Text + "') and StoreCode='" + drpSelect.SelectedItem.Value + "'";
                            DataTable dt = dataobj.Selectdatatable(checksql);
                            if (dt.Rows.Count==0)
                            {
                                string selsql = @"select Code from Items where BarCode='"+grdSelect.Rows[i].Cells[1].Text+"'";
                                DataTable dts=dataobj.Selectdatatable(selsql);
                                string sql = @"INSERT INTO [dbo].[ItemStore] ([ItemCode],[StoreCode]) VALUES(@1,@2)";
                                dataobj.Execute(sql, dts.Rows[0]["Code"].ToString(), drpSelect.SelectedItem.Value);
                                
                            }
                        }

                    }
            grdSelect.DataSource = null;
            grdSelect.DataBind();
            drpSelect.Text = "اختار المخزن";
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
}