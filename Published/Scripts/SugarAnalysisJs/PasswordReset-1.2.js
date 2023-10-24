
    
    $("#btnsubmit").on('click', function () {

        let token = $("#FirstString").val();
        let password = $("#Password").val();
        let jsonParam = {
            'FirstString': token,
            'SecondString': password
        };
        let message;
        $.ajax({
            method: 'POST',
            url: "/api/RegisterUserApi/PasswordReset",
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(jsonParam),
            success: (response) => {
                console.log(response);
                if (response["ErrorCode"] == 200) {
                    message = response["ErrorMessage"];
                    showSwal('success-message', message);
                }
                else {
                    message = response["ErrorMessage"];
                    showSwal('failure-message', message);
                }
            },
            error: (response) => {
                console.log(response);
                message = response["ErrorMessage"];
                showSwal('failure-message', message);
            }
        })
    });
    
(function ($) {
    showSwal = function (type,message) {
        'use strict';
        if (type === 'success-message') {
            swal({
                title: 'Password Reset',
                text: message,
                icon: 'success',
                button: {
                    text: "Continue",
                    value: true,
                    visible: true,
                    className: "btn btn-primary"
                }
            })
            $("#passwordDiv").slideUp();
            $("#jumboHeader").html("Sucess!");
            $("#jumboHeader").addClass("text-success");
            $("#jumboLead").html("Password Changed! Login with new password!")
        }
        else {
            swal({
                title: 'Error',
                text: message,
                icon: 'error',
                button: {
                    text: "Continue",
                    value: true,
                    visible: true,
                    className: "btn btn-danger"
                }
            })
            $("#passwordDiv").slideUp();
            $("#jumboHeader").html("Failed!");
            $("#jumboHeader").addClass("text-danger");
            $("#jumboLead").html("Can't change the password. Send a new request to rest your password!")

            let forgotPasswordLink = $("<a>");
            forgotPasswordLink.attr("href", "https://bslis.com/PasswordReset/PasswordReset");
            forgotPasswordLink.attr("title", "Forgot Password");
            forgotPasswordLink.text("Forgot Password?");
            forgotPasswordLink.addClass("btn btn-primary pull-right");
            $("#divforgotpassword").append(forgotPasswordLink)
        }
    }
})(jQuery);