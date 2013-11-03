using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Xml;
using System.Data;
using System.Text;

namespace gw2portal
{
    public partial class News : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        
        //forgive me for my sins...
        public void testRSS()
        {
            using (WebClient client = new WebClient())
            {
                string final = "";
                string output = client.DownloadString("https://www.guildwars2.com/en/feed/");
                string[] data = output.Split(new string[] { "<item>" }, StringSplitOptions.None);
                //Console.WriteLine(data[1]);

                char[] delimiters = new char[] { '\r', '\n' };
                for (int i = 1; i < data.Length; i++)
                {
                    string title = "";
                    string link = "";
                    string pub = "";
                    string content = "";
                    string[] parts = data[i].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    for (int k = 0; k < parts.Length; k++)
                    {
                        if (parts[k].Trim().StartsWith("<title>"))
                        {
                            title = parts[k].Trim().Replace("<title>", "").Replace("</title>", "");
                        }

                        if (parts[k].Trim().StartsWith("<link>"))
                        {
                            link = parts[k].Trim().Replace("<link>", "").Replace("</link>", "");
                        }

                        if (parts[k].Trim().StartsWith("<pubDate>"))
                        {
                            pub = parts[k].Trim().Replace("<pubDate>", "").Replace("</pubDate>", "").Replace(" +0000", "");
                        }

                        if (parts[k].Trim().StartsWith("<content:encoded>"))
                        {
                            content = parts[k].Trim().Replace("<content:encoded>", "").Replace("</content:encoded>", "");
                            for (int j = k; j < parts.Length; j++)
                            {
                                if (!parts[j].Trim().EndsWith("</content:encoded>"))
                                {
                                    content += parts[j].Trim().Replace("<content:encoded>", "").Replace("</content:encoded>", "");
                                }
                                else
                                    break;
                            }
                        }
                    }
                    final += "<h3><a href=\""+ link +"\">" + title + "</a></h3>" + pub + "<br />" + content + "<hr class=\"main\"/>";
                }
                Response.Write(final);
            }            
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NewsList.SelectedIndex == 0)
            {
                siteNews.Visible = false;
                gwNews.Visible = true;
            }
            else
            {
                gwNews.Visible = false;
                siteNews.Visible = true;                
            }
        }
    }
}