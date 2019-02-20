<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="MainReportPruches.aspx.cs" Inherits="MainReportPruches" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="Style/main.css" />
    <div class="container-fluid">
    <div class="row">
        <div class="f-width">
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-blue money">
                    <p>تقرير المشتريات</p>
                    <a href="ReportAllPruches.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
            </div>
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-rebec money">
                    <p>تقرير تفاصيل المشتريات</p>
                    <a href="ReportDetailPruches.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
        </div>
             <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-lb money">
                    <p>تقرير مرتجع المشتريات</p>
                    <a href="ReportAllBackPruches.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
                 </div>
                  <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-b money">
                    <p>تقرير تفاصيل مرتجع المشتريات</p>
                    <a href="ReportDetailsBackPrruches.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
        </div>
            
    </div>
</div>
</div>
</asp:Content>

