$(document).ready(function () {
    var initailPurity = 0;
    var nm_pj_purity, nm_mj_purity, nm_lj_purity = 0;
    var om_pj_purity, om_mj_purity, om_lj_purity = 0;
    var oliver_purity = cj_purity = unSulphured_purity = sulphured_purity = molasses_purity = 0;
    var oliver_mud = 0;

    // --- Author Ravi B. Date- 23-08-2019 00:17
    // class "{juiceType}Liquid" indicates that purity will be calculated for this
    // so are saperating such input fields by assigning a class named {JuiceType}liquid & {JuiceType}Purity.
    // e.g NmPjLiquid and OmPjPurity etc. (here NmPj & OmPj are juice types).
    var brix, pol = 0;
    $(".NmPjLiquid").change(function () {
        brix = parseFloat($("#NM_Primary_Juice_Brix").val());
        pol = parseFloat($("#NM_Primary_juice_pol").val());
        $("#nm_primary_juice_purity").val(calculatePurity(brix, pol));
    });

    $(".NmMjLiquid").change(function () {
        brix = parseFloat($("#nm_mixed_juice_brix").val());
        pol = parseFloat($("#nm_mixed_juice_pol").val());
        $("#nm_mixed_juice_purity").val(calculatePurity(brix, pol));
    });
    $(".NmLjLiquid").change(function () {
        brix = parseFloat($("#nm_last_juice_brix").val());
        pol = parseFloat($("#nm_last_juice_pol").val());
        $("#nm_last_juice_purity").val(calculatePurity(brix, pol));
    });

    //<old mill purity calculation>
    $(".OmPjLiquid").change(function () {
        brix = parseFloat($("#om_primary_juice_brix").val());
        pol = parseFloat($("#om_primary_juice_pol").val());
        $("#om_primary_juice_purity").val(calculatePurity(brix, pol));
    });
    $(".OmMjLiquid").change(function () {
        brix = parseFloat($("#om_mixed_juice_brix").val());
        pol = parseFloat($("#om_mixed_juice_pol").val());
        $("#om_mixed_juice_purity").val(calculatePurity(brix, pol));
    });
    $(".OmLjLiquid").change(function () {
        brix = parseFloat($("#om_last_juice_brix").val());
        pol = parseFloat($("#om_last_juice_pol").val());
        $("#om_last_juice_purity").val(calculatePurity(brix, pol));
    });
    //</old mill purity calculateion>

    $(".oliverLiquid").change(function () {
        brix = parseFloat($("#oliver_juice_brix").val());
        pol = parseFloat($("#oliver_juice_pol").val());
        $("#oliver_juice_purity").val(calculatePurity(brix, pol));
    });

    $(".fcsLiquid").change(function () {
        fcs_brix = parseFloat($("#fcs_juice_brix").val());
        fcs_pol = parseFloat($("#fcs_juice_pol").val());
        $("#fcs_juice_purity").val(calculatePurity(fcs_brix, fcs_pol));
    });

    $(".CjLiquid").change(function () {
        brix = parseFloat($("#clear_juice_brix").val());
        pol = parseFloat($("#clear_juice_pol").val());
        $("#clear_juice_purity").val(calculatePurity(brix, pol));
    });
    $(".UnsulphuredLiquid").change(function () {
        brix = parseFloat($("#unsulphured_syrup_brix").val());
        pol = parseFloat($("#unsulphured_syrup_pol").val());
        $("#unsulphured_syrup_purity").val(calculatePurity(brix, pol));
    });
    $(".SulphuredLiquid").change(function () {
        brix = parseFloat($("#sulphured_syrup_brix").val());
        pol = parseFloat($("#sulphured_syrup_pol").val());
        $("#sulphured_syrup_purity").val(calculatePurity(brix, pol));
    });
    $(".MolassesLiquid").change(function () {
        brix = parseFloat($("#final_molasses_brix").val());
        pol = parseFloat($("#final_molasses_pol").val());
        $("#final_molasses_purity").val(calculatePurity(brix, pol));
    });

    // SampleAverages
    
    $(".PressCakeSample").change(function () {
        var SampleId = count = sum = average = 0;
        //calCulateAverage(".PressCakeSample")
        $.each($(".PressCakeSample"), function () {
            SampleId++; // sample id are sequential so just concatenating number in the end
            if ($("#pol_pressure_cake_sample" + SampleId).val() > 0) {
                count++;
                sum = sum + parseFloat($("#pol_pressure_cake_sample" + SampleId).val());
                average = sum / count;
                $("#pol_pressure_cake_average").val(average);
            }
            if (sum === 0) {
                $("#pol_pressure_cake_average").val(0);
            }
        });
    });
});

function calculatePurity(brix, pol) {
    x = ((pol * 100) / brix).toFixed(2);
    if (isNaN(x)) {
        return 0;
    }
    else {
        return x;
    }
};
var formMode = $("#FormMode").val();
if (formMode == 'Create') {
    $("input[type='text']").val(0);
    $("input[type='number']").val(0);
}