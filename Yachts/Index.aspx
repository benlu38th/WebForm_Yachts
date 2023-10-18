<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Yachts.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>TtayanaWorld (DEMO)</title>
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.cycle.all.2.74.js"></script>
    <script type="text/javascript">


        $(function () {

            // 先取得 #abgne-block-20110111 , 必要參數及輪播間隔
            var $block = $('#abgne-block-20110111'),
                timrt, speed = 2500;

            // 幫 #abgne-block-20110111 .title ul li 加上 hover() 事件
            var $li = $('.title ul li', $block).hover(function () {
                // 當滑鼠移上時加上 .over 樣式
                $(this).addClass('over').siblings('.over').removeClass('over');
            }, function () {
                // 當滑鼠移出時移除 .over 樣式
                $(this).removeClass('over');
            }).click(function () {
                // 當滑鼠點擊時, 顯示相對應的 div.info
                // 並加上 .on 樣式

                $(this).addClass('on').siblings('.on').removeClass('on');
                $('#abgne-block-20110111 .bd .banner ul:eq(0)').children().hide().eq($(this).index()).fadeIn(1000);
            });

            // 幫 $block 加上 hover() 事件
            $block.hover(function () {
                // 當滑鼠移上時停止計時器
                clearTimeout(timer);
            }, function () {
                // 當滑鼠移出時啟動計時器
                timer = setTimeout(move, speed);
            });

            // 控制輪播
            function move() {
                var _index = $('.title ul li.on', $block).index();
                _index = (_index + 1) % $li.length;
                $li.eq(_index).click();

                timer = setTimeout(move, speed);
            }

            // 啟動計時器
            timer = setTimeout(move, speed);

        });
    </script>
    <!--[if lt IE 7]>
