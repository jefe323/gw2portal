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
function gw2map(world_id, map_container, language, continent_id, floor_id, region_id, map_id, poi_id, poi_type) {
    // first of all determine the max zoomlevel given in continents.json - Tyria: 7, The Mists: 6
    var eventsRA = [];
    var mz = continent_id == 1 ? 7 : 6,
		// the map object
		leaf = L.map(map_container, { minZoom: 0, maxZoom: mz, crs: L.CRS.Simple }),
		// some marker icons
		icon_wp = L.icon({ iconUrl: "/Content/Map/waypoint_icon.png", iconSize: [20, 20], iconAnchor: [10, 10], popupAnchor: [0, -10] }),
        icon_event = L.icon({ iconUrl: "/Content/Map/event_icon.png" }),
		icon_poi = L.icon({ iconUrl: "/Content/Map/poi_icon.png", iconSize: [20, 20], iconAnchor: [10, 10], popupAnchor: [0, -10] }),
		icon_vista = L.icon({ iconUrl: "/Content/Map/vista_icon.png", iconSize: [20, 20], iconAnchor: [10, 10], popupAnchor: [0, -10] }),
		icon_heart = L.icon({ iconUrl: "/Content/Map/heart_icon.png", iconSize: [20, 20], iconAnchor: [10, 10], popupAnchor: [0, -10] }),
		icon_skill = L.icon({ iconUrl: "/Content/Map/skill_icon.png", iconSize: [20, 20], iconAnchor: [10, 10], popupAnchor: [0, -10] }),

		// set the layerGroups
		vistas = L.layerGroup(),
        lEvents = L.layerGroup(),
        points = L.layerGroup(),
		pois = L.layerGroup(),
		tasks = L.layerGroup(),
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

		    //my horrible, horrible system to get active events on the map...
            $.getJSON("https://api.guildwars2.com/v1/maps.json?map_id=" + map_id, function (data) {
		        var m_rect_0, m_rect_1, c_rect_0, c_rect_1

		        m_rect_0 = data.maps[map_id].map_rect[0];
		        m_rect_1 = data.maps[map_id].map_rect[1]
		        c_rect_0 = data.maps[map_id].continent_rect[0]
		        c_rect_1 = data.maps[map_id].continent_rect[1]

                //get array of all active events
		        $.getJSON("https://api.guildwars2.com/v1/events.json?world_id=" + world_id, function (data) {
		            var myEvents;

		            myEvents = data.events

		            for (var i = 0; i < myEvents.length; i++) {
		                if (myEvents[i].map_id == map_id) {
		                    if (myEvents[i].state == "Active") {
		                        eventsRA.push(myEvents[i].event_id);
		                    }
		                }
		            }

		        for (var i = 0; i < eventsRA.length; i++) {
		            var url = "https://api.guildwars2.com/v1/event_details.json?event_id=" + eventsRA[i];
		            
		            $.getJSON("https://api.guildwars2.com/v1/event_details.json?event_id=" + eventsRA[i], function (data) {
		                var events, locations, center, type, e_center_x, e_center_y, Px, Py, Cx, Cy, event_coords;

		                for (events in data.events) {
		                    events = data.events[events];

		                    e_center_x = events.location.center[0];
		                    e_center_y = events.location.center[1];

		                    Px = (e_center_x - m_rect_0[0]) / (m_rect_1[0] - m_rect_0[0]);
		                    Py = 1 - (e_center_y - m_rect_0[1]) / (m_rect_1[1] - m_rect_0[1]);

		                    Cx = (c_rect_0[0] + (c_rect_1[0] - c_rect_0[0]) * Px);
		                    Cy = (c_rect_0[1] + (c_rect_1[1] - c_rect_0[1]) * Py);

		                    event_coords = [Cx, Cy];
		                    lEvents.addLayer(L.marker(leaf.unproject(event_coords, mz), { icon: icon_event, title: events.name }));
		                }

		            });		        
		        }
		        });
            });


		    // show stuff on the map
		    //pois.addTo(leaf);
		    lEvents.addTo(leaf);
		    //skills.addTo(leaf);
		    //tasks.addTo(leaf);
		    //vistas.addTo(leaf);
		    //sectors.addTo(leaf);
		    waypoints.addTo(leaf);

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
        "Active Events": lEvents,
        "Sector Names": sectors,
        "Skill Challenges": skills,
        "Tasks": tasks,
        "Vistas": vistas,
        "Waypoints": waypoints
    }).addTo(leaf);

    // magically display/remove sector names
    leaf.on("zoomend", function () {
        if (leaf.getZoom() > 4) {
            sectors.addTo(leaf);
        }
        else {
            leaf.removeLayer(sectors);
        }
    });

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