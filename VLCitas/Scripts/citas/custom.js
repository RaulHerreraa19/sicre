$(document).ready(function () {


    var dt = new Date();
    var today = dt.getFullYear() + "-" + (dt.getMonth() + 1) + "-" + dt.getDate();
    var locations = @Html.Raw(Json.Encode(offices));

    $('#calendar').fullCalendar({
        //delete
        eventRender: function (event, element, view) {
            element.append("<span class='closeon'>X</span>");
        },
        eventClick: function (calEvent, jsEvent, view) {
            swal({
                title: '¿Que desea realizar?',
                type: 'warning',
                html: "" +
                    "<br>" +
                    '<button type="button" role="button" tabindex="0" class="SwalBtn1 customSwalBtn1">' + 'Completar' + '</button>' +
                    '<button type="button" role="button" tabindex="0" class="SwalBtn2 swal-btn-red">' + 'Eliminar' + '</button>' +
                    '<button type="button" role="button" tabindex="0" class="SwalBtn3 customSwalBtn3">' + 'Cancelar' + '</button>',
                showCancelButton: false,
                showConfirmButton: false
            });
            $(document).on('click', '.SwalBtn1', function() {
                //Some code 1
                console.log('Completar cita');
                swal({
                    title: 'Ingresa el costo de la cita',
                    input: 'number',
                    showCancelButton: true,
                    cancelButtonText: 'Cancelar',
                    confirmButtonText: 'Aceptar',
                    showLoaderOnConfirm: true,
                    preConfirm: (text) => {
                        return new Promise((resolve) => {
                            setTimeout(() => {
                                if (text === '') {
                                    swal.showValidationError(
                                      'Ingresa un costo'
                                    )
                                }
                                resolve()
                            }, 2000)
                        })
                    },
                    allowOutsideClick: true
                }).then((result) => {
                    if (result.value) {
                        $.ajax({
                            type: "Post",
                            datatype: "html",
                            url: "/Citas/CompleteCita",
                            data: {
                                uId: calEvent.id,
                                precio: result.value
                            },
                            cache: false,
                            success: function (Result) {
                                if (Result["TypeOfResponse"] == 0) {
                                    console.log(calEvent.id);
                                    $('#calendar').fullCalendar('removeEvents', calEvent.id);
                                    swal("Bien!", Result["Message"] , "success")
                                }
                                if (Result["TypeOfResponse"] == 1) {
                                    swal("Oops..!", Result["Message"], "error")
                                }
                            },
                            error: function (request, status, error) {
                                console.error(request.responseText);
                                alert(request.responseText);
                            },
                            complete: function (xhr, status) {
                                //show_Status_Modal_close_(null);
                            }
                        });
                    }
                })
            });
            $(document).on('click', '.SwalBtn2', function() {
                //Some code 2
                console.log('Eliminar');
                swal({
                    title: 'Razón o motivo',
                    input: 'text',
                    showCancelButton: true,
                    cancelButtonText: 'Cancelar',
                    confirmButtonText: 'Aceptar',
                    showLoaderOnConfirm: true,
                    preConfirm: (text) => {
                        return new Promise((resolve) => {
                            setTimeout(() => {
                                if (text === '') {
                                    swal.showValidationError(
                                      'Ingresa un motivo'
                                    )
                                }
                                resolve()
                            }, 2000)
                        })
                    },
                    allowOutsideClick: true
                }).then((result) => {
                    if (result.value) {
                        $.ajax({
                            type: "Post",
                            datatype: "html",
                            url: "/Citas/DeleteCita",
                            data: {
                                uId: calEvent.id,
                                razon: result.value
                            },
                            cache: false,
                            success: function (Result) {
                                if (Result["TypeOfResponse"] == 0) {
                                    console.log(calEvent.id);
                                    $('#calendar').fullCalendar('removeEvents', calEvent.id);
                                    swal("Bien!", Result["Message"] , "success")
                                }
                                if (Result["TypeOfResponse"] == 1) {
                                    swal("Oops..!", Result["Message"], "error")
                                }
                            },
                            error: function (request, status, error) {
                                console.error(request.responseText);
                                alert(request.responseText);
                            },
                            complete: function (xhr, status) {
                                //show_Status_Modal_close_(null);
                            }
                        });
                    }
                })
                    
            });
            $(document).on('click', '.SwalBtn3', function() {
                //Some code 2
                console.log('Cancelar');
                swal(
                     'Cancelado',
                     'No hemos hecho ninguna acción.',
                     'error'
                   )
            });
        },


        allDaySlot: false,
        minTime: "@ViewBag.start",
        maxTime: "@ViewBag.ended",
        slotDuration: '00:' + @ViewBag.duration + ':00',
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'],
        lang: 'es',
        defaultDate: today,
        defaultView: 'agendaWeek',
        editable: true,

        //START AGREGAR NUEVA CITA
        selectable: true,
        selectHelper: true,
        editable: true,
        select: function (start, end) {
            end = $.fullCalendar.moment(start);
            //alert(start.format());
            //alert(end.format());

            //prompt
            swal.setDefaults({
                input: 'text',
                confirmButtonText: 'Siguiente &rarr;',
                showCancelButton: true,
                cancelButtonText: 'Cancelar',
                progressSteps: ['1', '2']
            })

            var steps = [
              {
                  title: 'Ingresa el nombre',
                  text: 'Agendar nueva cita'
              },
              'Ingresa el telefono'
            ]

            swal.queue(steps).then((result) => {
                swal.resetDefaults()
                if (result.value[0] === "" || result.value[1] === "" ) {
                    swal(
                      'Error!',
                      'No has llenado los campos requeridos.',
                      'error'
                    )
                    return false;
                    // result.dismiss can be 'cancel', 'overlay',
                    // 'close', and 'timer'
                }
                if (result.value) {
                    //Ajax
                    console.log(JSON.stringify(result.value));
                    //////////ajax/////
                    console.log("start normal --> " + start);
                    console.log("start format --> " + start.format())
                    var data = {
                        departaments_uId:  "@ViewBag.uId",
                        telefono: result.value[1],
                        title: result.value[0],
                        cita_date: start.format(),
                        status_id: 1,
                        Code: 1
                    };

                    $.ajax({
                        type: "Post",
                        datatype: "html",
                        url: "/Citas/AddCita",
                        data: data,
                        cache: false,
                        success: function (Result) {
                            //debugger;
                            if (Result["TypeOfResponse"] == 0) {
                                //Crea el objeto
                                console.log(Result["Data"]);
                                end.add(@ViewBag.duration, 'minutes');

                                $('#calendar').fullCalendar('renderEvent',
                                {
                                    id: Result.Objeto.uId,
                                    title: result.value[0],
                                    start: start,
                                    end: end,
                                    allDay: false
                                },
                                    true // stick the event
                                );
                                //muestra mensaje
                                swal({
                                    type: 'success',
                                    title: 'La cita se ha hecho correctamente!',
                                    timer: 3000,
                                    html: 'Nombre: ' + Result.Objeto.title +'<br>' + 'Telefono: ' + Result.Objeto.telefono
                                })
                            }
                            if (Result["TypeOfResponse"] == 1) {
                                swal("Oops..!", Result["Message"], "error")
                            }
                        },
                        error: function (request, status, error) {
                            console.error(request.responseText);
                            alert(request.responseText);
                        },
                        complete: function (xhr, status) {
                            //show_Status_Modal_close_(null);
                        }
                    });
                }
            })
            //
            //end select
            $('#calendar').fullCalendar('unselect');
        },
        ////////end nueva cita///////////////


        //Events

        events: locations,


        //{
        //    id: 3,
        //    title: 'Repeating Event',
        //    start: '2017-12-09T16:00:00',
        //    end: '2017-12-09T16:30:00'
        //},
        //{
        //    id: 4,
        //    title: 'Repeating Event',
        //    start: '2017-12-09T17:00:00',
        //    end: '2017-12-09T17:30:00'
        //},
        //{
        //    id: 5,
        //    title: 'Repeating Event',
        //    start: '2017-12-09T18:00:00',
        //    end: '2017-12-09T18:30:00'
        //}

    });

});