using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Update_Customer : System.Web.UI.Page
{
    databaseaccesslayer databaseaccesslayerobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            addValidationGroup();
            if (Page.Request.QueryString["code"] != "" && Page.Request.QueryString["code"] != null)
            {
                try{
                    string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=27 and IsVaild=1";
                DataTable dt1 = databaseaccesslayerobj.Selectdatatable(sql1);
                if (dt1.Rows.Count == 1)
                {
                string sql = "select * from CustomerAndSupplier where Code='" + Request.QueryString["code"].ToString() + "'";
                DataTable dt = databaseaccesslayerobj.Selectdatatable(sql);
                TxtCode.Text = dt.Rows[0]["Code"].ToString();
                TxtARName.Text = dt.Rows[0]["Name"].ToString();
                if (float.Parse(dt.Rows[0]["InitialBalance"].ToString()) >= 0)
                {
                    txtMoney.Text = dt.Rows[0]["InitialBalance"].ToString();
                    RadioCustomer.Items.FindByValue("مدين").Selected = true;
                }
                else
                {
                    float y = float.Parse(dt.Rows[0]["InitialBalance"].ToString()) * -1;
                    txtMoney.Text = y.ToString();
                    RadioCustomer.Items.FindByValue("دائن").Selected = true;
                }
                DateTime dts = DateTime.Parse(dt.Rows[0]["DateOfDealing"].ToString());
                string d = dts.ToString("MM/dd/yyyy");
                txtDate.Text = d;
                chkActive.Checked = bool.Parse(dt.Rows[0]["Status"].ToString());
                txtCompanyName.Text = dt.Rows[0]["CompanyName"].ToString();
                txtResponsibility.Text = dt.Rows[0]["Responsibility"].ToString();
                txtTelephone.Text = dt.Rows[0]["Mobil"].ToString();
                txtTelephone1.Text = dt.Rows[0]["Mobil1"].ToString();
                txtTelephone2.Text = dt.Rows[0]["Mobil2"].ToString();
                }
                else
                {
                    Response.Redirect("MainPage.aspx");
                }
                }
                catch (Exception) { Response.Redirect("MainPageaspx"); }
            }
            else
            {
                Response.Redirect("select-Customer.aspx");
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
            string sql = @"UPDATE [dbo].[CustomerAndSupplier] SET [Name] = @1,[InitialBalance] = @2,[DateOfDealing] = @3,[Status] = @4,
                        [CompanyName] = @5,[Responsibility] = @6,[Mobil] = @7,[Mobil1] = @8,[Mobil2] = @9 WHERE [Code]=@10";
            databaseaccesslayerobj.Execute(sql, TxtARName.Text, x, d, chkActive.Checked,txtCompanyName.Text, txtResponsibility.Text, txtTelephone.Text, txtTelephone1.Text, txtTelephone2.Text, Request.QueryString["code"].ToString());
            string sqlsel = @"select * from CustomerAndSupplier where Customer=1 and code='" + Request.QueryString["code"].ToString() + "'";
            DataTable  dts = databaseaccesslayerobj.Selectdatatable(sqlsel);
            if (dts.Rows.Count == 1)
            {
                Response.Redirect("select-Customer.aspx");
            }
            else if(dts.Rows.Count==0)
            {
                Response.Redirect("select-Supplier.aspx");
            }
        }
        catch (Exception ex)
        {
            databaseaccesslayerobj.Alert(ex.Message, this);
        }
    }
}