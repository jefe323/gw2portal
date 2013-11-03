using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gw2portal.Maps
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void WorldButton_Click(object sender, EventArgs e)
        {
            //string url = "../WvW.aspx?world_name=" + wWorldBox.SelectedItem.Text + "&world_id=" + wWorldBox.SelectedItem.Value;

            string url = "../WvW/" + wWorldBox.SelectedItem.Value;

            Response.Redirect(url);

            Response.Redirect(url);
        }

        protected void MapButton_Click(object sender, EventArgs e)
        {
            //string url = "Zones.aspx?world_name=" + MapBox.SelectedItem.Text + "&map_id=" + MapBox.SelectedItem.Value;

            string url = "Zones/" + MapBox.SelectedItem.Value;

            Response.Redirect(url);
        }
    }
}