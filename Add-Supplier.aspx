<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="Add-Supplier.aspx.cs" Inherits="Add_Supplier" %>
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
                        <h2 class="title fix-bg">اضافة مورد</h2>
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
                                                الاسم
                                            </label>

                                            <asp:TextBox ID="TxtARName" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtARName" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>

                                        </div>
                                        <div class="item-info">
                                            <label>
                                                الرصيد الافتتاحي
                                            </label>
                                            <asp:TextBox ID="txtMoney" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtMoney" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>
                                              <asp:RadioButtonList ID="RadioCustomer" CssClass="inline-div" RepeatDirection="Horizontal" runat="server">
                                                <asp:ListItem  Value="مدين"></asp:ListItem>
                                                <asp:ListItem Value="دائن" Selected="True"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:Label ID="lblType" CssClass="p-inline" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-xs-12 non-padding">
                                            <div class="item-info">
                                                <label>إعتباراً من</label>
                                                <asp:TextBox ID="txtDate" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1" ></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="datetimesetting" runat="server"  TargetControlID="txtDate"/>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDate" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <div class="re-chk ce-chk">
                                                    <asp:CheckBox ID="chkActive" CssClass="btn-chk" runat="server" />
                                                    <p class="p-chk">نشـط/Active</p>
                                                </div>
                                            </div>
                                        </div>
                                        </div>
                                    </div>
                                <br />
                                        <p class="Head">بيانات الاتصال</p>
                                        <hr />
                                        <div class="col-md-9 col-xs-12">
                                                <div class="item-info">
                                                    <label class="lab-cust">
                                                        اسم الشركة
                                                    </label>

                                                    <asp:TextBox ID="txtCompanyName" CssClass="form-control mid-screen xs-screen" onchange="changeborderColorafterfill(this)" ValidationGroup="2" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="item-info">
                                                    <label class="lab-cust">
                                                        المسئول
                                                    </label>

                                                    <asp:TextBox ID="txtResponsibility" CssClass="form-control mid-screen xs-screen" onchange="changeborderColorafterfill(this)" ValidationGroup="2" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="item-info">
                                                    <label class="lab-cust"> تليفون</label>

                                                    <asp:TextBox ID="txtTelephone" CssClass="form-control mid-screen xs-screen" onchange="changeborderColorafterfill(this)" ValidationGroup="2" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="item-info">
                                                    <label></label>

                                                    <asp:TextBox ID="txtTelephone1" CssClass="form-control mid-screen xs-screen" onchange="changeborderColorafterfill(this)" ValidationGroup="2" runat="server"></asp:TextBox>

                                                </div>
                                                <div class="item-info">
                                                    <label></label>

                                                    <asp:TextBox ID="txtTelephone2" CssClass="form-control mid-screen xs-screen" onchange="changeborderColorafterfill(this)" ValidationGroup="2" runat="server"></asp:TextBox>

                                                </div>

                                        <div class="col-md-12 non-padding">
                                            <div class="submit">
                                                <asp:Button ID="btnSave" CssClass="btn" runat="server" Text="حفظ / Save" ValidationGroup="1" OnClientClick="customValidations('1')" OnClick="btnSave_Click"  />


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


