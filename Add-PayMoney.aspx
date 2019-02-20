<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="Add-PayMoney.aspx.cs" Inherits="Add_PayMoney" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="Style/cust-tabs.css" rel="stylesheet" />--%>
    <link href="Style/Add-Cat.css" rel="stylesheet" />
    <link href="Style/style.css" rel="stylesheet" />
    <script src="js/inputvalidations.js"></script>

    <%-- Scan --%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="tabs-cus-Sup clearfix">

                <div class="container-fluid">
                    <div class="title-hav clearfix">
                        <h2 class="title fix-bg">دفع نقدية</h2>
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
                                                اختار
                                            </label>
                                            <asp:DropDownList ID="drpSelect" CssClass="form-control mid-screen xs-screen" AutoPostBack="true" OnTextChanged="drpSelect_TextChanged" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="اختار" ControlToValidate="drpSelect" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="item-info">
                                            <label>
                                                الاسم
                                            </label>
                                            <asp:DropDownList ID="drpName" CssClass="form-control mid-screen xs-screen" runat="server" AutoPostBack="true" OnTextChanged="drpName_TextChanged" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="اختار الاسم" ControlToValidate="drpName" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="item-info">
                                            <label>
                                                التاريخ
                                            </label>
                                            <asp:TextBox ID="txtDate" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="datetimesetting" runat="server" TargetControlID="txtDate" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                        </div>

                                        <ul class="nav nav-tabs" id="myTab" role="tablist">
                                            <li class="nav-item">
                                                <a class="nav-link active" data-toggle="tab" href="#general" role="tab" style="font-size: 24px" aria-controls="general">نقدي</a>
                                            </li>
                                            <li class="nav-item">
                                                <a class="nav-link" data-toggle="tab" href="#Over" role="tab" style="font-size: 24px" aria-controls="Over">قسط</a>
                                            </li>
                                        </ul>
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="general" role="tabpanel">
                                                <div class="item-info">
                                                    <label>
                                                        المبلغ
                                                    </label>
                                                    <asp:TextBox ID="txtAmount" CssClass="form-control mid-screen xs-screen" runat="server" ></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="tab-pane " id="Over" role="tabpanel">
                                                <div id="Div1" dir="rtl" runat="server" style="height: 85%; width: 100%; left: 5%; right: 5%; margin: 2em auto; overflow-x: scroll">
                                                    <asp:GridView ID="grdSelect" runat="server" CellPadding="2" AllowPaging="True" Height="100%" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" PageSize="4" OnPageIndexChanging="grdSelect_PageIndexChanging">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="btnCheckAll" runat="server" />
                                                                    <%--<input id="btnCheckAll" type="checkbox" name="AllCheck" />--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
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
                                        </div>

                                        <div class="item-info">
                                            <label class="lab-notes ">ملاحظات</label>
                                            <asp:TextBox ID="txtNotes" TextMode="MultiLine" Width="71%" Height="100px" CssClass="in-tabs form-control" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-md-12 non-padding">
                                            <div class="submit">
                                                <asp:Button ID="btnSave" CssClass="btn" runat="server" Text="حفظ / Save" ValidationGroup="1" OnClick="btnSave_Click"  OnClientClick="customValidations('1')" />


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




