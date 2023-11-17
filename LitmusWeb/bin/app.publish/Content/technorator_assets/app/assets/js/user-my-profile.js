(function()
{    
    $(document).ready(function()
    {
        init_renew_payment_alert();
        init_navigation_list_item();
        init_user_name_form();
        init_user_email_form();
        init_company_details_form();
        init_company_address_form();
        init_company_timezone_form();
        init_company_mobile_form();
        init_company_vatid_form();
        init_company_password_form();
        init_company_securityques_form();
        init_company_securityemail_form();
        init_company_billingmethod_form();
        
        add_more_users_in_plan();
        downgrade_user_in_plan();
        remove_downgrade();
        download_order_invoice();        
    });
        
    
}).call(this);

//Renew plan payment alert
function init_renew_payment_alert(){
    var trial_time_days_left = pln.trial_time_days_left;
    var trial_time_left_in_hours = pln.trial_time_left_in_hours;
    var trial_time_left_in_minutes = pln.trial_time_left_in_minutes;
    var trial_time_left_in_second = pln.trial_time_left_in_second;
    var plan_end_date_strtotimeval = pln.plan_end_date_strtotimeval;
    var plan_curr_date_strtotimeval = pln.plan_curr_date_strtotimeval;
    var trial_time_days_left_after = pln.trial_time_days_left_after;
    var totalAmountDue = pln.totalAmountDue;
    var currencyAmountDue = pln.currencyAmountDue;
    var payment_alert_message = "";
    
    if( plan_end_date_strtotimeval > plan_curr_date_strtotimeval ){
        if( (trial_time_days_left+1) <= 7 ){
            if( trial_time_left_in_minutes >= 1440 ){
                payment_alert_message+=trial_time_days_left+" Days Left";
            } else if( trial_time_left_in_minutes < 1440 && trial_time_left_in_minutes >= 60 ){
                payment_alert_message+=trial_time_left_in_hours+" Hours Left";
            } else if( trial_time_left_in_minutes < 60 && trial_time_left_in_second >= 60 ){
                payment_alert_message+=trial_time_left_in_minutes+" Minutes Left";
            } else if( $trial_time_left_in_second < 60 ){
                payment_alert_message+=trial_time_left_in_second+" Seconds Left";
            }
            payment_alert_message+=" to Renew the Plan, Your balance due is "+currencyAmountDue+" "+totalAmountDue;

            show_error_message('response_messages_payment_alert_users', payment_alert_message, 20000);
        }
    } else {
        if( (trial_time_days_left_after+1) <= 3 ){
            payment_alert_message+="Your balance due is";
        } else if( (trial_time_days_left_after+1) > 3 && (trial_time_days_left_after+1) <= 7 ){
            payment_alert_message+="Urgent, Please renew plan. Your balance due is";
        } else {
            if( currencyAmountDue != "" && totalAmountDue != "" ){
                payment_alert_message+="Plan expired. Purchase plan";
            } else {
                payment_alert_message+="Plan expired, Renew plan. Your balance due is";
            }
        }
        if( currencyAmountDue != "" && totalAmountDue != "" ){
            payment_alert_message+=" "+currencyAmountDue+" "+totalAmountDue;
        }

        show_error_message('response_messages_payment_alert_users', payment_alert_message, 20000);
    }
}


//navigation list item on profile page
function init_navigation_list_item(){
    /* profile page */
    var navItems = $('.admin-menu li > a');
    var navListItems = $('.admin-menu li');
    var allWells = $('.admin-content');
    var allWellsExceptFirst = $('.admin-content:not(:first)');
    allWellsExceptFirst.hide();
    navItems.click(function(e)
    {
        e.preventDefault();
        navListItems.removeClass('active');
        $(this).closest('li').addClass('active');
        allWells.hide();
        var target = $(this).attr('data-target-id');
        $('#' + target).show();
        
        if( target == "addmoreusers-plan" ){
            priceAddmoreRangeSlider();
        } else if( target == "downgrade-plan" ){
            priceDowngradeRangeSlider();
        }
    });    
}

