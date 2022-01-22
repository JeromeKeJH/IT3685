using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace IT3685
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM product", con);
            con.Open();
            
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            productsRepeater.DataSource = dataSet;
            productsRepeater.DataBind();
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
                cmd.Parameters.AddWithValue("@quantity", quantity+1);
                cmd.ExecuteNonQuery();
            }
        }
    }
}