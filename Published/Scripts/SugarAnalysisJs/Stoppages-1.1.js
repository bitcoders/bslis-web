
    var headCode = 0;
    var subHeadDdl = $("#s_sub_head_code");
    var Data;
$("#s_head_code").change(function () {
        var s_name = $("#s_head_code").children("option:selected").text();
        $("#s_head_name").val(s_name);
        headCode = $(this).val();
        subHeadDdl.empty();
        if ($("#s_head_code").val() == 0) {
            subHeadDdl.attr('disabled', 'disabled');
        }
        subHeadDdl.append('<option selected="true" disabled>--Choose Sub Type--</option>');
        subHeadDdl.prop('selectedIndex');
        x();
    });
$("#s_sub_head_code").change(function () {
    var subHeadName = $(this).children("option:selected").text();
    $("#s_sub_head_name").val(subHeadName);
});
// function to get the list of Stoppage Sub-head of selected Stoppage.
var x = function () {
    var param = {'id': headCode}
        $.ajax({
            type: 'POST',
            url: '/stoppage/AjaxGetSubStoppage/',
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            success: function (Response) {
                $.each(Response["StoppageSubTypes"], function (key, val) {

                    subHeadDdl.append($('<option></option>').attr('value', val["Value"]).text(val["Text"]));
                });
            },
            error: (Error) => {
                console.log(Error);
            }
        });
};

function endStoppage() {
    var entryDateString = $("#s_date").val();
    entryDateString = entryDateString.split('-');
    // convert enttryDatestring to the jquery date
    var jDate = new Date(entryDateString[2], entryDateString[1] - 1, entryDateString[0]);


    var start_time = ConvertTime($("#s_start_time").val());
    var end_time = ConvertTime($("#s_end_time").val());
    var timeDifference = 0;
    if (end_time <= start_time) {
        // if end time is less than start time i.e. calendar date is changed so we'r required to add on day to 
        // to the entry time and then we will calculate difference
        end_time.setDate(end_time.getDate() + 1);
        //--console.log(" new date = " + end_time);
        //--console.log(DateDifference(start_time, end_time));
        timeDifference = DateDifference(start_time, end_time);
        $("#s_duration").val(timeDifference);
    }
    else {
        timeDifference = DateDifference(start_time, end_time);
        $("#s_duration").val(timeDifference);
    }
    if (isNaN(timeDifference)) {
        $("#s_duration").val(0);
        $("#btnSubmit").attr("disabled", "true");
    }
    else {
        $("#btnSubmit").removeAttr("disabled");
    }
    
    // a function which converts time in string format to jquery time format
    function ConvertTime(rawTime) {
        var from = rawTime.split(":");
        var x = new Date();
        x.setHours(from[0]);
        x.setMinutes(from[1]);
        return x;
    }
    // a function that will calculate time difference and return in minutes -- 05-09-2019 - Ravi B.
    function DateDifference(start, end) {

        var msPerHour = 1000 * 60; // miliseconds per hour
        var msDifference = end - start;
        var minutes = msDifference / msPerHour; // converting timedifference into minuts
        return minutes;
    }
}


var formName = $("#s_end_time").parents("form").attr("id");
if (formName == "frmCloseStoppage") {
        // -- date 05-09-2019 18:40 - Ravi B. @seohara
        //console.log("into close stoppage");
        $("#s_end_time").blur(function () {
            // get entry date from the page
            endStoppage();
        });
}

if (formName == "frmChangeReason") {
    headCode = $("#s_head_code").children("option:selected").val();
    x();
}
if (formname = "frmChangeStoppageTime") {
    $(document).ready(function () {
        headCode = $("#s_head_code").children("option:selected").val();
        x();
        $("#btnSubmit").click(function () {
            endStoppage();
        })
        $("#s_end_time").blur(function () {
            endStoppage();
        });
        $("#s_start_time").blur(function () {
            endStoppage();
        });
    });
    
    
}