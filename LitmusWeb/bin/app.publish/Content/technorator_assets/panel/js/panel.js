/*----------  Doucment Ready Functions  ----------*/

$(document).ready(function()
{
	init_icheck();
	init_select2();
	init_datepicker();
	
	console.log('Ready');
});

/*----------  Window Events  ----------*/

$(window).on('load resize', function()
{
	
});

/*----------  jQuery Validators Add Custom Methods  ----------*/

$.validator.addMethod('valueNotEquals', function(value, element, arg)
{
	return arg != value;
}, 'No a valid selection.');

/*----------  jQuery Validators Set Defaults  ----------*/

$.validator.setDefaults(
{
	debug: true,
	ignore: '.select2-search__field',
	errorPlacement: function(error, element)
	{
		$(error).addClass('text-danger m-t-10');
		$(element).closest('.form-group').append($(error));
	}
});

/*----------  Sweet Alert Set Defaults  ----------*/

/*swal.setDefaults(
{
	html: true,
	animation: false,
	allowOutsideClick: true,
	confirmButtonColor: '#00c0ef',
});*/

/*----------  Init Box Loader  ----------*/

function box_overlay()
{
	console.log('box');
}

/*----------  Init Select2  ----------*/

function init_select2()
{
	var $select = $('.select2');

	if($select.length)
	{
		$select.css('width', '100%').select2();
	}
}

/*----------  Init Datepicker  ----------*/

function init_datepicker()
{
	var $datepicker = $('.datepicker');

	if($datepicker.length)
	{
		$datepicker.datepicker(
		{
			clearBtn: true,
			autoclose: true,
			todayHighlight: true,
			format: "yyyy-mm-dd"
		});
	}
}

/*----------  Init iCheck  ----------*/

function init_icheck()
{
	var $icheck = $('.icheck');

	if($icheck.length)
	{
		$icheck.iCheck(
		{
			checkboxClass: 'icheckbox_square-blue',
			radioClass: 'iradio_square-blue'
		});
	}
}