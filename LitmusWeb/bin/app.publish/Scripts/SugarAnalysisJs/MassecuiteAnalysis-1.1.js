$(document).ready(function () {
    var formMode = $("#FormMode").val();
    if (formMode === "Create") {

        $("input[type='text']").val(0);
        $("input[type='number']").val(0);
        var time = new Date().toTimeString().slice(0, 5)
        console.log(time);
        $("#m_entry_time").val(time);
    }
    $(".JuiceInput").change(function () {
        brix = $("#m_brix").val();
        pol = $("#m_pol").val();
        $("#m_purity").val(0);
        var purity = calculatePurity(brix, pol);
        if (!isNaN(purity)) {
            $("#m_purity").val(purity)
        }
    });
    function calculatePurity(brix, pol) {
        return (pol * 100 / brix).toFixed(2);
    }
});(jQuery);