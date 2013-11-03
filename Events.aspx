<%@ Page Title="Dynamic Events" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="gw2portal.Events" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="http://cdn.leafletjs.com/leaflet-0.5.1/leaflet.css"/>
    <link rel="stylesheet" type="text/css" href="https://d1h9a8s8eodvjz.cloudfront.net/fonts/menomonia/08-02-12/menomonia.css"/>
	<link rel="stylesheet" type="text/css" href="https://d1h9a8s8eodvjz.cloudfront.net/fonts/menomonia/08-02-12/menomonia-italic.css"/>

    <style>
        .map
        {
            width: 750px;
            height: 750px;
        }
        .sector_text{
			color: #fff;
			font-family: Menomonia;
			font-style: italic;
			font-size: 10px;
			white-space: nowrap;
		}
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div align="center">
        <br />
        <img runat="server" src="~/Content/events_logo.png" onContextMenu="return false;"/>
        <h1><img runat="server" src="~/Content/event_title.png" onContextMenu="return false;"/><br /><%=world_name %></h1>
        <h3><img runat="server" src="~/Content/event_map_title.png" onContextMenu="return false;" /><br /><%=map_name %><br /><img runat="server" src="~/Content/event_status_title.png" onContextMenu="return false;" /><br /><%=status %></h3>
        <p>
            <span style="font-size: 8pt">
                Please Note: These links are generated automatically to the Guild Wars 2 Wiki and not all of the linked pages exist yet<br />
                This page checks for updates to events every minute
            </span>
        </p>
    
    <p>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">            
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick"></asp:Timer>
                <asp:Literal ID="outputLit" runat="server" /> 
            
    </p>

    <p>
        <asp:PlaceHolder ID="MapPlaceHolder" runat="server" Visible="false">
        <div id="map2" class="map"></div>
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
	    <script src="http://cdn.leafletjs.com/leaflet-0.5.1/leaflet.js"></script>
        <script language="javascript" src="<%=ResolveUrl ("~/Maps/ZoneMap.js")%>"></script>

        <script>
            function EndRequestHandler() {
                (function mapUpdate() {
                    gw2map('<%=world_id %>', "map2", "en", 1, 1, '<%=region_id %>', '<%=map_id %>');
                })();
            }
            function load() {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                (function mapUpdate() {
                    gw2map('<%=world_id %>', "map2", "en", 1, 1, '<%=region_id %>', '<%=map_id %>');
                })();
            }
        </script>
                        
        </asp:PlaceHolder>
    </p>
            </ContentTemplate>   
        </asp:UpdatePanel>
    </div>
</asp:Content>
