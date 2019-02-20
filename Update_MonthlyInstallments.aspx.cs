using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Update_MonthlyInstallments : System.Web.UI.Page
{
    databaseaccesslayer databaseaccesslayerobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            addValidationGroup();
            for (int i = 1; i < 13; i++)
            {
                drpMonth.Items.Add(i.ToString());
            }
            for (int i = 1; i < 31; i++)
            {
                drpDay.Items.Add(i.ToString());
            }
            if (Page.Request.QueryString["code"] != "" && Page.Request.QueryString["code"] != null)
            {
                try{
                string sql = "SelectMonthlyInstallmentsByCode '" + Request.QueryString["code"].ToString() + "'";
                DataTable dt = databaseaccesslayerobj.Selectdatatable(sql);
                DateTime dts = DateTime.Parse(dt.Rows[0]["DateOfInstallments"].ToString());
                string d = dts.ToString("MM/dd/yyyy");
                txtCode.Text = dt.Rows[0]["Code"].ToString();
                txtDate.Text = d;
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                txtWin.Text = dt.Rows[0]["ProfitPremium"].ToString();
                labTotal.Text = dt.Rows[0]["InstallmentAmount"].ToString();
                txtNum.Text = dt.Rows[0]["NumOfInstallment"].ToString();
                drpMonth.Text = dt.Rows[0]["BeginMonth"].ToString();
                drpDay.Text = dt.Rows[0]["PaymentDay"].ToString();
                string sqldata = "select Date as التاريخ,Amount as المبلغ from MonthlyInstallmentsDetails where code='" + Request.QueryString["code"].ToString() + "'";
                grdInstallment.DataSource = databaseaccesslayerobj.Selectdatatable(sqldata);
                grdInstallment.DataBind();
                }
                catch (Exception) { Response.Redirect("Select_MonthlyInstallments.aspx"); }
            }
            else
            {
                Response.Redirect("Select_MonthlyInstallments.aspx");
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
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        float x = 0;
        try
        {
            if (txtWin.Text.Trim() == "")
            {
                labTotal.Text = (x + float.Parse(txtAmount.Text)).ToString();
            }
            else
            {
                labTotal.Text = (float.Parse(txtWin.Text) + float.Parse(txtAmount.Text)).ToString();
            }
        }
        catch (Exception ex)
        {
            databaseaccesslayerobj.Alert(ex.Message, this);
        }

    }
    protected void txtWin_TextChanged(object sender, EventArgs e)
    {
        float x = 0;
        try
        {
            if (txtAmount.Text.Trim() == "")
            {
                labTotal.Text = (x + float.Parse(txtWin.Text)).ToString();
            }
            else
            {
                labTotal.Text = (float.Parse(txtAmount.Text) + float.Parse(txtWin.Text)).ToString();
            }
        }
        catch (Exception ex)
        {
            databaseaccesslayerobj.Alert(ex.Message, this);
        }
    }
    protected void btnPuplish_Click(object sender, EventArgs e)
    {
        try
        {
            //MM-dd-yyyy
            string sql = "delete from [dbo].[MonthlyInstallmentsDetails] where code=@1";
            databaseaccesslayerobj.Execute(sql, txtCode.Text);
            int year = DateTime.Now.Year;
            string InsertSql = "INSERT INTO [dbo].[MonthlyInstallmentsDetails] ([Code],[Date],[Amount]) VALUES (@1,@2,@3)";
            int x = int.Parse(txtNum.Text);
            float Total = float.Parse(labTotal.Text) / x;
            for (int i = 1; i <= x; i++)
            {
                int num = (int.Parse(drpMonth.Text) + i - 1);
                if (num <= 12)
                {
                    string date = num + "-" + drpDay.Text + "-" + year;
                    databaseaccesslayerobj.Execute(InsertSql, txtCode.Text, date, Total);
                }
                else
                {
                    string date = (num - 12) + "-" + (int.Parse(drpDay.Text) + 1) + "-" + year;
                    databaseaccesslayerobj.Execute(InsertSql, txtCode.Text, date, Total);
                }
            }
            grdInstallment.DataSource = databaseaccesslayerobj.Selectdatatable("select [Date] as التاريخ,[Amount] as المبلغ from MonthlyInstallmentsDetails where code='" + txtCode.Text + "'");
            grdInstallment.DataBind();
        }
        catch (Exception ex)
        { databaseaccesslayerobj.Alert(ex.Message, this); }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string sql = "update MonthlyInstallments set DateOfInstallments=@1,Amount=@2,ProfitPremium=@3,InstallmentAmount=@4,NumOfInstallment=@5,BeginMonth=@6,PaymentDay=@7 where Code=@8";
            DateTime dt = DateTime.Parse(txtDate.Text);
            string d = dt.ToString("MM-dd-yyyy");
            databaseaccesslayerobj.Execute(sql, d, float.Parse(txtAmount.Text), float.Parse(txtWin.Text), float.Parse(labTotal.Text), int.Parse(txtNum.Text), int.Parse(drpMonth.Text), int.Parse(drpDay.Text), txtCode.Text);
            Response.Redirect("Select_MonthlyInstallments.aspx");
        }
        catch (Exception ex)
        {
            databaseaccesslayerobj.Alert(ex.Message, this);
        }
    }
}