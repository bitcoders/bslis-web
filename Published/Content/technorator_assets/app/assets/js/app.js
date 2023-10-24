/*----------  Global Variables  ----------*/

var w_width;
var w_height;

/*----------  Document Ready  ----------*/

$(document).ready(function()
{
	w_width = $(window).width();
	w_height = $(window).height();

	init_match_height();
	init_searchbar_handler();
	init_sitelayout_handler();
	init_user_actions_handler();
        init_contact_form();
        
        //Function replated to plan order
        change_currency_prices();
        buy_plan();
        try_plan();
        try_plan_check();        
        renew_purchased_plan();
        second_step_user_in_plan();
        stop_pause_plan();
        but_it_first_step();
        under_trial_payment();
        renew_purchased_plan_submit();
        print_order_invoice();
        
	console.log('Ready');

	if( $(".clean_up").length )
	{
		$( ".clean_up" ).keyup(function(e)
		{
			var $this = $(this);
			$this.parent('.input-group').find('.btn-clear').show();
		});

		$('.btn-clear').on('click', function(e)
		{
			var $this = $(this);
			$this.parent('.input-group').find('input').val('');
			$this.hide();
		});

		$( ".clean_up" ).on('blur', function(e)
		{
			var $this = $(this);
			window.setTimeout(function(){
				if($this.val() == '')
				{
					$this.parent('.input-group').find('.btn-clear').hide();
				}
			}, 100);
		});
		window.setTimeout(function(){
			$.each($(".clean_up"), function(index, val) {
				if( $(val).val() != '')
				{
					$(val).parent('.input-group').find('.btn-clear').show();
				}
			});
		}, 100);
	}
});

/*----------  Window Events  ----------*/

$(window).on('load resize', function()
{
	w_width = $(window).width();
	w_height = $(window).height();

	setTimeout(function()
	{
		init_match_height();
		init_sitelayout_handler();
	}, 200);
});

/*----------  Init Match Height  ----------*/

function init_match_height()
{
	$('.mh_booking_content').matchHeight();
	$('.mh_listing_block_content').matchHeight();
}

/*----------  Init Site Layout Handler  ----------*/

function init_sitelayout_handler()
{
	var $cnt_body = $('#cnt_body');
	var $cnt_header = $('#cnt_header');
	var $cnt_footer = $('#cnt_footer');
	var $cnt_content = $('#cnt_content');

	var padding_top = parseInt($cnt_header.innerHeight());
	var padding_bottom = parseInt($cnt_footer.innerHeight());

	$cnt_body.css(
	{
		height: '100%',
		minHeight: w_height + 'px',
		paddingTop: padding_top + 'px',
		paddingBottom: padding_bottom + 'px',
	});
}

/*----------  Init Search Bar Handler  ----------*/

function init_searchbar_handler()
{
	var $cnt_location_search = $('#cnt_location_search');
	var $btn_location_toggler = $('#btn_location_toggler');

	if($cnt_location_search.length)
	{
		$input = $btn_location_toggler.find('.dropoff_location_checkbox');

		if($input.is(':checked'))
		{
			handle_toggle();
		}

		$input.on('change', function()
		{
			handle_toggle();
		});
	}

	function handle_toggle()
	{
		if($input.is(':checked'))
		{
			init_sitelayout_handler();

			$cnt_location_search.addClass('dropoff-active');
		}
		else
		{
			$cnt_location_search.removeClass('dropoff-active');
		}
	}
}

/*----------  Init User Actions Handler  ----------*/

function init_user_actions_handler()
{
	var $user_atns = $('#user_atns');
	var $user_modal = $('#user_modal');
	var $user_atns_tabs = $('#user_atns_tabs');

	if($user_atns.length)
	{
		var $atn = $user_atns.find('a');

		$atn.off('click.atn').on('click.atn', function()
		{
			var tab = $(this).data('target-tab');

			$user_modal.off('show.bs.modal').on('show.bs.modal', function()
			{
				var $target_atn = $user_atns_tabs.find($("a[href='" + tab + "']"));

				$target_atn.trigger('click');
			});
		});
	}
}

