<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="company.aspx.cs" Inherits="Yachts.pages.B_company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="active" runat="server" Text="Company" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header d-flex " style="justify-content: space-between">
            <h4>公司特色</h4>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%-- 下面隱藏 --%>
    <asp:Label ID="Label2" runat="server" Text="Label" CssClass="btn btn-warning  waves-light" Visible="false">新增標題： 
        <asp:TextBox ID="txt_input_title" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="確定標題" OnClick="Button1_Click1" OnClientClick="return confirm('您確定要新增這個標題嗎？');" />
    </asp:Label>
    <%-- 上面隱藏 --%>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="title_id" Style="display: flex; flex-direction: column; justify-content: center; align-items: center; border: 0; margin-top: 20px;" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="流水號" SortExpression="CategoryName">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="標題名稱" SortExpression="標題名稱">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_title_name" runat="server" Text='<%# Bind("title_name") %>' Width="120px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_title_name" runat="server" Text='<%# Bind("title_name") %>' Width="120px" OnClick="lbtn_title_name_Click">LinkButton</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="130px">
                <EditItemTemplate>
                    <asp:LinkButton ID="lbtn_update"  Width="60px"  runat="server" CausesValidation="True" CommandName="Update" Text="更新" class="btn btn-info waves-effect waves-light"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="lbtn_cancel"  Width="60px"  runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" class="btn btn-info waves-effect waves-light"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbl_edit" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯" class="btn btn-info waves-effect waves-light"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_delete" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('您確定要刪除這個標題嗎？');" class="btn btn-danger waves-effect waves-light"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

</asp:Content>
<asp:Content ID="TopAndLeft" ContentPlaceHolderID="TopAndLeft" runat="server">
    <p style="text-align: right; padding-right: 24px; padding-top: 5px; font-size: 13px">
        <asp:LinkButton ID="lbtn_goCountries" runat="server" Style="font-size: 13px;">公司特色</asp:LinkButton>
<%--        >>
        <asp:LinkButton ID="lbtn_goAreas" runat="server" OnClick="lbtn_goAreas_Click" Style="font-size: 13px;">地區管理</asp:LinkButton>--%>
    </p>
</asp:Content>