// Organization full name form
function init_user_name_form()
{
    /* customer name edit */
    $("#edit_enable_org_name_button").click(function(e){
        e.preventDefault();
        $("#edit_ogn_name").hide();
        $("#edit_enable_ogn_name").show();
    });
    
    $("#edit_enable_cancel_ogn_name").click(function(e){
        e.preventDefault();
        $("#edit_ogn_name").show();
        $("#edit_enable_ogn_name").hide();
    });
    
    var $org_full_name_form = $('#org_full_name_form');
    
    $org_full_name_form.validate(
    {
        errorPlacement: function(error, element)
        {
            $(error).addClass('text-danger m-t-5 p-fixed');
            $(element).closest('.form-group').append($(error));
        },
        submitHandler: function(form)
        {
            var data = $(form).serializeObject();

            send_request();

            function send_request()
            {
                ioverlay('Please wait...', 'You name');
                post_data(g.base_url+'/user/myprofile/username', data, function(response)
                {
                    if(response.status)
                    {
                        ioverlay(response.message, 'plan', {status : 'success', successTimeout : 1000});

                        setTimeout(function()
                        {
                            $('#org_first_name').val(response.your_first_name);
                            $('#org_last_name').val(response.your_last_name);
                            $('.your-full-name').html(response.your_first_name+" "+response.your_last_name);
                            $("#edit_ogn_name").show();
                            $("#edit_enable_ogn_name").hide();
                        }, 1000);
                    }
                    else
                    {
                        show_error_message('response_messages' , response.message);
                        ioverlay(response.message, 'your_full_name', {status : 'error', errorTimeout : 1000});
                    }
                });
            }
        },

        rules:
        {
            'org_first_name':
            {
                    required: true
            },
            
            'org_last_name':
            {
                    required: true
            }
        },

        messages:
        {
            'org_first_name':
            {
                required: 'Enter your first name'
            },
            
            'org_last_name':
            {
                required: 'Enter your last name'
            }
        }
    });
}


// Organization email form
function init_user_email_form()
{
    /* customer email edit */
    $("#edit_enable_org_email_button").click(function(e){
        e.preventDefault();
        $("#edit_ogn_email").hide();
        $("#edit_enable_ogn_email").show();
    });
    
    $("#edit_enable_cancel_ogn_email").click(function(e){
        e.preventDefault();
        $("#edit_ogn_email").show();
        $("#edit_enable_ogn_email").hide();
    });
    
    var $org_email_form = $('#org_email_form');
    
    $org_email_form.validate(
    {
        errorPlacement: function(error, element)
        {
            $(error).addClass('text-danger m-t-5 p-fixed');
            $(element).closest('.form-group').append($(error));
        },
        submitHandler: function(form)
        {
            var data = $(form).serializeObject();

            send_request();

            function send_request()
            {
                ioverlay('Please wait...', 'You email');
                post_data(g.base_url+'/user/myprofile/useremail', data, function(response)
                {
                    if(response.status)
                    {
                        ioverlay(response.message, 'plan', {status : 'success', successTimeout : 1000});

                        setTimeout(function()
                        {
                            $('#email').val(response.your_email);
                            $('.org_email').html(response.your_email);
                            $("#edit_ogn_email").show();
                            $("#edit_enable_ogn_email").hide();
                        }, 1000);
                    }
                    else
                    {
                        show_error_message('response_messages' , response.message);
                        ioverlay(response.message, 'your_email', {status : 'error', errorTimeout : 1000});
                    }
                });
            }
        },

        rules:
        {
            'email':
            {
                required: true,
                email: true,
                remote: {
                    url: g.base_url+'/user/check_organization_email_update_available',
                    type: "post"
                }
            }
        },

        messages:
        {
            'email':
            {
                required: 'Enter email address.',
                email: "Enter a valid email address.",
                remote: "Email already in use!"
            }
        }
    });
}

// Organization details
function init_company_details_form()
{
    /* customer company details edit */
    $("#edit_enable_org_comp_details_button").click(function(e){
        e.preventDefault();
        $("#edit_comp_details").hide();
        $("#edit_enable_ogn_comp_details").show();
    });
    
    $("#edit_enable_cancel_ogn_comp_details").click(function(e){
        e.preventDefault();
        $("#edit_comp_details").show();
        $("#edit_enable_ogn_comp_details").hide();
    });
    
    var $company_details_form = $('#company_details_form');
    
    $company_details_form.validate(
    {
        errorPlacement: function(error, element)
        {
            $(error).addClass('text-danger m-t-5');
            $(element).closest('.form-group').append($(error));
        },
        submitHandler: function(form)
        {
            var data = $(form).serializeObject();

            send_request();

            function send_request()
            {
                ioverlay('Please wait...', 'Company details');
                post_data(g.base_url+'/user/myprofile/companydetails', data, function(response)
                {
                    if(response.status)
                    {
                        ioverlay(response.message, 'plan', {status : 'success', successTimeout : 1000});

                        setTimeout(function()
                        {
                            $('#adminname').val(response.company_name);
                            $('#org_website').val(response.org_website);
                            $('#org_tagline').val(response.org_tagline);
                            $('#org_description').val(response.org_description);
                            $('.org_adminname').html(response.company_name);
                            $('.org_tagline').html(response.org_tagline);
                            $("#edit_comp_details").show();
                            $("#edit_enable_ogn_comp_details").hide();
                        }, 1000);
                    }
                    else
                    {
                        show_error_message('response_messages' , response.message);
                        ioverlay(response.message, 'your_company_details', {status : 'error', errorTimeout : 1000});
                    }
                });
            }
        },

        rules:
        {
            'adminname':
            {
                required: true,
                remote: {
                    url: g.base_url+'/user/check_organization_name_update_available',
                    type: "post"
                }
            },
            'org_website':
            {
                remote: {
                    url: g.base_url+'/user/check_organization_websiteurl_update_available',
                    type: "post"
                }
            }
        },

        messages:
        {
            'adminname':
            {
                required: 'Enter company name.',
                remote: "Company name already in use!"
            },
            'org_website':
            {
                remote: "Website URL already in use!"
            }
        }
    });
}


