

var currentUser;
let currentUnit;

$(document).ready(function () {
    currentUser = $("input[name=_hiddenUserCode]").val();
    currentUnit = $("input[name=_hiddenUnitCode]").val();
    
    
    GetUnitRightsForReport();
    GetSeasonRightsForModification();
});
var GetUnitRightsForReport = function () {
    $.ajax({
        type: 'POST',
        url: "/api/UserMasterApi/UnitRightsList",
        data: JSON.stringify({"StringParam": currentUser}),
        contentType: "application/json; charset=utf-8",
        datatype: 'json',
        success: (Response) => {
            //console.log(Response);
            $.each(Response, function (key, value) {
                //console.log(value);
                $("#reportUnit").empty();

                $.each(value, function (tempKey, tempValues) {
                    if ($("input[name=_hiddenUnitCode]").val() == tempValues.Code) {
                        $("#unit_code").append('<option class="dropdown-item" value = ' + tempValues.Code + ' selected="selected">' + tempValues.Name + '</option>');
                    }
                    else {
                        $("#unit_code").append('<option class="dropdown-item" value = ' + tempValues.Code + '>' + tempValues.Name + '</option>');
                    }

                });
            });
        },
        Error: (error) => {
            console.log(error);
        }
    });
};

var GetSeasonRightsForModification = function () {
    $.ajax({
        type: "POST",
        url: "/api/UserMasterApi/SeasonModificationRights",
        datatype: "Json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "StringParam": currentUser }),
        success: (Response) => {
            //console.log(Response);
            
            $.each(Response, function (key, value) {
                //console.log(value);
                $("#season_code").empty();
                $("#season_code").append('<option class="dropdown-item" value = "0"> -- Select a Season --</option>');    

                $.each(value, function (tempKey, tempValues) {
                    if ($("input[name=_hiddenSeasonCode]").val() == tempValues.SeasonCode) {
                        $("#season_code").append('<option class="dropdown-item" value = ' + tempValues.SeasonCode + ' selected="selected">' + tempValues.SeasonYear + '</option>');
                    }
                    else {
                        $("#season_code").append('<option class="dropdown-item" value = ' + tempValues.SeasonCode + '>' + tempValues.SeasonYear + '</option>');
                    }

                });
            });
        },
        Error: (error) => {
            console.log(error);
        }
    });
}