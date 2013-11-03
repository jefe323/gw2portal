using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;

namespace gw2portal
{
    public partial class test : System.Web.UI.Page
    {
        public string first="";
        public string second="";
        public string third="";

        protected void Page_Load(object sender, EventArgs e)
        {
            IList<string> urlSegments = Request.GetFriendlyUrlSegments();
            first = urlSegments[0];
            second = urlSegments[1];
            third = urlSegments[2];

            Response.Write(first + "<br />");
            Response.Write(second + "<br />");
            Response.Write(third + "<br />");

            if (third == "")
                Response.Write("third is empty sir");
        }
    }
}