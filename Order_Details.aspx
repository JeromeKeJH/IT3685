<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Order_Details.aspx.cs" Inherits="IT3685.Order_Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Order Details</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <section class="product_section layout_padding">
        <div class="container">
            <div class="row">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 main-col">
                    <div class="heading_container heading_center">
                        <h2>Order <span>#<asp:Label runat="server" ID="lblOrderNo"></asp:Label></span>
                        </h2>
                    </div>

                    <div class="wishlist-table table-content table-responsive">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th class="product-name text-center alt-font" colspan="3">Product</th>
                                    <th class="product-price text-center alt-font">Quantity</th>
                                    <th class="product-price text-center alt-font">Subtotal</th>
                                    <th class="product-subtotal text-center alt-font"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="ProductsRepeater">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="product-remove text-center" valign="middle"></td>
                                            <td class="product-thumbnail text-center">
                                                <a href='<%# "Product_Details?id=" + Eval("Id") %>'>
                                                    <asp:Image runat="server" ImageUrl='<%# Eval("Img") %>' Height="100" />
                                            </td>
                                            <td class="product-name">
                                                <h4 class="no-margin"><a href='<%# "Product_Details?id=" + Eval("Id") %>'><%# Eval("Name") %></a></h4>
                                            </td>
                                            <td class="product-price text-center"><%# Eval("Quantity") %></td>
                                            <td class="product-price text-center"><span class="amount">$<%# Eval("Subtotal") %></span></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>

                        <div class="basket-product" runat="server" id="Empty_WishList" style="display: none;">
                            <br />
                            <br />
                            <br />
                            <h2 style="text-align: center;">There are no Items in your Wishlist. 
                            </h2>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
