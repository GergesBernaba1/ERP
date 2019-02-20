<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="ReportUserBackPruches.aspx.cs" Inherits="ReportUserBackPruches" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Style/cust-tabs.css" rel="stylesheet" />
    <link href="Style/style.css" rel="stylesheet" />
    <script src="js/inputvalidations.js"></script>

    <%-- Scan --%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="tabs-cus-Sup clearfix">

                <div class="container-fluid">
                    <div class="title-hav clearfix">
                        <h2 class="title fix-bg">تقرير مرتجع المشتريات للمستخدم</h2>
                    </div>
            </section>

            <CR:CrystalReportViewer ID="CrystalReportViewer1" ReportSourceID="CrystalReportSource1" HasRefreshButton="True" runat="server" AutoDataBind="true" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <Report FileName="ReportUserBackPruches.rpt"></Report>
            </CR:CrystalReportSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
