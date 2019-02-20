<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="MainReportSales.aspx.cs" Inherits="MainReportSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="Style/main.css" />
    <div class="container-fluid">
    <div class="row">
        <div class="f-width">
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-blue money">
                    <p>تقرير المبيعات</p>
                    <a href="ReportAllSales.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
            </div>
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-rebec money">
                    <p>تقرير تفاصيل المبيعات</p>
                    <a href="ReportDetailsSales.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
        </div>
             <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-lb money">
                    <p>تقرير مرتجع المبيعات</p>
                    <a href="ReportAllBackSales.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
                 </div>
                  <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-b money">
                    <p>تقرير تفاصيل مرتجع المبيعات</p>
                    <a href="ReportBackSaleDetail.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
        </div>
            
    </div>
</div>
</div>
</asp:Content>

