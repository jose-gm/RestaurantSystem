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
    document.getElementById("modal-delete-titulo").innerHTML = titulo;
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
            $('#borrarModal').modal('hide');
            toastr.error("Error al borrar");
            //alert(response);
        },
        success: function () {
            $('#' + table).DataTable().ajax.reload();
            $('#borrarModal').modal('hide');
            toastr.success("Eliminado");
        }
    });
}

//Elimina las ordenes
function eliminarOrdenes(_url, table, id,callback) {
    var url = _url;
    $.ajax({
        url: url,
        data: { id: id },
        error: function (response) {
            $('#borrarModal').modal('hide');
            //toastr.error("Error al borrar");
            //alert(response);
        },
        success: function () {
            $('#' + table).DataTable().ajax.reload(callback);
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