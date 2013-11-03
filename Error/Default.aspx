<%@ Page Title="Error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="gw2portal.Error.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="center">
        <h1>Whoops</h1>
        <h3>Looks like something went wrong!</h3>
        <img src="../Content/Error/error_g.png" onContextMenu="return false;"/>       

        <p>
            You may be getting this page if you incorrectly typed in a url, followed a malformed link or one of the Guild Wars 2 APIs are down.  You can check the status of the APIs by <a href="http://gw2stats.net/status/current">clicking here</a>.

            <p>
            If you continue to recieve errors and none of the above apply to you, then please contact me through one of the following methods:<br />
            <br />The "Chat" page
            <br />Twitter (linked at the bottom of every page)
            <br />Email (contact@jefe323.com)
            </p>
        </p>
    </div>
</asp:Content>
