
$(document).ready(function () {
    //setInterval(function () { x();},1000);
    x();
});
function x() {
    var back;
    var rand; 
    $.ajax({
        type: 'POST',
        url: '/api/NotificationTickerApi/PostNotificationTicker',
        dataType: "json",
        contentType : 'application/json; charset=utf-8',
        success: (Response, statusCode, jqXHR) => {
            //console.log(jqXHR.status);
            if (jqXHR.status == 200) {
                var notificationMessage = "";
                $("#marqueeContainer").show()
                $.each(Response, function (key, value) {
                    $.each(value, function (k, v) {
                        //console.log(v.DisplayText);
                        back = ["#ff0000", "blue", "gray"];
                        rand = back[Math.floor(Math.random() * back.length)];
                        notificationMessage += v.DisplayText+"&nbsp | &nbsp";
                        
                        
                    });
                });
                $("#marqueeContainer").append("<marquee class='marqueeText'>" + notificationMessage + " </marquee>");
                $(".marqueeText").css('color', rand);
            }
            else {
                $("#marqueeContainer").hide();
            }

        },
        Error: (error) => {
            console.log(error);
        }
    });
} 