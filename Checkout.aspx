<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="IT3685.Add_Card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="Content/css/CardListing.css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="product_section layout_padding">
        <div class="container">
            <div class="row billing-fields">
                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12 sm-margin-30px-bottom">
                    <div class="create-ac-content bg-light-gray padding-20px-all">
                        <fieldset>
                            <h2 class="login-title mb-3">Shipping Address</h2>
                            <div class="row">
                                <div class="col">
                                    <span class="txt1">Full Address<span style="color: red">*</span>
                                    </span>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtAddress"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row" style="padding-top: 15px">
                                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <span class="txt1">Unit Number<span style="color: red">*</span>
                                    </span>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtUnitNo"></asp:TextBox>
                                </div>
                                <div class="col">
                                    <span class="txt1">Postal Code<span style="color: red">*</span>
                                    </span>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtPostal"></asp:TextBox>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <hr />
                    <div>
                        <h2 class="login-title mb-3">Payment Card</h2>

                        <!-- Cards -->
                        <asp:RadioButton ID="rbMetric" runat="server" GroupName="measurementSystem" Style="display: inline-block; padding-right: 20px" />
                        <div class="credit-card visa" style="display: inline-block;">
                            <div class="credit-card-last4">
                                4242
                            </div>
                            <div class="credit-card-expiry">
                                08/25
                            </div>
                        </div>
                        <br />
                        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="measurementSystem" Style="display: inline-block; padding-right: 20px" />
                        <div class="credit-card visa" style="display: inline-block;">
                            <div class="credit-card-last4">
                                4242
                            </div>
                            <div class="credit-card-expiry">
                                08/25
                            </div>
                        </div>

                        <br />
                        <a href="#">
                            <div style="padding-left: 40px;">
                                <div class="credit-card none" style="display: inline-block;">
                                    Add New Card
                                </div>
                            </div>
                        </a>


                        <!-- Mastercard - selectable -->
                        <div class="credit-card mastercard selectable">
                            <div class="credit-card-last4">
                                8210
                            </div>
                            <div class="credit-card-expiry">
                                10/22
                            </div>
                        </div>

                        <!-- Amex - selectable -->
                        <div class="credit-card amex selectable">
                            <div class="credit-card-last4">
                                8431
                            </div>
                            <div class="credit-card-expiry">
                                01/24
                            </div>
                        </div>

                        <!-- Discover - selectable -->
                        <div class="credit-card discover selectable">
                            <div class="credit-card-last4">
                                9424
                            </div>
                            <div class="credit-card-expiry">
                                06/23
                            </div>
                        </div>

                        <!-- Diners - selectable -->
                        <div class="credit-card diners selectable">
                            <div class="credit-card-last4">
                                3237
                            </div>
                            <div class="credit-card-expiry">
                                08/25
                            </div>
                        </div>

                        <!-- JCB - selectable -->
                        <div class="credit-card jcb selectable">
                            <div class="credit-card-last4">
                                1060
                            </div>
                            <div class="credit-card-expiry">
                                02/21
                            </div>
                        </div>


                        <!-- Unionpay - selectable -->
                        <div class="credit-card unionpay selectable">
                            <div class="credit-card-last4">
                                0005
                            </div>
                            <div class="credit-card-expiry">
                                03/25
                            </div>
                        </div>



                    </div>
                </div>

                <div class="col-xl-6 col-lg-6 col-md-6 col-sm-12">
                    <div class="your-order-payment">
                        <div class="your-order">
                            <h2 class="order-title mb-4">Your Order</h2>

                            <div class="table-responsive-sm order-table">
                                <table class="bg-white table table-bordered table-hover text-center">
                                    <thead>
                                        <tr>
                                            <th class="text-left">Product Name</th>
                                            <th>Price</th>
                                            <th>Size</th>
                                            <th>Qty</th>
                                            <th>Subtotal</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td class="text-left">Spike Jacket</td>
                                            <td>$99</td>
                                            <td>S</td>
                                            <td>1</td>
                                            <td>$99</td>
                                        </tr>
                                        <tr>
                                            <td class="text-left">Argon Sweater</td>
                                            <td>$199</td>
                                            <td>M</td>
                                            <td>2</td>
                                            <td>$298</td>
                                        </tr>
                                        <tr>
                                            <td class="text-left">Babydoll Bow Dress</td>
                                            <td>$299</td>
                                            <td>XL</td>
                                            <td>3</td>
                                            <td>$398</td>
                                        </tr>
                                    </tbody>
                                    <tfoot class="font-weight-600">
                                        <tr>
                                            <td colspan="4" class="text-right">Shipping </td>
                                            <td>$50.00</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" class="text-right">Total</td>
                                            <td>$845.00</td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>

                        <hr />

                        <div class="your-payment">
                            <div class="order-button-payment">
                                <asp:Button runat="server" cssClass="btn" text="Place order" style="float: left"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
