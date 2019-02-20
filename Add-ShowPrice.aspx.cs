using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Add_ShowPrice : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=38 and IsVaild=1";
                DataTable dt1 = dataobj.Selectdatatable(sql1);
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
                  
                        float x = float.Parse(dt.Rows[0]["pruchPrice"].ToString());
                        txtprice.Text = x.ToString();
                        txtTotal.Text = ((float.Parse(x.ToString())) * (float.Parse(txtQuantity.Text))).ToString();
                        return true;
                }
                else
                {
                    string check = @"select ItemSaleType,Quantity,SalePrice from Items where Code='" + drpName.SelectedItem.Value + "'";
                    DataTable dts = dataobj.Selectdatatable(check);
                    if (dts.Rows[0]["ItemSaleType"].ToString() == drpType.Text)
                    {
                       
                            float x = float.Parse(dts.Rows[0]["SalePrice"].ToString());
                            txtprice.Text = x.ToString();
                            txtTotal.Text = ((float.Parse(x.ToString())) * (float.Parse(txtQuantity.Text))).ToString();
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
            if (sumprice() == true)
            {
                if (drpName.SelectedIndex > 0)
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
    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ViewState["price"] = float.Parse(txtprice.Text);
            ViewState["quantity"] = float.Parse(txtQuantity.Text);
            txtTotal.Text = (float.Parse(txtQuantity.Text) * float.Parse(txtprice.Text)).ToString();
            
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
            txtTotal.Text = ((float.Parse(dt.Rows[0]["pruchPrice"].ToString())) * (float.Parse(txtQuantity.Text))).ToString();
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
                txtTotal.Text = ((float.Parse(dt.Rows[0]["pruchPrice"].ToString())) * (float.Parse(txtQuantity.Text))).ToString();
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
    protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
    {
        sumprice();
    }
    private void sumTotal()
    {
        string sqlsum = @"select SUM(Price*Quantity) as sumprice from [dbo].[tempPrucheDetails] where PrucheCode='" + TxtCode.Text + "' and [Store_Id]='" + drpStore.SelectedItem.Value + "'";
        DataTable ds = dataobj.Selectdatatable(sqlsum);
        float x = float.Parse(ds.Rows[0]["sumprice"].ToString());
        float sum = x + (float.Parse(txtAdd.Text)) - (float.Parse(txtDiscount.Text));
        LabTotal.Text  = sum.ToString();
    }
    protected void txtAdd_TextChanged(object sender, EventArgs e)
    {
        sumTotal();
    }
    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        sumTotal();
    }
    private void CompeleteData()
    {
        try
        {
            string sql = @"insert into [dbo].[ShowPriceDetails] select [PrucheCode],[Item_Id],[Quantity],[Price],[Type],[Date],[Store_Id] from [dbo].[tempPrucheDetails] where PrucheCode=@1";
            dataobj.Execute(sql, TxtCode.Text);
            string sqldel = @"delete from [dbo].[tempPrucheDetails] where [PrucheCode]=@1";
            dataobj.Execute(sqldel, TxtCode.Text);

            txtAdd.Text = txtDiscount.Text = LabTotal.Text = 0.ToString();
            drpStore.Text = "اختار المخزن";
            drpName.Items.Clear();
            generateguid();
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
                    string sql = @"INSERT INTO [dbo].[ShowPrices] ([Code],[Store_Id],[DateOfOver],[User_Id],[AddMoney],[Discount],[Pay],[Total],[Notes],[Name]) VALUES(@1,@2,@3,@4,@5,@6,@7,@8,@9,@10)";
                    dataobj.Execute(sql, TxtCode.Text, drpStore.SelectedItem.Value, txtDate.Text, Session["Id"] , float.Parse(txtAdd.Text), float.Parse(txtDiscount.Text), 0, float.Parse(LabTotal.Text), txtNotes.Text, txtName.Text);
                    CompeleteData();
                    txtNotes.Text = string.Empty;
                    dataobj.Alert("Success", this);
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
}