using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.FriendlyUrls;

namespace gw2portal
{
    public partial class Events : System.Web.UI.Page
    {
        public string world_name;
        public string map_name = "All";
        public string world_id = "";
        public string status = "All";

        public string region_id = "";
        public string map_id = "";
        public string minlvl = "";
        public string maxlvl = "";

        public string active_events = "";

        public string final = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpRequest q = Request;
            /*world_name = q.QueryString["world_name"];
            world_id = q.QueryString["world_id"];
            map_id = q.QueryString["map_id"];*/

            IList<string> urlSegments = Request.GetFriendlyUrlSegments();
            world_id = urlSegments[0];
            map_id = urlSegments[1];
            world_name = getWorldName(world_id);
            map_name = getMapName(map_id);


            if (q.QueryString["map_name"] != null) { map_name = q.QueryString["map_name"]; }
            if (q.QueryString["status"] != null) { status = q.QueryString["status"]; }

            outputLit.Text = getData();

            if (status == "Active")
            {
                MapPlaceHolder.Visible = true;
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
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            outputLit.Text = getData();

            UpdatePanel1.Update();
        }

        public string getData()
        {
            string output = "<table><td><b>World</b></td><td><b>Map</b></td><td><b>Event</b></td><td><b>State</b></td>";
            Dictionary<string, string> worlds = new Dictionary<string, string>();
            Dictionary<string, string> events = new Dictionary<string, string>();
            Dictionary<string, string> maps = new Dictionary<string, string>();

            HttpRequest q = Request;
            //world_name = q.QueryString["world_name"];
            //string world_id = q.QueryString["world_id"];
            //map_name = q.QueryString["map_name"];
            //string map_id = q.QueryString["map_id"];
            status = q.QueryString["status"];

            using (WebClient client = new WebClient())
            {
                //get list of worlds
                if (world_name == null)
                {
                    var data = client.DownloadString("https://api.guildwars2.com/v1/world_names.json");
                    var o = JArray.Parse(data);
                    foreach (var item in o)
                    {
                        worlds.Add(item["id"].ToString(), item["name"].ToString());
                    }
                }
                //get list of map names
                if (map_name == null)
                {
                    var data = client.DownloadString("https://api.guildwars2.com/v1/map_names.json");
                    var o = JArray.Parse(data);
                    foreach (var item in o)
                    {
                        maps.Add(item["id"].ToString(), item["name"].ToString());
                    }
                }

                //get list of events
                var eData = client.DownloadString("https://api.guildwars2.com/v1/event_names.json");
                var eObject = JArray.Parse(eData);
                foreach (var item in eObject)
                {
                    events.Add(item["id"].ToString(), item["name"].ToString());
                }


                //get data from dictionaries
                if (world_id == null || world_name == null) { Response.Redirect("/"); }
                string myWorld = world_name;
                string url = "https://api.guildwars2.com/v1/events.json?world_id=" + world_id;
                if (map_name != null)
                {
                    url += "&map_id=" + map_id;
                }
                eData = client.DownloadString(url);
                JObject oo = JObject.Parse(eData);

                int i = 0;
                while (true)
                {
                    try
                    {
                        string myMap = map_name;
                        if (map_name == null) { myMap = maps[oo["events"][i]["map_id"].ToString()]; }

                        if (status != null && oo["events"][i]["state"].ToString() == status)
                        {
                            string myEvent = "<a href=\"http://wiki.guildwars2.com/wiki/" + events[oo["events"][i]["event_id"].ToString()].Replace(".", "") + "\">" + events[oo["events"][i]["event_id"].ToString()] + "</a>";
                            output += "<tr>";
                            output += String.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td>", myWorld, myMap, myEvent, status);
                            output += "</tr>";

                            if (status == "Active")
                            {
                                //store event names here
                               active_events += oo["events"][i]["event_id"].ToString() + "**";
                            }

                            i++;
                        }
                        else if (status != null && oo["events"][i]["state"].ToString() != status) { i++; }
                        else
                        {
                            string myEvent = "<a href=\"http://wiki.guildwars2.com/wiki/" + events[oo["events"][i]["event_id"].ToString()].Replace(".", "") + "\">" + events[oo["events"][i]["event_id"].ToString()] + "</a>";
                            output += "<tr>";
                            output += String.Format("<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td>", myWorld, myMap, myEvent, oo["events"][i]["state"].ToString());
                            output += "</tr>";
                            i++;
                        }
                    }
                    catch { break; }

                }
            }
            output += "</table>";
            return output;
        }

