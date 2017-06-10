// Open the default tab (as choosen in HTML) when document is ready
$(document).ready(function () {
    var b = document.getElementById("firstClick");
    b.click();
});

function openTab(evt, name) {
    // Declare the variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and pour the invisibility
    // potion on them.
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++)
    {
        tabcontent[i].style.display = "none";
    }

    // get all elements with class="tablinks" and remove the class "active"
    tablinks = document.getElementsByClassName("tablinks");
    for(i = 0; i < tablinks.length; i++)
    {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(name).style.display = "block";
    evt.currentTarget.className += " active";
}