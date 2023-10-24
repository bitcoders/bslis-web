function init_datatable($table , opts)
{
	var sNoRecords = "No records available";
	if(opts.sNoRecords)
	{
		sNoRecords = opts.sNoRecords;
	}
	
	var height = '';
	if(opts.sHeight)
	{
		height = opts.sHeight;
	}
	
	var id = $table.attr('id');
	
	var options = 
	{
		paginationType: "full_numbers",
		dom: '<"tbl-col-list-wrap"rt><"tbl-pager"p>',
		autoWidth:false,
		displayLength: 10,
		columnDefs: 
		[
			{
				sortable: false, 
			    targets: [ 0 ] 
			}
		],
		language: 
		{
			paginate: 
			{
		        first: "",
		        previous: "",
		        next: "",
		        last: ""
			},
			emptyTable : "",
			zeroRecords : "<div class='container cf'><div class='datatable-norecord'>"+ sNoRecords +"</div></div>",
			processing : "<div class='container cf'><div class='datatable-loader'></div></div>"
		},
		processing : true,
		serverSide : true,
	    ajaxSource : "",
	    columns : [],
	    sorting : []
	};
	
	/*
    fnHeaderCallback : function(){},
    fnInitCallback : function(){},
    fnServerData : function(){},
    fnRowCallback : function(){},
    drawCallback : function(){}
	/**/
	
	$.extend(options, opts);
	
	var drawCallback = options.drawCallback;
	
	options.drawCallback = function()
	{
		datatable_drawcallback($table);
		drawCallback();
	};
	
	options.fnServerParams = function()
	{
		datatable_servercallback($table);	
	};
	
	tblIdentifier = $table.dataTable(options);
	
	if(height)
	{
		$('#'+ id +'_wrapper').css('minHeight' , height);
	}
	
	return tblIdentifier;
}

function datatable_drawcallback($table)
{
	var id = $table.attr('id');
	$('#' + id + '_processing').hide().boxoverlay('success')
	$table.show();
}

function datatable_servercallback($table)
{
	var id = $table.attr('id');
	$('#' + id + '_processing').show().boxoverlay('processing');
	$table.hide();
}

function bind_table_action($table) 
{
	$('.tr' , $table).mouseenter(function() 
	{
		$(this).find('.btn-action').addClass('show');
	}).mouseleave(function() 
	{
		$('.btn-action').removeClass('show');
	});	
}

function init_total_count($cnt , total , start_index , count)
{
	$('.tbl_start' , $cnt).html(parseInt(start_index) + 1);
	$('.tbl_end' , $cnt).html(parseInt(start_index) + parseInt(count));
	$('.tbl_total' , $cnt).html(total);
	$('.dataTables_info' , $cnt).show();

	if(total == 0)
	{
		$('.dataTables_info' , $cnt).hide();
	}

	var total = total ? total : $cnt.data('total');
	$cnt.data('total' , total);
}

