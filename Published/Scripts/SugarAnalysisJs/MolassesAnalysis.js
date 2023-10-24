$(document).ready(function () {
    $(".JuiceInput").change(function () {
        brix = $("#mo_brix").val();
        pol = $("#mo_pol").val();
        $("#mo_purity").val(0);
        var purity = calculatePurity(brix, pol);
        if (!isNaN(purity)) {
            $("#mo_purity").val(purity)
        }
    });
    function calculatePurity(brix, pol) {
        return (pol * 100 / brix).toFixed(2);
    }

    var formMode = $("#FormMode").val();
    if (formMode === "Create") {

        $("input[type='text']").val(0);
        $("input[type='number']").val(0);
        var time = new Date().toTimeString().slice(0, 5)
        console.log(time);
        $("#mo_entry_time").val(time);
    }
});