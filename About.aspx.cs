using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace IT3685
{
    public partial class About : Page
    {
        protected void Updates(object sender, EventArgs e)
        {
            string datafromfrontend = "31";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["IT3685"].ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM table WHERE tableCoumn = @parameter", con);
            cmd.Parameters.AddWithValue("@parameter", datafromfrontend);

            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = cmd;
            adapter.Fill(ds);
        }
    }
}