function init_pagination($tbl , $pgn , first_last , page_switch)
{
	if(!first_last)
	{
		first_last = false;
	}

	if(!page_switch)
	{
		page_switch = false;
	}

	var id = $tbl.attr('id');
	var $paginate = $('#'+ id +'_paginate');
	$pgn.html('<ul class="pagination-list pagination"></ul>');
	
	if($('span a' , $paginate).length == 0)
	{
		return false;
	}
	
	if(first_last)
	{	
		var firstClass = $('.first' , $paginate).hasClass('paginate_button_disabled') ? 'disabled' : '';
		$('ul.pagination-list' , $pgn).append('<li class="first"><a href="#" class="fui-triangle-left-large dev_page dev_first '+ firstClass +'" data-page="first">&laquo;</a></li>');
	}

	var previousClass = $('.previous' , $paginate).hasClass('paginate_button_disabled') ? 'disabled' : '';
	$('ul.pagination-list' , $pgn).append('<li class="previous"><a href="#" class="fui-arrow-left dev_page dev_prev '+ previousClass +'" data-page="previous">&lt;</a></li>');
	
	$('span a' , $paginate).each(function()
	{
		var activeClass = $(this).hasClass('paginate_active') ? 'active' : '';
		var page = parseInt($(this).html().replace(/\,/g,""));
		$('ul.pagination-list' , $pgn).append('<li class="' + activeClass + '"><a href="#" class="dev_page dev_no" data-page="'+ (page - 1) +'">' + page + '</a></li>');
	});

	if(page_switch)
	{
		table = $tbl.dataTable();
		var settings = table.api(settings);

		console.log(table);
		console.log(settings);

		var total = settings._recordsDisplay;
		var page_length = settings._displayLength;
		var total_pages = Math.floor(total/page_length) + 1;
		var page_gap = Math.ceil(Math.floor(total_pages / 20) / 10) * 10;		
		var switches = page_gap > 0 ? Math.floor(total_pages/page_gap) : 0;

		if(switches > 0)
		{		
			var page_switch = '';
			page_switch += '<li class="pagination-dropdown dropup">';
			page_switch += '	<i class="dropdown-arrow"></i>';
			page_switch += '	<a data-toggle="dropdown" class="dropdown-toggle" href="#fakelink">';
			page_switch += '		<i class="fui-triangle-up"></i>';
			page_switch += '	</a>';
			page_switch += '	<ul class="dropdown-menu pagination-switch">';
			for(var i = 1; i <= switches; i ++)
			{
				page_switch += '		<li><a href="" class="dev_page" data-page='+ ((i * page_gap) - 1) +'>'+ (i * page_gap) +' - '+ ((i + 1) * page_gap) +'</a></li>';	
			}
			page_switch += '	</ul>';
			page_switch += '</li>';
			$('ul.pagination-list' , $pgn).append($(page_switch));
		}
	}
		
	var nextClass = $('.next' , $paginate).hasClass('paginate_button_disabled') ? 'disabled' : '';
	$('ul.pagination-list' , $pgn).append('<li class="next"><a href="#" class="fui-arrow-right dev_page dev_next '+ nextClass +'" data-page="next">&gt;</a></li>');
	
	if(first_last)
	{
		var lastClass = $('.last' , $paginate).hasClass('paginate_button_disabled') ? 'disabled' : '';
		$('ul.pagination-list' , $pgn).append('<li class="last"><a href="#" class="fui-triangle-right-large dev_page dev_last '+ lastClass +'" data-page="last">&raquo;</a></li>');
	}


	$('.dev_page' , $pgn).click(function()
	{
		if($(this).hasClass('disabled'))
		{
			return false;		
		}
		var page = $(this).data('page');
		$(this).parents('.pagination-dropdown').removeClass('open');
		$tbl.dataTable().fnPageChange(page);
		return false;
	});	
}
function get_restaurant_types(types)
{
    var restaurant_types = '';
    for(var i = 0 ; i < types.length; i ++)
    {
        var type = get_restaurant_type(types[i]);
        restaurant_types += type;

        if(i == types.length - 1)
        {

        }
        else if(i == types.length - 2)
        {
            restaurant_types += ' & ';
        }
        else 
        {
            restaurant_types += ', ';   
        }
    }
    return restaurant_types;
}

function get_restaurant_type(type)
{
    switch(type.toString())
    {
        case "0":
            return 'Vegan';
        case "1":
            return 'Vegetarian';
        case "2":
            return 'Veg-Friendly';
        case "3":
            return 'Bio-Shop';    
	    case "4":
	        return 'Flexitarian';        
	    case "5":
	        return 'Pescatarian';         
	    case "6":
	        return 'Omnivore';         
	    case "7":
	        return 'Raw';         
	    case "8":
	        return 'Fruitarian';   
	    case "9":
            return 'Gluten-free';    
        case "10":
            return 'Desserts';              
    }
}

