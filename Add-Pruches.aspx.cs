using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

public partial class Add_Pruches : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql2 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=32 and IsVaild=1";
                DataTable dt1 = dataobj.Selectdatatable(sql2);
                if (dt1.Rows.Count == 1)
                {
                    generateguid();
                    addValidationGroup();
                    txtDate.Text = DateTime.Now.ToString();
                    string sql = @"select Code,StoreName from Store";
                    drpStore.DataSource = dataobj.Selectdatatable(sql);
                    drpStore.DataValueField = "Code";
                    drpStore.DataTextField = "StoreName";
                    drpStore.DataBind();
                    drpStore.Items.Insert(0, "اختار المخزن");
                    string sql1 = @"select Code,Name from CustomerAndSupplier where Supplier=1";
                    drpSupply.DataSource = dataobj.Selectdatatable(sql1);
                    drpSupply.DataValueField = "Code";
                    drpSupply.DataTextField = "Name";
                    drpSupply.DataBind();
                    drpSupply.Items.Insert(0, "تعامل نقدي");
                    ViewState.Add("price", 0);
                    ViewState.Add("quantity", 0);
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
    protected void drpStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpStore.SelectedIndex > 0)
            {
                drpType.Items.Clear();
                string itemsql = @"select i.Code as Code,ItemName from ItemStore ii left join Items i on ii.ItemCode=i.Code left join Store s on ii.StoreCode=s.Code where StoreCode='" + drpStore.SelectedItem.Value + "'";
                drpName.DataSource = dataobj.Selectdatatable(itemsql);
                drpName.DataValueField = "Code";
                drpName.DataTextField = "ItemName";
                drpName.DataBind();
                drpName.Items.Insert(0, "اختار الصنف");
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    private void DataBindData()
    {
        try
        {
            string sql = @"select ItemSaleType,ItemPruchType,DateOfItem from Items where Code='" + drpName.SelectedItem.Value + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            drpType.Items.Insert(0, dt.Rows[0]["ItemPruchType"].ToString());
            drpType.Items.Insert(1, dt.Rows[0]["ItemSaleType"].ToString());
            if (dt.Rows[0]["DateOfItem"].ToString() == "False")
            {
                txtDateOfItem.Enabled = false;
            }
            else
            {
                txtDateOfItem.Enabled = true;
                txtDateOfItem.Text = DateTime.Now.ToShortDateString();
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void txtBarCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string sql = @"select Code,pruchPrice from Items where BarCode='" + txtBarCode.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            drpName.SelectedValue = dt.Rows[0]["Code"].ToString();
            txtQuantity.Text = "1";
            txtprice.Text = dt.Rows[0]["pruchPrice"].ToString();
            txtTotal.Text = (float.Parse(dt.Rows[0]["pruchPrice"].ToString()) * (float.Parse(txtQuantity.Text))).ToString();
            DataBindData();
            ViewState.Add("price", float.Parse(txtprice.Text));
            ViewState.Add("quantity", float.Parse(txtQuantity.Text));
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void drpName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpName.SelectedIndex > 0)
            {
                string sql = @"select BarCode,pruchPrice from Items where Code='" + drpName.SelectedItem.Value + "'";
                DataTable dt = dataobj.Selectdatatable(sql);
                txtBarCode.Text = dt.Rows[0]["BarCode"].ToString();
                txtQuantity.Text = "1";
                txtprice.Text = dt.Rows[0]["pruchPrice"].ToString();
                txtTotal.Text = (float.Parse(dt.Rows[0]["pruchPrice"].ToString()) * (float.Parse(txtQuantity.Text))).ToString();
                DataBindData();
                ViewState.Add("price", float.Parse(txtprice.Text));
                ViewState.Add("quantity", float.Parse(txtQuantity.Text));
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    private void sumTotal()
    {
        string sql = @"select * from [dbo].[tempPrucheDetails] where PrucheCode='" + TxtCode.Text + "'";
        DataTable dt = dataobj.Selectdatatable(sql);
        if (dt.Rows.Count > 0)
        {
            string sqlsum = @"select SUM(Price*Quantity) as sumprice from [dbo].[tempPrucheDetails] where PrucheCode='" + TxtCode.Text + "'";
            DataTable ds = dataobj.Selectdatatable(sqlsum);
            float x = float.Parse(ds.Rows[0]["sumprice"].ToString());
            float sum = x + (float.Parse(txtAdd.Text)) - (float.Parse(txtDiscount.Text));
            LabTotal.Text = txtDebt.Text = sum.ToString();
        }
        else
        {
            LabTotal.Text = txtDebt.Text = "0";
        }
    }
    protected void LinkAdd_Click(object sender, EventArgs e)
    {
        try
         {
            addValidationGroup();
            if (drpName.SelectedIndex > 0)
            {
                if (txtDateOfItem.Enabled == false)
                {
                    string check = @"select * from [dbo].[tempPrucheDetails] where [PrucheCode]='" + TxtCode.Text + "' and [Store_Id]='" + drpStore.SelectedItem.Value + "' and [Item_Id]='" + drpName.SelectedItem.Value + "' and [Price]=" + float.Parse(txtprice.Text) + " and [Quantity]=" + float.Parse(txtQuantity.Text) + " and [Type]='" + drpType.Text + "'";
                    DataTable dt = dataobj.Selectdatatable(check);
                    if (dt.Rows.Count == 0)
                    {
                        float x = float.Parse(ViewState["price"].ToString());
                        float y = float.Parse(ViewState["quantity"].ToString());
                        string sql = @"INSERT INTO [dbo].[tempPrucheDetails] ([PrucheCode],[Item_Id],[Quantity],[Type],[Price],[Store_Id]) VALUES(@1,@2,@3,@4,@5,@6)";
                        dataobj.Execute(sql, TxtCode.Text, drpName.SelectedItem.Value, y, drpType.Text, x, drpStore.SelectedItem.Value);
                        txtBarCode.Text = txtQuantity.Text = txtprice.Text = string.Empty;
                        txtTotal.Text = "0";
                        drpName.SelectedValue = "اختار الصنف";
                        drpType.Items.Clear();
                        sumTotal();
                        txtPay.Text = 0.ToString();
                    }
                    else
                    {
                        dataobj.Alert("تم ادخال هذه البيانات من قبل", this);
                    }
                }
                else
                {
                    string check = @"select * from [dbo].[tempPrucheDetails] where [PrucheCode]='" + TxtCode.Text + "' and [Store_Id]='" + drpStore.SelectedItem.Value + "' and [Date]='" + txtDateOfItem.Text + "' and [Item_Id]='" + drpName.SelectedItem.Value + "' and [Price]=" + float.Parse(txtprice.Text) + " and [Quantity]=" + float.Parse(txtQuantity.Text) + " and [Type]='" + drpType.Text + "'";
                    DataTable dt = dataobj.Selectdatatable(check);
                    if (dt.Rows.Count == 0)
                    {
                        float x = float.Parse(ViewState["price"].ToString());
                        float y = float.Parse(ViewState["quantity"].ToString());
                        string sql = @"INSERT INTO [dbo].[tempPrucheDetails] ([PrucheCode],[Item_Id],[Quantity],[Type],[Price],[Date],[Store_Id]) VALUES(@1,@2,@3,@4,@5,@6,@7)";
                        dataobj.Execute(sql, TxtCode.Text, drpName.SelectedItem.Value, y, drpType.Text, x, DateTime.Parse(txtDateOfItem.Text).ToShortDateString(), drpStore.SelectedItem.Value);
                        txtBarCode.Text = txtQuantity.Text = txtprice.Text = string.Empty;
                        txtTotal.Text = "0";
                        drpName.SelectedValue = "اختار الصنف";
                        drpType.Items.Clear();
                        sumTotal();
                        txtPay.Text = 0.ToString();
                    }
                    else
                    {
                        dataobj.Alert("تم ادخال هذه البيانات من قبل", this);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    private void sumprice()
    {
        try
        {
            if (drpName.SelectedIndex > 0)
            {
                string sql = @"select pruchPrice from Items where ItemPruchType='" + drpType.Text + "' and BarCode='" + txtBarCode.Text + "'";
                DataTable dt = dataobj.Selectdatatable(sql);
                if (dt.Rows.Count == 1)
                {
                    float x = float.Parse(dt.Rows[0]["pruchPrice"].ToString());
                    txtprice.Text = x.ToString();
                    txtTotal.Text = ((float.Parse(x.ToString())) * (float.Parse(txtQuantity.Text))).ToString();
                }
                else
                {
                    string sql1 = @"select SalePrice from Items where ItemSaleType='" + drpType.Text + "' and BarCode='" + txtBarCode.Text + "'";
                    DataTable dts = dataobj.Selectdatatable(sql1);
                    if (dts.Rows.Count == 1)
                    {
                        float x = float.Parse(dts.Rows[0]["SalePrice"].ToString());
                        txtprice.Text = x.ToString();
                        txtTotal.Text = ((float.Parse(x.ToString())) * (float.Parse(txtQuantity.Text))).ToString();
                    }
                    else
                    {
                        txtprice.Text = 0.ToString();
                        txtTotal.Text = 0.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        sumprice();
    }
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpName.SelectedIndex > 0)
            {
                ViewState["price"] = float.Parse(txtprice.Text);
                ViewState["quantity"] = float.Parse(txtQuantity.Text);
                txtTotal.Text = (float.Parse(txtprice.Text) * float.Parse(txtQuantity.Text)).ToString();
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void txtAdd_TextChanged(object sender, EventArgs e)
    {
        sumTotal();
    }
    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        sumTotal();
    }
    protected void txtPay_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtDebt.Text = ((float.Parse(LabTotal.Text)) - (float.Parse(txtPay.Text))).ToString();
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    private void CompeleteData()
    {
        try
        {
            string sqlTran = @"select  (select Code from Items where BarCode=i.BarCode) as code,o.Quantity as Num,Price,Type,Date,Store_Id from [dbo].[tempPrucheDetails] o left join Items i on o.Item_Id=i.Code where [PrucheCode]='" + TxtCode.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sqlTran);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string check = @"select ItemPruchType,ItemSaleType,Quantity from Items where Code='" + dt.Rows[i]["code"].ToString() + "'";
                DataTable dts = dataobj.Selectdatatable(check);
                if (dts.Rows[0]["ItemPruchType"].ToString() == dt.Rows[i]["Type"].ToString())
                {
                    float Q = float.Parse(dts.Rows[0]["Quantity"].ToString()) * float.Parse(dt.Rows[i]["Num"].ToString());
                    string insertTran = @"INSERT INTO [dbo].[TransactionOfStore] ([Code],[ItemId],[Quantity],[TypeOfStore],[TypeOfOperation],[DateOfItem],[Store_Id]) VALUES(@1,@2,@3,@4,@5,@6,@7)";
                    dataobj.Execute(insertTran, TxtCode.Text, dt.Rows[i]["code"].ToString(), Q, dts.Rows[0]["ItemSaleType"].ToString(), "مشتريات", dt.Rows[i]["Date"].ToString(), dt.Rows[i]["Store_Id"].ToString());
                }
                else if (dts.Rows[0]["ItemSaleType"].ToString() == dt.Rows[i]["Type"].ToString())
                {
                    string insertTran = @"INSERT INTO [dbo].[TransactionOfStore] ([Code],[ItemId],[Quantity],[TypeOfStore],[TypeOfOperation],[DateOfItem],[Store_Id]) VALUES(@1,@2,@3,@4,@5,@6,@7)";
                    dataobj.Execute(insertTran, TxtCode.Text, dt.Rows[i]["code"].ToString(), dt.Rows[i]["Num"].ToString(), dts.Rows[0]["ItemSaleType"].ToString(), "مشتريات", dt.Rows[i]["Date"].ToString(), dt.Rows[i]["Store_Id"].ToString());
                }
            }

            string sqlTrea = @"INSERT INTO [dbo].[TransactionOfTreasure] ([Code],[Amount],[TypeOfOperation],[DateOfOperation],[User_Id]) VALUES(@1,@2,@3,@4,@5)";
            float Amount = -1 * (float.Parse(txtPay.Text));
            dataobj.Execute(sqlTrea, TxtCode.Text, Amount, "مشتريات", DateTime.Now.ToString(), Session["Id"] );

            string sql = @"insert into [dbo].[PrucheDetails] select [PrucheCode],[Item_Id],[Quantity],[Price],[Type],[Date],[Store_Id] from [dbo].[tempPrucheDetails] where PrucheCode=@1";
            dataobj.Execute(sql, TxtCode.Text);
            string sqldel = @"delete from [dbo].[tempPrucheDetails] where [PrucheCode]=@1";
            dataobj.Execute(sqldel, TxtCode.Text);

            txtAdd.Text = txtDiscount.Text = txtPay.Text = txtDebt.Text = LabTotal.Text = 0.ToString();
            drpStore.Text = "اختار المخزن";
            drpName.Items.Clear();
            generateguid();
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    private void Save()
    {
        try
        {
            if (drpSupply.SelectedIndex == 0)
            {
                if (txtPay.Text == LabTotal.Text)
                {
                    string sql = @"INSERT INTO [dbo].[Pruches] ([Code],[Supplier_Id],[Store_Id],[DateOfPruches],[User_Id],[AddMoney],[Discount],[Pay],[Total],[Notes]) VALUES(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10)";
                    dataobj.Execute(sql, TxtCode.Text, drpSupply.Text, drpStore.SelectedItem.Value, txtDate.Text, Session["Id"], float.Parse(txtAdd.Text), float.Parse(txtDiscount.Text), float.Parse(txtPay.Text), float.Parse(LabTotal.Text), txtNotes.Text);
                    CompeleteData();
                    dataobj.Alert("Success", this);
                }
                else
                {
                    dataobj.Alert("يجب دفع الفاتورة كاملة", this);
                }
            }
            else
            {
                string sql = @"INSERT INTO [dbo].[Pruches] ([Code],[Supplier_Id],[Store_Id],[DateOfPruches],[User_Id],[AddMoney],[Discount],[Pay],[Total],[Notes]) VALUES(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10)";
                dataobj.Execute(sql, TxtCode.Text, drpSupply.SelectedItem.Value, drpStore.SelectedItem.Value, txtDate.Text, Session["Id"], float.Parse(txtAdd.Text), float.Parse(txtDiscount.Text), float.Parse(txtPay.Text), float.Parse(LabTotal.Text), txtNotes.Text);

                string UpSupSql = @"UPDATE [dbo].[CustomerAndSupplier] SET [InitialBalance] += @1 WHERE [Code]=@2";
                float dept = -1 * (float.Parse(txtDebt.Text));
                dataobj.Execute(UpSupSql, dept, drpSupply.SelectedItem.Value);
                drpSupply.Text = "تعامل نقدي";

                CompeleteData();
                txtNotes.Text = string.Empty;
                dataobj.Alert("Success", this);
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Save();
    }
    private void Links()
    {
        try
        {
            int x = int.Parse(TextBox1.Text);
            string sql = @"select i.BarCode as BarCode,i.ItemName as Name,o.Quantity as Num,Price,Type,Date from [dbo].[tempPrucheDetails] o left join Items i on o.Item_Id=i.Code where [PrucheCode]='" + TxtCode.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            string sqldel = @"delete from tempPrucheDetails where [PrucheCode]=@1 and [Item_Id]=(select Code from Items where ItemName=@2) and [Quantity]=@3 and [Type]=@4 and [Price]=@5";
            dataobj.Execute(sqldel, TxtCode.Text, dt.Rows[x]["Name"].ToString(), float.Parse(dt.Rows[x]["Num"].ToString()), dt.Rows[x]["Type"].ToString(),float.Parse(dt.Rows[x]["Price"].ToString()));
            sumTotal();
        }
        catch (Exception ex)
        {
            dataobj.Execute(ex.Message, this);
        }
    }
    protected void BtnDelConf_Click(object sender, EventArgs e)
    {
        Links();
    }
    protected void BtnEditConf_Click(object sender, EventArgs e)
    {
        try
        {
            int x = int.Parse(TextBox1.Text);
            string sql = @"select i.BarCode as BarCode,i.ItemName as Name,o.Quantity as Num,Price,Type,Date from [dbo].[tempPrucheDetails] o left join Items i on o.Item_Id=i.Code where [PrucheCode]='" + TxtCode.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            txtBarCode.Text = dt.Rows[x]["BarCode"].ToString();

            string sqln = @"select Code from Items where BarCode='" + txtBarCode.Text + "'";
            DataTable dx = dataobj.Selectdatatable(sqln);
            drpName.SelectedValue = dx.Rows[0]["Code"].ToString();
            txtQuantity.Text = dt.Rows[x]["Num"].ToString();
            DateTime d = DateTime.Parse(dt.Rows[x]["Date"].ToString());
            string dd = d.ToShortDateString();
            txtDate.Text = dd;
            txtprice.Text = dt.Rows[x]["Price"].ToString();
            txtTotal.Text = (float.Parse(txtprice.Text) * float.Parse(txtQuantity.Text)).ToString();
            string sqls = @"select ItemSaleType,ItemPruchType from Items where ItemName='" + dt.Rows[x]["Name"].ToString() + "'";
            DataTable dts = dataobj.Selectdatatable(sqls);
            drpType.Items.Insert(0, dts.Rows[0]["ItemPruchType"].ToString());
            drpType.Items.Insert(1, dts.Rows[0]["ItemSaleType"].ToString());
            drpType.Text = dt.Rows[x]["Type"].ToString();
            Links();
        }
        catch (Exception ex)
        {
            dataobj.Execute(ex.Message, this);
        }
    }
    protected void txtprice_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpName.SelectedIndex > 0)
            {
                ViewState["price"] = float.Parse(txtprice.Text);
                ViewState["quantity"] = float.Parse(txtQuantity.Text);
                txtTotal.Text = (float.Parse(txtprice.Text) * float.Parse(txtQuantity.Text)).ToString();
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            string x = TxtCode.Text;
            Save();
            if (x!=TxtCode.Text)
            {
                string sql = @"ReportPrucheDetails '" + x + "'";
            System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
            ReportDocument reportdocument = new ReportDocument();
            reportdocument.Load(Server.MapPath("Reports/ReportDetailsPruches.rpt"));
            reportdocument.DataSourceConnections[0].SetConnection(".", "ERPsystem", true);
            reportdocument.SetDataSource(dataobj.Selectdatatable(sql));
            reportdocument.SetParameterValue("@code", x);
            reportdocument.PrintOptions.PrinterName = printDocument.PrinterSettings.PrinterName;
            reportdocument.PrintToPrinter(1, true, 1, 100);
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
}