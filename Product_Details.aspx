<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product_Details.aspx.cs" Inherits="IT3685.Product_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="single-product">
        <div class="container">
            <div class="row">
                <div class="col-md-5">
                    <asp:Image runat="server" ID="prodImg" height="300" />
                </div>
                <div class="col-md-7">
                    <div class="right-content">
                        <p><a href="Product">&#8617; Continue Shopping</a></p>
                        <br />
                        <h4><strong><asp:Label runat="server" ID="prodName"></asp:Label></strong></h4>
                        <br />
                        <h6>$<asp:Label runat="server" ID="prodPrice"></asp:Label></h6>
                        <br />
                        <p><asp:Label runat="server" ID="prodDesc"></asp:Label> </p>
                    </div>
                    <br />

                    <asp:Button runat="server" Text="Add to Cart" style="display: inline-block; vertical-align: top" OnClick="Add_To_Cart" />
                    <asp:Button runat="server" Text="   Wishlist   " style="display: inline-block; vertical-align: top"/>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
