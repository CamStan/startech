var map;
var markers = [];

document.getElementById("zipCode").addEventListener('click', function () {
    document.getElementById("zipCode").value = "";
});


function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 40.7413549, lng: -73.9980244 },
        zoom: 8
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

    var largeInfowindow = new google.maps.InfoWindow();

    //var centerLat = center.lat;
   // var centerLng = center.lng;
    var pos = {
        lat: center.lat,
        lng: center.lng
    };
    map.setCenter(pos);
    var marker = new google.maps.Marker({
        icon: {
            path: google.maps.SymbolPath.CIRCLE,
            fillColor: 'black',
            strokeColor: 'silver',
            scale: 7
        },
        map: map,
        position: pos,
        title: 'Center of Postal Code'
    });
    markers.push(marker);
    for (var i = 0; i < arry.length; i++) {
        // Get longitude and latitude
        var position = {
            lat: arry[i].position.lat,
            lng: arry[i].position.lng
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

    map.addListener('center_changed', function () {
        var bounds = map.getBounds();
        for (var i = 1; i < markers.length; i++) {
            var marker = markers[i];
            if (bounds.contains(marker.getPosition())) {
                var content = createContent(marker);
                console.log(content);
            }
            else {
                console.log("not in bounds");
            }
        }
    });
}

function geocodeAddress(geocoder, resultsMap) {
    var zipCode = document.getElementById('zipCode').value;
    geocoder.geocode({ 'address': zipCode }, function (results, status) {
        if (status == 'OK') {
            resultsMap.setCenter(results[0].geometry.location);
            markers[0].setPosition(results[0].geometry.location);
        } else {
            alert('Postal code was not found for the following reason: '
                + status);
        }
    });
}

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

function createContent(marker)
{
    var contentLine = "";

    // Piece together string. Check if item has
    // a name and website, exclude if it doesn't.
    if (arry[marker.id].businessName != "" && arry[marker.id].businessName != null) {
        contentLine = '<div class="infowindow"><div class="business">'
                    + arry[marker.id].businessName + '</div>';
    } else {
        contentLine = '<div class="infowindow">';
    }
    // Address should always be present
    contentLine += '<div class="address">' + arry[marker.id].address1 + '</div>'
        + '<div class="address">' + arry[marker.id].address2 + '</div>';
    // Check to see if website is empty or null
    if (arry[marker.id].website != "" && arry[marker.id].website != null) {
        contentLine += '<div class="site-link"><a href="http://' + arry[marker.id].website + '">Visit their website!</div></div>'
    } else {
        contentLine += '</div>';
    }
    return contentLine;
}