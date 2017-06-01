// Fades out the success message on the member details page after a member has successfully applied
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $("#successMessage").fadeOut(4000, "swing");
});