function change_currency_prices()
{
    $('.currency_of_price').on('click', function()
    {
            var $this = $(this);
            var currency_id = $this.val();

            var change_currency_type_url = g.api_url + '/changecurrencyprice/'+currency_id;

            var data = 
            {
                currency_id: currency_id, 
                _token: '{{ csrf_token() }}'
            }

            post_data(change_currency_type_url, data, function(response)
            {
                if( response.status )
                {
                    if (response.result.length > 0) {
                        $.each(response.result, function(index, element) {
                            $('.planprice_div_'+element.plan_id).html(element.price_currency+' '+element.price_per_user);
                        });
                    }
                }
            });
    });
}

//Buy plan on click buy it button
function buy_plan(){
    $('.buy_it_button').click(function(event){
        event.preventDefault();
        var $this = $(this);
        var plan_id = $this.attr('data-plan');
        var ref_this = $("div.plan-type-desc a.active");
        //var plan_type_value = $('input[name=plan_type]:checked', '').val();
        var plan_type_value = ref_this.data("id");
        var currency_id_value = $('input[name=currency_id]:checked', '').val();        
        
        var buyPlanForm = jQuery('<form>', {
            'action': g.base_url+'/plancalculation',
            'method': 'post',
            'name': 'BuyPlanForm'
        }).append(jQuery('<input>', {
            'name': 'plan_id',
            'value': plan_id,
            'type': 'hidden'
        })).append(jQuery('<input>', {
            'name': 'plan_type_value',
            'value': plan_type_value,
            'type': 'hidden'
        })).append(jQuery('<input>', {
            'name': 'currency_id_value',
            'value': currency_id_value,
            'type': 'hidden'
        }));

        $(document.body).append(buyPlanForm);
        buyPlanForm.submit();
    });           
}

//Try plan on click try it button
function try_plan(){
    $('.try_it_button').click(function(event){
        event.preventDefault();
        var $this = $(this);
        var plan_id = $this.attr('data-plan');
        var plan_type_value = $('input[name=plan_type]:checked', '').val();
        var currency_id_value = $('input[name=currency_id]:checked', '').val();

        var tryPlanForm = jQuery('<form>', {
            'action': g.base_url+'/tryplancalculation',
            'method': 'post',
            'name': 'TryPlanForm'
        }).append(jQuery('<input>', {
            'name': 'plan_id',
            'value': plan_id,
            'type': 'hidden'
        })).append(jQuery('<input>', {
            'name': 'plan_type_value',
            'value': plan_type_value,
            'type': 'hidden'
        })).append(jQuery('<input>', {
            'name': 'currency_id_value',
            'value': currency_id_value,
            'type': 'hidden'
        }));

        $(document.body).append(tryPlanForm);
        tryPlanForm.submit();
    });           
}

//Try plan on click try it button
function try_plan_check(){
    
    $('.try_it_button_check').click(function(event){
        event.preventDefault();
        var $this = $(this);
        var plan_id = $this.attr('data-plan');
        //var plan_type_value = $('input[name=plan_type]:checked', '').val();
        if ($('div.plan-type-desc').length) {
            var ref_this = $("div.plan-type-desc a.active");
            var plan_type_value = ref_this.data("id");
        } else{
            var plan_type_value = 1;
        }
        //alert(plan_type_value);
        if($('#currency_id').length && $('#currency_id').val().length){
            var currency_id_value = $('input[name=currency_id]:checked', '').val();
        } else{
            var currency_id_value = 1;
        }
 
        
        //alert(currency_id_value);
        var tryPlanCheckForm = jQuery('<form>', {
            'action': g.base_url+'/tryplanprereg',
            'method': 'post',
            'name': 'TryPlanCheckForm'
        }).append(jQuery('<input>', {
            'name': 'plan_id',
            'value': plan_id,
            'type': 'hidden'
        })).append(jQuery('<input>', {
            'name': 'plan_type_value',
            'value': plan_type_value,
            'type': 'hidden'
        })).append(jQuery('<input>', {
            'name': 'currency_id_value',
            'value': currency_id_value,
            'type': 'hidden'
        }));

        $(document.body).append(tryPlanCheckForm);
        tryPlanCheckForm.submit();
    });           
}

