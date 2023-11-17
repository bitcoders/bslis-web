


$(document).ready(function () {
    var season_code = $("input[name=_hiddenSeasonCode]").val();


    var parameter = JSON.stringify({
        'unit_code': $("input[name=_hiddenUnitCode]").val(),
        'season_code': season_code
    });

    var setParameter = function () {
        parameter = JSON.stringify({
            'unit_code': $("input[name=_hiddenUnitCode]").val(),
            'season_code': season_code
        });
    }
    $(window).on("load", function () {
        getAjaxReponse();
    });
    $("#season_code").on("change", function () {
        season_code = $("#season_code option:selected").val();

        setParameter();
        getAjaxReponse();
    });

    //console.log(parameter);
    var getAjaxReponse = function () {
        $.ajax({
            type: 'post',
            url: 'api/DailyReportApi/getProcessedDateRange',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            data: parameter,
            success: (response, jqXHR) => {
                var firstDate = dateFormat(response.minDate);
                var lastDate = dateFormat(response.maxDate);

                $(".datepicker").datetimepicker({
                    maxDate: lastDate,
                    minDate: firstDate
                });
                $(".datepicker").val(lastDate);
                //$(".report_dates").text("Report is available from " + dateFormatDDMMYYYY(response.minDate) + " to " + dateFormatDDMMYYYY(response.maxDate) + ".");
                $("#spanFromDate").text(dateFormatDDMMYYYY(response.minDate));
                $("#spanToDate").text(dateFormatDDMMYYYY(response.maxDate));

                //console.log(firstDate);
                //console.log(lastDate);
            },
            error: (err) => {
                console.warn(err);
            }
        });
    }

    var dateFormat = function (inputDate) {
        var x = new Date(inputDate);
        var date = x.getDate();
        var month = x.getMonth() + 1;
        var year = x.getFullYear();
        //var result = date + "/" + month + "/" + year;
        var result = year + "/" + month + "/" + date;
        return result;
    }

    var dateFormatDDMMYYYY = function (inputDate) {
        var x = new Date(inputDate);
        var date = x.getDate();
        var month = x.getMonth() + 1;
        var year = x.getFullYear();
        var result = date + "-" + month + "-" + year;
        return result;
    }

    
});



