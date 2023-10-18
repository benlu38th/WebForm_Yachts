<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.Master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="Yachts.company" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="photoCarousel" runat="server">
    <div class="banner">
        <ul>
            <li>
                <img src="images/newbanner.jpg" alt="Tayana Yachts" /></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="LeftContent" runat="server">
    <div class="conbg">
        <!--尾標在RightContent最後一列-->
        <div class="left">
            <div class="left1">
                <p><span>COMPANY </span></p>
                <ul>
                    <li>
                        <asp:LinkButton ID="lbtn_aboutus" runat="server" OnClick="lbtn_aboutus_Click">About Us</asp:LinkButton></li>
                    <li>
                        <asp:LinkButton ID="lbtn_certificat" runat="server" OnClick="lbtn_certificat_Click">Certificat</asp:LinkButton></li>
                </ul>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="RightContent" runat="server">
    <div id="crumb">
        <a href="Index.aspx">Home</a> >> 
        <a href="#">Company</a>>> 
        <a href="#">
            <span class="on1">About Us</span>
        </a>
    </div>
    <div class="right">
        <div class="right1">
            <div class="title">
                <span>About Us</span>
            </div>
            <!--------------------------------內容開始---------------------------------------------------->
            <div class="box3">
                <h4>The Luxury Tayana 48 Pilothouse Lavish Woodwork Is Among</h4>
            </div>
            <%--  <p>
                    <img src="images/pit010.jpg" alt="&quot;&quot;" width="274" height="192" />
                </p>
                Founded in 1973, Ta Yang Building Co., Ltd. Has built over 1400 blue water cruising yachts to date. This world renowned custom yacht builder offers a large compliment of sailboats ranging from 37’ to 72’, many offer aft or center cockpit design, deck saloon or pilothouse options.
                        <br />
                <br />
                In 2003, Tayana introduced the new Tayana 64 Deck Saloon, designed by Robb Ladd, which offers the latest in building techniques, large sail area and a beam of 18 feet.
                        <br />
                <br />
                Tayana Yachts have been considered the leader in building custom interiors for the last two decades, offering it`s clients the luxury of a living arrangement they prefer rather than have to settle for the compromise of a production boat. Using the finest in exotic woods, the best equipment such as Lewmar, Whitlock, Yanmar engines, Selden spars to name a few, Ta yang has achieved the reputation for building one of the finest semi custom blue water cruising yachts in the world, at an affordable price.
                        <br />
                <br />
                Peter Chen was recently appointed the General Manager of Tayana. Peter has a wide-ranging knowledge of the yacht building industry; as part of the TAYANA team, Peter’s vision is genuinely rooted in honesty and integrity. “Our aim is to create outstanding styling, live aboard comfort, and safety at sea for every proud Tayana owner.”
                        <br />
            </div>--%>
            <asp:Label ID="title_des" runat="server"></asp:Label>

            <!--------------------------------內容結束------------------------------------------------------>
        </div>
    </div>
    </div><!--頭標在LeftContent第一列-->
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="footer" runat="server">
    <div class="footer">
        <p class="footerp01">© 1973-2011 Tayana Yachts, Inc. All Rights Reserved</p>
        <div class="footer01">
            <span>No. 60, Hai Chien Road, Chung Men Li, Lin Yuan District, Kaohsiung City, Taiwan, R.O.C.</span><br />
            <span>TEL：+886(7)641-2721</span> <span>FAX：+886(7)642-3193</span><span><a href="mailto:tayangco@ms15.hinet.net">E-mail：tayangco@ms15.hinet.net</a>.</span>
        </div>
    </div>
</asp:Content>
