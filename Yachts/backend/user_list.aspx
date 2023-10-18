<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="user_list.aspx.cs" Inherits="Yachts.pages.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="active" runat="server" Text="User_List" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="MainHeader" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header">
            <h4>使用者清單</h4>
        </div>
    </div>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="user_id" OnRowDataBound="GridView1_RowDataBound" HeaderStyle-BackColor="#39444e" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="white" OnRowDeleting="GridView1_RowDeleting">
        <Columns>
            <asp:TemplateField SortExpression="CategoryName" HeaderStyle-CssClass="text-center; p-10" HeaderStyle-Font-Size="Small" ItemStyle-Height="36px">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
                <HeaderStyle CssClass="text-center; p-10" Font-Size="Small"></HeaderStyle>
            </asp:TemplateField>
            <asp:BoundField DataField="user_account" HeaderText="帳號" SortExpression="user_account" HeaderStyle-CssClass="text-center; p-10" HeaderStyle-Font-Size="Small">
                <HeaderStyle CssClass="text-center; p-10" Font-Size="Small"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField DataField="user_sex" HeaderText="性別" SortExpression="user_sex" HeaderStyle-CssClass="text-center; p-10" HeaderStyle-Font-Size="Small">
                <HeaderStyle CssClass="text-center; p-10" Font-Size="Small"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField DataField="user_mail" HeaderText="信箱" SortExpression="user_mail" HeaderStyle-CssClass="text-center; p-10" HeaderStyle-Font-Size="Small">
                <HeaderStyle CssClass="text-center; p-10" Font-Size="Small"></HeaderStyle>
            </asp:BoundField>
            <asp:BoundField DataField="user_rank" HeaderText="層級" SortExpression="user_rank" HeaderStyle-CssClass="text-center; p-10" HeaderStyle-Font-Size="Small">
                <HeaderStyle CssClass="text-center; p-10" Font-Size="Small"></HeaderStyle>
            </asp:BoundField>
            <asp:TemplateField ShowHeader="False" HeaderStyle-CssClass="text-center; p-10" HeaderStyle-Font-Size="Small">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_user_management" runat="server" CausesValidation="false" CommandName="" Text="管理" OnClick="lbtn_user_management_Click" class="btn btn-success waves-effect waves-light" HeaderStyle-CssClass="text-center; p-10" HeaderStyle-Font-Size="Small"></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle CssClass="text-center; p-10" Font-Size="Small"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_delete" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" class="btn btn-danger waves-effect waves-light" HeaderStyle-CssClass="text-center; p-10" HeaderStyle-Font-Size="Small" OnClientClick="return confirm('您確定要刪除這個項目嗎？');"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle BackColor="#39444E" Font-Bold="True" ForeColor="White"></HeaderStyle>
    </asp:GridView>
</asp:Content>


