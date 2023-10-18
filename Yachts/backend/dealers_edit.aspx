<%@ Page Title="" Language="C#" MasterPageFile="~/pages/MasterPage.Master" AutoEventWireup="true" CodeBehind="dealers_edit.aspx.cs" Inherits="Yachts.pages.B_dealers_edit" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="active" runat="server" Text="Dealers" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header">
            <h4>Dealers</h4>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card-block">
        <div class="form-group">
            <label for="exampleSelect1" class="form-control-label">Country</label>
            <asp:TextBox ID="txt_countries" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="exampleSelect1" class="form-control-label">Area</label>
            <asp:TextBox ID="txt_area" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="exampleTextarea" class="form-control-label">Dealer Description</label>
            <CKEditor:CKEditorControl ID="cke_dealer_des" runat="server" BasePath="/Scripts/ckeditor/"></CKEditor:CKEditorControl>
        </div>

        <asp:Button ID="Button1" runat="server" Text="確認送出" class="btn btn-success waves-effect waves-light m-r-30" OnClick="Button1_Click"/>
        <asp:Button ID="Button2" runat="server" Text="返回區域頁" class="btn btn-success waves-effect waves-light m-r-30" onclick="Button2_Click"/>
    </div>
</asp:Content>
