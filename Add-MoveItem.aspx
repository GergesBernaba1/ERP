<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="Add-MoveItem.aspx.cs" Inherits="Add_MoveItem" %>

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
                        <h2 class="title fix-bg">تحويلات المخازن</h2>
                    </div>
                    <div class="container-fluid">
                        <div class=" mainfix clearfix">
                            <div class="item-info">
                                <label class="laborder">الكود</label>
                                <asp:TextBox ID="TxtCode" Enabled="false" CssClass="form-control mid-screen xs-screen" runat="server"></asp:TextBox>
                                <label class="laborder">التاريخ</label>
                                <asp:TextBox ID="txtDate" CssClass="form-control mid-screen xs-screen" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="datetimesetting" runat="server" TargetControlID="txtDate" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDate" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                            </div>
                            <div class="item-info">
                                <label class="laborder">من مخزن</label>
                                <asp:DropDownList ID="drpStore" CssClass="form-control mid-screen xs-screen" AutoPostBack="true" runat="server" ValidationGroup="1" OnSelectedIndexChanged="drpStore_SelectedIndexChanged" ></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="اختار المخزن المحول منه" ControlToValidate="drpStore" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                                <label class="laborder">الي مخزن</label>
                                <asp:DropDownList ID="drpStoreto" CssClass="form-control mid-screen xs-screen" AutoPostBack="true" runat="server" ValidationGroup="1" OnSelectedIndexChanged="drpStoreto_SelectedIndexChanged" ></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValue="اختار المخزن المحول اليه" ControlToValidate="drpStoreto" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                                
                            </div>
                            <div class="item-info">
                                <div id="Div1" dir="rtl" runat="server" style="height: 200px; width: 100%; left: 5%; right: 5%; margin: 2em auto; overflow-x: scroll; overflow-y: scroll">
                                    <table id="tabelLoad" style="width: 100%; border: solid">
                                        <thead>
                                            <tr style="background-color: #7ba4e5; color: #fff; height: 35px; font-size: 20px; font-weight: bold">
                                                <td style="width:25%">الباركود</td>
                                                <td>اسم الصنف</td>
                                                <td style="width:15%">الكمية</td>
                                                <td style="width:20%">النوع</td>
                                                <td></td>
                                            </tr>
                                            <tr style="border-style:solid">
                                                <td style="width:25%">
                                                    <asp:TextBox ID="txtBarCode" Style="width: 60%" runat="server" AutoPostBack="true" OnTextChanged="txtBarCode_TextChanged" onchange="changeborderColorafterfill(this)" ValidationGroup="3" ></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBarCode" ErrorMessage="" ValidationGroup="3"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="drpName" Style="width: 100%" AutoPostBack="true" OnSelectedIndexChanged="drpName_SelectedIndexChanged" onchange="changeborderColorafterfill(this)" ValidationGroup="3" runat="server" ></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="اختار الصنف" ControlToValidate="drpName" ErrorMessage="" ValidationGroup="3"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width:15%">
                                                    <asp:TextBox ID="txtQuantity" Style="width: 60%" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="3"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQuantity" ErrorMessage="" ValidationGroup="3"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width:20%">
                                                    <asp:DropDownList ID="drpType" Style="width: 100%" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="3"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="drpType" ErrorMessage="" ValidationGroup="3"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LinkAdd"  ValidationGroup="3" OnClientClick="getItems()" OnClick="LinkAdd_Click" runat="server" >اضافة</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody id="bodytable">
                                        
                                        <% 
                                            databaseaccesslayer dataobj = new databaseaccesslayer();
                                            string sql = @"select i.BarCode as BarCode,i.ItemName as Name,o.Quantity as Num,Type from tempMoveItemStores o left join Items i on o.ItemCode=i.Code where MoveCode='" + TxtCode.Text + "'";
                                            System.Data.DataTable dt = dataobj.Selectdatatable(sql);
                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {%>
                                        <tr style="background-color:#dbd9d9;font-size:20px;border-style:solid">
                                            <td style="width:25%"><%=dt.Rows[i]["BarCode"].ToString() %></td>
                                            <td><%=dt.Rows[i]["Name"].ToString() %></td>
                                            <td style="width:15%"><%=dt.Rows[i]["Num"].ToString() %></td>
                                            <td style="width:20%"><%=dt.Rows[i]["Type"].ToString() %></td>
                                            
                                            <td><a id="LinkEdit"  onclick="myFunctionEdit(<%=i%>)">Edit</a>&nbsp;&nbsp;&nbsp<a id="LinkDel"  onclick="myFunction(<%=i%>)">Delete</a></td>
                                            <%--<td><a id="LinkEdit"  onclick="EditOrder('<%=dt.Rows[i]["Name"].ToString() %>','<%=dt.Rows[i]["Num"].ToString() %>','<%=dt.Rows[i]["Type"].ToString() %>')">Edit</a>&nbsp;&nbsp;&nbsp<a id="LinkDel" onclick="DeleteOrder('<%=dt.Rows[i]["Name"].ToString() %>','<%=dt.Rows[i]["Num"].ToString() %>','<%=dt.Rows[i]["Type"].ToString() %>')">Delete</a></td>--%>
                                            <%--<td>
                                                <asp:LinkButton ID="LinkEdit" runat="server" >Edit</asp:LinkButton>&nbsp;&nbsp;&nbsp<asp:LinkButton ID="LinkDel" runat="server" OnClick="LinkDel_Click" >Delete</asp:LinkButton></td>--%>
                                        </tr>
                                        <%} %>
                                            </tbody>
                                    </table>
                                </div>
                            </div>
                            <div class="item-info">
                                <label class="lab-notes labwidth">ملاحظات</label>
                                <asp:TextBox ID="txtNotes" TextMode="MultiLine" Width="71%" Height="100px" CssClass="in-tabs form-control" runat="server"></asp:TextBox>
                            </div>
                            <div class="item-info">
                                <div class="col-md-12 non-padding">
                                    <div class="submit">
                                        <asp:Button ID="btnSave" CssClass="btn" runat="server" Text="حفظ / Save" ValidationGroup="1" OnClientClick="customValidations('1')" OnClick="btnSave_Click" />
                                    </div>
                                </div>
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
