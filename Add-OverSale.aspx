<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="Add-OverSale.aspx.cs" Inherits="Add_OverSale" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Style/Add-Cat.css" rel="stylesheet" />
    <link href="Style/style.css" rel="stylesheet" />
    <script src="js/inputvalidations.js"></script>

    <%-- Scan --%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="tabs-cus-Sup clearfix">

                <div class="container-fluid">
                    <div class="title-hav clearfix">
                        <h2 class="title fix-bg">أضافة عروض مبيعات</h2>
                    </div>
                    <p class="Head">بيانات اساسية</p>
                    <hr />
                    <div class="row">
                        <div class="mainfix clearfix">
                            <div class="col-xs-12">
                                <div class="main-info clearfix">
                                    <div class="item-info">
                                        <label class="laborder">الكود</label>
                                        <asp:TextBox ID="TxtCode" Enabled="false" CssClass="form-control mid-screen xs-screen" runat="server"></asp:TextBox>
                                        <label class="labsel">المخزن</label>
                                        <asp:DropDownList ID="drpStore" CssClass="form-control mid-screen xs-screen" AutoPostBack="true" OnTextChanged="drpStore_TextChanged" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="اختار المخزن" ControlToValidate="drpStore" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="item-info">
                                        <label class="labsel">الاسم</label>
                                        <asp:TextBox ID="txtNameOver" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNameOver" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <label class="labsel">المبلغ</label>
                                        <asp:TextBox ID="txtAmount" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAmount" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="item-info">
                                        <label class="labsel">الفترة من</label>
                                        <asp:TextBox ID="txtDateFrom" CssClass="form-control mid-screen xs-screen" onchange="changeborderColorafterfill(this)" ValidationGroup="1" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="datetimesetting" runat="server" TargetControlID="txtDateFrom" />
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDateFrom" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        <label class="labsel">الي</label>
                                        <asp:TextBox ID="txtDateTo" CssClass="form-control mid-screen xs-screen" onchange="changeborderColorafterfill(this)" ValidationGroup="1" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateTo" />
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtDateTo" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <br />
                                <p class="Head">الاصناف</p>
                                <hr />
                                <div id="Div1" dir="rtl" runat="server" style="height: 200px; width: 100%; left: 5%; right: 5%; margin: 2em auto; overflow-x: scroll; overflow-y: scroll">
                                    <table id="tabelLoad" style="width: 100%; border: solid">
                                        <tr style="background-color: #7ba4e5; color: #fff; height: 35px; font-size: 20px; font-weight: bold">
                                            <td style="width:25%">الباركود</td>
                                            <td>اسم الصنف</td>
                                            <td style="width:12%">الكمية</td>
                                            <td style="width:20%">النوع</td>
                                            <td></td>
                                        </tr>
                                        <tr style="border-style: solid">
                                            <td style="width:25%">
                                                <asp:TextBox ID="txtBarCode" Style="width: 60%" runat="server" AutoPostBack="true" onchange="changeborderColorafterfill(this)" ValidationGroup="3" OnTextChanged="txtBarCode_TextChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtBarCode" ErrorMessage="" ValidationGroup="3"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpName" Style="width: 100%" AutoPostBack="true" onchange="changeborderColorafterfill(this)" ValidationGroup="3" runat="server" OnSelectedIndexChanged="drpName_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="اختار الصنف" ControlToValidate="drpName" ErrorMessage="" ValidationGroup="3"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width:12%">
                                                <asp:TextBox ID="txtQuantity" Style="width: 60%" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="3" AutoPostBack="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtQuantity" ErrorMessage="" ValidationGroup="3"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width:20%">
                                                <asp:DropDownList ID="drpType" Style="width: 100%" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="3"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="drpType" ErrorMessage="" ValidationGroup="3"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkAdd" ValidationGroup="3" OnClientClick="customValidations('3')" runat="server" OnClick="LinkAdd_Click">اضافة</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <% 
                                            databaseaccesslayer dataobj = new databaseaccesslayer();
                                            string sql = @"select i.BarCode as BarCode,i.ItemName as Name,o.Quantity as Num,Type from [dbo].[tempOverSale] o left join Items i on o.Item_Id=i.Code where [OverCode]='" + TxtCode.Text + "'";
                                            System.Data.DataTable dt = dataobj.Selectdatatable(sql);
                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {%>
                                        <tr style="background-color: #dbd9d9; font-size: 20px; border-style: solid">
                                            <td style="width:25%"><%=dt.Rows[i]["BarCode"].ToString() %></td>
                                            <td><%=dt.Rows[i]["Name"].ToString() %></td>
                                            <td style="width:12%"><%=dt.Rows[i]["Num"].ToString() %></td>
                                            <td style="width:20%"><%=dt.Rows[i]["Type"].ToString() %></td>
                                            <td><a id="LinkEdit" onclick="myFunctionEdit(<%=i%>)">Edit</a>&nbsp;&nbsp;&nbsp<a id="LinkDel" onclick="myFunction(<%=i%>)">Delete</a></td>
                                        </tr>
                                        <%} %>
                                    </table>
                                </div>
                                <br />
                                <p class="Head">بيانات أضافية</p>
                                <hr />
                                <div class="item-info">
                                    <label class="lab-notes labwidth">ملاحظات</label>
                                    <asp:TextBox ID="txtNotes" TextMode="MultiLine" Width="71%" Height="100px" CssClass="in-tabs form-control" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-12 non-padding">
                                    <div class="submit">
                                        <asp:Button ID="btnSave" CssClass="btn" runat="server" Text="حفظ / Save" ValidationGroup="1" OnClientClick="customValidations('1')" OnClick="btnSave_Click" />


                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:TextBox ID="TextBox1" CssClass="txtvis" runat="server"></asp:TextBox>
                        <asp:Button ID="BtnDelConf" runat="server" CssClass="txtvis" Text="" OnClick="BtnDelConf_Click" />
                        <asp:Button ID="BtnEditConf" runat="server" CssClass="txtvis" Text="" OnClick="BtnEditConf_Click" />
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function myFunction(id) {
            $('#<%=TextBox1.ClientID%>').val(id);
             $('#<%=BtnDelConf.ClientID%>').click();
         }
         function myFunctionEdit(id) {
             $('#<%=TextBox1.ClientID%>').val(id);
            $('#<%=BtnEditConf.ClientID%>').click();
        }
    </script>
</asp:Content>
