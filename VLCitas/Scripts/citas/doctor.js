clControl = {
    doctor_uId: "",
    citas_calendar: null,
    user_rol :null,
    citas: [],
    schedules: [],
    minTime: null,
    maxTime : null,
    slotDuration : null,
    Initialize: function () {
        moment.locale(current_lang.LanguageCulture);
        $(".pad").hide();
        $("body").on("click", "#reload", clControl.Reload);
        $("body").on("click", "#btnAddCita", clControl.AddCita);
        $("body").on("click", "#btnSearch", clControl.SearchPatient);
        $("body").on("click", "#btnReagenda", clControl.Reagenda);
        $("body").on("click", "#see-list", clControl.SeeTable);
        $("body").on("click", "#see-calendar", clControl.SeeCalendar);
        $("body").on("click", ".show-appointment-details", clControl.SeeApoinmentDetails);
        clControl.ShowCalendar();
        clControl.ShowTable();
    },
    Reload: function () {
        $.ajax({
            type: "Post",
            datatype: "html",
            url: "/Doctor/ReloadEvents",
            data: {
                uId: clControl.doctor_uId,
            },
            cache: false,
            success: function (Result) {
                console.log(Result);
                //debugger;
                console.log("Recargando eventos");
                $('#calendar').fullCalendar('removeEvents');
                $('#calendar').fullCalendar('addEventSource', Result);
                $('#calendar').fullCalendar('rerenderEvents');

            },
            error: function (request, status, error) {
                console.error(request.responseText);
                alert(request.responseText);
            },
            complete: function (xhr, status) {
                //show_Status_Modal_close_(null);
            }
        });
    },
    AddCita: function () {
        if ($("#pa_select").val() === null) {
            //Si no entonces va registrar
            if ($("#pa_nombre").val() != '' && $("#pa_apellidos").val() != '') {
                debugger;
                console.log("case 2, si puso datos y va registrar");
                var data = {
                    departaments_uId: uid,
                    title: $("#pa_nombre").val() + " " + $("#pa_apellidos").val(), //$("#pa_titulo").val(),
                    cita_date: $("#format").val(),
                    status_id: 1,
                    Code: 1,
                    nombre: $("#pa_nombre").val(),
                    apellidos: $("#pa_apellidos").val(),
                    telefono_paciente: $("#pa_telefono").val(),
                    curp: $("#pa_curp").val()
                };
                $.ajax({
                    type: "Post",
                    datatype: "html",
                    url: "/Citas/AddCitaRegister",
                    data: data,
                    cache: false,
                    success: function (Result) {
                        //debugger;
                        if (Result["TypeOfResponse"] == 0) {
                            //muestra mensaje
                            $('#AddCita').modal('hide');
                            swal({
                                title: "Listo!",
                                text: "La cita se ha guardado correctamente",
                                type: "success",
                                timer: 2000
                            });
                            console.log("Entro al success case 2");
                            clControl.citas_calendar.fullCalendar('renderEvent', Result.Objeto, 'stick');
                            //setTimeout(function(){// wait for 5 secs(2)
                            //    location.reload(true); // then reload the page.(3)
                            //}, 2000);
                        }
                        else {
                            swal("Oops..!", Result["Message"], "error")
                        }
                    },
                    error: function (request, status, error) {
                        console.error(request.responseText);

                    }
                });
            }
            else {
                swal(
                    'Error!',
                    'No has llenado los campos requeridos.',
                    'error'
                );
            }
        }
        else {
            var data = {
                departaments_uId: uid,
                title: $("#pa_select option:selected").text(), //$("#ci_titulo").val(),
                cita_date: $("#format").val(),
                status_id: 1,
                Code: 1,
                id_paciente: $("#pa_select").val()
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
                        //muestra mensaje
                        $('#AddCita').modal('hide');
                        swal({
                            title: "Listo!",
                            text: "La cita se ha guardado correctamente",
                            type: "success",
                            timer: 2000
                        });
                        console.log("Entro al success case 1");
                        console.log(Result.Objeto);
                        clControl.citas_calendar.fullCalendar('renderEvent', Result.Objeto, 'stick');
                    }
                    else {
                        swal("Oops..!", Result["Message"], "error")
                    }
                },
                error: function (request, status, error) {
                    console.error(request.responseText);
                }
            });
        }
    },
    SearchPatient: function () {
        //debugger;
        $('.pad').hide();
        $('.sele').show();
        $('select option').remove();
        $.ajax({
            type: "Post",
            datatype: "html",
            url: "/Citas/GetPacientes",
            data: {
                data: $("#paciente").val()
            },
            cache: false,
            success: function (Result) {
                $.each(Result, function (i, vl) {
                    $("#pa_select").append('<option value="' + vl['id'] + '">' +
                        vl['nombre'] + " " + vl['apellidos'] + '</option>');
                });
            },
            error: function (request, status, error) {
                console.error(request.responseText);
                alert(request.responseText);
            },
            complete: function (xhr, status) {
                //show_Status_Modal_close_(null);
            }
        });
    },
    Reagenda: function () {
        debugger;
        $.ajax({
            type: "Post",
            datatype: "html",
            url: "/Asistente/ReAgenda",
            data: {
                uId: $("#bookId").val(),
                departaments_uId: uid,
                day: $("#day").val(),
                hour: $("#hour").val()
            },
            cache: false,
            success: function (Result) {
                console.log(Result);
                if (Result["TypeOfResponse"] == 0) {
                    swal("Bien!", Result["Message"], "success")
                    setTimeout(function () {// wait for 5 secs(2)
                        location.reload(); // then reload the page.(3)
                    }, 5000);
                }
                else {
                    swal("Oops..!", Result["Message"], "error")
                    setTimeout(function () {// wait for 5 secs(2)
                        location.reload(); // then reload the page.(3)
                    }, 5000);
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
    },
    ShowCalendar: function () {
        var dias = [];
        for (var i = 0; i < clControl.schedules.length; i++) {
            var dow_ = [];
            if (clControl.schedules[i].domingo == true)
                dow_.push(0);
            if (clControl.schedules[i].lunes == true)
                dow_.push(1);
            if (clControl.schedules[i].martes == true)
                dow_.push(2);
            if (clControl.schedules[i].miercoles == true)
                dow_.push(3);
            if (clControl.schedules[i].jueves == true)
                dow_.push(4);
            if (clControl.schedules[i].viernes == true)
                dow_.push(5);
            if (clControl.schedules[i].sabado == true)
                dow_.push(6);
            var obj = { dow: dow_, start: clControl.schedules[i].start_hour, end: clControl.schedules[i].end_hour };
            dias.push(obj);
        }
        clControl.citas_calendar = $('#calendar').fullCalendar({
            eventRender: function (event, element, view) {
                element.attr('data-step', '2');
                element.attr('data-intro', 'Estas son tus citas, <br> para agregar una nueva solo da click en un espacio vacio <br> o para borrarla solo da click en una cita');
                //console.log(element);
            },
            eventColor: '#0174DF',
            contentHeight: "auto",
            eventOverlap: false,
            allDaySlot: false,
            businessHours: dias,
            selectConstraint: 'businessHours',
            minTime: clControl.minTime,
            maxTime: clControl.maxTime,
            slotDuration: clControl.slotDuration,
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            buttonText: js_language[user_lang].buttonText,
            monthNames: js_language[user_lang].monthNames,
            monthNamesShort: js_language[user_lang].monthNamesShort,
            dayNames: js_language[user_lang].dayNames,
            dayNamesShort: js_language[user_lang].dayNamesShort,
            //lang: 'en',
            defaultDate: $.fullCalendar.moment(),
            defaultView: 'agendaWeek',
            editable: false,
            selectable: true,
            selectHelper: false,
            events: clControl.citas,
            displayEventTime: false
        });
    },
    ShowTable: function () {
        $("#table-appointments").dataTable({
            //dom: "Bfrtip",
            responsive: true,
            language: js_language[user_lang].DataTables,
            orderCellsTop: true,
            fixedHeader: true,
        });
        $("#see-calendar").css("display", "none");
        $("#see-list").css("display", "block");
    },
    SeeCalendar: function () {
        $("#appointment-list").css("display", "none");
        $("#calendar").css("display", "block");
        $("#see-calendar").css("display", "none");
        $("#see-list").css("display", "block");
    },
    SeeTable: function () {
        $("#calendar").css("display", "none");
        $("#appointment-list").css("display", "block");
        $("#see-list").css("display", "none");
        $("#see-calendar").css("display", "block");
    },
    SeeApoinmentDetails: function () {
        window.location = $(this).data("href");
    }
}
