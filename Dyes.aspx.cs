using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Newtonsoft.Json.Linq;

namespace gw2portal
{
    public partial class Dyes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string getData()
        {
            string output = "";

            HttpRequest q = Request;
            string name = q.QueryString["dye_name"];
            string temp = q.QueryString["mat"];
            string[] materials = temp.Split(',');

            bool cloth = false;
            bool leather = false;
            bool metal = false;

            string selVal = "";

            string title1 = "";
            string title2 = "";
            string title3 = "";

            int col = 1;

            foreach (string s in materials)
            {
                if (s == "Cloth")
                {
                    cloth = true;
                    selVal += "c";
                    col++;
                }
                else if (s == "Leather")
                {
                    leather = true;
                    selVal += "l";
                    col++;
                }
                else if (s == "Metal")
                {
                    metal = true;
                    selVal += "m";
                    col++;
                }
            }

            switch (selVal)
            {
                case "c":
                    title1 = "Cloth";
                    break;
                case "l":
                    title1 = "Leather";
                    break;
                case "m":
                    title1 = "Metal";
                    break;
                case "cl":
                    title1 = "Cloth";
                    title2 = "Leather";
                    break;
                case "cm":
                    title1 = "Cloth";
                    title2 = "Metal";
                    break;
                case "lm":
                    title1 = "Leather";
                    title2 = "Metal";
                    break;
                case "clm":
                    title1 = "Cloth";
                    title2 = "Leather";
                    title3 = "Metal";
                    break;
                default:
                    break;
            }

            switch (col)
            {
                case 2:
                    output = string.Format("<table><td><b><u>Dye Name</b></u></td><td><b><u>{0}</b></u></td>", title1);
                    break;
                case 3:
                    output = string.Format("<table><td><b><u>Dye Name</b></u></td><td><b><u>{0}</b></u></td><td><b><u>{1}</b></u></td>", title1, title2);
                    break;
                case 4:
                    output = string.Format("<table><td><b><u>Dye Name</b></u></td><td><b><u>{0}</b></u></td><td><b><u>{1}</b></u></td><td><b><u>{2}</b></u></td>", title1, title2, title3);
                    break;
                default:
                    break;
            }

            using (WebClient client = new WebClient())
            {
                string data = client.DownloadString("https://api.guildwars2.com/v1/colors.json");
                var o = JObject.Parse(data);

                for (int i = 2; i <= 1242; i++)
                {
                    try
                    {
                        string dye_name = (o["colors"][string.Format("{0}", i)]["name"].ToString());
                        string dye_cloth = "";
                        string dye_leather = "";
                        string dye_metal = "";

                        if ((name != "" && name.ToLower() == dye_name.ToLower()) || name == "")
                        {
                            if (cloth == true)
                            {
                                int[] rgb = new int[3];
                                rgb[0] = Convert.ToInt32(o["colors"][string.Format("{0}", i)]["cloth"]["rgb"][0]);
                                rgb[1] = Convert.ToInt32(o["colors"][string.Format("{0}", i)]["cloth"]["rgb"][1]);
                                rgb[2] = Convert.ToInt32(o["colors"][string.Format("{0}", i)]["cloth"]["rgb"][2]);

                                dye_cloth = string.Format("<td><span style=\"background-color: rgb({0},{1},{2}); color: rgb({0},{1},{2});\">________</span></td>", rgb[0], rgb[1], rgb[2]);
                            }
                            if (leather == true)
                            {
                                int[] rgb = new int[3];
                                rgb[0] = Convert.ToInt32(o["colors"][string.Format("{0}", i)]["leather"]["rgb"][0]);
                                rgb[1] = Convert.ToInt32(o["colors"][string.Format("{0}", i)]["leather"]["rgb"][1]);
                                rgb[2] = Convert.ToInt32(o["colors"][string.Format("{0}", i)]["leather"]["rgb"][2]);

                                dye_leather = string.Format("<td><span style=\"background-color: rgb({0},{1},{2}); color: rgb({0},{1},{2});\">________</span></td>", rgb[0], rgb[1], rgb[2]);
                            }
                            if (metal == true)
                            {
                                int[] rgb = new int[3];
                                rgb[0] = Convert.ToInt32(o["colors"][string.Format("{0}", i)]["metal"]["rgb"][0]);
                                rgb[1] = Convert.ToInt32(o["colors"][string.Format("{0}", i)]["metal"]["rgb"][1]);
                                rgb[2] = Convert.ToInt32(o["colors"][string.Format("{0}", i)]["metal"]["rgb"][2]);

                                //dye_metal = string.Format("Metal: <span style=\"background-color: rgb({0},{1},{2}); color: rgb({0},{1},{2});\">color</span>", rgb[0], rgb[1], rgb[2]);
                                dye_metal = string.Format("<td><span style=\"background-color: rgb({0},{1},{2}); color: rgb({0},{1},{2});\">________</span></td>", rgb[0], rgb[1], rgb[2]);
                            }

                            output += string.Format("<tr><td>{0}</td>{1}{2}{3}</tr>", dye_name, dye_cloth, dye_leather, dye_metal);
                        }
                    }
                    catch { }
                }
            }
            output += "</table>";
            return output;
        }
    }
}