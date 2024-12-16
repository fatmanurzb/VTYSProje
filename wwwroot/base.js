$(document).ready(function () {
    $("#userProfileBtn").on("click", function () {
        $.ajax({
            url: "/Home/UserProfile",
            type: "GET",

            success: function (response) {
                $("#PartialBody").html(response);
            }
        });
    });

    $("#getAllAdminBtn").on("click", function () {
        $.ajax({
            url: "/Home/GetAllAdmin",
            type: "GET",

            success: function (response) {
                $("#PartialBody").html(response);
            }
        });
    });
});


