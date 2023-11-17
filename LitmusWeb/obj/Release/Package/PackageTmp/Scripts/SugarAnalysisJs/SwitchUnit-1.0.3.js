var currentUser;
var jsonUnitParam;
$(document).ready(function () {
    $("#btnChangeUnit").click(function () {
        PostUserWorkingUnit();
    });
    currentUser = $("#_hiddenUserCode").val();
    jsonUnitParam = { 'StringParam': currentUser };
    $("#anchorSwitchUnit").click(function () {
        GetUnitRights();
    });
   
}); (jQuery);

var GetUnitRights = function () {
    $.ajax({
        type: 'POST',
        url: "/api/UserMasterApi/UnitRightsList",
        data: JSON.stringify(jsonUnitParam),
        contentType: "application/json; charset=utf-8",
        datatype : 'json',
        success: (Response) => {
            //console.log(Response);
            $.each(Response, function (key, value) {
                //console.log(value);
                $("#ddlAvailableUnits").empty();
                $.each(value, function (tempKey, tempValues) {
                    //$("#ddlAvailableUnits").append(new Option(tempValues.Name, tempValues.value));
                    $("#ddlAvailableUnits").append('<option class="dropdown-item" value = ' + tempValues.Code + '>' + tempValues.Name + '</option>');
                });
            });
        },
        Error: (error) => {
            console.log(error);
        }
    });
}; 

var PostUserWorkingUnit = function () {
    var formData = { 'Code': currentUser, 'UnitCode': $("#ddlAvailableUnits").children("option:selected").val()};

    $.ajax({
        type: 'POST',
        url: '/api/UserMasterApi/PostChangeWorkingUnit',
        contentType: 'application/json',
        dataType : 'json',
        data: JSON.stringify(formData),
        success: (response)=>{
            UpdateUserSession(formData.UnitCode);
            alert('Your working unit is changed to ' + $("#ddlAvailableUnits option:selected").text());
            $("#ModalChangeUnit").attr("aria-hidden", "true");
            $("#ModalChangeUnit").removeClass("show");
            $("#ModalChangeUnit").attr("style","display: none;");
            $("body").removeClass("modal-open");
            $("body").removeAttr("style");
            
        },
        error: (Error) => {
            console.log(Error);
        }
        
    });
};

var UpdateUserSession = function (WorkingUnit) {
    console.log(WorkingUnit);
    var Changedata = { 'id': WorkingUnit };
    $.ajax({
        type: 'POST',
        url: '/SessionHandler/UpdateSessionBaseUnit/',
        data: JSON.stringify(Changedata),
        contentType: 'application/json; charset=utf-8',
        dataType : 'json',
        success: (Response) => {
            console.log(Response)
        },
        failure: (response) => {
            console.log(response)
        },
        error: (response) => {
            console.log(response)
        }
    });
}