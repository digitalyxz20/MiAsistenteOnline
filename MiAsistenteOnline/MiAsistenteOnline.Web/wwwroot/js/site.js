﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function getDataAjax(GrupoArticulo, action) {
    $.ajax({
        type: "get",
        url: action,
        data: { GrupoArticulo },
        success: function (response) {
            $("#productosCategoria").html(response);
        }

    }

    );

}