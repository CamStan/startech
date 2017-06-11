// Persistant variables 
var map;
var markers = [];
var markersInSight = [];
var center; 

// Get value from the text box.
document.getElementById("zipCode").addEventListener('click', function () {
    document.getElementById("zipCode").value = "";
});


function initMap() {
    // Center - this value comes from the controller
    center = new google.maps.LatLng({
        lat: centerRaw.results[0].geometry.location.lat,
        lng: centerRaw.results[0].geometry.location.lng
    });
    placeArea(centerRaw.results[0].formatted_address);
    map = new google.maps.Map(document.getElementById('map'), {
        center: center,
        zoom: 9
    });

    var geocoder = new google.maps.Geocoder();

    // Set listeners on the input box and button to search.
    document.getElementById("zipCode").addEventListener('keypress', function (e) {
        var key = e.which || e.keyCode;
        if (key === 13) {
            geocodeAddress(geocoder, map);
        }
    });

    document.getElementById('getZip').addEventListener('click', function () {
        geocodeAddress(geocoder, map);
    });

    // Popup window when a marker is clicked.
    var largeInfowindow = new google.maps.InfoWindow();

    map.setCenter(center);

    // Customize the center marker
    var marker = new google.maps.Marker({
        icon: {
            path: google.maps.SymbolPath.CIRCLE,
            fillColor: 'black',
            strokeColor: 'silver',
            scale: 7
        },
        map: map,
        position: center,
        title: 'Center of Postal Code'
    });
    /* Make the center marker the very first marker in the array 
        This will also mean that all location markers will start
        at index 1
    */
    markers.push(marker);
    for (var i = 0; i < arry.length; i++) {
        // Get longitude and latitude
        var position = {
            lat: arry[i].Position.lat,
            lng: arry[i].Position.lng
        };

        var marker = new google.maps.Marker({
            position: position,
            map: map,
            id: i
        });
        // Push the marker into our array of markers.
        markers.push(marker);
        // Create an onclick event to open an infowindow at each marker.
        marker.addListener('click', function () {
            populateInfoWindow(this, largeInfowindow);
        });
    }
    //'dragend', 'zoom_changed' multiple listeners.
    map.addListener('dragend', updateSearch);
    map.addListener('zoom_changed', updateSearch);
    map.addListener('tilesloaded', updateSearch);

    // Remove and add markers as the map is done moving
    function updateSearch() {
        markersInSight = [];
        var bounds = map.getBounds();
        var inViewResults = "";
        for (var i = 1; i < markers.length; i++) {
            var marker = markers[i];
            if (bounds.contains(marker.getPosition())) {
                markersInSight.push(marker);
            }
        }
        if (markersInSight.length > 0) {
            // The distance to each marker from center is not stored in the
            // marker so we must calculate it 
            markersInSight.sort(function (a, b) { return findMileage(a) - findMileage(b) });
            // Write the HTML to display the search results
            for (var j = 0; j < markersInSight.length; j++) {
                var markerInSight = markersInSight[j];
                inViewResults += createDiv(markerInSight);
            }
        }
        
        $("#searchResults").empty();
        // if there's no markers shown on the map, use the default message
        if (inViewResults == "") {
            inViewResults = getMessageBox();
        }
        $("#searchResults").append(inViewResults);
    };
}

// Gets the location when search button is pressed.
function geocodeAddress(geocoder, resultsMap) {
    var zipCode = document.getElementById('zipCode').value;
    if (zipCode == "")
    {
        zipCode = "97304"; // Default to the IPG HQ in Salem, OR
    }
    geocoder.geocode({ 'address': zipCode }, function (results, status) {
        if (status == 'OK') {
            center = results[0].geometry.location;
            placeArea(results[0].formatted_address);
            resultsMap.setCenter(results[0].geometry.location);
            markers[0].setPosition(results[0].geometry.location);
        } else {
            alert('Enter a valid address, partial address or postal code');
        }
    });
}

// Creates the popup when marker is clicked.
function populateInfoWindow(marker, infowindow) {

    infowindow.marker = marker;
    var contentLine = createContent(marker);
    infowindow.setContent(contentLine);
    infowindow.open(map, marker);
    // maker sure the marker property is cleared if the infowindow is closed.
    infowindow.addListener('closeclick', function () {
        infowindow.marker = null;
    });
}