// Organization address
function init_company_address_form()
{
    /* customer address edit */
    $("#edit_enable_org_address_button").click(function(e){
        e.preventDefault();
        $("#edit_address_details").hide();
        $("#edit_enable_ogn_address_details").show();
    });
    
    $("#edit_enable_cancel_ogn_address").click(function(e){
        e.preventDefault();
        $("#edit_address_details").show();
        $("#edit_enable_ogn_address_details").hide();
    });
    
    var $company_address_form = $('#company_address_form');
    
    $company_address_form.validate(
    {
        errorPlacement: function(error, element)
        {
            $(error).addClass('text-danger m-t-5 p-fixed');
            $(element).closest('.form-group').append($(error));
        },
        submitHandler: function(form)
        {
            var data = $(form).serializeObject();

            send_request();

            function send_request()
            {
                ioverlay('Please wait...', 'Company address');
                post_data(g.base_url+'/user/myprofile/companyaddress', data, function(response)
                {
                    if(response.status)
                    {
                        ioverlay(response.message, 'plan', {status : 'success', successTimeout : 1000});

                        setTimeout(function()
                        {
                            $('#org_name_addr1').val(response.company_address);                            
                            $('.org_address').html(response.company_address);
                            $("#edit_address_details").show();
                            $("#edit_enable_ogn_address_details").hide();
                        }, 1000);
                    }
                    else
                    {
                        show_error_message('response_messages' , response.message);
                        ioverlay(response.message, 'your_company_detailsaddress', {status : 'error', errorTimeout : 1000});
                    }
                });
            }
        },

        rules:
        {
            'org_name_addr1':
            {
                required: true
            }
        },

        messages:
        {
            'org_name_addr1':
            {
                required: 'Enter company address.'
            }
        }
    });
}


// Organization timezone
function init_company_timezone_form()
{
    /* customer time zone edit */
    $("#edit_enable_org_time_zone_button").click(function(e){
        e.preventDefault();
        $("#edit_time_zone").hide();
        $("#edit_enable_ogn_time_zone").show();
    });
    
    $("#edit_enable_cancel_ogn_time_zone").click(function(e){
        e.preventDefault();
        $("#edit_time_zone").show();
        $("#edit_enable_ogn_time_zone").hide();
    });
    
    var $company_timezone_form = $('#company_timezone_form');
    
    $company_timezone_form.validate(
    {
        errorPlacement: function(error, element)
        {
            $(error).addClass('text-danger m-t-5 p-fixed');
            $(element).closest('.form-group').append($(error));
        },
        submitHandler: function(form)
        {
            var data = $(form).serializeObject();

            send_request();

            function send_request()
            {
                ioverlay('Please wait...', 'Company timezone');
                post_data(g.base_url+'/user/myprofile/companytimezone', data, function(response)
                {
                    if(response.status)
                    {
                        ioverlay(response.message, 'plan', {status : 'success', successTimeout : 1000});

                        setTimeout(function()
                        {
                            $('#org_timezone').val(response.company_timezone);                            
                            $('.org_timezone_details').html(response.company_timezone_details);
                            $("#edit_time_zone").show();
                            $("#edit_enable_ogn_time_zone").hide();
                        }, 1000);
                    }
                    else
                    {
                        show_error_message('response_messages' , response.message);
                        ioverlay(response.message, 'your_company_timezone', {status : 'error', errorTimeout : 1000});
                    }
                });
            }
        },

        rules:
        {
            'org_timezone':
            {
                required: true
            }
        },

        messages:
        {
            'org_timezone':
            {
                required: 'Select company timezone.'
            }
        }
    });
}

