<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="Add-Pruches.aspx.cs" Inherits="Add_Pruches" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Style/Add-Cat.css" rel="stylesheet" />
    <link href="Style/style.css" rel="stylesheet" />
    <script src="js/inputvalidations.js"></script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="tabs-cus-Sup clearfix">

                <div class="container-fluid">
                    <div class="title-hav clearfix">
                        <h2 class="title fix-bg">فاتورة مشتريات</h2>
                    </div>
                    <div class="container-fluid">
                        <div class=" mainfix clearfix">
                            <div class="item-info">
                                <label class="laborder">الكود</label>
                                <asp:TextBox ID="TxtCode" Enabled="false" CssClass="form-control mid-screen xs-screen" runat="server"></asp:TextBox>


                                <label class="laborder">المورد</label>
                                <asp:DropDownList ID="drpSupply" CssClass="form-control mid-screen xs-screen" AutoPostBack="true" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="2"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="drpSupply" ErrorMessage="" ValidationGroup="2"></asp:RequiredFieldValidator>
                            </div>
                            <div class="item-info">
                                <label class="laborder">المخزن</label>
                                <asp:DropDownList ID="drpStore" CssClass="form-control mid-screen xs-screen" AutoPostBack="true" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1" OnSelectedIndexChanged="drpStore_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="اختار المخزن" ControlToValidate="drpStore" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>

                                <label class="laborder">التاريخ</label>
                                <asp:TextBox ID="txtDate" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="datetimesetting" runat="server" TargetControlID="txtDate" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDate" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </div>
                            <div class="item-info">
                                <div id="Div1" dir="rtl" runat="server" style="height: 200px; width: 100%; left: 5%; right: 5%; margin: 2em auto; overflow-x: scroll; overflow-y: scroll">
                                    <table id="tabelLoad" style="width: 100%; border: solid">
                                        <tr style="background-color: #7ba4e5; color: #fff; height: 35px; font-size: 20px; font-weight: bold">
                                            <td style="width:20%">الباركود</td>
                                            <td>اسم الصنف</td>
                                            <td style="width:8%">الكمية</td>
                                            <td style="width:15%;font-size:18px">تاريخ الصلاحية</td>
                                            <td>سعر الشراء</td>
                                            <td style="width:8%">الاجمالي</td>
                                            <td>النوع</td>
                                            <td></td>
                                        </tr>
                                        <tr style="border-style:solid">
                                            <td style="width:20%">
                                                <asp:TextBox ID="txtBarCode" Style="width: 60%" runat="server" AutoPostBack="true" onchange="changeborderColorafterfill(this)" ValidationGroup="3" OnTextChanged="txtBarCode_TextChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBarCode" ErrorMessage="" ValidationGroup="3"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpName" Style="width: 100%" AutoPostBack="true" onchange="changeborderColorafterfill(this)" ValidationGroup="3" runat="server" OnSelectedIndexChanged="drpName_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="اختار الصنف" ControlToValidate="drpName" ErrorMessage="" ValidationGroup="3"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width:8%">
                                                <asp:TextBox ID="txtQuantity" Style="width: 60%" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="3" AutoPostBack="true" OnTextChanged="txtQuantity_TextChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQuantity" ErrorMessage="" ValidationGroup="3"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width:15%">
                                                <asp:TextBox ID="txtDateOfItem" Style="width: 60%" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="2"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateOfItem" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtDateOfItem" ErrorMessage="" ValidationGroup="2"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtprice" Style="width: 60%" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="3" AutoPostBack="true" OnTextChanged="txtprice_TextChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtprice" ErrorMessage="" ValidationGroup="3"></asp:RequiredFieldValidator>
                                            </td>
                                            <td style="width:8%">
                                                <asp:TextBox ID="txtTotal" Style="width: 60%" runat="server" Enabled="false" onchange="changeborderColorafterfill(this)" ValidationGroup="3"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtTotal" ErrorMessage="" ValidationGroup="3"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="drpType" Style="width: 100%" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="3" AutoPostBack="true" OnSelectedIndexChanged="drpType_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="drpType" ErrorMessage="" ValidationGroup="3"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="LinkAdd" ValidationGroup="3" OnClientClick="customValidations('3')" runat="server" OnClick="LinkAdd_Click">اضافة</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <% 
                                            databaseaccesslayer dataobj = new databaseaccesslayer();
                                            string sql = @"select i.BarCode as BarCode,i.ItemName as Name,o.Quantity as Num,Price,Type,Date from [dbo].[tempPrucheDetails] o left join Items i on o.Item_Id=i.Code where [PrucheCode]='" + TxtCode.Text + "'";
                                            System.Data.DataTable dt = dataobj.Selectdatatable(sql);
                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {%>
                                        <tr style="background-color:#dbd9d9;font-size:20px;border-style:solid">
                                            <td style="width:20%"><%=dt.Rows[i]["BarCode"].ToString() %></td>
                                            <td><%=dt.Rows[i]["Name"].ToString() %></td>
                                            <td style="width:8%"><%=dt.Rows[i]["Num"].ToString() %></td>
                                            <%string sd = "";
                                                try
                                              {
                                                  DateTime dd = DateTime.Parse(dt.Rows[i]["Date"].ToString());  sd = dd.ToShortDateString();
                                              }
                                              catch (Exception) { sd = ""; } %>
                                            <td style="width:15%"><%=sd %></td>
                                            <td><%=dt.Rows[i]["Price"].ToString() %></td>
                                            <td style="width:8%"><%=float.Parse(dt.Rows[i]["Price"].ToString())*float.Parse(dt.Rows[i]["Num"].ToString()) %></td>
                                            <td><%=dt.Rows[i]["Type"].ToString() %></td>
                                              <td><a id="LinkEdit"  onclick="myFunctionEdit(<%=i%>)">Edit</a>&nbsp;&nbsp;&nbsp<a id="LinkDel"  onclick="myFunction(<%=i%>)">Delete</a></td>
                                        </tr>
                                        <%} %>
                                    </table>
                                </div>
                            </div>

                            <div class="item-info">
                                <label class="laborder">اضافة</label>
                                <asp:TextBox ID="txtAdd" Style="width: 100px" runat="server" Text="0" AutoPostBack="true" onchange="changeborderColorafterfill(this)" ValidationGroup="1" OnTextChanged="txtAdd_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtAdd" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <label class="laborder">خصم</label>
                                <asp:TextBox ID="txtDiscount" Style="width: 100px" Text="0" runat="server" AutoPostBack="true" onchange="changeborderColorafterfill(this)" ValidationGroup="1" OnTextChanged="txtDiscount_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDiscount" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <label class="laborder">دفع</label>
                                <asp:TextBox ID="txtPay" runat="server" Text="0" Style="width: 100px" AutoPostBack="true" onchange="changeborderColorafterfill(this)" ValidationGroup="1" OnTextChanged="txtPay_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtPay" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>

                                <label class="laborder">باقي</label>
                                <asp:TextBox ID="txtDebt" runat="server" Text="0" Style="width: 100px" Enabled="false" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtDebt" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </div>
                            <div class="item-info" dir="ltr">
                                <label class="laborder">الاجمالي</label>
                                <asp:Label ID="LabTotal" Text="0" runat="server"></asp:Label>
                            </div>
                            <div class="item-info">
                                <label class="lab-notes labwidth">ملاحظات</label>
                                <asp:TextBox ID="txtNotes" TextMode="MultiLine" Width="71%" Height="100px" CssClass="in-tabs form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="item-info">
                                        <asp:Button ID="btnSave" CssClass="btn" runat="server" Text="حفظ / Save" ValidationGroup="1" OnClientClick="customValidations('1')" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnReport" CssClass="btn" runat="server" Text="حفظ وتقرير" ValidationGroup="1" OnClientClick="customValidations('1')" OnClick="btnReport_Click"  />
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

