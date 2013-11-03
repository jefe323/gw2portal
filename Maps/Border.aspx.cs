using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

namespace gw2portal.Maps
{
    public partial class Border : System.Web.UI.Page
    {
        public string[] point = new string[13];
        public string[] name = new string[13];
        public string map_name = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //passed query string
            //match_id
            //map_name
            //color
            HttpRequest q = Request;
            string match = q.QueryString["match"];
            int world = Convert.ToInt32(q.QueryString["world"]);
            map_name = q.QueryString["map"];

            using (WebClient client = new WebClient())
            {
                string data = client.DownloadString("https://api.guildwars2.com/v1/wvw/match_details.json?match_id=" + match);
                JObject o = JObject.Parse(data);
                //int world = 2;

                //set strings
                //red
                if (world == 0)
                {
                    point[7] = string.Format("<img src=\"../Content/Icons/keep_{0}.png\" class=\"i8\" />", o["maps"][world]["objectives"][0]["owner"].ToString().ToLower());
                    point[6] = string.Format("<img src=\"../Content/Icons/keep_{0}.png\" class=\"i7\" />", o["maps"][world]["objectives"][1]["owner"].ToString().ToLower());
                    point[12] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i13\" />", o["maps"][world]["objectives"][2]["owner"].ToString().ToLower());
                    point[9] = string.Format("<img src=\"../Content/Icons/tower_{0}.png\" class=\"i10\" />", o["maps"][world]["objectives"][3]["owner"].ToString().ToLower());
                    point[10] = string.Format("<img src=\"../Content/Icons/tower_{0}.png\" class=\"i11\" />", o["maps"][world]["objectives"][4]["owner"].ToString().ToLower());
                    point[4] = string.Format("<img src=\"../Content/Icons/keep_{0}.png\" class=\"i5\" />", o["maps"][world]["objectives"][5]["owner"].ToString().ToLower());
                    point[1] = string.Format("<img src=\"../Content/Icons/tower_{0}.png\" class=\"i2\" />", o["maps"][world]["objectives"][6]["owner"].ToString().ToLower());
                    point[0] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i1\" />", o["maps"][world]["objectives"][7]["owner"].ToString().ToLower());
                    point[2] = string.Format("<img src=\"../Content/Icons/tower_{0}.png\" class=\"i3\" />", o["maps"][world]["objectives"][8]["owner"].ToString().ToLower());
                    point[11] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i12\" />", o["maps"][world]["objectives"][9]["owner"].ToString().ToLower());
                    point[5] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i6\" />", o["maps"][world]["objectives"][10]["owner"].ToString().ToLower());
                    point[3] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i4\" />", o["maps"][world]["objectives"][11]["owner"].ToString().ToLower());
                    point[8] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i9\" />", o["maps"][world]["objectives"][12]["owner"].ToString().ToLower());

                    name[7] = getName(o["maps"][world]["objectives"][0]["id"].ToString());
                    name[6] = getName(o["maps"][world]["objectives"][1]["id"].ToString());
                    name[12] = getName(o["maps"][world]["objectives"][2]["id"].ToString());
                    name[9] = getName(o["maps"][world]["objectives"][3]["id"].ToString());
                    name[10] = getName(o["maps"][world]["objectives"][4]["id"].ToString());
                    name[4] = getName(o["maps"][world]["objectives"][5]["id"].ToString());
                    name[1] = getName(o["maps"][world]["objectives"][6]["id"].ToString());
                    name[0] = getName(o["maps"][world]["objectives"][7]["id"].ToString());
                    name[2] = getName(o["maps"][world]["objectives"][8]["id"].ToString());
                    name[11] = getName(o["maps"][world]["objectives"][9]["id"].ToString());
                    name[5] = getName(o["maps"][world]["objectives"][10]["id"].ToString());
                    name[3] = getName(o["maps"][world]["objectives"][11]["id"].ToString());
                    name[8] = getName(o["maps"][world]["objectives"][12]["id"].ToString());
                }
                //green
                if (world == 1)
                {
                    point[7] = string.Format("<img src=\"../Content/Icons/keep_{0}.png\" class=\"i8\" />", o["maps"][world]["objectives"][0]["owner"].ToString().ToLower());
                    point[10] = string.Format("<img src=\"../Content/Icons/tower_{0}.png\" class=\"i11\" />", o["maps"][world]["objectives"][1]["owner"].ToString().ToLower());
                    point[12] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i13\" />", o["maps"][world]["objectives"][2]["owner"].ToString().ToLower());
                    point[6] = string.Format("<img src=\"../Content/Icons/keep_{0}.png\" class=\"i7\" />", o["maps"][world]["objectives"][3]["owner"].ToString().ToLower());
                    point[9] = string.Format("<img src=\"../Content/Icons/tower_{0}.png\" class=\"i10\" />", o["maps"][world]["objectives"][4]["owner"].ToString().ToLower());
                    point[4] = string.Format("<img src=\"../Content/Icons/keep_{0}.png\" class=\"i5\" />", o["maps"][world]["objectives"][5]["owner"].ToString().ToLower());
                    point[1] = string.Format("<img src=\"../Content/Icons/tower_{0}.png\" class=\"i2\" />", o["maps"][world]["objectives"][6]["owner"].ToString().ToLower());
                    point[3] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i4\" />", o["maps"][world]["objectives"][7]["owner"].ToString().ToLower());
                    point[8] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i9\" />", o["maps"][world]["objectives"][8]["owner"].ToString().ToLower());
                    point[5] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i6\" />", o["maps"][world]["objectives"][9]["owner"].ToString().ToLower());
                    point[11] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i12\" />", o["maps"][world]["objectives"][10]["owner"].ToString().ToLower());
                    point[0] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i1\" />", o["maps"][world]["objectives"][11]["owner"].ToString().ToLower());
                    point[2] = string.Format("<img src=\"../Content/Icons/tower_{0}.png\" class=\"i3\" />", o["maps"][world]["objectives"][12]["owner"].ToString().ToLower());

                    name[7] = getName(o["maps"][world]["objectives"][0]["id"].ToString());
                    name[10] = getName(o["maps"][world]["objectives"][1]["id"].ToString());
                    name[12] = getName(o["maps"][world]["objectives"][2]["id"].ToString());
                    name[6] = getName(o["maps"][world]["objectives"][3]["id"].ToString());
                    name[9] = getName(o["maps"][world]["objectives"][4]["id"].ToString());
                    name[4] = getName(o["maps"][world]["objectives"][5]["id"].ToString());
                    name[1] = getName(o["maps"][world]["objectives"][6]["id"].ToString());
                    name[3] = getName(o["maps"][world]["objectives"][7]["id"].ToString());
                    name[8] = getName(o["maps"][world]["objectives"][8]["id"].ToString());
                    name[5] = getName(o["maps"][world]["objectives"][9]["id"].ToString());
                    name[11] = getName(o["maps"][world]["objectives"][10]["id"].ToString());
                    name[0] = getName(o["maps"][world]["objectives"][11]["id"].ToString());
                    name[2] = getName(o["maps"][world]["objectives"][12]["id"].ToString());
                }
                //blue
                if (world == 2)
                {
                    point[4] = string.Format("<img src=\"../Content/Icons/keep_{0}.png\" class=\"i5\" />", o["maps"][world]["objectives"][0]["owner"].ToString().ToLower());
                    point[12] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i13\" />", o["maps"][world]["objectives"][1]["owner"].ToString().ToLower());
                    point[9] = string.Format("<img src=\"../Content/Icons/tower_{0}.png\" class=\"i10\" />", o["maps"][world]["objectives"][2]["owner"].ToString().ToLower());
                    point[10] = string.Format("<img src=\"../Content/Icons/tower_{0}.png\" class=\"i11\" />", o["maps"][world]["objectives"][3]["owner"].ToString().ToLower());
                    point[6] = string.Format("<img src=\"../Content/Icons/keep_{0}.png\" class=\"i7\" />", o["maps"][world]["objectives"][4]["owner"].ToString().ToLower());
                    point[2] = string.Format("<img src=\"../Content/Icons/tower_{0}.png\" class=\"i3\" />", o["maps"][world]["objectives"][5]["owner"].ToString().ToLower());
                    point[0] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i1\" />", o["maps"][world]["objectives"][6]["owner"].ToString().ToLower());
                    point[1] = string.Format("<img src=\"../Content/Icons/tower_{0}.png\" class=\"i2\" />", o["maps"][world]["objectives"][7]["owner"].ToString().ToLower());
                    point[7] = string.Format("<img src=\"../Content/Icons/keep_{0}.png\" class=\"i8\" />", o["maps"][world]["objectives"][8]["owner"].ToString().ToLower());
                    point[3] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i4\" />", o["maps"][world]["objectives"][9]["owner"].ToString().ToLower());
                    point[8] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i9\" />", o["maps"][world]["objectives"][10]["owner"].ToString().ToLower());
                    point[5] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i6\" />", o["maps"][world]["objectives"][11]["owner"].ToString().ToLower());
                    point[11] = string.Format("<img src=\"../Content/Icons/camp_{0}.png\" class=\"i12\" />", o["maps"][world]["objectives"][12]["owner"].ToString().ToLower());

                    name[4] = getName(o["maps"][world]["objectives"][0]["id"].ToString());
                    name[12] = getName(o["maps"][world]["objectives"][1]["id"].ToString());
                    name[9] = getName(o["maps"][world]["objectives"][2]["id"].ToString());
                    name[10] = getName(o["maps"][world]["objectives"][3]["id"].ToString());
                    name[6] = getName(o["maps"][world]["objectives"][4]["id"].ToString());
                    name[2] = getName(o["maps"][world]["objectives"][5]["id"].ToString());
                    name[0] = getName(o["maps"][world]["objectives"][6]["id"].ToString());
                    name[1] = getName(o["maps"][world]["objectives"][7]["id"].ToString());
                    name[7] = getName(o["maps"][world]["objectives"][8]["id"].ToString());
                    name[3] = getName(o["maps"][world]["objectives"][9]["id"].ToString());
                    name[8] = getName(o["maps"][world]["objectives"][10]["id"].ToString());
                    name[5] = getName(o["maps"][world]["objectives"][11]["id"].ToString());
                    name[11] = getName(o["maps"][world]["objectives"][12]["id"].ToString());
                }
            }
        }

        private string getName(string id)
        {
            string output = "";
            switch (id)
            {
                case "1":
                    output = "Outlook";
                    break;
                case "2":
                    output = "Valley";
                    break;
                case "3":
                    output = "Lowloands";
                    break;
                case "4":
                    output = "Golanta Clearing";
                    break;
                case "5":
                    output = "Pangloss Rise";
                    break;
                case "6":
                    output = "Speldan Clearcut";
                    break;
                case "7":
                    output = "Danelon Passage";
                    break;
                case "8":
                    output = "Umberglade Woods";
                    break;
                case "9":
                    output = "Stonemist Castle";
                    break;
                case "10":
                    output = "Rogue's Quarry";
                    break;
                case "11":
                    output = "Aldon's Ledge";
                    break;
                case "12":
                    output = "Wildcreek Run";
                    break;
                case "13":
                    output = "Jerrifer's Slough";
                    break;
                case "14":
                    output = "Klovan Gully";
                    break;
                case "15":
                    output = "Langor Gulch";
                    break;
                case "16":
                    output = "Quentin Lake";
                    break;
                case "17":
                    output = "Mendon's Gap";
                    break;
                case "18":
                    output = "Anzalias Pass";
                    break;
                case "19":
                    output = "Ogrewatch Cut";
                    break;
                case "20":
                    output = "Veloka Slope";
                    break;
                case "21":
                    output = "Durios Gulch";
                    break;
                case "22":
                    output = "Bravost Escarpment";
                    break;
                case "23":
                    output = "Garrison";
                    break;
                case "24":
                    output = "Champion's Demense";
                    break;
                case "25":
                    output = "Redbriar";
                    break;
                case "26":
                    output = "Greenlake";
                    break;
                case "27":
                    output = "Ascension Bay";
                    break;
                case "28":
                    output = "Dawn's Eyrie";
                    break;
                case "29":
                    output = "The Spiritholme";
                    break;
                case "30":
                    output = "Woodhaven";
                    break;
                case "31":
                    output = "Askalion Hills";
                    break;
                case "32":
                    output = "Etheron Hills";
                    break;
                case "33":
                    output = "Dreaming Bay";
                    break;
                case "34":
                    output = "Victors's Lodge";
                    break;
                case "35":
                    output = "Greenbriar";
                    break;
                case "36":
                    output = "Bluelake";
                    break;
                case "37":
                    output = "Garrison";
                    break;
                case "38":
                    output = "Longview";
                    break;
                case "39":
                    output = "The Godsword";
                    break;
                case "40":
                    output = "Cliffside";
                    break;
                case "41":
                    output = "Shadaran Hills";
                    break;
                case "42":
                    output = "Redlake";
                    break;
                case "43":
                    output = "Hero's Lodge";
                    break;
                case "44":
                    output = "Dreadfall Bay";
                    break;
                case "45":
                    output = "Bluebriar";
                    break;
                case "46":
                    output = "Garrison";
                    break;
                case "47":
                    output = "Sunnyhill";
                    break;
                case "48":
                    output = "Faithleap";
                    break;
                case "49":
                    output = "Bluevale Refuge";
                    break;
                case "50":
                    output = "Bluewater Lowlands";
                    break;
                case "51":
                    output = "Astralhome";
                    break;
                case "52":
                    output = "Arah's Hope";
                    break;
                case "53":
                    output = "Greenvale Refuge";
                    break;
                case "54":
                    output = "Foghaven";
                    break;
                case "55":
                    output = "Redwater Lowlands";
                    break;
                case "56":
                    output = "The Titanpawy";
                    break;
                case "57":
                    output = "Cragtop";
                    break;
                case "58":
                    output = "Godslore";
                    break;
                case "59":
                    output = "Redvale Refuge";
                    break;
                case "60":
                    output = "Stargrove";
                    break;
                case "61":
                    output = "Greenwater Lowlands";
                    break;
                default:
                    break;
            }

            return output;
        }
    }
}