// Organization mobile
function init_company_mobile_form()
{
    /* customer phone edit */
    $("#edit_enable_org_phone_button").click(function(e){
        e.preventDefault();
        $("#edit_phone").hide();
        $("#edit_enable_ogn_phone").show();
    });
    
    $("#edit_enable_cancel_ogn_phone").click(function(e){
        e.preventDefault();
        $("#edit_phone").show();
        $("#edit_enable_ogn_phone").hide();
    });
    
    
    var $company_mobile_form = $('#company_mobile_form');
    
    $company_mobile_form.validate(
    {
        errorPlacement: function(error, element)
        {
            $(error).addClass('text-danger m-t-5 p-fixed');
            $(element).closest('.form-group').append($(error));
        },
        submitHandler: function(form)
        {
            var data = $(form).serializeObject();

            send_request();

            function send_request()
            {
                ioverlay('Please wait...', 'Company mobile');
                post_data(g.base_url+'/user/myprofile/companymobile', data, function(response)
                {
                    if(response.status)
                    {
                        ioverlay(response.message, 'plan', {status : 'success', successTimeout : 1000});

                        setTimeout(function()
                        {
                            $('#telephonic_code').val(response.country_phonecode);                            
                            $('#org_mobile').val(response.company_mobile);     
                            $('.org_mobile_details').html(response.company_mobile_details);
                            $("#edit_phone").show();
                            $("#edit_enable_ogn_phone").hide();
                        }, 1000);
                    }
                    else
                    {
                        show_error_message('response_messages' , response.message);
                        ioverlay(response.message, 'your_company_mobile', {status : 'error', errorTimeout : 1000});
                    }
                });
            }
        },

        rules:
        {
            'telephonic_code':
            {
                required: true
            },
            'org_mobile':
            {
                required: true,
                phoneValidate: true,
                minlength: 9
            }
        },

        messages:
        {
            'telephonic_code':
            {
                required: 'Select phone code.'
            },
            'org_mobile':
            {
                required: 'Enter phone number.'
            }
        }
    });
}

// Organization mobile
function init_company_vatid_form()
{
    /* customer vat id edit */
    $("#edit_enable_org_vat_id_button").click(function(e){
        e.preventDefault();
        $("#edit_vat_id").hide();
        $("#edit_enable_ogn_vat_id").show();
    });
    
    $("#edit_enable_cancel_ogn_vat_id").click(function(e){
        e.preventDefault();
        $("#edit_vat_id").show();
        $("#edit_enable_ogn_vat_id").hide();
    });
    
    var $company_vatid_form = $('#company_vatid_form');
    
    $company_vatid_form.validate(
    {
        errorPlacement: function(error, element)
        {
            $(error).addClass('text-danger m-t-5 p-fixed');
            $(element).closest('.form-group').append($(error));
        },
        submitHandler: function(form)
        {
            var data = $(form).serializeObject();

            send_request();

            function send_request()
            {
                ioverlay('Please wait...', 'Company mobile');
                post_data(g.base_url+'/user/myprofile/companyvatid', data, function(response)
                {
                    if(response.status)
                    {
                        ioverlay(response.message, 'plan', {status : 'success', successTimeout : 1000});

                        setTimeout(function()
                        {
                            $('#org_vat_id').val(response.org_vat_id);                            
                            $('.org_vat_id_details').html(response.org_vat_id);
                            $("#org_vat_id_blank_details").hide();
                            $("#org_vat_id_full_details").show();
                            $("#edit_vat_id").show();
                            $("#edit_enable_ogn_vat_id").hide();
                        }, 1000);
                    }
                    else
                    {
                        show_error_message('response_messages' , response.message);
                        ioverlay(response.message, 'your_company_mobile', {status : 'error', errorTimeout : 1000});
                    }
                });
            }
        },

        rules:
        {
            'org_vat_id':
            {
                required: true,
                noSpace: true,
                alphanumeric: true,
            }
        },

        messages:
        {
            'org_vat_id':
            {
                required: 'Enter VAT.'
            }
        }
    });
}


// Organization password
function init_company_password_form()
{
    /* customer vat id edit */
    $("#edit_enable_org_password_button").click(function(e){
        e.preventDefault();
        $("#edit_ogn_password").hide();
        $("#edit_enable_ogn_password").show();
        $("#org_old_password").val("");
        $("#org_password").val("");
        $("#org_repeat_password").val("");
    });
    
    $("#edit_enable_cancel_ogn_password").click(function(e){
        e.preventDefault();
        $("#edit_ogn_password").show();
        $("#edit_enable_ogn_password").hide();
    });
    
    var $org_password_form = $('#org_password_form');
    
    $org_password_form.validate(
    {
        errorPlacement: function(error, element)
        {
            $(error).addClass('text-danger m-t-5');
            $(element).closest('.form-group').append($(error));
        },
        submitHandler: function(form)
        {
            var data = $(form).serializeObject();

            send_request();

            function send_request()
            {
                ioverlay('Please wait...', 'Company Password');
                post_data(g.base_url+'/user/myprofile/companypassword', data, function(response)
                {
                    if(response.status)
                    {
                        ioverlay(response.message, 'plan', {status : 'success', successTimeout : 1000});

                        setTimeout(function()
                        {
                            $("#edit_ogn_password").show();
                            $("#edit_enable_ogn_password").hide();
                        }, 1000);
                    }
                    else
                    {
                        show_error_message('response_messages' , response.message);
                        ioverlay(response.message, 'your_company_password', {status : 'error', errorTimeout : 1000});
                    }
                });
            }
        },

        rules:
        {
            'org_old_password':
            {
                required: true,
                minlength : 6,
                remote: {
                    url: g.base_url+'/user/check_organization_check_password_match',
                    type: "post"
                }
            },
                
            'org_password' : 
            {
                required: true,
                minlength : 6
            },

            'org_repeat_password' : 
            {
                required: true,
                minlength : 6,
                equalTo : "#org_password"
            }
        },

        messages:
        {
            'org_old_password':
            {
                required: 'Enter old password.',
                minlength : 'Should not be less than 6 char',
                remote: 'Old password does not match.'
            },
            
            'org_password':
            {
                required: 'Enter new password.',
                minlength : 'Password should not be less than 6 char',
            },

            'org_repeat_password':
            {
                required: 'Enter same confirmed new password.',
                minlength : 'Password should not be less than 6 char',
                equalTo : "New password and Confirm new password doesn't match."
            }
        }
    });
}

