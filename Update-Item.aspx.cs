using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Update_Item : System.Web.UI.Page
{
    databaseaccesslayer databaseaccesslayerobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            addValidationGroup();
            if (Page.Request.QueryString["code"] != "" && Page.Request.QueryString["code"] != null)
            {
                try
                {
                    string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=13 and IsVaild=1";
                    DataTable dt1 = databaseaccesslayerobj.Selectdatatable(sql1);
                    if (dt1.Rows.Count == 1)
                    {
                        string sql = "select * from Items where BarCode='" + Request.QueryString["code"].ToString() + "'";
                        DataTable dt = databaseaccesslayerobj.Selectdatatable(sql);
                        TxtCode.Text = dt.Rows[0]["Code"].ToString();
                        TxtName.Text = dt.Rows[0]["ItemName"].ToString();
                        TxtBarCode.Text = dt.Rows[0]["BarCode"].ToString();
                        chkActive.Checked = bool.Parse(dt.Rows[0]["Status"].ToString());
                        chkDate.Checked = bool.Parse(dt.Rows[0]["DateOfItem"].ToString());
                        txtSale.Text = dt.Rows[0]["ItemSaleType"].ToString();
                        txtPruch.Text = dt.Rows[0]["ItemPruchType"].ToString();
                        txtNum.Text = dt.Rows[0]["Quantity"].ToString();
                        txtNotes.Text = dt.Rows[0]["Notes"].ToString();
                        txtPricePruch.Text = dt.Rows[0]["pruchPrice"].ToString();
                        txtPriceSale.Text = dt.Rows[0]["SalePrice"].ToString();
                    }
                    else
                    {
                        Response.Redirect("Select-Item.aspx");
                    }
                }
                catch (Exception) { Response.Redirect("Select-Item.aspx"); }
            }
            else
            {
                Response.Redirect("Select-Item.aspx");
            }
        }
    }
    private void addValidationGroup()
    {

        foreach (Control ctrl in UpdatePanel1.Controls[0].Controls)
        {

            TextBox txtbx = ctrl as TextBox;
            if (txtbx != null)
            {
                txtbx.Attributes["ValidationGroup"] = txtbx.ValidationGroup;
            }

            DropDownList drp = ctrl as DropDownList;
            if (drp != null)
            {
                drp.Attributes["ValidationGroup"] = drp.ValidationGroup;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string sql = @"UPDATE [dbo].[Items] SET [ItemName] = @1,[BarCode] =@2,[Status] =@3,[DateOfItem] =@4,[ItemSaleType] =@5,[ItemPruchType] =@6,[Quantity] = @7 ,[Notes] = @8,[pruchPrice]=@9,[SalePrice]=@10  WHERE [BarCode]='" + Request.QueryString["code"].ToString() + "'";
            databaseaccesslayerobj.Execute(sql, TxtName.Text, TxtBarCode.Text, chkActive.Checked, chkDate.Checked, txtSale.Text, txtPruch.Text, float.Parse(txtNum.Text), txtNotes.Text, float.Parse(txtPricePruch.Text), float.Parse(txtPriceSale.Text));
            Response.Redirect("Select-Item.aspx");
        }
        catch (Exception ex)
        {
            databaseaccesslayerobj.Alert(ex.Message, this);
        }
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
            string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=14 and IsVaild=1";
            DataTable dt1 = databaseaccesslayerobj.Selectdatatable(sql1);
            if (dt1.Rows.Count == 1)
            {
                string sql = @"delete from [dbo].[Items]  WHERE [BarCode]=@1";
                databaseaccesslayerobj.Execute(sql, Request.QueryString["code"].ToString());
                Response.Redirect("Select-Item.aspx");
            }
            else { Response.Redirect("Select-Item.aspx"); }
        }
        catch (Exception)
        {
            Response.Redirect("Select-Item.aspx");
        }
    }
}