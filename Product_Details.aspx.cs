using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace IT3685
{
    public partial class Product_Details : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Uri myUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
            string id = HttpUtility.ParseQueryString(myUri.Query).Get("id");

            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM product WHERE Id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            prodImg.ImageUrl = reader["Img"].ToString();
            prodName.Text = reader["Name"].ToString();
            prodPrice.Text = reader["Price"].ToString();
            prodDesc.Text = reader["Description"].ToString();
            reader.Close();
            con.Close();
        }

        protected void Add_To_Cart(object sender, EventArgs e)
        {
            var customerId = Session["customerId"];
            if (customerId == null)
            {
                Response.Redirect("Login?msg=AddToCart");
            }

            Uri myUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
            string productId = HttpUtility.ParseQueryString(myUri.Query).Get("id");
            customerId = customerId.ToString();
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM cart WHERE CustomerId=@customerId AND ProductId=@productId", con);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@productId", productId);

            con.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            cmd.Dispose();

            if (count == 0)
            {
                cmd = new MySqlCommand("INSERT INTO cart(`CustomerId`, `ProductId`, `Quantity`) " +
                    "VALUES(@customerId, @productId, 1)", con);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd = new MySqlCommand("SELECT Quantity FROM cart WHERE CustomerId=@customerId AND ProductId=@productId", con);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@productId", productId);
                int quantity = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                cmd.Dispose();

                cmd = new MySqlCommand("UPDATE cart SET Quantity=@quantity WHERE CustomerId=@customerId AND ProductId=@productId", con);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@quantity", quantity + 1);
                cmd.ExecuteNonQuery();
            }

            cmd.Dispose();
            cmd = new MySqlCommand("SELECT Name FROM product WHERE Id=@productId", con);
            cmd.Parameters.AddWithValue("@productId", productId);
            string name = cmd.ExecuteScalar().ToString();
            con.Close();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "none",
                $"alert('{name} has been added to your cart');", true);
        }
    }
}