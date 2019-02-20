<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Style/style.css" rel="stylesheet" />
    <title></title>
</head>
<body dir="rtl">
    <form id="form1" runat="server">
        <div>
            <section class="tabs-cus-Sup clearfix">

                <div class="container-fluid">
                    <div class="title-hav clearfix">
                        <h2 class="title fix-bg">صفحة التسجيل</h2>
                    </div>
                    <table style="width: 100%; height: 500px;">

                        <tr style="height: 100px">
                            <td style="width: 20%; height: 25%"></td>
                            <td style="width: 50%;">

                                <div class="item-info">
                                    <label>
                                        اسم المستخدم
                                    </label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtName" CssClass="form-control mid-screen-c xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>

                                </div>
                                <br />
                                <div class="item-info">
                                    <label class="login">
                                        كلمة المرور
                                    </label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control mid-screen-c xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword" ValidationGroup="1" ErrorMessage=""></asp:RequiredFieldValidator>

                                </div>
                                <br />
                                <div class="item-info">
                                    <div class="submit">
                                        <asp:Button ID="btnSave" CssClass="btn1" runat="server" Text="دخول / Login" ValidationGroup="1" OnClientClick="customValidations('1')" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                            </td>
                            <td style="width: 30%; height: 25%"></td>
                        </tr>
                        <tr style="height: 200px">
                            <td style="width: 20%;"></td>
                            <td style="width: 50%;"></td>
                            <td style="width: 30%;"></td>
                        </tr>
                    </table>
                </div>
            </section>
        </div>
    </form>
</body>
</html>
