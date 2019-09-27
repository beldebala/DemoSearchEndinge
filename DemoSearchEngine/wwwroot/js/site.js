// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    $("#searchText").keyup(_.debounce(function (e) {
        var text = document.getElementById('searchText').value;
        if (text.length >= 2) {
            $.post("/home/search", { pattern: text }, function (data) {
                $("#movieList").html(data);
            })
        }
        else {
            $("#movieList").html("");
        }
    }, 500))
})