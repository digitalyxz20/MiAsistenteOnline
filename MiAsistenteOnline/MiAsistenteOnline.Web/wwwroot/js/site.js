﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.




    var NombreProductos = new Array();
    var Precio = new Array();
    var Cantidad = new Array();
    var Subtotal = new Array();

var contador = 0;
var total = 0; 



//funciones categoria y productos///////////////////////////////////////////////

function getDataAjax(GrupoArticulo, action) {
    $.ajax({
        type: "get",
        url: action,
        data: { GrupoArticulo
    }
    }) // Se ejecuta si todo fue bien.
        .done(function (result) {
            $("#productosCategoria").html(result);
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {

        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });

}





$(function () {
    $("#IrProductos").click(function (e) {
        e.preventDefault();

        $.ajax({
            url: "@Url.Action('BusProductos', 'Products')", // Url
            data: {
                GrupoArticulo: $("#IrProductos").val()// Parámetros
            },
            type: "get"  // Verbo HTTP
        })
            // Se ejecuta si todo fue bien.
            .done(function (result) {
                $("#productosCategoria").html(result);
            })
            // Se ejecuta si se produjo un error.
            .fail(function (xhr, status, error) {

            })
            // Hacer algo siempre, haya sido exitosa o no.
            .always(function () {

            });
    });
});




$("#OcultarGrupos").click(function () {
    $("#v-pills-tab").toggle(500);
});

function OcultarGrupo()
{
    $("#v-pills-tab").toggle(500);
}



//funciones carrito de compras

function AgregarCarritoSessionIni()
{
    $.ajax({
        type: "GET",
        url: "AgregarCarritoSessionIni"
    }) // Se ejecuta si todo fue bien.
        .done(function (result) {
            $("#descripcionCompra").html(result);
            $("#descripcionCompraModal").html(result);
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {

        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });
}

function AgregarCarritoSession(Id, nombre, precio, elemento) {

    var cantidad = $("#" + elemento + "A").val();
    $.ajax({
        type: "POST",
        url: "AgregarCarritoSession",
        data: {
            Id: Id,
            Detalle: nombre,
            Precio: precio,
            Cantidad: cantidad
        }
    }) // Se ejecuta si todo fue bien.
        .done(function (result) {
            alert("Se añadio al carrito!");
            $("#descripcionCompra").html(result);
            $("#descripcionCompraModal").html(result);
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {

        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });

}



function MasCantidad(elemento) {
    var cantidad = parseInt($("#" + elemento + "A").val());
    if (cantidad < 12) {
        cantidad = cantidad + 1;
        $("#" + elemento + "A").val(cantidad);
    }
}

function MenorCantidad(elemento) {
    var cantidad = parseInt($("#" + elemento + "A").val());
    if (cantidad > 1) {
        cantidad = cantidad - 1;
        $("#" + elemento + "A").val(cantidad);
    }
}



function ConfirmarCompra() {

    var cantidad = $("#" + elemento + "A").val();
    $.ajax({
        type: "POST",
        url: "InsertarVenta",
    }) // Se ejecuta si todo fue bien.
        .done(function (result) {
            alert("operacion!");
            $("#mensajesModal").html(result);
        })
        // Se ejecuta si se produjo un error.
        .fail(function (xhr, status, error) {

        })
        // Hacer algo siempre, haya sido exitosa o no.
        .always(function () {

        });

}