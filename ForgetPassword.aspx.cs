using System;
using System.Runtime;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Linq;
using MySql.Data.MySqlClient;
using IT3685.App_Code;

namespace IT3685
{
    public partial class ForgetPassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void OnForgetPasswordClick(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM user WHERE EmailAddress=@emailAddress", con);
            cmd.Parameters.AddWithValue("@emailAddress", email);

            con.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            if (count == 1)
            {
                string key = GenerateKey();
                cmd.Dispose();
                cmd = new MySqlCommand("UPDATE user SET TempKey=@tempKey WHERE EmailAddress=@emailAddress", con);
                cmd.Parameters.AddWithValue("@emailAddress", email);
                cmd.Parameters.AddWithValue("@tempKey", key);
                cmd.ExecuteNonQuery();

                // Retrieve Url
                Uri uri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
                var noLastSegment = string.Format("{0}://{1}", uri.Scheme, uri.Authority);
                for (int i = 0; i < uri.Segments.Length - 1; i++)
                {
                    noLastSegment += uri.Segments[i];
                }
                noLastSegment = noLastSegment.Trim("/".ToCharArray());

                cmd = new MySqlCommand("SELECT Id FROM user WHERE EmailAddress=@emailAddress", con);
                cmd.Parameters.AddWithValue("@emailAddress", email);

                string userId = cmd.ExecuteScalar().ToString();
                string url = noLastSegment + $"/ResetPassword?userId={userId}&key={key}";
                string subject = "Password Reset Request";
                string message = "You have requested to reset your password. Please click on this to continue" +
                    " <a href=\"" + url + "\">link</a><br /><br />";

                message += HttpUtility.HtmlEncode(@"Or copy the following link onto a browser: " + url);
                Email.SendEmail(email, subject, message);
            }

            con.Close();
            lblErrorMsg.Style.Add("Color", "Green");
            lblErrorMsg.Text = "An Email has been sent to you if the entered Email Address exists";
        }

        private static string GenerateKey()
        {
            System.Security.Cryptography.AesCryptoServiceProvider crypto = new System.Security.Cryptography.AesCryptoServiceProvider();
            crypto.KeySize = 128;
            crypto.BlockSize = 128;
            crypto.GenerateKey();

            byte[] keyGenerated = crypto.Key;
            string Key = Convert.ToBase64String(keyGenerated);

            return Key;
        }
    }
}