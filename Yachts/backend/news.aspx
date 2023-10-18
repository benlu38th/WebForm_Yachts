<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="news.aspx.cs" Inherits="Yachts.pages.B_news" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="active" runat="server" Text="News" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header d-flex " style="justify-content: space-between">
            <h4>新聞總覽</h4>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div style="display: flex; justify-content: space-between; align-items: flex-end">
        <div>
            <div style="display: flex; justify-content: flex-start; align-items: flex-start">
                <asp:Label ID="lbl_input_title" runat="server" Text="新聞標題：" Visible="false"></asp:Label>
                <asp:TextBox ID="txt_input_title" runat="server" Visible="false" Style="margin-bottom: 10px; margin-right: 10px;" Width="300px"></asp:TextBox>
            </div>
            <div style="display: flex; justify-content: flex-start; align-items: flex-start">
                <asp:Label ID="lbl_input_launchDate" runat="server" Text="發布時間：" Visible="false"></asp:Label>
                <asp:TextBox ID="txt_input_launchDate" runat="server" type="date" Visible="false" Style="margin-bottom: 10px"></asp:TextBox>
            </div>
            <div style="display: flex; justify-content: flex-start; align-items: flex-start">
                <asp:Label ID="lbl_input_des" runat="server" Text="新聞簡介：" Visible="false"></asp:Label>
                <asp:TextBox ID="txt_input_des" runat="server" TextMode="MultiLine" Width="300px" Height="100px" Visible="false" Style="margin-bottom: 10px"></asp:TextBox>
            </div>
            <div style="display: flex; justify-content: flex-start; align-items: flex-start">
                <asp:Label ID="lbl_input_top" runat="server" Visible="false" Text="是否置頂："></asp:Label>
                <asp:CheckBox ID="chk_input_top" runat="server" Visible="false" Style="margin-bottom: 10px" />
            </div>
            <div style="display: flex; justify-content: flex-start; align-items: flex-start">
                <asp:Label ID="lbl_input_photo" runat="server" Text="上傳封面：" Visible="false"></asp:Label>
                <asp:FileUpload ID="ful_input_photo" runat="server" Visible="false" Style="margin-bottom: 10px" />
            </div>
            <div style="display: flex; justify-content: flex-start; align-items: flex-start">
                <asp:Button ID="Button1" runat="server" Text="新增新聞" OnClick="Button1_Click" Style="margin-right: 10px; margin-bottom: 10px" class="btn btn-success waves-effect waves-light" />
                <asp:Button ID="Button2" runat="server" Text="取消" OnClick="Button2_Click" Visible="false" class="btn btn-success waves-effect waves-light" />
            </div>
        </div>
        <div style="display:flex; align-items:center; margin-bottom: 10px">
            <asp:Label ID="Label2" runat="server" Text="每頁幾則新聞："></asp:Label>
            <asp:TextBox ID="txt_news_pageInAPage" runat="server" Visible="false" style=" Width:30px"></asp:TextBox>
            <asp:Label ID="lbl_news_pageInAPage" runat="server" ></asp:Label>
            <asp:Literal ID="lit_overviewsDL_id" runat="server" Visible="false"></asp:Literal>
            <asp:Button ID="btn_checkPage" runat="server"  Text="更新" OnClick="btn_checkPage_Click" style="margin-left:10px;" class="btn btn-success waves-effect waves-light"  />
        </div>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="news_id" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" style="margin-bottom:20px">
        <Columns>
            <asp:TemplateField SortExpression="CategoryName" ItemStyle-Width="20px" ItemStyle-CssClass="text-center">
                <ItemTemplate >
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="新聞名稱" SortExpression="news_title">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_news_title" Width="200px" Font-Size="XX-Small"  runat="server" Text='<%# Bind("news_title") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_news_title" Width="200px" Font-Size="XX-Small"  runat="server" Text='<%# Bind("news_title") %>' OnClick="lbtn_news_title_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="新聞內容(短)" SortExpression="news_des" HeaderStyle-Width="250px">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_news_des" Width="250px" Height="150px" Font-Size="XX-Small"   runat="server" Text='<%# Bind("news_des") %>' TextMode="MultiLine"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_news_des" Width="200px" Font-Size="XX-Small"  runat="server" Text='<%# Bind("news_des") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="發布日期"  SortExpression="news_launchDate">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_launchDate" Width="100px" Font-Size="XX-Small"  runat="server" Text='<%# Bind("news_launchDate", "{0:yyyy-MM-dd}") %>' type="date"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_news_launchDate" Width="100px" Font-Size="XX-Small"  runat="server" Text='<%# Bind("news_launchDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="新聞封面" SortExpression="news_photo">
                <EditItemTemplate>
                    <asp:FileUpload ID="GV1_ful_news_photo" Style="white-space:normal" Width="100px" Font-Size="XX-Small"  runat="server" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" Width="100px" Font-Size="XX-Small"  ImageUrl='<%# Eval("news_photo") %>'/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="置頂" SortExpression="news_top" ItemStyle-CssClass="text-center">
                <EditItemTemplate>
                    <asp:CheckBox ID="edit_chk_news_top" runat="server" Checked='<%# Bind("news_top") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chk_news_top" runat="server" Checked='<%# Bind("news_top") %>' Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" HeaderStyle-Width="130px">
                <EditItemTemplate>
                    <asp:LinkButton ID="lbtn_update" Width="60px" runat="server" CausesValidation="True" CommandName="Update" Text="更新"  class="btn btn-info waves-effect waves-light"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="lbtn_cancel" Width="60px" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" class="btn btn-info waves-effect waves-light"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_edit" Width="60px" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯"  class="btn btn-info waves-effect waves-light"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_delete" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('您確定要刪除這則新聞嗎？\n注意!會連同詳細內文一同刪除!');"  class="btn btn-danger waves-effect waves-light"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
</asp:Content>
