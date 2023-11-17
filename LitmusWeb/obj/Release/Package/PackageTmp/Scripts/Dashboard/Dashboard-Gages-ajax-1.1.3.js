var selected_unit, LatestHourlyEntryDate;
var tab;
var jsonParam;
var total_juice_hour = 0, total_juice_day = 0, total_crush, crop_day = 0
    , total_water_hour = 0, total_water_day = 0;
var nm_pj_brix = 0, nm_pj_purity = 0, nm_bagasse_pol = 0, nm_bagasse_moisture = 0;
var om_pj_brix = 0, om_pj_purity = 0, om_bagasse_pol = 0, om_bagasse_moisture = 0;
var combined_pj_brix = 0, combined_pj_pol = 0, combined_pj_purity = 0
var combined_mj_brix = 0, combined_mj_pol = 0, combined_mj_purity = 0;
var combined_lj_brix = 0, combined_lj_pol = 0,combined_lj_purity = 0;
var clear_juice_brix_avg, clear_juice_pol_avg, clear_juice_purity = 0;
var un_sulphered_brix_avg, un_sulphered_pol_avg, un_sulphered_purity = 0;
var sulphered_brix_avg, sulphered_pol_avg, sulphered_purity = 0;

var oliver_brix_avg, oliver_pol_avg, oliver_purity_avg = 0;
var press_cake_avg = 0, press_cake_moisture_avg = 0;
var final_mol_brix_avg, final_mol_pol_avg, final_mol_purity = 0;
var combined_bagasse_pol_avg, combined_bagasse_moist_avg = 0
var combined_bagasse_pol = 0, combined_bagasse_moisture = 0
var standing_truck = 0, standing_trippler = 0,
    standing_trolley = 0, standing_cart = 0, uncrushed_cane = 0, crushed_cane = 0
    ,hourly_sugar_bags, total_sugar_bags = 0
    , today_crushed_cane = 0, today_uncrushed_cane = 0
    , todate_crushed_cane = 0, todate_uncrushed_cane = 0;
    
// ledger data variables
var cane_crushed = 0, estimated_sugar_percent_cane = 0.0, total_loss = 0.0
    , total_sugar_bagged = 0, final_molasses_sent_out_percent = 0.0
    , sugar_in_process_qtl = 0, molasses_in_process_qtl = 0;

// Previous day summary variables 
var prv_day_cane_crushed = 0, todate_cane_crushed = 0,
    prv_day_recovery = 0, todate_recovery = 0,
    prv_day_losses = 0, todate_losses = 0,
    prv_day_sugar_bags = 0, todate_sugar_bags = 0,
    prv_day_final_molasses_sentout = 0, todate_final_molasses_sentout = 0,
    prv_day_stoppages = 0, todate_stoppages = 0,
    prv_day_sugar_in_proc_qtl = 0, todate_sugar_in_proc_qtl = 0,
    prv_day_molasses_in_proc = 0, todate_molasses_in_proc = 0,
    evaporation_percent_clear_juice = 0, evaporation_percent_clear_juice_todate = 0,
    diverted_clear_juice_qtl = 0, diverted_clear_juice_qtl_todate = 0,
    diverted_clear_juice_percent_cane = 0, diverted_clear_juice_percent_cane_todate= 0,
    diverted_cane_qtl = 0, diverted_cane_qtl_todate= 0,
    diverted_cane_percent = 0, diverted_cane_percent_todate =0 ,
    net_clear_juice_percent = 0, net_clear_juice_percent_todate = 0;

var entry_date, entry_time;

// a function to blink the text
function blink_text() {
    $('.blink').fadeOut(500);
    $('.blink').fadeIn(500);
}

function checkForCriticalAlerts() {
    $.ajax({
        type: 'POST',
        url: '/api/DashBoardApi/GetDashBoardAlers',
        data: JSON.stringify(jsonParam),
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: (response, statusCode, jqXHR) => {
            var message = response.alertMessage;
            if (response.alertMessage != "") {
                if (!$("#div-critical-alert").length) {
                    $('<div class="alert alert-danger hide" id="div-critical-alert" name="div-critical-alert"><span class="blink" id="criticalMessageText"></span></div>').prependTo($(".content-wrapper"));
                }
                
                $("#criticalMessageText").text(response.alertMessage);
                $("#div-critical-alert").fadeIn("slow");
                setInterval(blink_text, 1000);
                $("#div-critical-alert").removeClass('hide');
            } else {
                //$("#div-critical-alert").fadeOut("slow");
                $("#div-critical-alert").remove();
                //$("#div-critical-alert").addClass('hide');

            }
        },
        Error: (error) => {
            console.warn("Error occured at dashboard api")
        }
    });
}
// hourlyAnalyses variables