function get_restaurant_type_class(type)
{
	switch(type)
    {
        case "0":
            return 'type-vegan';
        case "1":
            return 'type-vegetarian';
        case "2":
            return 'type-veg-friendly';
        case "3":
            return 'type-bio-shops';          
    }
}

function get_restaurant_type_marker(type)
{
    var base_url = g['base_url']+'resources/images/';
    switch(type)
    {
        case "0":
            return base_url+'icon_map_marker_blue2.png';
        case "1":
            return base_url+'icon_map_marker_green2.png';
        case "2":
            return base_url+'icon_map_marker_peach2.png';
        case "3":
            return base_url+'icon_map_marker_pink2.png';          
    }
}


function get_restaurant_url(restaurant)
{
	return g['base_url']+restaurant['country_slug']+'/'+restaurant['city_slug']+'/'+restaurant['slug'];
}

function get_recipe_url(recipe)
{
    return g['base_url'] + 'recipes/' + recipe['slug'];
}

function get_user_url(user)
{
    return g['base_url'] + 'member/' + user['username'];   
}

function get_address(address)
{
	var addressString = '';
	
    if (address['address'])
    {
        addressString += address['address'] +', ';
    }

	if (address['city'])
	{
		addressString += address['city'];
	}
	
	if (address['state'])
	{
		addressString += ', '+address['state'];
	}
	
	if (address['country'])
	{
		addressString += ', '+address['country'];
	}

    if (address['zipcode'])
    {
        addressString += ', '+address['zipcode'];
    }
	return addressString;
}

function get_local_address(address)
{
	var addressString = '';
	
    if (address['address'])
    {
        addressString += address['address'] +', ';
    }

	if (address['city'])
	{
		addressString += address['city'] + '. ';
	}
	
	if (address['state'])
	{
		addressString += ', '+address['state'];
	}
	return addressString;
}

function get_city_address(address)
{
    var addressString = '';
    
    if (address['address'])
    {
        addressString += address['address'] +', ';
    }

    if (address['zipcode'])
    {
        addressString += address['zipcode'];
    }
    return addressString;
}

function get_recipe_source(source)
{
	switch(source)
	{
		case '0' :
			return 'AllRecipes.com';
		case '1' :
			return 'Food.com';	
		case '2' :
			return 'Food52.com';		
	}
}

$.fn.restaurant_action = function(is_saved , success , error) 
{
	var restaurant_id = $(this).data('restaurant_id');
	
	var $dev_save = $(this).find('.dev_save');		
	if(is_saved)
	{
		$dev_save.addClass('active');
	}	

	$dev_save.click(function() 
	{
		var active = !$(this).hasClass('active');
		
		if(typeof user_restaurants != "undefined")
		{
			if(active)
			{
				user_restaurants.push(restaurant_id);
			}
			else 
			{
				var index = user_restaurants.indexOf(restaurant_id);
				if (index > -1) 
				{
    				user_restaurants.splice(index, 1);
				}
			}
		}	

		$dev_save.toggleClass('active');
		post_data('user/save_restaurant' , {restaurant_id : restaurant_id , active : active} , function(response)
		{
			if(!response.status)
			{
				$dev_save.toggleClass('active');
			}
			else 
			{
				if(active)
				{
					show_popover();
				}
				else 
				{
					hide_popover();
				}
			}

		});
		return false;
	});

	var save_timer = null;
	$dev_save.mouseover(function()
	{
		if(save_timer)
			clearTimeout(save_timer);

		if($(this).hasClass('active'))
		{
			show_popover();
		}		
	}).mouseout(function()
	{
		save_timer = setTimeout(function()
		{
			hide_popover();
		} , 1000);		

		$('.popover').unbind('mouseover').mouseover(function()
		{
			if(save_timer)
				clearTimeout(save_timer);
		}).unbind('mouseleave').mouseleave(function()
		{
			save_timer = setTimeout(function()
			{
				hide_popover();
			} , 1000);	
		});
	});



	function show_popover()
	{
		if(g['user_id'] && $dev_save.data('show-popover'))
			$dev_save.popover('show');
	}

	function hide_popover()
	{
		$dev_save.popover('hide');	
	}

	if(g['user_id'])
	{
		var url = get_user_url(g['user'])+'#restaurants';
		$dev_save.popover(
		{
			title : 'SAVED!',
			content : 'You can view all your<br/> saved restaurants <a href="' + url + '">here</a>',
			html : true,
			trigger : 'manual',
			selector : $dev_save,
			container : 'body'
		});
	}	
	$(document).mouseup(function (e)
	{
		return;
		var $popover = $('.popover');
	    if ($dev_save.is(e.target) || $dev_save.has(e.target).length != 0 || $popover.is(e.target) || $popover.has(e.target).length != 0)
	    {

	    }
	    else 
	        hide_popover();
	});
};


