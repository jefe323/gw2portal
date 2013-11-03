<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Border.aspx.cs" Inherits="gw2portal.Maps.Border" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=map_name %> Borderlands</title>
    <link href="borderland.css" rel="stylesheet" />
    <link rel="shortcut icon" href="/Content/favicon.ico" type="image/ico" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <img src="../Content/border.png" style="z-index: -5; position: absolute;"/>
        <%=point[0] %>
        <%=point[1] %>
        <%=point[2] %>
        <%=point[3] %>
        <%=point[4] %>
        <%=point[5] %>
        <%=point[6] %>
        <%=point[7] %>
        <%=point[8] %>
        <%=point[9] %>
        <%=point[10] %>
        <%=point[11] %>
        <%=point[12] %>

        <div class="t1"><%=name[0] %></div>
        <div class="t2"><%=name[1] %></div>
        <div class="t3"><%=name[2] %></div>
        <div class="t4"><%=name[3] %></div>
        <div class="t5"><%=name[4] %></div>
        <div class="t6"><%=name[5] %></div>
        <div class="t7"><%=name[6] %></div>
        <div class="t8"><%=name[7] %></div>
        <div class="t9"><%=name[8] %></div>
        <div class="t10"><%=name[9] %></div>
        <div class="t11"><%=name[10] %></div>
        <div class="t12"><%=name[11] %></div>
        <div class="t13"><%=name[12] %></div>
    </div>
    </form>
</body>
</html>
