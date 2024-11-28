clAppointment = {
    appointmentReceta: null,
    appointment_uId:null,
    id_patient:null,
    DoBPicker: null,
    available_dt: null,
    used_dt: null,
    editorFeatures: null,
    reasonEditor: null,
    diagnosisEditor: null,
    notesEditor: null,
    observationsEditor: null,
    Initialize: function () {
        moment.locale(current_lang.LanguageCulture);
        $("body").on("click", ".btn_add_servicio", clAppointment.AddServicio);
        $("body").on("click", ".btn_remove_servicio", clAppointment.RemoveServicio);
        $("body").on("click", "#btnSave", clAppointment.CompleteCita);
        $("body").on("click", ".patient-action", clAppointment.PatientFieldAction);
        $("body").on("click", "#patient-birthday", clAppointment.ShowDoBCalendar);
        $("body").on("click", "#edit-allergy", clAppointment.EditAllergies);
        $("body").on("click", "#save-allergy", clAppointment.SaveAllergies);
        $("body").on("click", "#edit-surgeries", clAppointment.EditSurgeries);
        $("body").on("click", "#save-surgeries", clAppointment.SaveSurgeries);
        $("body").on("click", "#SavePrescription", clAppointment.SavePrescription);
        clAppointment.GenerateDT();
        clAppointment.editorFeatures = {
            //debug: 'info',
            modules: {
                toolbar: [
                    ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
                    [{ 'align': [] }],
                    [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                    [{ 'indent': '-1' }, { 'indent': '+1' }],          // outdent/indent
                    [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
                    //["link", "image", /*"video",*/ "formula"],
                    //['clean']
                    //[{ 'font': [] }],
                    //[{ 'header': 1 }, { 'header': 2 }],               // custom button values
                    //[{ 'size': ['small', false, 'large', 'huge'] }],  // custom dropdown
                    //[{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme// remove formatting button
                    //['blockquote', 'code-block'],
                    //[{ 'script': 'sub' }, { 'script': 'super' }],      // superscript/subscript
                    //[{ 'direction': 'rtl' }],                         // text direction
                ]
            },
            placeholder: js_language[user_lang].StarToWrite,
            //readOnly: true,
            theme: 'snow'
        };
        clAppointment.reasonEditor = new Quill("#appointment-reason .editor", clAppointment.editorFeatures);
        clAppointment.notesEditor = new Quill("#appointment-observations .editor", clAppointment.editorFeatures);
        clAppointment.diagnosisEditor = new Quill("#appointment-diagnosis .editor", clAppointment.editorFeatures);
        clAppointment.observationsEditor = new Quill("#appointment-observations-to-assitant .editor", clAppointment.editorFeatures);
        clAppointment.checkDefaultMedicines();
    },
    GenerateDT: function () {
        //clAppointment.available_dt = $("#available_dt").DataTable({
        //    "paging": false,
        //    "ordering": false,
        //    "info": false,
        //    language: js_language[user_lang].DataTables
        //});
        //clAppointment.used_dt = $("#used_dt").DataTable({
        //    "paging": false,
        //    "ordering": false,
        //    "info": false,
        //    language: js_language[user_lang].DataTables
        //});
    },
    UpdatePrice: function (price, add) {
        debugger;
        var current = parseFloat($("#precio").val());
        price = parseFloat(price);
        if (add) {
            current = current + price;
        } else {
            current = current - price;
        }
        $("#precio").val(current);
    },
    checkDefaultMedicines: function () {
        $.each(medicines, function () {
            AddRowToReceta(this.id, this.sentence);
        });
    },
    AddMedicine: function () {
        var medicine = formToObject("addMedicineForm");
        $.ajax({
            type: "Post",
            datatype: "html",
            url: "/Citas/AddMedicine",
            data: {
                medicine
            },
            cache: false,
            success: function (Result) {
                if (Result["TypeOfResponse"] == 0) {
                    swal(js_language[user_lang].Ok, js_language[user_lang].OkResponse, "success")
                    AddRowToReceta(Result.id, Result.sentence);
                    $("#medicineModal").modal("hide");
                }
                if (Result["TypeOfResponse"] == 1) {
                    swal("Oops..!", js_language[user_lang].BadResponse, "error")
                }
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
    RemoveMedicine: function (id) {
        $.ajax({
            type: "Post",
            datatype: "html",
            url: "/Citas/RemoveMedicine",
            data: {
                "id": id
            },
            cache: false,
            success: function (Result) {
                if (Result["TypeOfResponse"] == 0) {
                    swal(js_language[user_lang].Ok, js_language[user_lang].OkResponse, "success")
                }
                if (Result["TypeOfResponse"] == 1) {
                    swal("Oops..!", js_language[user_lang].BadResponse, "error")
                }
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
    AddServicio: function () {
        var id = $(this).attr("data-id");
        var nombre = $(this).attr("data-nombre");
        var precio = $(this).attr("data-price");
        var parametro = {
            servicio: {
                cita: clAppointment.appointment_uId,
                servicio: id,
                add: true
            }
        };

        $.ajax({
            type: "POST",
            url: '/Servicios/SetServicioToCita',
            data: parametro,
            success: function (Res) {
                //clAppointment.available_dt.destroy();
                //clAppointment.used_dt.destroy();
                //$("#row_" + id).remove();
                var servicio = '<tr id="row_' + id + '">' +
                    '<td>' + nombre + '</td>' +
                    '<td><span class="btn btn_remove_servicio" data-id="' + id + '" data-price="' + precio +'" data-nombre="' + nombre + '">' + js_language[user_lang].Remove + ' <i class="icon-android-remove"></i> </span></td>' +
                    '</tr>';
                $("#used_body").append(servicio);
                clAppointment.UpdatePrice(precio, true);
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                console.log(err);
            },
            complete: function (xhr, status) {
                console.log("success");

            },
        });


    },
    RemoveServicio: function () {
        var id = $(this).attr("data-id");
        var nombre = $(this).attr("data-nombre");
        var precio = $(this).attr("data-price");
        var parametro = {
            servicio: {
                cita: clAppointment.appointment_uId,
                servicio: id,
                add: false
            }
        };
        $.ajax({
            type: "POST",
            url: '/Servicios/SetServicioToCita',
            data: parametro,
            success: function (Res) {
                //clAppointment.available_dt.destroy();
                //clAppointment.used_dt.destroy();
                $("#row_" + id).remove();
                clAppointment.UpdatePrice(precio, false);
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                console.log(err);
            },
            complete: function (xhr, status) {
                console.log("success");

            },
        });
    },
    CompleteCita: function () {
        var appointment = formToObject("frm_cita");
        appointment["motivo"] = htmlEscape(clAppointment.reasonEditor.container.firstChild.innerHTML);
        appointment["diagnostico"] = htmlEscape(clAppointment.diagnosisEditor.container.firstChild.innerHTML);
        appointment["observaciones"] = htmlEscape(clAppointment.notesEditor.container.firstChild.innerHTML);
        appointment["observaciones_asistente"] = htmlEscape(clAppointment.observationsEditor.container.firstChild.innerHTML);
        appointment["precio"] = $("#precio").val();
        var rec = $("#receta").val();
        var prescription = htmlEscape(rec);
        $.ajax({
            type: "Post",
            datatype: "html",
            url: "/Citas/CompleteCita",
            data: {
                appointment
            },
            cache: false,
            success: function (Result) {
                if (Result["TypeOfResponse"] == 0) {
                    swal(js_language[user_lang].Ok, js_language[user_lang].OkResponse, "success")
                    setTimeout(function () {
                        window.location.reload();
                    }, 3000)
                }
                if (Result["TypeOfResponse"] == 1) {
                    swal("Oops..!", js_language[user_lang].BadResponse, "error")
                }
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
    ShowDoBCalendar: function () {
        var action = $("#birthday-action").data("action");
        if (action == "save") 
            clAppointment.OpenBodCalendar();
    },
    OpenBodCalendar: function () {
        var DoB = $("#patient-birthday").val();
        var year, month, day;
        var date = moment().startOf('day');
        if (!isNullOrEmpty(DoB)) {
            var d = DoB.split('/');
            year = d[2];
            if (current_lang.LanguageCulture == "es") {
                month = d[1] - 1; day = d[0];
            }
            else {
                month = d[0] - 1; day = d[1];
            }
            //console.log("DoB", DoB,"date", d, "year",year,"month",month,"day",day);
        } else {
            year = date.year(); month = date.month(); day = date.date();
        }
        year = year < 1920 ? 1920 : year;
        year = year > date.year() ? date.year() : year;
        clAppointment.DoBPicker.set('select', [year, month, day]);
        clAppointment.DoBPicker.set('highlight', [year, month, day]);
        setTimeout(function () { clAppointment.DoBPicker.open(); }, 1);
    },
    PatientFieldAction: function () {
        var action = $(this).data("action");
        var field = $(this).data("field");
        if (action == "edit") {
            $(this).data("action", "save");
            switch (field) {
                case "birthday":
                    clAppointment.EditDoB();
                    break;
                case "allergy":
                    clAppointment.EditAllergies();
                    break;
                case "surgeries":
                    clAppointment.EditSurgeries();
                    break;
                default:
                    break;
            }
        } else {
            $(this).data("action", "edit");
            switch (field) {
                case "birthday":
                    clAppointment.SaveDoB();
                    break;
                case "allergy":
                    clAppointment.SaveAllergies();
                    break;
                case "surgeries":
                    clAppointment.SaveSurgeries();
                    break;
                default:
                    break;
            }
        }
    },
    EditDoB: function () {
        $("#edit-birthday").css("display", "none");
        $("#patient-birthday").prop("disabled", false);
        $("#save-birthday").css("display", "block");
        $("#patient-birthday").pickadate({
            labelMonthNext: js_language[user_lang].NextMonth,
            labelMonthPrev: js_language[user_lang].PreviousMonth,
            labelMonthSelect: js_language[user_lang].SelectMonth,
            labelYearSelect: js_language[user_lang].SelectYear,
            selectYears: true,
            selectMonths: true,
            monthsFull: js_language[user_lang].monthNames,
            monthsShort: js_language[user_lang].monthNamesShort,
            weekdaysFull: js_language[user_lang].dayNames,
            weekdaysShort: js_language[user_lang].dayNamesShort,
            today: js_language[user_lang].Today,
            clear: js_language[user_lang].Clear,
            close: '',
            format: current_lang.DateFormat.toLowerCase(),
            editable: true
        });
        clAppointment.DoBPicker = $("#patient-birthday").pickadate('picker');
        clAppointment.OpenBodCalendar();
    },
    SaveDoB: function () {
        if ($("#patient-birthday").val() != '') {
            $("#edit-birthday").css("display", "block");
            $("#save-birthday").css("display", "none");
            $("#patient-birthday").prop("disabled", true);
            clAppointment.DoBPicker.stop();
            clAppointment.SendDataPaciente({
                id: clAppointment.id_patient,
                action: 1,
                data: $("#patient-birthday").val()
            });
        }
        else 
            swal("Oops..!", js_language[user_lang].MissingFields, "error")
    },
    EditAllergies: function () {
        $("#edit-allergy").css("display", "none");
        $("#patient-allergy").prop("disabled", false);
        $("#save-allergy").css("display", "block");
    },
    SaveAllergies: function () {
        var field = $("#patient-allergy").val();
        if (field!= '') {
            $("#edit-allergy").css("display", "block");
            $("#save-allergy").css("display", "none");
            $("#patient-allergy").prop("disabled", true);
            clAppointment.SendDataPaciente({
                id: clAppointment.id_patient,
                action: 2,
                data: field
            });
        }
        else 
            swal("Oops..!", js_language[user_lang].MissingFields, "error")
    },
    SendDataPaciente: function (data) {
        $.ajax({
            type: "Post",
            datatype: "html",
            url: "/Citas/SetDataPaciente",
            data: data,
            cache: false,
            success: function (Result) {
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
    EditSurgeries: function () {
        $("#edit-surgeries").css("display", "none");
        $("#patient-surgeries").prop("disabled", false);
        $("#save-surgeries").css("display", "block");
    },
    SaveSurgeries: function () {
        if ($("#patient-surgeries").val() != '') {
            $("#edit-surgeries").css("display", "block");
            $("#save-surgeries").css("display", "none");
            $("#patient-surgeries").prop("disabled", true);
            clAppointment.SendDataPaciente({
                id: clAppointment.id_patient,
                action: 3,
                data: $("#patient-surgeries").val()
            });
        }
        else 
            swal("Oops..!", js_language[user_lang].MissingFields, "error");
    },
    SavePrescription: function () {
        var prescription = $("#receta").val();
        $.ajax({
            type: "Post",
            datatype: "html",
            url: "/Prescription/New",
            data: {
                patient_id: clAppointment.id_patient,
                description: prescription
            },
            cache: false,
                success: function (Result) {
                    if (Result["TypeOfResponse"] == 0) {
                        var newWin = window.open('', 'Print-Window');
                        newWin.document.open();
                        newWin.document.write('<html><body onload="window.print()" style="margin-top: 75px;">' + prescription + '</body></html>');
                        newWin.document.close();
                        setTimeout(function () { newWin.close(); }, 10);
                    }
                    else {
                        swal("Oops..!", js_language[user_lang].BadResponse, "error")
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
};

function AddRowToReceta(id, sentence) {
    console.log("adding row to receta");
    $("#body_receta").append($('<tr>')
        .attr('id', 'row_'+id)
        .append($('<td>')
            .append("<span>" + sentence + "</span>")
        )
        .append($('<td>')
            .append("<span class='btn btn-secondary btn_delete_medicine' data-id='" + id + "'>" + js_language[user_lang].Delete + "</span>")
        )
    );
}