// Creates the text within the marker popup.
function createContent(marker) {
    var contentLine = "";
    var business = arry[marker.id];

    // Piece together string. Check if item has
    // a name and website, exclude if it doesn't.
    // First line of text
    if (business.BusinessName != "" && business.BusinessName != null) {
        contentLine = '<div class="infowindow"><div class="business">'
                    + business.BusinessName + '</div>';
    } else
        if (business.FullName != "" && business.FullName != null) {
            contentLine += '<div class="business">'
                        + business.FullName
                        + '</div>';
        } else {
            contentLine = '<div class="infowindow">';
        }

    // Address should always be present
    // Second line of text
    contentLine += '<div class="address">'
                + business.Address1
                + '</div>'
                + '<div class="address">'
                + business.Address2
                + '</div>';
    // Check to see if website is empty or null
    // Third line of text
    if (business.Website != "" && business.Website != null) {
        contentLine += '<div class="site-link"><a href=' + business.Website + '>Visit their website!</a></div></div>'
    } else {
        contentLine += '</div>';
    }
    return contentLine;
}

// Creates the search result box for each marker seen on map.
function createDiv(marker) {

    var business = arry[marker.id];
    // Add row for result box and add a left spacer column to start
    var contentLine = "<div class='row listing'>" + "<div class='col-lg-1'><p></p></div>";
    // Add a result box for showing results.
    contentLine += "<div class='col-lg-10 memberProfileBox w3-padding-12'>";
    // First column in result box for name of business and address
    contentLine += "<a href=" + "'/Member/Details/" + business.MemberID.toString() + "'>";
    contentLine += "<div class='col-sm-4'>";


    // 
    if (business.BusinessName != "" && business.BusinessName != null) {
        contentLine += '<div class="business">'
                    + business.BusinessName
                    + '</div>';
    } else
        if (business.FullName != "" && business.FullName != null) {
            contentLine += '<div class="business">'
                        + business.FullName
                        + '</div>';
        } else {
            contentLine += '<div class="business">'
                        + '<br />'
                        + '</div>';
        }
    // Address should always be present
    contentLine += '<div class="address">'
                + business.Address1
                + '</div>'
                + '<div class="address">'
                + business.Address2
                + '</div>';
    // Close off name and address column.
    contentLine += '</div></a>';

    // Further contact information (Middle Column)
    contentLine += "<div class='col-sm-4'>";
    if (business.PhoneNumber != "" && business.PhoneNumber != null) {
        contentLine += "<div class='PhoneNumber'>"
                    + "Phone Number : " + business.PhoneNumber
                    + "</div>";
    } else {
        contentLine += "<div><br /></div>";
    }

    if (business.Email != "" && business.Email != null) {
        contentLine += "<div class='email'>"
                    + "Email : " + business.Email
                    + "</div>";
    } else {
        contentLine += "<div><br /></div>";
    }
    if (business.Website != "" && business.Website != null) {
        contentLine += '<div class="site-link"><a href='
                     + business.Website + '>'
                     + business.Website
                     + "</a></div>";
    } else {
        contentLine += '<div><br /></div>';
    }
    // Close off contact info column
    contentLine += "</div>";

    // Compute approximate distance from center to marker without Google
    var distance = findMileage(marker);

    // Display distance (Third Column)
    contentLine += "<a href=" + "'/Member/Details/" + business.MemberID.toString() + "'>";
    contentLine += "<div class='col-sm-4'>";
    //contentLine += "<div><br /></div>";
    contentLine += "<div class='mileage'> Straight line distance: </div>";
    contentLine += "<div class='mileageResults'>"
                + distance + " km"
                + "</div>";

    // Close off mileage box
    contentLine += "</div>";

    // Close off result view box, add right spacer and close row.
    contentLine += "</div><div class='col-lg-1'></div></a></div>";
    return contentLine;
}

// Use Spherical Law of Cosines to find the distance between points
function findMileage(marker) {
    var centerLat = degToRads(center.lat());
    var destLat = degToRads(marker.getPosition().lat());
    var centerLng = center.lng();
    var destLng = marker.getPosition().lng();
    var lngDifRad = degToRads(destLng - centerLng);
    var earthRad = 6371;  //Replace with 3959 if want to display miles.

    var dist = Math.acos(Math.sin(centerLat) * Math.sin(destLat) + Math.cos(centerLat) * Math.cos(destLat) * Math.cos(lngDifRad)) * earthRad;

    return Math.ceil(dist);  // Round up to nearest whole number
}

//convert degress to radians
function degToRads(degrees) {
    var pi = Math.PI;
    return degrees * (pi / 180);
}

// Returns HTML of empty result message
function getMessageBox() {
    return '<div id="searchResults">'
            + '<div class="row listing">'
            + '<div class="col-lg-1"><p></p></div>'
            + '<div class="col-lg-10 memberProfileBox w3-padding-12">'
            + '<p>No results found. You can enter an address, a general location like a city, state or '
            + 'a postal code. You can also zoom the map out and drag it around to find a pin outside of '
            + 'the viewable map area.</p>'
            + '</div>'
            + '<div class="col-lg-1"></div>'
            + '</div></div>'
}

// Search result message
function placeArea(area)
{
    $('#searchArea').empty();
    $('#searchArea').append("Centered at : " + area + "<br /><br />");
}