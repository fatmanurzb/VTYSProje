﻿$("#userProfileBtn").on("click", function () {
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
            if (url == "/Home/Index") {
                window.location = "/Home";
            }
            else {
                $("#PartialBody").html(response);
            }
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

    $.ajax({
        url: "/Login/Logout",
        type: "GET",
        success: function (response) {
            window.location = response.redirectUrl;
        }
    });
});

$(document).on("click", ".courseSelectBtn", function (e) {
    e.preventDefault();
    const id = $(this).data("id");

    var selectedRow = $(".unSelectedCourse").filter(function () {
        return $(this).data("id") == id;
    });

    selectedRow.detach();
    selectedRow.removeClass("unSelectedCourse").addClass("selectedCourse");
    selectedRow.find("button").off("click").removeClass("btn-success courseSelectBtn").addClass("btn-danger courseUnSelectBtn").text("Çıkart").on("click");
    $("#selectedCoursesTable").append(selectedRow);
});


$(document).on("click", ".courseUnSelectBtn", function (e) {
    e.preventDefault();
    debugger;
    const id = $(this).data("id");

    var selectedRow = $(".selectedCourse").filter(function () {
        return $(this).data("id") == id;
    });

    selectedRow.detach();
    selectedRow.removeClass("selectedCourse").addClass("unSelectedCourse");
    selectedRow.find("button").off("click").removeClass("btn-danger courseUnSelectBtn").addClass("btn-success courseSelectBtn").text("Ekle").on("click");
    $("#unSelectedCoursesTable").append(selectedRow);
});


$(document).on("click", "#selectedCoursesSaveBtn", function (e) {
    debugger;
    var selectedCourses = [];
    const url = $(this).data("url");

    if ($("#selectedCoursesTable").find("tr").length == 0) {
        alert("Hiç ders seçmediniz!");
        return;
    }

    $("#selectedCoursesTable").find("tr").each(function () {
        var trTag = $(this);
        const id = trTag.data("id");
        selectedCourses.push(id);
    })

    $.ajax({
        url: url,
        type: "POST",
        data: { SelectedCoursesIds: JSON.stringify(selectedCourses) },

        success: function (response) {
            alert(response.message);
            $("button").prop("disabled", true);
        }
    })
});

$(document).on("click", ".studentCourseConfirmBtn", function (e) {
    e.preventDefault();

    const id = $(this).data("id");
    const $row = $(this).closest("tr"); // Tıklanan butonun bulunduğu tr'yi seçiyoruz.

    $.ajax({
        url: "/Teacher/StudentCourseConfirm",
        method: "POST",
        data: { Id: id },
        success: function (response) {
            if (response.success) {
                $row.remove(); // Seçilen tr etiketini DOM'dan kaldırıyoruz.
            }

            alert(response.message);
        }
    });
});

$(document).on("click", ".studentCourseRejectBtn", function (e) {
    e.preventDefault();

    const id = $(this).data("id");
    const $row = $(this).closest("tr"); // Tıklanan butonun bulunduğu tr'yi seçiyoruz.

    $.ajax({
        url: "/Teacher/StudentCourseReject",
        method: "POST",
        data: { Id: id },
        success: function (response) {
            if (response.success) {
                $row.remove(); // Seçilen tr etiketini DOM'dan kaldırıyoruz.
            }

            alert(response.message);
        }
    });
});

