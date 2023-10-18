<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="yatchs_overviews.aspx.cs" Inherits="Yachts.backend.yatchs_detail" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="active" runat="server" Text="Yatchs" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="TopAndLeft" ContentPlaceHolderID="TopAndLeft" runat="server">
    <p style="text-align: right; padding-right: 24px; padding-top: 5px; font-size: 13px">
        <asp:LinkButton ID="lbtn_goYatchs" runat="server" OnClick="lbtn_goYatchs_Click" Style="font-size: 13px; color: inherit">遊艇清單</asp:LinkButton>
        >>
        <asp:LinkButton ID="lbtn_goYatchsOverviews" runat="server" Style="font-size: 13px;">遊艇內容</asp:LinkButton>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header d-flex " style="justify-content: space-between">
            <h4>遊艇內容</h4>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <hr />
    <div style="display:flex; justify-content:space-around">
        <asp:Button ID="backYatch" runat="server" Text="返回遊艇清單" class="btn btn-success waves-effect waves-light" OnClick="backYatch_Click" />
        <asp:Button ID="goYatchOverview" runat="server" Text="遊艇內容" class="btn btn-default waves-effect waves-light" OnClick="goYatchOverview_Click" ForeColor="#00b9f5" />
        <asp:Button ID="goYatchSpecs" runat="server" Text="遊艇規格" class="btn btn-success waves-effect waves-light" OnClick="goYatchSpecs_Click" />
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
            <asp:Label ID="lbl_overview_content" runat="server"></asp:Label>
            <CKEditor:CKEditorControl ID="cke_overview_content" runat="server" BasePath="/Scripts/ckeditor/" Visible="false"></CKEditor:CKEditorControl>
        </div>
        <div class="form-group" style="width: 700px">
            <label for="exampleSelect1" class="form-control-label" style="display: block">遊艇尺寸：</label>
            <asp:Label ID="lbl_overview_dime" runat="server"></asp:Label>
            <CKEditor:CKEditorControl ID="cke_overview_dime" runat="server" BasePath="/Scripts/ckeditor/" Visible="false"></CKEditor:CKEditorControl>
        </div>
        <div class="form-group" style="width: 700px">
            <label for="exampleSelect1" class="form-control-label" style="display: block">尺寸照片：</label>
            <asp:Image ID="img_overview_dimePhoto" runat="server" Width="400px" />
            <asp:FileUpload ID="ful_overview_dimePhoto" runat="server" Visible="false" />
        </div>
        <asp:Button ID="editExceptDL" runat="server" Text="開始編輯" class="btn btn-success waves-effect waves-light m-r-30" OnClick="editExceptDL_Click" />
        <asp:Button ID="cancelExcelDL" runat="server" Text="取消編輯" class="btn btn-success waves-effect waves-light" OnClick="cancelExcelDL_Click" Visible="false" />
        <hr />
        <div class="form-group" style="width: 700px; margin-top: 15px">
            <label for="exampleSelect1" class="form-control-label" style="display: block">下載檔案：</label>
            <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                <ItemTemplate>
                    <asp:Button ID="btn_deleteDL" runat="server" Text="刪除檔案" class="btn btn-success waves-effect waves-light" CommandArgument='<%# Eval("overviewsDL_id") %>' OnClick="btn_deleteDL_Click" OnClientClick="return confirm('您確定要刪除這個檔案嗎？');" />
                    <asp:LinkButton ID="lbtn_overviewsDL_path" runat="server"></asp:LinkButton><br />
                </ItemTemplate>
            </asp:Repeater>
            <br />
            <asp:Button ID="btn_Upload" runat="server" Text="確認上傳" class="btn btn-success waves-effect waves-light" OnClick="btn_Upload_Click" />
            <asp:FileUpload ID="ful_overviewsDL_path" runat="server" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
</asp:Content>
