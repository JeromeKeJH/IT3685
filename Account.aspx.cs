using System;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.IO;
using MySql.Data.MySqlClient;

namespace IT3685
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var id = Session["customerId"];
            if (id == null)
            {
                Response.Redirect("Login?Msg=Login");
            }

            Uri myUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
            string msg = HttpUtility.ParseQueryString(myUri.Query).Get("msg");
            if (msg == "Success")
            {
                alertSuccess.Style.Add("display", "block");
            }

                if (!Page.IsPostBack)
            {
                MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM customer WHERE Id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                lblName.Text = $"{reader["FirstName"]} {reader["LastName"]}";
                lblEmail.Text = reader["EmailAddress"].ToString();
                lblGender.Text = reader["Gender"].ToString() == "M" ? "Male" : "Female";
                txtFirstName.Text = reader["FirstName"].ToString();
                txtLastName.Text = reader["LastName"].ToString();
                txtPhone.Text = reader["ContactNumber"].ToString();
                txtAddress.Text = reader["Address"].ToString();
                txtUnitNo.Text = reader["UnitNumber"].ToString();
                txtPostalCode.Text = reader["PostalCode"].ToString();
                imgProfile.ImageUrl = reader["ImageUrl"].ToString() == "" ? "Content/Images/Profiles/default.jpg" : reader["ImageUrl"].ToString();

                reader.Close();
                con.Close();
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            var customerId = Session["customerId"];
            if (customerId == null)
            {
                Response.Redirect("Login?Msg=Login");
            }
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            con.Open();

            // Get the name of the file that is posted.
            if (oFile.Value != "")
            {
                string strFileName;
                string strFilePath;
                string strFolder;
                strFolder = Server.MapPath("./") + "Content\\Images\\Profiles\\";
                strFileName = oFile.PostedFile.FileName;
                strFileName = Path.GetFileName(strFileName);
                strFileName = customerId.ToString() + "." + strFileName.Split('.')[1];
                // Create the directory if it does not exist.
                if (!Directory.Exists(strFolder))
                {
                    Directory.CreateDirectory(strFolder);
                }
                // Save the uploaded file to the server.
                strFilePath = strFolder + strFileName;
                oFile.PostedFile.SaveAs(strFilePath);

                string image = $"Content/Images/Profiles/{strFileName}";
                MySqlCommand cmd = new MySqlCommand("UPDATE customer SET FirstName=@firstName, LastName=@lastName, " +
                    "ContactNumber=@contactNumber, Address=@address, PostalCode=@postalCode, UnitNumber=@unitNumber, ImageUrl=@imageUrl " +
                    "WHERE Id=@id", con);
                cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@contactNumber", txtPhone.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@postalCode", txtPostalCode.Text);
                cmd.Parameters.AddWithValue("@unitNumber", txtUnitNo.Text);
                cmd.Parameters.AddWithValue("@imageUrl", image);
                cmd.Parameters.AddWithValue("@id", customerId.ToString());

                cmd.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand("UPDATE customer SET FirstName=@firstName, LastName=@lastName, " +
                    "ContactNumber=@contactNumber, Address=@address, PostalCode=@postalCode, UnitNumber=@unitNumber " +
                    "WHERE Id=@id", con);
                cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@contactNumber", txtPhone.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@postalCode", txtPostalCode.Text);
                cmd.Parameters.AddWithValue("@unitNumber", txtUnitNo.Text);
                cmd.Parameters.AddWithValue("@id", customerId.ToString());

                cmd.ExecuteNonQuery();
            }
            con.Close();
            string url = Page.Request.Url.ToString().Contains("Msg=Success") ? Page.Request.Url.ToString() :
                Page.Request.Url.ToString() + "?Msg=Success";
                
            Page.Response.Redirect(url, true);
        }
    }
}