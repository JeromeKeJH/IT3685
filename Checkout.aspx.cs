using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;

namespace IT3685
{
    public partial class Add_Card : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var customerId = Session["customerId"];
            if (customerId == null)
            {
                Response.Redirect("Login?Msg=Login");
            }
            customerId = customerId.ToString();
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM customer WHERE Id=@customerId", con);
            cmd.Parameters.AddWithValue("@customerId", customerId);

            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            txtAddress.Text = reader["Address"].ToString();
            txtPostal.Text = reader["PostalCode"].ToString();
            txtUnitNo.Text = reader["UnitNumber"].ToString();
            reader.Close();
            reader.Dispose();
            cmd.Dispose();

            cmd = new MySqlCommand("SELECT * FROM card WHERE CustomerId=@customerId", con);
            cmd.Parameters.AddWithValue("@customerId", customerId);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            cardRepeater.DataSource = ds;
            cardRepeater.DataBind();

            cmd = new MySqlCommand("SELECT * FROM cart INNER JOIN product " +
                "ON cart.ProductId = product.Id WHERE customerId=@customerId", con);
            cmd.Parameters.AddWithValue("@customerId", customerId.ToString());
            adapter.Dispose();
            adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet orderds = new DataSet();
            adapter.Fill(orderds);

            orderRepeater.DataSource = orderds;
            orderRepeater.DataBind();

            float total = 0;
            foreach (DataRow row in orderds.Tables[0].Rows)
            {
                total += float.Parse(row.ItemArray[7].ToString()) * Convert.ToInt32(row.ItemArray[3]);
            }
            lblTotal.Text = total.ToString("n2");
        }

        protected void Add_New_Card(object sender, EventArgs e)
        {
            var customerId = Session["customerId"];
            if (customerId == null)
            {
                Response.Redirect("Login?Msg=Login");
            }
            customerId = customerId.ToString();
            var cardNumber = txtCardNumber.Text;
            cardNumber = cardNumber.Replace(" ", "");
            var cardName = txtCardName.Text;
            var expiryDate = txtExpiryDate.Text;
            expiryDate = expiryDate.Replace(" ", "");
            var cvv = txtCvv.Text;

            if (cardNumber == "" || cardName == "" || expiryDate == "" || cvv == "")
            {
                errorMsgDiv.Visible = true;
                errorMessage.Text = "Add new card error: Please enter all required values";
                return;
            }
            else if (cardNumber.Length != 16)
            {
                errorMsgDiv.Visible = true;
                errorMessage.Text = "Add new card error: Please enter a 16 digit card number";
                return;
            }
            else if (GetCardType(cardNumber) == "")
            {
                errorMsgDiv.Visible = true;
                errorMessage.Text = "Add new card error: Invalid Card Number";
                return;
            }
            else if (!Regex.IsMatch(expiryDate, @"^(0[1-9]|1[012])[/](2[2-9]|3[012])"))
            {
                errorMsgDiv.Visible = true;
                errorMessage.Text = "Add new card error: Please ensure your card is not expired and expires before 2033";
                return;
            }
            else if (cvv.Length != 3)
            {
                errorMsgDiv.Visible = true;
                errorMessage.Text = "Add new card error: Please enter a valid Security Number (CVV)";
                return;
            }

            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM card WHERE CardNumber=@cardNumber AND CustomerId = @customerId", con);
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
            cmd.Parameters.AddWithValue("@customerId", customerId);

            con.Open();
            int count = int.Parse(cmd.ExecuteScalar().ToString());

            if (count > 0)
            {
                errorMsgDiv.Visible = true;
                errorMessage.Text = $"Add new card error: The card number ending with {cardNumber.ToString().Substring(12)} already exists";
                return;
            }

            cmd.Dispose();
            cmd = new MySqlCommand("INSERT INTO card(CustomerId, Name, CardNumber, ExpiryMonth, ExpiryYear, CVV) " +
                "VALUES(@customerId, @name, @cardNumber, @expiryMonth, @expiryYear, @cvv)", con);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@name", cardName);
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);

            var expiryDateList = expiryDate.Split('/');
            cmd.Parameters.AddWithValue("@expiryMonth", expiryDateList[0]);
            cmd.Parameters.AddWithValue("@expiryYear", expiryDateList[1]);
            cmd.Parameters.AddWithValue("@cvv", cvv);

            cmd.ExecuteNonQuery();
            con.Close();
            Page.Response.Redirect(Request.Url.ToString(), true);
        }

        protected void Place_Order(object sender, EventArgs e)
        {
            
            foreach (RepeaterItem row in cardRepeater.Items)
            {
                var test = (HtmlInputRadioButton)row.FindControl("Radio1");
            }
        }

        protected string GetCardType(string number)
        {
            if (
                Convert.ToInt32(number.Substring(0, 2)) > 50 &&
                Convert.ToInt32(number.Substring(0, 2)) < 56
            )
            {
                return "mastercard";
            }
            else if (Convert.ToInt32(number.Substring(0, 1)) == 4)
            {
                return "visa";
            }
            else if (
              Convert.ToInt32(number.Substring(0, 2)) == 36 ||
              Convert.ToInt32(number.Substring(0, 2)) == 38 ||
              Convert.ToInt32(number.Substring(0, 2)) == 39
          )
            {
                return "diners";
            }
            else if (
              Convert.ToInt32(number.Substring(0, 2)) == 34 ||
              Convert.ToInt32(number.Substring(0, 2)) == 37
          )
            {
                return "amex";
            }
            else if (Convert.ToInt32(number.Substring(0, 2)) == 65)
            {
                return "discover";
            }
            return "";
        }

        protected void OpenAddCard_Click(object sender, EventArgs e)
        {
            if (!AddCard.Visible)
            {
                AddCard.Visible = true;
            }
            else
            {
                AddCard.Visible = false;
            }
        }
    }
}