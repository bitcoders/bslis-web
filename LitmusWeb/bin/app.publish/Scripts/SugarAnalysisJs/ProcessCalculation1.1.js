

$(document).ready(function () {
    validateDataBeforeFinalProcess();
    if ($("#estimated_sugar_percent_cane").length) {
        //console.log("show success message");
        showSwal('success-message');
    }

});
(function ($) {
    var recovery = 0;
    var molasses_percent_cane = 0;
    var fiber_percent_cane = 0;
    recovery = $("#estimated_sugar_percent_cane").val();
    molasses_percent_cane = $("#estimated_molasses_percent_cane").val();
    fiber_percent_cane = $("#fiber_percent_cane").val();
    showSwal = function (type) {
        'use strict';
        if (type === 'success-message') {
            swal({
                title: 'Process summary',
                text: "Estimated Sugar % Cane = " + recovery
                    + "\r\n Estimated Molasses % Cane = " + molasses_percent_cane
                    + "\r\n Fiber % Cane = " + fiber_percent_cane,
                icon: 'success',
                button: {
                    text: "Continue",
                    value: true,
                    visible: true,
                    className: "btn btn-primary"
                }
            })
        }
    }
})(jQuery);



 