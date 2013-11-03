<%@ Page Title="World vs World" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WvW.aspx.cs" Inherits="gw2portal.WvW" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link runat="server" rel="stylesheet" type="text/css" href="http://cdn.leafletjs.com/leaflet-0.5.1/leaflet.css"/>
    <link runat="server" rel="stylesheet" type="text/css" href="https://d1h9a8s8eodvjz.cloudfront.net/fonts/menomonia/08-02-12/menomonia.css"/>
	<link runat="server" rel="stylesheet" type="text/css" href="https://d1h9a8s8eodvjz.cloudfront.net/fonts/menomonia/08-02-12/menomonia-italic.css"/>

    <style>
        .map
        {
            width: 750px;
            height: 600px;
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
        <img runat="server" src="~/Content/wvw_logo2.png" onContextMenu="return false;"/>        
        <h1><img runat="server" src="~/Content/wvw_banner_title.png" onContextMenu="return false;"/><br /><%=tWorld %></h1>
            <span style="font-size: 8pt">
                This page auto-updates every minute
            </span>
            <hr class="main" />
        

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">            
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick"></asp:Timer>
            <table>
                <tr>
                    <td class="space"><span style="color: green; font-size: large; font-weight: bold;"><%=world.Values.ElementAt(0) %></span></td>
                    <td class="space"><span style="color: blue; font-size: large; font-weight: bold;"><%=world.Values.ElementAt(1) %></span></td>
                    <td class="space"><span style="color: red; font-size: large; font-weight: bold;"><%=world.Values.ElementAt(2) %></span></td>
                </tr>
                <tr>
                    <td class="space"><%=mainScore[0] %></td>
                    <td class="space"><%=mainScore[1] %></td>
                    <td class="space"><%=mainScore[2] %></td>
                </tr></table>

            <table>
                <tr>
                    <th class="space">Contested Areas</th>
                    <th class="space">Potential Points</th>
                    <tr>
                        <td class="space">
                            <img runat="server" src="~/Content/Icons/camp_green.png" />
                            x<%=greenPoint[0] %><img runat="server" src="~/Content/Icons/tower_green.png" />x<%=greenPoint[1] %><img runat="server" src="~/Content/Icons/keep_green.png" />x<%=greenPoint[2] %><img runat="server" src="~/Content/Icons/castle_green.png" />x<%=greenPoint[3] %></td>
                        <td class="space"><%=greenIn %></td>
                    </tr>
                    <tr>
                        <td class="space">
                            <img runat="server" src="~/Content/Icons/camp_blue.png" />
                            x<%=bluePoint[0] %><img runat="server" src="~/Content/Icons/tower_blue.png" />x<%=bluePoint[1] %><img runat="server" src="~/Content/Icons/keep_blue.png" />x<%=bluePoint[2] %><img runat="server" src="~/Content/Icons/castle_blue.png" />x<%=bluePoint[3] %></td>
                        <td class="space"><%=blueIn %></td>
                    </tr>
                    <tr>
                        <td class="space">
                            <img runat="server" src="~/Content/Icons/camp_red.png" />
                            x<%=redPoint[0] %><img runat="server" src="~/Content/Icons/tower_red.png" />x<%=redPoint[1] %><img runat="server" src="~/Content/Icons/keep_red.png" />x<%=redPoint[2] %><img runat="server" src="~/Content/Icons/castle_red.png" />x<%=redPoint[3] %></td>
                        <td class="space"><%=redIn %></td>
                    </tr>
                </tr>
            </table>
               
                 

            <hr class="main" />
                <!--<asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="Green Borderlands" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Blue Borderlands"></asp:ListItem>
                    <asp:ListItem Text="Red Borderlands"></asp:ListItem>
                    <asp:ListItem Text="Eternal Battlegrounds"></asp:ListItem>
                </asp:RadioButtonList>
                <asp:Panel ID="GreenPanel" runat="server" Visible="true">
                    <div id="map1" class="map">
                </asp:Panel>
                <asp:Panel ID="BluePanel" runat="server" Visible="false">
                    <div id="map2" class="map">
                </asp:Panel>
                <asp:Panel ID="RedPanel" runat="server" Visible="false">
                    <div id="map3" class="map">
                </asp:Panel>
                <asp:Panel ID="CenterPanel" runat="server" Visible="false">
                    <div id="map4" class="map">
                </asp:Panel>-->
                
                <div id="map2" class="map"></div>
                <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
	            <script src="http://cdn.leafletjs.com/leaflet-0.5.1/leaflet.js"></script>
                <script language="javascript" src="<%=ResolveUrl ("~/wvwMap.js")%>"></script>

                <script>
                    function EndRequestHandler() {
                        (function mapUpdate() {
                            gw2map('<%=matchID %>', "map2", "en", 2, 3, 7);
                        })();
                    }
                    function load() {
                        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                        (function mapUpdate() {
                            gw2map('<%=matchID %>', "map2", "en", 2, 3, 7);
                        })();
                    }
                </script>
                </ContentTemplate>   
            </asp:UpdatePanel>
        </div >
</asp:Content>
