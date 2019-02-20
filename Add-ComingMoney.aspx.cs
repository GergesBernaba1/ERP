using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Add_ComingMoney : System.Web.UI.Page
{
    databaseaccesslayer databaseaccesslayerobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=50 and IsVaild=1";
                DataTable dt1 = databaseaccesslayerobj.Selectdatatable(sql1);
                if (dt1.Rows.Count == 1)
                {
                    CreateGuid();
                    addValidationGroup();
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
    private void CreateGuid()
    {
        Guid autocode = Guid.NewGuid();
        string Code = autocode.ToString().Substring(0, 8);
        TxtCode.Text = Code;

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
            string Expense = @"INSERT INTO [dbo].[ComingMoney] ([Code],[Name],[DateOfComingMoney],[Amount],[User_Id],[Notes]) VALUES(@1,@2,@3,@4,@5,@6)";
            databaseaccesslayerobj.Execute(Expense, TxtCode.Text, TxtName.Text, DateTime.Parse(txtDate.Text), float.Parse(txtAmount.Text), Session["Id"], txtNotes.Text);
            string treasure = @"INSERT INTO [dbo].[TransactionOfTreasure] ([Code],[Amount],[TypeOfOperation],[DateOfOperation],[User_Id]) VALUES(@1,@2,@3,@4,@5)";
            databaseaccesslayerobj.Execute(treasure, TxtCode.Text, float.Parse(txtAmount.Text), "ايرادات", DateTime.Now, Session["Id"]);
            helper.cleartxt(UpdatePanel1);
            databaseaccesslayerobj.Alert("Success", this);
            CreateGuid();
        }
        catch (Exception ex)
        {
            databaseaccesslayerobj.Alert(ex.Message, this);
        }
    }
}