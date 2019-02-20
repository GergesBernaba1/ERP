<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="ReportAllBackSales.aspx.cs" Inherits="ReportAllBackSales" %>

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
                        <h2 class="title fix-bg">تقرير فواتير مرتجع المبيعات</h2>
                    </div>
                    <div class="row">
                        <div class="mainfix clearfix">
                            <div class="col-xs-12">
                                <div class="main-info clearfix">
                                    <div class="col-md-9 col-xs-12">
                                        <div class="item-info">
                                            <label >الفترة من</label>
                                            <asp:TextBox ID="txtDateFrom" onchange="changeborderColorafterfill(this)" ValidationGroup="1" CssClass="form-control mid-screen xs-screen" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDateFrom" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                            <ajaxToolkit:CalendarExtender ID="datetimesetting" runat="server" TargetControlID="txtDateFrom" />
                                            <label >الي</label>
                                            <asp:TextBox ID="txtDateTo" onchange="changeborderColorafterfill(this)" ValidationGroup="1" CssClass="form-control mid-screen xs-screen" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDateTo" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateTo" />
                                        </div>
                                        <div class="col-md-12 non-padding">
                                            <div class="submit">
                                                <asp:Button ID="btnSave" CssClass="btn" runat="server" Text="تقرير / Report" ValidationGroup="1" OnClientClick="customValidations('1')" OnClick="btnSave_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="div"   visible="false" class="title-hav clearfix">
                            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
                            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                                <Report FileName="ReportBackSales"></Report>
                            </CR:CrystalReportSource>
                            </div>
                    </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>