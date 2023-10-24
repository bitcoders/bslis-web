function get_status_text(status)
{
	switch(parseInt(status))
	{
		case 10 : return 'Draft';
		case 20 : return 'Active';
		case 30 : return 'Inactive';
		case 40 : return 'Submitted';
		case 50 : return 'Completed';
		case 60 : return 'Assigned';
		case 70 : return 'Cancelled';
	}
}

function get_status_class(status)
{
	switch(parseInt(status))
	{
		case 10 : return 'default';
		case 20 : return 'success';
		case 30 : return 'warning';
		case 40 : return 'primary';
		case 50 : return 'success';
		case 60 : return 'success';
		case 70 : return 'danger';
	}
}

function get_status_icon(status)
{
	switch(parseInt(status))
	{
		case 10 : return 'folder';
		case 20 : return 'check';
		case 30 : return 'times';
		case 40 : return 'check';
		case 50 : return 'check';
		case 60 : return 'check';
		case 70 : return 'times';
	}
}

function get_lock_text(lock)
{
	switch(parseInt(lock))
	{
		case 0 : return 'In Sync';
		case 1 : return 'Locked';
	}
}

function get_lock_class(lock)
{
	switch(parseInt(lock))
	{
		case 0 : return 'success';
		case 1 : return 'warning';
	}
}

function get_lock_icon(lock)
{
	switch(parseInt(lock))
	{
		case 0 : return 'refresh';
		case 1 : return 'lock';
	}
}

function get_rate_text(rate)
{
	switch(parseInt(rate))
	{
		case 10 : return 'Good';
		case 20 : return 'Normal';
		case 30 : return 'Bad';
	}
}

function get_rate_class(rate)
{
	switch(parseInt(rate))
	{
		case 10 : return 'success';
		case 20 : return 'info';
		case 30 : return 'warning';
	}
}

function get_rate_icon(rate)
{
	switch(parseInt(rate))
	{
		case 10 : return 'smile-o';
		case 20 : return 'meh-o';
		case 30 : return 'frown-o';
	}
}

function get_location_type_text(type)
{
	switch(type)
	{
		case 'airport' : return 'Airport';
		case 'location' : return 'Location';
	}
}

function get_partner_type_text(type)
{
	switch(type)
	{
		case 'external' : return 'External';
		case 'internal' : return 'Internal';
	}
}

function get_email_type_text(type)
{
	switch(type)
	{
		case 'signup' : return 'Sign Up';
		case 'forgot_password' : return 'Forgot Password';
		case 'booking_confirmation' : return 'Booking Confirmation';
		case 'booking_copy' : return 'Booking Copy';
		case 'booking_cancellation' : return 'Booking Cancellation';
		case 'new_partner' : return 'New Partner';
		case 'new_car_type' : return 'New Car Type';
		case 'default'                       : return 'Default template';
		case 'alert_missing_api_response'    : return 'Alert missing api response';
		case 'alert_missing_api_response'    : return 'Alert missing api response';
		case 'alert_missing_car_type'        : return 'Alert missing car type';
		case 'alert_missing_partner'         : return 'Alert missing partner';
		case 'alert_location_not_found'    	: return 'Alert missing location';
		case 'booking_list'         		: return 'Booking List';
		case 'booking_confirmation'    		: return 'Booking Success';
		case 'booking_cancellation'    		: return 'Booking Cancelled';
		case 'contact_form'    				: return 'Contact Form';
                case 'order_invoice'    		: return 'Order Invoice';
	}
}

function get_render_url(attachment_id)
{
	if(attachment_id)
	{
		return g.base_url + '/attachment/render/'+attachment_id;
	}
	return g.base_url + '/assets/img/default-avatar.png';
}

function get_download_url(attachment_id)
{
	return g.base_url + '/attachment/download/'+attachment_id;
}

function get_alert_url(url)
{
	if(!url)
	{
		return '#';
	}

	if (url.indexOf("http://")==0 || url.indexOf("https://")==0)
	{
		return url;
	}
	else
	{
		return g.base_url+url;
	}
}

function event_logger(event_type, event_data, callback)
{
	var event_url = g.base_url + '/event/log';
	var data = {
		type: event_type,
		data: event_data
	};

	post_data(event_url, data , function(response){
		callback(response);
	});
}

jQuery(document).ready(function($) {
	$(".event_logger").on('click', function(e){
		e.preventDefault();
		var $this = $(this);
		var redirect_url = $(this).attr('href');
		var event_type = $this.data('event_type');
		var event_data = $this.data('event_data');
		var event_redirect = $this.data('event_redirect');

		event_logger(event_type, event_data, function(response)
		{
			if(event_redirect)
			{
				window.location = redirect_url;
			}
		});
		// return false;
	});
});

function isInt(value) {
  var x;
  return isNaN(value) ? !1 : (x = parseFloat(value), (0 | x) === x);
}

function show_price(number, decimals)
{
	decimals = typeof decimals !== 'undefined' ? decimals : 2;
	return parseFloat(number).toFixed(decimals);
}

function getParameterByName(name, url) {
	if (!url) {
	  url = window.location.href;
	}
	name = name.replace(/[\[\]]/g, "\\$&");
	var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
		results = regex.exec(url);
	if (!results) return null;
	if (!results[2]) return '';
	return decodeURIComponent(results[2].replace(/\+/g, " "));
}