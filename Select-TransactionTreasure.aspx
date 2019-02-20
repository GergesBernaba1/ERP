<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="Select-TransactionTreasure.aspx.cs" Inherits="Select_TransactionTreasure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Style/style.css" rel="stylesheet" />
    <%--<link href="Style/select-cut-group.css" rel="stylesheet" />--%>
    <script src="js/inputvalidations.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="add-custgrp clearfix">
                <div class="container-fluid">
                    <div class="title-hav clearfix">
                        <h2 class="title fix-bg">مراجعة تفاصيل الخزينة</h2>
                    </div>

                    <div class="item-info">
                        <label class="labsel-a">
                            نوع العملية
                        </label>
                        <asp:TextBox ID="txtName" CssClass="form-control mid-screen xs-screen" AutoPostBack="true" OnTextChanged="txtName_TextChanged" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                    </div>
                    <div class="item-info">
                        <label class="labsel-a">التاريخ من</label>
                        <asp:TextBox ID="txtDateFrom" CssClass="form-control mid-screen xs-screen" AutoPostBack="true" OnTextChanged="txtDateFrom_TextChanged" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="datetimesetting" runat="server" TargetControlID="txtDateFrom" />
                        <label class="labsel-a">الي</label>
                        <asp:TextBox ID="txtDateTo" CssClass="form-control mid-screen xs-screen" AutoPostBack="true" OnTextChanged="txtDateTo_TextChanged" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateTo" />
                    </div>
                    <hr />
                    <div id="Div1" dir="rtl" runat="server" style="width: 100%; left: 5%; right: 5%; margin: 2em auto; overflow-x: scroll">
                        <asp:GridView ID="grdSelect" runat="server" AllowPaging="true"  Height="40" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" OnPageIndexChanging="grdSelect_PageIndexChanging" BorderWidth="1px" PageSize="5">
                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" CssClass="trheigh" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" CssClass="trheigh" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" CssClass="trheigh" />
                            <RowStyle BackColor="White" ForeColor="#330099" CssClass="trheigh" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" CssClass="trheigh" />
                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
                            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                        </asp:GridView>
                    </div>
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
