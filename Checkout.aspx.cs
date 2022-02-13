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
            if (!IsPostBack)
            {
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


                reader = cmd.ExecuteReader();
                RadioButtonList1.Items.Clear();
                while (reader.Read())
                {
                    string text = "<div class='" + GetCardType(reader["CardNumber"].ToString()) + "' id='CardType' style='display: inline-block;'>";
                    text += "<div class='credit-card-last4'>";
                    text += reader["CardNumber"].ToString().Substring(12);
                    text += "</div>";
                    text += "<div class='credit-card-expiry'>";
                    text += "</div>";

                    RadioButtonList1.Items.Add(new ListItem(text, reader["CardNumber"].ToString() + "1"));
                }
                reader.Close();
                reader.Dispose();

                MySqlDataAdapter adapter;
                cmd.Dispose();

                cmd = new MySqlCommand("SELECT * FROM cart INNER JOIN product " +
                    "ON cart.ProductId = product.Id WHERE customerId=@customerId", con);
                cmd.Parameters.AddWithValue("@customerId", customerId.ToString());
                //adapter.Dispose();
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
            var customerId = Session["customerId"];
            if (customerId == null)
            {
                Response.Redirect("Login?Msg=Login");
            }
            customerId = customerId.ToString();

            if (RadioButtonList1.SelectedValue == "")
            {
                errorMsgDiv.Visible = true;
                errorMessage.Text = "Please select a payment card";
                return;
            }
            if (txtAddress.Text == "" || txtUnitNo.Text == "" || txtPostal.Text == "")
            {
                errorMsgDiv.Visible = true;
                errorMessage.Text = "Please enter all fields of Shipping Address";
                return;
            }

            var cardNumber = RadioButtonList1.SelectedValue.Remove(RadioButtonList1.SelectedValue.Length - 1);
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM card WHERE CardNumber=@cardNumber AND customerId=@customerId", con);
            cmd.Parameters.AddWithValue("@cardNumber", cardNumber);
            cmd.Parameters.AddWithValue("@customerId", customerId);

            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            cmd.Dispose();
            var cardId = reader["Id"].ToString();
            var expiryMonth = reader["ExpiryMonth"].ToString();
            var expiryYear = reader["ExpiryYear"].ToString();
            var cvv = reader["CVV"].ToString();
            reader.Close();
            reader.Dispose();

            cmd = new MySqlCommand("SELECT COUNT(*) FROM bank_card WHERE CardNumber=@cardNumber AND ExpiryMonth=@expiryMonth AND " +
                "ExpiryYear=@expiryYear AND CVV=@cvv", con);
            cmd.Parameters.AddWithValue("@CardNumber", cardNumber);
            cmd.Parameters.AddWithValue("@expiryMonth", expiryMonth);
            cmd.Parameters.AddWithValue("@expiryYear", expiryYear);
            cmd.Parameters.AddWithValue("@cvv", cvv);

            int Count = int.Parse(cmd.ExecuteScalar().ToString());
            if (Count < 1)
            {
                errorMsgDiv.Visible = true;
                errorMessage.Text = "Card Error: Selected card is not a valid card";
                return;
            }

            cmd.Dispose();
            cmd = new MySqlCommand("SELECT * FROM bank_card WHERE CardNumber=@cardNumber AND ExpiryMonth=@expiryMonth AND " +
                "ExpiryYear=@expiryYear AND CVV=@cvv", con);
            cmd.Parameters.AddWithValue("@CardNumber", cardNumber);
            cmd.Parameters.AddWithValue("@expiryMonth", expiryMonth);
            cmd.Parameters.AddWithValue("@expiryYear", expiryYear);
            cmd.Parameters.AddWithValue("@cvv", cvv);

            reader = cmd.ExecuteReader();
            reader.Read();
            if (float.Parse(reader["Balance"].ToString()) < float.Parse(lblTotal.Text.ToString()))
            {
                errorMsgDiv.Visible = true;
                errorMessage.Text = "Error: You have insufficient balance in your bank";
                return;
            }
            reader.Close();
            // Create new order
            cmd.Dispose();
            var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            cmd = new MySqlCommand("INSERT INTO `order` (CustomerId, Address, PostalCode, UnitNumber, Subtotal, CardId, OrderDate) " +
                "VALUES(@customerId, @address, @postalCode, @unitNumber, @subtotal, @cardId, @orderDate)", con);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@postalCode", txtPostal.Text);
            cmd.Parameters.AddWithValue("@unitNumber", txtUnitNo.Text);
            cmd.Parameters.AddWithValue("@subtotal", lblTotal.Text);
            cmd.Parameters.AddWithValue("@cardId", cardId);
            cmd.Parameters.AddWithValue("@orderDate", now);
            cmd.ExecuteNonQuery();

            // Retrieve orderId from newly inserted order
            cmd.Dispose();
            cmd = new MySqlCommand("SELECT Id FROM `order` WHERE CustomerId=@customerId AND cardId=@cardId AND OrderDate=@orderDate", con);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.Parameters.AddWithValue("@cardId", cardId);
            cmd.Parameters.AddWithValue("@orderDate", now);
            string orderId = cmd.ExecuteScalar().ToString();

            // Retrieve all items from the cart
            cmd.Dispose();
            cmd = new MySqlCommand("SELECT * FROM cart INNER JOIN product " +
                "ON cart.ProductId = product.Id WHERE customerId=@customerId", con);
            cmd.Parameters.AddWithValue("@customerId", customerId);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            // Create new Order Details 
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                var subtotal = float.Parse(row.ItemArray[7].ToString()) * Convert.ToInt32(row.ItemArray[3]);
                cmd.Dispose();
                cmd = new MySqlCommand("INSERT INTO order_details(OrderId, ProductId, Quantity, Subtotal) VALUES " +
                    "(@orderId, @productId, @quantity, @subtotal)", con);
                cmd.Parameters.AddWithValue("@orderId", orderId);
                cmd.Parameters.AddWithValue("@productId", row.ItemArray[2].ToString());
                cmd.Parameters.AddWithValue("@quantity", row.ItemArray[3].ToString());
                cmd.Parameters.AddWithValue("@subtotal", subtotal);
                cmd.ExecuteNonQuery();
            }

            cmd.Dispose();
            cmd = new MySqlCommand("DELETE FROM cart WHERE CustomerId=@customerId", con);
            cmd.Parameters.AddWithValue("@customerId", customerId);
            cmd.ExecuteNonQuery();
            con.Close();

            Response.Redirect("Orders");
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