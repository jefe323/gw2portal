using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

namespace gw2portal
{
    public partial class Default : System.Web.UI.Page
    {
        public string gem2gold;
        public string gold2gem;

        protected void Page_Load(object sender, EventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    string data = client.DownloadString("http://www.gw2spidy.com/api/v0.9/json/gem-price");
                    JObject o = JObject.Parse(data);

                    string tempGem = o["result"]["gem_to_gold"].ToString();
                    string tempGold = o["result"]["gold_to_gem"].ToString();

                    char[] gem = tempGem.ToCharArray();
                    char[] gold = tempGold.ToCharArray();

                    gem2gold = gem[0] + " <img src=\"/Content/Gold_coin.png\" /> " + gem[1] + gem[2] + " <img src=\"/Content/Silver_coin.png\" /> " + gem[3] + gem[4] + " <img src=\"/Content/Copper_coin.png\" />";
                    gold2gem = gold[0] + " <img src=\"/Content/Gold_coin.png\" /> " + gold[1] + gold[2] + " <img src=\"/Content/Silver_coin.png\" /> " + gold[3] + gold[4] + " <img src=\"/Content/Copper_coin.png\" />";
                }
                catch                
                {
                    gem2gold = "Unable to retrieve data at this time";
                    gold2gem = "Unable to retrieve data at this time";
                }
            }
        }

        protected void EventButton_Click(object sender, EventArgs e)
        {
            /*string url = "Events.aspx?";
            if (WorldBox.SelectedItem.Text != "All") { url += "world_name=" + WorldBox.SelectedItem.Text + "&world_id=" + WorldBox.SelectedItem.Value + "&"; }
            if (MapBox.SelectedItem.Text != "All") { url += "map_name=" + MapBox.SelectedItem.Text + "&map_id=" + MapBox.SelectedItem.Value + "&"; }
            if (StatusBox.SelectedItem.Text != "All") { url += "status=" + StatusBox.SelectedItem.Text; }*/

            string url = "Events/" + WorldBox.SelectedItem.Value + "/" + MapBox.SelectedItem.Value;
            if (StatusBox.SelectedItem.Text != "All") { url += "?status=" + StatusBox.SelectedItem.Text; }

            Response.Redirect(url);
        }

        protected void WorldButton_Click(object sender, EventArgs e)
        {
            //string url = "WvW.aspx?world_name=" + wWorldBox.SelectedItem.Text + "&world_id=" + wWorldBox.SelectedItem.Value;

            string url = "WvW/" + wWorldBox.SelectedItem.Value;

            Response.Redirect(url);
        }

        protected void DyeButton_Click(object sender, EventArgs e)
        {
            string url = "Dyes.aspx?mat=";
            bool check = false;
            foreach (ListItem item in MatList.Items)
            {
                if (item.Selected)
                {
                    url += item.Text + ",";
                    check = true;
                }
            }
            url = url.TrimEnd(',');
            url += "&dye_name=" + DyeNameBox.Text;
            if (check == true)
            {
                Response.Redirect(url);
            }
        }
    }
}