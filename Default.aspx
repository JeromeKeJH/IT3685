<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IT3685._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

         <section class="slider_section ">
            <div class="slider_bg_box">
               <img src="Content/images/recycling-background.jpg" alt="">
            </div>
            <div id="customCarousel1" class="carousel slide" data-ride="carousel">
               <div class="carousel-inner">
                  <div class="carousel-item active">
                     <div class="container ">
                        <div class="row">
                           <div class="col-md-7 col-lg-6 ">
                              <div class="detail-box">
                                 <h1>
                                    <span>
                                    Reduce Waste!  
                                    </span>
                                    <br>
                                    Save the Planet
                                 </h1>
                                 <p>
                                    We are a company who collects recycleable materials and construct them into new and usable items! Feel good when you buy from us as
                                     you will be doing your part in saving the planet!
                                 </p>
                                 <div class="btn-box">
                                    <a href="Product" class="btn1">
                                    Shop Now
                                    </a>
                                 </div>
                              </div>
                           </div>
                        </div>
                     </div>
                  </div>
                  <div class="carousel-item ">
                     <div class="container ">
                        <div class="row">
                           <div class="col-md-7 col-lg-6 ">
                              <div class="detail-box">
                                 <h1>
                                    <span>
                                    Go Green!
                                    </span>
                                    <br>
                                    Love the planet
                                 </h1>
                                 <p>
                                    Be part of our green community! Help us help yourselves through the 3Rs: Reduce, Reuse, Recycle!
                                     Our company puts our effort towards reducing wastage! Do your part in saving earth today!
                                 </p>
                                 <div class="btn-box">
                                    <a href="Product" class="btn1">
                                    Shop Now
                                    </a>
                                 </div>
                              </div>
                           </div>
                        </div>
                     </div>
                  </div>
               </div>
               <div class="container">
                  <ol class="carousel-indicators">
                     <li data-target="#customCarousel1" data-slide-to="0" class="active"></li>
                     <li data-target="#customCarousel1" data-slide-to="1"></li>
                  </ol>
               </div>
            </div>
         </section>

</asp:Content>
