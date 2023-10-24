$(document).ready(function () {

    $("input[name='UnitCheckBoxes']").change(function () {
        var units = "";
        var count = 0;
        $("input[name='UnitCheckBoxes']").each(function () {
            if ($(this).prop('checked') == true) {
                count++;
                if (count > 1) {
                    units = units + "," + $(this).val();
                }
                else {
                    units += $(this).val();
                }
            }
            //console.log(units);
        });
        $("#UnitRights").val(units);
    });

    $("input[name='DashBoardUnitCheckBoxes']").change(function () {
        var Dashunits = "";
        var count = 0;
        $("input[name='DashBoardUnitCheckBoxes']").each(function () {
            if ($(this).prop('checked') == true) {
                count++;
                if (count > 1) {
                    Dashunits = Dashunits + "," + $(this).val();
                }
                else {
                    Dashunits += $(this).val();
                }
            }
            
        });
        console.log(Dashunits);
        $("#DashboardUnits").val(Dashunits);
    });

    $("input[name='RolesCheckBox']").change(function () {
        var roles = "";
        var count = 0;
        $("input[name='RolesCheckBox']").each(function () {
            if ($(this).prop('checked') == true) {
                count++;
                if (count > 1) {
                    roles = roles + "," + $(this).val();
                }
                else {
                    roles += $(this).val();
                }
            }
            
        });
        console.log(roles);
        $("#Role").val(roles);
    });
});