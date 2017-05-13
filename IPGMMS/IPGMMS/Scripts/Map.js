var map;
var markers = [];


function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 40.7413549, lng: -73.9980244 },
        zoom: 11
    });

    var geocoder = new google.maps.Geocoder();

    document.getElementById('getZip').addEventListener('click', function() {
        geocodeAddress(geocoder, map);
    });

    var largeInfowindow = new google.maps.InfoWindow();

    var centerLat = center.lat;
    var centerLng = center.lng;
    var pos = {
        lat: centerLat,
        lng: centerLng
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
    for (var i = 0; i < arry.length; i++)
    {
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
        marker.addListener('click', function() {
            populateInfoWindow(this, largeInfowindow);
        });
    }
}

function geocodeAddress(geocoder, resultsMap)
{
    var zipCode = document.getElementById('zipCode').value;
    geocoder.geocode({'address' : zipCode}, function(results, status) {
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
    var contentLine = "";

    // Piece together infowindow string. Check if item has
    // a name and website, exclude if it doesn't.
    if (arry[marker.id].businessName != "" && arry[marker.id].businessName != null)
    {
        contentLine = '<div class="infowindow"><div class="business">'
                    + arry[marker.id].businessName + '</div>';
    } else {
        contentLine = '<div class="infowindow">';
    }
    // Address should always be present
    contentLine += '<div class="address">' + arry[marker.id].address1 + '</div>'
        + '<div class="address">' + arry[marker.id].address2 + '</div>';
    // Check to see if website is empty or null
    if (arry[marker.id].website != "" && arry[marker.id].website != null)
    {
        contentLine += '<div class="site-link"><a href="http://' + arry[marker.id].website + '">Visit their website!</div></div>'
    } else {
        contentLine += '</div>';
    }
    console.log(contentLine);
    console.log("web=" + arry[marker.id].website + "<>");
    infowindow.setContent(contentLine);
    infowindow.open(map, marker);
    // maker sure the marker property is cleared if the infowindow is closed.
    infowindow.addListener('closeclick', function() {
        infowindow.marker = null;
    });
}