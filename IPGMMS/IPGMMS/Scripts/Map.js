var zipCode;

document.getElementById("getZip").addEventListener("click", function () {
    zipCode = document.getElementById("zipCode").value;
    var source = "https://maps.googleapis.com/maps/api/geocode/json?address=";
    var key = "&key=AIzaSyCMzq0fLRdhVhgT42oiQrfu-gz9m0ftvhk";
    var finalAddie = source + zipCode + key;
    $.ajax({
        type: "GET",
        url: finalAddie,
        success: function
    })
});

