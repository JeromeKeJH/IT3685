using System;
using System.Data;
using System.Web;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace IT3685
{
    public partial class Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var customerId = Session["customerId"];
            if (customerId == null)
            {
                Response.Redirect("Login?Msg=Login");
            }

            customerId = customerId.ToString();
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString;
            con.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM `order` WHERE CustomerId=@customerId", con);
            cmd.Parameters.AddWithValue("@customerId", customerId);

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;

            DataSet ds = new DataSet();
            adapter.Fill(ds);

            orderRepeater.DataSource = ds;
            orderRepeater.DataBind();

            if (ds.Tables[0].Rows.Count == 0)
            {
                Empty_Orders.Style.Add("Display", "Block");
                return;
            }
        }
    }
}