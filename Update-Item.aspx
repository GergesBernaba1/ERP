<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="Update-Item.aspx.cs" Inherits="Update_Item" %>

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
                        <h2 class="title fix-bg">تعديل صنف</h2>
                    </div>
                    <p class="Head">البيانات الاساسية</p>
                    <hr />
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
                                                اسم الصنف
                                            </label>
                                            <asp:TextBox ID="TxtName" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="item-info">
                                            <label>
                                                الباركود
                                            </label>
                                            <asp:TextBox ID="TxtBarCode" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtBarCode" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-xs-12 non-padding">
                                            <div class="item-info">
                                                <label> حالة الصنف</label>
                                                <div class="re-chk ce-chk">
                                                    <asp:CheckBox ID="chkActive" CssClass="btn-chk" runat="server" />
                                                    <p class="p-chk">نشـط/Active</p>
                                                </div>
                                            </div>
                                            <div class="item-info">
                                                <label> تاريخ الصلاحية</label>
                                                <div class="re-chk ce-chk">
                                                    <asp:CheckBox ID="chkDate" CssClass="btn-chk" runat="server" />
                                                    <p class="p-chk">هذا الصنف يتعامل بتاريخ صلاحية</p>
                                                </div>
                                            </div>
                                        </div>
                                        </div>
                                    </div>
                                <br />
                                        <p class="Head">بيانات اضافية</p>
                                        <hr />
                                        <div class="col-md-9 col-xs-12">
                                                <div class="item-info">
                                                    <label class="labwidth">
                                                        الصنف يباع بـ
                                                    </label>
                                                    <asp:TextBox ID="txtSale" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSale" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="item-info">
                                                    <label class="labwidth">
                                                        الصنف يشتري بـ
                                                    </label>
                                                    <asp:TextBox ID="txtPruch" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPruch" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="item-info">
                                                    <label class="labwidth" > الكمية الموجودة في المباع</label>
                                                    <asp:TextBox ID="txtNum" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNum" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                                </div>
                                            <div class="item-info">
                                                    <label class="labwidth">
                                                        سعر الشراء
                                                    </label>
                                                    <asp:TextBox ID="txtPricePruch" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPricePruch" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                                </div>
                                                      <div class="item-info">
                                                    <label class="labwidth">
                                                        سعر البيع
                                                    </label>
                                                    <asp:TextBox ID="txtPriceSale" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPriceSale" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                                </div>
                                             <div class="item-info">
                                <label class="lab-notes labwidth">ملاحظات</label>
                                <asp:TextBox ID="txtNotes" TextMode="MultiLine" Width="71%" Height="100px" CssClass="in-tabs form-control" runat="server"></asp:TextBox>
                            </div>
                                           
                                </div>
                            </div>
                            <div class="item-info">
                                                <asp:Button ID="btnSave" CssClass="btn" runat="server" Text="حفظ / Save" ValidationGroup="1" OnClientClick="customValidations('1')" OnClick="btnSave_Click"   />

                                                    <asp:Button ID="btnDel" CssClass="btn" runat="server" Text="حذف / Delete"  OnClick="btnDel_Click"  />
                                            </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

