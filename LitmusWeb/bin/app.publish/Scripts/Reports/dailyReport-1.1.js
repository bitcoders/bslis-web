(function () {

    $("#btnSubmit").click(function () {
        alert("text");
        var selectedReportCode = $("#report_name").find(":selected").val;
        if (selectedReportCode == 0) {
            alert("Please select a report type from the list.");
        }
        console.log(selectedReportCode);
    });
    
    
});

var currentUser;
var jsonUnitParam;
$(document).ready(function () {
    currentUser = $("#_hiddenUserCode").val();
    GetUnitRightsForReport();
});


var GetUnitRightsForReport = function () {
    
    $.ajax({
        type: 'POST',
        url: "/api/UserMasterApi/UnitRightsList",
        data: JSON.stringify(jsonUnitParam),
        contentType: "application/json; charset=utf-8",
        datatype: 'json',
        success: (Response) => {
            //console.log(Response);
            $.each(Response, function (key, value) {
                //console.log(value);
                $("#reportUnit").empty();
                
                $.each(value, function (tempKey, tempValues) {
                    if ($("input[name=_hiddenUnitCode]").val() == tempValues.Code) {
                        $("#reportUnit").append('<option class="dropdown-item" value = ' + tempValues.Code + ' selected="selected">' + tempValues.Name + '</option>');
                    }
                    else {
                        $("#reportUnit").append('<option class="dropdown-item" value = ' + tempValues.Code + '>' + tempValues.Name + '</option>'); 
                    }
                    
                });
            });
        },
        Error: (error) => {
            console.log(error);
        }
    });
}; 