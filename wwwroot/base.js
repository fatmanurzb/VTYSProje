

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

$(document).on("click", ".courseSelectBtn", function (e) {
    e.preventDefault();
    const id = $(this).data("id");

    var selectedRow = $(".unSelectedCourse").filter(function () {
        return $(this).data("id") == id;
    });

    selectedRow.detach();
    selectedRow.removeClass("unSelectedCourse").addClass("selectedCourse");
    selectedRow.find("button").off("click").removeClass("btn-success courseSelectBtn").addClass("btn-danger courseSelected").text("Çıkart").on("click");
    $("#selectedCoursesTable").append(selectedRow);
});


$(document).on("click", ".courseUnSelectBtn", function (e) {
    e.preventDefault();
    const id = $(this).data("id");

    var selectedRow = $(".selectedCourse").filter(function () {
        return $(this).data("id") == id;
    });

    selectedRow.detach();
    selectedRow.removeClass("selectedCourse").addClass("unSelectedCourse");
    selectedRow.find("button").off("click").removeClass("btn-danger courseSelectBtn").addClass("btn-success courseSelected").text("Ekle").on("click");
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