//Buy plan after trial period on click buy it button
function under_trial_payment(){
    $('.under_trial_payment').click(function(event){
        event.preventDefault();
        var $this = $(this);
        var plan_id = $this.attr('data-plan');
        if ($('div.plan-type-desc').length) {
            var ref_this = $("div.plan-type-desc a.active");
            var plan_type_value = ref_this.data("id");
        } else{
            var plan_type_value = 1;
        }
        //alert(plan_type_value);
        if($('#currency_id').length && $('#currency_id').val().length){
            var currency_id_value = $('input[name=currency_id]:checked', '').val();
        } else{
            var currency_id_value = 1;
        }     
        
        var buyAfterTrialPlanForm = jQuery('<form>', {
            'action': g.base_url+'/plancalculation',
            'method': 'post',
            'name': 'BuyAfterTrialPlanForm'
        }).append(jQuery('<input>', {
            'name': 'plan_id',
            'value': plan_id,
            'type': 'hidden'
        })).append(jQuery('<input>', {
            'name': 'plan_type_value',
            'value': plan_type_value,
            'type': 'hidden'
        })).append(jQuery('<input>', {
            'name': 'currency_id_value',
            'value': currency_id_value,
            'type': 'hidden'
        }));

        $(document.body).append(buyAfterTrialPlanForm);
        buyAfterTrialPlanForm.submit();
    });           
}

//Renew purchased plan
function renew_purchased_plan(){
    $('.renew_plan_of_order').click(function(event){
        event.preventDefault();
        var $this = $(this);
        var plan_type_value = $('input[name=plan_type]:checked', '').val();
        var currency_id_value = $('input[name=currency_id]:checked', '').val();

        var renewPurchasedPlanForm = jQuery('<form>', {
            'action': g.base_url+'/renewinplancalculation',
            'method': 'post',
            'name': 'RenewPurchasedPlanForm'
        });

        $(document.body).append(renewPurchasedPlanForm);
        renewPurchasedPlanForm.submit();
    });           
}

//Renew purchased plan submit
function renew_purchased_plan_submit(){
    $('.but-it-first-step-renewplan').click(function(event){
        event.preventDefault();
        var $this = $(this);
        var ref_this = $("div.plan-type-desc a.active");
        //var plan_type_value = $('input[name=plan_type]:checked', '').val();
        var plan_type_value = ref_this.data("id");
        var no_of_users = $("#no_of_users").val();      
        
        var renewPurchasedPlanForm = jQuery('<form>', {
            'action': g.base_url+'/renewinplanpaymentdetails',
            'method': 'post',
            'name': 'RenewPurchasedPlanForm'
        }).append(jQuery('<input>', {
            'name': 'plan_type_value',
            'value': plan_type_value,
            'type': 'hidden'
        })).append(jQuery('<input>', {
            'name': 'no_of_users',
            'value': no_of_users,
            'type': 'hidden'
        }));     
       
        $(document.body).append(renewPurchasedPlanForm);
        renewPurchasedPlanForm.submit();
        
    });           
}

//Renew purchased plan
function second_step_user_in_plan(){
    $('.but-it-first-step').click(function(event){
        event.preventDefault();
        var $this = $(this);
        var ref_this = $("div.plan-type-desc a.active");
        //var plan_type_value = $('input[name=plan_type]:checked', '').val();
        var plan_type_value = ref_this.data("id");
        var no_of_users = $("#no_of_users").val();
        //var currency_id_value = $('input[name=currency_id]:checked', '').val();

        var buyItFirstStepPlanForm = jQuery('<form>', {
            'action': g.base_url+'/planpaymentdetails',
            'method': 'post',
            'name': 'ButItFirstStepPlanForm'
        }).append(jQuery('<input>', {
            'name': 'plan_type_value',
            'value': plan_type_value,
            'type': 'hidden'
        })).append(jQuery('<input>', {
            'name': 'no_of_users',
            'value': no_of_users,
            'type': 'hidden'
        }));

        $(document.body).append(buyItFirstStepPlanForm);
        buyItFirstStepPlanForm.submit();
    });           
}

