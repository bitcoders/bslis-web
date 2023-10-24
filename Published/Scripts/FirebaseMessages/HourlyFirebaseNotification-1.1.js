﻿
$("#btnSave").click(function () {
        $.ajax({
        url: '/api/FireBaseNotification/SendFirebaseNotification',
    method: 'post',
            data: {
        "MessageType": 1,
    "UnitCode": $("#hiddenUnit").val(),
    "Title": $(".unitName").text() +" - Hourly Status at " +$(".entryTime").text() + "hrs",
    "Body": "Total Juice Flow = " + $("#txtTotalJuice").val()
        + "\nTotal Water = " + $("#txtTotalWater").val()
        + "\nTotal Sugar Bags = " + $("#txtBagsTotal").val()
        + " (L=" + $("#txtBagLTotal").val()
        + ", M=" + $("#txtBagMTotal").val()
        + ", S=" + $("#txtBagSTotal").val()
        + ", Raw=" + $("#sugar_raw").val() + ")"
        + "\n CRUSHING POSITON "
        + "= Crushed = " + $("#txtCrushedCane").val()
        + " Un-Crushed = " + $("#txtUnCrushedCane").val()
        + "\n YRARD POSITION "
        + "\n Trucks = " + $("#txtStandingTrucks").val()
        + ", Trolly = " + $("#txtStandingTrolley").val()
        + ", Trippler = " + $("#txtStandingTrippler").val()
        + ", Cart = "+$("#txtStandingCart").val()
       
},
        success: function (response) {
        console.log(response)
    },
            error: function (jqXHR) {
        console.log(jqXHR)
    }
    });
});