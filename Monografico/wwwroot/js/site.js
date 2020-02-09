// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Agrega el formulario de inventario al body del modal
function agregarCrear(_url) {
    $('#modalBody').load(_url, () => {
        $('#myModal').modal({
            keyboard: false,
            backdrop: 'static'
        });
    });
}

//Agrega el formulario de editar al body del modal con los campos del modelo buscado por el id
function agregarEditar(id, _url) {
    //Recive el partial view atravez de ajax y lo inserta dentro del elemento #editarModal-body
    $('#modalBody').load(_url + "/" + id, () => {
        $('#myModal').modal({
            keyboard: false,
            backdrop: 'static'
        });
    });
}

//Abre el modal de mensaje de confirmacion para eliminar
var temp;
function abrirModalBorrar(id) {
    temp = id;
    $('#borrarModal').modal({
        keyboard: false,
        backdrop: 'static'
    });
}

//Abre el modal de mensaje de confirmacion para eliminar
/*function abrirModalBorrar() {
    $('#borrarModal').modal({
        keyboard: false,
        backdrop: 'static'
    });
}*/

function abrirModalMensaje(titulo, imgSrc) {
    $("h4#modal-delete-titulo").text(titulo);
    document.getElementById("modal-delete-imagen").src = imgSrc;
    $('#borrarModal').modal({
        keyboard: false,
        backdrop: 'static'
    });
}

function cerrarModalMensaje() {
    $('#borrarModal').modal('hide');
}

function agregarIngredientes(id,_url) {
    $('#modalBody-lg').load(_url + "/" + id, () => {
        $('#myModal-lg').modal({
            keyboard: true,
            backdrop: 'static'
        });
    });
}

//Elimina un modelo usando ajax
function eliminar(_url, table) {
    var url = _url;
    $.ajax({
        url: url,
        data: { id: temp },
        error: function (response) {
            console.log(response);
            $('#borrarModal').modal('hide');

            if(response !== null)
                toastr.error(response.responseText !== "" ? response.responseText : "Error al eliminar la zona");
            else
                toastr.error("Error al eliminar la zona");
        },
        success: function () {
            $('#' + table).DataTable().ajax.reload();
            $('#borrarModal').modal('hide');
            toastr.success("Eliminado");
        }
    });
}

function getActualDateUTC() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = mm + '/' + dd + '/' + yyyy;

    return new Date(today).toUTCString();
}

function getActualDateISO() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = mm + '/' + dd + '/' + yyyy;

    return new Date(today).toISOString();
}

function formatMoney(number, decPlaces, decSep, thouSep) {
    decPlaces = isNaN(decPlaces = Math.abs(decPlaces)) ? 2 : decPlaces,
        decSep = typeof decSep === "undefined" ? "." : decSep;
    thouSep = typeof thouSep === "undefined" ? "," : thouSep;
    var sign = number < 0 ? "-" : "";
    var i = String(parseInt(number = Math.abs(Number(number) || 0).toFixed(decPlaces)));
    var j = (j = i.length) > 3 ? j % 3 : 0;

    return "RD$" + sign +
        (j ? i.substr(0, j) + thouSep : "") +
        i.substr(j).replace(/(\decSep{3})(?=\decSep)/g, "$1" + thouSep) +
        (decPlaces ? decSep + Math.abs(number - i).toFixed(decPlaces).slice(2) : "");
}

