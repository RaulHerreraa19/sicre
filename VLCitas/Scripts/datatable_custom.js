$(document).ready(function () {
    $(".buttons-copy, .buttons-csv, .buttons-print, .buttons-pdf, .buttons-excel, .buttons-collection").addClass("btn btn-primary mr-1");

    var table = $('#example1').DataTable({
        dom: 'Bfrtip',
        buttons: [
       {
           extend: 'copy',
           text: 'Copiar',
           exportOptions: {
               columns: ':visible'
           }
       },
        {
            title: 'Sistema Integral de Citas',
            extend: 'csv',
            text: 'Csv',
            exportOptions: {
                columns: ':visible'
            }
        }
        ,
        {
            title: 'Sistema Integral de Citas',
            extend: 'excel',
            text: 'Excel',
            exportOptions: {
                columns: ':visible'
            }
        }
        ,
        {
            extend: 'pdfHtml5',
            message: 'REPORTE DE CITAS EN OFICINA: ',
            text: 'PDF',
            title: 'Sistema Integral de Citas',
            //customize: function (doc) {
            //    // Splice the image in after the header, but before the table
            //    doc.content.splice(1, 0, {
            //        margin: [0, 0, 0, 12],
            //        alignment: 'center',
            //        image: image_url,
            //    });
            //},
            //orientation: 'landscape',
            //pageSize: 'LEGAL',
            exportOptions: {
                columns: ':visible'
            }
            //},
            //message: 'Sistema Control de Turnos'
        }
        ,
        {
            extend: 'print',
            message: 'REPORTE DE CITAS EN OFICINA: ',
            title: 'Sistema Integral de Citas',
            text: 'Imprimir',
            exportOptions: {
                columns: ':visible'
            },

        },
          {
              extend: 'colvis',
              text: 'Seleccionar',

          }
        ],
        "language": {
            "sProcessing": "Procesando...",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "Ningún dato disponible en esta tabla",
            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
            "sInfoPostFix": "",
            "sSearch": "Buscar:",
            "sUrl": "",
            "sInfoThousands": ",",
            "sLoadingRecords": "Cargando...",
            "oPaginate": {
                "sFirst": "Primero",
                "sLast": "Último",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            buttons: {
                copyTitle: 'Copiado al portapapeles',
                copySuccess: {
                    _: 'Copiadas %d filas',
                    1: 'Copiada 1 fila'
                }
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }

        }
    });

    table.buttons().container()
        .appendTo('#example_wrapper .col-md-6:eq(0)');
    $(".buttons-collection").addClass("btn btn-primary mr-1");
});