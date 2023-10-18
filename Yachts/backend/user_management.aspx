<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="user_management.aspx.cs" Inherits="Yachts.backend.user_management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="active" runat="server" Text="User_List" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header">
            <h4>使用者管理</h4>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card-block">
        <div class="form-group" style="width: 700px">
            <label for="exampleSelect1" class="form-control-label" style="display: block">使用者帳號：</label>
            <asp:Label ID="lbl_user_account" runat="server"></asp:Label>
            <asp:TextBox ID="txt_user_account" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="form-group" style="width: 700px">
            <label for="exampleSelect1" class="form-control-label" style="display: block">使用者性別：</label>
            <asp:Label ID="lbl_user_sex" runat="server" Font-Size="Large"></asp:Label>
            <asp:TextBox ID="txt_user_sex" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="form-group" style="width: 700px">
            <label for="exampleSelect1" class="form-control-label" style="display: block">使用者信箱：</label>
            <asp:Label ID="lbl_user_mail" runat="server"></asp:Label>
            <asp:TextBox ID="txt_user_mail" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="form-group" style="width: 700px">
            <label for="exampleSelect1" class="form-control-label" style="display: block">使用者層級：</label>
            <asp:Label ID="lbl_user_rank" runat="server"></asp:Label>
            <asp:TextBox ID="txt_user_rank" runat="server" Visible="false"></asp:TextBox>
        </div>
        <label class="form-control-label">權限列表：</label>
        <asp:CheckBoxList ID="chkls_right_name" runat="server" Enabled="false" ></asp:CheckBoxList>
        <asp:Button ID="btn_startEdit" runat="server" Text="開始編輯" class="btn btn-success waves-effect waves-light m-r-30" OnClick="btn_startEdit_Click" />
        <asp:Button ID="btn_cancelEdit" runat="server" Text="取消編輯" class="btn btn-success waves-effect waves-light" Visible="false" OnClick="btn_cancelEdit_Click" />
        <hr />
        <asp:Button ID="btn_goUserList" runat="server" Text="返回使用者清單" class="btn btn-success waves-effect waves-light" OnClick="btn_goUserList_Click" />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
</asp:Content>
