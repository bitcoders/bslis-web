new WOW().init();

(function()
{
	/*----------  Init Other Functions  ----------*/

	$(document).ready(function()
	{
		fn_toggle();
		fn_tablet_menu();
		fn_tablet_menuOutSide();
		fn_trigger();
		fn_placeholdit();
		fn_smooth_scroll();
                fn_windowheightinit();
	});
	
	$(window).resize(function()
	{
		$('.tab-menu-opt').removeClass('open');
	});


	$(window).on('change load', function()
	{
		
	});

	/*----------  Toggle Function  ----------*/

	function fn_toggle()
	{
		var target;

		var $toggler = $('[data-fn-toggle]');

		if ($toggler.length > 0)
		{
			$toggler.off('click.toggle').on('click.toggle', function()
			{
				target = $(this).data('fn-target');

				if ($(target).length > 0)
				{
					if ($(target).is(':visible'))
					{
						$(target).slideUp('fast');
						$(this).removeClass('open');
					}
					else
					{
						$(target).slideDown('fast');
						$(this).addClass('open');
					}
				}

				return false;
			});
		}
	}
	
	
	/*----------  Tablet Menu  ----------*/
	function fn_tablet_menu()
	{
		$('.tab-toggle').on('click', function(){
			var winWidth = $(window).width();
			 
			if(winWidth < 1024 && winWidth > 767){ 
				$('.tab-menu-list').toggleClass('in');
				$('.tab-outsite').toggleClass('in');
			}else{
				$('.tab-menu-list').removeClass('in');
				$('.tab-outsite').removeClass('in');
			}
		});
	}
	function fn_tablet_menuOutSide()
	{
		$('.tab-outsite').on('click', function(){
			$('.tab-menu-list').removeClass('in');
			$('.tab-outsite').removeClass('in');
		});
	}
	
	/*----------  Smooth Scroll  ----------*/

	function fn_smooth_scroll()
	{
		var section;
		var $root = $('html, body');
		var $smooth = $('.smooth');

		if ($smooth.length > 0)
		{
			$smooth.off('click.smooth').on('click.smooth', function()
			{
				section = $.attr(this, 'href');
				
				if ($(section).length > 0)
				{
					$root.animate(
					{
						scrollTop: $(section).offset().top
					}, 1000, function ()
					{
						console.log('Reached to: ' + section);
					});
				}

				return false;
			});
		}
	}

	/*----------  Trigger  ----------*/
	
	function fn_trigger()
	{
		var $btn_trigger = $('[data-fn-trigger]');

		if($btn_trigger.length)
		{
			$btn_trigger.on('click', function()
			{
				var btn = $(this).data('fn-trigger');

				$(btn).trigger('click')
			});
		}
	}

	/*----------  Placeholdit  ----------*/

	function fn_placeholdit()
	{
		/* <img data-placeholdit data-ph-dimension="" data-ph-text="" data-ph-txtcolor="" data-ph-bgcolor="" src="" class="img-responsive"/> */

		var placeholdit = $('[data-placeholdit], .placeholdit');

		if ($(placeholdit).length > 0)
		{
			var url = 'https://placehold.it';

			$(placeholdit).each(function()
			{
				var dimension = $(this).data('ph-dimension');
				var text = $(this).data('ph-text');
				var txt_color = $(this).data('ph-txtcolor');
				var bg_color = $(this).data('ph-bgcolor');
				var ex_class = $(this).data('ph-class');

				if ((dimension == '') || (dimension == null))
				{
					dimension = '500x500';
				}

				if ((bg_color == '') || (bg_color == null))
				{
					bg_color = 'aaa';
				}

				if ((txt_color == '') || (txt_color == null))
				{
					txt_color = 'f5f5f5';
				}

				if ((text == '') || (text == null))
				{
					text = 'Placeholder';
				}

				var src = url + '/' + dimension + '/' + bg_color + '/' + txt_color + '/?text=' + text;

				if ($(this).is('img'))
				{
					$(this).attr('src', src).addClass(ex_class);
				}
				else
				{
					var img = '<img src="' + src + '" alt="' + text + '" class="img-responsive ' + ex_class + '"/>';
					$(this).hide().after(img);
				}
			});
		}
	};
                
        $(".monthly-payment").show();
        $(".yearly-prices").hide();
        $('.btn-try').addClass('active');
        
        $('#monthly-payment-home-euro').click(function (e) {
            var iso_code = "eur";
            $(".monthly-prices-"+iso_code).show();
            $(".yearly-prices-"+iso_code).hide();            
            $('.btn-buy.active').removeClass('active');
            $(this).addClass('active');
        });
        $('#yearly-payment-home-euro').click(function (e) {
            var iso_code = "eur";
            $(".monthly-prices-"+iso_code).hide();
            $(".yearly-prices-"+iso_code).show();
            $('.btn-try.active').removeClass('active');
            $('.btn-monthly.active').removeClass('active');
            $(this).addClass('active');
        });
        
        $('#monthly-payment-euro').click(function (e) {
            var iso_code = "eur";
            $(".monthly-prices-"+iso_code).show();
            $(".yearly-prices-"+iso_code).hide();
            $(".monthly-plan-block").show();
            $(".year-plan-block").hide();
            $('.btn-buy.active').removeClass('active');
            $(this).addClass('active');
            $('#plan_type_total_text').html('Total till End Of Month :');
            $('#plan_type_total_further_text').html('Total for further months :');
            priceRangeSlider();
        });
        $('#yearly-payment-euro').click(function (e) {
            var iso_code = "eur";
            $(".monthly-prices-"+iso_code).hide();
            $(".yearly-prices-"+iso_code).show();
            $(".monthly-plan-block").hide();
            $(".year-plan-block").show();
            $('.btn-try.active').removeClass('active');
            $('.btn-monthly.active').removeClass('active');
            $(this).addClass('active');
            $('#plan_type_total_text').html('Total till End Of Year :');
            $('#plan_type_total_further_text').html('Total for further years :');
            priceRangeSlider();
        });
        
        $('#monthly-payment-renewplan-euro').click(function (e) {
            var iso_code = "eur";
            $(".monthly-prices-"+iso_code).show();
            $(".yearly-prices-"+iso_code).hide();
            $(".monthly-plan-block").show();
            $(".year-plan-block").hide();
            $('.btn-buy.active').removeClass('active');
            $(this).addClass('active');
            $('#plan_type_total_text').html('Total till End Of Month :');
            $('#plan_type_total_further_text').html('Total for further months :');
            priceRenewRangeSlider();
        });
        $('#yearly-payment-renewplan-euro').click(function (e) {
            var iso_code = "eur";
            $(".monthly-prices-"+iso_code).hide();
            $(".yearly-prices-"+iso_code).show();
            $(".monthly-plan-block").hide();
            $(".year-plan-block").show();
            $('.btn-try.active').removeClass('active');
            $('.btn-monthly.active').removeClass('active');
            $(this).addClass('active');
            $('#plan_type_total_text').html('Total till End Of Year :');
            $('#plan_type_total_further_text').html('Total for further years :');
            priceRenewRangeSlider();
        });
	
}).call(this);

function fn_windowheightinit()
{
    $('#headermenu');
    var isFixed = false;
    var maxidHeightHeaderTop = 0;
    $(window).scroll(function () {
        var mixedHeightHeader = $('#section-slider');
        if (mixedHeightHeader.length) {
            var maxidHeightHeaderTop = mixedHeightHeader.offset().top;
        }
        var mixedHeightHeaderBottom = maxidHeightHeaderTop + mixedHeightHeader.height();
        var headerMenu = $('#headermenu');


        if ($(document).scrollTop() < mixedHeightHeaderBottom && isFixed)       {        
            if (headerMenu) {                  
                headerMenu.css({
                    top: '0',
                    position: 'relative'
                });  
                isFixed = false;
                mixedHeightHeader.css('margin-bottom', '0');
            }    
        }    
        else  if ($(document).scrollTop() > mixedHeightHeaderBottom && !isFixed)   {        
            if (headerMenu) {                      
                headerMenu.css({
                    top: '0',
                    position: 'fixed'
                });
                mixedHeightHeader.css('margin-bottom', headerMenu.height() + 'px');
            }
            isFixed = true;
        }
    });
}