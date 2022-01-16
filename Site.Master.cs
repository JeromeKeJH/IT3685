using System;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace IT3685
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["customerId"] != null)
            {
                Login.Style.Add("Display", "None");
                Signup.Style.Add("Display", "None");
                User.Style.Add("Display", "Block");
            }
            string url = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).ToString();
            var urlList = url.Split('/');
            if (urlList.Last() == "Product")
            {
                Product.Attributes.Add("Class", "nav-item active");
            }
            else if (urlList.Last() == "Login")
            {
                Login.Attributes.Add("Class", "nav-item active");
            }
            else if (urlList.Last() == "Signup")
            {
                Signup.Attributes.Add("Class", "nav-item active");
            }
            else
            {
                Home.Attributes.Add("Class", "nav-item active");
            }
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("/");
        }
    }
}