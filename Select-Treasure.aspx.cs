using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Select_Treasure : System.Web.UI.Page
{
    databaseaccesslayer DataObj = new databaseaccesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string sql = @"select * from Application_Setting where User_Id=" + Session["Id"] + " and Appl_Id=9 and IsVaild=1";
                DataTable dt = DataObj.Selectdatatable(sql);
                if (dt.Rows.Count == 1)
                {
                    string sql1 = @"select Name as الاسم,(select (OldAmountCome+(select sum(Amount) from TransactionOfTreasure where Amount>0)) from MainTreasure ) as الايرادات,(select (OldAmountOut+(select (-1*sum(Amount)) from TransactionOfTreasure where Amount<0)) from MainTreasure) as المصروفات,(select (Amount+(select (sum(Amount)) from TransactionOfTreasure)) from MainTreasure ) as المبلغ from MainTreasure ";
                    grdSelect.DataSource = DataObj.Selectdatatable(sql1);
                    grdSelect.DataBind();
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
}