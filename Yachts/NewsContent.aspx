<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.Master" AutoEventWireup="true" CodeBehind="NewsContent.aspx.cs" Inherits="Yachts.News" %>

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
    <div class="conbg">
        <!--尾標在RightContent最後一列-->
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
            <div class="box3">
                <h4>
                    <asp:Literal ID="lit_news_title" runat="server"></asp:Literal>
                </h4>
                <asp:Literal ID="lit_news_longDes" runat="server"></asp:Literal>
            </div>
            <!--下載開始-->
            <div class="downloads">
                <p>
                    <img src="images/downloads.gif" alt="&quot;&quot;" />
                </p>
                <ul>
                    <asp:Repeater ID="RepeaterNewsDL" runat="server" OnItemDataBound="RepeaterNewsDL_ItemDataBound">
                        <ItemTemplate>
                            <li>
                                <asp:Literal ID="lit_newsDL" runat="server" Text='<%# Eval("newsDL_path") %>' Visible= false ></asp:Literal>
                                <asp:LinkButton ID="lbtn_newsDL" runat="server" Text='<%# Eval("newsDL_path") %>' OnClick="lbtn_newsDL_Click"></asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <!--下載結束-->
            <div class="buttom001">
                <a href="#">
                    <img src="images/back.gif" alt="&quot;&quot;" width="55" height="28" />
                </a>
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
