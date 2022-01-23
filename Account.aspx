<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="IT3685.Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        body {
            margin: 0;
            padding-top: 40px;
            color: #2e323c;
            background: #f5f6fa;
            position: relative;
            height: 100%;
        }

        .account-settings .user-profile {
            margin: 0 0 1rem 0;
            padding-bottom: 1rem;
            text-align: center;
        }

            .account-settings .user-profile .user-avatar {
                margin: 0 0 1rem 0;
            }

                .account-settings .user-profile .user-avatar img {
                    width: 90px;
                    height: 90px;
                    -webkit-border-radius: 100px;
                    -moz-border-radius: 100px;
                    border-radius: 100px;
                }

            .account-settings .user-profile h5.user-name {
                margin: 0 0 0.5rem 0;
            }

            .account-settings .user-profile h6.user-email {
                margin: 0;
                font-size: 0.8rem;
                font-weight: 400;
                color: #9fa8b9;
            }

        .account-settings .about {
            margin: 2rem 0 0 0;
            text-align: center;
        }

            .account-settings .about h5 {
                margin: 0 0 15px 0;
                color: #007ae1;
            }

            .account-settings .about p {
                font-size: 0.825rem;
            }

        .form-control {
            border: 1px solid #cfd1d8;
            -webkit-border-radius: 2px;
            -moz-border-radius: 2px;
            border-radius: 2px;
            font-size: .825rem;
            background: #ffffff;
            color: #2e323c;
        }

        .card {
            background: #ffffff;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
            border: 0;
            margin-bottom: 1rem;
        }

        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



    <div class="container">
        <div class="alert alert-success" runat="server" id="alertSuccess" style="display: none;">
            <strong>Success!</strong> Your account details has been updated
        </div>
        <div class="row gutters">
            <div class="col-xl-3 col-lg-3 col-md-12 col-sm-12 col-12">
                <div class="card h-100">
                    <div class="card-body">
                        <div class="account-settings">
                            <div class="user-profile">
                                <div class="user-avatar">
                                    <asp:Image runat="server" ID="imgProfile" />
                                </div>

                                <h5 class="user-name">
                                    <asp:Label runat="server" ID="lblName"></asp:Label></h5>
                                <h6 class="user-email">
                                    <asp:Label runat="server" ID="lblEmail"></asp:Label></h6>
                                <h6 class="user-email">
                                    <asp:Label runat="server" ID="lblGender"></asp:Label></h6>
                            </div>

                            <p style="text-align: center;">Upload new display picture</p>
                            <input id="oFile" type="file" runat="server" name="oFile">

                            <asp:Panel ID="frmConfirmation" Visible="true" runat="server">
                                <asp:Label ID="lblUploadResult" runat="server"></asp:Label>
                            </asp:Panel>
                            <!-- <div class="about">
                                <h5>About</h5>
                                <p>I'm Yuki. Full Stack Designer I enjoy creating user-centric, delightful and human experiences.</p>
                            </div> -->
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xl-9 col-lg-9 col-md-12 col-sm-12 col-12">
                <div class="card h-100">
                    <div class="card-body">
                        <div class="row gutters">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                                <h6 class="mb-2 text-primary">Personal Details</h6>
                            </div>
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
                                <div class="form-group">
                                    <label for="txtFirstName">First Name</label>
                                    <asp:TextBox runat="server" ID="txtFirstName" class="form-control" placeholder="Enter first name"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
                                <div class="form-group">
                                    <label for="txtLastName">Last Name</label>
                                    <asp:TextBox runat="server" ID="txtLastName" class="form-control" placeholder="Enter last name"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
                                <div class="form-group">
                                    <label for="txtPhone">Phone</label>
                                    <asp:TextBox runat="server" ID="txtPhone" class="form-control" placeholder="Enter phone number" type="number"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row gutters">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                                <h6 class="mt-3 mb-2 text-primary">Address</h6>
                            </div>

                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                                <div class="form-group">
                                    <label for="txtAddress">Address</label>
                                    <asp:TextBox runat="server" ID="txtAddress" class="form-control" placeholder="Enter address"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
                                <div class="form-group">
                                    <label for="txtUnitNo">Unit No. <small>(Optional)</small></label>
                                    <asp:TextBox runat="server" ID="txtUnitNo" class="form-control" placeholder="Enter unit number" type="number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-12">
                                <div class="form-group">
                                    <label for="txtPostalCode">Postal Code</label>
                                    <asp:TextBox runat="server" ID="txtPostalCode" class="form-control" placeholder="Enter postal code" type="number"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row gutters">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                                <div class="text-right">
                                    <asp:LinkButton runat="server" ID="Update" name="submit" class="btn btn-primary" OnClick="Update_Click">Update</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
</asp:Content>
