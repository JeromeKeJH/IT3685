using System;
using System.Web;
using System.Web.UI;

using MySql.Data.MySqlClient;

using System.Configuration;

namespace IT3685
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Uri myUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
            string msg = HttpUtility.ParseQueryString(myUri.Query).Get("msg");
            if (msg == "RegisterSuccess")
            {
                lblErrorMsg.Style.Add("color", "green");
                lblErrorMsg.Text = "Registration was successful, please proceed to log in";
            }
            else if (msg == "ResetSuccess")
            {
                lblErrorMsg.Style.Add("color", "green");
                lblErrorMsg.Text = "Password reset was successful, please proceed to log in";
            }
            else if(msg == "AddToCart")
            {
                lblErrorMsg.Style.Add("color", "red");
                lblErrorMsg.Text = "Please log in to Add to Cart";
            }
            else if(msg == "AddToWishlist")
            {
                lblErrorMsg.Style.Add("color", "red");
                lblErrorMsg.Text = "Please log in to Add to Wishlist";
            }
            else if(msg == "Login")
            {
                lblErrorMsg.Style.Add("color", "red");
                lblErrorMsg.Text = "Please log in to proceed";
            }
        }

        protected void OnLoginClick(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString;
            con.Open();

            // Verification if Email Address exists
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM user WHERE EmailAddress=@email", con);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);

            int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            if (count == 1)
            {
                // Verification if Password is correct
                cmd.Dispose();
                cmd = new MySqlCommand("SELECT * FROM user WHERE EmailAddress=@email", con);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);

                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                string password = reader["Password"].ToString();
                string userId = reader["Id"].ToString();
                reader.Close();

                if (password == txtPassword.Text)
                {
                    lblErrorMsg.Text = "";
                    cmd.Dispose();
                    cmd = new MySqlCommand("SELECT Id FROM customer WHERE UserId=@userId", con);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    string customerId = cmd.ExecuteScalar().ToString();
                    Session["customerId"] = customerId;
                    Response.Redirect("/"); 
                }
            }
            lblErrorMsg.Style.Add("color", "red");
            lblErrorMsg.Text = "Incorrect Email or Password";
        }
    }
}