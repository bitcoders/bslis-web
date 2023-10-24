//import { json } from "d3";
'use strict'

let zones =[];

$(document).ready(function () {

    GetZoneList(); //Get the zone list for the unit and bind the value in 'ZoneCode' dropdown list
    GetFlexMasterData(2, 'LandPosition'); // bind land position list to 'LandPosition' dropdown
    GetFlexMasterData(3, 'FieldCondition');// bind field condition list to 'FieldCondition' dropdown
    GetCaneTypes() // bind cane types to the dropdown list
});

// ====================== page Events start=======================
$("#VillageCode").on('focusin', function (e) {
    {
        $("#VillageCode").val("");
    }
});
$("#VillageCode").on("focusout", function (e) {
    getVillageDetails();
    $("#GrowerCode").val("");
    $("#growerName").text("");
});

$("#VarietyCode").on("keyup keydown", function (e) {
    //console.warn("VarietyCode" + e.type);
    SearchCaneVariety();
})
$("#GrowerCode").on('focusout', function (e) {
    getGrowerDetails();
});

$("#FieldCondition").on("change", function (e) {
    if ($("#FieldCondition").val() == "7") {
        $(".irrigationDays").fadeIn(1000);
        $("#IrrigatedDays").val(0)
    } else {
        $(".irrigationDays").fadeOut(1000);
        $("#IrrigatedDays").val(0)
    }
});

/// ===================== page events ends===============================

function getGrowerDetails() {
    let parameter = JSON.stringify({
        'unitCode': unitCode,
        'villageCode': $("#VillageCode").val(),
        'growerCode': $("#GrowerCode").val()
    });
    $.ajax({
        type: 'post',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: '/api/GrowerApi/GetGrower',
        data: parameter,
        success: (Response) => {
            console.log(Response);
            if (Response["header"]["statusCode"] == 200 || Response["header"]["statusCode"] == 302) {
                $("#growerName").text(Response["model"]["Name"] + "/" + Response["model"]["RelativeName"]);
            } else {
                $("#growerName").text("");
            }
        }
    });
}
function getVillageDetails() {
    let parameter = JSON.stringify({
        'unitCode': unitCode,
        'villageCode': $("#VillageCode").val(),
    });
    $.ajax({
        type: 'post',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        url: '/api/VillageApi/GetVillageByCode',
        data: parameter,
        success: (Response) => {
            console.log(Response);
            if (Response["header"]["statusCode"] == 200) {
                $("#villageName").text(Response["model"]["Name"]);
            } else {
                $("#villageName").text("");
            }
        }
    });
}


/// Get the zone list for the unit and bind the value in 'ZoneCode' dropdown list
function GetZoneList() {
    let parameter = JSON.stringify({
        'unit_code': unitCode
    });
    $.ajax({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        url: '/api/ZoneApi/GetZones',
        data: parameter,
        success: (response) => {
            if (response["header"]["statusCode"] == 200) {
                //console.log(response);
                $.each(response["model"], function (k, v) {
                    //console.log(v["Name"]);
                    //zones.push(v["Name"]);
                    $("#ZoneCode").append("<option value = " + v["Code"] + ">" + v["Name"] + " </option>");
                })
            } else {
                console.error("Invalid zones");
            }
        }
    })
};

/// search the cane variety and show in 'Autocomplete textbox'
function SearchCaneVariety() {
    let caneVarities = [];
    let parameter = JSON.stringify({
        'StringParam': $("#VarietyCode").val()
    });

    $.ajax({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: parameter,
        url: "/api/CaneVarietyApi/SearchCaneVariety",
        success: (Response) => {
            if (Response["header"]["statusCode"] == 200) {
                console.log(Response);
                $.each(Response["model"], function (k, v) {
                    caneVarities.push(v["Code"].toString());
                });
                $("#VarietyCode").autocomplete({
                    source: caneVarities,
                    select: (event, ui) => {
                        $("#VarietyCode").text(ui.item.label);
                        console.log(ui.item.label);
                    }
                });
                
            } else {
                console.error("Faild to load cane varities!");
            }
        }
    });
}


function GetFlexMasterData(flexMasterCode, controlId) {
    let parameter = JSON.stringify({
        'IntegerParam': flexMasterCode
    });
    $.ajax({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        url: '/api/FlexApiController/GetFlexSubMasterByCode',
        data: parameter,
        success: (response) => {
            if (response["header"]["statusCode"] == 200) {
                $.each(response["model"], function (k, v) {
                    //console.log(v["Name"]);
                    //zones.push(v["Name"]);
                    $("#" + controlId).append("<option value = " + v["Code"] + ">" + v["Value"] + " </option>");
                })
            } else {
                console.error("Invalid Flex Master Code");
            }
        }
    })
}

function GetCaneTypes() {
    
    $.ajax({
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        url: '/api/CaneType/GetCaneTypes',
        success: (response) => {
            let statusCode = response["header"]["statusCode"]
            if ( statusCode == "200") {
                $.each(response["model"], function (k, v) {
                    $("#CaneType").append("<option value = " + v["Code"] + ">" + v["Name"] + " </option>");
                });
            } else {
                console.error("Error : " + setatusCode);
            }
        }
    });
}