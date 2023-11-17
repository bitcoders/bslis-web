
(function ($) {
    unit_code = $("#hiddenUnitCode").val();
    var param = { 'unit_code': unit_code };
    console.log(unit_code);


    var sugarBags = 0;
    var molassesSentOut = 0;
    if ($("#InitialState").val() == "New") {
        $("input[type='text']").val(0);
        $("input[type='number']").val(0);
        $("input[type='decimal']").val("0.00000");
        $("#btnSave").val('Save');
        $(".decimalthreeplace").val("0.000");
    }
    else {
        $("#btnSubmit").attr('value', 'Update');
    }

    /// api to get total sugar bags for the date 
    $.ajax({
        type: 'POST',
        url: 'api/HourlySummary/GetHourlyDataForDate/',
        data: JSON.stringify(param),
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: (data, txtStatus, jqXHR) => {
            if (data != null) {
                sugarBags = data.od_total_bags;
            }

        }, Error: (Error) => {
            console.log(Error);
        }
    });




    $("#live_steam_generation").change(function () {
        $("#exhaust_steam_generation").val($(this).val())
    });

    // start calculation of exhaust steam consumption
    var exhaustSteamConsumption = 0;

    $("#cane_crushed").change(function () {
        cane_crushed = $("#cane_crushed").val();
        calculate_steam_data();
    });
    $(".steam-data").change(function () {
        exhaustSteamConsumption = 0;
        steam_percent_cane = 0
        cane_crushed = $("#cane_crushed").val();
        $(".steam-data").each(function () {
            exhaustSteamConsumption = parseFloat(exhaustSteamConsumption) + parseFloat($(this).val());
        });
        $("#exhaust_steam_consumption").val(exhaustSteamConsumption);
        if (cane_crushed > 0) {
            calculate_steam_data();
        }
        else {
            $("#steam_percent_cane").val(0);
            $("#steam_per_ton_cane").val(0);
            //var mutePrompt = false;
            //if (mutePrompt === true) {

            //}
            //else {
            //    value = prompt("Cane Crushed is 0, provied a valid figure.");
            //    $("#cane_crushed").val(value);
            //    if (value === null) {
            //        mutePrompt = true;
            //    }
            //}

        }
    });


    /// power tab calculation
    $(".power-data").change(function () {
        var cane_crushed = 0;
        cane_crushed = parseFloat($("#cane_crushed").val());
        var power_from_sugar = 0, power_from_dg = 0, power_from_cogen = 0, power_from_grid = 0, total_power_consumption = 0;
        var power_per_ton_cane = 0, power_per_qtl_sugar = 0;
        power_from_sugar = $("#power_from_sugar").val();
        power_from_dg = $("#power_dg_set").val();
        power_from_cogen = $("#power_import_cogen").val();
        power_from_grid = $("#power_from_grid").val();
        total_power_consumption = parseFloat(power_from_sugar) + parseFloat(power_from_dg) + parseFloat(power_from_cogen) + parseFloat(power_from_grid);
        if (cane_crushed > 0) {
            power_per_ton_cane = ((total_power_consumption) / (cane_crushed / 10)).toFixed(2);
        } else {
            power_per_ton_cane = 0;
        }

        if (sugarBags > 0) {
            power_per_qtl_sugar = (total_power_consumption / sugarBags).toFixed(2);
        }
        else {
            //power_per_qtl_sugar = (total_power_consumption / 1).toFixed(2);
            power_per_qtl_sugar = 0;
        }


        $("#total_power").val(total_power_consumption);
        $("#power_per_ton_cane").val(power_per_ton_cane);
        $("#power_per_qtl_sugar").val(power_per_qtl_sugar);
    });

    // a function which calculates and display exhaust steam consumption value when it is called.
    var calculate_steam_data = function () {
        if (cane_crushed > 0) {
            steam_percent_cane = ((parseFloat(exhaustSteamConsumption) / parseFloat(cane_crushed)) * 100).toFixed(2);
            $("#steam_percent_cane").val(steam_percent_cane);
            $("#steam_per_ton_cane").val(steam_percent_cane);
        }
        else {
            $("#steam_percent_cane").val(0);
            $("#steam_per_ton_cane").val(0);
        }


        var sugarPromptMute = false;
        //if (sugarPromptMute === false) {
        //    if (sugarBags == 0) {
        //        sugarPromptMute = confirm("Sugar bags not avialable.\nKindly provide Sugar Bags details before proceeding.\nClick Cancel to continue");
        //    }
        //}

        //console.log(sugarBags);
        if (sugarBags == 0) {
            steam_per_qtl_sugar = 0;
        }
        else {
            steam_per_qtl_sugar = parseFloat(($("#exhaust_steam_consumption").val()) * 10) / parseFloat(sugarBags);
            $("#steam_per_qtl_sugar").val(steam_per_qtl_sugar.toFixed(2));
        }
    }
    // end calculation of exhaust steam consumption

    // get sugar bagged quantity from hourlyanalyses which will be used to calculate
    // Steam used per 10 tone of cane
    //for the day using the api(ApiControllers/HourlySummaryControllers)

    $("#MolassesSentOutBHeavy").focusout(function () {
        funcCalculateMolassesSentOut();
    }).focusin(function () {
        funcCalculateMolassesSentOut();
    });
    $("#MolassesSentOutCHeavy").focusout(function () {
        funcCalculateMolassesSentOut();
    }).focusin(function () {
        funcCalculateMolassesSentOut();
    });
    var funcCalculateMolassesSentOut = function () {
        molassesSentOut = parseFloat($("#MolassesSentOutBHeavy").val()) + parseFloat($("#MolassesSentOutCHeavy").val());
        $("#molasses_sent_out").val(molassesSentOut);
    }
})(jQuery);
