     
        //$('.JuiceInput').val(0);
        //$('.WaterInput').val(0);
        //$('.SugarBag').val(0);
        //$('.CoolingTraceInput').val(0);

        //$('.YardInput').val(0);
    var unitCode = $('#hiddenUnit').val();
    var NewMillJuice = 0;
    var OldMillJuice = 0;
    var totlJuice = 0;

    var NewMillWater = OldMillWater = TotalWater = 0;
    var L31 = L30 = M31 = M30 = S31 = S30 = BISS = Total_L = Total_M = Total_S =0, Raw_Sugar=0, exported_sugar = 0, TotalSugarBags = 0

    var CoolingTrace; var CoolingPol = CoolingPh = 0;
    var truck = 0, trippler = 0, trolley = 0,cart = uncrushedCane = 0;
    var noOfTruck = noOFTrippler = NoOfTrolley = noOfCart = 0;

    // loding average value from vehcile master using api
    //http://localhost:65205/api/Vehicles/1
    
function ajaxVehicleAvgWeight() {
    debugger;
    var jsonParam = { 'unit_code': unitCode };
    $.ajax({
        type: "POST",
        url: "/api/Vehicles/GetVehicleDetails",
        data: JSON.stringify(jsonParam),
        contentType: "application/json; charset=utf-8",
        datatype: 'json',
        success: (response) => {
            truck = response[0]['AverageWeight'];
            trippler = response[1]['AverageWeight'];
            trolley = response[2]['AverageWeight'];
            cart = response[3]['AverageWeight'];
        },
        error: (Error) => {
            alert(Error);
            console.error(Error);
        }
    });
}

    // if any juice input box is changed, update the value of total juice
        $('.JuiceInput').change(function () {
        NewMillJuice = $('#txtNewMillJuice').val();
        OldMillJuice = $('#txtOldMillJuice').val();
        totlJuice = parseInt(NewMillJuice) + parseInt(OldMillJuice);
        $('#txtTotalJuice').val(totlJuice);
    });

    // Water Input boxes change event
    $('.WaterInput').change(function () {
        NewMillWater = $('#txtNewMillWater').val();
        OldMillWater = $('#txtOldMillWater').val();
        TotalWater = parseFloat(NewMillWater) + parseFloat(OldMillWater);
        $('#txtTotalWater').val(TotalWater);
    });

    // Sugar Bags Input Chage Event
$('.SugarBag').change(function () {
        L31 = $('#txtBagL31').val();
        M31 = $('#txtBagM31').val();
        S31 = $('#txtBagS31').val();
        BISS = $('#txtBagBiss').val();

        L30 = $('#txtBagL30').val();
        M30 = $('#txtBagM30').val();
        S30 = $('#txtBagS30').val();
        Raw_Sugar = $("#sugar_raw").val();
        exported_sugar = $("#export_sugar").val();
        Total_L = parseFloat(L31) + parseFloat(L30);
        Total_M = parseFloat(M31) + parseFloat(M30);
        Total_S = parseFloat(S31) + parseFloat(S30);
        TotalSugarBags = parseFloat(Total_L) + parseFloat(Total_M) + parseFloat(Total_S) + parseFloat(BISS) + parseFloat(Raw_Sugar) + parseFloat(exported_sugar);

        $('#txtBagLTotal').val(Total_L);
        $('#txtBagMTotal').val(Total_M);
        $('#txtBagSTotal').val(Total_S);
        $('#txtBagTotal').val(TotalSugarBags);
    });

    // Cooling Traces Logic
    
    $('#Cooling_trace').change(function () {
        CoolingTrace = $('#Cooling_trace').val();
        $('.CoolingTraceInput').val(0);
    });


    // Yard Position
    // when user enters the number of standing vehicle, calculated the uncrushed cane based on it
    // using web api http://localhost:65205/api/Vehicles/1
    
$('.YardInput').change(() => {
    debugger;
        noOfTruck = $("#txtStandingTrucks").val();
        noOfTrippler = $("#txtStandingTrippler").val();
        noOfTrolley = $("#txtStandingTrolley").val();
        noOfCart = $("#txtStandingCart").val();
        uncrushedCane = (parseInt(noOfTruck) * truck) +
            (parseInt(noOfTrippler) * trippler) +
            (parseInt(noOfTrolley) * trolley) +
            (parseInt(noOfCart) * cart);
        $("#txtUnCrushedCane").val(uncrushedCane)
        });

var formType = $('#FormType').val();
if (formType == 'Create') {
    $("input[type='text']").val(0);
    $("input[type='number']").val(0);
}
$(document).ready(function () {
    ajaxVehicleAvgWeight();
});


(jQuery);