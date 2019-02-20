<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterAR.master" AutoEventWireup="true" CodeFile="Update_MonthlyInstallments.aspx.cs" Inherits="Update_MonthlyInstallments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Style/style.css" rel="stylesheet" />
     <script src="js/inputvalidations.js"></script>

       <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
    <section class="add-InsMent clearfix">
        <div class="container-fluid">
            <div class="title-hav clearfix">
                <h2 class="title fix-bg">الاقساط الشهرية</h2>
            </div>
            <p class="Head">البيانات</p>
            <hr />
            <div class="col-md-0 col-xs-18">
                
                <div class="item-info">
                    <label>الرقم</label>
                    <asp:TextBox ID="txtCode" CssClass=" form-control mid-screen-b  xs-screen-b" Enabled="false" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1"></asp:TextBox>  
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCode" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="item-info">
                    <label>التاريخ</label>
                    <asp:TextBox ID="txtDate" CssClass=" form-control mid-screen-b  xs-screen-b" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1" TabIndex="1" ></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="datetimesetting" runat="server"  TargetControlID="txtDate"/>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDate" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>  
                </div>
                <div class="item-info">
                    <label>الاسم</label>
                    <asp:TextBox ID="txtName" CssClass="form-control mid-screen-b  xs-screen-b" runat="server"  onchange="changeborderColorafterfill(this)" ValidationGroup="1" TabIndex="3" ></asp:TextBox>  
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"  ControlToValidate="txtName" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                </div>
                <p class="Head">القسط</p>
            <hr /> 
                    <div class="item-info">
                        <label>المبلغ</label>
                        <asp:TextBox ID="txtAmount" CssClass=" form-control mid-screen-b  xs-screen-b" runat="server" AutoPostBack="true" OnTextChanged="txtAmount_TextChanged" onchange="changeborderColorafterfill(this)" ValidationGroup="1" TabIndex="4"></asp:TextBox>  
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtAmount" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="item-info">
                        <label>ربح القسط</label>
                        <asp:TextBox ID="txtWin" CssClass=" form-control mid-screen-b  xs-screen-b" runat="server" AutoPostBack="true" OnTextChanged="txtWin_TextChanged" onchange="changeborderColorafterfill(this)" ValidationGroup="1" TabIndex="5"></asp:TextBox>  
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtWin" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                        
                    </div>
                    <div class="item-info">
                        <label>مبلغ التقسيط</label>

                         <asp:Label ID="labTotal"  runat="server" Text="0"></asp:Label>
                    </div>

                </div>
                    <div class="item-info">
                        <label>عدد الاقساط</label>

                        <asp:TextBox ID="txtNum" CssClass=" form-control mid-screen-b  xs-screen-b" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="1" TabIndex="6" ></asp:TextBox> 
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtNum" ErrorMessage="" ValidationGroup="1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="item-info">
                        <label>البدء من الشهر</label>

                        <asp:DropDownList ID="drpMonth" CssClass=" form-control mid-screen-b  xs-screen-b" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="2" TabIndex="7" ></asp:DropDownList>   
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="drpMonth" ErrorMessage="" ValidationGroup="2"></asp:RequiredFieldValidator>
                        
                    </div>
                    <div class="item-info">
                        <label>يوم السداد</label>
                        <asp:DropDownList ID="drpDay" CssClass=" form-control mid-screen-b  xs-screen-b" runat="server" onchange="changeborderColorafterfill(this)" ValidationGroup="2" TabIndex="8" ></asp:DropDownList>   
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="drpDay" ErrorMessage="" ValidationGroup="2"></asp:RequiredFieldValidator>
                        <asp:Button ID="btnPuplish" CssClass="btn" runat="server"  Text="توزيع الاقساط" style="background-color:grey" OnClick="btnPuplish_Click" TabIndex="9"  />
                    </div>
             <div class="col-xs-12 non-padding">
                <div class="col-md-7 col-xs-12">
                    <asp:GridView ID="grdInstallment" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Bold="True" Width="342px">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                </div>
                
            </div>
            <div class="submit-rev-add">
                            <asp:Button ID="btnSave" CssClass="btn" runat="server" Text="حفظ / Save"  ValidationGroup="1" OnClientClick="customValidations('1')" OnClick="btnSave_Click" />
                           
                        </div>
        </div>
    </section>
            </ContentTemplate>
           </asp:UpdatePanel>
</asp:Content>


