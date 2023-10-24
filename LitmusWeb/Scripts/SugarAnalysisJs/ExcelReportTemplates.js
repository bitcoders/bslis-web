$('document').ready(function () {
    $("#btnSubmit").prop('disabled',true)
    var reportType;
    $("#selectReportType").on('change', function () {
        reportType = $("#selectReportType").find(":selected").val();
        console.log(reportType);
        switch (reportType) {
            case '0':
                $("#btnSubmit").prop('disabled', true)
                break;
            case 'daily':
                $("#divDateFrom").removeClass("col-sm-4");
                $("#divDateFrom").addClass("col-sm-10");
                $(".txtDateTo").hide();
                $("#btnSubmit").prop('disabled', false)
                break;
            case 'periodical':
                $("#divDateFrom").removeClass("col-sm-10");
                $("#divDateFrom").addClass("col-sm-4");
                $(".txtDateTo").show();
                $("#btnSubmit").prop('disabled', false)
                break;
            default:

                $("#btnSubmit").prop('disabled', true)
                break;
        }
    });
   
});