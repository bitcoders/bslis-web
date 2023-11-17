
///======================== Formula================================
// pol_in_cane = pol * .72



// winter_crop_recovery = ((((brix-pol)*.54)-pol)*juice_percent)/100

// rec by mpol pty = ((((brix-pol)*.54)-pol)*.65)/100
///======================== Formula================================
let purity = 0, pol_in_cane = 0, winter_crop_recovery = 0, recovery_by_mol_and_extraction = 0, juice_percent = 0;
$(document).ready(function () {
    let d = new Date()
    let brix = 0;
    let pol = 0;
    
    //let currentDate = d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear();
    //$("#SampleDate").val(currentDate);
    //$("#AnalysisDate").val(currentDate);

    $("#Brix").change(function () {
         brix = $("#Brix").val();
        pol = $("#Pol").val();

        getPurity(brix, pol);
        getPolInCane(pol);
        getRecoveryOfWinterCrop(brix, pol);
        getRecoveryByMolassesAndExtraction(brix, pol);
    })

    $("#Pol").change(function () {
         brix = $("#Brix").val();
         pol = $("#Pol").val();
        getPurity(brix, pol);
        getPolInCane(pol, brix);
        getRecoveryOfWinterCrop(brix, pol);
        getRecoveryByMolassesAndExtraction(brix, pol);
    })
    $("#JuicePercent").on("focusout focusin keyup keydown", function (e) {
        brix = $("#Brix").val();
        pol = $("#Pol").val();
        getPurity(brix, pol);
        getPolInCane(pol, brix);
        getRecoveryOfWinterCrop(brix, pol);
        getRecoveryByMolassesAndExtraction(brix, pol);
    });

});

var getPurity = function (brix, pol) {
    
    if (pol > 0 && brix > 0) {
         purity = ((pol / brix) * 100).toFixed(2);
        
    } else{
        purity = 0;
    }
    $("#Purity").val(purity);
}

var getPolInCane = function (pol) {
    if (pol > 0) {
        pol_in_cane = (pol * .72).toFixed(2);
    }
    $("#PolInCaneToday").val(pol_in_cane);
}

var getRecoveryOfWinterCrop = function (brix, pol) {
    
    juice_percent = parseInt($("#JuicePercent").val());

    if (pol > 0 && brix > 0 && juice_percent > 0) {
        winter_crop_recovery = (((pol - ((brix - pol) * .54)) * (juice_percent / 100)).toFixed(2));
    }
    $("#RecoveryByWCapToday").val(winter_crop_recovery);
}

var getRecoveryByMolassesAndExtraction = function (brix, pol) {
    if (brix > 0 && pol > 0) {
        recovery_by_mol_and_extraction = (((pol - ((brix - pol) * .43))  *.65)).toFixed(2);
    }
    $("#RecoveryByMolPurityToday").val(recovery_by_mol_and_extraction);
}

