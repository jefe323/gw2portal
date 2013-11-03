using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.FriendlyUrls;

namespace gw2portal.Maps
{
    public partial class Zones : System.Web.UI.Page
    {
        public string world_name = "";
        public string region_id = "";
        public string map_id = "";
        public string minlvl = "";
        public string maxlvl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //HttpRequest q = Request;
            //world_name = q.QueryString["world_name"];
            //map_id = q.QueryString["map_id"];

            IList<string> urlSegments = Request.GetFriendlyUrlSegments();
            map_id = urlSegments[0];
            world_name = getMapName(map_id);

            Page.Header.Title = "GW2 Zone - " + world_name;

            if (world_name == null || map_id == null)
            {
                Response.Redirect("../Maps/Default.aspx");
            }

            //get region_id
            using (WebClient client = new WebClient())
            {
                try
                {
                    string data = client.DownloadString("https://api.guildwars2.com/v1/maps.json");
                    JObject o = JObject.Parse(data);

                    region_id = o["maps"][map_id]["region_id"].ToString();
                    minlvl = o["maps"][map_id]["min_level"].ToString();
                    maxlvl = o["maps"][map_id]["max_level"].ToString();
                }
                catch { }
            }
        }

        private string getMapName(string id)
        {
            string name = "";

            switch (id)
            {
                case "50":
                    name = "Lion's Arch";
                    break;
                case "922":
                    name = "Labyrinthine Cliffs";
                    break;
                case "24":
                    name = "Gendarran Fields";
                    break;
                case "23":
                    name = "Kessex Hills";
                    break;
                case "29":
                    name = "Timberline Falls";
                    break;
                case "39":
                    name = "Mount Maelstrom";
                    break;
                case "54":
                    name = "Brisban Wildlands";
                    break;
                case "22":
                    name = "Fireheart Rise";
                    break;
                case "873":
                    name = "Southsun Cove";
                    break;
                case "53":
                    name = "Sparkfly Fen";
                    break;
                case "31":
                    name = "Snowden Drifts";
                    break;
                case "27":
                    name = "Lornar's Pass";
                    break;
                case "65":
                    name = "Malchor's Leap";
                    break;
                case "62":
                    name = "Cursed Shore";
                    break;
                case "21":
                    name = "Fields of Ruin";
                    break;
                case "51":
                    name = "Straits of Devastation";
                    break;
                case "15":
                    name = "Queensdale";
                    break;
                case "17":
                    name = "Harathi Hinterlands";
                    break;
                case "26":
                    name = "Dredgehaunt Cliffs";
                    break;
                case "28":
                    name = "Wayfarer Foothills";
                    break;
                case "73":
                    name = "Bloodtide Coast";
                    break;
                case "32":
                    name = "Diessa Plateau";
                    break;
                case "25":
                    name = "Iron Marches";
                    break;
                case "20":
                    name = "Blazeridge Steppes";
                    break;
                case "19":
                    name = "Plains of Ashford";
                    break;
                case "35":
                    name = "Metrica Province";
                    break;
                case "34":
                    name = "Caledon Forest";
                    break;
                case "30":
                    name = "Frostgorge Sound";
                    break;
            }

            return name;
        }
    }
}