// Organization security question
function init_company_securityques_form()
{
    /* customer vat id edit */
    $("#edit_enable_org_securityques_button").click(function(e){
        e.preventDefault();
        $("#edit_ogn_securityques").hide();
        $("#edit_enable_ogn_securityques").show();
        $("#org_security_question_old").val("");
        $("#old_security_answer").val("");
        $("#org_security_question").val("");
        $("#new_security_question").val("");
    });
    
    $("#edit_enable_cancel_ogn_securityques").click(function(e){
        e.preventDefault();
        $("#edit_ogn_securityques").show();
        $("#edit_enable_ogn_securityques").hide();
    });
    
    var $org_security_question_form = $('#org_security_question_form');
    
    $org_security_question_form.validate(
    {
        errorPlacement: function(error, element)
        {
            $(error).addClass('text-danger m-t-5');
            $(element).closest('.form-group').append($(error));
        },
        submitHandler: function(form)
        {
            var data = $(form).serializeObject();

            send_request();

            function send_request()
            {
                ioverlay('Please wait...', 'Company security question');
                post_data(g.base_url+'/user/myprofile/companysecques', data, function(response)
                {
                    if(response.status)
                    {
                        ioverlay(response.message, 'plan', {status : 'success', successTimeout : 1000});

                        setTimeout(function()
                        {
                            $("#org_security_question_old").val("");
                            $("#old_security_answer").val("");
                            $("#org_security_question").val("");
                            $("#new_security_question").val("");
                            $("#edit_ogn_securityques").show();
                            $("#edit_enable_ogn_securityques").hide();
                        }, 1000);
                    }
                    else
                    {
                        show_error_message('response_messages' , response.message);
                        ioverlay(response.message, 'your_company_security_question', {status : 'error', errorTimeout : 1000});
                    }
                });
            }
        },

        rules:
        {
            'org_security_question_old':
            {
                required: true,
                remote: {
                    url: g.base_url+'/user/check_organization_security_question_match',
                    type: "post"
                }
            },
                
            'old_security_answer' : 
            {
                required: true,
                remote: {
                    url: g.base_url+'/user/check_organization_security_question_answer_match',
                    type: "post"
                }
            },

            'org_security_question' : 
            {
                required: true
            },

            'new_security_question' : 
            {
                required: true,
                maxlength : 100
            }
        },

        messages:
        {
            'org_security_question_old':
            {
                required: 'Select old security question.',
                remote: 'Selected old security question does not match.'
            },
            
            'old_security_answer':
            {
                required: 'Enter old security answer.',
                remote: 'Old security answer does not match.'
            },

            'org_security_question':
            {
                required: 'Select new security question.'
            },

            'new_security_question':
            {
                required: 'Enter new security answer password.',
                maxlength : 'Security answer should not be more than 100 char'
            }
        }
    });
}


