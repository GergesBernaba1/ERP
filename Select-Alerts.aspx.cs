using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class Select_Alerts : System.Web.UI.Page
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql = @"select Code as الكود,Name as الاسم,InitialBalance as الرصيد,Mobil as الموبيل,Customer as عميل,Supplier as مورد from CustomerAndSupplier where InitialBalance >=10000 or InitialBalance<=-10000";
                DataTable dt = dataobj.Selectdatatable(sql);
                if (dt.Rows.Count > 0)
                {
                    LinkCustomer.ForeColor = Color.Red;
                }
                string sql1 = @"select c.Name as الاسم,c.Mobil as الموبيل,md.Amount as المبلغ,md.Date as تاريخ_السداد from MonthlyInstallments m left join CustomerAndSupplier c on m.CustomerOrSupplier_Id=c.Code left join MonthlyInstallmentsDetails md on m.Code=md.Code where md.Date<'" + DateTime.Now.ToShortDateString() + "' and md.IsVaild=0";
                DataTable dts = dataobj.Selectdatatable(sql1);
                if (dts.Rows.Count > 0)
                {
                    LinkMonthely.ForeColor = Color.Red;
                }
                string sql2 = @"select i.ItemName as اسم_الصنف,t.TypeOfStore as نوع_المخزون,s.StoreName as المخزن,t.DateOfItem as تاريخ_الصلاحية,t.Quantity as الكمية from TransactionOfStore t left join Items i on t.ItemId=i.Code left join Store s on t.Store_Id=s.Code where DATEDIFF(DD,GETDATE(),t.DateOfItem)<=10";
                DataTable dts1 = dataobj.Selectdatatable(sql2);
                if (dts1.Rows.Count > 0)
                {
                    LinkDate.ForeColor = Color.Red;
                }
                string sql3 = @"select i.ItemName as اسم_الصنف,i.ItemSaleType as نوع_المخزون,(select SUM(Quantity) from TransactionOfStore where ItemId=i.Code) as الكمية from Items i where (select SUM(Quantity) from TransactionOfStore where ItemId=i.Code)<10";
                DataTable dts2 = dataobj.Selectdatatable(sql3);
                if (dts2.Rows.Count > 0)
                {
                    LinkQuantity.ForeColor = Color.Red;
                }
            }
            catch (Exception)
            { Response.Redirect("MainPage.aspx"); }
        }
    }
    protected void LinkCustomer_Click(object sender, EventArgs e)
    {
        try
        {
            string sql = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=5 and IsVaild=1";
            DataTable dt = dataobj.Selectdatatable(sql);
            if (dt.Rows.Count == 1)
            {
                string sql1 = @"select Code as الكود,Name as الاسم,InitialBalance as الرصيد,Mobil as الموبيل,Customer as عميل,Supplier as مورد from CustomerAndSupplier where InitialBalance >=10000 or InitialBalance<=-10000";
                grdSelect.DataSource = dataobj.Selectdatatable(sql1);
                grdSelect.DataBind();
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void LinkMonthely_Click(object sender, EventArgs e)
    {
        try
        {
            string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=6 and IsVaild=1";
            DataTable dt = dataobj.Selectdatatable(sql1);
            if (dt.Rows.Count == 1)
            {
                string sql = @"select c.Name as الاسم,c.Mobil as الموبيل,md.Amount as المبلغ,md.Date as تاريخ_السداد from MonthlyInstallments m left join CustomerAndSupplier c on m.CustomerOrSupplier_Id=c.Code left join MonthlyInstallmentsDetails md on m.Code=md.Code where md.Date<'" + DateTime.Now.ToShortDateString() + "' and md.IsVaild=0";
                grdSelect.DataSource = dataobj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void LinkDate_Click(object sender, EventArgs e)
    {
        try
        {
            string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=7 and IsVaild=1";
            DataTable dt = dataobj.Selectdatatable(sql1);
            if (dt.Rows.Count == 1)
            {
                string sql = @"select i.ItemName as اسم_الصنف,t.TypeOfStore as نوع_المخزون,s.StoreName as المخزن,t.DateOfItem as تاريخ_الصلاحية,t.Quantity as الكمية from TransactionOfStore t left join Items i on t.ItemId=i.Code left join Store s on t.Store_Id=s.Code where DATEDIFF(DD,GETDATE(),t.DateOfItem)<=10";
                grdSelect.DataSource = dataobj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
    protected void LinkQuantity_Click(object sender, EventArgs e)
    {
        try
        {
            string sql1 = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=8 and IsVaild=1";
            DataTable dt = dataobj.Selectdatatable(sql1);
            if (dt.Rows.Count == 1)
            {
                string sql = @"select i.ItemName as اسم_الصنف,i.ItemSaleType as نوع_المخزون,(select SUM(Quantity) from TransactionOfStore where ItemId=i.Code) as الكمية from Items i where (select SUM(Quantity) from TransactionOfStore where ItemId=i.Code)<10";
                grdSelect.DataSource = dataobj.Selectdatatable(sql);
                grdSelect.DataBind();
            }
        }
        catch (Exception ex)
        {
            dataobj.Alert(ex.Message, this);
        }
    }
}