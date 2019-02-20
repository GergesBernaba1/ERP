<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="Add-Expenses.aspx.cs" Inherits="Add_Expenses" %>

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
                        <h2 class="title fix-bg">المصروفات</h2>
                    </div>
                    <div class="row">
                        <div class="mainfix clearfix">
                            <div class="col-xs-12">
                                <div class="main-info clearfix">
                                    <div class="col-md-9 col-xs-12">
                                        <div class="item-info">
                                            <label>الكود</label>

                                            <asp:TextBox ID="TxtCode" Enabled="false" CssClass="form-control mid-screen xs-screen" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="item-info">
                                            <label>
                                                نوع المصروف
                                            </label>
                                            <asp:TextBox ID="TxtName" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="item-info">
                                            <label>
                                                التاريخ
                                            </label>
                                            <asp:TextBox ID="txtDate" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="datetimesetting" runat="server" TargetControlID="txtDate" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="item-info">
                                            <label>
                                                المبلغ
                                            </label>
                                            <asp:TextBox ID="txtAmount" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAmount" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                        </div>
                                             <div class="item-info">
                                <label class="lab-notes ">ملاحظات</label>
                                <asp:TextBox ID="txtNotes" TextMode="MultiLine" Width="71%" Height="100px" CssClass="in-tabs form-control" runat="server"></asp:TextBox>
                            </div>
                                               
                                        <div class="col-md-12 non-padding">
                                            <div class="submit">
                                                <asp:Button ID="btnSave" CssClass="btn" runat="server" Text="حفظ / Save" ValidationGroup="1" OnClientClick="customValidations('1')" OnClick="btnSave_Click" />


                                            </div>
                                        </div>
                                </div>
                            </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