// Organization security email
function init_company_securityemail_form()
{
    /* customer vat id edit */
    $("#edit_enable_org_security_email_button").click(function(e){
        e.preventDefault();
        $("#edit_ogn_security_email").hide();
        $("#edit_enable_ogn_security_email").show();
    });
    
    $("#edit_enable_cancel_ogn_security_email").click(function(e){
        e.preventDefault();
        $("#edit_ogn_security_email").show();
        $("#edit_enable_ogn_security_email").hide();
    });
    
    var $org_security_email_form = $('#org_security_email_form');
    
    $org_security_email_form.validate(
    {
        errorPlacement: function(error, element)
        {
            $(error).addClass('text-danger m-t-5');
            $(element).closest('.form-group').append($(error));
        },
        submitHandler: function(form)
        {
            var data = $(form).serializeObject();

            send_request();

            function send_request()
            {
                ioverlay('Please wait...', 'Company security email');
                post_data(g.base_url+'/user/myprofile/companysecemail', data, function(response)
                {
                    if(response.status)
                    {
                        ioverlay(response.message, 'plan', {status : 'success', successTimeout : 1000});

                        setTimeout(function()
                        {
                            $("#edit_ogn_security_email").show();
                            $("#edit_enable_ogn_security_email").hide();
                        }, 1000);
                    }
                    else
                    {
                        show_error_message('response_messages' , response.message);
                        ioverlay(response.message, 'your_company_security_email', {status : 'error', errorTimeout : 1000});
                    }
                });
            }
        },

        rules:
        {
            'org_email_con2':
            {
                required: true,
                email: true,
                remote: {
                    url: g.base_url+'/user/check_organization_corp_email_update_available',
                    type: "post"
                }
            }
        },

        messages:
        {
            'org_email_con2':
            {
                required: 'Enter security email address',
                remote: "Email already in use!"
            }
        }
    });
}


