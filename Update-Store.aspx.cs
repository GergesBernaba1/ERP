using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Update_Store : System.Web.UI.Page
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
                    string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=17 and IsVaild=1";
                    DataTable dt1 = databaseaccesslayerobj.Selectdatatable(sql1);
                    if (dt1.Rows.Count == 1)
                    {
                        string sql = "select * from Store where Code='" + Request.QueryString["code"].ToString() + "'";
                        DataTable dt = databaseaccesslayerobj.Selectdatatable(sql);
                        txtStoreNumber.Text = dt.Rows[0]["Code"].ToString();
                        txtARStoreName.Text = dt.Rows[0]["StoreName"].ToString();
                        txtPhone.Text = dt.Rows[0]["StoreTel"].ToString();
                        chkActive.Checked = bool.Parse(dt.Rows[0]["Status"].ToString());
                        txtARManager.Text = dt.Rows[0]["Responsibility"].ToString();
                        txtMobile.Text = dt.Rows[0]["Mobil"].ToString();
                        txtARAddress.Text = dt.Rows[0]["StoreAddress"].ToString();
                    }
                    else
                    {
                        Response.Redirect("Select-Stores.aspx");
                    }
                }
                catch (Exception) { Response.Redirect("Select-Stores.aspx"); }
            }
            else
            {
                Response.Redirect("Select-Stores.aspx");
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
            string sql = @"UPDATE [dbo].[Store] SET [StoreName] =@1,[StoreTel] =@2,[Status] =@3,[Responsibility] =@4,[Mobil] =@5,[StoreAddress] =@6 WHERE [Code]='" + Request.QueryString["code"].ToString() + "'";
            databaseaccesslayerobj.Execute(sql, txtARStoreName.Text, txtPhone.Text, chkActive.Checked, txtARManager.Text, txtMobile.Text, txtARAddress.Text);
            Response.Redirect("Select-Stores.aspx");
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
            string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=18 and IsVaild=1";
            DataTable dt1 = databaseaccesslayerobj.Selectdatatable(sql1);
            if (dt1.Rows.Count == 1)
            {
                string sql = @"delete from  [dbo].[Store]  WHERE [Code]=@1";
                databaseaccesslayerobj.Execute(sql, Request.QueryString["code"].ToString());
                Response.Redirect("Select-Stores.aspx");
            }
            else
            {
                Response.Redirect("Select-Stores.aspx");
            }
        }
        catch (Exception)
        {
            Response.Redirect("Select-Stores.aspx");
        }
    }
}