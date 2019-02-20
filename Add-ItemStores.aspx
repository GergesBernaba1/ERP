<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="Add-ItemStores.aspx.cs" Inherits="Add_ItemStores" %>

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
                        <h2 class="title fix-bg">ربط الاصناف بالمخازن</h2>
                    </div>
                    <p class="Head">المخازن</p>
                    <hr />
                    <div class="row">
                        <div class="mainfix clearfix">
                            <div class="col-xs-12">
                                <div class="main-info clearfix">
                                    <div class="col-md-9 col-xs-12">
                                        <div class="item-info">
                                            <label>اختار المخزن</label>

                                            <asp:DropDownList ID="drpSelect" AutoPostBack="true"  CssClass="form-control mid-screen xs-screen" runat="server" OnSelectedIndexChanged="drpSelect_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        </div>
                                    </div>
                                <br />
                                        <p class="Head">الاصناف</p>
                                        <hr />
                                        <div class="col-md-9 col-xs-12">
                                                <div class="item-info">
                                                    <label class="lab-cust">
                                                        الاسم
                                                    </label>

                                                    <asp:TextBox ID="txtName" AutoPostBack="true" CssClass="form-control mid-screen xs-screen" onchange="changeborderColorafterfill(this)" ValidationGroup="2" runat="server" OnTextChanged="txtName_TextChanged"></asp:TextBox>
                                                </div>
                                            <div id="Div1" dir="rtl" runat="server" style="height: 200px; width: 100%; left: 5%; right: 5%; margin: 2em auto; overflow-x: scroll">
                            <asp:GridView ID="grdSelect" runat="server"  AllowPaging="true"  Height="40" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" PageSize="5" OnPageIndexChanging="grdSelect_PageIndexChanging"  >
                                  <Columns>
                         <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:CheckBox ID="btnCheckAll" runat="server"  />
                                 <%--<input id="btnCheckAll" type="checkbox" name="AllCheck" />--%>
                             </ItemTemplate>
                         </asp:TemplateField>
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


