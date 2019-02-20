using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_UserDetails : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try{
            string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=3 and IsVaild=1";
            DataTable dt1 = dataobj.Selectdatatable(sql1);
            if (dt1.Rows.Count != 1)
            {
                Response.Redirect("Select-Users.aspx");
            }
            addValidationGroup();

            if (Page.Request.QueryString["User"] != "" && Page.Request.QueryString["User"] != null)
            {
                try
                {
                    string sql = "select * from Users where UserName='" + Request.QueryString["User"].ToString() + "'";
                    DataTable dt = dataobj.Selectdatatable(sql);
                    TxtName.Text = dt.Rows[0]["Name"].ToString();
                    txtUserName.Text = dt.Rows[0]["UserName"].ToString();
                    txtMobil.Text = dt.Rows[0]["Mobil"].ToString();
                    txtAddress.Text = dt.Rows[0]["Address"].ToString();
                    BindgrdTrades();
                }
                catch (Exception) { Response.Redirect("MainPage.aspx"); }
            }
            else
            {
                Response.Redirect("Select-Users.aspx");
            }
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
            string sql1 = @"select Appl_Id from Application_Setting where User_Id=(select Id from Users where UserName='" + Request.QueryString["User"].ToString() + "')";
            DataTable dt = dataobj.Selectdatatable(sql1);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < grdSelect.Rows.Count; j++)
                {
                    if (dt.Rows[i]["Appl_Id"].ToString() == grdSelect.Rows[j].Cells[1].Text)
                    {
                        CheckBox c = (grdSelect.Rows[j].Cells[0].FindControl("btnCheckAll") as CheckBox);
                        c.Checked = true;
                    }
                }
            }
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
            string sql = @"UPDATE [dbo].[Users] SET [Name] = @1,[UserName] = @2,[Mobil] = @3,[Address] = @4 WHERE UserName='" + Request.QueryString["User"].ToString() + "'";
            dataobj.Execute(sql, TxtName.Text, txtUserName.Text, txtMobil.Text, txtAddress.Text);
            
            string sql1 = @"select Id from [dbo].[Users] where [UserName]='" + txtUserName.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sql1);
            string sql3 = @"delete from Application_Setting where User_Id=@1";
            dataobj.Execute(sql3,dt.Rows[0]["Id"].ToString());
            for (int i = 0; i < grdSelect.Rows.Count; i++)
            {
                CheckBox c = (grdSelect.Rows[i].Cells[0].FindControl("btnCheckAll") as CheckBox);

                if (c.Checked == true)
                {
                    string sql2 = @"INSERT INTO [dbo].[Application_Setting] ([User_Id],[Appl_Id],[IsVaild]) VALUES(@1,@2,@3)";
                    dataobj.Execute(sql2, dt.Rows[0]["Id"].ToString(), grdSelect.Rows[i].Cells[1].Text, true);
                }
            }
            Response.Redirect("Select-Users.aspx");
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        try
        {
            string sql5 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=4 and IsVaild=1";
            DataTable dt1 = dataobj.Selectdatatable(sql5);
            if (dt1.Rows.Count != 1)
            {
                Response.Redirect("Select-Users.aspx");
            }
            string sql1 = @"select Id from [dbo].[Users] where [UserName]='" + Request.QueryString["User"].ToString() + "'";
            DataTable dt = dataobj.Selectdatatable(sql1);
            string sql2 = @"delete from Application_Setting where User_Id=@1";
            dataobj.Execute(sql2, dt.Rows[0]["Id"].ToString());
            string sql3 = @"delete from Users where Id=@1";
            dataobj.Execute(sql3, dt.Rows[0]["Id"].ToString());
            Response.Redirect("Select-Users.aspx");
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
}