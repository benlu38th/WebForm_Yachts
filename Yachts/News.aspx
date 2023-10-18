<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="Yachts.News1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>TtayanaWorld (DEMO)</title>
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.cycle.all.2.74.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.slideshow').cycle({
                fx: 'fade' // choose your transition type, ex: fade, scrollUp, shuffle, etc...
            });
        });
    </script>
    <!--[if lt IE 7]>
<script type="text/javascript" src="javascript/iepngfix_tilebg.js"></script>
<![endif]-->
    <link href="css/homestyle.css" rel="stylesheet" type="text/css" />
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bannermasks" runat="server">
    <div class="bannermasks">
        <img src="images/banner02_masks.png" alt="&quot;&quot;" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="photoCarousel" runat="server">
    <div class="banner">
        <ul>
            <li>
                <img src="images/newbanner.jpg" alt="Tayana Yachts" /></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="LeftContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <div class="conbg">
        <!--尾標在RightContent最後一列-->
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">
            <div class="left1">
                <p><span>NEWS</span></p>
                <ul>
                    <li><a href="#">News & Events</a></li>
                </ul>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="RightContent" runat="server">
    <div id="crumb"><a href="Index.aspx">Home</a> >> <a href="#">News </a>>> <a href="#"><span class="on1">News & Events</span></a></div>
    <div class="right">
        <div class="right1">
            <div class="title"><span>News & Events</span></div>
            <!--------------------------------內容開始---------------------------------------------------->
            <div class="box2_list">
                <ul>
                    <asp:Repeater ID="RepeaterNews" runat="server">
                        <ItemTemplate>
                            <li>
                                <div class="list01">
                                    <ul>
                                        <li>
                                            <div>
                                                <p>
                                                    <asp:Image ID="img_news_photo" runat="server" alt="&quot;&quot;" ImageUrl='<%# Eval("news_photo") %>' Style="width: 100%" />
                                                </p>
                                            </div>
                                        </li>
                                        <li>
                                            <asp:Label ID="lbl_news_launcgDate" runat="server" Text='<%# Eval("news_launchDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                                            <br />
                                        </li>
                                        <li>
                                            <asp:Literal ID="lit_news_id" runat="server" Visible="false" Text='<%# Eval("news_id") %>'></asp:Literal>
                                            <asp:LinkButton ID="lbtn_news_title" runat="server" Text='<%# Eval("news_title") %>' Style="color: #34A9D4; font-weight: bold; text-decoration: none" OnClick="lbtn_news_title_Click"></asp:LinkButton>
                                            <br />
                                        </li>
                                        <li>
                                            <asp:Literal ID="lit_news_des" runat="server" Text='<%# Eval("news_des") %>'></asp:Literal>
                                        </li>
                                    </ul>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div class="pagenumber">
                    <asp:LinkButton ID="lbtn_firstPage" runat="server" OnClick="lbtn_firstPage_Click">FirstPage</asp:LinkButton>|
                    <asp:Repeater ID="RepeaterPages" runat="server" OnItemDataBound="RepeaterPages_ItemDataBound">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtn_page" runat="server" Text='<%# Container.ItemIndex + 1 %>' OnClick="lbtn_page_Click"></asp:LinkButton>
                            |
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:LinkButton ID="lbtn_lastPage" runat="server" OnClick="lbtn_lastPage_Click">LirstPage</asp:LinkButton>
                </div>
                <%--<div class="pagenumber1">Items：<span>89</span>  |  Pages：<span>1/9</span></div>--%>
            </div>
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
