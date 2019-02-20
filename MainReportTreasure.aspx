<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="MainReportTreasure.aspx.cs" Inherits="MainReportTreasure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="Style/main.css" />
    <div class="container-fluid">
    <div class="row">
        <div class="f-width">
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-blue money">
                    <p>تقرير الخزينة</p>
                    <a href="ReportTreasure.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
            </div>
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-rebec money">
                    <p>تقرير عمليات الخزينة</p>
                    <a href="ReportTransactionTrasure.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
        </div>
    </div>
</div>
</div>
</asp:Content>