        private string getWorldName(string id)
        {
            string name = "";

            switch (id)
            {
                case "1010":
                    name = "Ehmry Bay";
                    break;
                case "1018":
                    name = "Northern Shiverpeaks";
                    break;
                case "1002":
                    name = "Borlis Pass";
                    break;
                case "1008":
                    name = "Jade Quarry";
                    break;
                case "1005":
                    name = "Maguuma";
                    break;
                case "1015":
                    name = "Isle of Janthir";
                    break;
                case "1009":
                    name = "Fort Aspenwood";
                    break;
                case "1013":
                    name = "Sanctum of Rall";
                    break;
                case "1007":
                    name = "Gate of Madness";
                    break;
                case "1006":
                    name = "Sorrow's Furnace";
                    break;
                case "1019":
                    name = "Blackgate";
                    break;
                case "1021":
                    name = "Dragonbrand";
                    break;
                case "1012":
                    name = "Darkhaven";
                    break;
                case "1003":
                    name = "Yak's Bend";
                    break;
                case "1014":
                    name = "Crystal Desert";
                    break;
                case "1001":
                    name = "Anvil Rock";
                    break;
                case "1011":
                    name = "Stormbluff Isle";
                    break;
                case "1020":
                    name = "Ferguson's Crossing";
                    break;
                case "1016":
                    name = "Sea of Sorrows";
                    break;
                case "1022":
                    name = "Kaineng";
                    break;
                case "1023":
                    name = "Devona's Rest";
                    break;
                case "1017":
                    name = "Tarnished Coast";
                    break;
                case "1024":
                    name = "Eredon Terrace";
                    break;
                case "1004":
                    name = "Henge of Denravi";
                    break;
                case "2012":
                    name = "Piken Square";
                    break;
                case "2003":
                    name = "Gandara";
                    break;
                case "2007":
                    name = "Far Shiverpeaks";
                    break;
                case "2204":
                    name = "Abaddon's Mouth";
                    break;
                case "2201":
                    name = "Kodash";
                    break;
                case "2010":
                    name = "Seafarer's Rest";
                    break;
                case "2301":
                    name = "Baruch Bay";
                    break;
                case "2205":
                    name = "Drakkar Lake";
                    break;
                case "2002":
                    name = "Desolation";
                    break;
                case "2202":
                    name = "Riverside";
                    break;
                case "2008":
                    name = "Whiteside Ridge";
                    break;
                case "2203":
                    name = "Elona Reach";
                    break;
                case "2206":
                    name = "Miller's Sound";
                    break;
                case "2004":
                    name = "Blacktide";
                    break;
                case "2207":
                    name = "Dzagonur";
                    break;
                case "2105":
                    name = "Arborstone";
                    break;
                case "2404":
                    name = "Jade Sea";
                    break;
                case "2013":
                    name = "Aurora Glade";
                    break;
                case "2103":
                    name = "Augury Rock";
                    break;
                case "2101":
                    name = "Fort Ranik";
                    break;
                case "2104":
                    name = "Vizunah Square";
                    break;
                case "2009":
                    name = "Ruins of Surmia";
                    break;
                case "2014":
                    name = "Gunnar's Hold";
                    break;
                case "2005":
                    name = "Ring of Fire";
                    break;
                case "2006":
                    name = "Underworld";
                    break;
                case "2011":
                    name = "Vabbi";
                    break;
                case "2001":
                    name = "Fissure of Woez";
                    break;
            }

            return name;
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