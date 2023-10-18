<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="yatchs_layouts.aspx.cs" Inherits="Yachts.backend.yatchs_layouts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="active" runat="server" Text="Yatchs" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="TopAndLeft" ContentPlaceHolderID="TopAndLeft" runat="server">
    <p style="text-align: right; padding-right: 24px; padding-top: 5px; font-size: 13px">
        <asp:LinkButton ID="lbtn_goYatchs" runat="server" OnClick="lbtn_goYatchs_Click" Style="font-size: 13px; color: inherit">遊艇清單</asp:LinkButton>
        >>
        <asp:LinkButton ID="lbtn_goYatchsLayouts" runat="server" Style="font-size: 13px;">遊艇設計圖</asp:LinkButton>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header d-flex " style="justify-content: space-between">
            <h4>遊艇設計圖</h4>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <hr />
    <div style="display: flex; justify-content: space-around">
        <asp:Button ID="backYatch" runat="server" Text="返回遊艇清單" class="btn btn-success waves-effect waves-light" OnClick="backYatch_Click" />
        <asp:Button ID="goYatchOverview" runat="server" Text="遊艇內容" class="btn btn-success waves-effect waves-light" OnClick="goYatchOverview_Click" />
        <asp:Button ID="goYatchSpecs" runat="server" Text="遊艇規格" class="btn btn-success waves-effect waves-light" OnClick="goYatchSpecs_Click" />
        <asp:Button ID="goYatchLayouts" runat="server" Text="遊艇設計圖" class="btn btn-default waves-effect waves-light" OnClick="goYatchLayouts_Click" ForeColor="#00b9f5" />
        <asp:Button ID="goYatchPhotos" runat="server" Text="遊艇照片集" class="btn btn-success waves-effect waves-light" OnClick="goYatchPhotos_Click" />
    </div>
    <hr />
    <div class="card-block">
        <div class="form-group" style="width: 700px">
            <label for="exampleSelect1" class="form-control-label" style="display: block">遊艇型號：</label>
            <asp:Label ID="lbl_yatch_name" runat="server"></asp:Label>
        </div>
        <label for="exampleSelect1" class="form-control-label" style="display: block">設計圖型：</label>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <div style="display: flex; justify-content: flex-start; align-items: flex-start">
                    <asp:Button ID="deleteImage" runat="server" Text="刪除" class="btn btn-success waves-effect waves-light m-r-1" CommandArgument='<%# Eval("layout_id") %>' OnClick="deleteImage_Click" OnClientClick="return confirm('您確定要刪除這個項目嗎？');" />
                    <asp:Image ID="img_layout_photoPath" runat="server" ImageUrl='<%# Eval("layout_photoPath") %>' />
                </div>
                <br />
            </ItemTemplate>
        </asp:Repeater>
        <asp:Button ID="btn_uploadLayouts" runat="server" Text="確認上傳" class="btn btn-success waves-effect waves-light" OnClick="btn_uploadLayouts_Click" />
        <asp:FileUpload ID="ful_layout_photoPath" runat="server" />

    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
</asp:Content>
