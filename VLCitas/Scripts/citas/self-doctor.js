clControl = {
    doctor_uId: null,
    citas_calendar: [],
    user_rol: null,
    citas: [],
    businessHours: [],
    notWorkingDays: [],
    schedules: [],
    minTime: null,
    maxTime: null,
    slotDuration: null,
    consultory_uId: null,
    doctors: null,
    patients: null,
    patientForm: null,
    existStepping: false,
    selectPatient: null,
    tableAppointments: null,
    Initialize: function () {
        moment.locale(current_lang.LanguageCulture);
        $(".pad").hide();
        $("body").on("click", "#reload", clControl.Reload);
        $("body").on("click", "#btnReagenda", clControl.Reagenda);
        $("body").on("click", "#see-list", clControl.SeeTable);
        $("body").on("click", "#see-calendar", clControl.SeeCalendar);
        $("body").on("click", "#add-appointment", clControl.ShowFormToAddAnAppoinment);
        $("body").on("click", "#patient-new", clControl.EnableNewPatient);
        $("body").on("hidden.bs.modal", "#AddCita", clControl.CleanNewAppointmentModal);
        $("body").on("hidden.bs.modal", "#DetalleCita", clControl.CleanDetailsAppointmentModal);
        $("body").on("change", "#select_patient_id", clControl.OnChangePatient);
        $("body").on("submit", "#frm-patient", clControl.OnNewPatientSubmited);

        $("body").on("click", ".delete-appointment", clControl.DeleteAppointment);
        $("body").on("click", "#search-patient-btn", clControl.SearchPatientByParams);
        $("body").on("click", "#btn-confirm-appointment", clControl.ConfirmAppointment);
        $("body").on("click", ".doctor-view", clControl.ShowDoctorDetails);

        $("body").on("click", ".close-swal", clControl.CloseSwalModal);
        $(window).resize(clControl.DocumentResize);
        clControl.ShowCalendar();
        clControl.ShowTable();
    },
    CloseSwalModal: function () {
        swal.close();
    },
    DocumentResize: function () {
        var height = $(window).height(); // New height
        var width = $(window).width(); // New width
        //console.log("height", height, "width", width);
        if (width > 991) {
            if ($("#table-dr-schedule").hasClass("table-responsive"))
                $("#table-dr-schedule").removeClass("table-responsive");
            if ($("#table-dr-details-schedule").hasClass("table-responsive"))
                $("#table-dr-details-schedule").removeClass("table-responsive");
        } else {
            if ($("#table-dr-schedule").hasClass("table-responsive") == false)
                $("#table-dr-schedule").addClass("table-responsive");
            if ($("#table-dr-details-schedule").hasClass("table-responsive") == false)
                $("#table-dr-details-schedule").addClass("table-responsive");
        }
    },
    Reload: function () {
        $.ajax({
            type: "Post",
            datatype: "html",
            url: "/Citas/ReloadEvents",
            data: {
                uId: clControl.consultory_uId,
            },
            cache: false,
            success: function (Result) {
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
        $("#AddCita").modal("hide");
        ShowLoading(js_language[user_lang].Processing);
        $.ajax({
            type: "Post",
            datatype: "html",
            url: "/Citas/AddCita",
            data: {
                consultory_uId: clControl.consultory_uId,
                doctor_uId: clControl.doctor_uId,
                title: $("#patient-name-selected").val(),
                cita_date: $("#appointment-format").val(),
                status_id: 1,
                Code: 1,
                id_patient: $("#patient_id").val(),
                observacion_doctor: $("#appointment-observations").val()
            },
            cache: false,
            success: function (Result) {
                swal.close();
                if (Result["TypeOfResponse"] == 0) {
                    var cita = Result.Data;
                    clControl.citas_calendar.fullCalendar('renderEvent', cita, 'stick');
                    clControl.citas.push(cita);
                    var revenueToday = currencyFormatter.format(clControl.GetTotalRevenueToday());
                    $("#revenue-day").text(revenueToday);
                    clControl.tableAppointments.row.add([
                        cita.title,
                        moment(cita.start_date).format("DD/MM/YYYY hh:mm:ss a"),
                        currencyFormatter.format(cita.precio),
                        '<button data-appointment-id="' + cita.id + '" type="button" class="show-appointment-details btn btn-info"><i class="ft-eye"></i></button>'
                    ]).draw().nodes().to$()
                        .addClass('text-center');

                    swal(js_language[user_lang].Ok, js_language[user_lang].OkResponse, "success");
                } else {
                    swal("Oops..!", js_language[user_lang].BadResponse, "error")
                }
            },
            error: function (request, status, error) {
                console.error(request.responseText);
            }
        });
    },
    GetPacientesByDoctor: function () {
        $('#select_patient_id option').remove();
        $("#select-patient-div").block({
            message: '<div class="icon-spinner9 icon-spin icon-lg"></div>',
            overlayCSS: { backgroundColor: "#fff", opacity: .8, cursor: "wait" },
            css: { border: 0, padding: 0, backgroundColor: "transparent" }
        });
        $.ajax({
            type: "Post",
            datatype: "html",
            url: "/Citas/GetPacientes",
            data: {
                doctor_uId: clControl.doctor_uId
            },
            cache: false,
            success: function (Result) {
                clControl.patients = Result;
                $("#select-patient-div").unblock();
                if (clControl.selectPatient != null) {
                    $.each(clControl.selectPatient, function (i, item) {
                        item.selectize.destroy();
                    });
                    clControl.selectPatient = null;
                    $(".selectize-control").each(function (index) {
                        $(this).remove();
                    });
                }
                if (Result.length > 0) {
                    $("#select_patient_id").append('<option value="">' + js_language[user_lang].NoPatientSelected + '</option>');
                    $.each(Result, function (i, vl) {
                        $("#select_patient_id").append('<option value="' + vl['id'] + '">' + vl['nombre'] + '</option>');
                    });
                } else
                    $("#select_patient_id").append('<option value="">' + js_language[user_lang].NoPatientsInTheSystem + '</option>');

                clControl.selectPatient = $(".selectPatient").selectize({
                    sortField: 'text'
                });
            },
            error: function (request, status, error) {
                console.error(request.responseText);
            },
            complete: function (xhr, status) {
                //show_Status_Modal_close_(null);
            }
        });
    },
    OnChangePatient: function () {
        $("#patient-warning-module").css("display", "none");
        var patient_id = $("#select_patient_id").val();
        var patient = $.grep(clControl.patients, function (n, i) {
            return n.id == patient_id;
        });
        $("#patient-new").data("new", false);
        $('.patient-data').prop("disabled", true);
        $("#patient-name").val(patient[0].nombre);
        $("#patient-email").val(patient[0].email);
        $("#patient-phone").val(patient[0].telefono);
        $("#patient-curp").val(patient[0].curp);
        $("#patient-sex").val(patient[0].sex_id);
        var date = moment(patient[0].fecha_nacimiento).format("YYYY-MM-DD");
        $("#patient-birth").val(date);
        $("#patient_id").val(patient_id);
    },
    EnableNewPatient: function (event) {
        event.preventDefault();
        $(this).data("new", true);
        $(".patient-data").val("");
        $('.patient-data').prop("disabled", false);
        setTimeout(function () { $('.patient-data').prop("disabled", false); }, 100);
        clControl.selectPatient[0].selectize.setValue("", false);
    },
    OnNewPatientSubmited: function () {
        $.ajax({
            type: "Post",
            datatype: "html",
            url: "/Pacientes/Nuevo",
            data: {
                nombre: $("#patient-name").val(),
                telefono: $("#patient-phone").val(),
                fecha_nacimiento: $("#patient-birth").val(),
                email: $("#patient-email").val(),
                sex_id: $("#patient-sex").val(),
                curp: $("#patient-curp").val()
            },
            cache: false,
            success: function (Result) {
                console.log(Result);
                if (Result["TypeOfResponse"] == 0) {
                    $("#patient_id").val(Result.Data.id);
                    $("#patient-name-selected").val(Result.Data.nombre);
                } else {
                    swal("Oops..!", js_language[user_lang].BadResponse, "error");
                }
            },
            error: function (request, status, error) {
                console.error(request.responseText);
            }
        });
        return false;
    },
    Reagenda: function (event) {
        ShowLoading(js_language[user_lang].Processing);
        $.ajax({
            type: "Post",
            datatype: "html",
            url: "/Citas/ReAgenda",
            data: {
                uId: event.id,
                cita_date: event.start.format()
            },
            cache: false,
            success: function (Result) {
                swal.close();
                if (Result["TypeOfResponse"] == 0)
                    swal(js_language[user_lang].Ok, js_language[user_lang].OkResponse, "success");
                else
                    swal("Oops..!", js_language[user_lang].BadResponse, "error");
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
        clControl.businessHours = [];
        clControl.notWorkingDays = [];
        for (var i = 0; i < clControl.schedules.length; i++) {
            var dow_ = []; var days = [];
            if (clControl.schedules[i].domingo == true)
                dow_.push(0);
            else
                days.push(0);
            if (clControl.schedules[i].lunes == true)
                dow_.push(1);
            else
                days.push(1);
            if (clControl.schedules[i].martes == true)
                dow_.push(2);
            else
                days.push(2);
            if (clControl.schedules[i].miercoles == true)
                dow_.push(3);
            else
                days.push(3);
            if (clControl.schedules[i].jueves == true)
                dow_.push(4);
            else
                days.push(4);
            if (clControl.schedules[i].viernes == true)
                dow_.push(5);
            else
                days.push(5);
            if (clControl.schedules[i].sabado == true)
                dow_.push(6);
            else
                days.push(6);
            clControl.businessHours.push({ dow: dow_, start: clControl.schedules[i].start_hour, end: clControl.schedules[i].end_hour });
            clControl.notWorkingDays.push({ days: days });
        }
        //console.log("businessHours:", clControl.businessHours, "default:", $.fullCalendar.moment(),"slot:", clControl.slotDuration);
        clControl.citas_calendar = $('#calendar').fullCalendar({
            eventRender: function (event, element, view) {
                element.attr('data-step', '2');
                element.attr('data-intro', 'Estas son tus citas, <br> para agregar una nueva solo da click en un espacio vacio <br> o para borrarla solo da click en una cita');
                //console.log(element);
            },
            contentHeight: "auto",
            eventColor: '#0174DF',
            eventOverlap: false,
            allDaySlot: false,
            businessHours: clControl.businessHours,
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
            defaultDate: $.fullCalendar.moment(),
            defaultView: 'agendaWeek',
            editable: true,
            selectable: true,
            selectHelper: true,
            editable: true,
            displayEventTime: false,
            events: clControl.citas,
            eventDrop: function (event, delta, revertFunc) {
                var the_event = event;
                //console.log(event.status_id);
                var now = new Date();
                var cita_date = new Date(event.start.format());
                if (event.status_id != 3 && now <= cita_date) {
                    swal({
                        title: js_language[user_lang].Sure,
                        text: event.title + js_language[user_lang].WillReagendado + event.start.format().replace("T", " "),
                        type: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: js_language[user_lang].Yes,
                        cancelButtonText: "No",
                    }).then((result) => {
                        if (result.value) {
                            console.log("event: ", the_event);
                            clControl.Reagenda(the_event);
                        } else {
                            revertFunc();
                        }
                    });
                }
                else {
                    revertFunc();
                }
            },
            select: function (start, end) {
                var now = new Date();
                var cita_date = new Date(start.format());
                if (now <= cita_date) {
                    end = $.fullCalendar.moment(start);
                    var duration = ConvertTimeSpanToMinutes(clControl.slotDuration);
                    $("#appointment-start").val(start);
                    start = $("#appointment-start").val().replace('GMT+0000', '');
                    $("#appointment-start").val(start);
                    $("#appointment-end").val(end.add(duration, 'minutes'));
                    $("#appointment-format").val(start.format());
                    clControl.ShowFormToAddAnAppoinment();
                }
                $('#calendar').fullCalendar('unselect');
            }
        });
    },
    GoToCustomStep: function (step) {
        var currentStep = $(".add-appointment-steps").steps("getCurrentIndex");
        var currentStep2 = $(".add-appointment-steps").steps("getCurrentIndex");
        if (currentStep < step) {
            while (currentStep !== step) {
                $(".add-appointment-steps").steps("next");
                currentStep++;
            }
        } else if (currentStep > step) {
            while (currentStep !== step) {
                console.log("GoToCustomStep currentStep:", currentStep);
                $(".add-appointment-steps").steps("previous");
                currentStep--;
            }
        }
        return currentStep2;
    },
    ResetStepping: function () {
        //debugger;
        if (clControl.existStepping)
            $(".add-appointment-steps").steps('destroy');
        clControl.SetSteppingForm();
    },
    SetSteppingForm: function () {
        $(".add-appointment-steps").steps({
            headerTag: "h6",
            bodyTag: "fieldset",
            transitionEffect: "fade",
            titleTemplate: '<span class="step" id="appointment-step-#index#">#title#</span> #title#',
            labels: {
                current: js_language[user_lang].CurrentStep,
                pagination: js_language[user_lang].Pagination,
                finish: js_language[user_lang].Submit,
                next: js_language[user_lang].Next,
                previous: js_language[user_lang].Previous,
                loading: js_language[user_lang].Loading
            },
            onStepChanging: function (event, currentIndex, newIndex) {
                var moveOn = true;
                if (newIndex > currentIndex) {
                    switch (currentIndex) {
                        case 0: //calendar
                            if (isNullOrEmpty($("#appointment-start").val())) {
                                $("#calendar-warning-module").css("display", "block");
                                moveOn = false;
                            } else {
                                var date = $("#appointment-start").val();
                                $("#dr-selected-time2").val(moment(date).format('LLLL'));
                            }
                            break;
                        case 1:  //patient
                            if (isNullOrEmpty($("#select_patient_id").val())) {
                                var isNew = $("#patient-new").data("new");
                                if (isNew) {
                                    $("#frm-patient").submit();
                                    if (!clControl.patientForm.valid())
                                        moveOn = false;
                                } else {
                                    $("#patient-warning-module").css("display", "block");
                                    moveOn = false;
                                }
                            } else {
                                $("#patient_id").val($("#select_patient_id").val());
                                $("#patient-name-selected").val($("#select_patient_id").text().trim());
                            }
                            break;
                        default:
                            break;
                    }
                } else {
                    switch (currentIndex) {
                        case 0:
                            $("#calendar-warning-module").css("display", "none");
                            $("#calendar-info-module").css("display", "none");
                            break;
                        case 1:
                            $("#patient-warning-module").css("display", "none");
                            break;
                        default:
                            break;
                    }
                }

                return moveOn;
            },
            onFinishing: function (event, currentIndex) {
                var moveOn = true;
                return moveOn;
            },
            onFinished: function (event, currentIndex) {
                clControl.AddCita();
            }
        });
        $(".step").each(function (index) {
            var iElement = $(this).html();
            var e = iElement.split("</i>");
            $(this).html(e[0] + "</i>");
            $(this).next().remove();
        });
        clControl.existStepping = true;
    },
    CleanNewAppointmentModal: function (event) {
        $("#appointment-start").val("");
        $("#appointment-end").val("");
        $("#appointment-format").val("");
        $("#patient_id").val("");
        clControl.patientForm.destroy();
        clControl.newAppointment = null;
        $('#calendar-choose-appointment').fullCalendar('destroy');
    },
    ShowFormToAddAnAppoinment: function () {
        clControl.ResetStepping();
        clControl.GetPacientesByDoctor();
        var start = $("#appointment-start").val();
        if (isNullOrEmpty(start)) {
            $("#show-date-selected").css("display", "none");
            $("#calendar-choose-appointment").css("display", "block");
        } else {
            $("#show-date-selected").css("display", "block");
            $("#calendar-choose-appointment").css("display", "none");
            $("#dr-selected-time").val(moment(start).format('LLLL'));
        }
        $("#calendar-warning-module").css("display", "none");
        $("#calendar-info-module").css("display", "none");
        $("#patient-warning-module").css("display", "none");
        clControl.SetDateTimePicker();
        clControl.patientForm = $("#frm-patient").validate();
        $('#AddCita').modal('show');
        if (isNullOrEmpty(start) == false) 
            $(".add-appointment-steps").steps("next");

    },
    SetDateTimePicker: function () {
        var staticAppointments = [];
        for (i = 0; i < clControl.citas.length; i++) {
            staticAppointments.push(clControl.citas[i]);
            staticAppointments[i].color = "#535d63"
            staticAppointments[i].url = ""
        }
        $('#calendar-choose-appointment').fullCalendar({
            eventRender: function (event, element, view) { },
            contentHeight: "auto",
            eventColor: '#0174DF',
            eventOverlap: false,
            allDaySlot: false,
            businessHours: clControl.businessHours,
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
            defaultDate: $.fullCalendar.moment(),
            defaultView: 'agendaWeek',
            editable: true,
            selectable: true,
            selectHelper: true,
            editable: true,
            displayEventTime: false,
            events: staticAppointments,
            eventClick: function (calEvent, jsEvent, view) { return false; },
            eventDrop: function (event, delta, revertFunc) { },
            select: function (start, end) {
                var now = new Date();
                var cita_date = new Date(start.format());
                if (now <= cita_date) {
                    $("#calendar-choose-appointment").fullCalendar('removeEvents', 0);
                    var start_date = start;
                    end = $.fullCalendar.moment(start);
                    var duration = ConvertTimeSpanToMinutes(clControl.slotDuration);
                    $("#appointment-start").val(start);
                    start = $("#appointment-start").val().replace('GMT+0000', '');
                    $("#appointment-start").val(start);
                    $("#appointment-end").val(end.add(duration, 'minutes'));
                    $("#appointment-format").val(start.format());
                    $("#calendar-warning-module").css("display", "none");
                    $("#calendar-info-module").css("display", "block");
                    $("#calendar-info-message").html(js_language[user_lang].SelectedDate + " " + moment(start).format('LLLL'));
                    $('#calendar-choose-appointment').fullCalendar('unselect');
                    var newAppointment = { id: 0, title: js_language[user_lang].NewAppointment, start: start_date, end: end, color: "#0174DF", durationEditable: false };
                    $('#calendar-choose-appointment').fullCalendar('renderEvent', newAppointment, 'stick');
                }
            }
        });
    },
    GetNotWorkingDays: function () {
        var data = null;
        $.each(clControl.notWorkingDays, function (i, item) {
            if (data == null)
                data = item.days;
            else if (JSON.stringify(data) != JSON.stringify(item.days)) {
                for (var i = 0; i < item.days.length; i++) {
                    if (data.includes(item.days[i]) == false)
                        data.push(item.days[i]);
                }
            }
        });
        return data;
    },
    GetWorkingHoursTableByDay: function (dayNumber, startHour) {
        var duration = ConvertTimeSpanToMinutes(clControl.slotDuration);
        var appoinments = [];
        $.each(clControl.businessHours, function (i, item) {
            if (item.dow.includes(dayNumber) == true) {
                var currentHour = item.start;
                var ban = true;
                while (ban) {
                    if (currentHour.Hours >= startHour)
                        appoinments.push({ Hours: currentHour.Hours, Minutes: currentHour.Minutes });
                    currentHour.Minutes += duration;
                    if (currentHour.Minutes >= 60) {
                        currentHour.Minutes -= 60;
                        currentHour.Hours += 1;
                    }
                    if (currentHour.Hours >= item.end.Hours)
                        ban = false;
                }
            }
        });
        var html = "";
        $.each(appoinments, function (i, item) {
            html += '<tr>' +
                '<td class="Hora">' + item.Hours + ':' + item.Minutes + '</td >' +
                '<td class="edit"><button class="btn mr-1 mb-1 btn-success btn-sm">Choose</button></td>' +
                '</tr>';
        });
        return html;
    },
    GetWorkingHoursByDay: function (dayNumber, startHour) {
        startHour = isNullOrEmpty(startHour) ? 0 : startHour;
        var data = [];
        $.each(clControl.businessHours, function (i, item) {
            if (item.dow.includes(dayNumber) == true) {
                for (var i = item.start.Hours; i <= item.end.Hours; i++) {
                    if (i >= startHour)
                        data.push(i);
                }
            }
        });
        return data;
    },
    DeleteAppointment: function () {
        var uId = $(this).attr("data-uId");
        $('#DetalleCita').modal('hide');
        swal({
            title: js_language[user_lang].Reason,
            input: 'text',
            showCancelButton: true,
            cancelButtonText: js_language[user_lang].Cancel,
            confirmButtonText: js_language[user_lang].Accept,
            showLoaderOnConfirm: true,
            preConfirm: (text) => {
                return new Promise((resolve) => {
                    setTimeout(() => {
                        if (text === '')
                            swal.showValidationError(js_language[user_lang].PutReason);
                        resolve();
                    }, 2000)
                })
            },
            allowOutsideClick: true
        }).then((result) => {
            if (result.value) {
                ShowLoading(js_language[user_lang].Processing);
                $.ajax({
                    type: "Post",
                    datatype: "html",
                    url: "/Citas/Cancel",
                    data: {
                        uId: uId,
                        razon: result.value
                    },
                    cache: false,
                    success: function (Result) {
                        swal.close();
                        if (Result["TypeOfResponse"] == 0) {
                            $('#calendar').fullCalendar('removeEvents', uId);
                            clControl.tableAppointments.row($("#" + uId)).remove().draw();
                            clControl.citas = clControl.citas.filter(function (obj) { return obj.id !== uId; });
                            var revenueToday = currencyFormatter.format(clControl.GetTotalRevenueToday());
                            $("#revenue-day").text(revenueToday);

                            swal({
                                title: js_language[user_lang].Ok,
                                text: js_language[user_lang].OkResponse,
                                type: "success",
                                timer: 2000
                            });
                        } else {
                            swal("Oops..!", js_language[user_lang].BadResponse, "error")
                        }
                    },
                    error: function (request, status, error) {
                        console.error(request.responseText);
                    },
                    complete: function (xhr, status) { }
                });
            }
        })
    },
    GetTotalRevenueToday() {
        var today = moment().startOf('day');
        var tomorrow = moment().startOf('day').add(1, 'days');

        var citasToday = clControl.citas.filter(function (obj) {
            var appointmentDate = moment(obj.start_date);
            return appointmentDate >= today && appointmentDate <= tomorrow;
        });

        var sum = citasToday.reduce(function (s, a) {
            return s + a.precio;
        }, 0);

        return sum == null ? 0 : sum;
    },
    ShowTable: function () {
        clControl.tableAppointments = $("#table-appointments").DataTable({
            //dom: "Bfrtip",
            responsive: true,
            language: js_language[user_lang].DataTables,
            orderCellsTop: true,
            fixedHeader: true
        });
        $("#see-calendar").css("display", "none");
        $("#see-list").css("display", "block");
    },
    SeeCalendar: function () {
        $("#appointment-list").css("display", "none");
        $("#calendar").css("display", "block");
        $("#see-calendar").css("display", "none");
        $("#see-list").css("display", "block");
        $("#title-calendar").css("display", "block");
        $("#add-appointment").css("display", "none");
    },
    SeeTable: function () {
        $("#calendar").css("display", "none");
        $("#appointment-list").css("display", "block");
        $("#see-list").css("display", "none");
        $("#see-calendar").css("display", "block");
        $("#title-calendar").css("display", "none");
        $("#add-appointment").css("display", "block");
    },
    CleanDetailsAppointmentModal: function () {
        $(".details-appointment").val("");
    },
    SearchPatientByParams: function () {
        var data = $("#search-patient").val();

        $("#select-patient-div").block({
            message: '<div class="icon-spinner9 icon-spin icon-lg"></div>',
            overlayCSS: { backgroundColor: "#fff", opacity: .8, cursor: "wait" },
            css: { border: 0, padding: 0, backgroundColor: "transparent" }
        });
        $.ajax({
            type: "Post",
            datatype: "html",
            url: "/Citas/SearchPatients",
            data: {
                data: data
            },
            cache: false,
            success: function (Result) {
                clControl.patients = Result;
                $("#select-patient-div").unblock();
                if (clControl.selectPatient != null) {
                    $.each(clControl.selectPatient, function (i, item) {
                        item.selectize.destroy();
                    });
                    clControl.selectPatient = null;
                    $(".selectize-control").remove();
                }
                $('#select_patient_id option').remove();
                if (clControl.patients.length > 0) {
                    $("#select_patient_id").append('<option value="">' + js_language[user_lang].NoPatientSelected + '</option>');
                    $.each(clControl.patients, function (i, patient) {
                        $("#select_patient_id").append('<option value="' + patient['id'] + '">' + patient['nombre'] + '</option>');
                    });
                } else
                    $("#select_patient_id").append('<option value="">' + js_language[user_lang].NoPatientsInTheSystem + '</option>');

                clControl.selectPatient = $(".selectPatient").selectize({
                    sortField: 'text'
                });
            },
            error: function (request, status, error) {
                console.error(request.responseText);
            },
            complete: function (xhr, status) {
                //show_Status_Modal_close_(null);
            }
        });
    },
    ConfirmAppointment: function () {
        $('#DetalleCita').modal('hide');
        var uId = $("#btn-confirm-appointment").attr("data-uId");
        ShowLoading(js_language[user_lang].Processing);
        $.ajax({
            type: "Post",
            datatype: "html",
            url: "/Citas/Confirm",
            data: {
                uId: uId
            },
            cache: false,
            success: function (Result) {
                swal.close();
                if (Result["TypeOfResponse"] == 0) {
                    swal(js_language[user_lang].Ok, js_language[user_lang].OkResponse, "success");
                    clControl.citas_calendar.fullCalendar('removeEvents', uId);
                    clControl.citas_calendar.fullCalendar('renderEvent', Result.Data);
                    var badgeId = "#badge-status-" + Result.Data.id;
                    $(badgeId).removeClass("badge-info");
                    $(badgeId).removeClass("badge-warning");
                    $(badgeId).addClass("badge-success");
                    $(badgeId).text(js_language[user_lang].AppointmentStatus[Result.Data.status_id]);
                } else
                    swal("Oops..!", js_language[user_lang].BadResponse, "error")
            },
            error: function (request, status, error) {
                console.error(request.responseText);
                //alert(request.responseText);
            },
            complete: function (xhr, status) {
                //show_Status_Modal_close_(null);
            }
        });
    },
    ShowDoctorDetails: function () {
        var doctor_uId = $(this).data("id");
        var data = clControl.GetDoctorScheduleTable(doctor_uId);
        $("#dr-details-name").val(data.doctor.value1);
        $("#dr-details-speciality").val(data.doctor.value3);
        $("#tbody-dr-details-schedule").html(data.html);
        clControl.DocumentResize();
        var today = moment().startOf('day');
        var tomorrow = moment().startOf('day').add(1, 'days');
        var citas = clControl.citas.filter(function (obj) {
            var appointmentDate = moment(obj.start_date);
            return obj.doctor_uid == doctor_uId && (appointmentDate >= today && appointmentDate <= tomorrow);
        });

        var html = "";
        $("#tbody-dr-appointments").html(html);
        $.each(citas, function (i, cita) {
            var btn = "";
            switch (cita.status_id) {
                case 1:
                    btn = "btn-info";
                    break;
                case 2:
                    btn = "btn-danger";
                    break;
                case 3:
                    btn = "btn-blue-gray";
                    break;
                case 4:
                    btn = "btn-success";
                    break;
                case 5:
                    btn = "btn-warning";
                    break;
                default:
                    break;
            }

            html += '<tr>' +
                '<td class="">' + cita.title + '</td >' +
                '<td class="">' + moment(cita.start_date).format("DD/MM/YYYY hh:mm:ss a") + '</td>' +
                '<td class="">' + currencyFormatter.format(cita.precio) + '</td>' +
                '<td class=""><a href="' + cita.url + '" class="btn ' + btn + '"><i class="ft-eye"></i></button></td>' +
                '</tr>';
        });
        $("#tbody-dr-appointments").html(html);
        $("#DoctorDetails").modal('show');
    }
};
