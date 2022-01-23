<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="IT3685.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- product section -->
    <section class="product_section layout_padding">
        <div class="container">
            <div class="heading_container heading_center">
                <h2>Our <span>products</span>
                </h2>
            </div>

            <div class="row">
                <asp:Repeater runat="server" ID="productsRepeater">
                    <ItemTemplate>
                        <div class="col-sm-6 col-md-4 col-lg-3">
                            <div class="box">
                                <div class="option_container">
                                    <div class="options">

                                        <a href="" class="option1">
                                            <%# Eval("Name") %>
                                        </a>
                                        <a href="" class="option3">Wishlist
                                        </a>
                                        <asp:LinkButton runat="server" OnClick="Add_To_Cart"
                                            CssClass="option2" CommandArgument='<%# Eval("Id") %>'>
                                             Add to Cart
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div class="img-box">
                                    <img src='<%# Eval("Img") %>' alt="">
                                </div>
                                <div class="detail-box">
                                    <h5><%# Eval("Name") %>
                                    </h5>
                                    <h6>$<%# Eval("Price") %>
                                    </h6>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>
</asp:Content>