$.fn.recipe_action = function(is_saved , success , error) 
{
	var recipe_id = $(this).data('recipe_id');
	
	var $dev_save = $(this).find('.dev_save');		
	if(is_saved)
	{
		$dev_save.addClass('active');
	}	

	$dev_save.click(function() 
	{
		var active = !$(this).hasClass('active');
		
		if(typeof user_recipes != "undefined")
		{
			if(active)
			{
				user_recipes.push(recipe_id);
			}
			else 
			{
				var index = user_recipes.indexOf(recipe_id);
				if (index > -1) 
				{
    				user_recipes.splice(index, 1);
				}
			}
		}	

		$dev_save.toggleClass('active');
		post_data('user/save_recipe' , {recipe_id : recipe_id , active : active} , function(response)
		{
			if(!response.status)
			{
				$dev_save.toggleClass('active');
			}
			else 
			{
				if(active)
				{
					show_popover();
				}
				else 
				{
					hide_popover();
				}
			}
		});
		return false;
	});

	var save_timer = null;
	$dev_save.mouseover(function()
	{
		if(save_timer)
			clearTimeout(save_timer);

		if($(this).hasClass('active'))
		{
			show_popover();
		}		
	}).mouseout(function()
	{
		save_timer = setTimeout(function()
		{
			hide_popover();
		} , 1000);		

		$('.popover').unbind('mouseover').mouseover(function()
		{
			if(save_timer)
				clearTimeout(save_timer);
		}).unbind('mouseleave').mouseleave(function()
		{
			save_timer = setTimeout(function()
			{
				hide_popover();
			} , 1000);	
		});
	});



	function show_popover()
	{
		if(g['user_id'] && $dev_save.data('show-popover'))
			$dev_save.popover('show');
	}

	function hide_popover()
	{
		$dev_save.popover('hide');	
	}

	if(g['user_id'])
	{
		var url = get_user_url(g['user'])+'#recipes';
		$dev_save.popover(
		{
			title : 'SAVED!',
			content : 'You can view all your<br/> saved recipes <a href="' + url + '">here</a>',
			html : true,
			trigger : 'manual',
			selector : $dev_save,
			container : 'body'
		});
	}	
	$(document).mouseup(function (e)
	{
		return;
		var $popover = $('.popover');
	    if ($dev_save.is(e.target) || $dev_save.has(e.target).length != 0 || $popover.is(e.target) || $popover.has(e.target).length != 0)
	    {

	    }
	    else 
	        hide_popover();
	});
};


