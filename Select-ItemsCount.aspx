<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="Select-ItemsCount.aspx.cs" Inherits="Select_ItemsCount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Style/style.css" rel="stylesheet" />
    <link href="Style/select-cut-group.css" rel="stylesheet" />
    <script src="js/inputvalidations.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="add-custgrp clearfix">
                <div class="container-fluid">
                    <div class="title-hav clearfix">
                        <h2 class="title fix-bg">مراجعة كميات الاصناف بالمخازن</h2>
                    </div>
                            <div class="item-info">
                                <label>المخزن</label>
                                <asp:DropDownList ID="drpStore" CssClass="form-control mid-screen xs-screen" AutoPostBack="true" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1" OnSelectedIndexChanged="drpStore_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="اختار المخزن" ControlToValidate="drpStore" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>

                        <label>اسم الصنف</label>
                        <asp:TextBox ID="txtName" CssClass=" form-control mid-screen-b  xs-screen-b" runat="server" AutoPostBack="true" OnTextChanged="txtName_TextChanged" onchange="changeborderColorafterfill(this)" ValidationGroup="1" TabIndex="4"></asp:TextBox>  
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtName" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                    </div>
                    <hr />
        <div id="Div1" dir="rtl" runat="server" style="height: 300px; width: 100%; left: 5%; right: 5%; margin: 2em auto; overflow-x: scroll">
                            <asp:GridView ID="grdSelect" runat="server"  AllowPaging="true"  Height="40" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" PageSize="5" OnPageIndexChanging="grdSelect_PageIndexChanging" >   
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" ForeColor="#330099" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
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




