// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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

function agregarcarrito(nombre, precio, elemento) {

    var nombreA = elemento + "A";
    var cantidad = document.getElementById(nombreA).value;
    alert( "SE añadio a carrito " );

    NombreProductos[contador] = nombre;
    Precio[contador] = precio;
    Cantidad[contador] = cantidad;
    Subtotal[contador] = (parseFloat(cantidad).toFixed(2) * parseFloat(precio).toFixed(2)).toFixed(2);
    contador = contador + 1;

    var cadena = '';
    total = 0;
    for (var i = 0; i < contador; i++) {

        total = (parseFloat(total) + parseFloat(Subtotal[i]));
     
        cadena = cadena + '<tr><th scope = "col" >' + NombreProductos[i] + '</th ><th scope="col">' + Precio[i] + '</th><th scope="col">' + Cantidad[i] + '</th><th scope="col">' + Subtotal[i] + '</th></tr >';
    } 

    document.getElementById("descripcionCompra").innerHTML = cadena;
    document.getElementById("total").innerHTML = "Total : S/."+total.toString();

    document.getElementById("descripcionCompraModal").innerHTML = cadena;
    document.getElementById("totalModal").innerHTML = "Total : S/." + total.toString();
}