$.fn.user_action = function(is_saved , success , error) 
{
	var user_id = $(this).data('user_id');
	
	var $dev_save = $(this).find('.dev_save');		
	if(is_saved)
	{
		$dev_save.addClass('active');
	}	

	$dev_save.click(function() 
	{
		var active = !$(this).hasClass('active');
		
		if(typeof user_followers != "undefined")
		{
			if(active)
			{
				user_followers.push(user_id);
			}
			else 
			{
				var index = user_followers.indexOf(user_id);
				if (index > -1) 
				{
    				user_followers.splice(index, 1);
				}
			}
		}	

		$dev_save.toggleClass('active');
		post_data('user/save_user' , {user_id : user_id , active : active} , function(response)
		{
			if(!response.status)
			{
				$dev_save.toggleClass('active');
			}	
			else 
			{	
				if($.isFunction(success))
				{
					success(active);
				}
			}		
		});
		return false;
	});	
};
$.fn.repeater = function(objects , options)
{
	var options = $.extend(
	{
		min_length : 1,
		reset : false
	} , options);

	$(this).each(function(index, el) 
	{
		var $this = $(this);

		if(options.reset)
		{
			$this.find('>.repeat').remove();
		}

		if(!objects || objects.length == 0)
		{
			add_repeat({} , null , 0);	
			$this.data('value' , []);
		}
		else 
		{			
			var index = 0;
			for(var i in objects) 
			{
				add_repeat(objects[i] , null , index);	
				index ++;
			}
			$this.data('value' , objects);
		}


		function add_repeat(object , $after , index)
		{
			var $repeat = $this.find('>.repeat-dom').clone();
			$repeat.removeClass('dom').removeClass('repeat-dom').addClass('repeat');

			if($after)
				$after.after($repeat);	
			else 
				$this.append($repeat);

			adjust($repeat);
			bind($repeat)

			if($.isFunction(options.on_add))
			{
				options.on_add($repeat , object , index);
			}
		}

		function remove_repeat($repeat)
		{
			if($.isFunction(options.on_remove))
			{
				options.on_remove($repeat);
			}

			$repeat.remove();
			adjust();
		}

		function bind($repeat)
		{
			$('.btn-rem', $repeat).click(function(event) 
			{
				remove_repeat($(this).closest('.repeat'));
				return false;		
			});

			$('.btn-add' , $repeat).click(function(event) 
			{
				add_repeat({} , $(this).closest('.repeat') , $(this).closest('.repeats').find('> .repeat').length);

				if($.isFunction(options.on_value))
				{
					var value = $this.repeater_objects({on_evaluate : options.on_evaluate});
					$this.data('value' , value);
					options.on_value(value);
				}

				return false;
			});
		}

		function adjust($repeat)
		{
			$repeats = $this.find('>.repeat');

			$repeats.each(function(index, el) 
			{
				$(this).children().find('.btn-rem:eq(0)').show();
			});

			if($repeats.length <= options.min_length)
			{
				$repeats.each(function(index, el) 
				{
					$(this).children().find('.btn-rem:eq(0)').hide();
				});
			}
		}

		$this.bind('trigger_value' , function()
		{
			if($.isFunction(options.on_value))
			{
				var value = $this.repeater_objects({on_evaluate : options.on_evaluate});
				$this.data('value' , value);
				options.on_value(value);
			}
		});
	});
};

$.fn.repeater_objects = function(options)
{
	var $this = $(this);

	var objects = [];
	$this.find('>.repeat').each(function(index, el) 
	{
		var object = options.on_evaluate($(this));	
		if(object != null)
			objects.push(object);	
	});

	return objects;
};

$.fn.timedropdown = function()
{
	var $this = $(this);
	for(var i = 420; i <= 1320; i += 15)
	{
        hours = Math.floor(i / 60);
        minutes = i % 60;
        if (minutes < 10)
        {
            minutes = '0' + minutes; // adding leading zero
        }
        
        ampm = hours % 24 < 12 ? 'AM' : 'PM';
        hours = hours % 12;
        
        if (hours === 0)
        {
            hours = 12;
        }
        
        $this.append($('<option></option>')
            .attr('value', i)
            .text(hours + ':' + minutes + ' ' + ampm)); 
    }
};

$.fn.rater = function(opts)
{
	var options = {
		min: 0,
		max: 5,
		step: 1,
		readonly: false,
		resetable: false,
		size: 'small',
		value: 0
	};

	$.extend(options , opts);
	
	switch(options.size)
	{
		case 'small':
			options.starheight = 16;
			options.starwidth = 16;
			break;
		case 'big':
			options.starheight = 32;
			options.starwidth = 32;
			break;	
	}
	var $this = $(this);
	$this.rateit(options);	

	if(options.size)
	{
		$this.addClass(options.size+'stars');
	}

	$this.data('value' , $(this).rateit('value')),  

	$this.bind('rated' , function()
	{
		$this.data('value' , $(this).rateit('value'));
	});

	$this.bind('reset' , function()
	{
		$this.data('value' , 0);
	});
};

