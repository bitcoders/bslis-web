
'use strict'

/// parameters = unit_code, season_code report_date
let otherRecovery_unit;
let param;
$(document).ready(function () {
	otherRecovery_unit = $(".dashboard-tab").attr('href').replace("#tab-", '');
	param = { 'unit_code': otherRecovery_unit };
	getOtherRecoveriesOfPreviousDay();
});

$(".dashboard-tab").click(function () {
	otherRecovery_unit = $(this).attr('href').replace("#tab-", '');
	param = { 'unit_code': otherRecovery_unit };
	getOtherRecoveriesOfPreviousDay();
	// assignValuesToDOM();
});

function getOtherRecoveriesOfPreviousDay() {
	$.ajax({
		type: 'post',
		url: '/api/OtherRecoveriesApi/PostOtherRecoveryPreviousDay',
		contentType: 'application/json; charset=utf-8',
		dataType: 'json',
		data: JSON.stringify(param),
		success: (response) => {
			console.log(response);
			if (response.status["statusCode"] != 200) {
				$("#otherRecoveriesStatus").show();
				$("#otherRecoveriesStatus").text(response.status["statusMessage"])
			}
			else {
				$("#otherRecoveriesStatus").hide();
			}
			assignValuesOnDashboard(response.otherRecovery);
		}
	});
}

function assignValuesOnDashboard(data) {
//	console.log(data);
	$("#prv-day-loss-in-c-heavy-percent-cane").text(data.LossInCHeavyPercentCane.toFixed(2));
	$("#prv-day-recovery-on-c-heavy").text(data.estimated_sugar_on_c_heavy_on_date.toFixed(2));
	$("#prv-day-recovery-on-c-heavy_to_date").text(data.estimated_sugar_on_c_heavy_to_date.toFixed(2));

	$("#prv-day-loss-in-b-heavy-percent-cane").text(data.LossInBHeavyPercentCane.toFixed(2));
	$("#prv-day-recovery-on-b-heavy").text(data.estimated_sugar_on_b_heavy_on_date.toFixed(2));
	$("#prv-day-recovery-on-b-heavy_to_date").text(data.estimated_sugar_on_b_heavy_to_date.toFixed(2));

	//prv-day-raw-sugar-gain
	$("#prv-day-raw-sugar-gain").text(data.RawSugarGain.toFixed(2));
	$("#prv-day-recovery-on-raw-sugar").text(data.estimated_sugar_on_raw_sugar_on_date.toFixed(2));
	$("#prv-day-recovery-on-raw-sugar_to_date").text(data.estimated_sugar_on_raw_sugar_to_date.toFixed(2));

	$("#prv-day-syrup-diversion").text(data.SyrupDiversion.toFixed(2));
	$("#prv-day-recovery-on-syrup-sugar").text(data.estimated_sugar_on_syrup_on_date.toFixed(2));
	$("#prv-day-recovery-on-syrup-sugar_to_date").text(data.estimated_sugar_on_syrup_to_date.toFixed(2));
}