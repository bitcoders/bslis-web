
$(document).ready(function () {
    let _token = $("#UserCode").val();
    console.log(_token);
    verfiyUser(_token);
});

function verfiyUser (token) {
    $.ajax({
        method: 'POST',
        url: '/api/UserVerificationApi/VerifyUserByToken',
        data: JSON.stringify({'StringParam' : token}),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: (r) => {
            if (r.status["ErrorCode"] == 200) {
                $("#headerText").html("Success");
                $("#statusMessage").html(r.status["ErrorMessage"]);
                $("#statusMessage").removeClass('text-danger');
                $("#statusMessage").addClass('text-success');
            } else {
                $("#headerText").html("Failed!!");
                $("#statusMessage").html(r.status["ErrorMessage"]);
                $("#statusMessage").removeClass('text-success');
                $("#statusMessage").addClass('text-danger');
            }
            
        },
        error: (response) => {
            $("#statusMessage").text = response.status["ErrorMessage"]
            $("#statusMessage").removeClass('text-success');
            $("#statusMessage").addClass('text-danger');
        }
    });
    
}