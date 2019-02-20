using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Add_MoveItem : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=19 and IsVaild=1";
                DataTable dt = dataobj.Selectdatatable(sql1);
                if (dt.Rows.Count == 1)
                {
                    generateguid();
                    addValidationGroup();
                    txtDate.Text = DateTime.Now.ToString();
                    string sql = @"select Code,StoreName from Store";
                    drpStore.DataSource = dataobj.Selectdatatable(sql);
                    drpStore.DataValueField = "Code";
                    drpStore.DataTextField = "StoreName";
                    drpStore.DataBind();
                    drpStore.Items.Insert(0, "اختار المخزن المحول منه");
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
                string sql = @"select Code,StoreName from Store where Code!='" + drpStore.SelectedItem.Value + "'";
                drpStoreto.DataSource = dataobj.Selectdatatable(sql);
                drpStoreto.DataValueField = "Code";
                drpStoreto.DataTextField = "StoreName";
                drpStoreto.DataBind();
                drpStoreto.Items.Insert(0, "اختار المخزن المحول اليه");
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void drpStoreto_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpStore.SelectedIndex > 0)
            {
                string itemsql = @"select ii.Code as Code,ItemName as Name from ItemStore i left join Items ii on i.ItemCode=ii.Code left join Store s on i.StoreCode=s.Code where s.Code='" + drpStore.SelectedItem.Value + "' intersect select ii.Code as Code,ItemName as Name from ItemStore i left join Items ii on i.ItemCode=ii.Code left join Store s on i.StoreCode=s.Code where s.Code='" + drpStoreto.SelectedItem.Value + "'";
                drpName.DataSource = dataobj.Selectdatatable(itemsql);
                drpName.DataValueField = "Code";
                drpName.DataTextField = "Name";
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
            string sql = @"select ItemSaleType,ItemPruchType from Items where Code='" + drpName.SelectedItem.Value + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            drpType.Items.Insert(0, dt.Rows[0]["ItemPruchType"].ToString());
            drpType.Items.Insert(1, dt.Rows[0]["ItemSaleType"].ToString());
            txtQuantity.Text = "1";
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
            string sql = @"select Code from Items where BarCode='" + txtBarCode.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            drpName.SelectedValue = dt.Rows[0]["Code"].ToString();
            DataBindData();
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
                string sql = @"select BarCode from Items where Code='" + drpName.SelectedItem.Value + "'";
                DataTable dt = dataobj.Selectdatatable(sql);
                txtBarCode.Text = dt.Rows[0]["BarCode"].ToString();
                DataBindData();
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    private bool vaild()
    {
        try
        {
            if (drpName.SelectedIndex > 0)
            {
                string checksum = @"select Quantity,Type,ItemCode from [dbo].[tempMoveItemStores] where [MoveCode]='" + TxtCode.Text + "' and [ItemCode]='" + drpName.SelectedItem.Value + "'";
                DataTable dte = dataobj.Selectdatatable(checksum);
                if (dte.Rows.Count > 0)
                {
                    string sqlch = @"select sum(Quantity) as sum from [dbo].[TransactionOfStore] where [Store_Id]='" + drpStore.SelectedItem.Value + "' and  ItemId=(select Code from Items where BarCode='" + txtBarCode.Text + "')";
                    DataTable dst = dataobj.Selectdatatable(sqlch);
                    float An = 0;
                    for (int i = 0; i < dte.Rows.Count; i++)
                    {
                        string sal = @"SELECT ItemPruchType,Quantity FROM [dbo].[Items] where [Code]='" + dte.Rows[i]["ItemCode"].ToString() + "'";
                        DataTable dx = dataobj.Selectdatatable(sal);
                        if (dx.Rows[0]["ItemPruchType"].ToString() == dte.Rows[i]["Type"].ToString())
                        {
                            An = An + (float.Parse(dte.Rows[i]["Quantity"].ToString()) * float.Parse(dx.Rows[0]["Quantity"].ToString()));
                            An = An + (float.Parse(txtQuantity.Text) * float.Parse(dx.Rows[0]["Quantity"].ToString()));
                        }
                        else
                        {
                            An = An + (float.Parse(dte.Rows[i]["Quantity"].ToString()));
                            An = An + float.Parse(txtQuantity.Text);
                        }
                    }
                    if (An <= (float.Parse(dst.Rows[0]["sum"].ToString())))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    string sqlch = @"select sum(Quantity) as sum from [dbo].[TransactionOfStore] where [Store_Id]='" + drpStore.SelectedItem.Value + "' and  ItemId=(select Code from Items where BarCode='" + txtBarCode.Text + "')";
                    DataTable dst = dataobj.Selectdatatable(sqlch);
                    float An = 0;
                    string sal = @"SELECT ItemPruchType,Quantity FROM [dbo].[Items] where [Code]='" + drpName.SelectedItem.Value + "'";
                    DataTable dx = dataobj.Selectdatatable(sal);
                    if (dx.Rows[0]["ItemPruchType"].ToString() == drpType.Text)
                    {
                        An = An + (float.Parse(txtQuantity.Text) * float.Parse(dx.Rows[0]["Quantity"].ToString()));
                    }
                    else
                    {
                        An = An + float.Parse(txtQuantity.Text);
                    }
                    if (An <= (float.Parse(dst.Rows[0]["sum"].ToString())))
                    {
                        return true;
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
    protected void LinkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            addValidationGroup();
            if (drpName.SelectedIndex > 0)
            {
                if (vaild() == true)
                {
                    string check = @"select * from [dbo].[tempMoveItemStores] where [MoveCode]='" + TxtCode.Text + "' and [StoreTo]='" + drpStoreto.SelectedItem.Value + "' and [Store_Id]='" + drpStore.SelectedItem.Value + "' and [ItemCode]='" + drpName.SelectedItem.Value + "' and [Quantity]=" + float.Parse(txtQuantity.Text) + " and [Type]='" + drpType.Text + "'";
                    DataTable dt = dataobj.Selectdatatable(check);
                    if (dt.Rows.Count == 0)
                    {
                        string sql = @"INSERT INTO [dbo].[tempMoveItemStores] ([MoveCode],[ItemCode],[Quantity],[Type],[Store_Id],[StoreTo]) VALUES(@1,@2,@3,@4,@5,@6)";
                        dataobj.Execute(sql, TxtCode.Text, drpName.SelectedItem.Value, float.Parse(txtQuantity.Text), drpType.Text, drpStore.SelectedItem.Value, drpStoreto.SelectedItem.Value);
                        txtBarCode.Text = txtQuantity.Text = string.Empty;
                        drpName.SelectedValue = "اختار الصنف";
                        drpType.Items.Clear();
                    }
                    else
                    {
                        dataobj.Alert("تم ادخال هذه البيانات من قبل", this);
                    }
                }
                else
                {
                    dataobj.Alert("الكمية غير موجودة", this);
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
            string sql = @"select i.BarCode as BarCode,i.ItemName as Name,o.Quantity as Num,Type from tempMoveItemStores o left join Items i on o.ItemCode=i.Code where MoveCode='" + TxtCode.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            string sqldel = @"delete from tempMoveItemStores where [MoveCode]=@1 and [ItemCode]=(select Code from Items where ItemName=@2) and [Quantity]=@3 and [Type]=@4";
            dataobj.Execute(sqldel, TxtCode.Text, dt.Rows[x]["Name"].ToString(), float.Parse(dt.Rows[x]["Num"].ToString()), dt.Rows[x]["Type"].ToString());
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
            string sql = @"select i.BarCode as BarCode,i.ItemName as Name,o.Quantity as Num,Type from tempMoveItemStores o left join Items i on o.ItemCode=i.Code where MoveCode='" + TxtCode.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            txtBarCode.Text = dt.Rows[x]["BarCode"].ToString();

            string sqln = @"select Code from Items where BarCode='" + txtBarCode.Text + "'";
            DataTable dx = dataobj.Selectdatatable(sqln);
            drpName.SelectedValue = dx.Rows[0]["Code"].ToString();
            txtQuantity.Text = dt.Rows[x]["Num"].ToString();
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
    private void CompeleteData()
    {
        try
        {
            string sqlTran = @"select  (select Code from Items where BarCode=i.BarCode) as code,o.Quantity as Num,Type,Store_Id,StoreTo from [dbo].[tempMoveItemStores] o left join Items i on o.ItemCode=i.Code where [MoveCode]='" + TxtCode.Text + "'";
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
                            string up = @"UPDATE [dbo].[TransactionOfStore] SET [Store_Id]=@1 , [Code]=@2, [TypeOfOperation]=@3,[Quantity] = @4 where [Code] = @5 and [ItemId] = @6 and [Quantity] = @7 and[TypeOfStore] = @8 and [TypeOfOperation] = @9 and [DateOfItem] = @10 and  [Quantity]= @11 and [Store_Id]=@12";
                            dataobj.Execute(up, drpStoreto.SelectedItem.Value, TxtCode.Text, "تحويل المخازن", float.Parse(dt.Rows[i]["Num"].ToString()) * (float.Parse(dts.Rows[0]["Quantity"].ToString())), dm.Rows[0]["Code"].ToString(), dm.Rows[0]["ItemId"].ToString(), dm.Rows[0]["Quantity"].ToString(), dm.Rows[0]["TypeOfStore"].ToString(), dm.Rows[0]["TypeOfOperation"].ToString(), dm.Rows[0]["DateOfItem"].ToString(), dm.Rows[0]["Quantity"].ToString(), dm.Rows[0]["Store_Id"].ToString());
                        }
                        else
                        {
                            string selSameto = @"select top(1)* from TransactionOfStore where Store_Id='" + dt.Rows[i]["Store_Id"].ToString() + "' and ItemId='" + dt.Rows[i]["code"].ToString() + "' order by DateOfItem";
                            DataTable dm = dataobj.Selectdatatable(selSameto);
                            string up = @"UPDATE [dbo].[TransactionOfStore] SET [Quantity]=@1 where [Code] = @2 and [ItemId] = @3 and [Quantity] = @4 and[TypeOfStore] = @5 and [TypeOfOperation] = @6 and [DateOfItem] = @7 and [Store_Id] = @8";
                            float upnum = (float.Parse(newval) - x);
                            dataobj.Execute(up, upnum, dm.Rows[0]["Code"].ToString(), dm.Rows[0]["ItemId"].ToString(), dm.Rows[0]["Quantity"].ToString(), dm.Rows[0]["TypeOfStore"].ToString(), dm.Rows[0]["TypeOfOperation"].ToString(), dm.Rows[0]["DateOfItem"].ToString(), dm.Rows[0]["Store_Id"].ToString());
                            string insertnew = @"INSERT INTO [dbo].[TransactionOfStore] ([Code],[ItemId],[Quantity],[TypeOfStore],[TypeOfOperation],[DateOfItem],[Store_Id]) VALUES(@1,@2,@3,@4,@5,@6,@7)";
                            dataobj.Execute(insertnew, TxtCode.Text, dt.Rows[i]["code"].ToString(), float.Parse(dt.Rows[i]["Num"].ToString()) * (float.Parse(dts.Rows[0]["Quantity"].ToString())), dm.Rows[0]["TypeOfStore"].ToString(), "تحويل المخازن", dm.Rows[0]["DateOfItem"].ToString(), dt.Rows[i]["StoreTo"].ToString());
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
                            string up = @"UPDATE [dbo].[TransactionOfStore] SET [Store_Id]=@1 , [Code]=@2, [TypeOfOperation]=@3,[Quantity] = @4 where [Code] = @5 and [ItemId] = @6 and [Quantity] = @7 and[TypeOfStore] = @8 and [TypeOfOperation] = @9 and [DateOfItem] = @10 and  [Quantity]= @11 and [Store_Id]=@12";
                            dataobj.Execute(up, drpStoreto.SelectedItem.Value, TxtCode.Text, "تحويل المخازن", float.Parse(dt.Rows[i]["Num"].ToString()), dm.Rows[0]["Code"].ToString(), dm.Rows[0]["ItemId"].ToString(), dm.Rows[0]["Quantity"].ToString(), dm.Rows[0]["TypeOfStore"].ToString(), dm.Rows[0]["TypeOfOperation"].ToString(), dm.Rows[0]["DateOfItem"].ToString(), dm.Rows[0]["Quantity"].ToString(), dm.Rows[0]["Store_Id"].ToString());
                        }
                        else
                        {
                            string selSameto = @"select top(1)* from TransactionOfStore where Store_Id='" + dt.Rows[i]["Store_Id"].ToString() + "' and ItemId='" + dt.Rows[i]["code"].ToString() + "' order by DateOfItem";
                            DataTable dm = dataobj.Selectdatatable(selSameto);
                            string up = @"UPDATE [dbo].[TransactionOfStore] SET [Quantity]=@1 where [Code] = @2 and [ItemId] = @3 and [Quantity] = @4 and[TypeOfStore] = @5 and [TypeOfOperation] = @6 and [DateOfItem] = @7 and [Store_Id] = @8";
                            float upnum = (float.Parse(newval) - x);
                            dataobj.Execute(up, upnum, dm.Rows[0]["Code"].ToString(), dm.Rows[0]["ItemId"].ToString(), dm.Rows[0]["Quantity"].ToString(), dm.Rows[0]["TypeOfStore"].ToString(), dm.Rows[0]["TypeOfOperation"].ToString(), dm.Rows[0]["DateOfItem"].ToString(), dm.Rows[0]["Store_Id"].ToString());
                            string insertnew = @"INSERT INTO [dbo].[TransactionOfStore] ([Code],[ItemId],[Quantity],[TypeOfStore],[TypeOfOperation],[DateOfItem],[Store_Id]) VALUES(@1,@2,@3,@4,@5,@6,@7)";
                            dataobj.Execute(insertnew, TxtCode.Text, dt.Rows[i]["code"].ToString(), float.Parse(dt.Rows[i]["Num"].ToString()), dm.Rows[0]["TypeOfStore"].ToString(), "تحويل المخازن", dm.Rows[0]["DateOfItem"].ToString(), dt.Rows[i]["StoreTo"].ToString());
                        }
                    }
                }
            }

            string sql = @"insert into [dbo].[MoveItemStoresDetails] select * from [dbo].[tempMoveItemStores] where MoveCode=@1";
            dataobj.Execute(sql, TxtCode.Text);
            string sqldel = @"delete from [dbo].[tempMoveItemStores] where [MoveCode]=@1";
            dataobj.Execute(sqldel, TxtCode.Text);
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
            string sql = @"select * from tempMoveItemStores where MoveCode='" + TxtCode.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            if (dt.Rows.Count > 0)
            {
                string sqlinsert = @"INSERT INTO [dbo].[MoveItemStores] ([Code],[StoreFrom],[StoreTo],[DateOfOperation],[User_Id],[Notes]) VALUES(@1,@2,@3,@4,@5,@6)";
                dataobj.Execute(sqlinsert, TxtCode.Text, drpStore.SelectedItem.Value, drpStoreto.SelectedItem.Value, txtDate.Text, Session["Id"], txtNotes.Text);
                CompeleteData();
                dataobj.Alert("Success", this);
                txtNotes.Text = string.Empty;
                drpStore.SelectedValue = "ختار المخزن المحول منه";
                drpName.Items.Clear();
                generateguid();
            }
            else
            {
                dataobj.Alert("ادخل الاصناف المطلوبة", this);
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
}