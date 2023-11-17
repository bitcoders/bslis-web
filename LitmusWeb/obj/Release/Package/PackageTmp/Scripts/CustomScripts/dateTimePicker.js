$(function () {
    $(".datetimepicker").datetimepicker({
        format: 'Y-m-d H:i',
        lang: 'en',
        step: 1
    });
    $(".datepicker").datetimepicker({
        format: 'Y-m-d',
        timepicker: false,
        lang: 'en'
    });
    $(".timepicker").datetimepicker({
        datepicker: false,
        format: 'H:i'
    });
    $(".yearpicker").datetimepicker({
        
        format: 'Y',

    });
});