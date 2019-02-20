using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Add_Users : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try{
            string sql = @"select * from Application_Setting where User_Id="+Session["Id"]+" and Appl_Id=1 and IsVaild=1";
            DataTable dt = dataobj.Selectdatatable(sql);
            if (dt.Rows.Count!=1)
            {
                Response.Redirect("MainPage.aspx");
            }
            addValidationGroup();
            BindgrdTrades();
            }
            catch (Exception)
            { Response.Redirect("MainPage.aspx"); }
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
    private void BindgrdTrades()
    {
        try
        {
            string sql = @"select Id as الكود,FormName as الاسم from Application";
            grdSelect.DataSource = dataobj.Selectdatatable(sql);
            grdSelect.DataBind();
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string sql = @"INSERT INTO [dbo].[Users] ([Name],[UserName],[Password],[Mobil],[Address]) VALUES(@1,@2,@3,@4,@5)";
            dataobj.Execute(sql, TxtName.Text, txtUserName.Text, txtPassword.Text, txtMobil.Text, txtAddress.Text);
            string sql1 = @"select Id from [dbo].[Users] where [UserName]='" + txtUserName.Text + "' and [Password]='"+txtPassword.Text+"'";
            DataTable dt = dataobj.Selectdatatable(sql1);
            for (int i = 0; i < grdSelect.Rows.Count; i++)
            {
                CheckBox c = (grdSelect.Rows[i].Cells[0].FindControl("btnCheckAll") as CheckBox);

                if (c.Checked == true)
                {
                    string sql2 = @"INSERT INTO [dbo].[Application_Setting] ([User_Id],[Appl_Id],[IsVaild]) VALUES(@1,@2,@3)";
                    dataobj.Execute(sql2, dt.Rows[0]["Id"].ToString(), grdSelect.Rows[i].Cells[1].Text, true);
                }
            }
            helper.cleartxt(UpdatePanel1);
            BindgrdTrades();
            dataobj.Alert("Success", this);
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void chkActive_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            bool x = chkActive.Checked;
            for (int i = 0; i < grdSelect.Rows.Count; i++)
            {
                CheckBox c = (grdSelect.Rows[i].Cells[0].FindControl("btnCheckAll") as CheckBox);
                c.Checked = x;
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
}