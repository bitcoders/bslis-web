using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.ReportsRepository;
using LitmusWeb.Models;
using LitmusWeb.Models.CustomModels;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LitmusWeb.ApiControllers
{
    public class LedgerDataApiController : ApiController
    {
        private int BaseUnitCode, CrushingSeason;
        private DateTime EntryDate, ProcessDate, CrushingStartDate;
        LedgerDataRepository Repository = new LedgerDataRepository();

        [System.Web.Mvc.NonAction]
        private void SetUnitDefaultValues(int unit_code)
        {
            MasterUnitRepository UnitRepository = new MasterUnitRepository();
            var UnitDefaultValues = UnitRepository.FindUnitByPk(unit_code);
            BaseUnitCode = unit_code;
            EntryDate = UnitDefaultValues.EntryDate;
            ProcessDate = UnitDefaultValues.ProcessDate;
            CrushingSeason = UnitDefaultValues.CrushingSeason;
            CrushingStartDate = UnitDefaultValues.CrushingStartDate;
        }

        [HttpPost]
        [ActionName(name: "LedgerDataForPreviousDay")]
        [Route("api/LedgerDataApi/LedgerDataForPreviousDay")]
        public IHttpActionResult GetLedgerDataForPreviousDay([FromBody] ApiParamUnitCode apiParam)
        {
            SetUnitDefaultValues(apiParam.unit_code);
            ledger_data ledgerData = new ledger_data();
            try
            {
                ledgerData = Repository.GetLedgerDataForThePreviousDate(BaseUnitCode, CrushingSeason, EntryDate);

                if (ledgerData == null)
                {
                    return Content(HttpStatusCode.NoContent, "No Content");
                }
                LedgerDataModel model = new LedgerDataModel()
                {
                    unit_code = ledgerData.unit_code,
                    season_code = ledgerData.season_code,
                    trans_date = ledgerData.trans_date,
                    cane_crushed = ledgerData.cane_crushed,
                    gross_mixed_juice_qtl = ledgerData.gross_mixed_juice_qtl,
                    gross_mixed_juice_percent_cane = ledgerData.gross_mixed_juice_percent_cane,
                    //dirt_correction_qtl = ledgerData.dirt_correction_qtl,
                    //dirt_correction_perccent_cane = ledgerData.dirt_correction_perccent_cane,
                    //net_mixed_juice_qtl = ledgerData.net_mixed_juice_qtl,
                    //net_mixed_juice_percent_cane = ledgerData.net_mixed_juice_percent_cane,
                    //total_water = ledgerData.total_water,
                    //water_percent_cane = ledgerData.water_percent_cane,
                    //total_bagasse_qtl = ledgerData.total_bagasse_qtl,
                    //total_bagasse_percent_cane = ledgerData.total_bagasse_percent_cane,
                    //press_cake = ledgerData.press_cake,
                    //press_cake_percent_cane = ledgerData.press_cake_percent_cane,
                    //pol_in_mixed_juice_qtl = ledgerData.pol_in_mixed_juice_qtl,
                    //combined_pol_mixed_juice_avg = ledgerData.combined_pol_mixed_juice_avg,
                    //pol_in_mixed_juice_percent_cane = ledgerData.pol_in_mixed_juice_percent_cane,
                    //brix_in_mixed_juice = ledgerData.brix_in_mixed_juice,
                    //brix_in_mixed_juice_percentage_cane = ledgerData.brix_in_mixed_juice_percentage_cane,
                    //non_sugar_mixed_juice = ledgerData.non_sugar_mixed_juice,
                    //non_sugar_mixed_juice_percent_cane = ledgerData.non_sugar_mixed_juice_percent_cane,
                    //pol_in_bagasse_qtl = ledgerData.pol_in_bagasse_qtl,
                    //pol_in_bagasse_percentage_cane = ledgerData.pol_in_bagasse_percentage_cane,
                    //brix_in_bagasse_qtl = ledgerData.brix_in_bagasse_qtl,
                    //brix_precent_bagasse = ledgerData.brix_precent_bagasse,
                    //brix_in_bagasse_precent_cane = ledgerData.brix_in_bagasse_precent_cane,
                    //brix_in_cane = ledgerData.brix_in_cane,
                    //brix_percent_cane = ledgerData.brix_percent_cane,
                    //pol_in_cane_qtl = ledgerData.pol_in_cane_qtl,
                    //pol_in_cane_percent = ledgerData.pol_in_cane_percent,
                    //pol_in_press_cake_qtl = ledgerData.pol_in_press_cake_qtl,
                    //pol_in_press_cake_percentage_cane = ledgerData.pol_in_press_cake_percentage_cane,
                    //brix_in_press_cake_qtl = ledgerData.brix_in_press_cake_qtl,
                    //brix_in_press_cake_percentage_cane = ledgerData.brix_in_press_cake_percentage_cane,
                    //moist_bagasse_qtl = ledgerData.moist_bagasse_qtl,
                    //moist_bagasse_in_percent_cane = ledgerData.moist_bagasse_in_percent_cane,
                    //moist_percent_bagasse = ledgerData.moist_percent_bagasse,
                    //fiber_percent_bagasse = ledgerData.fiber_percent_bagasse,
                    //fiber_in_bagasse_qtl = ledgerData.fiber_in_bagasse_qtl,
                    //fiber_percent_cane = ledgerData.fiber_percent_cane,
                    //dry_mill_factor = ledgerData.dry_mill_factor,
                    //dry_mill_factor_percent_cane = ledgerData.dry_mill_factor_percent_cane,
                    //pol_in_clear_juice_qtl = ledgerData.pol_in_clear_juice_qtl,
                    //pol_in_clear_juice_percentage_cane = ledgerData.pol_in_clear_juice_percentage_cane,
                    //brix_in_clear_juice_qtl = ledgerData.brix_in_clear_juice_qtl,
                    //brix_in_clear_juicce_percentage_cane = ledgerData.brix_in_clear_juicce_percentage_cane,
                    //non_sugar_in_clear_juice_qtl = ledgerData.non_sugar_in_clear_juice_qtl,
                    //non_sugar_clear_juice_percentage_cane = ledgerData.non_sugar_clear_juice_percentage_cane,
                    //non_sugar_in_cj_percent_non_sugar_in_mj = ledgerData.non_sugar_in_cj_percent_non_sugar_in_mj,
                    //clear_juice_qtl = ledgerData.clear_juice_qtl,
                    //clear_juice_qtl_percentage_cane = ledgerData.clear_juice_qtl_percentage_cane,
                    //non_sugar_final_molasses = ledgerData.non_sugar_final_molasses,
                    //estimated_molasses_qtl = ledgerData.estimated_molasses_qtl,
                    //estimated_molasses_percent_cane = ledgerData.estimated_molasses_percent_cane,
                    //pol_in_molasses_qtl = ledgerData.pol_in_molasses_qtl,
                    //pol_in_molasses_percent_cane = ledgerData.pol_in_molasses_percent_cane,
                    unknown_loss_qtl = ledgerData.unknown_loss_qtl,
                    unknown_loss_percent_cane = ledgerData.unknown_loss_percent_cane,
                    //available_sugar_qtl = ledgerData.available_sugar_qtl,
                    //available_sugar_qtl_percent_cane = ledgerData.available_sugar_qtl_percent_cane,
                    estimated_sugar_qtl = ledgerData.estimated_sugar_qtl,
                    estimated_sugar_percent_cane = ledgerData.estimated_sugar_percent_cane,
                    sugar_in_process_qtl = ledgerData.sugar_in_process_qtl,
                    sugar_in_process_percent_cane = ledgerData.sugar_in_process_percent_cane,
                    molasses_in_process_qtl = ledgerData.molasses_in_process_qtl,
                    molasses_in_process_percent_cane = ledgerData.molasses_in_process_percent_cane,
                    //sugar_in_sugar = ledgerData.sugar_in_sugar,
                    //sugar_in_sugar_percent_cane = ledgerData.sugar_in_sugar_percent_cane,
                    unknown_losses_calculated = ledgerData.unknown_losses_calculated,
                    unknown_losses_calculated_percent_cane = ledgerData.unknown_losses_calculated_percent_cane,
                    total_losses = ledgerData.total_losses,
                    total_losses_percent = ledgerData.total_losses_percent,
                    //java_ratio = ledgerData.java_ratio,
                    //clerification_efficiency = ledgerData.clerification_efficiency,
                    //clerification_factor = ledgerData.clerification_factor,
                    //mill_extraction = ledgerData.mill_extraction,
                    //lost_juice_per_fiber = ledgerData.lost_juice_per_fiber,
                    //reduced_mill_extraction_deer = ledgerData.reduced_mill_extraction_deer,
                    //reduced_mill_extraction_mittal = ledgerData.reduced_mill_extraction_mittal,
                    //boiling_house_recovery = ledgerData.boiling_house_recovery,
                    //bhr_basic = ledgerData.bhr_basic,
                    //virtual_purity_final_molasses = ledgerData.virtual_purity_final_molasses,
                    //reduced_boiling_house_recovery_deer = ledgerData.reduced_boiling_house_recovery_deer,
                    //reduced_boilin_house_recovery_rao = ledgerData.reduced_boilin_house_recovery_rao,
                    //over_all_recovery = ledgerData.over_all_recovery,
                    //reduced_overall_recovery_deer = ledgerData.reduced_overall_recovery_deer,
                    //reduced_overall_recovery_rao = ledgerData.reduced_overall_recovery_rao,
                    //efficiency_percent_avail_sucros_in_mixed_juice = ledgerData.efficiency_percent_avail_sucros_in_mixed_juice,
                    //efficiency_percent_avail_sucros_in_primary_juice = ledgerData.efficiency_percent_avail_sucros_in_primary_juice,
                    //erq_mj_to_pj = ledgerData.erq_mj_to_pj,
                    //erq_lj_to_pj = ledgerData.erq_lj_to_pj,
                    //extracted_mj_added_water = ledgerData.extracted_mj_added_water,
                    //brix_free_cane_water_percent_cane = ledgerData.brix_free_cane_water_percent_cane,
                    //brix_free_cane_water_percent_fiber = ledgerData.brix_free_cane_water_percent_fiber,
                    //milling_ratio = ledgerData.milling_ratio,
                    //dilution_percent_pj_to_added_water = ledgerData.dilution_percent_pj_to_added_water,
                    //added_water_percent_fiber = ledgerData.added_water_percent_fiber,
                    //undiluted_juice_extracted = ledgerData.undiluted_juice_extracted,
                    //undiluted_juice_lost_percent_fiber = ledgerData.undiluted_juice_lost_percent_fiber,
                    //dilution_percent_cane = ledgerData.dilution_percent_cane,
                    //undiulted_juice_percent_cane = ledgerData.undiulted_juice_percent_cane,
                    //total_juice = ledgerData.total_juice,
                    //combined_bagasse_average = ledgerData.combined_bagasse_average,
                    //combined_bagasse_moist_percent = ledgerData.combined_bagasse_moist_percent,
                    //press_cake_average = ledgerData.press_cake_average,
                    //sp_gravity = ledgerData.sp_gravity,
                    //combined_pj_brix = ledgerData.combined_pj_brix,
                    //combined_Pj_pol = ledgerData.combined_Pj_pol,
                    //combined_pj_purity = ledgerData.combined_pj_purity,
                    //combined_pj_solids = ledgerData.combined_pj_solids,
                    //combined_pj_pol_qtl = ledgerData.combined_pj_pol_qtl,
                    //combined_lj_brix = ledgerData.combined_lj_brix,
                    //combined_lj_pol = ledgerData.combined_lj_pol,
                    //combined_lj_purity = ledgerData.combined_lj_purity,
                    //combined_mj_brix = ledgerData.combined_mj_brix,
                    //combined_mj_pol = ledgerData.combined_mj_pol,
                    //combined_mj_purity = ledgerData.combined_mj_purity,
                    //combined_mj_solids = ledgerData.combined_mj_solids,
                    //combined_mj_pol_qtl = ledgerData.combined_mj_pol_qtl,
                    //combined_pj_pol_avg = ledgerData.combined_pj_pol_avg,
                    //combined_pj_brix_avg = ledgerData.combined_pj_brix_avg,
                    //combined_lj_brix_avg = ledgerData.combined_lj_brix_avg,
                    //combined_lj_pol_avg = ledgerData.combined_lj_pol_avg,
                    //combined_lj_brix_solid = ledgerData.combined_lj_brix_solid,
                    //combined_lj_pol_qtl = ledgerData.combined_lj_pol_qtl,
                    //clear_juice_brix = ledgerData.clear_juice_brix,
                    //clear_juice_pol = ledgerData.clear_juice_pol,
                    //clear_juice_purity = ledgerData.clear_juice_purity,
                    //oliver_brix = ledgerData.oliver_brix,
                    //oliver_pol = ledgerData.oliver_pol,
                    //oliver_purity = ledgerData.oliver_purity,
                    //brix_in_final_mol_qtl = ledgerData.brix_in_final_mol_qtl,
                    //pol_in_final_mol_qtl = ledgerData.pol_in_final_mol_qtl,
                    //non_sugar_in_final_mol_qtl = ledgerData.non_sugar_in_final_mol_qtl,
                    //non_sugar_in_final_mol_percent_cane = ledgerData.non_sugar_in_final_mol_percent_cane,
                    final_molasses_sent_out = ledgerData.final_molasses_sent_out,
                    final_molasses_sent_out_solids = ledgerData.final_molasses_sent_out_solids,
                    final_molasses_sent_out_pol_qtl = ledgerData.final_molasses_sent_out_pol_qtl,
                    final_molasses_brix = ledgerData.final_molasses_brix,
                    final_molasses_pol = ledgerData.final_molasses_pol,
                    final_molasses_purity = ledgerData.final_molasses_purity,
                    unknown_loss = ledgerData.unknown_loss,
                    //boiling_house_extraction = ledgerData.boiling_house_extraction,
                    //overall_extraction = ledgerData.overall_extraction,
                    total_sugar_bagged = ledgerData.total_sugar_bagged,
                    //remelting_sugar_totals = ledgerData.remelting_sugar_totals,
                    //remelting_raw_sugar_total = ledgerData.remelting_raw_sugar_total,
                    net_sugar_bagged = ledgerData.net_sugar_bagged,
                    //remelting_molasses_total = ledgerData.remelting_molasses_total,
                    //remelting_raw_molasses_total = ledgerData.remelting_raw_molasses_total,
                    //net_molasses_sent_out = ledgerData.net_molasses_sent_out,
                    //combined_juice = ledgerData.combined_juice,
                    //combined_mj_brix_qtl = ledgerData.combined_mj_brix_qtl,
                    //pj_brix_qtl = ledgerData.pj_brix_qtl,
                    //pj_pol_qtl = ledgerData.pj_pol_qtl,
                    //un_sulphered_brix = ledgerData.un_sulphered_brix,
                    //un_sulphered_pol = ledgerData.un_sulphered_pol,
                    //un_sulphered_purity = ledgerData.un_sulphered_purity,
                    //sulphered_brix = ledgerData.sulphered_brix,
                    //sulphered_pol = ledgerData.sulphered_pol,
                    //sulphered_purity = ledgerData.sulphered_purity,
                    //theoretical_final_molasses_qtl = ledgerData.theoretical_final_molasses_qtl,
                    //theoretical_final_molasses_percent_cane = ledgerData.theoretical_final_molasses_percent_cane,
                    //actual_percent_theoretical_final_molasses_percent_cane = ledgerData.actual_percent_theoretical_final_molasses_percent_cane,
                    //a_mass_pol = ledgerData.a_mass_pol,
                    //a_mass_brix = ledgerData.a_mass_brix,
                    //a_mass_purity = ledgerData.a_mass_purity,
                    //a1_mass_pol = ledgerData.a1_mass_pol,
                    //a1_mass_brix = ledgerData.a1_mass_brix,
                    //a1_mass_purity = ledgerData.a1_mass_purity,
                    //b_mass_pol = ledgerData.b_mass_pol,
                    //b_mass_brix = ledgerData.b_mass_brix,
                    //b_mass_purity = ledgerData.b_mass_purity,
                    //c1_mass_pol = ledgerData.c1_mass_pol,
                    //c1_mass_brix = ledgerData.c1_mass_brix,
                    //c1_mass_purity = ledgerData.c1_mass_purity,
                    //c_mass_pol = ledgerData.c_mass_pol,
                    //c_mass_brix = ledgerData.c_mass_brix,
                    //c_mass_purity = ledgerData.c_mass_purity,
                    //raw_mass_pol = ledgerData.raw_mass_pol,
                    //raw_mass_brix = ledgerData.raw_mass_brix,
                    //raw_mass_purity = ledgerData.raw_mass_purity,
                    //r1_mass_pol = ledgerData.r1_mass_pol,
                    //r1_mass_brix = ledgerData.r1_mass_brix,
                    //r1_mass_purity = ledgerData.r1_mass_purity,
                    //r3_mass_pol = ledgerData.r3_mass_pol,
                    //r3_mass_brix = ledgerData.r3_mass_brix,
                    //r3_mass_purity = ledgerData.r3_mass_purity,
                    //cane_centre = ledgerData.cane_centre,
                    //cane_gate = ledgerData.cane_gate,
                    //bagasse_sold = ledgerData.bagasse_sold,
                    //store_sulpher = ledgerData.store_sulpher,
                    //store_phosphoric = ledgerData.store_phosphoric,
                    //store_lime = ledgerData.store_lime,
                    //store_viscosity_reducer = ledgerData.store_viscosity_reducer,
                    //store_biocide = ledgerData.store_biocide,
                    //store_color_reducer = ledgerData.store_color_reducer,
                    //store_magnafloe = ledgerData.store_magnafloe,
                    //store_lub_oil = ledgerData.store_lub_oil,
                    //store_lub_grease = ledgerData.store_lub_grease,
                    //store_boiler_chemical = ledgerData.store_boiler_chemical,
                    //store_sulpher_percent_cane = ledgerData.store_sulpher_percent_cane,
                    //store_phosphoric_percent_cane = ledgerData.store_phosphoric_percent_cane,
                    //store_lime_percent_cane = ledgerData.store_lime_percent_cane,
                    //store_viscosity_reducer_percent_cane = ledgerData.store_viscosity_reducer_percent_cane,
                    //store_biocide_percent_cane = ledgerData.store_biocide_percent_cane,
                    //store_color_reducer_percent_cane = ledgerData.store_color_reducer_percent_cane,
                    //store_magnafloe_percent_cane = ledgerData.store_magnafloe_percent_cane,
                    //store_lub_oil_percent_cane = ledgerData.store_lub_oil_percent_cane,
                    //store_lub_grease_percent_cane = ledgerData.store_lub_grease_percent_cane,
                    //store_boiler_chemical_percent_cane = ledgerData.store_boiler_chemical_percent_cane,
                    //cane_early = ledgerData.cane_early,
                    //cane_general = ledgerData.cane_general,
                    //cane_reject = ledgerData.cane_reject,
                    //cane_farm = ledgerData.cane_farm,
                    //cane_early_percent = ledgerData.cane_early_percent,
                    //cane_general_percent = ledgerData.cane_general_percent,
                    //cane_reject_percent = ledgerData.cane_reject_percent,
                    //cane_gate_percent = ledgerData.cane_gate_percent,
                    //cane_centre_percent = ledgerData.cane_centre_percent,
                    //cane_farm_percent = ledgerData.cane_farm_percent,
                    //final_molasses_sent_out_percent = ledgerData.final_molasses_sent_out_percent,
                    //cane_burnt = ledgerData.cane_burnt,
                    //cane_burnt_percent = ledgerData.cane_burnt_percent,
                    //store_washing_soda = ledgerData.store_washing_soda,
                    //store_hydrocloric_acid = ledgerData.store_hydrocloric_acid,
                    //store_de_scaling_chemical = ledgerData.store_de_scaling_chemical,
                    //store_seed_slurry = ledgerData.store_seed_slurry,
                    //store_anti_fomer = ledgerData.store_anti_fomer,
                    //store_chemical_for_brs_cleaning = ledgerData.store_chemical_for_brs_cleaning,
                    available_minutes = ledgerData.available_minutes,
                    EvaporationPercentClearJuice = ledgerData.EvaporationPercentClearJuice,
                    TotalNetClearJuice = ledgerData.TotalNetClearJuice,
                    ClearJuiceDiversion = ledgerData.ClearJuiceDiversion,
                    DivertedCane = ledgerData.DivertedCane,
                    DivertedMixedJuice = ledgerData.DivertedMixedJuice,
                    loss_in_diverted_cane_qtl = ledgerData.loss_in_diverted_cane_qtl,
                };
                return Ok(model);
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return Content(HttpStatusCode.InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        [Route("api/LedgerDataApi/PostLedgerDataForTheDay")]
        [ActionName("PostLedgerDataForTheDay")]
        public IHttpActionResult PostLedgerDataForTheDay([FromBody] ApiParamUnitCode apiParam)
        {
            SetUnitDefaultValues(apiParam.unit_code);
            ledger_data ledgerData = new ledger_data();
            try
            {
                ledgerData = Repository.GetLedgerDataForTheDate(BaseUnitCode, CrushingSeason, ProcessDate);
                if (ledgerData == null)
                {
                    return Content(HttpStatusCode.NoContent, "No Content");
                }
                return Ok(ledgerData);
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return Content(HttpStatusCode.InternalServerError, "Internal server error");
            }
        }

        /// <summary>
        /// Get the todate figure till given date and unit code
        /// </summary>
        /// <param name="unit_code"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("api/LedgerDataApi/GetLedgerDataToDatesAsync")]
        [ActionName("PostLedgerDataForTheDay")]
        public async Task<HttpResponseMessage> GetLedgerDataToDatesAsync([FromBody] ApiParamUnitCode apiParam)
        {
            SetUnitDefaultValues(apiParam.unit_code);
            func_ledger_data_period_summary_Result data = new func_ledger_data_period_summary_Result();
            try
            {
                data = await Repository.GetLedgerDataPeriodSummaryAsync(apiParam.unit_code, CrushingSeason, CrushingStartDate, ProcessDate);
                if (data == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                var GenericData = new { data };
                return Request.CreateResponse(HttpStatusCode.OK, GenericData);
            }
            catch (Exception ex)
            {
                new Exception(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
