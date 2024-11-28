var js_language = {
    esMX: {
        DTEditor: {
            "create": {
                "button": "Nuevo",
                "title": "Nuevo Registro",
                "submit": "Crear"
            },

            "edit": {
                "button": "Editar",
                "title": "Editar Registro",
                "submit": "Actualizar"
            },

            "remove": {
                "button": "Eliminar",
                "title": "Eliminar",
                "submit": "Eliminar",
                "confirm": {
                    "_": "Estas seguro de eliminar %d registros?",
                    "1": "Estas seguro de eliminar 1 registro?"
                }
            },

            "error": {
                "system": "Error (Más Información)"
            },

            "multi": {
                "title": "Valores múltiples",
                "info": "Los elementos seleccionados contienen valores diferentes para esta entrada. Para editar y establecer todos los elementos para esta entrada en el mismo valor, haga clic o toque aquí, de lo contrario, retendrán sus valores individuales.",
                "restore": "Deshacer cambios",
                "noMulti": "Esta entrada puede editarse individualmente, pero no es parte de un grupo."
            },

            "datetime": {
                "previous": 'Previo',
                "next": 'Siguiente',
                "months": ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                "weekdays": ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
                "amPm": ['am', 'pm'],
                "unknown": '-'
            }
        },
        DataTables: {
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
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        },
        AppendGrid: {
            append: "Agregar nueva fila",
            removeLast: "Precaución! Quitar la última fila!",
            insert: "Insertar nueva fila",
            remove: "Quitar fila",
            moveUp: "Subir",
            moveDown: "Bajar",
            rowEmpty: "Sin datos por el momento",
        },
        WaitResponse: "Espere por favor",
        Generate: "Generando reporte",
        Buttons: [
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
                message: 'REPORTE DE CITAS',
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
                message: 'REPORTE DE CITAS',
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
        Close: "Cerrar",
        of: "de",
        AppointmentStatus: ['', 'Agendada', 'Cancelada', 'Completada', 'Confirmada'],
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'],
        buttonText: {
            today: 'Hoy',
            month: 'Mes',
            week: 'Semana',
            day: 'Día'
        },
        CurrentStep: "Paso actual",
        Pagination: "Paginación",
        Finish: "Terminar",
        Next: "Siguiente",
        Previous: "Anterior",
        Loading: "Cargando",
        Submit: "Enviar",
        canceled: "Cancelado",
        Ok: "Listo!",
        NoChanges: "No hemos hecho ninguna acción",
        Sure: "¿Estás seguro?",
        WillReagendado: "será reagendado el",
        OkResponse: "Todo se ha procesado correctamente",
        BadResponse: "Algo salió mal, por favor intenta de nuevo",
        MissingFields: "No has llenado los campos requeridos.",
        Details: "Ver Detalles",
        Delete: "Eliminar",
        ReSchedule: "Reagendar",
        WDYWTD: "¿Qué desea hacer?",
        Cancel: "Cancelar",
        Accept: "Aceptar",
        Reason: "Motivo",
        PutReason: "Ingresa un motivo",
        Yes: "Si",
        NoComments: "No tiene observaciones",
        Newdate: "Elige nueva fecha",
        FieldRequired: "Este campo es requerido",
        Unrevserible: "Esta acción no se podrá deshacer",
        Add: "Agregar",
        Remove: "Eliminar",
        Last30Days: "Citas en los últimos 30 días",
        Completed: "Completadas",
        Unattended: "Sin atender",
        Canceled: "Canceladas",
        Quantity: "Cantidad",
        Percentage_citas: "Porcentaje por estado de citas",
        Month_revenues: "Ganancia por mes",
        Last_SixMonths: "Estadísticas de los últimos 6 meses",
        All_users: "Todos los usuarios",
        Monday: "Lunes",
        Tuesday: "Martes",
        Wednesday: "Miércoles",
        Thursday: "Jueves",
        Friday: "Viernes",
        Saturday: "Sábado",
        Sunday: "Domingo",
        NoPatientSelected: "Ningún paciente seleccionado",
        NoPatientsInTheSystem: "No hay pacientes en el sistema",
        SelectedDate: "Fecha Seleccionada:",
        NewAppointment: "Nueva cita",
        Loading: "Cargando",
        SomethingWrong: "Algo anda mal",
        Processing: "Procesando",
        NextMonth: 'Mes siguiente',
        PreviousMonth: 'Mes anterior',
        SelectMonth: 'Seleccione un mes',
        SelectYear: 'Seleccione un año',
        Today: 'Hoy',
        Clear: 'Limpiar',
        Close: 'Cerrar',
        DistributionByDay: 'Distribución por día',
        StarToWrite:'Empieza a escribir..',
        DropFilesHereToUpload:'Arrastra los archivos aquí para subirlos'
    },
    en: {
        DTEditor: {
            "create": {
                "button": "New",
                "title": "Create new entry",
                "submit": "Create"
            },

            "edit": {
                "button": "Edit",
                "title": "Edit entry",
                "submit": "Update"
            },

            "remove": {
                "button": "Delete",
                "title": "Delete",
                "submit": "Delete",
                "confirm": {
                    "_": "Are you sure you wish to delete %d rows?",
                    "1": "Are you sure you wish to delete 1 row?"
                }
            },

            "error": {
                "system": "A system error has occurred (More information)"
            },

            "multi": {
                "title": "Multiple values",
                "info": "The selected items contain different values for this input. To edit and set all items for this input to the same value, click or tap here, otherwise they will retain their individual values.",
                "restore": "Undo changes",
                "noMulti": "This input can be edited individually, but not part of a group."
            },

            "datetime": {
                "previous": 'Previous',
                "next": 'Next',
                "months": ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
                "weekdays": ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
                "amPm": ['am', 'pm'],
                "unknown": '-'
            }
        },
        DataTables: {
            "sEmptyTable": "No data available in table",
            "sInfo": "Showing _START_ to _END_ of _TOTAL_ entries",
            "sInfoEmpty": "Showing 0 to 0 of 0 entries",
            "sInfoFiltered": "(filtered from _MAX_ total entries)",
            "sInfoPostFix": "",
            "sInfoThousands": ",",
            "sLengthMenu": "Show _MENU_ entries",
            "sLoadingRecords": "Loading...",
            "sProcessing": "Processing...",
            "sSearch": "Search:",
            "sZeroRecords": "No matching records found",
            "oPaginate": {
                "sFirst": "First",
                "sLast": "Last",
                "sNext": "Next",
                "sPrevious": "Previous"
            },
            "oAria": {
                "sSortAscending": ": activate to sort column ascending",
                "sSortDescending": ": activate to sort column descending"
            }
        },
        AppendGrid: {
            append: "Add new row",
            removeLast: "Caution! Remove the last row!",
            insert: "Insert new row",
            remove: "Remove row",
            moveUp: "Up",
            moveDown: "Down",
            rowEmpty: "No data at the moment",
        },
        WaitResponse: "Please wait",
        Generate: "Generating report",
        Buttons:
        [
            {
                extend: 'copy',
                text: 'Copy',
                exportOptions: {
                    columns: ':visible'
                }
            },
            {
                title: 'Smart Appointment System',
                extend: 'csv',
                text: 'Csv',
                exportOptions: {
                    columns: ':visible'
                }
            }
            ,
            {
                title: 'Smart Appointment System',
                extend: 'excel',
                text: 'Excel',
                exportOptions: {
                    columns: ':visible'
                }
            }
            ,
            {
                extend: 'pdfHtml5',
                message: 'APPOINTMENT REPORT',
                text: 'PDF',
                title: 'Smart Appointment System',
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
                message: 'APPOINTMENT REPORT',
                title: 'Smart Appointment System',
                text: 'Print',
                exportOptions: {
                    columns: ':visible'
                },

            },
            {
                extend: 'colvis',
                text: 'Select',

            }
        ],
        Close: "Close",
        of: "of",
        AppointmentStatus: ['', 'Scheduled', 'Canceled', 'Completed', 'Confirmed'],
        monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
        monthNamesShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        dayNames: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
        dayNamesShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
        buttonText: {
            today: 'Today',
            month: 'Month',
            week: 'Week',
            day: 'Day'
        },
        CurrentStep: "Current step",
        Pagination: "Pagination",
        Finish: "Finish",
        Next: "Next",
        Previous: "Previous",
        Loading: "Loading",
        Submit: "Submit",
        canceled: "Cancelled",
        Ok: "Success!",
        NoChanges: "No changes",
        Sure: "Are you sure?",
        WillReagendado: "will be rescheduled on",
        OkResponse: "all went well",
        BadResponse: "Something went wrong, please try again",
        MissingFields: "Please fill all the required fields",
        Details: "View Details",
        Delete: "Delete",
        ReSchedule: "Reschedule",
        WDYWTD: "What do you want to do?",
        Cancel: "Cancel",
        Accept: "Ok",
        Reason: "Reason",
        PutReason: "Put a reason",
        Yes: "Yes",
        NoComments: "No Comments",
        Newdate: "Choose a new date",
        FieldRequired: "This field is required",
        Unrevserible: "This action can't be reverted",
        Add: "Add",
        Remove: "Remove",
        Completed: "Completed",
        Unattended: "Unattended",
        Canceled: "Canceled",
        Quantity: "Quantity",
        Percentage_citas: "Percentage by appointment status",
        Month_revenues: "Monthly revenue",
        Last_SixMonths: "Statistics of the last 6 months",
        All_users: "All users",
        Monday: "Monday",
        Tuesday: "Tuesday",
        Wednesday: "Wednesday",
        Thursday: "Thursday",
        Friday: "Friday",
        Saturday: "Saturday",
        Sunday: "Sunday",
        NoPatientSelected: "No patient selected",
        NoPatientsInTheSystem: "There are no patients in the system",
        SelectedDate: "Selected Date:",
        NewAppointment: "New Appointment",
        Loading: "Loading",
        SomethingWrong: "Something wrong",
        Processing: "Processing",
        NextMonth: 'Next month',
        PreviousMonth: 'Previous month',
        SelectMonth: 'Select a month',
        SelectYear: 'Select a year',
        Today: 'Today',
        Clear: 'Clear',
        Close: 'Close',
        DistributionByDay: 'Distribution by day',
        StarToWrite: 'Start writing..',
        DropFilesHereToUpload: 'Drop Files Here To Upload'
    }
};