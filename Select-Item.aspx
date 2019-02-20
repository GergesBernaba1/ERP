<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="Select-Item.aspx.cs" Inherits="Select_Item" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Style/style.css" rel="stylesheet" />
    <link href="Style/select-cut-group.css" rel="stylesheet" />
    <script src="js/inputvalidations.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="add-custgrp clearfix">
                <div class="container-fluid">
                    <div class="title-hav clearfix">
                        <h2 class="title fix-bg">مراجعة الاصناف</h2>
                    </div>
                            <asp:RadioButtonList ID="Radio" RepeatDirection="Horizontal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioCustomer_SelectedIndexChanged">
                                <asp:ListItem Value="الكل" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="نشط"></asp:ListItem>
                                <asp:ListItem Value="غير نشط"></asp:ListItem>
                            </asp:RadioButtonList>
                    <hr />
        <div id="Div1" dir="rtl" runat="server" style="height: 300px; width: 100%; left: 5%; right: 5%; margin: 2em auto; overflow-x: scroll">
                            <asp:GridView ID="grdSelect" runat="server"  AllowPaging="true"  Height="40" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" PageSize="5" OnPageIndexChanging="grdSelect_PageIndexChanging" OnSelectedIndexChanged="grdSelect_SelectedIndexChanged" >
                                <Columns>
                                <asp:CommandField ShowSelectButton="True" SelectText="تعديل" />
                            </Columns>
                            
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