// Organization billing method
function init_company_billingmethod_form()
{
    var li_list = "";   
    
    // Delete billing method
    $('.billing_method_btn_delete').on('click', function()
    {
        var $this = $(this);
        var delete_action_url = $this.attr('href');

        var data = 
        {
            _token: '{{ csrf_token() }}'
        }

        bconfirm('Are you sure you want to delete this billing method?',
        {
            on_confirm: function()
            {
                post_data(delete_action_url, data, function(response)
                {
                    $("#ogn_billingmethod_no_"+$this.attr('data-billing')).hide();
                    show_success_message('response_messages', response.message);
                });
            }
        });

        return false;
    });
    
    //Set default billing method
    $('.updt_default_billing_method').on('click', function()
    {
            var $this = $(this);
            var update_default_billing_action_url = $this.attr('href');

            var data = 
            {
                _token: '{{ csrf_token() }}'
            }

            bconfirm('Are you sure you want to make this default billing method?',
            {
                on_confirm: function()
                {
                    post_data(update_default_billing_action_url, data, function(response)
                    {
                        show_success_message('response_messages', response.message);
                    });
                }
            });

            return false;
    });
                                
    /* customer vat id edit */
    $("#add_enable_billing_method").click(function(e){
        e.preventDefault();
        $('#add-billing').modal('hide');
        $("#form_enable_ogn_billingmethod").show();
    });
    
    $("#add_enable_cancel_ogn_billing_method").click(function(e){
        e.preventDefault();
        $("#form_enable_ogn_billingmethod").hide();
    });
    
    var $org_billing_methods_form = $('#org_billing_methods_form');
    
    $org_billing_methods_form.validate(
    {
        errorPlacement: function(error, element)
        {
            $(error).addClass('text-danger m-t-5');
            $(element).closest('.form-group').append($(error));
        },
        submitHandler: function(form)
        {
            var data = $(form).serializeObject();

            send_request();

            function send_request()
            {
                ioverlay('Please wait...', 'Company billing methods');
                post_data(g.base_url+'/user/myprofile/companybillingmethods', data, function(response)
                {
                    if(response.status)
                    {
                        ioverlay(response.message, 'plan', {status : 'success', successTimeout : 1000});

                        setTimeout(function()
                        {
                            $("#form_enable_ogn_billingmethod").hide();
                            //$("#ogn_billingmethod_no_found").hide();
                            li_list+='<li>';
                            li_list+='<div class="method-intro main-method">'+response.responseDataResult.card_type+' ending in '+response.responseDataResult.card_last_4_digit+' </div>';
                            li_list+='<div class="method-intro text-center">EUR</div>';
                            li_list+='<div class="method-intro text-right">Options';
                            li_list+='<div class="btn-group">';
                            li_list+='<button class="btn dropdown-toggle" data-toggle="dropdown">';
                            li_list+='<img src="'+g.img_url+'m-dotted.png" alt="" />';
                            li_list+='</button>';
                            li_list+='<ul class="dropdown-menu">';
                            li_list+='<li><a href="">Edit</a></li>';
                            li_list+='<li class="divider"></li>';
                            li_list+='<li><a href="">Set As Primary</a></li>';
                            li_list+='<li class="divider"></li>';
                            li_list+='<li><a href="">Remove</a></li>';
                            li_list+='</ul>';
                            li_list+='</div>';
                            li_list+='</div>';
                            li_list+='</li>';
                            
                            $(".bill_list").append(li_list);                             
                        }, 1000);
                    }
                    else
                    {
                        show_error_message('response_messages' , response.message);
                        ioverlay(response.message, 'your_company_billing_methods', {status : 'error', errorTimeout : 1500});
                    }
                });
            }
        },

        rules:
        {
            'card_first_name':
            {
                required: true
            },
            
            'card_last_name':
            {
                required: true
            },
            
            'card_number':
            {
                required: true,
                creditcard: true,
                remote: {
                    url: g.base_url+'/user/check_organization_card_number_available',
                    type: "post"
                }
            },
            
            'card_expire_month':
            {
                required: true
            },
            
            'card_expire_year':
            {
                required: true
            },
            
            'card_security_code':
            {
                required: true
            },
            
            'card_country':
            {
                required: true
            },
            
            'card_address_1':
            {
                required: true
            },
            
            'card_city':
            {
                required: true
            }
        },

        messages:
        {
            'card_first_name':
            {
                required: 'Enter first name'
            },
            
            'card_last_name':
            {
                required: 'Enter last name'
            },
            
            'card_number':
            {
                required: 'Enter card number',
                remote: 'Card number already exists!'
            },
            
            'card_expire_month':
            {
                required: 'Select expire month'
            },
            
            'card_expire_year':
            {
                required: 'Select expire year'
            },
            
            'card_security_code':
            {
                required: 'Enter security code'
            },
            
            'card_country':
            {
                required: 'Select country'
            },
            
            'card_address_1':
            {
                required: 'Enter address'
            },
            
            'card_city':
            {
                required: 'Enter city name'
            }
        }
    });
    
    // Edit billing method
    
    $("#edit_enable_cancel_ogn_billing_method").click(function(e){
        e.preventDefault();
        $("#form_enable_ogn_billingmethod_edit").hide();
        $("#org_billing_methods_edit_form.frm").trigger('reset');
    });
    
    //Get billing details for edit
    $('.edit_billing_method').on('click', function()
    {
            var $this = $(this);
            var edit_billing_action_url = $this.attr('href');
            var edit_billing_id = $this.data('id');

            var data = 
            {
                _token: '{{ csrf_token() }}'
            }

            post_data(edit_billing_action_url, data, function(response)
            {
                //show_success_message('response_messages', response.message);
                /* customer billing edit */
                $("#form_enable_ogn_billingmethod").hide();
                $("#form_enable_ogn_billingmethod_edit").show();
                
                var responseResult = response.result;
                $(".card_first_name").val(responseResult.card_first_name);
                $(".card_last_name").val(responseResult.card_last_name);
                $(".card_number").val(responseResult.card_number);
                $(".card_expire_month").val(responseResult.card_expire_month);
                $(".card_expire_year").val(responseResult.card_expire_year);
                $(".card_security_code").val(responseResult.card_security_code);
                $(".card_country").val(responseResult.card_country);
                $(".card_address_1").val(responseResult.card_address_1);
                $(".card_address_2").val(responseResult.card_address_2);
                $(".card_city").val(responseResult.card_city);
                $(".card_pin_code").val(responseResult.card_pin_code);
                $("#hdnbillingid").val(responseResult.id);
            });

            return false;
    });
    
    var $org_billing_methods_edit_form = $('#org_billing_methods_edit_form');
    
    $org_billing_methods_edit_form.validate(
    {
        errorPlacement: function(error, element)
        {
            $(error).addClass('text-danger m-t-5');
            $(element).closest('.form-group').append($(error));
        },
        submitHandler: function(form)
        {
            var data = $(form).serializeObject();

            send_request();

            function send_request()
            {
                ioverlay('Please wait...', 'Company billing methods');
                post_data(g.base_url+'/user/myprofile/editcompanybillingmethods', data, function(response)
                {
                    if(response.status)
                    {
                        ioverlay(response.message, 'plan', {status : 'success', successTimeout : 1000});
                        $("#ogn_billingmethod_no_"+response.result).html();
                        setTimeout(function()
                        {
                            $("#form_enable_ogn_billingmethod_edit").hide();
                            $("#org_billing_methods_edit_form.frm").trigger('reset');
                            //$("#ogn_billingmethod_no_found").hide();
                            li_list+='<div class="method-intro main-method">'+response.responseDataResult.card_type+' ending in '+response.responseDataResult.card_last_4_digit+' </div>';
                            li_list+='<div class="method-intro text-center">EUR</div>';
                            li_list+='<div class="method-intro text-right">Options';
                            li_list+='<div class="btn-group">';
                            li_list+='<button class="btn dropdown-toggle" data-toggle="dropdown">';
                            li_list+='<img src="'+g.img_url+'m-dotted.png" alt="" />';
                            li_list+='</button>';
                            li_list+='<ul class="dropdown-menu">';
                            li_list+='<li><a href="">Edit</a></li>';
                            li_list+='<li class="divider"></li>';
                            li_list+='<li><a href="">Set As Primary</a></li>';
                            li_list+='<li class="divider"></li>';
                            li_list+='<li><a href="">Remove</a></li>';
                            li_list+='</ul>';
                            li_list+='</div>';
                            li_list+='</div>';
                            
                            $("#ogn_billingmethod_no_"+response.result).html(li_list);        
                            window.location.href = g.base_url+'/user/myprofile';
                        }, 1000);
                    }
                    else
                    {
                        show_error_message('response_messages' , response.message);
                        ioverlay(response.message, 'your_company_billing_methods', {status : 'error', errorTimeout : 1500});
                    }
                });
            }
        },

        rules:
        {
            'card_first_name':
            {
                required: true
            },
            
            'card_last_name':
            {
                required: true
            },
            
            'card_number':
            {
                required: true,
                creditcard: true
            },
            
            'card_expire_month':
            {
                required: true
            },
            
            'card_expire_year':
            {
                required: true
            },
            
            'card_security_code':
            {
                required: true
            },
            
            'card_country':
            {
                required: true
            },
            
            'card_address_1':
            {
                required: true
            },
            
            'card_city':
            {
                required: true
            }
        },

        messages:
        {
            'card_first_name':
            {
                required: 'Enter first name'
            },
            
            'card_last_name':
            {
                required: 'Enter last name'
            },
            
            'card_number':
            {
                required: 'Enter card number',
            },
            
            'card_expire_month':
            {
                required: 'Select expire month'
            },
            
            'card_expire_year':
            {
                required: 'Select expire year'
            },
            
            'card_security_code':
            {
                required: 'Enter security code'
            },
            
            'card_country':
            {
                required: 'Select country'
            },
            
            'card_address_1':
            {
                required: 'Enter address'
            },
            
            'card_city':
            {
                required: 'Enter city name'
            }
        }
    });
}

