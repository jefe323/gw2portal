<%@ Page Title="Guild Wars 2 Dye List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dyes.aspx.cs" Inherits="gw2portal.Dyes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div align="center">
        <img runat="server" src="~/Content/dye_logo.png" onContextMenu="return false;" /><br />
        <img runat="server" src="~/Content/dye_title.png" onContextMenu="return false;"/>

        <%=getData() %>
        </div>
</asp:Content>
