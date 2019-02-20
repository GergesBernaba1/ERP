using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddSaleOver : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql3 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=36 and IsVaild=1";
                DataTable dt1 = dataobj.Selectdatatable(sql3);
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
                    string sql1 = @"select Code,Name from CustomerAndSupplier where Customer=1";
                    drpSupply.DataSource = dataobj.Selectdatatable(sql1);
                    drpSupply.DataValueField = "Code";
                    drpSupply.DataTextField = "Name";
                    drpSupply.DataBind();
                    drpSupply.Items.Insert(0, "تعامل نقدي");
                    ViewState.Add("priceOver", 0);
                    ViewState.Add("quantityOver", 0);
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
                string sqlover = @"select Code,NameOfOver from OverSales where Store_Id='" + drpStore.SelectedItem.Value + "' and DateFrom <='" + DateTime.Now.ToShortDateString() + "' and DateTo>='" + DateTime.Now.ToShortDateString() + "'";
                DropOverName.DataSource = dataobj.Selectdatatable(sqlover);
                DropOverName.DataValueField = "Code";
                DropOverName.DataTextField = "NameOfOver";
                DropOverName.DataBind();
                DropOverName.Items.Insert(0, "اختار العرض");
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    private void sumTotal()
    {
        float x = 0;
        float y = 0;
        string check1 = @"select * from tempOverSaleCustomer where PrucheCode='" + TxtCode.Text + "'";
        DataTable dt1 = dataobj.Selectdatatable(check1);
        if (dt1.Rows.Count > 0)
        {
            string sqlsum1 = @"select sum(Price*Quantity) as sumprice from tempOverSaleCustomer where PrucheCode='" + TxtCode.Text + "'";
            DataTable ds1 = dataobj.Selectdatatable(sqlsum1);
            y = float.Parse(ds1.Rows[0]["sumprice"].ToString());
        }
        float sum = y + x + (float.Parse(txtAdd.Text)) - (float.Parse(txtDiscount.Text));
        LabTotal.Text = txtDebt.Text = sum.ToString();
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
    private void compeleteOver()
    {
        try
        {
            string over = @"select * from tempOverSaleCustomer where PrucheCode='" + TxtCode.Text + "'";
            DataTable ot = dataobj.Selectdatatable(over);
            for (int i = 0; i < ot.Rows.Count; i++)
            {
                string detail = @"select * from OverSaleDetails where OverCode='" + ot.Rows[i]["Over_Id"].ToString() + "'";
                DataTable ox = dataobj.Selectdatatable(detail);
                for (int j = 0; j < ox.Rows.Count; j++)
                {
                    string inse = @"INSERT INTO [dbo].[tempPrucheDetails] ([PrucheCode],[Item_Id],[Quantity],[Type],[Store_Id],[OverDis]) VALUES(@1,@2,@3,@4,@5,@6)";
                    dataobj.Execute(inse, TxtCode.Text, ox.Rows[j]["Item_Id"].ToString(), (float.Parse(ox.Rows[j]["Quantity"].ToString()) * float.Parse(ot.Rows[i]["Quantity"].ToString())), ox.Rows[j]["Type"].ToString(), drpStore.SelectedItem.Value, 1);
                }
            }
            string sqlTran = @"select  (select Code from Items where BarCode=i.BarCode) as code,o.Quantity as Num,Price,Type,Date,Store_Id from [dbo].[tempPrucheDetails] o left join Items i on o.Item_Id=i.Code where [PrucheCode]='" + TxtCode.Text + "' and [OverDis]=1";
            DataTable dt = dataobj.Selectdatatable(sqlTran);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string check = @"select ItemPruchType,ItemSaleType,Quantity from Items where Code='" + dt.Rows[i]["code"].ToString() + "'";
                DataTable dts = dataobj.Selectdatatable(check);
                if (dts.Rows[0]["ItemPruchType"].ToString() == dt.Rows[i]["Type"].ToString())
                {
                    string selSame = @"select top(1)Quantity as Quantity from TransactionOfStore where Store_Id='" + dt.Rows[i]["Store_Id"].ToString() + "' and ItemId='" + dt.Rows[i]["code"].ToString() + "' order by DateOfItem";
                    DataTable ds = dataobj.Selectdatatable(selSame);
                    float x = float.Parse(dt.Rows[i]["Num"].ToString()) * (float.Parse(dts.Rows[0]["Quantity"].ToString()));
                    int y = 1;
                    string newval = ds.Rows[0]["Quantity"].ToString();
                    for (int j = 0; j < y; j++)
                    {

                        if (x > (float.Parse(newval)))
                        {
                            x = x - float.Parse(ds.Rows[0]["Quantity"].ToString());
                            string selSameto = @"select top(1)* from TransactionOfStore where Store_Id='" + dt.Rows[i]["Store_Id"].ToString() + "' and ItemId='" + dt.Rows[i]["code"].ToString() + "' order by DateOfItem";
                            DataTable dm = dataobj.Selectdatatable(selSameto);
                            string up = @"UPDATE [dbo].[TransactionOfStore] SET [Quantity]=@1 where [Code] = @2 and [ItemId] = @3 and [Quantity] = @4 and[TypeOfStore] = @5 and [TypeOfOperation] = @6 and [DateOfItem] = @7 and [Store_Id] = @8";
                            dataobj.Execute(up, -100, dm.Rows[0]["Code"].ToString(), dm.Rows[0]["ItemId"].ToString(), dm.Rows[0]["Quantity"].ToString(), dm.Rows[0]["TypeOfStore"].ToString(), dm.Rows[0]["TypeOfOperation"].ToString(), dm.Rows[0]["DateOfItem"].ToString(), dm.Rows[0]["Store_Id"].ToString());
                            string del = @"delete from TransactionOfStore where Quantity=-100";
                            dataobj.Execute(del);
                            string selSame1 = @"select top(1)Quantity as Quantity from TransactionOfStore where Store_Id='" + dt.Rows[i]["Store_Id"].ToString() + "' and ItemId='" + dt.Rows[i]["code"].ToString() + "' order by DateOfItem";
                            DataTable ds1 = dataobj.Selectdatatable(selSame1);
                            newval = ds1.Rows[0]["Quantity"].ToString();
                            y++;
                        }
                        else if (x == (float.Parse(newval)))
                        {
                            string selSameto = @"select top(1)* from TransactionOfStore where Store_Id='" + dt.Rows[i]["Store_Id"].ToString() + "' and ItemId='" + dt.Rows[i]["code"].ToString() + "' order by DateOfItem";
                            DataTable dm = dataobj.Selectdatatable(selSameto);
                            string up = @"UPDATE [dbo].[TransactionOfStore] SET [Quantity]=@1 where [Code] = @2 and [ItemId] = @3 and [Quantity] = @4 and[TypeOfStore] = @5 and [TypeOfOperation] = @6 and [DateOfItem] = @7 and [Store_Id] = @8";
                            dataobj.Execute(up, -100, dm.Rows[0]["Code"].ToString(), dm.Rows[0]["ItemId"].ToString(), dm.Rows[0]["Quantity"].ToString(), dm.Rows[0]["TypeOfStore"].ToString(), dm.Rows[0]["TypeOfOperation"].ToString(), dm.Rows[0]["DateOfItem"].ToString(), dm.Rows[0]["Store_Id"].ToString());
                            string del = @"delete from TransactionOfStore where Quantity=-100";
                            dataobj.Execute(del);
                        }
                        else
                        {
                            string selSameto = @"select top(1)* from TransactionOfStore where Store_Id='" + dt.Rows[i]["Store_Id"].ToString() + "' and ItemId='" + dt.Rows[i]["code"].ToString() + "' order by DateOfItem";
                            DataTable dm = dataobj.Selectdatatable(selSameto);
                            string up = @"UPDATE [dbo].[TransactionOfStore] SET [Quantity]=@1 where [Code] = @2 and [ItemId] = @3 and [Quantity] = @4 and[TypeOfStore] = @5 and [TypeOfOperation] = @6 and [DateOfItem] = @7 and [Store_Id] = @8";
                            float upnum = (float.Parse(newval) - x);
                            dataobj.Execute(up, upnum, dm.Rows[0]["Code"].ToString(), dm.Rows[0]["ItemId"].ToString(), dm.Rows[0]["Quantity"].ToString(), dm.Rows[0]["TypeOfStore"].ToString(), dm.Rows[0]["TypeOfOperation"].ToString(), dm.Rows[0]["DateOfItem"].ToString(), dm.Rows[0]["Store_Id"].ToString());
                        }
                    }
                }
                else if (dts.Rows[0]["ItemSaleType"].ToString() == dt.Rows[i]["Type"].ToString())
                {
                    string selSame = @"select top(1)Quantity as Quantity from TransactionOfStore where Store_Id='" + dt.Rows[i]["Store_Id"].ToString() + "' and ItemId='" + dt.Rows[i]["code"].ToString() + "' order by DateOfItem";
                    DataTable ds = dataobj.Selectdatatable(selSame);
                    float x = float.Parse(dt.Rows[i]["Num"].ToString());
                    int y = 1;
                    string newval = ds.Rows[0]["Quantity"].ToString();
                    for (int j = 0; j < y; j++)
                    {
                        if (x > (float.Parse(newval)))
                        {
                            x = x - float.Parse(ds.Rows[0]["Quantity"].ToString());
                            string selSameto = @"select top(1)* from TransactionOfStore where Store_Id='" + dt.Rows[i]["Store_Id"].ToString() + "' and ItemId='" + dt.Rows[i]["code"].ToString() + "' order by DateOfItem";
                            DataTable dm = dataobj.Selectdatatable(selSameto);
                            string up = @"UPDATE [dbo].[TransactionOfStore] SET [Quantity]=@1 where [Code] = @2 and [ItemId] = @3 and [Quantity] = @4 and[TypeOfStore] = @5 and [TypeOfOperation] = @6 and [DateOfItem] = @7 and [Store_Id] = @8";
                            dataobj.Execute(up, -100, dm.Rows[0]["Code"].ToString(), dm.Rows[0]["ItemId"].ToString(), dm.Rows[0]["Quantity"].ToString(), dm.Rows[0]["TypeOfStore"].ToString(), dm.Rows[0]["TypeOfOperation"].ToString(), dm.Rows[0]["DateOfItem"].ToString(), dm.Rows[0]["Store_Id"].ToString());
                            string del = @"delete from TransactionOfStore where Quantity=-100";
                            string selSame1 = @"select top(1)Quantity as Quantity from TransactionOfStore where Store_Id='" + dt.Rows[i]["Store_Id"].ToString() + "' and ItemId='" + dt.Rows[i]["code"].ToString() + "' order by DateOfItem";
                            dataobj.Execute(del);
                            DataTable ds1 = dataobj.Selectdatatable(selSame1);
                            newval = ds1.Rows[0]["Quantity"].ToString();
                            y++;
                        }
                        else if (x == (float.Parse(newval)))
                        {
                            string selSameto = @"select top(1)* from TransactionOfStore where Store_Id='" + dt.Rows[i]["Store_Id"].ToString() + "' and ItemId='" + dt.Rows[i]["code"].ToString() + "' order by DateOfItem";
                            DataTable dm = dataobj.Selectdatatable(selSameto);
                            string up = @"UPDATE [dbo].[TransactionOfStore] SET [Quantity]=@1 where [Code] = @2 and [ItemId] = @3 and [Quantity] = @4 and[TypeOfStore] = @5 and [TypeOfOperation] = @6 and [DateOfItem] = @7 and [Store_Id] = @8";
                            dataobj.Execute(up, -100, dm.Rows[0]["Code"].ToString(), dm.Rows[0]["ItemId"].ToString(), dm.Rows[0]["Quantity"].ToString(), dm.Rows[0]["TypeOfStore"].ToString(), dm.Rows[0]["TypeOfOperation"].ToString(), dm.Rows[0]["DateOfItem"].ToString(), dm.Rows[0]["Store_Id"].ToString());
                            string del = @"delete from TransactionOfStore where Quantity=-100";
                            dataobj.Execute(del);
                        }
                        else
                        {
                            string selSameto = @"select top(1)* from TransactionOfStore where Store_Id='" + dt.Rows[i]["Store_Id"].ToString() + "' and ItemId='" + dt.Rows[i]["code"].ToString() + "' order by DateOfItem";
                            DataTable dm = dataobj.Selectdatatable(selSameto);
                            string up = @"UPDATE [dbo].[TransactionOfStore] SET [Quantity]=@1 where [Code] = @2 and [ItemId] = @3 and [Quantity] = @4 and[TypeOfStore] = @5 and [TypeOfOperation] = @6 and [DateOfItem] = @7 and [Store_Id] = @8";
                            float upnum = (float.Parse(newval) - x);
                            dataobj.Execute(up, upnum, dm.Rows[0]["Code"].ToString(), dm.Rows[0]["ItemId"].ToString(), dm.Rows[0]["Quantity"].ToString(), dm.Rows[0]["TypeOfStore"].ToString(), dm.Rows[0]["TypeOfOperation"].ToString(), dm.Rows[0]["DateOfItem"].ToString(), dm.Rows[0]["Store_Id"].ToString());
                        }
                    }
                }
            }
            string sqlTrea = @"INSERT INTO [dbo].[TransactionOfTreasure] ([Code],[Amount],[TypeOfOperation],[DateOfOperation],[User_Id]) VALUES(@1,@2,@3,@4,@5)";
            float Amount = float.Parse(txtPay.Text);
            dataobj.Execute(sqlTrea, TxtCode.Text, Amount, "مبيعات عروض", DateTime.Now.ToString(), Session["Id"]);

            string sqldel = @"delete from [dbo].[tempPrucheDetails] where [PrucheCode]=@1 and [OverDis]=1";
            dataobj.Execute(sqldel, TxtCode.Text);

            string sql = @"insert into [dbo].[OverSaleCustomer] select * from [dbo].[tempOverSaleCustomer] where PrucheCode=@1";
            dataobj.Execute(sql, TxtCode.Text);
            string sqldel1 = @"delete from [dbo].[tempOverSaleCustomer] where [PrucheCode]=@1";
            dataobj.Execute(sqldel1, TxtCode.Text);
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
                string sqlover = @"select * from tempOverSaleCustomer where PrucheCode='" + TxtCode.Text + "'";
                DataTable dto = dataobj.Selectdatatable(sqlover);
                if (dto.Rows.Count > 0)
                {
                    if (checkOver() == true)
                    {
                        if (drpSupply.SelectedIndex == 0)
                        {
                            if (txtPay.Text == LabTotal.Text)
                            {
                                string sql = @"INSERT INTO [dbo].[SalesOver] ([Code],[Customer_Id],[Store_Id],[DateOfPruches],[User_Id],[AddMoney],[Discount],[Pay],[Total],[Notes]) VALUES(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10)";
                                dataobj.Execute(sql, TxtCode.Text, drpSupply.Text, drpStore.SelectedItem.Value, txtDate.Text, Session["Id"], float.Parse(txtAdd.Text), float.Parse(txtDiscount.Text), float.Parse(txtPay.Text), float.Parse(LabTotal.Text), txtNotes.Text);
                                compeleteOver();

                                txtNotes.Text = string.Empty;
                                dataobj.Alert("Success", this);
                            }
                            else
                            {
                                dataobj.Alert("يجب دفع الفاتورة كاملة", this);
                            }
                        }
                        else
                        {
                            string sql = @"INSERT INTO [dbo].[SalesOver] ([Code],[Customer_Id],[Store_Id],[DateOfPruches],[User_Id],[AddMoney],[Discount],[Pay],[Total],[Notes]) VALUES(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10)";
                            dataobj.Execute(sql, TxtCode.Text, drpSupply.SelectedItem.Value, drpStore.SelectedItem.Value, txtDate.Text, Session["Id"], float.Parse(txtAdd.Text), float.Parse(txtDiscount.Text), float.Parse(txtPay.Text), float.Parse(LabTotal.Text), txtNotes.Text);

                            string UpSupSql = @"UPDATE [dbo].[CustomerAndSupplier] SET [InitialBalance] += @1 WHERE [Code]=@2";
                            float dept = float.Parse(txtDebt.Text);
                            dataobj.Execute(UpSupSql, dept, drpSupply.SelectedItem.Value);
                            drpSupply.Text = "تعامل نقدي";
                            compeleteOver();

                            dataobj.Alert("Success", this);
                        }
                    }
                    else
                    {
                        dataobj.Alert("توجد اصناف  في العروض غير متاحة", this);
                    }
                }
                else
                {
                    dataobj.Alert("اختار عرض واحد علي الاقل", this);
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
    private bool checkOver()
    {
        try
        {
            bool vaild = false;
            string sqlover = @"select * from tempOverSaleCustomer where PrucheCode='" + TxtCode.Text + "'";
            DataTable dtox = dataobj.Selectdatatable(sqlover);
            for (int m = 0; m < dtox.Rows.Count; m++)
            {
                string itemOver = @"select * from OverSaleDetails where OverCode='" + dtox.Rows[m]["Over_Id"].ToString() + "'";
                DataTable dtx = dataobj.Selectdatatable(itemOver);
                for (int n = 0; n < dtx.Rows.Count; n++)
                {
                    string checksum = @"select * from [dbo].[tempPrucheDetails] where [PrucheCode]='" + TxtCode.Text + "' and [Item_Id]='" + dtx.Rows[n]["Item_Id"].ToString() + "'";
                    DataTable dte = dataobj.Selectdatatable(checksum);
                    if (dte.Rows.Count > 0)
                    {
                        float An = 0;
                        string checksum1 = @"select * from [dbo].[tempPrucheDetails] where [PrucheCode]='" + TxtCode.Text + "' and Item_Id='" + dtx.Rows[n]["Item_Id"].ToString() + "'";
                        DataTable dte1 = dataobj.Selectdatatable(checksum1);
                        for (int j = 0; j < dte1.Rows.Count; j++)
                        {
                            string sqlch = @"select sum(Quantity) as sum from [dbo].[TransactionOfStore] where [Store_Id]='" + drpStore.SelectedItem.Value + "' and  ItemId='" + dte1.Rows[j]["Item_Id"].ToString() + "'";
                            DataTable dst = dataobj.Selectdatatable(sqlch);
                            string sal = @"SELECT ItemPruchType,Quantity FROM [dbo].[Items] where [Code]='" + dte1.Rows[j]["Item_Id"].ToString() + "'";
                            DataTable dx = dataobj.Selectdatatable(sal);
                            if (dx.Rows[0]["ItemPruchType"].ToString() == dte1.Rows[j]["Type"].ToString())
                            {
                                An = An + (float.Parse(dte1.Rows[j]["Quantity"].ToString()) * float.Parse(dx.Rows[0]["Quantity"].ToString()));
                            }
                            else
                            {
                                An = An + (float.Parse(dte1.Rows[j]["Quantity"].ToString()));
                            }
                            if (dx.Rows[0]["ItemPruchType"].ToString() == dtx.Rows[n]["Type"].ToString())
                            {
                                An = An + (float.Parse(dtx.Rows[n]["Quantity"].ToString()) * float.Parse(dx.Rows[0]["Quantity"].ToString()) * float.Parse(dtox.Rows[m]["Quantity"].ToString()));
                            }
                            else
                            {
                                An = An + (float.Parse(dtx.Rows[n]["Quantity"].ToString())) * float.Parse(dtox.Rows[m]["Quantity"].ToString());
                            }
                            if (An <= (float.Parse(dst.Rows[0]["sum"].ToString())))
                            {
                                vaild = true;
                            }
                            else
                            {
                                vaild = false;
                                goto x;
                            }
                        }
                    }
                    else
                    {
                        float An = 0;
                        string sqlch = @"select sum(Quantity) as sum from [dbo].[TransactionOfStore] where [Store_Id]='" + drpStore.SelectedItem.Value + "' and  ItemId='" + dtx.Rows[n]["Item_Id"].ToString() + "'";
                        DataTable dst = dataobj.Selectdatatable(sqlch);
                        string sal = @"SELECT ItemPruchType,Quantity FROM [dbo].[Items] where [Code]='" + dtx.Rows[n]["Item_Id"].ToString() + "'";
                        DataTable dx = dataobj.Selectdatatable(sal);
                        if (dx.Rows[0]["ItemPruchType"].ToString() == dtx.Rows[n]["Type"].ToString())
                        {
                            An = An + (float.Parse(dtx.Rows[n]["Quantity"].ToString()) * float.Parse(dx.Rows[0]["Quantity"].ToString()) * float.Parse(dtox.Rows[m]["Quantity"].ToString()));
                        }
                        else
                        {
                            An = An + (float.Parse(dtx.Rows[n]["Quantity"].ToString()) * float.Parse(dtox.Rows[m]["Quantity"].ToString()));
                        }
                        if (An <= (float.Parse(dst.Rows[0]["sum"].ToString())))
                        {
                            vaild = true;
                        }
                        else
                        {
                            vaild = false;
                            goto x;
                        }
                    }
                }
            }
        x:
            return vaild;
        }
        catch (Exception)
        {
            return false;
        }
    }
    protected void DropOverName_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (DropOverName.SelectedIndex > 0)
            {
                string sql = @"select Amount from OverSales where Code='" + DropOverName.SelectedItem.Value + "'";
                DataTable dt = dataobj.Selectdatatable(sql);
                txtpriceover.Text = dt.Rows[0]["Amount"].ToString();
                txtQuantityOver.Text = "1";
                txtTotalOver.Text = dt.Rows[0]["Amount"].ToString();
                ViewState.Add("priceOver", float.Parse(txtpriceover.Text));
                ViewState.Add("quantityOver", float.Parse(txtQuantityOver.Text));
            }
            else
            {
                txtpriceover.Text = txtQuantityOver.Text = txtTotalOver.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }

    }
    protected void txtQuantityOver_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (DropOverName.SelectedIndex > 0)
            {
                ViewState["priceOver"] = float.Parse(txtpriceover.Text);
                ViewState["quantityOver"] = float.Parse(txtQuantityOver.Text);
                txtTotalOver.Text = (float.Parse(txtpriceover.Text) * float.Parse(txtQuantityOver.Text)).ToString();
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void txtpriceover_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (DropOverName.SelectedIndex > 0)
            {
                ViewState["priceOver"] = float.Parse(txtpriceover.Text);
                ViewState["quantityOver"] = float.Parse(txtQuantityOver.Text);
                txtTotalOver.Text = (float.Parse(txtpriceover.Text) * float.Parse(txtQuantityOver.Text)).ToString();
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void LinkAddOver_Click(object sender, EventArgs e)
    {
        try
        {
            if (DropOverName.SelectedIndex > 0)
            {
                ViewState["priceOver"] = float.Parse(txtpriceover.Text);
                ViewState["quantityOver"] = float.Parse(txtQuantityOver.Text);
                float x = float.Parse(ViewState["priceOver"].ToString());
                float y = float.Parse(ViewState["quantityOver"].ToString());
                string sqlcheck = @"select * from [dbo].[tempOverSaleCustomer] where [PrucheCode]='" + TxtCode.Text + "' and [Over_Id] ='" + DropOverName.SelectedItem.Value + "' and [Quantity]=" + y + " and [Price]=" + x + "";
                DataTable dt = dataobj.Selectdatatable(sqlcheck);
                if (dt.Rows.Count == 0)
                {
                    string sql = @"INSERT INTO [dbo].[tempOverSaleCustomer] ([PrucheCode],[Over_Id],[Quantity],[Price]) VALUES(@1,@2,@3,@4)";
                    dataobj.Execute(sql, TxtCode.Text, DropOverName.SelectedItem.Value, y, x);
                    sumTotal();
                    txtQuantityOver.Text = txtpriceover.Text = txtTotalOver.Text = string.Empty;
                    DropOverName.SelectedValue = "اختار العرض";
                }
                else
                {
                    dataobj.Alert("هذه البيانات دخلت من قبل", this);
                }
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    private void LinksOver()
    {
        try
        {
            int x = int.Parse(TextBox2.Text);
            string sql = @"select o.NameOfOver as Name,Quantity,Price from tempOverSaleCustomer t left join OverSales o on t.Over_Id=o.Code where t.PrucheCode='" + TxtCode.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            string sqldel = @"delete from tempOverSaleCustomer where [PrucheCode]=@1 and [Over_Id]=(select Code from OverSales where NameOfOver=@2) and [Quantity]=@3 and [Price]=@4";
            dataobj.Execute(sqldel, TxtCode.Text, dt.Rows[x]["Name"].ToString(), float.Parse(dt.Rows[x]["Quantity"].ToString()), dt.Rows[x]["Price"].ToString());
            sumTotal();
        }
        catch (Exception ex)
        {
            dataobj.Execute(ex.Message, this);
        }
    }
    protected void BtnDelConf1_Click(object sender, EventArgs e)
    {
        LinksOver();
    }
    protected void BtnEditConf1_Click(object sender, EventArgs e)
    {
        try
        {
            int x = int.Parse(TextBox2.Text);
            string sql = @"select o.Code as code,o.NameOfOver as Name,Quantity,Price from tempOverSaleCustomer t left join OverSales o on t.Over_Id=o.Code where t.PrucheCode='" + TxtCode.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            DropOverName.SelectedValue = dt.Rows[x]["code"].ToString();
            txtQuantityOver.Text = dt.Rows[x]["Quantity"].ToString();
            txtpriceover.Text = dt.Rows[x]["Price"].ToString();
            txtTotalOver.Text = (float.Parse(txtpriceover.Text) * float.Parse(txtQuantityOver.Text)).ToString();
            LinksOver();
        }
        catch (Exception ex)
        {
            dataobj.Execute(ex.Message, this);
        }
    }
}