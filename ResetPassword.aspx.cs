using System;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace IT3685
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Uri myUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
            string userId = HttpUtility.ParseQueryString(myUri.Query).Get("userId");
            string key = HttpUtility.ParseQueryString(myUri.Query).Get("key");

            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM user WHERE Id=@id", con);
            cmd.Parameters.AddWithValue("@Id", userId);

            con.Open();

            int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            if (count == 1)
            {
                cmd.Dispose();
                cmd = new MySqlCommand("SELECT TempKey FROM user WHERE Id=@id", con);
                cmd.Parameters.AddWithValue("@Id", userId);
                string ActualKey = cmd.ExecuteScalar().ToString();

                if (ActualKey == key.Replace(" ", "+"))
                {
                    Password.Style.Add("Display", "Block");
                    ConfirmPassword.Style.Add("Display", "Block");
                    ResetBtn.Style.Add("Display", "Block");
                    return;
                }
            }
            lblErrorMsg.Text = "The URL that you have clicked on is no longer valid. Please request for a new URL by clicking" +
                " <a href='ForgetPassword'>here</a>";
        }

        protected void OnResetPasswordClick(object sender, EventArgs e)
        {
            string password = txtPassword.Text; string confirmPassword = txtConfirmPassword.Text;
            // Verify all required fields
            if (password == "" || confirmPassword == "")
            {
                lblErrorMsg.Text = "Please Fill in all fields";
                return;
            }
            // Password Complexity check
            if (!Regex.IsMatch(password, @"^^(?=.*[A-Z])(?=.*[\d])(?=.*[a-z]).{8,}$"))
            {
                lblErrorMsg.Text = "Password must contain 1 Uppercase, 1 Lowercase, 1 Number and more than 8 characters";
                return;
            }
            // Verify that Password matches Confirm Password
            if (password != confirmPassword)
            {
                lblErrorMsg.Text = "Password does not match Confirm Password";
                return;
            }

            Uri myUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
            string userId = HttpUtility.ParseQueryString(myUri.Query).Get("userId");
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("UPDATE user SET Password=@password WHERE Id=@id", con);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@id", userId);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Response.Redirect("Login?Msg=ResetSuccess");
        }
    }
}