$.fn.signature = function(output_id , clear_id , signature)
{
	$(this).signaturePad(
    {
        drawOnly: true,
        defaultAction: 'drawIt',
        validateFields: false,
        lineWidth: 0,
        output: output_id,
        sigNav: null,
        name: null,
        typed: null,
        clear: clear_id,
        typeIt: null,
        drawIt: null,
        typeItDesc: null,
        drawItDesc: null
    }).regenerate(signature);
}

$.validator.addMethod("greater_than", function(value, element , element2) 
{
    return $(element).val() > $(element2).val();
}, "");


$.validator.addMethod("greater_than_eq", function(value, element , element2) 
{
    return $(element).val() >= $(element2).val();
}, "");

$.validator.addMethod("rating_required", function(value, element) 
{
	return $(element).val() == 0 || $(element).val() == "" ? false : true;
}, "");

$.validator.addMethod("check_country", function(value, element){
	var country_arr = g.allowed_countries;
	if (_.contains(country_arr, value.toLowerCase())) {
		return true;  // FAIL validation when REGEX matches
	} else {
		return false;   // PASS validation otherwise
	}
}, "Our Service is not available in this Country"); 

// $.validator.addMethod("autocomplete", function(value, element) 
// {
// 	var options = $(element).data('options');
	
// 	return $(element).data('is_valid') || options.freetext || $(element).val() == "";
// }, "");

$.validator.setDefaults(
{
	errorPlacement: function(error, element) 
    {
    	element.addClass('error');
    	var error_placement = element.data('error-placement');

    	if(error_placement)
    	{
    		error.appendTo(element.closest(error_placement));
    	}
    	else if(element.attr('type') == 'radio')
    	{
        	error.appendTo(element.closest('.radio-group'));
        }
     	else if(element.attr('type') == 'checkbox')
    	{
        	error.appendTo(element.closest('.checkbox-group'));
        }
        else if(element.hasClass('rater'))
        {
        	error.appendTo(element.closest('.rating-active'));
        }
        else if(element.hasClass('spinner-input'))
        {
        	error.appendTo(element.closest('.spinner-container'));	
        }
        else 
        {
        	error.appendTo(element.parent());   
    	}
    },
    unhighlight: function(element, errorClass) 
    {    	
        var $element = $(element);        
        $element.removeClass('error');

        var error_placement = $element.data('error-placement');

    	if(error_placement)
    	{
    		$element.closest(error_placement).find('label.error').hide();
    	}
    	else if($element.attr('type') == 'radio')
    	{
    		$element.closest('.radio-group').find('label.error').hide();
        }
     	else if($element.attr('type') == 'checkbox')
    	{
        	$element.closest('.checkbox-group').find('label.error').hide();
        }
        else if($element.hasClass('rater'))
        {
        	$element.closest('.rating-active').find('label.error').hide();
        }
        else if($element.hasClass('spinner-input'))
        {
        	$element.closest('.spinner-container').find('label.error').hide();	
        }
        else 
        {
        	$element.parent().find('label.error').hide();
    	}        
    },
    resetForm: function(form) 
    {    	
    	var $form = $(form);
    	$('input' , $form).removeClass('error');
    	$('textarea' , $form).removeClass('error');
    	$('select' , $form).removeClass('error');
    }        
});

function show_element_error_message(element , error)
{
	element.parent().find('.error').remove();
	
	error = $('<label class="error">'+ error +'</label>');
	if(element.attr('type') == 'radio')
	{
    	error.appendTo(element.closest('.radio-group'));
    }
 	else if(element.attr('type') == 'checkbox')
	{
    	error.appendTo(element.closest('.checkbox-group'));
    }
    else if(element.hasClass('rater'))
    {
    	error.appendTo(element.closest('.rating-active'));
    }
    else 
    {
    	error.appendTo(element.parent());   
	}
}

