<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="gw2portal.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="Prices" runat="server">
        <div align="center">
            <img runat="server" src="~/Content/bl_logo.png" onContextMenu="return false;"/><br />
            <img runat="server" src="~/Content/rates.png" onContextMenu="return false;"/><br />
            <span style="font-size: 8pt">Updates every 15 minutes, may not be exact</span>
            <p>
                Gem to Gold: <%=gem2gold %><br />
                <span style="font-size: 8pt">Number of coins you will recieve for 100 gems</span><br />
            </p>
            <p>
                Gold to Gem: <%=gold2gem %><br />
                <span style="font-size: 8pt">Number of coins you will spend for 100 gems</span><br />
            </p>
        </div>
    </asp:Panel>

    <hr class="main" />

    <asp:Panel ID="EventPanel" runat="server">
        <div align ="center">
            <img runat="server" src="~/Content/events_logo.png" onContextMenu="return false;"/><br />
            <img runat="server" src="~/Content/events.png" onContextMenu="return false;"/><br />           
              World:   
            <asp:DropDownList ID="WorldBox" runat="server">
                <asp:ListItem Value="2204">Abaddon&#39;s Mouth</asp:ListItem>
                <asp:ListItem Value="1001">Anvil Rock</asp:ListItem>
                <asp:ListItem Value="2105">Arborstone</asp:ListItem>
                <asp:ListItem Value="2103">Augury Rock</asp:ListItem>
                <asp:ListItem Value="2013">Aurora Glade</asp:ListItem>
                <asp:ListItem Value="2301">Baruch Bay</asp:ListItem>
                <asp:ListItem Value="1019">Blackgate</asp:ListItem>
                <asp:ListItem Value="2004">Blacktide</asp:ListItem>
                <asp:ListItem Value="1002">Borlis Pass</asp:ListItem>
                <asp:ListItem Value="1014">Crystal Desert</asp:ListItem>
                <asp:ListItem Value="1012">Darkhaven</asp:ListItem>
                <asp:ListItem Value="2002">Desolation</asp:ListItem>
                <asp:ListItem Value="1023">Devona&#39;s Rest</asp:ListItem>
                <asp:ListItem Value="1021">Dragonbrand</asp:ListItem>
                <asp:ListItem Value="2205">Drakkar Lake</asp:ListItem>
                <asp:ListItem Value="2207">Dzagonur</asp:ListItem>
                <asp:ListItem Value="1010">Ehmry Bay</asp:ListItem>
                <asp:ListItem Value="2203">Elona Reach</asp:ListItem>
                <asp:ListItem Value="1024">Eredon Terrace</asp:ListItem>
                <asp:ListItem Value="2007">Far Shiverpeaks</asp:ListItem>
                <asp:ListItem Value="1020">Ferguson&#39;s Crossing</asp:ListItem>
                <asp:ListItem Value="2001">Fissure of Woe</asp:ListItem>
                <asp:ListItem Value="1009">Fort Aspenwood</asp:ListItem>
                <asp:ListItem Value="2102">Fort Ranik</asp:ListItem>
                <asp:ListItem Value="2003">Gandara</asp:ListItem>
                <asp:ListItem Value="1007">Gate of Madness</asp:ListItem>
                <asp:ListItem Value="2014">Gunnar&#39;s Hold</asp:ListItem>
                <asp:ListItem Value="1004">Henge of Denravi</asp:ListItem>
                <asp:ListItem Value="1015">Isle of Janthir</asp:ListItem>
                <asp:ListItem Value="1008">Jade Quarry</asp:ListItem>
                <asp:ListItem Value="2101">Jade Sea</asp:ListItem>
                <asp:ListItem Value="1022">Kaineng</asp:ListItem>
                <asp:ListItem Value="2201">Kodash</asp:ListItem>
                <asp:ListItem Value="1005">Maguuma</asp:ListItem>
                <asp:ListItem Value="2206">Miller&#39;s Sound</asp:ListItem>
                <asp:ListItem Value="1018">Northern Shiverpeaks</asp:ListItem>
                <asp:ListItem Value="2012">Piken Square</asp:ListItem>
                <asp:ListItem Value="2005">Ring of Fire</asp:ListItem>
                <asp:ListItem Value="2202">Riverside</asp:ListItem>
                <asp:ListItem Value="2009">Ruins of Surmia</asp:ListItem>
                <asp:ListItem Value="1016">Sea of Sorrows</asp:ListItem>
                <asp:ListItem Value="2010">Seafarer&#39;s Rest</asp:ListItem>
                <asp:ListItem Value="1013">Sanctum of Rall</asp:ListItem>
                <asp:ListItem Value="1011">Stormbluff Isle</asp:ListItem>
                <asp:ListItem Value="1006">Sorrow&#39;s Furnace</asp:ListItem>
                <asp:ListItem Value="1017">Tarnished Coast</asp:ListItem>
                <asp:ListItem Value="2006">Underworld</asp:ListItem>
                <asp:ListItem Value="2011">Vabbi</asp:ListItem>
                <asp:ListItem Value="2104">Vizunah Square</asp:ListItem>
                <asp:ListItem Value="2008">Whiteside Ridge</asp:ListItem>
                <asp:ListItem Value="1003">Yak's Bend</asp:ListItem>
            </asp:DropDownList>
              Map:   
            <asp:DropDownList ID="MapBox" runat="server">
                <asp:ListItem Value="20">Blazeridge Steppes</asp:ListItem>
                <asp:ListItem Value="73">Bloodtide Coast</asp:ListItem>
                <asp:ListItem Value="54">Brisban Wildlands</asp:ListItem>
                <asp:ListItem Value="34">Caledon Forest</asp:ListItem>
                <asp:ListItem Value="62">Cursed Shore</asp:ListItem>
                <asp:ListItem Value="32">Diessa Plateau</asp:ListItem>
                <asp:ListItem Value="26">Dredgehaunt Cliffs</asp:ListItem>
                <asp:ListItem Value="21">Fields of Ruin</asp:ListItem>
                <asp:ListItem Value="22">Fireheart Rise</asp:ListItem>
                <asp:ListItem Value="30">Frostgorge Sound</asp:ListItem>
                <asp:ListItem Value="24">Gendarran Fields</asp:ListItem>
                <asp:ListItem Value="17">Harathi Hinterlands</asp:ListItem>
                <asp:ListItem Value="25">Iron Marches</asp:ListItem>
                <asp:ListItem Value="23">Kessex Hills</asp:ListItem>
                <asp:ListItem Value="27">Lornar&#39;s Pass</asp:ListItem>
                <asp:ListItem Value="65">Malchor&#39;s Leap</asp:ListItem>
                <asp:ListItem Value="35">Metrica Province</asp:ListItem>
                <asp:ListItem Value="39">Mount Maelstrom</asp:ListItem>
                <asp:ListItem Value="19">Plains of Ashford</asp:ListItem>
                <asp:ListItem Value="15">Queensdale</asp:ListItem>
                <asp:ListItem Value="31">Snowden Drifts</asp:ListItem>
                <asp:ListItem Value="873">Southsun Cove</asp:ListItem>
                <asp:ListItem Value="53">Sparkfly Fen</asp:ListItem>
                <asp:ListItem Value="51">Straits of Devastation</asp:ListItem>
                <asp:ListItem Value="29">Timberline Falls</asp:ListItem>
                <asp:ListItem Value="28">Wayfarer Foothills</asp:ListItem>
            </asp:DropDownList>
              Staus:   
            <asp:DropDownList ID="StatusBox" runat="server">
                <asp:ListItem>All</asp:ListItem>
                <asp:ListItem>Active</asp:ListItem>
                <asp:ListItem>Success</asp:ListItem>
                <asp:ListItem>Fail</asp:ListItem>
                <asp:ListItem>Warmup</asp:ListItem>
                <asp:ListItem>Preparation</asp:ListItem>
            </asp:DropDownList>
            <p><asp:Button ID="EventButton" runat="server" Text="Search!" OnClick="EventButton_Click" /></p>
            </div>
    </asp:Panel>

    <hr class="main" />

    <asp:Panel ID="WvWPanel" runat="server">
        <div align="center">
        <img runat="server" src="~/Content/wvw_logo2.png" onContextMenu="return false;"/><br />
        <img runat="server" src="~/Content/wvw.png" onContextMenu="return false;"/><br />
            World:   
            <asp:DropDownList ID="wWorldBox" runat="server">
                <asp:ListItem Value="2204">Abaddon&#39;s Mouth</asp:ListItem>
                <asp:ListItem Value="1001">Anvil Rock</asp:ListItem>
                <asp:ListItem Value="2105">Arborstone</asp:ListItem>
                <asp:ListItem Value="2103">Augury Rock</asp:ListItem>
                <asp:ListItem Value="2013">Aurora Glade</asp:ListItem>
                <asp:ListItem Value="2301">Baruch Bay</asp:ListItem>
                <asp:ListItem Value="1019">Blackgate</asp:ListItem>
                <asp:ListItem Value="2004">Blacktide</asp:ListItem>
                <asp:ListItem Value="1002">Borlis Pass</asp:ListItem>
                <asp:ListItem Value="1014">Crystal Desert</asp:ListItem>
                <asp:ListItem Value="1012">Darkhaven</asp:ListItem>
                <asp:ListItem Value="2002">Desolation</asp:ListItem>
                <asp:ListItem Value="1023">Devona&#39;s Rest</asp:ListItem>
                <asp:ListItem Value="1021">Dragonbrand</asp:ListItem>
                <asp:ListItem Value="2205">Drakkar Lake</asp:ListItem>
                <asp:ListItem Value="2207">Dzagonur</asp:ListItem>
                <asp:ListItem Value="1010">Ehmry Bay</asp:ListItem>
                <asp:ListItem Value="2203">Elona Reach</asp:ListItem>
                <asp:ListItem Value="1024">Eredon Terrace</asp:ListItem>
                <asp:ListItem Value="2007">Far Shiverpeaks</asp:ListItem>
                <asp:ListItem Value="1020">Ferguson&#39;s Crossing</asp:ListItem>
                <asp:ListItem Value="2001">Fissure of Woe</asp:ListItem>
                <asp:ListItem Value="1009">Fort Aspenwood</asp:ListItem>
                <asp:ListItem Value="2102">Fort Ranik</asp:ListItem>
                <asp:ListItem Value="2003">Gandara</asp:ListItem>
                <asp:ListItem Value="1007">Gate of Madness</asp:ListItem>
                <asp:ListItem Value="2014">Gunnar&#39;s Hold</asp:ListItem>
                <asp:ListItem Value="1004">Henge of Denravi</asp:ListItem>
                <asp:ListItem Value="1015">Isle of Janthir</asp:ListItem>
                <asp:ListItem Value="1008">Jade Quarry</asp:ListItem>
                <asp:ListItem Value="2101">Jade Sea</asp:ListItem>
                <asp:ListItem Value="1022">Kaineng</asp:ListItem>
                <asp:ListItem Value="2201">Kodash</asp:ListItem>
                <asp:ListItem Value="1005">Maguuma</asp:ListItem>
                <asp:ListItem Value="2206">Miller&#39;s Sound</asp:ListItem>
                <asp:ListItem Value="1018">Northern Shiverpeaks</asp:ListItem>
                <asp:ListItem Value="2012">Piken Square</asp:ListItem>
                <asp:ListItem Value="2005">Ring of Fire</asp:ListItem>
                <asp:ListItem Value="2202">Riverside</asp:ListItem>
                <asp:ListItem Value="2009">Ruins of Surmia</asp:ListItem>
                <asp:ListItem Value="1016">Sea of Sorrows</asp:ListItem>
                <asp:ListItem Value="2010">Seafarer&#39;s Rest</asp:ListItem>
                <asp:ListItem Value="1013">Sanctum of Rall</asp:ListItem>
                <asp:ListItem Value="1011">Stormbluff Isle</asp:ListItem>
                <asp:ListItem Value="1006">Sorrow&#39;s Furnace</asp:ListItem>
                <asp:ListItem Value="1017">Tarnished Coast</asp:ListItem>
                <asp:ListItem Value="2006">Underworld</asp:ListItem>
                <asp:ListItem Value="2011">Vabbi</asp:ListItem>
                <asp:ListItem Value="2104">Vizunah Square</asp:ListItem>
                <asp:ListItem Value="2008">Whiteside Ridge</asp:ListItem>
                <asp:ListItem Value="1003">Yak's Bend</asp:ListItem>
            </asp:DropDownList>

            <p><asp:Button ID="WorldButton" runat="server" Text="Search!" OnClick="WorldButton_Click"/></p>
        </div>
    </asp:Panel>
        
    <hr class="main" />

    <asp:Panel ID="Panel4" runat="server">
        <div align="center">
            <img runat="server" src="~/Content/dye_logo.png" onContextMenu="return false;"/><br />
            <img runat="server" src="~/Content/dyes.png" onContextMenu="return false;"/><br />
            <asp:CheckBoxList ID="MatList" runat="server" RepeatDirection="Horizontal"> 
                <asp:ListItem>Cloth</asp:ListItem>
                <asp:ListItem>Leather</asp:ListItem>
                <asp:ListItem>Metal</asp:ListItem>
            </asp:CheckBoxList>
        
            <asp:TextBox ID="DyeNameBox" runat="server"></asp:TextBox><br />
            Dye Name (Optional)

            <p><asp:Button ID="DyeButton" runat="server" Text="Go!" OnClick="DyeButton_Click" /></p>

        </div>       
    </asp:Panel>
    
    <asp:Panel ID="Panel2" runat="server" Enabled="False" Visible="False">
        <h3>Item Database</h3>
        <p>Soon...</p>
    </asp:Panel>

    <asp:Panel ID="Panel3" runat="server" Enabled="False" Visible="False">
        <h3>Recipe Database</h3>
        <p>Soon...</p>
    </asp:Panel>
</asp:Content>
