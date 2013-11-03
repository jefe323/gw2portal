<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="gw2portal.News" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:RadioButtonList ID="NewsList" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal" AutoPostBack="True">
        <asp:ListItem Value="0" Selected="True">Guild Wars News</asp:ListItem>
        <asp:ListItem Value="1">Site News</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Panel ID="gwNews" runat="server">
        <div align="center"><img runat="server" src="~/Content/gw_news_logo.png" onContextMenu="return false;"/><hr class="main" /></div>

        <%testRSS(); %>
    </asp:Panel>

    <asp:Panel ID="siteNews" runat="server" Visible="false">
        <div align="center"><img runat="server" src="~/Content/site_news_logo.png" onContextMenu="return false;"/><hr class="main" /></div>
        27 July 2013
            <h2>Site Launched</h2>
            I've been working on this site on and off for the past month or so and now I am proud to present the final result.  The site is still a work in progress and might be subject to change in the future.  I hope you all enjoy and look forward to new features that will soon be arriving
            <br /> - jefe
    </asp:Panel>
</asp:Content>
