<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product_Details.aspx.cs" Inherits="IT3685.Product_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="product_section layout_padding">
    <div class="single-product">
        <div class="container">
            <div class="heading_container heading_center">
                <h2>Product <span>Details</span>
                </h2>
            </div>

            <div class="row">
                <div class="col-md-5">
                    <asp:Image runat="server" ID="prodImg" height="300" />
                </div>
                <div class="col-md-7">
                    <div class="right-content">
                        <br />
                        <h4><strong><asp:Label runat="server" ID="prodName"></asp:Label></strong></h4>
                        <br />
                        <h6>$<asp:Label runat="server" ID="prodPrice"></asp:Label></h6>
                        <br />
                        <p><asp:Label runat="server" ID="prodDesc"></asp:Label> </p>
                    </div>
                    <br />
                    <p style="display:inline-block; margin-right: 10px">Quantity: </p>
                    <asp:TextBox type="number" id="TxtQuantity" runat="server" Text="1" width="50" CssClass="border" style="padding-left: 7px"></asp:TextBox>
                    <br /> <br />
                    <asp:Button runat="server" Text="Add to Cart" style="display: inline-block; vertical-align: top" OnClick="Add_To_Cart" />
                    <asp:Button runat="server" Text="   Wishlist   " style="display: inline-block; vertical-align: top"/>
                    <br /><br />
                    <p><a href="Product">&#8617; Continue Shopping</a></p>
                </div>
            </div>
        </div>
    </div>
        </section>
</asp:Content>
