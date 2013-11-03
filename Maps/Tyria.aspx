<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tyria.aspx.cs" Inherits="gw2portal.Maps.Tyria" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Guild Wars 2 Map - Tyria</title>
    <link rel="shortcut icon" href="/Content/favicon.ico" type="image/ico" />
    <link rel="stylesheet" href="http://cdn.leafletjs.com/leaflet-0.5/leaflet.css" />
    <link rel="stylesheet" href="https://d1h9a8s8eodvjz.cloudfront.net/fonts/menomonia/08-02-12/menomonia-italic.css" />
    <script src="http://cdn.leafletjs.com/leaflet-0.5/leaflet.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7/jquery.min.js"></script>
    <link href="map.css" rel="stylesheet" />
</head>

<body>
    <div id="map"></div>
    <script type="text/javascript">
        function unproject(coord) {
            return map.unproject(coord, map.getMaxZoom());
        }

        var map = L.map("map", {
            minZoom: 0,
            maxZoom: 7,
            crs: L.CRS.Simple
        }).setView([0, 0], 0);

        southWest = unproject([0, 32768]);
        northEast = unproject([32768, 0]);

        map.setMaxBounds(new L.LatLngBounds(southWest, northEast));

        //1=tyria, 2= mists
        L.tileLayer("https://tiles.guildwars2.com/1/2/{z}/{x}/{y}.jpg", {
            attribution: 'Map Data @<a href="http://www.arena.net/">ArenaNet</a> - Return to <a href="http://gw2port.al">gw2port.al</a>',
            minZoom: 0,
            maxZoom: 7,
            continuousWorld: true
        }).addTo(map);
     

        //icons
        var waypointIcon = L.icon({ iconUrl: '../Content/Map/waypoint_icon.png' });
        var vistaIcon = L.icon({ iconUrl: '../Content/Map/vista_icon.png' });
        var poiIcon = L.icon({ iconUrl: '../Content/Map/poi_icon.png' });
        var heartIcon = L.icon({ iconUrl: '../Content/Map/heart_icon.png' });
        var skillIcon = L.icon({ iconUrl: '../Content/Map/skill_icon.png' });
        var testIcon = L.icon({ iconUrl: '../Content/camp_blue.png' });

        var vistas = L.layerGroup();
        var pois = L.layerGroup();
        var heart = L.layerGroup();
        var skill = L.layerGroup();
        var waypoints = L.layerGroup();
        var sectors = L.layerGroup();


        $.getJSON("https://api.guildwars2.com/v1/map_floor.json?continent_id=1&floor=2", function (data) {
            var region, gameMap, i, il, poi, marker, name;

            for (region in data.regions) {
                region = data.regions[region];

                L.marker(unproject(region.label_coord), {
                    icon: L.divIcon({ className: "sector_text", html: region.name }),
                    title: region.name
                }).addTo(map);

                for (gameMap in region.maps) {
                    gameMap = region.maps[gameMap];

                    for (i = 0, il = gameMap.points_of_interest.length; i < il; i++) {
                        poi = gameMap.points_of_interest[i];

                        if (poi.type == "waypoint") {
                            marker = L.marker(unproject(poi.coord), {
                                icon: waypointIcon,
                                title: poi.name
                            });
                            waypoints.addLayer(marker);
                        }

                        if (poi.type == "landmark") {
                            marker = L.marker(unproject(poi.coord), {
                                icon: poiIcon,
                                title: poi.name
                            });
                            pois.addLayer(marker);
                        }

                        if (poi.type == "vista") {
                            marker = L.marker(unproject(poi.coord), {
                                icon: vistaIcon,
                                title: poi.name
                            });
                            vistas.addLayer(marker);
                        }
                    }

                    for (i = 0, il = gameMap.skill_challenges.length; i < il; i++) {
                        var sc = gameMap.skill_challenges[i];

                        skill.addLayer(L.marker(unproject(sc.coord), {
                            icon: skillIcon
                        }));
                    }

                    for (i = 0, il = gameMap.tasks.length; i < il; i++) {
                        var task = gameMap.tasks[i];

                        heart.addLayer(L.marker(unproject(task.coord), {
                            icon: heartIcon,
                            title: "(" + task.level + ") " + task.objective
                        }));
                    }

                    for (i = 0, il = gameMap.sectors.length; i < il; i++) {
                        name = gameMap.sectors[i];

                        sectors.addLayer(L.marker(unproject(name.coord), {
                            icon: L.divIcon({ className: "sector_text_small", html: name.name }),
                            title: name.name
                        }));
                    }
                }
            }
        });

        waypoints.addTo(map);

        var overlayMaps = {
            "Vistas": vistas,
            "Waypoints": waypoints,
            "Points of Interest": pois,
            "Skill Challenges": skill,
            "Tasks": heart,
            "Sector Names": sectors
        };

        L.control.layers(null, overlayMaps).addTo(map);

        map.on("zoomend", function () {
            if (map.getZoom() > 4) {
                sectors.addTo(map);
                pois.addTo(map);
                vistas.addTo(map);
                skill.addTo(map);
                heart.addTo(map);
            }
            else {
                map.removeLayer(sectors);
                map.removeLayer(pois);
                map.removeLayer(vistas);
                map.removeLayer(skill);
                map.removeLayer(heart);
            }
        });
    </script>
</body>
</html>
