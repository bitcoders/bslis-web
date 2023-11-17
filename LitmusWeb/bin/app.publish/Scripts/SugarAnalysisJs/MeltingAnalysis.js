$(document).ready(function () {
    var formMode = $("#FormMode").val();
    if (formMode === "Create") {

        $("input[type='text']").val(0);
        $("input[type='number']").val(0);
        var time = new Date().toTimeString().slice(0, 5)
        console.log(time);
        $("#entry_time").val(time);
    }

    $(".JuiceInput").change(function () {
        brix = $("#brix").val();
        pol = $("#pol").val();
        $("#purity").val(0);
        var purity = calculatePurity(brix, pol);
        if (!isNaN(purity)) {
            $("#purity").val(purity)
        }
    });
    function calculatePurity(brix, pol) {
        return (pol * 100 / brix).toFixed(2);
    }
});