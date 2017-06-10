// Script to open and close sidebar
function w3_open() {
    document.getElementById("mySidebar").style.display = "block";
    document.getElementById("myOverlay").style.display = "block";
}

function w3_close() {
    document.getElementById("mySidebar").style.display = "none";
    document.getElementById("myOverlay").style.display = "none";
}

//Sidebar for Admin Portal
var mySidebar = document.getElementById("adminPortalBar");

function w3_open_bar() {
    if (adminPortalBar.style.display === 'block') {
        adminPortalBar.style.display = 'none';
    } else {
        adminPortalBar.style.display = 'block';
    }
}
function w3_close_bar() {
    adminPortalBar.style.display = "none";
}

// If a user clicks anywhere but the divs classed as navButton close sideNav
// If they click on the navButton elements, open sideNave. This is needed
// so the click event doesn't get ate.
$(document).click(function (event) {
    if ($(event.target).is('.navButton')) {
        w3_open();
    }
    if (!$(event.target).is('.navButton')) {
        w3_close();
    }
});


