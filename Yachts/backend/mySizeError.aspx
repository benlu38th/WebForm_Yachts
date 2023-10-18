<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="mySizeError.aspx.cs" Inherits="Yachts.backend.mySizeError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <asp:Literal ID="active" runat="server" Text="" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header d-flex " style="justify-content: space-between">
            <h4>錯誤頁</h4>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-group" style="width:700px">
        <label for="exampleSelect1" class="form-control-label" style="display: block; font-size:15px" >🚀🚀🚀上傳文件過大!請上傳小於5MB的檔案!!</label>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
</asp:Content>
