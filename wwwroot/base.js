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

    $(".mainNavBtn").on("click", function () {
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

    $(".tableDetailBtn").on("click", function () {
        e.preventDefault();

        const url = $(this).data("url");
        const id = $(this).data("id");

        $.ajax({
            url: url,
            type: "POST",
            data: {Id : id},
            success: function (response) {
                $("#PartialBody").html(response);
            }
        });
    });


    $("#getAllAdminBtn").on("click", function () {
        $.ajax({
            url: "/Admin/Index",
            type: "GET",

            success: function (response) {
                $("#PartialBody").html(response);
            }
        });
    });

    $("#getAllTeacherBtn").on("click", function () {
        $.ajax({
            url: "/Teacher/Index",
            type: "GET",

            success: function (response) {
                $("#PartialBody").html(response);
            }
        });
    });
    $("#getAllStudentsBtn").on("click", function () {
        $.ajax({
            url: "/Students/Index",
            type: "GET",

            success: function (response) {
                $("#PartialBody").html(response);
            }
        });
    });

    $("#getAllCoursesBtn").on("click", function () {
        $.ajax({
            url: "/Courses/Index",
            type: "GET",

            success: function (response) {
                $("#PartialBody").html(response);
            }
        });
    });


});


