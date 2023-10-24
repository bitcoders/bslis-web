

var weatherRecordsByDates = function () {
    var parameter = JSON.stringify({
        'UnitCode': $("#UnitCode").val(),
        'FromDate': $("#FromDate").val(),
        'ToDate': $("#ToDate").val()
    });
    $.ajax({
        type: 'POST',
        url: 'api/WeatherApiController/GetWeatherRecordsByDateRange',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: parameter,
        success: (Response, jqXHR) => {
            //console.log(JSON.stringify(Response));  
            fillTableBody(Response);
        },
        Error: (error) => {
            console.log(error);
        }
    })
}


var fillTableBody = function (response) {
    console.log(response);
    let row;
    $.each(response.wModel, function (i, item) {
        $("#tbody").append('<tr><td>' + item.Id + '</td>'
            + '<td>' + item.RecordDate + '</td>'
            + '<td>' + item.TemperatureMin + '</td>'
            + '<td>' + item.TemperatureMax + '</td>'
            + '<td>' + item.Humidity + '</td>'
            + '<td>' + item.WindSpeed + '</td>'
            + '<td>' + item.RainFallMm + '</td>'
            + '<td>' + item.UvIndex + '</td>'
            + '<td>' + item.WeatherType + '</td>'
            + '<td>' + item.AllWeatherConditions + '</td>'
            + ' </tr > ')
        
    });
    
}
$("#btnSearch").click(function () {
    $("#tbody tr").remove();
    weatherRecordsByDates();
    
})