function unit_one_data(selected_unit) {

    // ajax for two horly summary data
    
    $.ajax({
        type: "POST",
        url: "/api/TwoHourlySummary/",
        data: JSON.stringify(jsonParam),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: (response, txtStatus, jqXHR) => {
            if (jqXHR.status == 200) {
                //total_jice = response[0]['total_juice'];
                total_crush = 0
                combined_pj_brix = response[0]['combined_pj_brix'];
                combined_pj_pol = response[0]['combined_pj_pol'];

                combined_mj_brix = response[0]['combined_mj_brix'];
                combined_mj_pol = response[0]['combined_mj_pol'];

                combined_lj_brix = response[0]['combined_lj_brix'];
                combined_lj_pol = response[0]['combined_lj_pol'];

                clear_juice_brix_avg = response[0]['clear_juice_brix_avg'];
                clear_juice_pol_avg = response[0]['clear_juice_pol_avg'];
                clear_juice_purity = response[0]['clear_juice_purity'];
                un_sulphered_brix_avg = response[0]['un_sulphered_brix_avg'];
                un_sulphered_pol_avg = response[0]['un_sulphered_pol_avg'];
                un_sulphered_purity = response[0]['un_sulphured_purity'];

                sulphered_brix_avg = response[0]['sulphered_brix_avg'];
                sulphered_pol_avg = response[0]['sulphered_pol_avg'];
                sulphered_purity = response[0]['sulphered_purity'];

                oliver_brix_avg = response[0]['oliver_brix_avg'];
                oliver_pol_avg = response[0]['oliver_pol_avg'];
                oliver_purity = response[0]['oliver_purity'];

                press_cake_avg = response[0]['press_cake_avg'];
                press_cake_moisture_avg = response[0]['press_cake_moisture_avg'];
                final_mol_brix_avg = response[0]['final_mol_brix_avg'];
                final_mol_pol_avg = response[0]['final_mol_pol_avg'];
                final_mol_purity = response[0]['final_mol_purity'];
                combined_bagasse_pol = response[0]['combined_bagasse_pol_avg'];
                combined_bagasse_moisture = response[0]['combined_bagasse_moist_avg'];

                combined_pj_purity = ((combined_pj_pol * 100) / combined_pj_brix).toFixed(2);
                combined_mj_purity = ((combined_mj_pol * 100) / combined_mj_brix).toFixed(2);
                combined_lj_purity = ((combined_lj_pol * 100) / combined_lj_brix).toFixed(2);
            }
            else {
                total_jice = 0
                total_crush = 0
                combined_pj_brix = 0;
                combined_pj_pol = 0;

                combined_mj_brix = 0;
                combined_mj_pol = 0;

                combined_lj_brix = 0;
                combined_lj_pol = 0;

                clear_juice_brix_avg = 0;
                clear_juice_pol_avg = 0;
                clear_juice_purity = 0;
                un_sulphered_brix_avg = 0;
                un_sulphered_pol_avg = 0
                un_sulphered_purity = 0;

                sulphered_brix_avg = 0;
                sulphered_pol_avg = 0
                sulphered_purity = 0;

                oliver_brix_avg = 0;
                oliver_pol_avg = 0;
                oliver_purity = 0;

                press_cake_avg = 0;
                press_cake_moisture_avg = 0;

                final_mol_brix_avg = 0;
                final_mol_pol_avg = 0;
                final_mol_purity = 0;
                combined_bagasse_pol = 0;
                combined_bagasse_moisture = 0;

                combined_pj_purity = 0;
                combined_mj_purity = 0;
                combined_lj_purity = 0;
                
            }
        },
        Error: (error) => {
            alert(error);
            console.log(error);
        }
    });
    // ajax for horuly summary data
    $.ajax({
        type: "POST",
        url: "/api/HourlySummary/GetSummary",
        data: JSON.stringify(jsonParam),
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        success: (data, txtStatus, jqXHR) => {
            
            if (jqXHR.status == 200) {
                total_juice_hour = data["juice_total"];
                total_water_hour = data["water_total"];
                standing_truck = data["standing_truck"];
                standing_trippler = data["standing_trippler"];
                standing_trolley = data["standing_trolley"];
                standing_cart = data["standing_cart"];
                uncrushed_cane = data["un_crushed_cane"];
                crushed_cane = data["crushed_cane"];
                hourly_sugar_bags = data["sugar_bags_total"];
                entry_time = data["entry_time"]
            }
            else {
                total_juice_hour = 0;
                total_water_hour = 0;
                standing_truck = 0;
                standing_trippler = 0;
                standing_trolley = 0;
                standing_cart = 0;
                uncrushed_cane = 0;
                crushed_cane = 0;
                hourly_sugar_bags = 0;
                entry_time = "N/A";
            }
        },
        Error:(error) =>{
            console.log(error);
        }
    });
    // ledger data for previous day
    $.ajax({
        type: 'POST',
        url: '/api/LedgerDataApi/LedgerDataForPreviousDay',
        data: JSON.stringify(jsonParam),
        dataType: 'json',
        contentType : 'application/json; charset=utf-8',
        success: (data, txtStatus, jqXHR) => {
            console.log(data);
            if (jqXHR.status == 200) {
                cane_crushed = data['cane_crushed'];
                estimated_sugar_percent_cane = data['estimated_sugar_percent_cane'];
                total_loss = data['total_losses_percent'];
                total_sugar_bagged = data['total_sugar_bagged'];
                final_molasses_sent_out_percent = data['final_molasses_sent_out'];
                stoppage_hours = data['stoppage_hours'];
                sugar_in_process_qtl = data['sugar_in_process_qtl'];
                molasses_in_process_qtl = data['molasses_in_process_qtl'];
                evaporation_percent_clear_juice = data['EvaporationPercentClearJuice'];
                diverted_clear_juice_qtl = data['ClearJuiceDiversion'];
                diverted_clear_juice_percent_cane = diverted_clear_juice_qtl > 0 ? ((diverted_clear_juice_qtl * 100) / cane_crushed).toFixed(2) : 0;
                diverted_cane_qtl = data['DivertedCane'];
                diverted_cane_percent = (diverted_cane_qtl > 0) ?((diverted_cane_qtl * 100) / cane_crushed).toFixed(2) : 0;
                
            }
            else {
                cane_crushed = 0;
                estimated_sugar_percent_cane = 0;
                total_loss = 0;
                total_sugar_bagged = 0;
                final_molasses_sent_out_percent = 0;
                stoppage_hours = 0;
                sugar_in_process_qtl = 0;
                molasses_in_process_qtl = 0;
            }
        },
        Error: (error) => {
            console.error(error)
        }
    });

    // ajax for hourly summary for the date 
    //this will return json object containg summary for single date which is EntryDate.
    // 14-Oct-2019 02-04 am -- Ravi Bhushan
    // api --> api/HourlySummary/GetHourlyDataForDate?unit_code=1
    $.ajax({
        type: 'POST',
        url: '/api/HourlySummary/GetHourlyDataForDate',
        data: JSON.stringify(jsonParam),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: (data, txtStatus, jqXHR) => {
            if (jqXHR.status == 200) {
                total_juice_day = data["od_juice_total"];
                total_water_day = data["od_water_total"];
                total_sugar_bags = data['od_total_bags'];
                entry_date = data["entry_date"].substring(0, 10);
                today_crushed_cane = data["od_crushed"];
                today_uncrushed_cane = data["od_uncrushed"];
                todate_crushed_cane = data["td_crushed"];
                todate_uncrushed_cane = data["td_uncrushed"];
            }
            else {
                total_juice_day = 0;
                total_water_day = 0;
                total_sugar_bags = 0;
                entry_date = "NA";
                today_crushed_cane = 0;
                today_uncrushed_cane = 0;
                todate_crushed_cane = 0;
                todate_uncrushed_cane = 0;
            }
        },
        Error: (error) => {
            console.error(error)
        }
    });

    // ajax for previous day and todate summary
    $.ajax({
        type: 'POST',
        url: '/api/LedgerDataApi/GetLedgerDataToDatesAsync',
        data: JSON.stringify(jsonParam),
        dataType: 'json',
        contentType : 'application/json; charset=utf-8',
        success: (response, statusCode, jqXHR) => {
            todate_cane_crushed = response.data.cane_crushed;
            todate_recovery = response.data.estimated_suagar_percent;
            todate_losses = response.data.total_loss_percent;
            todate_sugar_bags = response.data.total_sugar_bags;
            todate_final_molasses_sentout = response.data.final_molasses_sent_out;
            todate_sugar_in_proc_qtl = response.data.sugar_in_process;
            todate_molasses_in_proc = response.data.molasses_in_process;
            evaporation_percent_clear_juice_todate = 'NA';
            diverted_clear_juice_qtl_todate = response.data.clear_juice_diversion;
            diverted_clear_juice_percent_cane_todate = diverted_clear_juice_qtl_todate > 0 ? ((diverted_clear_juice_qtl_todate * 100) / todate_cane_crushed).toFixed(2) : 0;
            diverted_cane_qtl_todate = response.data.diverted_cane;
            diverted_cane_percent_todate = (diverted_cane_qtl_todate > 0) ? ((diverted_cane_qtl_todate * 100) / todate_cane_crushed).toFixed(2) : 0;
        },
        Error: (error) => {
            console.error(error);
        }
    });

    /// ajax for Stoppage Summary previous date and current date
    $.ajax({
        type: 'POST',
        url: '/api/StoppageApi/PostStoppageSummary',
        data : JSON.stringify(jsonParam),
        datatype: 'json',
        contentType : 'application/json; charset=utf-8',
        success: (Response, statuCode, jqXHR) => {
            //console.log(Response);
            prv_day_stoppages = Math.floor(Response.TotalDuration / 60) + ":" + Math.floor(Response.TotalDuration % 60);
            todate_stoppages = Math.floor(Response.td_TotalDuration / 60) + ":" + Math.floor(Response.td_TotalDuration % 60);

            // calling assignValuesToDom() function here so that it show all values once the last api called is loaded. Otherwise it is not showing data till the next refresh.
            assignValuesToDOM(selected_unit);
        } 
    });

}

