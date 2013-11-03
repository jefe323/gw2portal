<%@ Page Title="Chat" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="gw2portal.Chat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
        Already have an IRC client? Connect to irc.esper.net:6667 and join channel #gw2portal<br />
        Need help with IRC? Here is a handy <a href="http://www.esper.net/getting_started.php">guide</a>
        <iframe src="http://webchat.esper.net/?nick=Guest...&channels=gw2portal&prompt=1" width="700" height="700"></iframe>
    </div>
</asp:Content>
