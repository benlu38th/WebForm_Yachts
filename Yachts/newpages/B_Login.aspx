<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="B_Login.aspx.cs" Inherits="Yachts.newpages.B_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登入頁</title>

    <!-- Favicon icon -->
    <link rel="shortcut icon" href="assets/images/favicon.png" type="image/x-icon">
    <link rel="icon" href="assets/images/favicon.ico" type="image/x-icon">

    <!-- Google font-->
    <link href="https://fonts.googleapis.com/css?family=Ubuntu:400,500,700" rel="stylesheet">

    <!-- Font Awesome -->
    <link href="assets/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <!--ico Fonts-->
    <link rel="stylesheet" type="text/css" href="assets/icon/icofont/css/icofont.css">

    <!-- Required Fremwork -->
    <link rel="stylesheet" type="text/css" href="assets/plugins/bootstrap/css/bootstrap.min.css">

    <!-- waves css -->
    <link rel="stylesheet" type="text/css" href="assets/plugins/Waves/waves.min.css">

    <!-- Style.css -->
    <link rel="stylesheet" type="text/css" href="assets/css/main.css">

    <!-- Responsive.css-->
    <link rel="stylesheet" type="text/css" href="assets/css/responsive.css">

    <!--color css-->
    <link rel="stylesheet" type="text/css" href="assets/css/color/color-1.min.css" id="color" />

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

                                            <asp:TextBox ID="txt_user_account" runat="server" class="md-form-control" placeholder="your account"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="md-input-wrapper">
                                           <asp:TextBox ID="txt_user_password" runat="server" class="md-form-control" placeholder="your password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-xs-12">
                                        <div class="rkmd-checkbox checkbox-rotate checkbox-ripple m-b-25">
                                            <a href="forgot-password.html" class="text-right f-w-600">Forget Password?</a>
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
                                    <a href="register2.html" class="f-w-600 p-l-5">Sign up Now</a>
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
