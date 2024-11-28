var ChartColor = ["#5D62B4", "#54C3BE", "#EF726F", "#F9C446", "rgb(93.0, 98.0, 180.0)", "#21B7EC", "#04BCCC", "#ffaf00", "#ecf8ab", "#3498DB", "#F39C12"];
CLReports = {
    Settings: {
        start_date: "",
        end_date: "",
        type_report: 1,
        view: "",
        report_name: ""
    },
    ShowReport: function () {
        console.log("getting report...");
        $("#btn_getReport").addClass("btn_disabled");
        var dates = $("#reportrange").text().trim();
        CLReports.Settings.type_report = $("#report_type").val();
        ShowLoading();
        console.log("settings:", CLReports.Settings);
        var the_data = JSON.stringify(CLReports.Settings);
        $.ajax({
            type: "post",
            url: "/Reports/getReport",
            contentType: "application/json; charset=utf-8",
            data: the_data,
            success: function (vista) {
                $("#report_container").html("");
                $("#report_container").html(vista);
            },
            error: function (request, status, error) {
                Open_Swal("error", "something went wrong, try again", null, null, 5000);
            },
            complete: function (xhr, status) {
                swal.close();
                $("#btn_getReport").removeClass("btn_disabled");
            }
        });
    },
    PieChart: function (divId, labels, data) {
        //var a = $("#" + divId);
        var pieChartCanvas = $("#" + divId).get(0).getContext("2d");
        var pieChart = new Chart(pieChartCanvas, {
            type: "pie",
            options: {
                responsive: true,
                animation: {
                    duration: 0,
                    animateScale: false,
                    animateRotate: false
                }
            },
            data: {
                labels: labels,
                datasets: [{
                    data: data,
                    backgroundColor: ChartColor
                }]
            }
        });
    },
    LineChart: function (divId, labels, data, titleChart, footerChart, valueLabelChart, dataLabel) {
        var lineChartCanvas = $("#" + divId).get(0).getContext("2d");
        console.log(footerChart);
        new Chart(lineChartCanvas, {
            type: "line",
            options: {
                responsive: true,
                maintainAspectRatio: !1,
                animation: {
                    duration: 0,
                    animateScale: false,
                    animateRotate: false
                },
                legend: {
                    position: "bottom"
                },
                hover: {
                    mode: "label"
                },
                scales: {
                    xAxes: [{
                        display: !0, gridLines: {
                            color: "#f3f3f3", drawTicks: !1
                        },
                        scaleLabel: {
                            display: !0,
                            labelString: footerChart
                        }
                    }
                    ],
                    yAxes: [{
                        display: !0, gridLines: {
                            color: "#f3f3f3", drawTicks: !1
                        }
                        , scaleLabel: {
                            display: !0, labelString: valueLabelChart
                        }
                    }
                    ]
                },
                title: {
                    display: !0,
                    text: titleChart
                }
            },
            data: {
                labels: labels,
                datasets: [{
                    data: data,
                    label: dataLabel,
                    lineTension: 0,
                    borderColor: ChartColor[0],
                    pointBorderColor: ChartColor[0],
                    borderWidth: 3,
                    pointBackgroundColor: "#FFF",
                    pointBorderWidth: 2,
                    pointHoverBorderWidth: 2,
                    pointRadius: 4,
                    fill: 'false',
                }]
            }
        }
        );
    }
};


$("#btn_getReport").on("click", CLReports.ShowReport);