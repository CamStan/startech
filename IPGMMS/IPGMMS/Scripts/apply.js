/*
    Checkbox on application form that, when clicked, duplicates all the information in the mailing contact information fields
    and puts the same information in the listing contact information fields
*/
$(function () {
    var checkbox = $("#trigger");
    var hidden = $("#hidden_form");
    //hidden.hide();

    checkbox.change(function () {
        if (checkbox.is(':checked')) {
            document.getElementById("ListingInfo_Email").value = document.getElementById("MailingInfo_Email").value;
            document.getElementById("ListingInfo_PhoneNumber").value = document.getElementById("MailingInfo_PhoneNumber").value;
            document.getElementById("ListingInfo_StreetAddress").value = document.getElementById("MailingInfo_StreetAddress").value;
            document.getElementById("ListingInfo_City").value = document.getElementById("MailingInfo_City").value;
            document.getElementById("ListingInfo_StateName").value = document.getElementById("MailingInfo_StateName").value;
            document.getElementById("ListingInfo_Country").value = document.getElementById("MailingInfo_Country").value;
            document.getElementById("ListingInfo_PostalCode").value = document.getElementById("MailingInfo_PostalCode").value;
        } else {
            document.getElementById("ListingInfo_Email").value = "";
            document.getElementById("ListingInfo_PhoneNumber").value = "";
            document.getElementById("ListingInfo_StreetAddress").value = "";
            document.getElementById("ListingInfo_City").value = "";
            document.getElementById("ListingInfo_StateName").value = "";
            document.getElementById("ListingInfo_Country").value = "";
            document.getElementById("ListingInfo_PostalCode").value = "";
        }
    });
});