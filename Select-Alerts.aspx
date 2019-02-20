<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="Select-Alerts.aspx.cs" Inherits="Select_Alerts" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Style/style.css" rel="stylesheet" />
    <script src="js/inputvalidations.js"></script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="add-custgrp clearfix">
                <div class="container-fluid">
                    <div class="title-hav clearfix">
                        <h2 class="title fix-bg">التنبيهات</h2>
                    </div>
                    <div class="item-info">
                        <asp:LinkButton ID="LinkCustomer" runat="server" OnClick="LinkCustomer_Click" Width="50%" CssClass="link" >ارصدة الدائنين أو المديونين</asp:LinkButton>

                        <asp:LinkButton ID="LinkMonthely" runat="server" OnClick="LinkMonthely_Click" CssClass="link" >الاقساط المتاخرة</asp:LinkButton>
                     </div>
                       <br />
                    <div class="item-info">
                        <asp:LinkButton ID="LinkDate" runat="server" OnClick="LinkDate_Click" Width="50%" CssClass="link" >تواريخ الصلاحية</asp:LinkButton>
                          <asp:LinkButton ID="LinkQuantity" runat="server" OnClick="LinkQuantity_Click"  CssClass="link" >كميات الاصناف الموجودة</asp:LinkButton>
                        </div>
                     </div>
                <hr />
                    <div class="col-xs-12 over-height-2" dir="rtl" >
        <div id="Div1" dir="rtl" runat="server" style="height:85%; width: 100%; left: 5%; right: 5%; margin: 2em auto; overflow-x: scroll">                        
                        <asp:GridView ID="grdSelect" runat="server" CellPadding="2"  AllowPaging="True" Height="100%" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" PageSize="4" >
                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" CssClass="trheigh" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC"  CssClass="trheigh" />
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
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
