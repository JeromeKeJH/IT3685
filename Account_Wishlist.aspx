<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Account_Wishlist.aspx.cs" Inherits="IT3685.Wishlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="product_section layout_padding">
        <div class="container">
            <div class="row">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 main-col">
                    <div class="wishlist-table table-content table-responsive">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th class="product-name text-center alt-font" colspan="3">Product</th>
                                    <th class="product-price text-center alt-font">Unit Price</th>
                                    <th class="product-subtotal text-center alt-font"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="wishlistRepeater">
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
                                            <td class="product-price text-center"><span class="amount">$<%# Eval("Price") %></span></td>
                                            <td class="product-subtotal text-center">
                                                <asp:LinkButton runat="server" OnClick="Add_To_Cart" CommandArgument='<%# Eval("ProductId") %>'>
                                                        <strong>Add To Cart</strong>
                                                </asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton runat="server" OnClick="Remove" CommandArgument='<%# Eval("Id") %>'>
                                                        <strong>Remove</strong>
                                                    </asp:LinkButton>
                                            </td>
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
