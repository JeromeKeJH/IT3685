<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="IT3685.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/css/Cart.css" />
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Cart</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="basket">
            <div class="basket-labels">
                <ul>
                    <li class="item item-heading checkout-li">Item</li>
                    <li class="price checkout-li">Price</li>
                    <li class="quantity checkout-li">Quantity</li>
                    <li class="subtotal checkout-li">Subtotal</li>
                </ul>
            </div>

            <asp:Repeater runat="server" ID="itemRepeater">
                <ItemTemplate>
                    <div class="basket-product">
                        <div class="item">
                            <div class="product-image">
                                <img class="checkout-img" src='<%# Eval("Img") %>' alt="Placholder Image">
                            </div>
                            <div class="product-details">
                                <h4><strong>
                                    <%# Eval("Name") %></h4>
                                </strong> 
                            </div>
                        </div>
                        <div class="price"><%# Eval("Price") %></div>
                        <div class="quantity">
                            <span class="item-quantity"><%# Eval("Quantity") %></span>
                        </div>
                        <div class="subtotal">
                            <%# Convert.ToInt32(Eval("Quantity")) * Convert.ToInt32(Eval("Price")) %>
                        </div>
                        <div class="remove">
                            <button>
                                <asp:LinkButton runat="server" OnClick="Remove_Item" CommandArgument='<%# Eval("ProductId") %>'>
                                    Remove
                                </asp:LinkButton>
                            </button>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <div class="basket-product" runat="server" id="Empty_Cart" style="display: none;">
                <br />
                <h2 style="text-align: center;">There are no Items in your cart. 
                </h2>
                <br />
            </div>

        </div>

        <aside>
            <div class="summary">
                <div class="summary-total-items"><span class="total-items"></span>Items in your Bag</div>
                <div class="summary-subtotal">
                    <div class="subtotal-title">Subtotal</div>
                    <div class="subtotal-value final-value" id="basket-subtotal"><asp:Label runat="server" ID="lblSubtotal" Text="0"></asp:Label></div>
                </div>
                <div class="summary-delivery">
                    <div class="subtotal-title">Local Postage</div>
                    <div class="subtotal-value final-value" id="basket-summary-delivery">0</div>
                </div>
                <div class="summary-total">
                    <div class="total-title">Total</div>
                    <div class="total-value final-value" id="basket-total"><asp:Label runat="server" ID="lblTotal" Text="0"></asp:Label></div>
                </div>
                <div class="summary-checkout">
                    <a href="Checkout.aspx" class="checkout-cta btn"><strong>Go to Checkout</strong></a>
                </div>
            </div>
        </aside>
    </main>
</asp:Content>
