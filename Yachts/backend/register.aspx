<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="Yachts.pages.B_register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <!-- Favicon icon -->
    <link rel="shortcut icon" href="assets/images/favicon.png" type="image/x-icon">
    <link rel="icon" href="assets/images/favicon.ico" type="image/x-icon">

    <!-- Google font-->
    <link href="https://fonts.googleapis.com/css?family=Ubuntu:400,500,700" rel="stylesheet">

    <!--ico Fonts-->
    <link rel="stylesheet" type="text/css" href="assets/icon/icofont/css/icofont.css">

    <!-- Required Fremwork -->
    <link rel="stylesheet" type="text/css" href="assets/plugins/bootstrap/css/bootstrap.min.css">

    <!-- Style.css -->
    <link rel="stylesheet" type="text/css" href="assets/css/main.css">

    <!-- Responsive.css-->
    <link rel="stylesheet" type="text/css" href="assets/css/responsive.css">
</head>
<body>
    <form id="form1" runat="server">
        <section class="login common-img-bg">
            <!-- Container-fluid starts -->
            <div class="container-fluid">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="login-card card-block bg-white">
                            <form class="md-float-material" action="index.html">
                                <div class="text-center">
                                    <img src="assets/images/logo-black.png" alt="logo">
                                </div>
                                <h3 class="text-center txt-primary">Create an account </h3>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txt_user_account" runat="server" class="md-form-control" PlaceHolder="Your Account"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="md-input-wrapper">
                                            <asp:DropDownList ID="ddl_user_sex" runat="server" class="md-form-control">
                                                <asp:ListItem>男</asp:ListItem>
                                                <asp:ListItem>女</asp:ListItem>
                                                <asp:ListItem>無</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="md-input-wrapper">
                                    <asp:TextBox ID="txt_user_mail" runat="server" class="md-form-control" PlaceHolder="Email Address"></asp:TextBox>
                                </div>
                                <div class="md-input-wrapper">
                                    <asp:TextBox ID="txt_user_password" runat="server" class="md-form-control" PlaceHolder="Password" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="md-input-wrapper">
                                    <asp:TextBox ID="txt_user_passwordCheck" runat="server" class="md-form-control" PlaceHolder="Confirm Password" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="col-xs-10 offset-xs-1">
                                    <asp:Button ID="Button1" runat="server" Text="Sign up" class="btn btn-primary btn-md btn-block waves-effect waves-light m-b-20" OnClick="Button1_Click"/>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 text-center">
                                        <span class="text-muted">Already have an account?</span>
                                        <a href="login.aspx" class="f-w-600 p-l-5">Sign In Here</a>

                                    </div>
                                </div>
                            </form>
                            <!-- end of form -->
                        </div>
                        <!-- end of login-card -->
                    </div>
                    <!-- end of col-sm-12 -->
                </div>
                <!-- end of row-->
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



    </form>

            <!-- Required Jqurey -->
        <script src="assets/plugins/jquery/dist/jquery.min.js"></script>

        <script src="assets/plugins/jquery-ui/jquery-ui.min.js"></script>
        <script src="assets/plugins/tether/dist/js/tether.min.js"></script>

        <!-- Required Fremwork -->
        <script src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>

        <!--text js-->
        <script type="text/javascript" src="assets/pages/elements.js"></script>


</body>
</html>
