<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="Add-Store.aspx.cs" Inherits="Add_Store" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Style/style.css" rel="stylesheet" />
     <script src="js/inputvalidations.js"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
            <section class="add-store clearfix">
                <div class="container-fluid">
                    <div class="title-hav clearfix">
                        <h2 class="title fix-bg">اضافة مخزن</h2>
                    </div>
                    <p class="Head">بيانات أساسية</p>
                    <hr />
                    <div class="col-md-7 col-xs-12">
                        <div class="item-info">
                            <label>كود المخزن</label>

                            <asp:TextBox ID="txtStoreNumber" CssClass="store-tabs form-control " runat="server" Enabled="false"  onchange="changeborderColorafterfill(this)" ValidationGroup="1"                       ></asp:TextBox>  
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStoreNumber" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="item-info">
                            <label>اسم المخزن</label>

                            <asp:TextBox ID="txtARStoreName" CssClass="store-tabs form-control" runat="server"  onchange="changeborderColorafterfill(this)" ValidationGroup="1"                       ></asp:TextBox>  
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtARStoreName" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                          </div>
                        <div class="item-info">
                            <label>رقم الهاتف</label>
                            <asp:TextBox ID="txtPhone" CssClass="store-tabs form-control" runat="server"  onchange="changeborderColorafterfill(this)" ValidationGroup="1"                       ></asp:TextBox>  
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPhone" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </div>
                        <div class="item-info">
                                <label>الحالة</label>
                                <div class="re-chk">

                                    <asp:CheckBox ID="chkActive" CssClass="btn-chk" runat="server" />
                                    <p class="p-chk p-po">نــشط</p>
                                </div>
                            </div>
                        </div>
                    </div>
                        <p class="Head">بيانات اضافية</p>
                        <hr />
                <div class="col-md-7 col-xs-12">
                        <div class="item-info">
                            <label>المسئول</label>
                            <asp:TextBox ID="txtARManager" CssClass="store-tabs form-control" runat="server"  onchange="changeborderColorafterfill(this)" ValidationGroup="1"                       ></asp:TextBox>  
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtARManager" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </div>
                            
                            <div class="item-info">
                                <label>الموبيل</label>

                                <asp:TextBox ID="txtMobile" CssClass="store-tabs form-control" runat="server"  onchange="changeborderColorafterfill(this)" ValidationGroup="2"                       ></asp:TextBox>  
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMobile" ErrorMessage="" ValidationGroup="2"></asp:RequiredFieldValidator>

                            </div>
                            <div class="item-info">
                                <label>عنوان المخزن</label>

                                <asp:TextBox ID="txtARAddress" CssClass="store-tabs form-control" runat="server"  onchange="changeborderColorafterfill(this)" ValidationGroup="2"                       ></asp:TextBox>  
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtARAddress" ErrorMessage="" ValidationGroup="2"></asp:RequiredFieldValidator>

                                <div class="submit-rev-add">

                          
                                    <asp:Button ID="btnSave" CssClass="btn" runat="server" Text="حفظ / Save"  ValidationGroup="1" OnClientClick="customValidations('1');" OnClick="btnSave_Click" />

                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


