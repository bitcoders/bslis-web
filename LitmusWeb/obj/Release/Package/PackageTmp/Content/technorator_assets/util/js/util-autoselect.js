(function()
{
	$.fn.extend(
	{
		car_type: function()
		{
			var args = arguments;
			var options;

			$(this).each(function()
			{
				var $this = $(this);

				function init_autoselect($this)
				{
					var options = $this.data('options');

					$this.autocomplete(
					{
						source: function(request, callback)
						{
							var data =
							{
								offset : 0,
								count: 10,
								term: request.term
							};

							post_data('/panel/car_types/search' , data , function(response)
							{
								if (response.status)
								{
									var objects = response.result.result;
									callback($.map(objects, function(item)
									{
										var label = property_autoselect($this , item);
										item.label = label;
										item.value = label;
										return item;
									}));
								}
							});
						},
						minLength: 1,
						search: function(event , ui) { search_autoselect($this , event , ui); },
						select: function(event , ui) { select_autoselect($this , event , ui); },
					}).data("uiAutocomplete")._renderItem = function (ul, item)
					{
						var options = $this.data('options');
						$(ul).addClass(options['class']);
						var label = '<div class="autocomplete-title">' + $this.data('property_autoselect')($this , item) + '</div>';
						return $("<li></li>").data("item.autocomplete", item).append("<a class='autocomplete-link'>" + label + "</a>").appendTo(ul);
					};

					bind_autoselect($this);
				}
				$this.data('init_autoselect' , init_autoselect);

				function property_autoselect($this , object)
				{
					var options = $this.data('options');
					switch(options.type)
					{
						default : return object.name;
					}
				}
				$this.data('property_autoselect' , property_autoselect);

				setup_autoselect($this , args);
			});
		},

		partner: function()
		{
			var args = arguments;
			var options;

			$(this).each(function()
			{
				var $this = $(this);

				function init_autoselect($this)
				{
					var options = $this.data('options');

					$this.autocomplete(
					{
						source: function(request, callback)
						{
							var data =
							{
								offset : 0,
								count: 10,
								term: request.term
							};

							post_data('/panel/partners/search' , data , function(response)
							{
								if (response.status)
								{
									var objects = response.result.result;
									callback($.map(objects, function(item)
									{
										var label = property_autoselect($this , item);
										item.label = label;
										item.value = label;
										return item;
									}));
								}
							});
						},
						minLength: 1,
						search: function(event , ui) { search_autoselect($this , event , ui); },
						select: function(event , ui) { select_autoselect($this , event , ui); },
					}).data("uiAutocomplete")._renderItem = function (ul, item)
					{
						var options = $this.data('options');
						$(ul).addClass(options['class']);
						var label = '<div class="autocomplete-title">' + $this.data('property_autoselect')($this , item) + '</div>';
						return $("<li></li>").data("item.autocomplete", item).append("<a class='autocomplete-link'>" + label + "</a>").appendTo(ul);
					};

					bind_autoselect($this);
				}
				$this.data('init_autoselect' , init_autoselect);

				function property_autoselect($this , object)
				{
					var options = $this.data('options');
					switch(options.type)
					{
						default : return object.name;
					}
				}
				$this.data('property_autoselect' , property_autoselect);

				setup_autoselect($this , args);
			});
		},

		location: function()
		{
			var args = arguments;
			var options;

			$(this).each(function()
			{
				var $this = $(this);

				function init_autoselect($this)
				{
					var options = $this.data('options');

					$this.autocomplete(
					{
						source: function(request, callback)
						{
							$this.data('search-term', request.term);
							var data =
							{
								offset : 0,
								count: 10,
								term: request.term
							};

							post_data( options.prefix + '/locations/search' , data , function(response)
							{
								if (response.status)
								{
									var objects = response.result.result;
									callback($.map(objects, function(item)
									{
										item.label = property_autoselect($this , item , 'label');
										item.value = property_autoselect($this , item , 'value');
										return item;
									}));

									if ($.isFunction(options.on_complete))
									{
										var object = new Object();
										object['name'] = $this.val();
										options.on_complete(object);
									}
								}
							});
						},
						minLength: 1,
						search: function(event , ui) { search_autoselect($this , event , ui); },
						select: function(event , ui) { select_autoselect($this , event , ui); },
						open: function(event, ui) {
							if( $(this).hasClass('location-autocomplete') )
							{
								var offset = $(".ui-autocomplete:visible").offset();
								$(".ui-autocomplete:visible").css({
									'left': offset.left - 30,
									'width': $(this).outerWidth() + 30
								});
							}
						}
					}).data("uiAutocomplete")._renderItem = function (ul, item)
					{
						term = $this.data('search-term')
						var options = $this.data('options');
						$(ul).addClass(options['class']);

						var label = '<div class="autocomplete-title">';
						if(item.type == 'airport')
						{
							label += '<i class="fa fa-plane" aria-hidden="true"></i>';
							label += '<span class="code reg">' + item.code + '</span>';
						}
						else
						{
							label += '<i class="fa fa-building-o" aria-hidden="true"></i>';
						}

						if(item.type == 'airport')
						{
							label += '<div class="name reg">'+ item.name + "</div>, in ";
						}

						label += item.city ? '<span class="city">'+ item.city + '</span>' : '';
						label += item.state_code ? '<span class="state_code">, '+ item.state_code + '</span>' : '';

						if(isInt(term))
						{
							label += ' <span class="zipcode reg">(' + item.zipcode + ')</span>';
						}

						label += '</div>';

						return $("<li></li>").data("item.autocomplete", item).append("<a class='autocomplete-link'>" + label + "</a>").appendTo(ul);
					};

					bind_autoselect($this);
				}
				$this.data('init_autoselect' , init_autoselect);

				function property_autoselect($this , object , type)
				{
					var options = $this.data('options');
					var term = $this.data('search-term');

					if(type == 'value')
					{
						var label = '';
						if(object.type == 'airport')
						{
							label += object.code + ' ';
							label += object.name + ", in ";
						}

						label += object.city ? object.city : '';
						label += object.state_code ? ', '+ object.state_code : '';

						if(isInt(term))
						{
							label += (" " + object.zipcode);
						}
					}
					else
					{
						var label = object.name;

						if(object.type == 'airport')
						{
							label += object.city ? ', in '+ object.city : '';
							label += object.state_code ? ', '+ object.state_code : '';
						}
					}

					switch(options.type)
					{
						default : return label;
					}
				}
				$this.data('property_autoselect' , property_autoselect);

				setup_autoselect($this , args);
			});
		},

		location_api: function()
		{
			var args = arguments;
			var options;

			$(this).each(function()
			{
				var $this = $(this);

				function init_autoselect($this)
				{
					var options = $this.data('options');

					$this.autocomplete(
					{
						source: function(request, callback)
						{
							$this.data('search-term', request.term);
							var data =
							{
								offset : 0,
								count: 10,
								term: request.term
							};

							if(!(cacheautoarr[data.term] === undefined))
							{
								objects = cacheautoarr[data.term];
								callback($.map(objects, function(item)
								{
									item.label = property_autoselect($this , item , 'label');
									item.value = property_autoselect($this , item , 'value');
									return item;
								}));

								if ($.isFunction(options.on_complete))
								{
									var object = new Object();
									object['name'] = $this.val();
									options.on_complete(object);
								}
							} else {
								if(cacheautoarr.length > 9)
								{
									cacheautoarr.shift();
								}
								var init_url = 'https://api.ladybug.com';
								if(g.api_url != null){init_url = g.api_url; };

								post_data( init_url + '/autocomplete/getlocations' , data , function(response)
								{
									if (response.status)
									{
										var objects = response.result;

										cacheautoarr[request.term] = objects;

										callback($.map(objects, function(item)
										{
											item.label = property_autoselect($this , item , 'label');
											item.value = property_autoselect($this , item , 'value');
											return item;
										}));

										if ($.isFunction(options.on_complete))
										{
											var object = new Object();
											object['name'] = $this.val();
											options.on_complete(object);
										}
									}
								});
							};
						},
						minLength: 1,
						search: function(event , ui) { search_autoselect($this , event , ui); },
						select: function(event , ui) { select_autoselect($this , event , ui); },
						open: function(event, ui) {
							if( $(this).hasClass('location-autocomplete') )
							{
								var offset = $(".ui-autocomplete:visible").offset();
								$(".ui-autocomplete:visible").css({
									'left': offset.left - 30,
									'width': $(this).outerWidth() + 30
								});
							}
						}
					}).data("uiAutocomplete")._renderItem = function (ul, item)
					{
						term = $this.data('search-term')
						var options = $this.data('options');
						$(ul).addClass(options['class']);

						var label = '<div class="autocomplete-title">';
						if(item.type_id == 0)
						{
							label += '<i class="fa fa-plane" aria-hidden="true"></i>';
							label += '<span class="code reg">' + item.code + '</span>';
							label += '<span class="name reg loc_short">'+ item.name + '</span>';
						}
						else
						{
							label += '<i class="fa fa-building-o" aria-hidden="true"></i>';
							label += '<span class="name reg loc_short">'+ item.name + '</span>';
						}

						if(isInt(term) && item.zipcode)
						{
							label += ' <span class="zipcode reg">(' + item.zipcode + ')</span>';
						}

						label += '</div>';

						return $("<li></li>").data("item.autocomplete", item).append("<a class='autocomplete-link'>" + label + "</a>").appendTo(ul);
					};

					bind_autoselect($this);
				}
				$this.data('init_autoselect' , init_autoselect);

				function property_autoselect($this , object , type)
				{
					var options = $this.data('options');
					var term = $this.data('search-term');

					var label = object.name;

					if(type == 'value')
					{
						if(isInt(term) && object.zipcode)
						{
							label += (" " + object.zipcode);
						}
					}
					else
					{
						if(object.type == 'city')
						{
							//label += object.display_name ? object.display_name : '';
						}

					}

					switch(options.type)
					{
						default : return label;
					}
				}
				$this.data('property_autoselect' , property_autoselect);

				setup_autoselect($this , args);
			});
		},

		user: function()
		{
			var args = arguments;
			var options;

			$(this).each(function()
			{
				var $this = $(this);

				function init_autoselect($this)
				{
					var options = $this.data('options');

					$this.autocomplete(
					{
						source: function(request, callback)
						{
							var data =
							{
								offset : 0,
								count: 10,
								term: request.term
							};

							post_data('/panel/users/search' , data , function(response)
							{
								if (response.status)
								{
									var objects = response.result.result;
									callback($.map(objects, function(item)
									{
										var label = property_autoselect($this , item);
										item.label = label;
										item.value = label;
										return item;
									}));
								}
							});
						},
						minLength: 1,
						search: function(event , ui) { search_autoselect($this , event , ui); },
						select: function(event , ui) { select_autoselect($this , event , ui); },
					}).data("uiAutocomplete")._renderItem = function (ul, item)
					{
						var options = $this.data('options');
						$(ul).addClass(options['class']);
						var label = '<div class="autocomplete-title">' + $this.data('property_autoselect')($this , item) + '</div>';
						return $("<li></li>").data("item.autocomplete", item).append("<a class='autocomplete-link'>" + label + "</a>").appendTo(ul);
					};

					bind_autoselect($this);
				}
				$this.data('init_autoselect' , init_autoselect);

				function property_autoselect($this , object)
				{
					var options = $this.data('options');
					switch(options.type)
					{
						default : return object.first_name + " " + object.last_name;
					}
				}
				$this.data('property_autoselect' , property_autoselect);

				setup_autoselect($this , args);
			});
		},
	});
})();

