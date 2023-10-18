<%@ Page Title="" Language="C#" MasterPageFile="~/backend/MasterPage.Master" AutoEventWireup="true" CodeBehind="dealers_areas.aspx.cs" Inherits="Yachts.pages.B_dealers_areas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:Literal ID="active" runat="server" Text="Dealers" Visible="false"></asp:Literal>
</asp:Content>
<asp:Content ID="TopAndLeft" ContentPlaceHolderID="TopAndLeft" runat="server">
    <p style="text-align: right; padding-right: 24px; padding-top: 5px; font-size: 13px">
        <asp:LinkButton ID="lbtn_goCountries" runat="server" OnClick="lbtn_goCountries_Click" Style="font-size: 13px; color: inherit">國家管理</asp:LinkButton>
        >>
        <asp:LinkButton ID="lbtn_goAreas" runat="server" OnClick="lbtn_goAreas_Click" Style="font-size: 13px;">地區管理</asp:LinkButton>
    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainHeader" runat="server">
    <div class="row">
        <div class="main-header d-flex " style="justify-content: space-between">
            <h4>地區管理</h4>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div style="display: flex; justify-content: flex-start;">
        <div style="display: flex; justify-content: center; align-items: center;">
            <label for="lbl_selectCountry" class="form-control-label">新增地區：</label>
        </div>
        <asp:TextBox ID="txt_input_area" runat="server"></asp:TextBox>
        <asp:Button ID="btn_addArea" runat="server" Text="確定新增" class="btn btn-success waves-effect waves-light" Style="margin-left: 10px" OnClick="btn_addArea_Click" OnClientClick="return confirm('您確定要新增這個地區嗎？');" />
    </div>

    <div class="form-group" style="margin-top: 10px;">
        <label for="lbl_selectCountry" class="form-control-label">選擇國家</label>
        <asp:DropDownList ID="ddl_country_name" runat="server" class="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddl_country_name_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <div class="form-group" style="margin-top: 10px;">
        <label for="lbl_selectCountry" class="form-control-label">地區列表：</label>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="area_id" Style="display: flex; flex-direction: column; justify-content: space-between; align-items: center; border: 0; margin-top: 15px;" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="流水號" SortExpression="CategoryName" HeaderStyle-CssClass="text-center">
                <ItemTemplate>
                    <%# Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="地區名稱" SortExpression="area_name" HeaderStyle-CssClass="text-center">
                <EditItemTemplate>
                    <asp:TextBox ID="txt_area_name" runat="server" Text='<%# Bind("area_name") %>' Width="120px"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_area_name" runat="server" Text='<%# Bind("area_name") %>' Width="120px" ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" ItemStyle-Width="70px" HeaderStyle-Width="130px">
                <EditItemTemplate>
                    <asp:LinkButton ID="lbtn_update" runat="server" CausesValidation="True" CommandName="Update" Text="更新" class="btn btn-info waves-effect waves-light" Width="60px"></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="lbtn_cancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" class="btn btn-info waves-effect waves-light" Width="60px"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="編輯" class="btn btn-info waves-effect waves-light" Width="60px"></asp:LinkButton>
                </ItemTemplate>

                <ItemStyle Width="70px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_delete" runat="server" CausesValidation="False" CommandName="Delete" Text="刪除" OnClientClick="return confirm('您確定要刪除這個區域嗎？');" class="btn btn-danger waves-effect waves-light"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
<%--            <asp:TemplateField ShowHeader="False" HeaderText="代理商" HeaderStyle-CssClass="text-center">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtn_addDealer" runat="server" CausesValidation="false" CommandName="" Text="新增" OnClick="lbtn_addDealer_Click" class="btn btn-success waves-effect waves-light"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>--%>

        </Columns>
    </asp:GridView>
<%--    <hr />
    <div style="display: flex; justify-content: space-around;">
        <asp:Button ID="goDealers_countries" runat="server" Text="國家管理" class="btn btn-success waves-effect waves-light" OnClick="goDealers_countries_Click" />
        <asp:Button ID="goDealers_areas" runat="server" Text="地區管理(新增代理商)" class="btn btn-success waves-effect waves-light" OnClick="goDealers_areas_Click" />
        <asp:Button ID="goDealers" runat="server" Text="代理商管理" class="btn btn-success waves-effect waves-light" OnClick="goDealers_Click" />
    </div>--%>


</asp:Content>
