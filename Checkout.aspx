<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="IT3685.Add_Card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" href="Content/css/CardListing.css" />
    <link rel="stylesheet" href="Content/css/AddCard.css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        window.onload = function () {
            console.log(document.getElementById("CardType"));
        };

        function onSubmit() {
            console.log(document.querySelector('input[name="cardRadio"]:checked').value);
        }
    </script>
    <section class="product_section layout_padding">
        <div class="container">
            <div class="alert alert-danger" role="alert" runat="server" visible="false" id="errorMsgDiv">
                <asp:Label runat="server" ID="errorMessage"></asp:Label>
            </div>

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
                    <br />
                    <hr />
                    <br />
                    <div>
                        <h2 class="login-title mb-3">Payment Card</h2>

                        <!-- Cards -->
                        <asp:Repeater runat="server" ID="cardRepeater">
                            <ItemTemplate>
                                <input type="radio" name="cardRadio" id='<%# Eval("CardNumber") %>' value='<%# Eval("CardNumber") %>' style="display: inline-block;">
                                &nbsp &nbsp
                                    <div class='<%# "credit-card " + GetCardType(Eval("CardNumber").ToString()) %>' id="CardType" style="display: inline-block;">
                                        <div class="credit-card-last4">
                                            <%# Eval("CardNumber").ToString().Substring(12) %>
                                        </div>
                                        <div class="credit-card-expiry">
                                            <%# Eval("ExpiryMonth") + "/" + Eval("ExpiryYear") %>
                                        </div>
                                    </div>
                                    <br />
                            </ItemTemplate>
                        </asp:Repeater>

                        <div style="padding-left: 33px;">
                            <asp:LinkButton runat="server" ID="OpenAddCard" OnClick="OpenAddCard_Click">
                                <div class="credit-card none" style="display: inline-block;">
                                    Add New Card
                                </div>
                            </asp:LinkButton>
                        </div>


                        <!--
                        <!- JCB - selectable ->
                        <div class="credit-card jcb selectable">
                            <div class="credit-card-last4">
                                1060
                            </div>
                            <div class="credit-card-expiry">
                                02/21
                            </div>
                        </div>


                        <!- Unionpay - selectable ->
                        <div class="credit-card unionpay selectable">
                            <div class="credit-card-last4">
                                0005
                            </div>
                            <div class="credit-card-expiry">
                                03/25
                            </div>
                        </div>
                        -->


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
                                            <th>Qty</th>
                                            <th>Subtotal</th>
                                        </tr>
                                    </thead>

                                    <tbody>
                                        <asp:Repeater runat="server" ID="orderRepeater">
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="text-left"><%# Eval("Name") %></td>
                                                    <td>$<%# Eval("Price") %></td>
                                                    <td><%# Eval("Quantity") %></td>
                                                    <td>$<%# Convert.ToDecimal(Eval("Price").ToString()) * Convert.ToInt32(Eval("Quantity").ToString()) %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot class="font-weight-600">
                                        <tr>
                                            <td colspan="3" class="text-right">Shipping</td>
                                            <td>$0.00</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" class="text-right">Total</td>
                                            <td>$<asp:Label runat="server" ID="lblTotal" Text="0"></asp:Label></td>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                        <hr />
                        <div class="your-payment">
                            <div class="order-button-payment">
                                <input type="button" onclick="onSubmit()" value="Place Order" class="btn-face"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" visible="false" id="AddCard" runat="server">
            <div class="cardcontainer container">
                <div class="col1">
                    <div class="card">
                        <div class="front">
                            <div class="type">
                                <img class="bankid" />
                            </div>
                            <span class="chip"></span>
                            <span class="card_number">&#x25CF;&#x25CF;&#x25CF;&#x25CF; &#x25CF;&#x25CF;&#x25CF;&#x25CF; &#x25CF;&#x25CF;&#x25CF;&#x25CF; &#x25CF;&#x25CF;&#x25CF;&#x25CF; </span>
                            <div class="date"><span class="date_value">MM / YYYY</span></div>
                            <span class="fullname">FULL NAME</span>
                        </div>
                        <div class="back">
                            <div class="magnetic"></div>
                            <div class="bar"></div>
                            <span class="seccode">&#x25CF;&#x25CF;&#x25CF;</span>
                            <span class="chip"></span><span class="disclaimer">This card is property of Random Bank of Random corporation.
                                <br>
                                If found please return to Random Bank of Random corporation - 21968 Paris, Verdi Street, 34 </span>
                        </div>
                    </div>
                </div>
                <div class="col2">
                    <label>Card Number</label>
                    <asp:TextBox runat="server" class="number" type="text" ng-model="ncard" MaxLength="19"
                        onkeypress='return event.charCode >= 48 && event.charCode <= 57' ID="txtCardNumber" />
                    <label>Cardholder name</label>
                    <asp:TextBox runat="server" class="inputname" type="text" placeholder="" ID="txtCardName" />
                    <label>Expiry date</label>
                    <asp:TextBox runat="server" class="expire" type="text" placeholder="MM / YY" ID="txtExpiryDate" />
                    <label>Security Number</label>
                    <asp:TextBox runat="server" class="ccv" type="text" placeholder="CVV" MaxLength="3"
                        onkeypress='return event.charCode >= 48 && event.charCode <= 57' ID="txtCvv" />
                    <asp:Button runat="server" class="buy" Text="      Add Card" OnClick="Add_New_Card" />
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </section>
    <script src="Content/js/jquery-3.4.1.min.js"></script>
    <script src="Content/js/AddCard.js"></script>
</asp:Content>
