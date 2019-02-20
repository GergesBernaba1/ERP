using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Add_MonthlyInstallments : System.Web.UI.Page
{
    databaseaccesslayer databaseaccesslayerobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=28 and IsVaild=1";
                DataTable dt = databaseaccesslayerobj.Selectdatatable(sql);
                if (dt.Rows.Count == 1)
                {
                    addValidationGroup();
                    generateguid();
                    drpType.Items.Insert(0, "اختار");
                    drpType.Items.Insert(1, "عملاء");
                    drpType.Items.Insert(2, "موردين");
                    for (int i = 1; i < 13; i++)
                    {
                        drpMonth.Items.Add(i.ToString());
                    }
                    for (int i = 1; i < 31; i++)
                    {
                        drpDay.Items.Add(i.ToString());
                    }
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
        txtCode.Text = Code;
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
    protected void btnPuplish_Click(object sender, EventArgs e)
    {
        try
        {
            string sql = "delete from [dbo].[tempMonthlyInstallmentsDetails] where code=@1";
            databaseaccesslayerobj.Execute(sql, txtCode.Text);
            int year = DateTime.Now.Year;
            string InsertSql = "INSERT INTO [dbo].[tempMonthlyInstallmentsDetails] ([Code],[Date],[Amount],[IsVaild]) VALUES (@1,@2,@3,@4)";
            int x = int.Parse(txtNum.Text);
            float Total = float.Parse(txtAmount.Text) / x;
            for (int i = 1; i <= x; i++)
            {
                int num = (int.Parse(drpMonth.Text) + i - 1);
                if (num <= 12)
                {
                    string date = num + "-" + drpDay.Text + "-" + year;
                    databaseaccesslayerobj.Execute(InsertSql, txtCode.Text, date, Total, 0);
                }
                else
                {
                    string date = (num - 12) + "-" + (int.Parse(drpDay.Text) + 1) + "-" + year;
                    databaseaccesslayerobj.Execute(InsertSql, txtCode.Text, date, Total, 0);
                }
            }
            grdInstallment.DataSource = databaseaccesslayerobj.Selectdatatable("select [Date] as التاريخ,[Amount] as المبلغ from tempMonthlyInstallmentsDetails where code='" + txtCode.Text + "'");
            grdInstallment.DataBind();
            btnSave.Enabled = true;
        }
        catch (Exception ex)
        {
            databaseaccesslayerobj.Alert(ex.Message, this);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string x = "";
            if (drpName.SelectedIndex > 0)
            {
                x = drpName.SelectedItem.Value;
            }
            bool y = false;
            if (Radio.SelectedValue == "استلام")
            {
                y = true;
            }
            string sql = @"INSERT INTO [dbo].[MonthlyInstallments]([Code],[DateOfInstallments],[CustomerOrSupplier_Id],[Amount],
[NumOfInstallment],[BeginMonth],[PaymentDay],[IsVaild]) VALUES(@1,@2,@3,@4,@5,@6,@7,@8)";
            DateTime dt = DateTime.Parse(txtDate.Text);
            string d = dt.ToString("MM-dd-yyyy");
            databaseaccesslayerobj.Execute(sql, txtCode.Text, d, x, float.Parse(txtAmount.Text),int.Parse(txtNum.Text), int.Parse(drpMonth.Text), int.Parse(drpDay.Text),y);
            string sql1 = @"insert into [dbo].[MonthlyInstallmentsDetails] select * from [dbo].[tempMonthlyInstallmentsDetails] where [Code]=@1";
            databaseaccesslayerobj.Execute(sql1, txtCode.Text);
            string sqldel = @"delete from [dbo].[tempMonthlyInstallmentsDetails] where [Code]=@1";
            databaseaccesslayerobj.Execute(sqldel, txtCode.Text);
            databaseaccesslayerobj.Alert("success", this);
            helper.cleartxt(UpdatePanel1);
            generateguid();
            grdInstallment.DataSource = null;
            grdInstallment.DataBind();
        }
        catch (Exception ex)
        {
            databaseaccesslayerobj.Alert(ex.Message, this);
        }
    }
    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpType.SelectedIndex == 1)
        {
            string sqlcust = "select Code,Name from CustomerAndSupplier where Customer=1";
            drpName.DataSource = databaseaccesslayerobj.Selectdatatable(sqlcust);
            drpName.DataValueField = "Code";
            drpName.DataTextField = "Name";
            drpName.DataBind();

        }
        else if (drpType.SelectedIndex == 2)
        {
            string sqlsup = "select Code,Name from CustomerAndSupplier where Supplier=1";
            drpName.DataSource = databaseaccesslayerobj.Selectdatatable(sqlsup);
            drpName.DataValueField = "Code";
            drpName.DataTextField = "Name";
            drpName.DataBind();
        }
        drpName.Items.Insert(0, "اختر الاسم");
    }
}