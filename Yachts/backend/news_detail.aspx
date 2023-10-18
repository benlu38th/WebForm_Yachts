<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="news_detail.aspx.cs" Inherits="Yachts.backend.news_detail" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="active" runat="server" Text="News" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header d-flex " style="justify-content: space-between">
            <h4>內文管理</h4>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card-block">
        <div class="form-group" style="width: 700px">
            <label for="exampleSelect1" class="form-control-label" style="display: block">新聞標題：</label>
            <asp:Label ID="lbl_news_title" runat="server" Text="Label"></asp:Label>
        </div>
        <div class="form-group" style="width: 700px">
            <label for="exampleSelect1" class="form-control-label" style="display: block">新聞內容(長)：</label>
            <asp:Label ID="lbl_news_longDes" runat="server" Text="Label"></asp:Label>
            <CKEditor:CKEditorControl ID="cke_news_longDes" runat="server" BasePath="/Scripts/ckeditor/" Visible="false"></CKEditor:CKEditorControl>
        </div>
        <asp:Button ID="btn_startEdit" runat="server" Text="開始編輯" class="btn btn-success waves-effect waves-light m-r-30" OnClick="btn_startEdit_Click" />
        <asp:Button ID="btn_cancelEdit" runat="server" Text="取消編輯" class="btn btn-success waves-effect waves-light" OnClick="btn_cancelEdit_Click" Visible="false" />
        <hr />
        <div class="form-group" style="width: 700px; margin-top: 15px">
            <label for="exampleSelect1" class="form-control-label" style="display: block">下載檔案：</label>
            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                <ItemTemplate>
                    <asp:Button ID="Button4" runat="server" Text="刪除檔案" class="btn btn-success waves-effect waves-light" OnClick="Button4_Click" CommandArgument='<%# Eval("newsDL_id") %>' OnClientClick="return confirm('您確定要刪除這個檔案嗎？');" />
                    <asp:LinkButton ID="lbtn_newsDL_path" runat="server"></asp:LinkButton><br />
                </ItemTemplate>
            </asp:Repeater>
            <br />
            <asp:Button ID="Button3" runat="server" Text="確認上傳" class="btn btn-success waves-effect waves-light" OnClick="Button3_Click" />

            <asp:FileUpload ID="ful_newsDL_path" runat="server" />
        </div>
        <hr />
        <asp:Button ID="goNews" runat="server" Text="返回新聞總覽" class="btn btn-success waves-effect waves-light" OnClick="goNews_Click" />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
</asp:Content>
