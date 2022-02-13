using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace IT3685
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var customerId = Session["customerId"];
            if (customerId == null)
            {
                Response.Redirect("Login?Msg=Login");
            }
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM cart INNER JOIN product " +
                "ON cart.ProductId = product.Id WHERE customerId=@customerId", con);
            cmd.Parameters.AddWithValue("@customerId", customerId.ToString());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            float total = 0;
            if (ds.Tables[0].Rows.Count == 0)
            {
                Empty_Cart.Style.Add("Display", "Block");
                return;
            }
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                total += Convert.ToInt32(row.ItemArray[3]) * float.Parse(row.ItemArray[7].ToString());
            }
            lblTotal.Text = total.ToString("n2");
            lblSubtotal.Text = total.ToString("n2");
            itemRepeater.DataSource = ds;
            itemRepeater.DataBind();
        }

        protected void Remove_Item(object sender, EventArgs e)
        {
            var customerId = Session["CustomerId"];
            if (customerId == null)
            {
                Response.Redirect("Login?Msg=Login");
            }
            var productId = ((LinkButton)sender).CommandArgument;
            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("DELETE FROM cart WHERE productId=@productId AND customerId=@customerId", con);
            cmd.Parameters.AddWithValue("@productId", productId);
            cmd.Parameters.AddWithValue("@customerId", customerId.ToString());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Page.Response.Redirect(Request.Url.ToString(), true);
        }
    }
}