//but it 1st step form
function but_it_first_step(){
    $('.plan_stop_status').click(function(event){
        if ($('input.plan_stop_status').is(':checked')) {

            var plan_stop_status_url = g.base_url+'/planorderstop';

            var data = 
            {
                _token: '{{ csrf_token() }}'
            }

            bconfirm('Are you sure you want to stop plan, You cant start again the plan?',
            {
                on_confirm: function()
                {
                    post_data(plan_stop_status_url, data, function(response)
                    {
                            refresh_table(false, true);
                            show_success_message('response_messages', response.message);
                    });
                }
            });     
        }                
    });       

    $('.plan_pause_status').click(function(event){
        if ($('input.plan_pause_status').is(':checked')) {

            var plan_pause_status_url = g.base_url+'/planorderpause';

            var data = 
            {
                _token: '{{ csrf_token() }}'
            }

            bconfirm('Are you sure you want to pause plan?',
            {
                on_confirm: function()
                {
                    post_data(plan_pause_status_url, data, function(response)
                    {
                            refresh_table(false, true);
                            show_success_message('response_messages', response.message);
                    });
                }
            });     
        }                
    });
}

//stop pause plan
function stop_pause_plan(){
    $('.plan_stop_status').click(function(event){
        if ($('input.plan_stop_status').is(':checked')) {

            var plan_stop_status_url = g.base_url+'/planorderstop';

            var data = 
            {
                _token: '{{ csrf_token() }}'
            }

            bconfirm('Are you sure you want to stop plan, You cant start again the plan?',
            {
                on_confirm: function()
                {
                    post_data(plan_stop_status_url, data, function(response)
                    {
                            refresh_table(false, true);
                            show_success_message('response_messages', response.message);
                    });
                }
            });     
        }                
    });       

    $('.plan_pause_status').click(function(event){
        if ($('input.plan_pause_status').is(':checked')) {

            var plan_pause_status_url = g.base_url+'/planorderpause';

            var data = 
            {
                _token: '{{ csrf_token() }}'
            }

            bconfirm('Are you sure you want to pause plan?',
            {
                on_confirm: function()
                {
                    post_data(plan_pause_status_url, data, function(response)
                    {
                            refresh_table(false, true);
                            show_success_message('response_messages', response.message);
                    });
                }
            });     
        }                
    });
}

// Contact form
function init_contact_form()
{
    var $contact_form = $('#contact_form');
    
    $contact_form.validate(
    {
        errorPlacement: function(error, element)
        {
            $(error).addClass('text-danger m-t-5');
            $(element).closest('.form-group').append($(error));
        },

        rules:
        {
            'contact_name':
            {
                required: true
            },
            
            'contact_email':
            {
                required: true,
                email: true
            },
            
            'contact_message':
            {
                required: true
            }
        },

        messages:
        {
            'contact_name':
            {
                required: 'Enter name'
            },
            
            'contact_email':
            {
                required: 'Enter email address'
            },
            
            'contact_message':
            {
                required: 'Enter message'
            }
        }
    });
}

//Print order invoice
function print_order_invoice(){
    $('.print_order_invoice').click(function(event){
                
        event.preventDefault();
        var $this = $(this);
        var print_invoice_url = $this.attr('href');
        
        $.ajax({
            url: print_invoice_url,
            type: 'POST',
            data: {_token: '{{ csrf_token() }}'},
            success: function (response) {
                $("#printcontainer").html(response);
                $("#printcontainer").show();
                var mode = 'iframe'; // popup
                var close = mode == "popup";
                var title = "";
                var options = { mode : mode, popClose : close, popTitle : title};
                $("div.printable").printArea( options );
                $("#printcontainer").html("");
            }
        });
        
        return false;
    });           
}