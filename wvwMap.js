/**
 * gw2Maps.js
 * created: 21.06.13
 * 
 * Code by smiley.1438 
 * https://github.com/codemasher/gw2api-tools/blob/master/js/gw2maps.js
 *
 * based on Cliff's example
 * http://jsfiddle.net/cliff/CRRGC/
 *
 * and Dr. Ishmaels proof of concept
 * http://wiki.guildwars2.com/wiki/User:Dr_ishmael/leaflet
 */
function gw2map(match_id, map_container, language, continent_id, floor_id, region_id, map_id, poi_id, poi_type) {
    // first of all determine the max zoomlevel given in continents.json - Tyria: 7, The Mists: 6
    var mz = continent_id == 1 ? 7 : 6,
		// the map object
		leaf = L.map(map_container, { minZoom: 0, maxZoom: mz, crs: L.CRS.Simple }),
		// some marker icons
		icon_wp = L.icon({ iconUrl: "../Content/Map/waypoint_icon.png", iconSize: [20, 20], iconAnchor: [10, 10], popupAnchor: [0, -10] }),
        icon_event = L.icon({ iconUrl: "../Content/Map/event_icon.png" }),
		icon_poi = L.icon({ iconUrl: "../Content/Map/poi_icon.png", iconSize: [20, 20], iconAnchor: [10, 10], popupAnchor: [0, -10] }),
		icon_vista = L.icon({ iconUrl: "../Content/Map/vista_icon.png", iconSize: [20, 20], iconAnchor: [10, 10], popupAnchor: [0, -10] }),
		icon_heart = L.icon({ iconUrl: "../Content/Map/heart_icon.png", iconSize: [20, 20], iconAnchor: [10, 10], popupAnchor: [0, -10] }),
		icon_skill = L.icon({ iconUrl: "../Content/Map/skill_icon.png", iconSize: [20, 20], iconAnchor: [10, 10], popupAnchor: [0, -10] }),

        red_camp = L.icon({ iconUrl: "../Content/Icons/camp_red.png" }),
        green_camp = L.icon({ iconUrl: "../Content/Icons/camp_green.png" }),
        blue_camp = L.icon({ iconUrl: "../Content/Icons/camp_blue.png" }),
        red_tower = L.icon({ iconUrl: "../Content/Icons/tower_red.png" }),
        green_tower = L.icon({ iconUrl: "../Content/Icons/tower_green.png" }),
        blue_tower = L.icon({ iconUrl: "../Content/Icons/tower_blue.png" }),
        red_castle = L.icon({ iconUrl: "../Content/Icons/castle_red.png" }),
        green_castle = L.icon({ iconUrl: "../Content/Icons/castle_green.png" }),
        blue_castle = L.icon({ iconUrl: "../Content/Icons/castle_blue.png" }),
        red_keep = L.icon({ iconUrl: "../Content/Icons/keep_red.png" }),
        green_keep = L.icon({ iconUrl: "../Content/Icons/keep_green.png" }),
        blue_keep = L.icon({ iconUrl: "../Content/Icons/keep_blue.png" }),

		// set the layerGroups
		vistas = L.layerGroup(),
        objectives = L.layerGroup(),
        points = L.layerGroup(),
		pois = L.layerGroup(),
		skills = L.layerGroup(),
		waypoints = L.layerGroup(),
		sectors = L.layerGroup(),
		// the map parser
		parse_map = function (map) {

		    // determine the wiki prefix
		    var wiki;
		    switch (language) {
		        case "de": wiki = "-de"; break;
		        case "es": wiki = "-es"; break;
		        case "fr": wiki = "-fr"; break;
		        default: wiki = ""; break;
		    }

		    // loop out pois
		    $.each(map.points_of_interest, function () {
		        if (this.type == "waypoint") {
		            waypoints.addLayer(L.marker(leaf.unproject(this.coord, mz), { title: this.name, icon: icon_wp }).bindPopup(this.name));
		        }
		        if (this.type == "landmark") {
		            pois.addLayer(L.marker(leaf.unproject(this.coord, mz), { title: this.name, icon: icon_poi })
						.bindPopup('<a href="http://wiki' + wiki + ".guildwars2.com/wiki/" + encodeURIComponent(this.name) + '" target="_blank">' + this.name + "</a>"));
		        }
		        if (this.type == "vista") {
		            vistas.addLayer(L.marker(leaf.unproject(this.coord, mz), { icon: icon_vista }));
		        }
		    });
		    // sector names
		    $.each(map.sectors, function () {
		        sectors.addLayer(L.marker(leaf.unproject(this.coord, mz), { title: this.name, icon: L.divIcon({ className: "sector_text", html: this.name }) }));
		    });
		    // skill challenges
		    $.each(map.skill_challenges, function () {
		        skills.addLayer(L.marker(leaf.unproject(this.coord, mz), { icon: icon_skill }));
		    });
		    // tasks (hearts)
		    $.each(map.tasks, function () {
		        tasks.addLayer(L.marker(leaf.unproject(this.coord, mz), { title: this.objective, icon: icon_heart })
					.bindPopup('<a href="http://wiki' + wiki + ".guildwars2.com/wiki/" + encodeURIComponent(this.objective.replace(/\.$/, "")) + '" target="_blank">' + this.objective + "</a> (" + this.level + ")"));
		    });


		    $.getJSON("https://api.guildwars2.com/v1/wvw/match_details.json?match_id=" + match_id, function (data) {
		        var maps, red, green, blue, center;

		        maps = data.maps;

		        red = maps[0];
		        green = maps[1];
		        blue = maps[2];
		        center = maps[3];

		        //red

		        if (red.objectives[0].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([11659, 10946], mz), { icon: red_keep })); }
		        else if (red.objectives[0].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([11659, 10946], mz), { icon: green_keep })); }
		        else if (red.objectives[0].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([11659, 10946], mz), { icon: blue_keep })); }

		        if (red.objectives[1].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([9433, 11016], mz), { icon: red_keep })); }
		        else if (red.objectives[1].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([9433, 11016], mz), { icon: green_keep })); }
		        else if (red.objectives[1].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([9433, 11016], mz), { icon: blue_keep })); }

		        if (red.objectives[2].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([10490, 12109], mz), { icon: red_camp })); }
		        else if (red.objectives[2].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([10490, 12109], mz), { icon: green_camp })); }
		        else if (red.objectives[2].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([10490, 12109], mz), { icon: blue_camp })); }

		        if (red.objectives[3].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([10096, 11401], mz), { icon: red_tower })); }
		        else if (red.objectives[3].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([10096, 11401], mz), { icon: green_tower })); }
		        else if (red.objectives[3].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([10096, 11401], mz), { icon: blue_tower })); }

		        if (red.objectives[4].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([10980, 11475], mz), { icon: red_tower })); }
		        else if (red.objectives[4].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([10980, 11475], mz), { icon: green_tower })); }
		        else if (red.objectives[4].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([10980, 11475], mz), { icon: blue_tower })); }

		        if (red.objectives[5].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([10462, 10498], mz), { icon: red_keep })); }
		        else if (red.objectives[5].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([10462, 10498], mz), { icon: green_keep })); }
		        else if (red.objectives[5].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([10462, 10498], mz), { icon: blue_keep })); }

		        if (red.objectives[6].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([9844, 10144], mz), { icon: red_tower })); }
		        else if (red.objectives[6].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([9844, 10144], mz), { icon: green_tower })); }
		        else if (red.objectives[6].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([9844, 10144], mz), { icon: blue_tower })); }

		        if (red.objectives[7].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([10481, 9280], mz), { icon: red_camp })); }
		        else if (red.objectives[7].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([10481, 9280], mz), { icon: green_camp })); }
		        else if (red.objectives[7].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([10481, 9280], mz), { icon: blue_camp })); }

		        if (red.objectives[8].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([11079, 10096], mz), { icon: red_tower })); }
		        else if (red.objectives[8].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([11079, 10096], mz), { icon: green_tower })); }
		        else if (red.objectives[8].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([11079, 10096], mz), { icon: blue_tower })); }

		        if (red.objectives[9].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([11417, 11551], mz), { icon: red_camp })); }
		        else if (red.objectives[9].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([11417, 11551], mz), { icon: green_camp })); }
		        else if (red.objectives[9].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([11417, 11551], mz), { icon: blue_camp })); }

		        if (red.objectives[10].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([11451, 10222], mz), { icon: red_camp })); }
		        else if (red.objectives[10].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([11451, 10222], mz), { icon: green_camp })); }
		        else if (red.objectives[10].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([11451, 10222], mz), { icon: blue_camp })); }

		        if (red.objectives[11].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([9609, 10265], mz), { icon: red_camp })); }
		        else if (red.objectives[11].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([9609, 10265], mz), { icon: green_camp })); }
		        else if (red.objectives[11].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([9609, 10265], mz), { icon: blue_camp })); }

		        if (red.objectives[12].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([9689, 11504], mz), { icon: red_camp })); }
		        else if (red.objectives[12].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([9689, 11504], mz), { icon: green_camp })); }
		        else if (red.objectives[12].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([9689, 11504], mz), { icon: blue_camp })); }

		        //green
		        if (green.objectives[0].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([8057, 13494], mz), { icon: red_keep })); }
		        else if (green.objectives[0].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([8057, 13494], mz), { icon: green_keep })); }
		        else if (green.objectives[0].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([8057, 13494], mz), { icon: blue_keep })); }

		        if (green.objectives[1].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([7397, 14030], mz), { icon: red_tower })); }
		        else if (green.objectives[1].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([7397, 14030], mz), { icon: green_tower })); }
		        else if (green.objectives[1].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([7397, 14030], mz), { icon: blue_tower })); }

		        if (green.objectives[2].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([6906, 14630], mz), { icon: red_camp })); }
		        else if (green.objectives[2].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([6906, 14630], mz), { icon: green_camp })); }
		        else if (green.objectives[2].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([6906, 14630], mz), { icon: blue_camp })); }

		        if (green.objectives[3].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([5847, 13575], mz), { icon: red_keep })); }
		        else if (green.objectives[3].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([5847, 13575], mz), { icon: green_keep })); }
		        else if (green.objectives[3].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([5847, 13575], mz), { icon: blue_keep })); }

		        if (green.objectives[4].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([6562, 13953], mz), { icon: red_tower })); }
		        else if (green.objectives[4].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([6562, 13953], mz), { icon: green_tower })); }
		        else if (green.objectives[4].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([6562, 13953], mz), { icon: blue_tower })); }

		        if (green.objectives[5].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([6872, 13053], mz), { icon: red_keep })); }
		        else if (green.objectives[5].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([6872, 13053], mz), { icon: green_keep })); }
		        else if (green.objectives[5].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([6872, 13053], mz), { icon: blue_keep })); }

		        if (green.objectives[6].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([6255, 12696], mz), { icon: red_tower })); }
		        else if (green.objectives[6].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([6255, 12696], mz), { icon: green_tower })); }
		        else if (green.objectives[6].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([6255, 12696], mz), { icon: blue_tower })); }

		        if (green.objectives[7].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([6025, 12821], mz), { icon: red_camp })); }
		        else if (green.objectives[7].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([6025, 12821], mz), { icon: green_camp })); }
		        else if (green.objectives[7].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([6025, 12821], mz), { icon: blue_camp })); }

		        if (green.objectives[8].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([6109, 14054], mz), { icon: red_camp })); }
		        else if (green.objectives[8].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([6109, 14054], mz), { icon: green_camp })); }
		        else if (green.objectives[8].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([6109, 14054], mz), { icon: blue_camp })); }

		        if (green.objectives[9].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([7858, 12785], mz), { icon: red_camp })); }
		        else if (green.objectives[9].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([7858, 12785], mz), { icon: green_camp })); }
		        else if (green.objectives[9].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([7858, 12785], mz), { icon: blue_camp })); }

		        if (green.objectives[10].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([7828, 14120], mz), { icon: red_camp })); }
		        else if (green.objectives[10].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([7828, 14120], mz), { icon: green_camp })); }
		        else if (green.objectives[10].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([7828, 14120], mz), { icon: blue_camp })); }

		        if (green.objectives[11].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([6906, 11845], mz), { icon: red_camp })); }
		        else if (green.objectives[11].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([6906, 11845], mz), { icon: green_camp })); }
		        else if (green.objectives[11].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([6906, 11845], mz), { icon: blue_camp })); }

		        if (green.objectives[12].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([7496, 12648], mz), { icon: red_tower })); }
		        else if (green.objectives[12].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([7496, 12648], mz), { icon: green_tower })); }
		        else if (green.objectives[12].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([7496, 12648], mz), { icon: blue_tower })); }

		        //blue
		        if (blue.objectives[0].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([14039, 12412], mz), { icon: red_keep })); }
		        else if (blue.objectives[0].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([14039, 12412], mz), { icon: green_keep })); }
		        else if (blue.objectives[0].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([14039, 12412], mz), { icon: blue_keep })); }

		        if (blue.objectives[1].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([14078, 14005], mz), { icon: red_camp })); }
		        else if (blue.objectives[1].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([14078, 14005], mz), { icon: green_camp })); }
		        else if (blue.objectives[1].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([14078, 14005], mz), { icon: blue_camp })); }

		        if (blue.objectives[2].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([13732, 13320], mz), { icon: red_tower })); }
		        else if (blue.objectives[2].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([13732, 13320], mz), { icon: green_tower })); }
		        else if (blue.objectives[2].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([13732, 13320], mz), { icon: blue_tower })); }

		        if (blue.objectives[3].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([14563, 13387], mz), { icon: red_tower })); }
		        else if (blue.objectives[3].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([14563, 13387], mz), { icon: green_tower })); }
		        else if (blue.objectives[3].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([14563, 13387], mz), { icon: blue_tower })); }

		        if (blue.objectives[4].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([13019, 12941], mz), { icon: red_keep })); }
		        else if (blue.objectives[4].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([13019, 12941], mz), { icon: green_keep })); }
		        else if (blue.objectives[4].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([13019, 12941], mz), { icon: blue_keep })); }

		        if (blue.objectives[5].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([14670, 12017], mz), { icon: red_tower })); }
		        else if (blue.objectives[5].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([14670, 12017], mz), { icon: green_tower })); }
		        else if (blue.objectives[5].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([14670, 12017], mz), { icon: blue_tower })); }

		        if (blue.objectives[6].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([14069, 11203], mz), { icon: red_camp })); }
		        else if (blue.objectives[6].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([14069, 11203], mz), { icon: green_camp })); }
		        else if (blue.objectives[6].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([14069, 11203], mz), { icon: blue_camp })); }

		        if (blue.objectives[7].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([13427, 12061], mz), { icon: red_tower })); }
		        else if (blue.objectives[7].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([13427, 12061], mz), { icon: green_tower })); }
		        else if (blue.objectives[7].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([13427, 12061], mz), { icon: blue_tower })); }

		        if (blue.objectives[8].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([15237, 12862], mz), { icon: red_keep })); }
		        else if (blue.objectives[8].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([15237, 12862], mz), { icon: green_keep })); }
		        else if (blue.objectives[8].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([15237, 12862], mz), { icon: blue_keep })); }

		        if (blue.objectives[9].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([13201, 12177], mz), { icon: red_camp })); }
		        else if (blue.objectives[9].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([13201, 12177], mz), { icon: green_camp })); }
		        else if (blue.objectives[9].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([13201, 12177], mz), { icon: blue_camp })); }

		        if (blue.objectives[10].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([13274, 13425], mz), { icon: red_camp })); }
		        else if (blue.objectives[10].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([13274, 13425], mz), { icon: green_camp })); }
		        else if (blue.objectives[10].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([13274, 13425], mz), { icon: blue_camp })); }

		        if (blue.objectives[11].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([15029, 12133], mz), { icon: red_camp })); }
		        else if (blue.objectives[11].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([15029, 12133], mz), { icon: green_camp })); }
		        else if (blue.objectives[11].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([15029, 12133], mz), { icon: blue_camp })); }

		        if (blue.objectives[12].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([14996, 13478], mz), { icon: red_camp })); }
		        else if (blue.objectives[12].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([14996, 13478], mz), { icon: green_camp })); }
		        else if (blue.objectives[12].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([14996, 13478], mz), { icon: blue_camp })); }

		        //center
		        if (center.objectives[0].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([10750, 13664], mz), { icon: red_keep })); }
		        else if (center.objectives[0].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([10750, 13664], mz), { icon: green_keep })); }
		        else if (center.objectives[0].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([10750, 13664], mz), { icon: blue_keep })); }

		        if (center.objectives[1].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([11481, 15099], mz), { icon: red_keep })); }
		        else if (center.objectives[1].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([11481, 15099], mz), { icon: green_keep })); }
		        else if (center.objectives[1].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([11481, 15099], mz), { icon: blue_keep })); }

		        if (center.objectives[2].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([9584, 15112], mz), { icon: red_keep })); }
		        else if (center.objectives[2].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([9584, 15112], mz), { icon: green_keep })); }
		        else if (center.objectives[2].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([9584, 15112], mz), { icon: blue_keep })); }

		        if (center.objectives[3].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([10181, 15437], mz), { icon: red_camp })); }
		        else if (center.objectives[3].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([10181, 15437], mz), { icon: green_camp })); }
		        else if (center.objectives[3].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([10181, 15437], mz), { icon: blue_camp })); }

		        if (center.objectives[4].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([11200, 13774], mz), { icon: red_camp })); }
		        else if (center.objectives[4].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([11200, 13774], mz), { icon: green_camp })); }
		        else if (center.objectives[4].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([11200, 13774], mz), { icon: blue_camp })); }

		        if (center.objectives[5].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([9854, 13549], mz), { icon: red_camp })); }
		        else if (center.objectives[5].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([9854, 13549], mz), { icon: green_camp })); }
		        else if (center.objectives[5].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([9854, 13549], mz), { icon: blue_camp })); }

		        if (center.objectives[6].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([10999, 15493], mz), { icon: red_camp })); }
		        else if (center.objectives[6].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([10999, 15493], mz), { icon: green_camp })); }
		        else if (center.objectives[6].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([10999, 15493], mz), { icon: blue_camp })); }

		        if (center.objectives[7].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([11534, 14419], mz), { icon: red_camp })); }
		        else if (center.objectives[7].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([11534, 14419], mz), { icon: green_camp })); }
		        else if (center.objectives[7].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([11534, 14419], mz), { icon: blue_camp })); }

		        if (center.objectives[8].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([10589, 14561], mz), { icon: red_castle })); }
		        else if (center.objectives[8].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([10589, 14561], mz), { icon: green_castle })); }
		        else if (blue.objectives[8].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([10589, 14561], mz), { icon: blue_castle })); }

		        if (center.objectives[9].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([9539, 14417], mz), { icon: red_camp })); }
		        else if (center.objectives[9].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([9539, 14417], mz), { icon: green_camp })); }
		        else if (center.objectives[9].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([9539, 14417], mz), { icon: blue_camp })); }

		        if (center.objectives[10].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([9363, 14811], mz), { icon: red_tower })); }
		        else if (center.objectives[10].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([9363, 14811], mz), { icon: green_tower })); }
		        else if (center.objectives[10].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([9363, 14811], mz), { icon: blue_tower })); }

		        if (center.objectives[11].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([9892, 14608], mz), { icon: red_tower })); }
		        else if (center.objectives[11].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([9892, 14608], mz), { icon: green_tower })); }
		        else if (center.objectives[11].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([9892, 14608], mz), { icon: blue_tower })); }

		        if (center.objectives[12].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([9791, 15383], mz), { icon: red_tower })); }
		        else if (center.objectives[12].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([9791, 15383], mz), { icon: green_tower })); }
		        else if (center.objectives[12].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([9791, 15383], mz), { icon: blue_tower })); }

		        if (center.objectives[13].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([10154, 15059], mz), { icon: red_tower })); }
		        else if (center.objectives[13].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([10154, 15059], mz), { icon: green_tower })); }
		        else if (center.objectives[13].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([10154, 15059], mz), { icon: blue_tower })); }

		        if (center.objectives[14].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([11434, 15467], mz), { icon: red_tower })); }
		        else if (center.objectives[14].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([11434, 15467], mz), { icon: green_tower })); }
		        else if (center.objectives[14].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([11434, 15467], mz), { icon: blue_tower })); }

		        if (center.objectives[15].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([10837, 15206], mz), { icon: red_tower })); }
		        else if (center.objectives[15].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([10837, 15206], mz), { icon: green_tower })); }
		        else if (center.objectives[15].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([10837, 15206], mz), { icon: blue_tower })); }

		        if (center.objectives[16].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([10246, 13503], mz), { icon: red_tower })); }
		        else if (center.objectives[16].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([10246, 13503], mz), { icon: green_tower })); }
		        else if (center.objectives[16].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([10246, 13503], mz), { icon: blue_tower })); }

		        if (center.objectives[17].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([10177, 14063], mz), { icon: red_tower })); }
		        else if (center.objectives[17].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([10177, 14063], mz), { icon: green_tower })); }
		        else if (center.objectives[17].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([10177, 14063], mz), { icon: blue_tower })); }

		        if (center.objectives[18].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([10995, 13827], mz), { icon: red_tower })); }
		        else if (center.objectives[18].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([10995, 13827], mz), { icon: green_tower })); }
		        else if (center.objectives[18].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([10995, 13827], mz), { icon: blue_tower })); }

		        if (center.objectives[19].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([11077, 13471], mz), { icon: red_tower })); }
		        else if (center.objectives[19].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([11077, 13471], mz), { icon: green_tower })); }
		        else if (center.objectives[19].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([11077, 13471], mz), { icon: blue_tower })); }

		        if (center.objectives[20].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([11144, 14513], mz), { icon: red_tower })); }
		        else if (center.objectives[20].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([11144, 14513], mz), { icon: green_tower })); }
		        else if (center.objectives[20].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([11144, 14513], mz), { icon: blue_tower })); }

		        if (center.objectives[21].owner == "Red") { objectives.addLayer(L.marker(leaf.unproject([11750, 14778], mz), { icon: red_tower })); }
		        else if (center.objectives[21].owner == "Green") { objectives.addLayer(L.marker(leaf.unproject([11750, 14778], mz), { icon: green_tower })); }
		        else if (center.objectives[21].owner == "Blue") { objectives.addLayer(L.marker(leaf.unproject([11750, 14778], mz), { icon: blue_tower })); }

		        
		    });

		    // show stuff on the map
		    //pois.addTo(leaf);
		    //skills.addTo(leaf);
		    //vistas.addTo(leaf);
		    sectors.addTo(leaf);
		    objectives.addTo(leaf);

		    // showing the sector names on the initial map would be confusing in most cases,
		    // so we'll show them automatically only on higher zoom levels - they're anyway in the layer menu
		};


    // set the base tiles
    L.tileLayer("https://tiles.guildwars2.com/" + continent_id + "/" + floor_id + "/{z}/{x}/{y}.jpg", {
        attribution: 'Map Data @<a href="http://www.arena.net/">ArenaNet</a>',
        minZoom: 0,
        maxZoom: mz,
        continuousWorld: true
    }).addTo(leaf);

    // add a Layer control
    L.control.layers(null, {
        "Points of Interest": pois,
        "Objectives": objectives,
        "Sector Names": sectors,
        "Skill Challenges": skills,
        "Vistas": vistas,
        "Waypoints": waypoints
    }).addTo(leaf);

    // magically display/remove sector names
    /*leaf.on("zoomend", function () {
        if (leaf.getZoom() > 4) {
            sectors.addTo(leaf);
        }
        else {
            leaf.removeLayer(sectors);
        }
    });*/

    leaf.on("click", function (e) {
        console.log("You clicked the map at " + leaf.project(e.latlng));
    });

    // get the JSON and start the action
    $.getJSON("https://api.guildwars2.com/v1/map_floor.json?continent_id=" + continent_id + "&floor=" + floor_id + "&lang=" + language, function (data) {
        var bounds, clamp;

        // the map has a clamped view? ok, we use this as bound
        if (data.clamped_view) {
            clamp = data.clamped_view;
            bounds = new L.LatLngBounds(leaf.unproject([clamp[0][0], clamp[1][1]], mz), leaf.unproject([clamp[1][0], clamp[0][1]], mz));
            leaf.setMaxBounds(bounds).fitBounds(bounds);
        }
            // we display a specific map? so lets use the maps bounds
        else if (region_id && map_id) {
            clamp = data.regions[region_id].maps[map_id].continent_rect;
            bounds = new L.LatLngBounds(leaf.unproject([clamp[0][0], clamp[1][1]], mz), leaf.unproject([clamp[1][0], clamp[0][1]], mz)).pad(0.2);
            leaf.setMaxBounds(bounds).fitBounds(bounds);
            // we have also a poi? lets find and display it...
            if (poi_id && poi_type) {
                var a, n;
                switch (poi_type) {
                    //case "skill": t = data.regions[region_id].maps[map_id].skill_challenges; break; //skill challenges don't have ids yet
                    case "poi":
                        a = data.regions[region_id].maps[map_id].points_of_interest;
                        n = "poi_id";
                        break;
                    case "sector":
                        a = data.regions[region_id].maps[map_id].sectors;
                        n = "sector_id";
                        break;
                    case "task":
                        a = data.regions[region_id].maps[map_id].tasks;
                        n = "task_id";
                        break;
                }

                // workaround to get the given poi_id
                // life could be so easy with data.regions[region_id].maps[map_id].points_of_interest[poi_id];
                $.each(a, function () {
                    if (this[n] == poi_id) {
                        leaf.panTo(leaf.unproject(this.coord, mz)).setZoom(mz);
                    }
                });
            }
        }
            // else use the texture_dims as bounds
        else {
            bounds = new L.LatLngBounds(leaf.unproject([0, data.texture_dims[1]], mz), leaf.unproject([data.texture_dims[0], 0], mz));
            leaf.setMaxBounds(bounds).setView(bounds.getCenter(), 0);
        }

        // ok, we want to display a single map
        if (region_id && map_id) {
            parse_map(data.regions[region_id].maps[map_id]);
        }
            // little workaround to display the 1st floor of the worldmap without instance data
        else if (continent_id == 1 && floor_id == 1) {
            $.each(data.regions, function () {
                $.each(this.maps, function (i, m) {
                    //try jQuery.inArray(i, [...]) != -1 instead... jQuery sucks, it reports -1 for all iterations -.-
                    if (in_array(i, [15, 17, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 34, 35, 39, 51, 53, 54, 62, 65, 73, 873, 18, 50, 91, 139, 218, 326, 807])) {
                        parse_map(m);
                    }
                });
            });
        }
            // else render anything we get
        else {
            $.each(data.regions, function () {
                $.each(this.maps, function () {
                    parse_map(this);
                });
            });
        }
    });
}

/**
 *  excerpts from phpJS
 *  @link http://phpjs.org
 */
function in_array(needle, haystack, argStrict) {
    // http://kevin.vanzonneveld.net
    // +   original by: Kevin van Zonneveld (http://kevin.vanzonneveld.net)
    // +   improved by: vlado houba
    // +   input by: Billy
    // +   bugfixed by: Brett Zamir (http://brett-zamir.me)
    // *     example 1: in_array('van', ['Kevin', 'van', 'Zonneveld']);
    // *     returns 1: true
    // *     example 2: in_array('vlado', {0: 'Kevin', vlado: 'van', 1: 'Zonneveld'});
    // *     returns 2: false
    // *     example 3: in_array(1, ['1', '2', '3']);
    // *     returns 3: true
    // *     example 3: in_array(1, ['1', '2', '3'], false);
    // *     returns 3: true
    // *     example 4: in_array(1, ['1', '2', '3'], true);
    // *     returns 4: false
    var key = '',
		strict = !!argStrict;

    if (strict) {
        for (key in haystack) {
            if (haystack[key] === needle) {
                return true;
            }
        }
    }
    else {
        for (key in haystack) {
            if (haystack[key] == needle) {
                return true;
            }
        }
    }

    return false;
}