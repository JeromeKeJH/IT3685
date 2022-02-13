using System;
using System.Data;
using System.Configuration;
using System.Web;
using MySql.Data.MySqlClient;

namespace IT3685
{
    public partial class Order_Details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var customerId = Session["customerId"];
            if (customerId == null)
            {
                Response.Redirect("Login?Msg=Login");
            }
            Uri myUri = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
            string orderId = HttpUtility.ParseQueryString(myUri.Query).Get("id");

            MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM order_details INNER JOIN product " +
                "ON order_details.ProductId = product.Id WHERE OrderId=@orderId", con);
            cmd.Parameters.AddWithValue("@orderId", orderId);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            lblOrderNo.Text = orderId;
            ProductsRepeater.DataSource = ds;
            ProductsRepeater.DataBind();
        }
    }
}