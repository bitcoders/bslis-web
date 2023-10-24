/*----------  Preloader  ----------*/

/**
 *
 * Include this file above your custom js (ex: app.js).
 *
 */

var $preloader = $('#preloader');
var $cnt_preloader = $('#cnt_preloader');

$(document).ready(function()
{
	init_preloader();
});

$(window).on('beforeunload', function()
{
	show_preloader();
});

function is_preloader()
{
	if (($cnt_preloader.length > 0) && ($preloader.length > 0))
	{
		return true;
	}
	else
	{
		return false;
	}
}

function show_preloader()
{
	if(is_preloader())
	{
		$preloader.fadeIn('fast');
		$cnt_preloader.fadeIn('fast');
		$('body').css('overflow', 'hidden');
	}
}

function init_preloader()
{
	if(is_preloader())
	{
		show_preloader();

		setTimeout(function()
		{
			$('body').css('overflow', '');
			$preloader.fadeOut("slow");

			setTimeout(function()
			{
				$cnt_preloader.fadeOut("slow");
			}, 1000);
		}, 0);
	}
}
