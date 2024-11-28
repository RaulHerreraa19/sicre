// A $( document ).ready() block.
$(document).ready(function () {
    moment.locale(user_lang);
    $(".reports").hide();
    $(".table_show").hide();
    $("#display_table").click(function () {
        $(".table_show").show("slow");
    });
    $(".icon-cross2").click(function () {
        $("#table_apoint").hide();
    });

    var table1 = $('#example_1').DataTable({
        dom: 'Bfrtip',
        buttons: js_language[user_lang].Buttons,
        language: js_language[user_lang].DataTables
    });
    table1.buttons().container()
       .appendTo('#example_wrapper .col-md-6:eq(0)');
    $(".buttons-collection").addClass("btn btn-primary mr-1");
    setTimeout(function () { $(".buttons-copy, .buttons-csv, .buttons-print, .buttons-pdf, .buttons-excel, .buttons-collection").addClass("btn btn-primary mr-1") }, 300);
    var table = $('#example').DataTable({
        dom: 'Bfrtip',
        buttons: js_language[user_lang].Buttons,
        language: js_language[user_lang].DataTables
    });

    table.buttons().container()
        .appendTo('#example_wrapper .col-md-6:eq(0)');
    $(".buttons-collection").addClass("btn btn-primary mr-1");
    setTimeout(function () { $(".buttons-copy, .buttons-csv, .buttons-print, .buttons-pdf, .buttons-excel, .buttons-collection").addClass("btn btn-primary mr-1") }, 300);

    $('#datetimepicker1').datetimepicker({
        locale: user_lang
    });
    $('#datetimepicker2').datetimepicker({
        locale: user_lang
    });
    $("#today").click(function () {
        $('#datetimepicker1').datetimepicker({
            locale: user_lang,
            "setDate": new Date()
        });
        var dateTime = new Date();
        dateTime = moment(dateTime).format('L');
        $('#start').val(dateTime);
        $('#end').val(dateTime);
        $('#datetimepicker2').datetimepicker({
            locale: user_lang,
            "setDate": new Date()
        });
    });
    $("#quince").click(function () {
        var dateTime = new Date();
        dateTime = moment(dateTime).format('L');


        var menos = new Date();
        menos = moment(menos).days(-14).format('L');
        //alert(menos);
        $('#datetimepicker1').datetimepicker({
            locale: user_lang,
            "setDate": menos
        });

        $('#start').val(menos);
        $('#end').val(dateTime);
        $('#datetimepicker2').datetimepicker({
            locale: user_lang,
            "setDate": dateTime
        });
    });

    $("#month").click(function () {
        var dateTime = new Date();
        dateTime = moment(dateTime).format('L');

        var diasxmes = moment().daysInMonth();
        var menos = new Date();
        menos = moment(menos).days(-diasxmes).format('L');
        //alert(diasxmes);
        $('#datetimepicker1').datetimepicker({
            locale: user_lang,
            "setDate": menos
        });

        $('#start').val(menos);
        $('#end').val(dateTime);
        $('#datetimepicker2').datetimepicker({
            locale: user_lang,
            "setDate": dateTime
        });
        //format: 'MM/DD/YYYY'
    });

    $.ajax({
        type: "GET",
        datatype: "html",
        url: "/Asistente/GetPersons",
        cache: false,
        success: function (Result) {
            //console.log(Result);
            //debugger;
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
    var chart1;
    var service_ch;

    //Ajax to construct report
    $("#search").click(function () {
        if ($("#pa_select").val() !== null && $("#start").val() != '' && $("#end").val() != '') {
            swal({
                title: js_language[user_lang].WaitResponse,
                text: js_language[user_lang].Generate,
                timer: 6000,
                showConfirmButton: false
            });
            console.log("Entonces va hacer report by users" + chart1);
            if (chart1 != null) {
                //alert("te destrozare");
                chart1.destroy();
                //chart1.redraw();
            }
            if (service_ch != null) {
                //alert("te destrozare");
                service_ch.destroy();
                //chart1.redraw();
            }
            //debugger;
            $.ajax({
                type: "Post",
                datatype: "html",
                url: "/Asistente/Reports",
                data: {
                    start: $("#start").val(),
                    fin: $("#end").val(),
                    id: $("#pa_select").val()
                },
                cache: false,
                success: function (Result) {
                    //console.log(Result);
                    //debugger;
                    console.log("Reporte creado correctamente")
                    var t = $('#example_1').DataTable();
                    t.clear().draw();
                    var total = 0;
                    $.each(Result.Data.datatable, function (i, vl) {
                        //debugger;
                        var precio = vl.precio;
                        var observaciones = vl.observaciones;
                        var observaciones_asistente = vl.observaciones_asistente;
                        if (precio == null) {
                            precio = 'no tiene';
                        }
                        else {
                            total = total + parseInt(precio);
                        }
                        if (observaciones == null) {
                            observaciones = 'no tiene';
                        }
                        if (observaciones_asistente == null) {
                            observaciones_asistente = 'no tiene';
                        }
                        var date = moment(vl.cita_date).format('DD-MM-YYYY');
                        //console.log("new date... " + date);

                        t.row.add([
                              date,
                              vl.status,
                              observaciones,
                              observaciones_asistente,
                              precio
                        ]).draw(false);
                    });
                    console.log(t.data().length);
                    console.log("total " + total);
                    $("#inicio").html($("#start").val());
                    $("#fin").html($("#end").val());
                    $("#name").html(Result.Data.nombre);
                    var string = numeral(total).format('$ 0,0[.]00');
                    $("#Total_revenues").html(string);
                    $("#Total_appoint").html(t.data().length);
                    $("#Total_revenues_").html(string);
                    $("#Total_appoint_").html(t.data().length);
                    var pay = [];
                    $.each(Result.Data.datapie, function (i, vl) {
                        if (Result.Data.datapie[i].name == "AGENDADA") {
                            pay.push({
                                name: js_language[user_lang].Unattended,
                                y: Result.Data.datapie[i].y
                            })
                        }
                        if (Result.Data.datapie[i].name == "CANCELADA") {
                            pay.push({
                                name: js_language[user_lang].Canceled,
                                y: Result.Data.datapie[i].y
                            })
                        }
                        if (Result.Data.datapie[i].name == "COMPLETADA") {
                            pay.push({
                                name: js_language[user_lang].Completed,
                                y: Result.Data.datapie[i].y
                            })
                        }
                    });
                    console.log(pay);
                    var colors = []
                    // Build the chart
                    if (pay.length <= 2) {
                        colors.push('#8bbc21', '#EEF526')
                        
                    }
                    else {
                        colors.push('#EEF526', '#FF0000', '#8bbc21')
                    }
                   
                    chart1 = Highcharts.chart('container_six', {
                        colors: colors,
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            type: 'pie'
                        },
                        title: {
                            text: js_language[user_lang].Percentage_citas
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: false
                                },
                                showInLegend: true
                            }
                        },
                        series: [{
                            name: 'Total',
                            colorByPoint: true,
                            data: pay
                        }]
                    });

                    //Put datatable services
                    var tb = $("#example").DataTable();
                    tb.clear().draw();
                    var total_services = 0;
                    $.each(Result.Data.datatable_service, function (i, vl) {
                        tb.row.add([
                              Result.Data.datatable_service[i].nombre,
                              Result.Data.datatable_service[i].total
                        ]).draw(false);

                        total_services = total_services + parseInt(Result.Data.datatable_service[i].total);
                    });
                    console.log("total service.." + total_services);
                    tb.row.add([
                              'Total',
                              total_services
                    ]).draw(false);

                    var services = [];
                    var data_ = [];
                    $.each(Result.Data.datatable_service, function (i, vl) {
                        services.push({
                            name: Result.Data.datatable_service[i].nombre,
                            data: [Result.Data.datatable_service[i].total]
                            });
                    });
                    //Put the other chart
                    console.log("Services...");
                    console.log(services)
                   service_ch = Highcharts.chart('container_service', {
                        chart: {
                            type: 'column'
                        },
                        title: {
                            text: ''
                        },
                        subtitle: {
                            text: ''
                        },
                        xAxis: {
                            crosshair: true,
                            visible: false
                        },
                        yAxis: {
                            min: 0,
                            title: {
                            text: js_language[user_lang].Quantity
                            },
                            visible: true
                        },
                        tooltip: {
                            headerFormat: '<span style="font-size:10px"></span><table>',
                            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                                '<td style="padding:0"><b>{point.y:.1f}</b></td></tr>',
                            footerFormat: '</table>',
                            shared: true,
                            useHTML: true
                        },
                        plotOptions: {
                            column: {
                                pointPadding: 0.2,
                                borderWidth: 0
                            }
                        },
                        series: services
                   });
                   swal("Good..!", js_language[user_lang].OkResponse, "success")
                   $(".reports").show("slow");
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
        else {
            swal("Oops..!", js_language[user_lang].BadResponse, "error")
        }
    });





  

});