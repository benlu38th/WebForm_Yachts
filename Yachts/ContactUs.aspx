<%@ Page Title="" Language="C#" MasterPageFile="~/FrontMaster.Master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="Yachts.ContactUs" %>

<%@ Register Assembly="Recaptcha.Web" Namespace="Recaptcha.Web.UI.Controls" TagPrefix="cc1" %>

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
        <img src="images/contact.jpg" alt="&quot;&quot;" width="967" height="371" />
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
        <!--------------------------------左邊選單開始---------------------------------------------------->
        <div class="left">
            <div class="left1">
                <p><span>CONTACT</span></p>
                <ul>
                    <li><a href="#">contacts</a></li>
                </ul>
            </div>
        </div>
        <!--------------------------------左邊選單結束---------------------------------------------------->
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="RightContent" runat="server">
    <!--------------------------------右邊選單開始---------------------------------------------------->
    <div id="crumb">
        <a href="Index.aspx">Home</a> >> 
        <a href="#">
            <span class="on1">Contact</span>
        </a>
    </div>
    <div class="right">
        <div class="right1">
            <div class="title"><span>Contact</span></div>
            <!--------------------------------內容開始---------------------------------------------------->
            <!--表單-->
            <div class="from01">
                <p>
                    Please Enter your contact information<span class="span01">*Required</span>
                </p>
                <br />
                <table>
                    <tr>
                        <td class="from01td01">Name :</td>
                        <td><span>*</span>
                            <asp:TextBox ID="txt_contact_name" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="from01td01">Email :</td>
                        <td><span>*</span>
                            <asp:TextBox ID="txt_contact_mail" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="from01td01">Phone :</td>
                        <td><span>*</span>
                            <asp:TextBox ID="txt_contact_phone" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="from01td01">Country :</td>
                        <td><span>*</span>

                            <%--                            <select name="select" id="select">
                                <option>Annapolis</option>
                            </select>--%>
                            <asp:DropDownList ID="ddl_contact_country" runat="server" DataSourceID="SqlDataSource1" DataTextField="country_name" DataValueField="country_id" AppendDataBoundItems="True" AutoPostBack="True" Width="150px">
                                <asp:ListItem Selected="true" Value="0">--請選擇--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:YachtsConnectionString %>" SelectCommand="SELECT * FROM [countries]"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="from01td01">
                        Area :                  
                        <td><span>*</span>
                            <%--                            <select name="select" id="select">
                                <option>Annapolis</option>
                            </select>--%>
                            <asp:DropDownList ID="ddl_contact_area" runat="server" Width="150px" Enabled="false">
                                <asp:ListItem Selected="true" Value="0">--請先選國家--</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"><span>*</span>Brochure of interest  *Which Brochure would you like to view?</td>
                    </tr>
                    <tr>
                        <td class="from01td01">&nbsp;</td>
                        <td>
                            <%--                            <select name="select" id="select">
                                <option>Dynasty 72 </option>
                            </select>--%>
                            <asp:DropDownList ID="ddl_contact_yatch" runat="server" DataSourceID="SqlDataSource3" DataTextField="yatch_name" DataValueField="yatch_id"></asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:YachtsConnectionString %>" SelectCommand="SELECT * FROM [yatchs]"></asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="from01td01">
                        Comments:                  
                        <td>
                            <asp:TextBox ID="txt_contact_comments" runat="server" Columns="45" Rows="5" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="from01td01">
                        <td>
                            <cc1:RecaptchaApiScript ID="RecaptchaApiScript1" runat="server" />
                            <cc1:RecaptchaWidget ID="Recaptcha1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="from01td01">&nbsp;</td>
                        <td class="f_right">
                            <a href="#">
                                <%--                                <img src="images/buttom03.gif" alt="submit" width="59" height="25" />--%>
                                <asp:ImageButton ID="ImageButton" runat="server" Width="59" Height="25" ImageUrl="images/buttom03.gif" OnClick="ImageButton_Click" />
                            </a>
                        </td>
                    </tr>
                </table>
            </div>
            <!--表單-->
            <div class="box1">
                <span class="span02">Contact with us</span><br />
                Thanks for your enjoying our web site as an introduction to the Tayana world and our range of yachts.
As all the designs in our range are semi-custom built, we are glad to offer a personal service to all our potential customers. 
If you have any questions about our yachts or would like to take your interest a stage further, please feel free to contact us.
            </div>
            <div class="list03">
                <p>
                    <span>TAYANA HEAD OFFICE</span><br />
                    NO.60 Haichien Rd. Chungmen Village Linyuan Kaohsiung Hsien 832 Taiwan R.O.C<br />
                    tel. +886(7)641 2422<br />
                    fax. +886(7)642 3193<br />
                    info@tayanaworld.com<br />
                </p>
            </div>
            <div class="list03">
                <p>
                    <span>SALES DEPT.</span><br />
                    +886(7)641 2422  ATTEN. Mr.Basil Lin<br />
                    <br />
                </p>
            </div>
            <div class="box4">
                <h4>Location</h4>
                <p>
                    <iframe width="695" height="518" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3682.690768993764!2d120.30793527465838!3d22.62801987945227!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x346e0491b7febacd%3A0x24542bac2726199b!2z5a-25oiQ5LiW57SA5aSn5qiT!5e0!3m2!1szh-TW!2stw!4v1685176150894!5m2!1szh-TW!2stw" style="border: 0;" allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
                </p>
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
