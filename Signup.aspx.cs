using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace IT3685
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnRegisterClick(object sender, EventArgs e)
        {
            string firstName = txtFirstname.Text; string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            string contact = txtContactNumber.Text;
            string dateOfBirth = txtDOB.Text; string gender = DropDownGender.SelectedValue;
            string password = txtPassword.Text; string confirmPassword = txtConfirmPassword.Text;

            // Verify all required fields are not empty
            List<string> requiredValues = new List<string>(new string[] { firstName, email, contact, dateOfBirth, gender, password, confirmPassword });
            if (requiredValues.Any(x => x == ""))
            {
                lblErrorMsg.Text = "Please Fill In All Required Fields";
                return;
            }

            // Verify Email does not already exist
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM user WHERE EmailAddress=@email", con);
            cmd.Parameters.AddWithValue("@email", email);

            con.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            if (count > 0)
            {
                lblErrorMsg.Text = "Your Email Address already exists, please sign in instead";
                return;
            }

            // Verify Contact Number is a Singapore Number
            if (!Regex.IsMatch(contact, @"8|9\d{7}"))
            {
                lblErrorMsg.Text = "Please Enter a valid Singapore Number";
                return;
            }
            // Verify Email is a Real Email
            if (!Regex.IsMatch(email, @"[^@]+@[^@]+.[a-zA-Z]{2,6}"))
            {
                lblErrorMsg.Text = "Please Enter a valid Email Address";
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

            // Insert into User Table
            cmd.Dispose();
            cmd = new MySqlCommand("INSERT INTO user(EmailAddress, Password) VALUES (@emailAddress, @password)", con);
            cmd.Parameters.AddWithValue("@emailAddress", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.ExecuteNonQuery();

            // Retrieve User Id from the inserted record
            cmd.Dispose();
            cmd = new MySqlCommand("SELECT Id FROM user WHERE EmailAddress=@emailAddress", con);
            cmd.Parameters.AddWithValue("@emailAddress", email);
            string userId = cmd.ExecuteScalar().ToString();

            // Insert into Customer Table
            cmd.Dispose();
            cmd = new MySqlCommand("INSERT INTO customer(UserId, FirstName, LastName, Gender, EmailAddress, DateOfBirth, ContactNumber)" +
                "Values(@userId, @firstName, @lastName, @gender, @emailAddress, @dateOfBirth, @contactNumber)", con);
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@emailAddress", email);
            cmd.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
            cmd.Parameters.AddWithValue("@contactNumber", contact);
            cmd.ExecuteNonQuery();

            Response.Redirect("Login?msg=RegisterSuccess"); 
        }
    }
}