<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="dealers.aspx.cs" Inherits="Yachts.pages.B_dealers" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="active" runat="server" Text="DealersDetail" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="TopAndLeft" ContentPlaceHolderID="TopAndLeft" runat="server">
    <p style="text-align: right; padding-right: 24px; padding-top: 5px; font-size: 13px">
        <asp:LinkButton ID="lbtn_goDealers" runat="server" OnClick="lbtn_goDealers_Click" Style="font-size: 13px;">代理商管理</asp:LinkButton>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header d-flex " style="justify-content: space-between">
            <h4>代理商管理</h4>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="form-group" style="margin-top: 10px; display: flex; flex-direction: column; align-items: center;">
        <div style="display: flex; flex-direction: column; align-items: flex-start">
            <label for="lbl_selectCountry" class="form-control-label">選擇國家</label>
            <asp:DropDownList ID="ddl_country_name" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_country_name_SelectedIndexChanged" Width="300px">
                <asp:ListItem Selected="true" Value="0">--請選擇一個國家--</asp:ListItem>
            </asp:DropDownList>
            <label for="lbl_selectArea" class="form-control-label">選擇地區</label>
            <asp:DropDownList ID="ddl_area_name" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_area_name_SelectedIndexChanged" Width="300px">
                <asp:ListItem Selected="true" Value="0">--請先選擇一個國家--</asp:ListItem>
            </asp:DropDownList>
            <label for="lbl_selectCountry" class="form-control-label m-t-15">代理商列表：</label>
        </div>
    </div>
    <asp:Button ID="btn_addDealer" runat="server" Text="新增代理商" OnClick="btn_addDealer_Click" class="btn btn-success waves-effect waves-light" />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="dealer_id" OnRowDeleting="GridView1_RowDeleting" Style="display: flex; flex-direction: column; justify-content: space-between; align-items: center; border: 0; margin-top: 15px;" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
        <Columns>
            <asp:TemplateField SortExpression="CategoryName" HeaderStyle-CssClass="text-center">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="照片" SortExpression="dealer_des" HeaderStyle-CssClass="text-center">
                <EditItemTemplate>
                    <asp:FileUpload ID="ful_dealer_photoPath" runat="server" Font-Size="XX-Small" Style="white-space: normal; height: 200px;" Width="100px" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("dealer_photoPath") %>' Width="100px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="地區" SortExpression="dealer_des " HeaderStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:Label ID="lbl_area_name" Width="100%" runat="server" Text='<%# Eval("area_name").ToString()%>' Font-Size="XX-Small"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="代理商" SortExpression="dealer_des " HeaderStyle-CssClass="text-center">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_dealer_name" runat="server" Text='<%# Eval("dealer_name").ToString()%>' Width="100%" Font-Size="XX-Small"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label OnClick="lbtn_dealer_details_Click123" ID="lbl_dealer_name" Width="100%" runat="server" Text='<%# Eval("dealer_name").ToString()%>' Font-Size="XX-Small"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="聯絡人" SortExpression="dealer_des" HeaderStyle-CssClass="text-center">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_dealer_contact" Width="100%" runat="server" Text='<%# Eval("dealer_contact").ToString()%>' Font-Size="X-Small"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_dealer_contact" Width="100%" runat="server" Text='<%# Eval("dealer_contact").ToString()%>' Font-Size="XX-Small"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="地址" SortExpression="dealer_address" HeaderStyle-CssClass="text-center">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_dealer_address" Width="100%" Height="100px" runat="server" Text='<%# Eval("dealer_address").ToString()%>' TextMode="MultiLine" Font-Size="XX-Small"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_dealer_address" Width="100%" runat="server" Text='<%# Eval("dealer_address").ToString()%>' Font-Size="XX-Small"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="電話" SortExpression="dealer_tel" HeaderStyle-CssClass="text-center">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_dealer_tel" runat="server" Text='<%# Eval("dealer_tel").ToString()%>' Width="100%" Font-Size="XX-Small"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_dealer_tel" Width="100%" runat="server" Text='<%# Eval("dealer_tel").ToString()%>' Font-Size="XX-Small"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="傳真" SortExpression="dealer_des">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_dealer_fax" Width="100%" runat="server" Text='<%# Eval("dealer_fax").ToString()%>' Font-Size="XX-Small"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_dealer_fax" Width="100%" runat="server" Text='<%# Eval("dealer_fax").ToString()%>' Font-Size="XX-Small"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="手機" SortExpression="dealer_des">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_dealer_cell" Width="100%" runat="server" Text='<%# Eval("dealer_cell").ToString()%>' Font-Size="XX-Small"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_dealer_cell" Width="100%" runat="server" Text='<%# Eval("dealer_cell").ToString()%>' Font-Size="XX-Small"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="信箱" SortExpression="dealer_des">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_dealer_mail" Width="100%" Height="100px" runat="server" Text='<%# Eval("dealer_mail").ToString()%>' Font-Size="XX-Small" TextMode="MultiLine"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_dealer_mail" Width="100%" runat="server" Text='<%# Eval("dealer_mail").ToString()%>' Font-Size="XX-Small"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="網站" SortExpression="dealer_des">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_dealer_website" Width="100%" Height="100px" runat="server" Text='<%# Eval("dealer_website").ToString()%>' Font-Size="XX-Small" TextMode="MultiLine"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_dealer_webdite" Width="100%" runat="server" Text='<%# Eval("dealer_website").ToString()%>' Font-Size="XX-Small"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <EditItemTemplate>
                    <asp:LinkButton ID="lbtn_update" runat="server" CausesValidation="True" CommandName="Update" Text="更新" Font-Size="XX-Small"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="lbtn_cancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" Font-Size="XX-Small"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_edit" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯" Font-Size="XX-Small"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_delete" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('您確定要刪除這家代理商嗎？');" Font-Size="XX-Small"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <%--    <hr />
    <div style="display: flex; justify-content: space-around; margin-bottom: 20px">
        <asp:Button ID="goDealers_countries" runat="server" Text="國家管理" class="btn btn-success waves-effect waves-light" OnClick="goDealers_countries_Click" />
        <asp:Button ID="goDealers_areas" runat="server" Text="地區管理(新增代理商)" class="btn btn-success waves-effect waves-light" OnClick="goDealers_areas_Click" />
        <asp:Button ID="goDealers" runat="server" Text="代理商管理" class="btn btn-success waves-effect waves-light" OnClick="goDealers_Click" />
    </div>--%>
</asp:Content>
