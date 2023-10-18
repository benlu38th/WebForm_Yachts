<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.Master" AutoEventWireup="true" CodeBehind="Dealers.aspx.cs" Inherits="Yachts.dealers" %>

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
        <img src="images/DEALERS.jpg" alt="&quot;&quot;" width="967" height="371" />
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
                <p><span>DEALERS</span></p>
                <ul>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <li>
                                <asp:LinkButton ID="lbtn_chose" runat="server" Text='<%# Eval("country_name") %>' OnClick="lbtn_chose_Click" CommandArgument='<%# Eval("country_id") %>'></asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="RightContent" runat="server">
    <div id="crumb">
        <a href="Index.aspx">Home</a> >> <a href="#">Dealers </a>>> <a href="#"><span class="on1">
            <asp:Literal ID="lit_country2" runat="server"></asp:Literal></span></a>
    </div>
    <div class="right">
        <div class="right1">
            <div class="title">
                <span>
                    <asp:Literal ID="lit_country" runat="server"></asp:Literal>
                </span>
            </div>

            <!--------------------------------內容開始---------------------------------------------------->
            <div class="box2_list">
                <ul>
                    <asp:Repeater ID="Repeater3" runat="server" OnItemDataBound="Repeater3_ItemDataBound">
                        <ItemTemplate>
                            <li>
                                <div class="list02">
                                    <ul>
                                        <li class="list02li">
                                            <div>
                                                <p>
                                                    <asp:Image ID="img_dealer_photoPath" runat="server" ImageUrl='<%# Eval("dealer_photoPath") %>' Style="max-width: 100%;" />
                                                </p>
                                            </div>
                                        </li>
                                        <li>
                                            <asp:Label ID="lbl_area" runat="server" Text='<%# Eval("area_name") %>'></asp:Label>
                                            <br />
                                            <asp:Literal ID="lit_dealer_name" runat="server" Text='<%# Eval("dealer_name") %>'></asp:Literal>
                                            <br />
                                            Contact：<asp:Literal ID="lit_dealer_contact" runat="server" Text='<%# Eval("dealer_contact") %>'></asp:Literal>
                                            <br />
                                            Address：<asp:Literal ID="lit_dealer_address" runat="server" Text='<%# Eval("dealer_address") %>'></asp:Literal>
                                            <br />
                                            TEL：<asp:Literal ID="lit_dealer_tel" runat="server" Text='<%# Eval("dealer_tel") %>'></asp:Literal>
                                            <br />
                                            FAX：<asp:Literal ID="lit_dealer_fax" runat="server" Text='<%# Eval("dealer_fax") %>'></asp:Literal>
                                            <br />
                                            CELL：<asp:Literal ID="lit_dealer_cell" runat="server" Text='<%# Eval("dealer_cell") %>'></asp:Literal>
                                            <br />
                                            E-mail：<asp:Literal ID="lit_dealer_mail" runat="server" Text='<%# Eval("dealer_mail") %>'></asp:Literal>
                                            <br />
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("dealer_website") %>' Text='<%# Eval("dealer_website") %>'></asp:HyperLink>
                                        </li>
                                    </ul>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div class="pagenumber">
                    <asp:LinkButton ID="lbtn_firstPage" runat="server" OnClick="lbtn_firstPage_Click">FirstPage</asp:LinkButton>
                    | 
                    <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Text='<%# Container.ItemIndex + 1 %>'  ></asp:LinkButton>
                            |
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:LinkButton ID="lbtn_lastPage" runat="server" OnClick="lbtn_lastPage_Click">LastPage</asp:LinkButton>
                </div>
                <%--                <div class="pagenumber1">Items：<span>89</span>  |  Pages：<span>1/9</span></div>--%>
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
