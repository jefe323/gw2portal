<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Zones.aspx.cs" Inherits="gw2portal.Maps.Zones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="shortcut icon" href="/Content/favicon.ico" type="image/ico" />
    <style type="text/css">
        html, body
        {
            margin: 0;
            padding: 0;
            height: 100%;
        }

        #background
        {
            width: 100%;
            height: 100%;
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: -1;
        }

        .stretch
        {
            width: 100%;
            height: 100%;
        }

        body {
            font-size: .85em;
            font-family: "Segoe UI", Verdana, Helvetica, Sans-Serif;
        }

        .center
        {   
            text-align: center;
            margin-left: auto;
            margin-right: auto;
        }

        div.main
        {
            width: 90%;
            padding: 3px;
            margin: 0px;
            border-style: outset;
            border-width: 3px;
            margin-left: auto;
            margin-right: auto;
            /*background-color:rgba(150,150,150,0.9);*/
            background-color:rgba(164,159,99,0.9);
        }

        ul.nav
        {
            text-align: center;
        }

        li.nav
        {    
            display: inline;
            padding-left: 10px;
            padding-right: 10px;
        }

        a.link
        {
            color:#FFFFFF;
        }
	</style>
</head>
<body>
    <div id="background">
        <img runat="server" src="~/Content/bg.jpg" class="stretch" alt="" />
    </div>    
    <form id="form1" runat="server">
        <div class="center">
            <a runat="server" href="~/Default.aspx"><img runat="server" src="~/Content/site_logo.png" /></a>
            <ul class="nav">
            <li class="nav"><a runat="server" href="~/Default.aspx"><img runat="server" src="~/Content/Menu/home.png" /></a></li>
            <li class="nav"><a runat="server" href="~/News.aspx"><img runat="server" src="~/Content/Menu/news.png" /></a></li>
            <li class="nav"><a runat="server" href="~/Maps"><img runat="server" src="~/Content/Menu/maps.png" /></a></li>
            <li class="nav"><a runat="server" href="~/Chat.aspx"><img runat="server" src="~/Content/Menu/chat.png" /></a></li>
        </ul>
        </div>   

        <div class="main">
            <div class="gw2map" data-language="2" data-region_id='<%=region_id %>' data-map_id='<%=map_id %>' data-height="800" data-width="100" data-w_percent="1" data-linkbox="400" data-poi_id="225" data-poi_type="4"></div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
	<script src="http://cdn.leafletjs.com/leaflet-0.6.2/leaflet.js"></script>
    <script language="javascript" src="<%=ResolveUrl ("~/Maps/Zones.js")%>"></script>

    <script>


        jQuery(document).ready(function () {
            $.each([
				"http://cdn.leafletjs.com/leaflet-0.6.2/leaflet.css",
				"https://d1h9a8s8eodvjz.cloudfront.net/fonts/menomonia/08-02-12/menomonia.css",
				"https://d1h9a8s8eodvjz.cloudfront.net/fonts/menomonia/08-02-12/menomonia-italic.css",
				"<%=ResolveUrl ("gw2maps.css")%>"
            ], function () {// used in the wiki map widget since we cannot access the header there
                var ref = document.createElement("link");
                ref.setAttribute("rel", "stylesheet");
                ref.setAttribute("href", this);
                document.getElementsByTagName("head")[0].appendChild(ref);
            });


            /* temporary GW2Wiki workaround */
            var gw2maps = function (c) { $(c).each(function () { GW2Maps.init(this); }); };

            gw2maps(".gw2map");
            //			$(".gw2map").each(function(){
            //				GW2Maps.init(this);
            //			});
        });
	</script>
            <div class="center"><asp:Label ID="Label1" runat="server" Font-Size="Small" ForeColor="White">This page created by using code from <a class="link" href="https://github.com/codemasher/gw2api-tools">smiley.1438</a></asp:Label></div>
        </div>
        
        <div class="center"><asp:Label ID="footer" runat="server" Font-Size="Small" ForeColor="White">Site Created by <a class="link" href="https://twitter.com/ElJefe323">jefe323</a></asp:Label><br /><br /></div>
    </form>
</body>
</html>
