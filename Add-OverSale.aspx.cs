using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Add_OverSale : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=40 and IsVaild=1";
                DataTable dt1 = dataobj.Selectdatatable(sql1);
                if (dt1.Rows.Count == 1)
                {
                    generateguid();
                    addValidationGroup();
                    string sql = @"select Code,StoreName from Store";
                    drpStore.DataSource = dataobj.Selectdatatable(sql);
                    drpStore.DataValueField = "Code";
                    drpStore.DataTextField = "StoreName";
                    drpStore.DataBind();
                    drpStore.Items.Insert(0, "اختار المخزن");
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
    protected void drpStore_TextChanged(object sender, EventArgs e)
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
                string sql = @"select BarCode,pruchPrice from Items where Code='" + drpName.SelectedItem.Value + "'";
                DataTable dt = dataobj.Selectdatatable(sql);
                txtBarCode.Text = dt.Rows[0]["BarCode"].ToString();
                txtQuantity.Text = "1";
                DataBindData();
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void LinkAdd_Click(object sender, EventArgs e)
    {
        try
        {
            addValidationGroup();
            if (drpName.SelectedIndex > 0)
            {
                string check = @"select * from [dbo].[tempOverSale] where [OverCode]='" + TxtCode.Text + "' and [Store_Id]='" + drpStore.SelectedItem.Value + "' and [Item_Id]='" + drpName.SelectedItem.Value + "' and [Quantity]=" + float.Parse(txtQuantity.Text) + " and [Type]='" + drpType.Text + "'";
                DataTable dt = dataobj.Selectdatatable(check);
                if (dt.Rows.Count == 0)
                {
                    string sql = @"INSERT INTO [dbo].[tempOverSale] ([OverCode],[Item_Id],[Quantity],[Type],[Store_Id]) VALUES(@1,@2,@3,@4,@5)";
                    dataobj.Execute(sql, TxtCode.Text, drpName.SelectedItem.Value, float.Parse(txtQuantity.Text), drpType.Text, drpStore.SelectedItem.Value);
                    txtBarCode.Text = txtQuantity.Text = string.Empty;
                    drpName.SelectedValue = "اختار الصنف";
                    drpType.Items.Clear();
                    ;
                }
                else
                {
                    dataobj.Alert("تم ادخال هذه البيانات من قبل", this);
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
            string sql = @"select i.BarCode as BarCode,i.ItemName as Name,o.Quantity as Num,Type from tempOverSale o left join Items i on o.Item_Id=i.Code where OverCode='" + TxtCode.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            string sqldel = @"delete from tempOverSale where [OverCode]=@1 and [Item_Id]=(select Code from Items where ItemName=@2) and [Quantity]=@3 and [Type]=@4";
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
            string sql = @"select i.BarCode as BarCode,i.ItemName as Name,o.Quantity as Num,Type from tempOverSale o left join Items i on o.Item_Id=i.Code where OverCode='" + TxtCode.Text + "'";
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string sql = @"select * from tempOverSale where OverCode='" + TxtCode.Text + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            if (dt.Rows.Count > 0)
            {
                string sqlinsert = @"INSERT INTO [dbo].[OverSales] ([Code],[Store_Id],[NameOfOver],[Amount],[DateFrom],[DateTo],[Notes]) VALUES(@1,@2,@3,@4,@5,@6,@7)";
                dataobj.Execute(sqlinsert, TxtCode.Text, drpStore.SelectedItem.Value, txtNameOver.Text, float.Parse(txtAmount.Text), DateTime.Parse(txtDateFrom.Text).ToShortDateString(), DateTime.Parse(txtDateTo.Text).ToShortDateString(), txtNotes.Text);
                string sqlmove = @"insert into [dbo].[OverSaleDetails] select * from [dbo].[tempOverSale] where OverCode=@1";
                dataobj.Execute(sqlmove, TxtCode.Text);
                string sqldel = @"delete from [dbo].[tempOverSale] where [OverCode]=@1";
                dataobj.Execute(sqldel, TxtCode.Text);
                txtNotes.Text = txtAmount.Text=txtDateFrom.Text=txtDateTo.Text=txtNameOver.Text= string.Empty;
                drpStore.Text = "اختار المخزن";
                drpName.Items.Clear();
                generateguid();
                dataobj.Alert("Success", this);
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