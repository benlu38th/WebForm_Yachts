<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="yatchs_specs.aspx.cs" Inherits="Yachts.backend.yatchs_specs" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="active" runat="server" Text="Yatchs" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="TopAndLeft" ContentPlaceHolderID="TopAndLeft" runat="server">
    <p style="text-align: right; padding-right: 24px; padding-top: 5px; font-size: 13px">
        <asp:LinkButton ID="lbtn_goYatchs" runat="server" OnClick="lbtn_goYatchs_Click" Style="font-size: 13px; color: inherit">遊艇清單</asp:LinkButton>
        >>
        <asp:LinkButton ID="lbtn_goYatchsSpecs" runat="server" Style="font-size: 13px;">遊艇規格</asp:LinkButton>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header d-flex " style="justify-content: space-between">
            <h4>遊艇規格</h4>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <hr />
    <div style="display: flex; justify-content: space-around">
        <asp:Button ID="backYatch" runat="server" Text="返回遊艇清單" class="btn btn-success waves-effect waves-light" OnClick="backYatch_Click" />
        <asp:Button ID="goYatchOverview" runat="server" Text="遊艇內容" class="btn btn-success waves-effect waves-light" OnClick="goYatchOverview_Click" />
        <asp:Button ID="goYatchSpecs" runat="server" Text="遊艇規格" class="btn btn-default waves-effect waves-light" OnClick="goYatchSpecs_Click" ForeColor="#00b9f5"/>
        <asp:Button ID="goYatchLayouts" runat="server" Text="遊艇設計圖" class="btn btn-success waves-effect waves-light" OnClick="goYatchLayouts_Click" />
        <asp:Button ID="goYatchPhotos" runat="server" Text="遊艇照片集" class="btn btn-success waves-effect waves-light" OnClick="goYatchPhotos_Click" />
    </div>
    <hr />
    <div class="card-block">
        <div class="form-group" style="width: 700px">
            <label for="exampleSelect1" class="form-control-label" style="display: block">遊艇型號：</label>
            <asp:Label ID="lbl_yatch_name" runat="server"></asp:Label>
        </div>
        <div class="form-group" style="width: 700px">
            <label for="exampleSelect1" class="form-control-label" style="display: block">遊艇介紹：</label>
            <asp:Label ID="lbl_spec_content" runat="server"></asp:Label>
            <CKEditor:CKEditorControl ID="cke_spec_content" runat="server" BasePath="/Scripts/ckeditor/" Visible="false"></CKEditor:CKEditorControl>
        </div>
        <asp:Button ID="editSpecContent" runat="server" Text="開始編輯" class="btn btn-success waves-effect waves-light m-r-30" OnClick="editSpecContent_Click" />
        <asp:Button ID="cancelSpecContent" runat="server" Text="取消編輯" class="btn btn-success waves-effect waves-light" Visible="false" OnClick="cancelSpecContent_Click" />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
</asp:Content>
