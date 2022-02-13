using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace IT3685
{
    public partial class Wishlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var customerId = Session["customerId"];
            if (customerId == null)
            {
                Response.Redirect("Login?Msg=Login");
            }
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM wishlist INNER JOIN product " +
                "ON wishlist.ProductId = product.Id WHERE customerId=@customerId", con);
            cmd.Parameters.AddWithValue("@customerId", customerId.ToString());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            if (ds.Tables[0].Rows.Count == 0)
            {
                Empty_WishList.Style.Add("Display", "Block");
                return;
            }
            wishlistRepeater.DataSource = ds;
            wishlistRepeater.DataBind();
        }

        protected void Add_To_Cart(object sender, EventArgs e)
        {
            var customerId = Session["customerId"];
            if (customerId == null)
            {
                Response.Redirect("Login?msg=AddToCart");
            }

            string productId = ((LinkButton)sender).CommandArgument;
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

        protected void Remove(object sender, EventArgs e)
        {
            string wishlistId = ((LinkButton)sender).CommandArgument;
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            var cmd = new MySqlCommand("DELETE FROM wishlist WHERE Id=@wishlistId", con);
            cmd.Parameters.AddWithValue("@wishlistId", wishlistId);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Page.Response.Redirect(Request.Url.ToString(), true);
        }
    }
}