// add reportSchemaColumn

var addReportSchemaColumn = function () {
    var param = JSON.stringify({
        'SchemaCode': $("#SchemaCode").val(),
        'ColumnText': $("#ColumnText").val(),
        'IsActive' : true,
        'SearchKeyword': $("#SearchKeyword").val(),
        'CreatedBy': $("#CreatedBy").val()
    });
    $.ajax({
        type: 'Post',
        url: '/api/ReportSchmeaColumn/Add',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data : param,
        success: (Response) => {
            if (Response['statusCode'] == 201) {
                showSuccessToastCustomMessage("Data saved successfully");
                $("#ColumnText").val('');
                $("#SearchKeyword").val('');
                $("#CreatedBy").val('');
                $("#ColumnText").focus();
            }
            else {
                showDangerToast("Failed to save details.");
                console.log(Response);
            }
            
        },
        Error: (Error) => {
            console.log(Error);
            
        }
    });
}