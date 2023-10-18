<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="dealers_add.aspx.cs" Inherits="Yachts.pages.B_dealers_add_country" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="active" runat="server" Text="DealersDetail" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="TopAndLeft" ContentPlaceHolderID="TopAndLeft" runat="server">
    <p style="text-align: right; padding-right: 24px; padding-top: 5px; font-size: 13px">
        <asp:LinkButton ID="lbtn_goDealers" runat="server" OnClick="lbtn_goDealers_Click" Style="font-size: 13px; color: inherit">代理商管理</asp:LinkButton>
        >>
        <asp:LinkButton ID="lbtn_goDealersAdd" runat="server" Style="font-size: 13px; ">代理商新增</asp:LinkButton>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header">
            <h4>代理商新增</h4>
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
            <label for="exampleSelect1" class="form-control-label">Dealer Photo</label>
            <asp:FileUpload ID="FileUpload1" runat="server" />
        </div>
        <div class="form-group">
            <label for="exampleSelect1" class="form-control-label">Dealer Name</label>
            <asp:TextBox ID="txt_dealer_name" runat="server" class="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="exampleSelect1" class="form-control-label">Dealer Contact</label>
            <asp:TextBox ID="txt_dealer_contact" runat="server" class="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="exampleSelect1" class="form-control-label">Dealer Address</label>
            <asp:TextBox ID="txt_dealer_address" runat="server" class="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="exampleSelect1" class="form-control-label">Dealer Tel</label>
            <asp:TextBox ID="txt_dealer_tel" runat="server" class="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="exampleSelect1" class="form-control-label">Dealer Fax</label>
            <asp:TextBox ID="txt_dealer_fax" runat="server" class="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="exampleSelect1" class="form-control-label">Dealer Cell</label>
            <asp:TextBox ID="txt_dealer_cell" runat="server" class="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="exampleSelect1" class="form-control-label">Dealer Mail</label>
            <asp:TextBox ID="txt_dealer_mail" runat="server" class="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="exampleSelect1" class="form-control-label">Dealer Website</label>
            <asp:TextBox ID="txt_dealer_website" runat="server" class="form-control"></asp:TextBox>
        </div>

        <asp:Button ID="Button1" runat="server" Text="確認送出" class="btn btn-success waves-effect waves-light m-r-30" OnClick="Button1_Click" />
        <asp:Button ID="btn_returnToDealers" runat="server" Text="返回" class="btn btn-success waves-effect waves-light m-r-30" onclick="btn_returnToDealers_Click"/>
    </div>
</asp:Content>