jQuery.validator.addMethod("complete_url", function(val, elem) {
    // if no url, don't do anything
    if (val.length == 0) { return true; }

    // if user has not entered http:// https:// or ftp:// assume they mean http://
    if(!/^(https?|ftp):\/\//i.test(val)) {
        val = 'http://'+val; // set both the value
        $(elem).val(val); // also update the form element
    }
    // now check if valid url
    // http://docs.jquery.com/Plugins/Validation/Methods/url
    // contributed by Scott Gonzalez: http://projects.scottsplayground.com/iri/
    return /^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&amp;'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&amp;'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&amp;'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&amp;'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&amp;'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i.test(val);
});

function show_box_message($box_message_container , message)
{
	$box_message_container.addClass('box-message-container').find('.box-message').remove();

	var $box_message = $('<div class="box-message"><div class="box-message-inner"></div></div>').hide();
	$box_message_container.append($box_message);

	var $box_message_inner = $box_message.find('.box-message-inner');
	$box_message_inner.html(message);

	var height = $box_message_inner.outerHeight();

	$box_message.fadeIn();

	$box_message.click(function()
	{
		$box_message.fadeOut();
	});

	setTimeout(function()
	{
		$box_message.fadeOut();
	} , 10000);
}

$.fn.state = function($city , city_name , $state , state_name , $city_other)
{
	var $this = $(this);
	get_data('common/common/get_states' , {} , function(response)
	{
		if(response.status)
		{
			var states = response.result;

			$state.html('<option value="">-- Select State --</option>');
			for (var i = 0; i < states.length; i++) 
			{
				var state = states[i];
				$state.append('<option value="'+ state['name'] +'">'+ state['name'] +'</option>');
			}

			if(state_name)
			{
				$state.val(state_name);				
			}
			$this.city($city , city_name , state_name , $city_other);
		}
	} , null , null , null , false , true);

	$state.change(function(event) 
	{
		var state_name = $state.val();				
		$this.city($city , '' , state_name , $city_other);
	});
};

$.fn.city = function($city , city_name , state_name , $city_other)
{
	$city_other.hide();
	if(state_name)
	{	
		get_data('common/common/get_cities' , {state : state_name} , function(response)
		{
			if(response.status)
			{
				var cities = response.result;

				$city.html('<option value="">-- Select City --</option>');
				for (var i = 0; i < cities.length; i++) 
				{
					var city = cities[i];
					$city.append('<option value="'+ city['name'] +'">'+ city['name'] +'</option>');
				}
				
				if($city_other.length > 0)
				{
					$city.append('<option value="_other_">-- Other --</option>');
				}

				if(city)
				{
					$city.val(city_name);
				}
				$city.change();
			}
		} , null , null , null , false , true);
	}
	else 
	{
		$city.html('<option value="">-- Select City --</option>');
	}

	$city.change(function(event) 
	{
		var city = $city.val();		
		if($city_other.length > 0)
		{		
			if(city == '_other_' || ($city.val() == '' && city_name != "")) // if _other is selected or value can't be set in dropdown.
			{
				$city.hide();
				$city_other.show();// .focus();			
			}
			else 
			{
				$city_other.hide();
				$city.show(); //.focus();
				// $city_other.val(city);
			}
		}
	});
};

function isEmpty(obj) 
{
    for(var prop in obj) 
    {
        if(obj.hasOwnProperty(prop))
            return false;
    }

    return true;
}

$(document).ready(function() {
	$('body').on('click', function (e) {
	    $('[data-popover-trigger="click"]').each(function () {
	        //the 'is' for buttons that trigger popups
	        //the 'has' for icons within a button that triggers a popup
	        if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
	            $(this).popover('hide');
	        }
	    });
	});
});