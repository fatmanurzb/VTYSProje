
$("#userProfileBtn").on("click", function () {
    $.ajax({
        url: "/Home/UserProfile",
        type: "GET",

        success: function (response) {
            $("#PartialBody").html(response);
        }
    });
});

$(".mainNavBtn").on("click", function (e) {
    e.preventDefault();

    const url = $(this).data("url");

    $.ajax({
        url: url,
        type: "GET",
        success: function (response) {
            $("#PartialBody").html(response);
        }
    });
});

$(".tableDetailBtn").on("click", function (e) {
    e.preventDefault();

    const url = $(this).data("url");
    const id = $(this).data("id");

    $.ajax({
        url: url,
        type: "POST",
        data: { Id: id },
        success: function (response) {
            $("#PartialBody").html(response);
        }
    });
});

$("#logoutBtn").on("click", function (e) {
    e.preventDefault();
    debugger;
    $.ajax({
        url: "/Login/Logout",
        type: "GET",
        success: function (response) {
            window.location = response.redirectUrl;
        }
    });
});




