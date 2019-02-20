<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="MainPage.aspx.cs" Inherits="MainPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="Style/main.css" />
    <div class="container-fluid">
    <div class="row">
        <div class="f-width">
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-b ">
                    <img src="image/imac (1).png" alt="" />
                    <p>فاتورة المبيعات</p>
                    <a href="Add-Sales.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
            <div class="col-md-6 col-xs-12 n-paddin-l">
                <div class="item-min orang">
                    <img src="image/circuit (1).png" alt="" />
                    <p>مرتجع المبيعات</p>
                    <a href="Add-BackSales.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
            <div class="col-md-6 col-xs-12 n-paddin-r">
                <div class="item-min bg-rebec">
                    <img src="image/file (1).png" alt="" />
                    <p>عروض تخفيضات</p>
                    <a href="Add-OverSale.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
           <div class="col-xs-12">
               <div class="item bg-blue money">
                   <p>عرض الاسعار</p>
                   <a href="Add-ShowPrice.aspx">
                       <i class="glyphicon glyphicon-plus"></i>
                   </a>
               </div>
           </div>
        </div>
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item green">
                    <img src="image/login.png" alt="" />
                    <p>فاتورة مشتريات</p>
                    <a href="Add-Pruches.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
            <div class="col-md-6 col-xs-12 n-paddin-l">
                <div class="item-min move-col">
                    <img src="image/spin.png" alt="" />
                    <p>مرتجع المشتريات</p>
                    <a href="Add-BackPruches.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
            <div class="col-md-6 col-xs-12 n-paddin-r">
                <div class="item-min bg-blue">
                    <img src="image/bicycle.png" alt="" />
                    <p>طلبية المشتريات</p>
                    <a href="Add-Order.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
            <div class="col-xs-12">
                <div class="item bg-lb">
                    <img src="image/railroad-with-wood-planks%20(1).png" alt="" />
                    <p>تحويلات المخازن</p>
                    <a href="Add-MoveItem.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-rebec">
                    <img src="image/change.png" alt="" />
                    <p>استلام نقدية</p>
                    <a href="Add-ReciveMoney.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
            <div class="col-md-6 col-xs-12 n-paddin-l">
                <div class="item-min bg-lb">
                    <img src="image/wallet.png" alt="" />
                    <p>ايرادات</p>
                    <a href="Add-ComingMoney.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
            <div class="col-md-6 col-xs-12 n-paddin-r">
                <div class="item-min move-col">
                    <img src="image/notes.png" alt="" />
                    <p>مصروفات</p>
                    <a href="Add-Expenses.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
            <div class="col-xs-12">
                <div class="item bg-b">
                    <img src="image/payment-method.png" alt="" />
                    <p>دفع نقدية</p>
                    <a href="Add-PayMoney.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
        </div>
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-lb ">
                    <img src="image/danger.png" alt="" />
                    <p>التنبيهات</p>
                    <a href="Select-Alerts.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
        </div>
    </div>
</div>
</div>
</asp:Content>

