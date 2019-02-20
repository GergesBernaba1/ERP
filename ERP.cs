using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

/// <summary>
/// Summary description for ERP
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ERP : System.Web.Services.WebService
{
    databaseaccesslayer dataobj = new databaseaccesslayer();
    public ERP()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    public class ItemOrder
    {
        public string Code { get; set; }
        public string Item { get; set; }
        public float quantity { get; set; }
        public string Type { get; set; }
    }
    [WebMethod]
    public void DeleteOrder(string code, string item, float quantity, string type)
    {
        try
        {
            string sql = @"delete from OrderDetails where [OrderCode]=@1 and [Item_Id]=(select Code from Items where ItemName=@2) and [Quantity]=@3 and [Type]=@4";
            dataobj.Execute(sql, code, item, quantity, type);
            var list = Key._KeyValuePair("status", "ok");
        }
        catch (Exception ex)
        {
            var list = Key._KeyValuePair("status", ex.Message);
            var json = new JavaScriptSerializer().Serialize(list);
            Context.Response.Write(json);
        }

    }
    [WebMethod]
    public void SelectItem(string code, string item, float quantity, string type)
    {
        try
        {

            List<ItemOrder> more = new List<ItemOrder> { };
            string sql = @"select BarCode ,ItemName,o.Quantity as quantity,Type from OrderDetails o left join Items i on o.Item_Id=i.Code where [OrderCode]='" + code + "' and [Item_Id]=(select Code from Items where ItemName='" + item + "') and o.[Quantity]=" + quantity + " and [Type]='" + type + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            more.Add(new ItemOrder { Code = dt.Rows[0]["BarCode"].ToString(), Item = dt.Rows[0]["ItemName"].ToString(), quantity = float.Parse(dt.Rows[0]["quantity"].ToString()), Type = dt.Rows[0]["Type"].ToString() });
            var json = new JavaScriptSerializer().Serialize(more);
            Context.Response.Write(json);
            DeleteOrder(code, item, quantity, type);
        }
        catch (Exception ex)
        {
            var list = Key._KeyValuePair("Error", ex.Message);
            var json = new JavaScriptSerializer().Serialize(list);
            Context.Response.Write(json);
        }

    }
    [WebMethod]
    public void SelectItemOrder(string order)
    {
        try
        {
            List<ItemOrder> more = new List<ItemOrder> { };
            string sql = @"select i.BarCode as BarCode,i.ItemName as Name,o.Quantity as Num,Type from OrderDetails o left join Items i on o.Item_Id=i.Code where OrderCode='" + order + "'";
            DataTable dt = dataobj.Selectdatatable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                more.Add(new ItemOrder { Code = dt.Rows[i]["BarCode"].ToString(), Item = dt.Rows[i]["Name"].ToString(), quantity = float.Parse(dt.Rows[i]["Num"].ToString()), Type = dt.Rows[i]["Type"].ToString() });
            }
            var json = new JavaScriptSerializer().Serialize(more);
            Context.Response.Write(json);
        }
        catch (Exception ex)
        {
            var list = Key._KeyValuePair("Error", ex.Message);
            var json = new JavaScriptSerializer().Serialize(list);
            Context.Response.Write(json);
        }

    }
}