//Add more users in current purchased plan
function add_more_users_in_plan(){
    $('#pay_for_added_more_users').click(function(event){
        event.preventDefault();
        var $this = $(this);
        var no_of_users = $(".added_more_users_in_plan").html();
        //alert(no_of_users);
        if( no_of_users > 0 ){

            var addMoreInPlanForm = jQuery('<form>', {
                'action': g.base_url+'/addmoreinplancalculation',
                'method': 'post',
                'name': 'AddMoreInPlanForm'
            }).append(jQuery('<input>', {
                'name': 'no_of_users',
                'value': no_of_users,
                'type': 'hidden'
            }));

            $(document.body).append(addMoreInPlanForm);
            addMoreInPlanForm.submit();
        } else {
            show_error_message('response_messages_add_more_users', "Add users by moving cursor forward");
        }
    });           
}

//Add downgrade in plan
function downgrade_user_in_plan(){
    $('#pay_for_downgrade_users').click(function(event){
        event.preventDefault();
        var $this = $(this);
        var plan_total_no_users = pln.plan_total_no_users;
        var total_no_users = $(".downgrade_users_in_plan").html();
        //alert(total_no_users);
        if( total_no_users <= 0 ){
            show_error_message('response_messages_downgrade_users', "Downgrade users by moving cursor forward");
        } else if( total_no_users == plan_total_no_users ){
            show_error_message('response_messages_downgrade_users', "Sorry, you can not downgrade all users");
        } else if( total_no_users > 0 ){
            var delete_action_url = g.base_url+'/downgradeplan';
            var data = 
            {
                total_no_users: total_no_users,
                _token: '{{ csrf_token() }}'
            }
            post_data(delete_action_url, data, function(response)
            {
                $("#remove_downgrade_user_in_plan").show();
                //priceDowngradeRangeSlider();
                show_success_message('response_messages_downgrade_users', response.message);
            });        
        }
    });           
}

//Remove downgrade in plan
function remove_downgrade(){
    $('#remove_downgrade_user_in_plan').click(function(event){
        
        var data = 
        {
            _token: '{{ csrf_token() }}'
        }
        var delete_action_url = g.base_url+'/deletedowngrade';
        
        bconfirm('Are you sure you want to delete this downgrade?',
        {
            on_confirm: function()
            {
                post_data(delete_action_url, data, function(response)
                {
                    $("#remove_downgrade_user_in_plan").hide();
                    $('.downgrade_users_in_plan').html(0);
                    priceDowngradeRangeSlider();
                    show_success_message('response_messages_downgrade_users', response.message);
                });
            }
        });

        return false;    
    });           
}


//Download order invoice
function download_order_invoice(){
    $('.download_order_invoice').click(function(event){
                
        event.preventDefault();
        var $this = $(this);
        var download_invoice_url = $this.attr('href');
        
        var data = 
        {
            _token: '{{ csrf_token() }}'
        }
        post_data(download_invoice_url, data, function(response)
        {
            if( response.status == false ){
                show_error_message('response_messages_purchased_plan', response.message, 20000);
            } else {
                window.open(g.base_url+'/user/myprofile/downloadinvoice/'+response.result,'_blank');
            }
        });  
        
        return false;
    });           
}