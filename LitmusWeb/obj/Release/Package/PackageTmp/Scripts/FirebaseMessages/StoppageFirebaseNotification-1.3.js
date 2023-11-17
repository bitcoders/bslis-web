
var millName = "New Mill";
var hours = 0;
var min = 0;
$("#btnSubmit").click(function () {
    var millCode = $("#s_mill_code").text();

    switch (millCode) {
        case 0:
            millName = "New Mill";
            break;
        case 1:
            millCode = "Old Mill";
            break;
        default:
            millCode = "Old Mill";
            break;
    }
    if ($.isNumeric($("#s_duration").val())) {
        stoppageClosed();
    }
    else {
        stoppageStart();
    }

});

function stoppageStart() {
    $.ajax({
        url: '/api/FireBaseNotification/SendFirebaseNotification',
        method: 'post',
        data: {
            "MessageType": 3,
            "UnitCode": $("#hiddenUnit").val(),
            "Title": $(".unitName").text() + " - Stoppage Occured at " + millName,
            "Body": "Stoppage Occured @ " + $("#s_start_time").val()
                + " due to  => " + $("#s_comment").val()
                + "\nStoppage Department => " + $("#s_head_name").val()
                + " (" + $("#s_sub_head_name").val() + ")"
        },
        success: function (response) {
            console.log(response)
        },
        error: function (jqXHR) {
            console.log(jqXHR)
        }
    });
}

function stoppageClosed() {
    convertMinutesToHours($("#s_duration").val());
    $.ajax({

        url: '/api/FireBaseNotification/SendFirebaseNotification',
        method: 'post',
        data: {
            "MessageType": 3,
            "UnitCode": $("#hiddenUnit").val(),
            "Title": $(".unitName").text() + " - " + millName + " Stoppage Ended! ",
            "Body": millName + " started @ " + $("#s_end_time").val()
                + " Total Duration of stoppage is " + hours + " Hour " + min + " minute(s)"
                + "\nReason of Stoppage was  => " + $("#s_comment").val() + "."
                + "\nStoppage Department => " + $("#s_head_name").val()
                + " (" + $("#s_sub_head_name").val() + ")"
        },
        success: function (response) {
            console.log(response)
        },
        error: function (jqXHR) {
            console.log(jqXHR)
        }
    });
}

function convertMinutesToHours(minutes) {
    hours = (minutes / 60).toFixed(0);
    min = (minutes % 60).toFixed(0);
    //hourMinuts = hours + ":" + minutes;
}