﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Yachts.pages.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <!-- Favicon icon -->
    <link rel="shortcut icon" href="assets/images/favicon.png" type="image/x-icon" />
    <link rel="icon" href="assets/images/favicon.ico" type="image/x-icon" />

    <!-- Google font-->
    <link href="https://fonts.googleapis.com/css?family=Ubuntu:400,500,700" rel="stylesheet" />

    <!-- Font Awesome -->
    <link href="assets/css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <!--ico Fonts-->
    <link rel="stylesheet" type="text/css" href="assets/icon/icofont/css/icofont.css" />

    <!-- Required Fremwork -->
    <link rel="stylesheet" type="text/css" href="assets/plugins/bootstrap/css/bootstrap.min.css" />

    <!-- waves css -->
    <link rel="stylesheet" type="text/css" href="assets/plugins/Waves/waves.min.css" />

    <!-- Style.css -->
    <link rel="stylesheet" type="text/css" href="assets/css/main.css" />

    <!-- Responsive.css-->
    <link rel="stylesheet" type="text/css" href="assets/css/responsive.css" />

    <!--color css-->
    <link rel="stylesheet" type="text/css" href="assets/css/color/color-1.min.css" id="color" />

    <style>
        .my-input::placeholder {
            color: lightgray; /* 在此設定顏色 */
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <section class="login p-fixed d-flex text-center bg-primary common-img-bg">
            <!-- Container-fluid starts -->
            <div class="container-fluid">
                <div class="row">

                    <div class="col-sm-12">
                        <div class="login-card card-block">
                            <form class="md-float-material">
                                <div class="text-center">
                                    <img src="assets/images/logo-black.png" alt="logo">
                                </div>
                                <h3 class="text-center txt-primary">Sign In to your account
                                </h3>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="md-input-wrapper">
                                            <asp:TextBox ID="txt_user_account" runat="server" class="md-form-control my-input" placeholder="your account"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="md-input-wrapper ">
                                            <asp:TextBox ID="txt_user_password" runat="server" class="md-form-control my-input" placeholder="your password" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>

                                    <%--                                    <div class="col-sm-6 col-xs-12">
                                        <div class="rkmd-checkbox checkbox-rotate checkbox-ripple m-b-25">
                                            <label class="input-checkbox checkbox-primary">
                                                <input type="checkbox" id="checkbox">
                                                <span class="checkbox"></span>
                                            </label>
                                            <div class="captions">Remember Me</div>

                                        </div>
                                    </div>--%>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="rkmd-checkbox checkbox-rotate checkbox-ripple m-b-25">
                                            <asp:LinkButton ID="LinkButton1" runat="server" class="text-right f-w-600" OnClick="LinkButton1_Click">Forget Password?</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-10 offset-xs-1">
                                        <asp:Button ID="Button2" runat="server" Text="LOGIN" class="btn btn-primary btn-md btn-block waves-effect text-center m-b-20" OnClick="Button2_Click" />
                                    </div>
                                </div>
                                <!-- <div class="card-footer"> -->
                                <div class="col-sm-12 col-xs-12 text-center">
                                    <span class="text-muted">Don't have an account?</span>
                                    <a href="register.aspx" class="f-w-600 p-l-5">Sign up Now</a>
                                </div>

                                <!-- </div> -->
                            </form>
                            <!-- end of form -->
                        </div>
                        <!-- end of login-card -->
                    </div>
                    <!-- end of col-sm-12 -->
                </div>
                <!-- end of row -->
            </div>
            <!-- end of container-fluid -->
        </section>

        <!-- Warning Section Starts -->
        <!-- Older IE warning message -->
        <!--[if lt IE 9]>
<div class="ie-warning">
	<h1>Warning!!</h1>
	<p>You are using an outdated version of Internet Explorer, please upgrade <br/>to any of the following web browsers to access this website.</p>
	<div class="iew-container">
		<ul class="iew-download">
			<li>
				<a href="http://www.google.com/chrome/">
					<img src="assets/images/browser/chrome.png" alt="Chrome">
					<div>Chrome</div>
				</a>
			</li>
			<li>
				<a href="https://www.mozilla.org/en-US/firefox/new/">
					<img src="assets/images/browser/firefox.png" alt="Firefox">
					<div>Firefox</div>
				</a>
			</li>
			<li>
				<a href="http://www.opera.com">
					<img src="assets/images/browser/opera.png" alt="Opera">
					<div>Opera</div>
				</a>
			</li>
			<li>
				<a href="https://www.apple.com/safari/">
					<img src="assets/images/browser/safari.png" alt="Safari">
					<div>Safari</div>
				</a>
			</li>
			<li>
				<a href="http://windows.microsoft.com/en-us/internet-explorer/download-ie">
					<img src="assets/images/browser/ie.png" alt="">
					<div>IE (9 & above)</div>
				</a>
			</li>
		</ul>
	</div>
	<p>Sorry for the inconvenience!</p>
</div>
<![endif]-->
        <!-- Warning Section Ends -->
        <!-- Required Jqurey -->
        <script src="assets/plugins/jquery/dist/jquery.min.js"></script>
        <script src="assets/plugins/jquery-ui/jquery-ui.min.js"></script>
        <script src="assets/plugins/tether/dist/js/tether.min.js"></script>

        <!-- Required Fremwork -->
        <script src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>

        <!-- waves effects.js -->
        <script src="assets/plugins/Waves/waves.min.js"></script>
        <!-- Custom js -->
        <script type="text/javascript" src="assets/pages/elements.js"></script>

    </form>
</body>
</html>
