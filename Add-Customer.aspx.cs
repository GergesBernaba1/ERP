using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    databaseaccesslayer databaseaccesslayerobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=25 and IsVaild=1";
                DataTable dt = databaseaccesslayerobj.Selectdatatable(sql);
                if (dt.Rows.Count == 1)
                {
                    addValidationGroup();
                    generateguid();
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
    private void generateguid()
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
            int x = 0;
            if (RadioCustomer.SelectedValue == "مدين")
            {
                x = int.Parse(txtMoney.Text);
            }
            else
            {
                x = int.Parse(txtMoney.Text) * -1;
            }
            DateTime dt = DateTime.Parse(txtDate.Text);
            string d = dt.ToString("MM-dd-yyyy");
            string sql = @"INSERT INTO [dbo].[CustomerAndSupplier] ([Code],[Name],[InitialBalance],[DateOfDealing],[Status],[CompanyName],[Responsibility],[Mobil],[Mobil1],[Mobil2],[Customer],[Supplier]) VALUES(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12)";
            databaseaccesslayerobj.Execute(sql, TxtCode.Text, TxtARName.Text, x, d, chkActive.Checked, txtCompanyName.Text, txtResponsibility.Text, txtTelephone.Text, txtTelephone1.Text, txtTelephone2.Text, true, false);
            databaseaccesslayerobj.Alert("success", this);
            helper.cleartxt(UpdatePanel1);
            generateguid();
            chkActive.Checked = false;
        }
        catch (Exception ex)
        {
            databaseaccesslayerobj.Alert(ex.Message, this);
        }
    }
}