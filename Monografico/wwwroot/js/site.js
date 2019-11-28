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

//Abre el modal de mensaje de confirmacion para eliminar un inventario
var temp;
function abrirModalBorrar(id) {
    temp = id;
    $('#borrarModal').modal({
        keyboard: false,
        backdrop: 'static'
    });
}

//Elimina un inventario usando ajax
function eliminar(_url, table) {
    var url = _url;
    $.ajax({
        url: url,
        data: { id: temp },
        error: function (response) { alert(response); },
        success: function () {
            $('#' + table).DataTable().ajax.reload();
            $('#borrarModal').modal('hide');
            toastr.success("Eliminado");
        }
    });
}