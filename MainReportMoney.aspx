<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="MainReportMoney.aspx.cs" Inherits="MainReportMoney" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="Style/main.css" />
    <div class="container-fluid">
    <div class="row">
        <div class="f-width">
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-blue money">
                    <p>تقرير استلام نقدية</p>
                    <a href="ReportReciveMoney.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
            </div>
        <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-rebec money">
                    <p>تقرير دفع نقدية</p>
                    <a href="ReportPayMoney.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
        </div>
             <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-lb money">
                    <p>تقرير المصروفات</p>
                    <a href="ReportExpenses.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
                 </div>
                  <div class="col-md-3 col-sm-6 col-xs-12">
            <div class="col-xs-12">
                <div class="item bg-b money">
                    <p>تقرير الايرادات</p>
                    <a href="ReportComingMoney.aspx">
                        <i class="glyphicon glyphicon-plus"></i>
                    </a>
                </div>
            </div>
        </div>
            
    </div>
</div>
</div>
</asp:Content>

