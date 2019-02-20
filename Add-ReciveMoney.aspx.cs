using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Add_ReciveMoney : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=44 and IsVaild=1";
                DataTable dt1 = dataobj.Selectdatatable(sql1);
                if (dt1.Rows.Count == 1)
                {
                    BindgrdTrades();
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
    private void BindgrdTrades()
    {
        try
        {
            drpSelect.Items.Insert(0, "اختار");
            drpSelect.Items.Insert(1, "عملاء");
            drpSelect.Items.Insert(2, "موردين");
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
    protected void drpSelect_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpSelect.SelectedIndex == 1)
            {
                string sql1 = @"select Code,Name from CustomerAndSupplier where Customer=1";
                drpName.DataSource = dataobj.Selectdatatable(sql1);
                drpName.DataValueField = "Code";
                drpName.DataTextField = "Name";
                drpName.DataBind();
                drpName.Items.Insert(0, "اختار الاسم");
            }
            else if (drpSelect.SelectedIndex == 2)
            {
                string sql1 = @"select Code,Name from CustomerAndSupplier where Supplier=1";
                drpName.DataSource = dataobj.Selectdatatable(sql1);
                drpName.DataValueField = "Code";
                drpName.DataTextField = "Name";
                drpName.DataBind();
                drpName.Items.Insert(0, "اختار الاسم");
            }
            else
            {
                drpName.Items.Clear();
            }

        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void drpName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string sql1 = @"select * from MonthlyInstallments where CustomerOrSupplier_Id='" + drpName.SelectedItem.Value + "'";
            DataTable dt = dataobj.Selectdatatable(sql1);
            if (dt.Rows.Count > 0)
            {
                if (bool.Parse(dt.Rows[0]["IsVaild"].ToString()) == true)
                {
                    string sql2 = @"select Date as تاريخ_السداد,Amount as المبلغ from MonthlyInstallmentsDetails where Code='" + dt.Rows[0]["Code"].ToString() + "' and IsVaild=0";
                    grdSelect.DataSource = dataobj.Selectdatatable(sql2);
                    grdSelect.DataBind();
                }
                else
                {
                    grdSelect.DataSource = null;
                    grdSelect.DataBind();
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
            float x = 0;
            for (int i = 0; i < grdSelect.Rows.Count; i++)
            {
                CheckBox c = (grdSelect.Rows[i].Cells[0].FindControl("btnCheckAll") as CheckBox);

                if (c.Checked == true)
                {
                    x = x + float.Parse(grdSelect.Rows[i].Cells[2].Text);
                    string sql3 = @"select * from MonthlyInstallments where CustomerOrSupplier_Id='" + drpName.SelectedItem.Value + "'";
                    DataTable dts = dataobj.Selectdatatable(sql3);
                    string sql4 = @"update MonthlyInstallmentsDetails set IsVaild=@1 where Date=@2 and Code=@3";
                    dataobj.Execute(sql4, 1, DateTime.Parse(grdSelect.Rows[i].Cells[1].Text).ToShortDateString(), dts.Rows[0]["Code"].ToString());
                }
            }
            if (txtAmount.Text != "")
            {
                x = x + float.Parse(txtAmount.Text);
            }
            string sql1 = @"INSERT INTO [dbo].[TransactionOfTreasure] ([Code],[Amount],[TypeOfOperation],[DateOfOperation],[User_Id]) VALUES(@1,@2,@3,@4,@5)";
            dataobj.Execute(sql1, TxtCode.Text, x, "استلام نقدية", DateTime.Now, Session["Id"]);
            string sql = @"select * from CustomerAndSupplier where Code='" + drpName.SelectedItem.Value + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            string upsql = @"update CustomerAndSupplier set InitialBalance=InitialBalance+@1 where Code=@2";
            dataobj.Execute(upsql, (-1 * x), drpName.SelectedItem.Value);
            string sql2 = @"INSERT INTO [dbo].[ReciveMoney] ([Code],[CustomerOrSuppler_Id],[DateOfOperation],[Amount]) VALUES(@1,@2,@3,@4)";
            dataobj.Execute(sql2, TxtCode.Text, drpName.SelectedItem.Value, DateTime.Parse(txtDate.Text), x);
            helper.cleartxt(UpdatePanel1);
            grdSelect.DataSource = null;
            grdSelect.DataBind();
            drpName.Items.Clear();
            drpSelect.SelectedValue = "اختار";
            CreateGuid();
            dataobj.Alert("Success", this);
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
}