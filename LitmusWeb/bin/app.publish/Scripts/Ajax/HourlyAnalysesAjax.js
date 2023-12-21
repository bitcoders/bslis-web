let param;
let syrup_diversion_cane_factor = 0;
let user_code;
$(document).ready(function(){
   let season_code = $("#season_code").val();
   let unit_code = $("#unit_code").val();
    let entry_date = $("#entry_date").val();
    let user_code = $("#user_code").val();
     param = JSON.stringify({
        "unitCode": unit_code,
        "seasonCode": season_code,
        "reportDate": entry_date
    });
    //console.log(param);
    post_syrup_diversion_cane_factor();
    
    $(".delete-button").click(function (e) {
        let result = confirm("Are you sure to delete the record? Once it is deleted can't be recovered.");
        if (result) {
            var line_id = $(this).data("id");
            DeleteHourlyAnalyses(unit_code, user_code, line_id);
        }
        
    });
});
function post_syrup_diversion_cane_factor() {
    

    $.ajax({
        method: "POST",
        url: "/api/HourlyAnalyses/GetSyrupDiversionFactor",
        contentType: "application/json",
        dataType: "json",
        data: param,
        success: (response) => {
            syrup_diversion_cane_factor = response.result.factor_value;
            let factor_from_date = response.result.factor_date;
            let factor_from_season = response.result.season_code;
            $("#cane_diversion_factor").html(" Date " + factor_from_date + " diversion factor = " + syrup_diversion_cane_factor);
            if (syrup_diversion_cane_factor == 0) {
                $("#cane_diverted_for_syrup").prop('readonly',false);
            }
        },
        error: (jqXHR) => {
            console.warn(jqXHR);
        }
    });
}

$("#diverted_syrup_quantity").on('change', function () {
    post_syrup_diversion_cane_factor();
    let syrup_diverted = $("#diverted_syrup_quantity").val();
    $("#cane_diverted_for_syrup").val((syrup_diverted * syrup_diversion_cane_factor).toFixed(2));
});

//Delete HourlyAnalyses


function DeleteHourlyAnalyses(unit_code, user_code, line_id) {
    $.ajax({
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: "/api/HourlyAnalyses/Delete",
        "headers": {
            "Content-Type": "application/json",
            "unit_code": unit_code,
            "user_code": user_code,
            "line_id" : line_id
        },
        success:(response)=> {
            if (response.Success) {
                alert("Record Deleted");
                location.reload();// refresh current page
            } else {
                alert("Error: " + response.Message);
            }
        },
        error: (error) => {
            var details = error.responseText;
            alert("Error: " + details);
        }
    });
}