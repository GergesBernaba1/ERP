﻿using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Add_Sales : System.Web.UI.Page
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
                    string sql2 = @"select Code,Name from ShowPrices";
                    drpShowPrice.DataSource = dataobj.Selectdatatable(sql2);
                    drpShowPrice.DataValueField = "Code";
                    drpShowPrice.DataTextField = "Name";
                    drpShowPrice.DataBind();
                    drpShowPrice.Items.Insert(0, "اختار العرض ");
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
            drpType.Items.Insert(0, dt.Rows[0]["ItemSaleType"].ToString());
            drpType.Items.Insert(1, dt.Rows[0]["ItemPruchType"].ToString());
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
            string sql = @"select Code,SalePrice from Items where BarCode='" + txtBarCode.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            drpName.SelectedValue = dt.Rows[0]["Code"].ToString();
            txtQuantity.Text = "1";
            txtprice.Text = dt.Rows[0]["SalePrice"].ToString();
            txtTotal.Text = ((float.Parse(dt.Rows[0]["SalePrice"].ToString())) * (float.Parse(txtQuantity.Text))).ToString();
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
                string sql = @"select BarCode,SalePrice from Items where Code='" + drpName.SelectedItem.Value + "'";
                DataTable dt = dataobj.Selectdatatable(sql);
                txtBarCode.Text = dt.Rows[0]["BarCode"].ToString();
                txtQuantity.Text = "1";
                txtprice.Text = dt.Rows[0]["SalePrice"].ToString();
                txtTotal.Text = ((float.Parse(dt.Rows[0]["SalePrice"].ToString())) * (float.Parse(txtQuantity.Text))).ToString();
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
    private bool sumprice()
    {
        try
        {
            if (drpName.SelectedIndex > 0)
            {
                string sql = @"select pruchPrice,Quantity from Items where ItemPruchType='" + drpType.Text + "' and BarCode='" + txtBarCode.Text + "'";
                DataTable dt = dataobj.Selectdatatable(sql);
                if (dt.Rows.Count == 1)
                {
                    string sqlch = @"select sum(Quantity) as sum from [dbo].[TransactionOfStore] where [Store_Id]='" + drpStore.SelectedItem.Value + "' and ItemId=(select Code from Items where BarCode='" + txtBarCode.Text + "')";
                    DataTable ds = dataobj.Selectdatatable(sqlch);
                    if (float.Parse(ds.Rows[0]["sum"].ToString()) / float.Parse(dt.Rows[0]["Quantity"].ToString()) >= float.Parse(txtQuantity.Text))
                    {
                        float x = float.Parse(dt.Rows[0]["pruchPrice"].ToString());
                        txtprice.Text = x.ToString();
                        txtTotal.Text = ((float.Parse(x.ToString())) * (float.Parse(txtQuantity.Text))).ToString();
                        return true;
                    }
                    else
                    {
                        dataobj.Alert("الكمية غير متاحة", this);
                        return false;
                    }
                }
                else
                {
                    string check = @"select ItemSaleType,Quantity,SalePrice from Items where Code='" + drpName.SelectedItem.Value + "'";
                    DataTable dts = dataobj.Selectdatatable(check);
                    if (dts.Rows[0]["ItemSaleType"].ToString() == drpType.Text)
                    {
                        string sqlch = @"select sum(Quantity) as sum from [dbo].[TransactionOfStore] where [Store_Id]='" + drpStore.SelectedItem.Value + "' and  ItemId=(select Code from Items where BarCode='" + txtBarCode.Text + "')";
                        DataTable ds = dataobj.Selectdatatable(sqlch);
                        if (float.Parse(ds.Rows[0]["sum"].ToString()) >= float.Parse(txtQuantity.Text))
                        {
                            float x = float.Parse(dts.Rows[0]["SalePrice"].ToString());
                            txtprice.Text = x.ToString();
                            txtTotal.Text = ((float.Parse(x.ToString())) * (float.Parse(txtQuantity.Text))).ToString();
                            return true;
                        }
                        else
                        {
                            dataobj.Alert("الكمية غير متاحة", this);
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
            return false;
        }
    }
    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        sumprice();
    }
    protected void LinkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (sumprice() == true)
            {
                if (drpName.SelectedIndex > 0)
                {
                    string check = @"select * from [dbo].[tempPrucheDetails] where [PrucheCode]='" + TxtCode.Text + "' and [Store_Id]='" + drpStore.SelectedItem.Value + "' and [Item_Id]='" + drpName.SelectedItem.Value + "' and [Price]=" + float.Parse(txtprice.Text) + " and [Quantity]=" + float.Parse(txtQuantity.Text) + " and [Type]='" + drpType.Text + "'";
                    DataTable dt = dataobj.Selectdatatable(check);

                    string checksum = @"select Quantity,Type,Item_Id from [dbo].[tempPrucheDetails] where [PrucheCode]='" + TxtCode.Text + "' and [Item_Id]='" + drpName.SelectedItem.Value + "'";
                    DataTable dte = dataobj.Selectdatatable(checksum);
                    if (dte.Rows.Count > 0)
                    {
                        string sqlch = @"select sum(Quantity) as sum from [dbo].[TransactionOfStore] where [Store_Id]='" + drpStore.SelectedItem.Value + "' and  ItemId=(select Code from Items where BarCode='" + txtBarCode.Text + "')";
                        DataTable dst = dataobj.Selectdatatable(sqlch);
                        float An = 0;
                        for (int i = 0; i < dte.Rows.Count; i++)
                        {
                            string sal = @"SELECT ItemPruchType,Quantity FROM [dbo].[Items] where [Code]='" + dte.Rows[i]["Item_Id"].ToString() + "'";
                            DataTable dx = dataobj.Selectdatatable(sal);
                            if (dx.Rows[0]["ItemPruchType"].ToString() == dte.Rows[i]["Type"].ToString())
                            {
                                An = An + (float.Parse(dte.Rows[i]["Quantity"].ToString()) * float.Parse(dx.Rows[0]["Quantity"].ToString()));
                            }
                            else
                            {
                                An = An + (float.Parse(dte.Rows[i]["Quantity"].ToString()));
                            }
                        }
                        string sals = @"SELECT ItemPruchType,Quantity FROM [dbo].[Items] where [Code]='" + drpName.SelectedItem.Value + "'";
                        DataTable dxm = dataobj.Selectdatatable(sals);
                        if (dxm.Rows[0]["ItemPruchType"].ToString() == dte.Rows[0]["Type"].ToString())
                        {
                            An = An + (float.Parse(txtQuantity.Text) * float.Parse(dxm.Rows[0]["Quantity"].ToString()));
                        }
                        else
                        {
                            An = An + float.Parse(txtQuantity.Text);
                        }
                        if (An <= (float.Parse(dst.Rows[0]["sum"].ToString())))
                        {
                            if (dt.Rows.Count == 0)
                            {
                                ViewState["price"] = float.Parse(txtprice.Text);
                                ViewState["quantity"] = float.Parse(txtQuantity.Text);
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
                            dataobj.Alert("الكمية غير متاحة", this);
                        }
                    }
                    else
                    {
                        if (dt.Rows.Count == 0)
                        {
                            ViewState["price"] = float.Parse(txtprice.Text);
                            ViewState["quantity"] = float.Parse(txtQuantity.Text);
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
                }
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    private void Links()
    {
        try
        {
            int x = int.Parse(TextBox1.Text);
            string sql = @"select i.BarCode as BarCode,i.ItemName as Name,o.Quantity as Num,Price,Type from [dbo].[tempPrucheDetails] o left join Items i on o.Item_Id=i.Code where [PrucheCode]='" + TxtCode.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            string sqldel = @"delete from tempPrucheDetails where [PrucheCode]=@1 and [Item_Id]=(select Code from Items where ItemName=@2) and [Quantity]=@3 and [Type]=@4 and [Price]=@5";
            dataobj.Execute(sqldel, TxtCode.Text, dt.Rows[x]["Name"].ToString(), float.Parse(dt.Rows[x]["Num"].ToString()), dt.Rows[x]["Type"].ToString(), dt.Rows[x]["Price"].ToString());
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
            string sql = @"select i.BarCode as BarCode,i.ItemName as Name,o.Quantity as Num,Price,Type from [dbo].[tempPrucheDetails] o left join Items i on o.Item_Id=i.Code where [PrucheCode]='" + TxtCode.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            txtBarCode.Text = dt.Rows[x]["BarCode"].ToString();

            string sqln = @"select Code from Items where BarCode='" + txtBarCode.Text + "'";
            DataTable dx = dataobj.Selectdatatable(sqln);
            drpName.SelectedValue = dx.Rows[0]["Code"].ToString();
            txtQuantity.Text = dt.Rows[x]["Num"].ToString();
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
    private void sumTotal()
    {
        float x = 0;
        float y = 0;
        string check = @"select *  from [dbo].[tempPrucheDetails] where PrucheCode='" + TxtCode.Text + "'";
        DataTable dt = dataobj.Selectdatatable(check);
        if (dt.Rows.Count > 0)
        {
            string sqlsum = @"select SUM(Price*Quantity) as sumprice from [dbo].[tempPrucheDetails] where PrucheCode='" + TxtCode.Text + "' and [Store_Id]='" + drpStore.SelectedItem.Value + "'";
            DataTable ds = dataobj.Selectdatatable(sqlsum);
            x = float.Parse(ds.Rows[0]["sumprice"].ToString());
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
    private bool check()
    {
        try
        {
            bool vaild = false;
            string checksum = @"select * from [dbo].[tempPrucheDetails] where [PrucheCode]='" + TxtCode.Text + "'";
            DataTable dte = dataobj.Selectdatatable(checksum);
            if (dte.Rows.Count > 0)
            {

                for (int i = 0; i < dte.Rows.Count; i++)
                {
                    float An = 0;
                    string checksum1 = @"select * from [dbo].[tempPrucheDetails] where [PrucheCode]='" + TxtCode.Text + "' and Item_Id='" + dte.Rows[i]["Item_Id"].ToString() + "'";
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
            x:
                return vaild;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
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
            dataobj.Execute(sqlTrea, TxtCode.Text, Amount, "مبيعات", DateTime.Now.ToString(), Session["Id"] );

            string sql = @"insert into [dbo].[SalesDetails] select [PrucheCode],[Item_Id],[Quantity],[Price],[Type],[Date],[Store_Id] from [dbo].[tempPrucheDetails] where PrucheCode=@1";
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
            if (check() == true)
            {
                if (drpSupply.SelectedIndex == 0)
                {
                    if (txtPay.Text == LabTotal.Text)
                    {
                        string sql = @"INSERT INTO [dbo].[Sales] ([Code],[Customer_Id],[Store_Id],[DateOfPruches],[User_Id],[AddMoney],[Discount],[Pay],[Total],[Notes]) VALUES(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10)";
                        dataobj.Execute(sql, TxtCode.Text, drpSupply.Text, drpStore.SelectedItem.Value, txtDate.Text, Session["Id"], float.Parse(txtAdd.Text), float.Parse(txtDiscount.Text), float.Parse(txtPay.Text), float.Parse(LabTotal.Text), txtNotes.Text);
                        CompeleteData();
                        txtNotes.Text = string.Empty;
                        drpShowPrice.Text = "اختار العرض ";
                        dataobj.Alert("Success", this);
                    }
                    else
                    {
                        dataobj.Alert("يجب دفع الفاتورة كاملة", this);
                    }
                }
                else
                {
                    string sql = @"INSERT INTO [dbo].[Sales] ([Code],[Customer_Id],[Store_Id],[DateOfPruches],[User_Id],[AddMoney],[Discount],[Pay],[Total],[Notes]) VALUES(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10)";
                    dataobj.Execute(sql, TxtCode.Text, drpSupply.SelectedItem.Value, drpStore.SelectedItem.Value, txtDate.Text, Session["Id"], float.Parse(txtAdd.Text), float.Parse(txtDiscount.Text), float.Parse(txtPay.Text), float.Parse(LabTotal.Text), txtNotes.Text);

                    string UpSupSql = @"UPDATE [dbo].[CustomerAndSupplier] SET [InitialBalance] += @1 WHERE [Code]=@2";
                    float dept = float.Parse(txtDebt.Text);
                    dataobj.Execute(UpSupSql, dept, drpSupply.SelectedItem.Value);
                    drpSupply.Text = "تعامل نقدي";
                    drpShowPrice.Text = "اختار العرض ";
                    CompeleteData();
                    dataobj.Alert("Success", this);
                }
            }
            else
            {
                dataobj.Alert("توجد اصناف غير متاحة", this);
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
    protected void drpShowPrice_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpShowPrice.SelectedIndex > 0)
            {
                string sql = @"select * from ShowPrices where Code='" + drpShowPrice.SelectedItem.Value + "'";
                DataTable dt = dataobj.Selectdatatable(sql);
                generateguid();
                drpStore.SelectedValue = dt.Rows[0]["Store_Id"].ToString();
                txtDate.Text = dt.Rows[0]["DateOfOver"].ToString();
                drpSupply.Text = "تعامل نقدي";
                txtAdd.Text = dt.Rows[0]["AddMoney"].ToString();
                txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
                txtPay.Text = dt.Rows[0]["Pay"].ToString();
                LabTotal.Text = dt.Rows[0]["Total"].ToString();
                txtDebt.Text = (float.Parse(LabTotal.Text) - float.Parse(txtPay.Text)).ToString();
                txtNotes.Text = dt.Rows[0]["Notes"].ToString();
                string checksum = @"select * from [dbo].[ShowPriceDetails] where [PrucheCode]='" + dt.Rows[0]["Code"].ToString() + "'";
                DataTable dte = dataobj.Selectdatatable(checksum);
                for (int i = 0; i < dte.Rows.Count; i++)
                {
                    string sqls = @"INSERT INTO [dbo].[tempPrucheDetails] ([PrucheCode],[Item_Id],[Quantity],[Type],[Price],[Store_Id]) VALUES(@1,@2,@3,@4,@5,@6)";
                    dataobj.Execute(sqls, TxtCode.Text, dte.Rows[i]["Item_Id"].ToString(), dte.Rows[i]["Quantity"].ToString(), dte.Rows[i]["Type"].ToString(), dte.Rows[i]["Price"].ToString(), dte.Rows[i]["Store_Id"].ToString());
                }
            }
            else
            {
                generateguid();
                txtDate.Text = DateTime.Now.ToString();
                txtPay.Text = LabTotal.Text = txtDebt.Text = txtDiscount.Text = txtAdd.Text = "0";
                drpStore.Text = "اختار المخزن";
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
                string sql = @"ReportSalesDetails '" + x + "'";
                System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
                ReportDocument reportdocument = new ReportDocument();
                reportdocument.Load(Server.MapPath("Reports/ReportDetailsSales.rpt"));
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
    protected void LinkNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddSaleOver.aspx");
    }
}