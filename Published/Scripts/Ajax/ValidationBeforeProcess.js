
var validateDataBeforeFinalProcess = function () {
    var parameter = {
        "unit_code": $("#unit_code").val(),
        "season_code": $("#season_code").val(),
        "process_date": $("#process_date").val()
    };
$.ajax({
    type: 'POST',
    url: '/api/ValidationApi/validateDataBeforeFinalProcess',
    data: JSON.stringify(parameter),
    dataType: 'json',
    contentType: 'application/json; charset=utf-8',
    success: (response, jqxhr) => {
        $.each(response.validationModels, function (k, v) {
            if (v.validated == true) {
                $("#data-validation").append(
                    '<h4 class="alert alert-success md-5"><i class="mdi mdi-checkbox-marked-circle-outline"> ' + v.validationMessage + '<i></h4>'
                );
            }
            else {
                $("#btnProcess").prop('disabled','true');
                $("#data-validation").append(
                    '<h4 class="alert alert-danger md-5"><i class="mdi mdi-close-circle-outline"> ' + v.validationMessage + '<i></h4>'
                );
            }
        })
    },
    Error: (error) => {
        console.warn(error);
    }
});
}
   