<script type="text/javascript" src="javascript/iepngfix_tilebg.js"></script>
<![endif]-->
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="photoCarousel" runat="server">
    <div id="abgne-block-20110111">
        <div class="bd">
            <div class="banner">
                <ul>
                    <asp:Repeater ID="RepeaterFirsrBigPhoto" runat="server" OnItemDataBound="RepeaterFirsrBigPhoto_ItemDataBound">
                        <ItemTemplate>
                            <li class="info on">
                                <a href="#">
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("yatch_coverPhoto") %>' />
                                </a>
                                <!--文字開始-->
                                <div class="wordtitle">
                                    <asp:Literal ID="lit_yatch_modelName" runat="server" Text='<%# Eval("yatch_modelName") %>'></asp:Literal>
                                    <span>
                                        <asp:Literal ID="lit_yatch_num" runat="server" Text='<%# Eval("yatch_num") %>'></asp:Literal>
                                    </span>
                                    <br />
                                    <p>SPECIFICATION SHEET</p>
                                </div>
                                <!--文字結束-->
                                <!--新船型開始  54型才出現其於隱藏 -->
                                <div class="new">
                                    <asp:Literal ID="lit_yatch_newModel" runat="server" Text='<%# Eval("yatch_newModel") %>' Visible="false"></asp:Literal>
                                    <asp:Image ID="img_yatch_newModel" runat="server" ImageUrl="images/new01.png" Visible="false" />
                                </div>
                                <!--新船型結束-->
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Repeater ID="RepeaterBigPhoto" runat="server" OnItemDataBound="RepeaterBigPhoto_ItemDataBound">
                        <ItemTemplate>
                            <li class="info off">
                                <a href="#">
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("yatch_coverPhoto") %>' />
                                </a>
                                <!--文字開始-->
                                <div class="wordtitle">
                                    <asp:Literal ID="lit_yatch_modelName" runat="server" Text='<%# Eval("yatch_modelName") %>'></asp:Literal>
                                    <span>
                                        <asp:Literal ID="lit_yatch_num" runat="server" Text='<%# Eval("yatch_num") %>'></asp:Literal>
                                    </span>
                                    <br />
                                    <p>SPECIFICATION SHEET</p>
                                </div>
                                <!--文字結束-->
                                <!--新船型開始  54型才出現其於隱藏 -->
                                <div class="new">
                                    <asp:Literal ID="lit_yatch_newModel" runat="server" Text='<%# Eval("yatch_newModel") %>' Visible="false"></asp:Literal>
                                    <asp:Image ID="img_yatch_newModel" runat="server" ImageUrl="images/new01.png" Visible="false" />
                                </div>
                                <!--新船型結束-->
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>

                <!--小圖開始-->
                <div class="bannerimg title">
                    <ul>
                        <asp:Repeater ID="RepeaterFirstLittlePhoto" runat="server">
                            <ItemTemplate>
                                <li class="on">
                                    <div>
                                        <p class="bannerimg_p">
                                            <asp:Literal ID="lit_first_little_yatch_id" runat="server" Visible="false" Text='<%# Eval("yatch_id") %>'></asp:Literal>
                                            <asp:ImageButton ID="img_little_yatch_coverPhoto" runat="server" ImageUrl='<%# Eval("yatch_coverPhoto") %>' Style="max-width: 100%; height: 100%" OnClick="img_little_yatch_coverPhoto_Click"/>
                                        </p>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Repeater ID="RepeaterLittlePhoto" runat="server">
                            <ItemTemplate>
                                <li class="off">
                                    <div>
                                        <p class="bannerimg_p">
                                            <asp:Literal ID="lit_first_little_yatch_id" runat="server" Visible="false" Text='<%# Eval("yatch_id") %>'></asp:Literal>
                                            <asp:ImageButton ID="img_little_yatch_coverPhoto" runat="server" ImageUrl='<%# Eval("yatch_coverPhoto") %>' Style="max-width: 100%; height: 100%" OnClick="img_little_yatch_coverPhoto_Click"/>
                                        </p>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <!--小圖結束-->
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="news">
        <div class="newstitle">
            <p class="newstitlep1">
                <img src="images/news.gif" alt="news" />
            </p>
            <p class="newstitlep2">
                <a href="News.aspx">More>></a>
            </p>
        </div>

        <ul>
            <asp:Repeater ID="RepeaterNews" runat="server">
                <ItemTemplate>
                    <!--TOP第一則最新消息-->
                    <li>
                        <div class="news01" style="height: 100px;">
                            <!--TOP標籤-->
                            <div class="newstop">
                                <asp:Image ID="img_topIcon" runat="server" ImageUrl="images/new_top01.png" />
                            </div>
                            <!--TOP標籤結束-->
                            <div class="news02p1">
                                <p class="news02p1img" style="display: flex; justify-content: center; align-items: center;">
                                    <asp:Image ID="img_news_photo" runat="server" ImageUrl='<%# Eval("news_photo") %>' Style="height: 100%;" />
                                </p>
                            </div>
                            <p class="news02p2" style="overflow: hidden; height: 90px">
                                <span>
                                    <asp:Literal ID="lit_news_title" runat="server" Text='<%# Eval("news_title") %>'></asp:Literal>
                                </span>
                                <asp:LinkButton ID="lbtn_goToNews" runat="server" OnClick="lbtn_goToNews_Click">
                                    <asp:Literal ID="lit_news_id" runat="server" Visible="false" Text='<%# Eval("news_id") %>'></asp:Literal>
                                    <asp:Literal ID="lit_news_des" runat="server" Text='<%# Eval("news_des") %>'></asp:Literal>
                                </asp:LinkButton>
                            </p>
                        </div>
                    </li>
                    <!--TOP第一則最新消息結束-->
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bannermasks" runat="server">
    <div class="bannermasks">
        <img src="images/banner00_masks.png" alt="&quot;&quot;" />
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="server">
    <div class="footer">
        <div class="footerp00">
            <a href="#">
                <img src="images/tog.jpg" alt="&quot;&quot;" /></a>
            <p class="footerp001">© 1973-2011 Tayana Yachts, Inc. All Rights Reserved</p>
        </div>
        <div class="footer01">
            <span>No. 60, Hai Chien Road, Chung Men Li, Lin Yuan District, Kaohsiung City, Taiwan, R.O.C.</span><br />
            <span>TEL：+886(7)641-2721</span> <span>FAX：+886(7)642-3193</span><span><a href="mailto:tayangco@ms15.hinet.net">E-mail：tayangco@ms15.hinet.net</a>.</span>
        </div>
    </div>
</asp:Content>
