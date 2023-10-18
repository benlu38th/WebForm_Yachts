<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="yatchs.aspx.cs" Inherits="Yachts.backend.yatchs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="active" runat="server" Text="Yatchs" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="TopAndLeft" ContentPlaceHolderID="TopAndLeft" runat="server">
    <p style="text-align: right; padding-right: 24px; padding-top: 5px; font-size: 13px">
        <asp:LinkButton ID="lbtn_goYatchs" runat="server">遊艇清單</asp:LinkButton>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header d-flex " style="justify-content: space-between">
            <h4>遊艇清單</h4>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: block; flex-direction: column; justify-content: flex-start; align-content: flex-start;">
        <div style="display: flex; justify-content: flex-start; align-items: flex-start">
            <asp:Label ID="lbl_input_name" runat="server" Text="遊艇型號：" Visible="false"></asp:Label>
            <asp:TextBox ID="txt_input_modelName" runat="server" placeholder="船名" Width="80px" Visible="false" Style="margin-right: 10px"></asp:TextBox>
            <asp:TextBox ID="txt_input_num" runat="server" placeholder="船號" Width="40px" Visible="false" Style="margin-right: 10px"></asp:TextBox>
            <asp:TextBox ID="txt_input_note" runat="server" placeholder="備註" Width="130px" Visible="false" Style="margin-bottom: 10px;"></asp:TextBox>
        </div>
        <div style="display: flex; justify-content: flex-start; align-items: flex-start">
            <asp:Label ID="lbl_input_coverPhoto" runat="server" Text="上傳封面：" Visible="false"></asp:Label>
            <asp:FileUpload ID="ful_input_coverPhoto" runat="server" Visible="false" Style="margin-bottom: 10px" />
        </div>
        <div style="display: flex; justify-content: flex-start; align-items: flex-start">
            <asp:Label ID="lbl_input_newModel" runat="server" Text="最新船型：" Visible="false"></asp:Label>
            <asp:CheckBox ID="chk_input_newModel" runat="server" Visible="false" Style="margin-bottom: 10px" />
        </div>

        <div style="display: flex; justify-content: flex-start; align-items: flex-start">
            <asp:Button ID="Button1" runat="server" Text="新增船型" OnClick="Button1_Click" Style="margin-right: 10px" class="btn btn-success waves-effect waves-light" />
            <asp:Button ID="Button2" runat="server" Text="取消" Visible="false" OnClick="Button2_Click" class="btn btn-success waves-effect waves-light" />
        </div>
    </div>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="yatch_id" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="流水號" SortExpression="CategoryName">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="遊艇型號" SortExpression="yatch_modelName">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_yatch_name" runat="server" Text='<%# Bind("yatch_name") %>' OnClick="LinkButton1_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="船名" SortExpression="yatch_modelName">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_yatch_modelName" runat="server" Text='<%# Bind("yatch_modelName")%>' Width="100px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_yatch_modelName" runat="server" Text='<%# Bind("yatch_modelName") %>' Width="100px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="船號" SortExpression="yatch_num">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_yatch_num" runat="server" Text='<%# Bind("yatch_num") %>' Width="40px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_yatch_num" runat="server" Text='<%# Bind("yatch_num") %>' Width="40px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="備註" SortExpression="yatch_num">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_yatch_note" runat="server" Text='<%# Bind("yatch_note") %>' Width="120px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lbl_yatch_note" runat="server" Text='<%# Bind("yatch_note") %>' Width="120px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="封面" SortExpression="yatch_coverPhoto">
                <EditItemTemplate>
                    <asp:FileUpload ID="ful_yatch_coverPhoto" runat="server" />
                    <asp:Image ID="edit_img_yatch_coverPhoto" runat="server" ImageUrl='<%# Eval("yatch_coverPhoto") %>' Visible="false" width="200px"/>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Image ID="img_yatch_coverPhoto" runat="server" ImageUrl='<%# Eval("yatch_coverPhoto") %>' width="150px"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="新型" SortExpression="yatch_newModel" ItemStyle-CssClass="text-center">
                <EditItemTemplate>
                    <asp:CheckBox ID="edit_chk_yatch_newModel" runat="server" Checked='<%# Bind("yatch_newModel") %>' Width="40px" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chk_yatch_newModel" runat="server" Checked='<%# Bind("yatch_newModel") %>' Enabled="false" Width="40px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="130px">
                <EditItemTemplate>
                    <asp:LinkButton ID="lbtn_update" Width="60px" runat="server" CausesValidation="True" CommandName="Update" Text="更新" class="btn btn-info waves-effect waves-light"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="lbtn_cancel" Width="60px" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" class="btn btn-info waves-effect waves-light"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_edit" Width="60px" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯" class="btn btn-info waves-effect waves-light"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_delete" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('您確定要刪除快艇嗎？');" class="btn btn-danger waves-effect waves-light"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
</asp:Content>
