
var reportDetailsByCode = function (code) {
    var parameter = JSON.stringify({
        'workingCode' : code
    });
    $.ajax({
        type: 'POST',
        url: '/api/ReportDetails/GetReportDetailsByCode',
        datatype: 'json',
        contentType: 'application/json; charset=utf-8',
        data: parameter,
        success: (response, jqXHR) => {
            if (response.statusCode == 200) {
                //console.log(response);
                $("#Code").val(response.result["Code"]);
                $("#Name").val(response.result["Name"]);
                $("#Description").val(response.result["Description"]);
                $("#ReportSchemaCode").val(response.result["ReportSchemaCode"]);
            } else {
                console.log(response.statusCode);
            }
        },
        Error: (error) => {
            console.warn(error);
        }
    });
};

var updateReoirtDetails = function () {
    var parameter = $('form.EditReportDetails').serialize();
    $.ajax({
        type: 'post',
        url: '/api/ReportDetails/PostReportDetailsByCode',
        data : parameter,
        success: (response) => {
            $('#exampleModal-2').modal('toggle');
            location.reload(true);
            console.log(response);
        },
        failure: (Error) => {
            console.warn(Error);
        }
    });
}