function assignValuesToDOM(selected_unit) {
    
    tab = "#tab-" + selected_unit;
    console.log("assigning dom " + tab);
    //unit_one_data(selected_unit);
    $("#EntryDate").text(entry_date);
    $("#EntryTime").text(entry_time);
    $(tab + "-total-juice-hour").text(total_juice_hour);
    $(tab + "-total-juice-day").text(total_juice_day);
    $(tab + "-total-water-hour").text(total_water_hour);
    $(tab + "-total-water-day").text(total_water_day);
    $(tab + "-total-crushing-hour").text(crushed_cane);
    $(tab + "-total-crushing-day").text(today_crushed_cane);
    $(tab + "-total-SugarBags-hour").text(hourly_sugar_bags);
    $(tab + "-total-SugarBags-day").text(total_sugar_bags);


    $(tab + "-combined_pj_brix").text(combined_pj_brix);
    $(tab + "-combined_pj_pol").text(combined_pj_pol);
    $(tab + "-combined_pj_purity").text(combined_pj_purity);
    $(tab + "-combined_mj_brix").text(combined_mj_brix);
    $(tab + "-combined_mj_purity").text(combined_mj_purity);
    $(tab + "-combined_mj_pol").text(combined_mj_pol);
    
    $(tab + "-combined_lj_brix").text(combined_lj_brix);
    $(tab + "-combined_lj_pol").text(combined_lj_pol);
    $(tab + "-combined_lj_purity").text(combined_lj_purity);
    $(tab + "-clear_juice_brix_avg").text(clear_juice_brix_avg);
    $(tab + "-clear_juice_pol_avg").text(clear_juice_pol_avg);
    $(tab + "-clear_juice_purity").text(clear_juice_purity);

    $(tab + "-un_sulphered_brix_avg").text(un_sulphered_brix_avg);
    $(tab + "-un_sulphered_pol_avg").text(un_sulphered_pol_avg);
    $(tab + "-un_sulphered_purity").text(un_sulphered_purity);

    $(tab + "-sulphered_brix_avg").text(sulphered_brix_avg);
    $(tab + "-sulphered_pol_avg").text(sulphered_pol_avg);
    $(tab + "-sulphered_purity").text(sulphered_purity);

    $(tab + "-oliver_brix_avg").text(oliver_brix_avg);
    $(tab + "-oliver_pol_avg").text(oliver_pol_avg);
    $(tab + "-oliver_purity_avg").text(oliver_purity);
    $(tab + "-press_cake_avg").text(press_cake_avg);
    $(tab + "-press_cake_moisture_avg").text(press_cake_moisture_avg);
    
    $(tab + "-final_mol_brix_avg").text(final_mol_brix_avg);
    $(tab + "-final_mol_pol_avg").text(final_mol_pol_avg);
    $(tab + "-final_mol_purity").text(final_mol_purity);
    $(tab + "-combined_bagasse_pol_avg").text(combined_bagasse_pol);
    $(tab + "-combined_bagasse_moist_avg").text(combined_bagasse_moisture);
    $(tab +"-standing_truck").text(standing_truck);
    $(tab +"-standing_trippler").text(standing_trippler);
    $(tab +"-standing_trolley").text(standing_trolley);
    $(tab +"-standing_cart").text(standing_cart);
    $(tab + "-uncrushed_cane").text(uncrushed_cane);
    $(tab + "-crushed_cane").text(crushed_cane + "/" + today_crushed_cane);
    // ledger data start
    //$(tab +"-cane_crushed").text(cane_crushed);
    //$(tab +"-estimated_sugar_percent_cane").text(estimated_sugar_percent_cane);
    //$(tab +"-total_loss").text(total_loss);
    //$(tab +"-total_sugar_bagged").text(total_sugar_bagged);
    //$(tab +"-final_molasses_sent_out_percent").text(final_molasses_sent_out_percent);
    //$(tab +"-stoppage_hours").text(stoppage_hours);
    //$(tab +"-sugar_in_process_qtl").text(sugar_in_process_qtl);
    //$(tab +"-molasses_in_process_qtl").text(molasses_in_process_qtl);
    // ledger data end

    // ledger data previous day start
    
    $("#prv-day-cane-crushed").text(cane_crushed);
    $("#prv_todate-cane-crushed").text(todate_cane_crushed);
    $("#prv-day-recovery").text(estimated_sugar_percent_cane);
    $("#prv_todate-day-recovery").text(todate_recovery);
    $("#prv-day-losess").text(total_loss);
    $("#prv_todate-losses").text(todate_losses);
    $("#prv-day-sugar-bags").text(total_sugar_bagged);
    $("#prv_todate-sugar-bags").text(todate_sugar_bags);
    $("#prv-day-final-mol-sent").text(final_molasses_sent_out_percent);
    $("#prv_todate-final-mol-sent").text(todate_final_molasses_sentout);
    $("#prv-day-stoppage").text(prv_day_stoppages);
    $("#prv_todate-stoppage").text(todate_stoppages);
    $("#prv-day-sugar-in-proc").text(sugar_in_process_qtl);
    $("#prv_todate-sugar-in-proc").text(todate_sugar_in_proc_qtl);
    $("#prv-day-mol-in-proc").text(molasses_in_process_qtl);
    $("#prv_todate-mol-in-proc").text(todate_molasses_in_proc);
    $("#prv-day-evaporation_percent_clear_juice").text(evaporation_percent_clear_juice);
    $("#prv-day-diverted_clear_juice").text(diverted_clear_juice_qtl + ' (' + diverted_clear_juice_percent_cane+'%)');
    $("#prv-day-diverted_cane_qtl").text(diverted_cane_qtl);
    $("#prv-day-diverted_cane_percent").text(diverted_cane_percent);

    $("#prv-day-evaporation_percent_clear_juice_todate").text(evaporation_percent_clear_juice_todate);
    $("#prv-day-diverted_clear_juice_todate").text(diverted_clear_juice_qtl_todate + ' (' + diverted_clear_juice_percent_cane_todate+'%)');
    $("#prv-day-diverted_cane_qtl_todate").text(diverted_cane_qtl_todate);
    $("#prv-day-diverted_cane_percent_todate").text(diverted_cane_percent_todate);
    //$("#prv-day-stoppage").text(prv_day_stoppages);

    // ledger data previous day end

    // ledger data previous day start
    //$("#prv-day-cane-crushed").text(prv_day_cane_crushed);
   // $("#prv_todate-cane-crushed").text(todate_cane_crushed)
    // ledger data previous day end
    // Hourly Data Summary for the date (sugar bags)
    //$(tab + "-total_sugar_bags").text(hourly_sugar_bags+"/"+total_sugar_bags)
}

$(document).ready(function () {
    
    executeFunctionChain();
});



function executeFunctionChain() {
    
    selected_unit = $(".dashboard-tab").attr('href').replace("#tab-", '');
    jsonParam = { 'unit_code': selected_unit };
    //assignValuesToDOM();
    //console.log(selected_unit);
    checkForCriticalAlerts();
    unit_one_data(selected_unit);


    $(".dashboard-tab").click(function () {
        selected_unit = $(this).attr('href').replace("#tab-", '');
        jsonParam = { 'unit_code': selected_unit };
        checkForCriticalAlerts();
        tab = "#tab-" + selected_unit;
        unit_one_data(selected_unit);
       // assignValuesToDOM();
    });

    setInterval(function () {
        //assignValuesToDOM();
        selected_unit = $(".dashboard-tab").filter(".active").attr('href').replace("#tab-", '');
        tab = "#tab-" + selected_unit;
        unit_one_data(selected_unit);
        assignValuesToDOM(selected_unit)
    }, 1000*60);
}