<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="IT3685.Orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Orders</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="product_section layout_padding">
        <div class="container">
            <div class="col-12 tm-block-col">
                <div class="tm-bg-primary tm-block tm-block-taller tm-block-scroll">
                    <div class="heading_container heading_center">
                        <h2>My <span>Orders</span>
                        </h2>
                    </div>

                    <table class="table">
                        <thead>
                            <tr>
                                <th scope="col">ORDER NO.</th>
                                <th scope="col">ORDER DATE</th>
                                <th scope="col" col-span="2">ADDRESS</th>
                                <th scope="col">SUBTOTAL</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater runat="server" ID="orderRepeater">
                                <ItemTemplate>
                                    <tr>
                                        <th scope="row"><a href='<%# "Order_Details?id=" + Eval("Id") %>'><b>#<%# Eval("Id") %></b></a></th>
                                        <td><%# Eval("OrderDate") %></td>
                                        <td col-span="2"><%# Eval("Address") + ", " + Eval("UnitNumber") + ", Singapore " + Eval("PostalCode") %></td>
                                        <td>$<%# Eval("Subtotal") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                        </tbody>
                    </table>

                    <div class="basket-product" runat="server" id="Empty_Orders" style="display: none;">
                        <br />
                        <h2 style="text-align: center;">YOU HAVE NO ORDERS
                        </h2>
                        <br />
                    </div>
                </div>
            </div>

        </div>
    </section>
</asp:Content>
