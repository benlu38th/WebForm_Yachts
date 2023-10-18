<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.Master" AutoEventWireup="true" CodeBehind="Yachts_Layout.aspx.cs" Inherits="Yachts.Yachts_Lauout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>TtayanaWorld (DEMO)</title>
    <link rel="stylesheet" type="text/css" href="css/jquery.ad-gallery.css">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.ad-gallery.js"></script>
    <script type="text/javascript">
        $(function () {
            var galleries = $('.ad-gallery').adGallery();
            galleries[0].settings.effect = 'slide-hori';
        });
    </script>
    <!--[if lt IE 7]>
<script type="text/javascript" src="javascript/iepngfix_tilebg.js"></script>
<![endif]-->
    <link href="css/homestyle.css" rel="stylesheet" type="text/css" />
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bannermasks" runat="server">
    <a id="top"></a>
    <div class="bannermasks">
        <img src="images/banner01_masks.png" alt="&quot;&quot;" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="photoCarousel" runat="server">
    <div class="banner">
        <div id="gallery" class="ad-gallery">
            <div class="ad-image-wrapper">
            </div>
            <div class="ad-controls" style="display: none">
            </div>
            <div class="ad-nav">
                <div class="ad-thumbs">
                    <ul class="ad-thumb-list">
                        <asp:Repeater ID="RepeaterPhotos" runat="server" OnItemDataBound="RepeaterPhotos_ItemDataBound">
                            <ItemTemplate>
                                <li>
                                    <asp:HyperLink ID="link_yatchPhoto_photoPath" runat="server">
                                        <asp:Image ID="img_yatchPhoto_photoPath" runat="server" ImageUrl='<%# Bind("yatchPhoto_photoPath") %>' Style="height: 59px" />
                                    </asp:HyperLink>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="LeftContent" runat="server">
    <div class="conbg">
        <!--尾標在RightContent最後一列-->
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">
            <div class="left1">
                <p><span>YACHTS</span></p>
                <ul>
                    <asp:Repeater ID="RepeaterLeftList" runat="server">
                        <ItemTemplate>
                            <li>
                                <asp:Literal ID="lit_yatch_id" runat="server" Text='<%# Eval("yatch_id") %>' Visible="false"></asp:Literal>
                                <asp:Literal ID="lit_yatch_name" runat="server" Text='<%# Eval("yatch_name") %>' Visible="false"></asp:Literal>
                                <asp:LinkButton ID="lbtn_yatch_name" runat="server" Text='<%# Eval("yatch_name")+" "+Eval("yatch_note") %>' OnClick="lbtn_yatch_name_Click">
                                </asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="RightContent" runat="server">
    <!--------------------------------右邊選單開始---------------------------------------------------->
    <div id="crumb">
        <a href="Index.aspx">Home</a> >> 
        <a href="#">Yachts</a> >> 
        <a href="#">
            <span class="on1">
                <asp:Literal ID="little_yatch_name" runat="server"></asp:Literal>
            </span>
        </a>
    </div>
    <div class="right">
        <div class="right1">
            <div class="title">
                <span>
                    <asp:Literal ID="big_yatch_name" runat="server"></asp:Literal>
                </span>
            </div>
            <!--------------------------------內容開始---------------------------------------------------->
            <!--次選單-->
            <div class="menu_y">
                <ul>
                    <li class="menu_y00">YACHTS</li>
                    <li>
                        <asp:LinkButton class="menu_yli01" ID="lbtn_overview" runat="server" OnClick="lbtn_overview_Click">Overview</asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton class="menu_yli02" ID="lbtn_layout" runat="server" OnClick="lbtn_layout_Click">Layout & deck pla</asp:LinkButton>
                    </li>
                    <li>
                        <asp:LinkButton class="menu_yli03" ID="lbtn_spec" runat="server" OnClick="lbtn_spec_Click">Specification</asp:LinkButton>
                    </li>
                </ul>
            </div>
            <!--次選單-->
            <div class="box6">
                <p>Layout & deck plan</p>
                <ul>
                    <asp:Repeater ID="RepeaterLayoutPhoto" runat="server">
                        <ItemTemplate>
                            <li>
                                <asp:Image ID="img_layout_photoPath" runat="server" ImageUrl='<%# Eval("layout_photoPath") %> ' />
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div class="clear"></div>
            <a href="#top">
                <p class="topbuttom">
                    <img src="images/top.gif" alt="top" />
                </p>
            </a>
            <!--------------------------------內容結束------------------------------------------------------>
        </div>
    </div>
    <!--------------------------------右邊選單結束---------------------------------------------------->
    </div>   
    <!--頭標在LeftContent第一列-->
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="footer" runat="server">
    <div class="footer">
        <p class="footerp01">© 1973-2011 Tayana Yachts, Inc. All Rights Reserved</p>
        <div class="footer01">
            <span>No. 60, Hai Chien Road, Chung Men Li, Lin Yuan District, Kaohsiung City, Taiwan, R.O.C.</span><br />
            <span>TEL：+886(7)641-2721</span> <span>FAX：+886(7)642-3193</span><span><a href="mailto:tayangco@ms15.hinet.net">E-mail：tayangco@ms15.hinet.net</a>.</span>
        </div>
    </div>
</asp:Content>
