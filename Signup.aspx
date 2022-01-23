<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="IT3685.Signup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="limiter">
        <div class="container-login100" style="background-image: url('Content/images/login-bg.jpg');">
            <div class="wrap-login100 p-l-110 p-r-110 p-t-62 p-b-33">
                <form class="login100-form validate-form flex-sb flex-w">
                    <span class="login100-form-title p-b-53">Sign Up
                    </span>


                    <asp:Label runat="server" ID="lblErrorMsg" Style="color: red" Text=""></asp:Label>

                    <div class="row">
                        <div class="col">
                            <span class="txt1">First Name<span style="color: red">*</span>
                            </span>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtFirstname" placeholder="John"></asp:TextBox>
                        </div>
                        <div class="col">
                            <span class="txt1">Last Name
                            </span>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtLastName" placeholder="Doe"></asp:TextBox>
                        </div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col">
                            <span class="txt1">Email Address<span style="color: red">*</span>
                            </span>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" placeholder="johndoe@gmail.com"></asp:TextBox>
                        </div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col">
                            <span class="txt1">Contact Number<span style="color: red">*</span>
                            </span>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtContactNumber" placeholder="91234567"></asp:TextBox>
                        </div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col">
                            <span class="txt1">Date of Birth<span style="color: red">*</span>
                            </span>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtDOB" type="date" placeholder=""></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4">
                            <span class="txt1">Gender<span style="color: red">*</span>
                            </span>
                            <asp:DropDownList runat="server" class="form-control" ID="DropDownGender">
                                <asp:ListItem Value="">Choose..</asp:ListItem>
                                <asp:ListItem Value="M">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <asp:Label runat="server" ID="lblPasswordValidation" Style="color: red" Text=""></asp:Label>

                    <div class="row" style="padding-top: 8px">
                        <div class="col">
                            <span class="txt1">Password<span style="color: red">*</span>
                            </span>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" type="password"></asp:TextBox>
                        </div>
                        <div class="col">
                            <span class="txt1">Confirm Password<span style="color: red">*</span>
                            </span>
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtConfirmPassword" type="password"></asp:TextBox>
                        </div>
                    </div>


                    <div class="container-login100-form-btn m-t-17" style="padding-top:10px;">
                        <asp:LinkButton runat="server" CssClass="login100-form-btn" Style="color: white" OnClick="OnRegisterClick">Register</asp:LinkButton>
                    </div>

                    <div class="w-full text-center p-t-55">
                        <span class="txt2">Already have an account?
                        </span>

                        <a href="Login" class="txt2 bo1">Sign in now
                        </a>
                    </div>
                </form>
            </div>
        </div>
    </div>


    <div id="dropDownSelect1"></div>

    <!--===============================================================================================-->
    <script src="Content/vendor/jquery/jquery-3.2.1.min.js"></script>
    <!--===============================================================================================-->
    <script src="Content/vendor/animsition/js/animsition.min.js"></script>
    <!--===============================================================================================-->
    <script src="Content/vendor/bootstrap/js/popper.js"></script>
    <script src="Content/vendor/bootstrap/js/bootstrap.min.js"></script>
    <!--===============================================================================================-->
    <script src="Content/vendor/select2/select2.min.js"></script>
    <!--===============================================================================================-->
    <script src="Content/vendor/daterangepicker/moment.min.js"></script>
    <script src="Content/vendor/daterangepicker/daterangepicker.js"></script>
    <!--===============================================================================================-->
    <script src="Content/vendor/countdowntime/countdowntime.js"></script>
    <!--===============================================================================================-->
    <script src="Content/js/login.js"></script>


</asp:Content>
