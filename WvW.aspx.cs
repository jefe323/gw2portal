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
    public partial class WvW : System.Web.UI.Page
    {
        public Dictionary<string, string> world = new Dictionary<string, string>();
        public string tWorld;
        public string matchID;

        public string[] mainScore = new string[3];
        public string[] redScore = new string[3];
        public string[] greenScore = new string[3];
        public string[] blueScore = new string[3];
        public string[] battleScore = new string[3];

        //0=camp, 1=tower, 2=keep, 3=castle
        public int[] greenPoint = new int[4] {0, 0, 0, 0};
        public int[] bluePoint = new int[4] { 0, 0, 0, 0 };
        public int[] redPoint = new int[4] { 0, 0, 0, 0 };

        public int greenIn = 0;
        public int blueIn = 0;
        public int redIn = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> locationName = new Dictionary<string, string>();
            Dictionary<string, string> locations = new Dictionary<string, string>();
            //HttpRequest q = Request;
            //tWorld = q.QueryString["world_name"];
            IList<string> urlSegments = Request.GetFriendlyUrlSegments();
            string world_id = urlSegments[0];
            tWorld = getWorldName(world_id);


            Dictionary<string, string> wData = new Dictionary<string, string>();
            #region wData input
            wData.Add("1008", "Jade Quarry");
            wData.Add("2006", "Underworld");
            wData.Add("1023", "Devona's Rest");
            wData.Add("2105", "Arborstone");
            wData.Add("1014", "Crystal Desert");
            wData.Add("1022", "Kaineng");
            wData.Add("2001", "Fissure of Woe");
            wData.Add("1001", "Anvil Rock");
            wData.Add("2003", "Gandara");
            wData.Add("1003", "Yak's Bend");
            wData.Add("2007", "Far Shiverpeaks");
            wData.Add("1011", "Stormbluff Isle");
            wData.Add("2013", "Aurora Glade");
            wData.Add("1016", "Sea of Sorrows");
            wData.Add("2005", "Ring of Fire");
            wData.Add("2012", "Piken Square");
            wData.Add("1012", "Darkhaven");
            wData.Add("1005", "Maguuma");
            wData.Add("2204", "Abaddon's Mouth");
            wData.Add("2203", "Elona Reach");
            wData.Add("2010", "Seafarer's Rest");
            wData.Add("2104", "Vizunah Square");
            wData.Add("2207", "Dzagonur");
            wData.Add("2009", "Ruins of Surmia");
            wData.Add("1002", "Borlis Pass");
            wData.Add("2002", "Desolation");
            wData.Add("1010", "Ehmry Bay");
            wData.Add("1024", "Eredon Terrace");
            wData.Add("1004", "Henge of Denravi");
            wData.Add("1007", "Gate of Madness");
            wData.Add("2205", "Drakkar Lake");
            wData.Add("2008", "Whiteside Ridge");
            wData.Add("1017", "Tarnished Coast");
            wData.Add("2101", "Jade Sea");
            wData.Add("1013", "Sanctum of Rall");
            wData.Add("2014", "Gunnar's Hold");
            wData.Add("1021", "Dragonbrand");
            wData.Add("2301", "Baruch Bay");
            wData.Add("2102", "Fort Ranik");
            wData.Add("2103", "Augury Rock");
            wData.Add("2201", "Kodash");
            wData.Add("2202", "Riverside");
            wData.Add("2206", "Miller's Sound");
            wData.Add("1018", "Northern Shiverpeaks");
            wData.Add("1015", "Isle of Janthir");
            wData.Add("2004", "Blacktide");
            wData.Add("1006", "Sorrow's Furnace");
            wData.Add("2011", "Vabbi");
            wData.Add("1009", "Fort Aspenwood");
            wData.Add("1020", "Ferguson's Crossing");
            wData.Add("1019", "Blackgate");
            #endregion

            //pull data from api
            using (WebClient client = new WebClient())
            {
                //get matches
                string data = client.DownloadString("https://api.guildwars2.com/v1/wvw/matches.json");
                JObject o = JObject.Parse(data);
                int i = 0;
                while (true)
                {
                    try
                    {
                        //Response.Write(o["wvw_matches"][i]["wvw_match_id"]);
                        //loop through and find match_id for our world
                        if (o["wvw_matches"][i]["red_world_id"].ToString() == world_id || o["wvw_matches"][i]["blue_world_id"].ToString() == world_id || o["wvw_matches"][i]["green_world_id"].ToString() == world_id)
                        {
                            matchID = o["wvw_matches"][i]["wvw_match_id"].ToString();
                            world.Add(o["wvw_matches"][i]["green_world_id"].ToString(), wData[o["wvw_matches"][i]["green_world_id"].ToString()]);
                            world.Add(o["wvw_matches"][i]["blue_world_id"].ToString(), wData[o["wvw_matches"][i]["blue_world_id"].ToString()]);
                            world.Add(o["wvw_matches"][i]["red_world_id"].ToString(), wData[o["wvw_matches"][i]["red_world_id"].ToString()]);
                        }
                        i++;
                    }
                    catch { break; }
                }
                //get location names
                setLocationName(locationName);

                data = client.DownloadString("https://api.guildwars2.com/v1/wvw/objective_names.json");
                JArray jo = JArray.Parse(data);
                foreach (var item in jo)
                {
                    locations.Add(item["id"].ToString(), item["name"].ToString());
                }

                //get match details
                data = client.DownloadString("https://api.guildwars2.com/v1/wvw/match_details.json?match_id=" + matchID);
                o = JObject.Parse(data);

                //set scores
                mainScore[2] = o["scores"][0].ToString();
                mainScore[1] = o["scores"][1].ToString();
                mainScore[0] = o["scores"][2].ToString();

                redScore[2] = o["maps"][0]["scores"][0].ToString();
                redScore[1] = o["maps"][0]["scores"][1].ToString();
                redScore[0] = o["maps"][0]["scores"][2].ToString();

                greenScore[2] = o["maps"][1]["scores"][0].ToString();
                greenScore[1] = o["maps"][1]["scores"][1].ToString();
                greenScore[0] = o["maps"][1]["scores"][2].ToString();

                blueScore[2] = o["maps"][2]["scores"][0].ToString();
                blueScore[1] = o["maps"][2]["scores"][1].ToString();
                blueScore[0] = o["maps"][2]["scores"][2].ToString();

                battleScore[2] = o["maps"][3]["scores"][0].ToString();
                battleScore[1] = o["maps"][3]["scores"][1].ToString();
                battleScore[0] = o["maps"][3]["scores"][2].ToString();

                //build tables
                #region RED

                if (o["maps"][0]["objectives"][0]["owner"].ToString() == "Green") { greenPoint[2] += 1; }
                else if (o["maps"][0]["objectives"][0]["owner"].ToString() == "Blue") { bluePoint[2] += 1; }
                else if (o["maps"][0]["objectives"][0]["owner"].ToString() == "Red") { redPoint[2] += 1; }

                if (o["maps"][0]["objectives"][1]["owner"].ToString() == "Green") { greenPoint[2] += 1; }
                else if (o["maps"][0]["objectives"][1]["owner"].ToString() == "Blue") { bluePoint[2] += 1; }
                else if (o["maps"][0]["objectives"][1]["owner"].ToString() == "Red") { redPoint[2] += 1; }

                if (o["maps"][0]["objectives"][2]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][0]["objectives"][2]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][0]["objectives"][2]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][0]["objectives"][3]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][0]["objectives"][3]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][0]["objectives"][3]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][0]["objectives"][4]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][0]["objectives"][4]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][0]["objectives"][4]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][0]["objectives"][5]["owner"].ToString() == "Green") { greenPoint[2] += 1; }
                else if (o["maps"][0]["objectives"][5]["owner"].ToString() == "Blue") { bluePoint[2] += 1; }
                else if (o["maps"][0]["objectives"][5]["owner"].ToString() == "Red") { redPoint[2] += 1; }

                if (o["maps"][0]["objectives"][6]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][0]["objectives"][6]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][0]["objectives"][6]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][0]["objectives"][7]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][0]["objectives"][7]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][0]["objectives"][7]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][0]["objectives"][8]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][0]["objectives"][8]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][0]["objectives"][8]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][0]["objectives"][9]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][0]["objectives"][9]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][0]["objectives"][9]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][0]["objectives"][10]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][0]["objectives"][10]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][0]["objectives"][10]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][0]["objectives"][11]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][0]["objectives"][11]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][0]["objectives"][11]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][0]["objectives"][12]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][0]["objectives"][12]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][0]["objectives"][12]["owner"].ToString() == "Red") { redPoint[0] += 1; }
                
                #endregion

                #region GREEN
                if (o["maps"][1]["objectives"][0]["owner"].ToString() == "Green") { greenPoint[2] += 1; }
                else if (o["maps"][1]["objectives"][0]["owner"].ToString() == "Blue") { bluePoint[2] += 1; }
                else if (o["maps"][1]["objectives"][0]["owner"].ToString() == "Red") { redPoint[2] += 1; }

                if (o["maps"][1]["objectives"][1]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][1]["objectives"][1]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][1]["objectives"][1]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][1]["objectives"][2]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][1]["objectives"][2]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][1]["objectives"][2]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][1]["objectives"][3]["owner"].ToString() == "Green") { greenPoint[2] += 1; }
                else if (o["maps"][1]["objectives"][3]["owner"].ToString() == "Blue") { bluePoint[2] += 1; }
                else if (o["maps"][1]["objectives"][3]["owner"].ToString() == "Red") { redPoint[2] += 1; }

                if (o["maps"][1]["objectives"][4]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][1]["objectives"][4]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][1]["objectives"][4]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][1]["objectives"][5]["owner"].ToString() == "Green") { greenPoint[2] += 1; }
                else if (o["maps"][1]["objectives"][5]["owner"].ToString() == "Blue") { bluePoint[2] += 1; }
                else if (o["maps"][1]["objectives"][5]["owner"].ToString() == "Red") { redPoint[2] += 1; }

                if (o["maps"][1]["objectives"][6]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][1]["objectives"][6]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][1]["objectives"][6]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][1]["objectives"][7]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][1]["objectives"][7]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][1]["objectives"][7]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][1]["objectives"][8]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][1]["objectives"][8]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][1]["objectives"][8]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][1]["objectives"][9]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][1]["objectives"][9]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][1]["objectives"][9]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][1]["objectives"][10]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][1]["objectives"][10]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][1]["objectives"][10]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][1]["objectives"][11]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][1]["objectives"][11]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][1]["objectives"][11]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][1]["objectives"][12]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][1]["objectives"][12]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][1]["objectives"][12]["owner"].ToString() == "Red") { redPoint[1] += 1; }
                
                #endregion

                #region BLUE
                if (o["maps"][2]["objectives"][0]["owner"].ToString() == "Green") { greenPoint[2] += 1; }
                else if (o["maps"][2]["objectives"][0]["owner"].ToString() == "Blue") { bluePoint[2] += 1; }
                else if (o["maps"][2]["objectives"][0]["owner"].ToString() == "Red") { redPoint[2] += 1; }

                if (o["maps"][2]["objectives"][1]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][2]["objectives"][1]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][2]["objectives"][1]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][2]["objectives"][2]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][2]["objectives"][2]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][2]["objectives"][2]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][2]["objectives"][3]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][2]["objectives"][3]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][2]["objectives"][3]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][2]["objectives"][4]["owner"].ToString() == "Green") { greenPoint[2] += 1; }
                else if (o["maps"][2]["objectives"][4]["owner"].ToString() == "Blue") { bluePoint[2] += 1; }
                else if (o["maps"][2]["objectives"][4]["owner"].ToString() == "Red") { redPoint[2] += 1; }

                if (o["maps"][2]["objectives"][5]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][2]["objectives"][5]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][2]["objectives"][5]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][2]["objectives"][6]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][2]["objectives"][6]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][2]["objectives"][6]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][2]["objectives"][7]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][2]["objectives"][7]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][2]["objectives"][7]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][2]["objectives"][8]["owner"].ToString() == "Green") { greenPoint[2] += 1; }
                else if (o["maps"][2]["objectives"][8]["owner"].ToString() == "Blue") { bluePoint[2] += 1; }
                else if (o["maps"][2]["objectives"][8]["owner"].ToString() == "Red") { redPoint[2] += 1; }

                if (o["maps"][2]["objectives"][9]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][2]["objectives"][9]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][2]["objectives"][9]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][2]["objectives"][10]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][2]["objectives"][10]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][2]["objectives"][10]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][2]["objectives"][11]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][2]["objectives"][11]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][2]["objectives"][11]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][2]["objectives"][12]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][2]["objectives"][12]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][2]["objectives"][12]["owner"].ToString() == "Red") { redPoint[0] += 1; }
                
                #endregion

                #region BATTLE
                if (o["maps"][3]["objectives"][0]["owner"].ToString() == "Green") { greenPoint[2] += 1; }
                else if (o["maps"][3]["objectives"][0]["owner"].ToString() == "Blue") { bluePoint[2] += 1; }
                else if (o["maps"][3]["objectives"][0]["owner"].ToString() == "Red") { redPoint[2] += 1; }

                if (o["maps"][3]["objectives"][1]["owner"].ToString() == "Green") { greenPoint[2] += 1; }
                else if (o["maps"][3]["objectives"][1]["owner"].ToString() == "Blue") { bluePoint[2] += 1; }
                else if (o["maps"][3]["objectives"][1]["owner"].ToString() == "Red") { redPoint[2] += 1; }

                if (o["maps"][3]["objectives"][2]["owner"].ToString() == "Green") { greenPoint[2] += 1; }
                else if (o["maps"][3]["objectives"][2]["owner"].ToString() == "Blue") { bluePoint[2] += 1; }
                else if (o["maps"][3]["objectives"][2]["owner"].ToString() == "Red") { redPoint[2] += 1; }

                if (o["maps"][3]["objectives"][3]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][3]["objectives"][3]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][3]["objectives"][3]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][3]["objectives"][4]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][3]["objectives"][4]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][3]["objectives"][4]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][3]["objectives"][5]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][3]["objectives"][5]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][3]["objectives"][5]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][3]["objectives"][6]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][3]["objectives"][6]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][3]["objectives"][6]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][3]["objectives"][7]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][3]["objectives"][7]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][3]["objectives"][7]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][3]["objectives"][8]["owner"].ToString() == "Green") { greenPoint[3] += 1; }
                else if (o["maps"][3]["objectives"][8]["owner"].ToString() == "Blue") { bluePoint[3] += 1; }
                else if (o["maps"][3]["objectives"][8]["owner"].ToString() == "Red") { redPoint[3] += 1; }

                if (o["maps"][3]["objectives"][9]["owner"].ToString() == "Green") { greenPoint[0] += 1; }
                else if (o["maps"][3]["objectives"][9]["owner"].ToString() == "Blue") { bluePoint[0] += 1; }
                else if (o["maps"][3]["objectives"][9]["owner"].ToString() == "Red") { redPoint[0] += 1; }

                if (o["maps"][3]["objectives"][10]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][3]["objectives"][10]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][3]["objectives"][10]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][3]["objectives"][11]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][3]["objectives"][11]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][3]["objectives"][11]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][3]["objectives"][12]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][3]["objectives"][12]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][3]["objectives"][12]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][3]["objectives"][13]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][3]["objectives"][13]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][3]["objectives"][13]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][3]["objectives"][14]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][3]["objectives"][14]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][3]["objectives"][14]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][3]["objectives"][15]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][3]["objectives"][15]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][3]["objectives"][15]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][3]["objectives"][16]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][3]["objectives"][16]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][3]["objectives"][16]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][3]["objectives"][17]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][3]["objectives"][17]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][3]["objectives"][17]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][3]["objectives"][18]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][3]["objectives"][18]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][3]["objectives"][18]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][3]["objectives"][19]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][3]["objectives"][19]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][3]["objectives"][19]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][3]["objectives"][20]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][3]["objectives"][20]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][3]["objectives"][20]["owner"].ToString() == "Red") { redPoint[1] += 1; }

                if (o["maps"][3]["objectives"][21]["owner"].ToString() == "Green") { greenPoint[1] += 1; }
                else if (o["maps"][3]["objectives"][21]["owner"].ToString() == "Blue") { bluePoint[1] += 1; }
                else if (o["maps"][3]["objectives"][21]["owner"].ToString() == "Red") { redPoint[1] += 1; }
                
                #endregion

                greenIn = (greenPoint[0]*5)+(greenPoint[1]*10)+(greenPoint[2]*25)+(greenPoint[3]*35);
                redIn = (redPoint[0] * 5) + (redPoint[1] * 10) + (redPoint[2] * 25) + (redPoint[3] * 35);
                blueIn = (bluePoint[0] * 5) + (bluePoint[1] * 10) + (bluePoint[2] * 25) + (bluePoint[3] * 35);

            }
            world.Add(tWorld, "Green");
        }

        //forgive me for my sins...
        private Dictionary<string, string> setLocationName(Dictionary<string, string> locationName)
        {
            locationName.Add("1", "Outlook");
            locationName.Add("2", "Valley");
            locationName.Add("3", "Lowloands");
            locationName.Add("4", "Golanta Clearing");
            locationName.Add("5", "Pangloss Rise");
            locationName.Add("6", "Speldan Clearcut");
            locationName.Add("7", "Danelon Passage");
            locationName.Add("8", "Umberglade Woods");
            locationName.Add("9", "Stonemist Castle");
            locationName.Add("10", "Rogue's Quarry");
            locationName.Add("11", "Aldon's Ledge");
            locationName.Add("12", "Wildcreek Run");
            locationName.Add("13", "Jerrifer's Slough");
            locationName.Add("14", "Klovan Gully");
            locationName.Add("15", "Langor Gulch");
            locationName.Add("16", "Quentin Lake");
            locationName.Add("17", "Mendon's Gap");
            locationName.Add("18", "Anzalias Pass");
            locationName.Add("19", "Ogrewatch Cut");
            locationName.Add("20", "Veloka Slope");
            locationName.Add("21", "Durios Gulch");
            locationName.Add("22", "Bravost Escarpment");
            locationName.Add("23", "Garrison");
            locationName.Add("24", "Champion's demense");
            locationName.Add("25", "Redbriar");
            locationName.Add("26", "Greenlake");
            locationName.Add("27", "Ascension Bay");
            locationName.Add("28", "Dawn's Eyrie");
            locationName.Add("29", "The Spiritholme");
            locationName.Add("30", "Woodhaven");
            locationName.Add("31", "Askalion Hills");
            locationName.Add("32", "Etheron Hills");
            locationName.Add("33", "Dreaming Bay");
            locationName.Add("34", "Victors's Lodge");
            locationName.Add("35", "Greenbriar");
            locationName.Add("36", "Bluelake");
            locationName.Add("37", "Garrison");
            locationName.Add("38", "Longview");
            locationName.Add("39", "The Godsword");
            locationName.Add("40", "Cliffside");
            locationName.Add("41", "Shadaran Hills");
            locationName.Add("42", "Redlake");
            locationName.Add("43", "Hero's Lodge");
            locationName.Add("44", "Dreadfall Bay");
            locationName.Add("45", "Bluebriar");
            locationName.Add("46", "Garrison");
            locationName.Add("47", "Sunnyhill");
            locationName.Add("48", "Faithleap");
            locationName.Add("49", "Bluevale Refuge");
            locationName.Add("50", "Bluewater Lowlands");
            locationName.Add("51", "Astralholme");
            locationName.Add("52", "Arah's Hope");
            locationName.Add("53", "Greenvale Refuge");
            locationName.Add("54", "Foghaven");
            locationName.Add("55", "Redwater Lowlands");
            locationName.Add("56", "The Titanpawy");
            locationName.Add("57", "Cragtop");
            locationName.Add("58", "Godslore");
            locationName.Add("59", "Redvale Refuge");
            locationName.Add("60", "Stargrove");
            locationName.Add("61", "Greenwater Lowlands");

            return locationName;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            UpdatePanel1.Update();
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
    }
}