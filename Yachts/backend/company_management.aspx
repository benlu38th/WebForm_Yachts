<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="company_management.aspx.cs" Inherits="Yachts.pages.B_company_management" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="active" runat="server" Text="Company" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header d-flex " style="justify-content: space-between">
            <h4>特色說明</h4>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card-block">
        <div class="form-group" style="width: 700px">
            <label for="exampleSelect1" class="form-control-label" style="display: block">特色標題：</label>
            <asp:Label ID="lbl_title_name" runat="server" Text="Label"></asp:Label>
        </div>

        <div class="form-group" style="width: 700px">
            <label for="exampleSelect1" class="form-control-label" style="display: block">特色內容：</label>
            <asp:Label ID="lbl_title_des" runat="server" Text="Label"></asp:Label>
            <CKEditor:CKEditorControl ID="cke_title_des" runat="server" class="form-control" BasePath="/Scripts/ckeditor/" Visible="false"></CKEditor:CKEditorControl>
        </div>

        <asp:Button ID="Button1" runat="server" Text="開始編輯" class="btn btn-success waves-effect waves-light m-r-30" OnClick="Button1_Click1" />

        <asp:Button ID="Button2" runat="server" Text="返回" class="btn btn-success waves-effect waves-light " OnClick="Button2_Click" />

    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
</asp:Content>
<asp:Content ID="TopAndLeft" ContentPlaceHolderID="TopAndLeft" runat="server">
    <p style="text-align: right; padding-right: 24px; padding-top: 5px; font-size: 13px">
        <asp:LinkButton ID="lbtn_goCompany" runat="server" OnClick="lbtn_goCompany_Click" Style="font-size: 13px; color: inherit;">公司特色</asp:LinkButton>
        >>
        <asp:LinkButton  runat="server" Style="font-size: 13px;">特色管理</asp:LinkButton>
    </p>
</asp:Content>