<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="dealers_countries.aspx.cs" Inherits="Yachts.pages.B_dealers_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="active" runat="server" Text="Dealers" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="TopAndLeft" ContentPlaceHolderID="TopAndLeft" runat="server">
    <p style="text-align: right; padding-right: 24px; padding-top: 5px;">
        <asp:LinkButton ID="lbtn_goCountries" runat="server" OnClick="lbtn_goCountries_Click" Style="font-size: 13px;">國家管理</asp:LinkButton>
    </p>
</asp:Content>
<asp:Content ID="MainHeader" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header">
            <h4>國家管理</h4>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: flex; justify-content: flex-start;">
        <div style="display: flex; justify-content: center; align-items: center;">
            <label for="lbl_selectCountry" class="form-control-label">新增國家：</label>
        </div>
        <asp:TextBox ID="txt_input_country" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="確定新增" OnClick="Button1_Click" OnClientClick="return confirm('您確定要新增這個國家嗎？');" class="btn btn-success waves-effect waves-light" Style="margin-left: 10px" />
    </div>
    <div style="margin-top: 10px; margin-bottom: -20px;">
        <label for="lbl_selectCountry" class="form-control-label">國家列表：</label>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="country_id" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" Style="display: flex; flex-direction: column; justify-content: flex-start; align-items: center; border: 0; margin-top: 20px;">
        <Columns>
            <asp:TemplateField SortExpression="CategoryName" HeaderStyle-Width="20px">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="國家名稱" SortExpression="country_name" HeaderStyle-CssClass="text-center">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_country_name" runat="server" Text='<%# Bind("country_name") %>' Width="120px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_areaManagement" runat="server" CausesValidation="false" CommandName="" OnClick="lbtn_areaManagement_Click" Text='<%# Bind("country_name") %>' Width="120px"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" HeaderStyle-Width="130px">
                <EditItemTemplate>
                    <asp:LinkButton ID="lbtn_update" runat="server" CausesValidation="True" CommandName="Update" Text="更新" class="btn btn-info waves-effect waves-light" Width="60px"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" class="btn btn-info waves-effect waves-light" Width="60px"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯" class="btn btn-info waves-effect waves-light" Width="60px"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="70px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('您確定要刪除這個國家嗎？');" class="btn btn-danger waves-effect waves-light"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>


</asp:Content>

