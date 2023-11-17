
    'use strict';
let weatherTypes = [];
$(".weather").click(function () {
    weatherTypes = [];
    $(".weather").each(function () {
        console.log('into the loop');
        if (this.checked) {
            weatherTypes.push(this.value)
        }
    });
    console.log(weatherTypes);
    console.log(weatherTypes.toString())
    $("#AllWeatherConditions").val(weatherTypes.toString());
});

   