function setup_autoselect($this , args)
{
	var options = $this.data('options');

	switch (args.length)
	{
		case 0:
			args[0] = {};

		case 1:
			options = $.extend(
			{
				type: 'term',
				default_role: '',
			}, args[0]);

			$this.data('options', options);
			$this.data('init_autoselect')($this);

			if(options.value)
			{
				option_autoselect($this , 'value' , options.value);
			}
			break;

		case 3:
			options = $this.data('options');
			switch (args[0])
			{
				case 'option':
					option_autoselect($this , args[1], args[2]);
					break;
			}
			break;
	}
}

function render_autoselect($this , ul, item)
{

	var options = $this.data('options');

	var _property_autoselect = $this.data('property_autoselect');

	var title = _property_autoselect($this , item);

	$(ul).addClass(options['class']);
	var label = '<div class="autocomplete-title">' + title + '</div>';
	$li = $("<li></li>").data("item.autocomplete", item).append("<a class='autocomplete-link'>" + label + "</a>").appendTo(ul);
	return $li;
}

function bind_autoselect($this)
{
	var options = $this.data('options');

	if (options.freetext)
	{
		$this.onenter(function()
		{
			var options = $this.data('options');

			if ($this.val() != "")
			{
				if ($.isFunction(options.on_select))
				{
					var object = new Object();
					object['name'] = $this.val();
					options.on_select(object);
				}

				if ($.isFunction(options.on_value))
				{
					var object = new Object();
					object['name'] = $this.val();
					options.on_value(object);
				}
			}
		});

	}

	$this.blur(function(event)
	{
		var options = $this.data('options');

		if(!options.freetext)
		{
			if($this.val() == '')
			{
				$this.data('is_valid' , false);
			}

			if (!$this.data('is_valid'))
			{
				$this.val('');
				$this.data('object', null);

				if ($.isFunction(options.on_select))
				{
					options.on_select(null);
				}

				if ($.isFunction(options.on_value))
				{
					options.on_value(null);
				}
			}
		}
		else
		{
			$this.data('is_valid' , true);
		}
	});
}

function search_autoselect($this , event, ui)
{
	var options = $this.data('options');

	$this.data('is_valid', false);
	$this.data('object', null);
}

function select_autoselect($this , event, ui)
{
	var options = $this.data('options');

	var object = ui.item;
	$this.data('is_valid', true);
	$this.data('object', object);

	if ($.isFunction(options.on_select))
	{
		options.on_select(object);
	}

	if ($.isFunction(options.on_value))
	{
		options.on_value(object);
	}
}

function option_autoselect($this , key, value)
{
	var options = $this.data('options');

	switch (key)
	{
		case 'value': value_autoselect($this , value); break;
	}
}

function value_autoselect($this , object)
{
	var options = $this.data('options');

	if (object)
	{
		$this.data('is_valid', true);
		$this.data('object', object);
		var label = $this.data('property_autoselect')($this , object);
		$this.val(label)

		if ($.isFunction(options.on_value))
		{
			options.on_value(object);
		}
	}
	else
	{
		$this.val("");
		$this.data('is_valid', false);
		$this.data('object', null);

		if ($.isFunction(options.on_value))
		{
			options.on_value(null);
		}
	}
}