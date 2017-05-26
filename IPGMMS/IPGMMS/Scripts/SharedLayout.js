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


