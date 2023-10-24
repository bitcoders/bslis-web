using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class DailyAnalysisModel
    {
        public int id { get; set; }

        public Nullable<int> unit_code { get; set; }


        [Display(Name = "Season")]
        public Nullable<int> season_code { get; set; }


        [Display(Name = "Date")]
        public System.DateTime entry_date { get; set; }

        [Display(Name = "Time")]
        public string trans_time { get; set; }

        [Required]
        [Display(Name = "Early(Qtl)")]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        public decimal cane_early { get; set; }

        [Required]
        [Display(Name = "General (Qtl.)")]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        public decimal cane_general { get; set; }

        [Required]
        [Display(Name = "Reject (Qtl.)")]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        public decimal cane_rejected { get; set; }

        [Required]
        [Display(Name = "Burnt (Qtl.)")]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        public decimal cane_burnt { get; set; }

        [Required]
        [Display(Name = "Gate (Qtl.)")]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        public decimal cane_gate { get; set; }

        [Required]
        [Display(Name = "Centre (Qtl.)")]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        public decimal cane_centre { get; set; }

        [Required]
        [Display(Name = "Crushed (Qtl.)")]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        public decimal cane_crushed { get; set; }

        [Required]
        [Display(Name = "Juice flow m³ x 10")]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        public decimal total_juice { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: 0.00000}")]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "SP Gravity")]
        public Nullable<decimal> sp_gravity { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Total Water (Qtl.)")]
        public decimal total_water { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Press Cake (Qtl.)")]
        public decimal press_cake { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Molasses Sent Out (Qtl.)")]
        public decimal molasses_sent_out { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "BISS Avbl. Sugar (Qtl.)")]
        public decimal biss_sugar { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "BISS Avbl Mol. (Qtl.)")]
        public decimal biss_molasses { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Scrap Avbl. Sugar (Qtl.)")]
        public decimal scrap_sugar { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Scrap Avbl. Mol. (Qtl.)")]
        public decimal scrap_molasses { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Moist Avbl. Sugar(Qtl.)")]
        public decimal moist_sugar { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Moist Avbl. Mol.(Qtl.)")]
        public decimal moist_molasses { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Raw Avbl. Sugar(Qtl.)")]
        public decimal raw_sugar { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Raw Avbl. Mol.(Qtl.)")]
        public decimal raw_molasses { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Other Avbl. Sugar(Qtl.)")]
        public decimal other_sugar { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Other Avbl. Mol")]
        public decimal other_molasses { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Sulphur(Qtl.)")]
        public decimal store_sulpher { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Phosphoric(Kg.)")]
        public decimal store_phosphoric { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Lime(Qtl.)")]
        public decimal store_lime { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Visco. Reducer(Kg.)")]
        public decimal store_viscosity_reducer { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Biocide(Kg.)")]
        public decimal store_biocide { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Color Reducer(Kg.)")]
        public decimal store_color_reducer { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Megnafloe(Kg.)")]
        public decimal store_megnafloe { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Lub. Oil (Ltr.)")]
        public decimal store_lub_oil { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Lub. Grease(Kg.)")]
        public decimal store_lub_grease { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Boiler Chemical(Kg.)")]
        public decimal store_boiler_chemical { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Washing Soda(Kg.)")]
        public decimal store_washing_soda { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Hydrochloric Acid(Kg.)")]
        public decimal store_hydrolic_acid { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "De-scaling Chemical(Kg.)")]
        public decimal store_de_scaling_chemical { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Seed Slurry(Kg.)")]
        public decimal store_seed_slurry { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Anti Foam(Kg.)")]
        public decimal store_anti_fomer { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Chem. For BRS Cleaning(Kg.)")]
        public decimal store_chemical_for_brs_cleaning { get; set; }


        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Icumsa L31")]
        public short icumsa_l31 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Icumsa L30")]
        public short icumsa_l30 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Icumsa M-31")]
        public short icumsa_m31 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Icumsa M-30")]
        public short icumsa_m30 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Icumsa S-31")]
        public short icumsa_s31 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Icumsa S-30")]
        public short icumsa_s30 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Icumsa Raw Sugar")]
        public int icumsa_raw_sugar { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Foreign Matter L-31 (ppm)")]
        public short foreign_matter_l31 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Foreign Matter L-30 (ppm)")]
        public short foreign_matter_l30 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Foreign Matter M-31 (ppm)")]
        public short foreign_matter_m31 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Foreign Matter M-30 (ppm)")]
        public short foreign_matter_m30 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Foreign Matter S-31 (ppm)")]
        public short foreign_matter_s31 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Foreign Matter S-30 (ppm)")]
        public short foreign_matter_s30 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Foreign Matter Raw Sugar (ppm)")]
        public int foreign_matter_raw_sugar { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Retention L-31 %")]
        public short retention_l31 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Retention L-30 %")]
        public short retention_l30 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Retention M-31 %")]
        public short retention_m31 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Retention M-30 %")]
        public short retention_m30 { get; set; }

        [Required]
        [Display(Name = "Retention S-31 %")]
        public short retention_s31 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Retention S-30 %")]
        public short retention_s30 { get; set; }

        [Required]
        [Display(Name = "Retention Raw Sugar %")]
        public int retention_raw_sugar { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Moisture L-31 %")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: 0.000}")]
        public decimal moisture_l31 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Moisture L-30 %")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: 0.000}")]
        public decimal moisture_l30 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Moisture M-31 %")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: 0.000}")]
        public decimal moisture_m31 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Moisture M-30 %")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: 0.000}")]
        public decimal moisture_m30 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Moisture S-31 %")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: 0.000}")]
        public decimal moisture_s31 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Moisture S-30 %")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: 0.000}")]
        public decimal moisture_s30 { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Moisture Raw Sugar %")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: 0.000}")]
        public decimal moisture_raw_sugar { get; set; }


        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "ETP pH")]
        public decimal etp_ph { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "ETP TSS")]
        public decimal etp_tss { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "ETP C.O.D")]
        public decimal etp_cod { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "ETP B.O.D")]
        public decimal etp_bod { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "ETP Water Flow")]
        public decimal etp_water_flow { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "ETP Water Consumption")]
        public decimal etp_water_consumption_at_plant { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Calcium M.Juice (ppm)")]
        public decimal calcium_mixed_juice { get; set; }
        [Required]
        [Display(Name = "Calcium C.Juice (ppm)")]
        public decimal calcium_clear_juice { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Phosphate M.Juice (ppm)")]
        public decimal phosphate_mixed_juice { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Phosphate C.Juice (ppm)")]
        public decimal phosphate_clear_juice { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "P.J. R.S. 100 Brix ")]
        public decimal rs_primary_juice { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "M.J. R.S. 100 Brix ")]
        public decimal rs_mixed_juice { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "C.J. R.S. 100 Brix ")]
        public decimal rs_clear_juice { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Unknown Loss %")]
        public Nullable<decimal> unknown_losses { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "Dirt Correction %")]
        public decimal dirt_correction { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*[1-9]*$",ErrorMessage ="Integer value only!")]
        [Display(Name = "Operating Tube Wells (hrs.)")]
        public Nullable<long> total_operating_tube_well { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "E.C Recovery %")]
        public Nullable<decimal> exhaust_condensate_recovery { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        [Display(Name = "T.C.Massecuite Pan (Nos.)")]
        public Nullable<decimal> T_c_massecuite_pan { get; set; }

        [Required]
        
        [Display(Name = "pH Injection Inlet")]
        public decimal ph_injection_inlet { get; set; }

        [Required]
        
        [Display(Name = "pH Injection Outlet")]
        public decimal ph_injection_outlet { get; set; }

        [Required]
        [Display(Name = "Temp. Injection Water Inlet °C")]
        public decimal Temp_Injection_Water_Inlet { get; set; }

        [Required]
        [Display(Name = "Temp. Injection Water Outlet °C")]
        public decimal Temp_Injection_Water_Outlet { get; set; }

        [Required]
        [Display(Name = "Average Vaccume Pan (Inch.)")]
        public decimal average_vaccume_pan { get; set; }

        [Required]
        [Display(Name = "Average Vaccume Evap(Inch.)")]
        public decimal average_vaccume_evap { get; set; }

        [Required]
        [Display(Name = "Exhaust Steam Pressure L.P")]
        public decimal Exhaust_steam_press_lp { get; set; }

        [Required]
        [Display(Name = "Exhaust Steam Pressure H.P")]
        public decimal Exhaust_steam_press_hp { get; set; }

        [Required]
        [Display(Name = "Boiler Steam Pressure L.P")]
        public decimal boiler_steam_press_lp { get; set; }

        [Required]
        [Display(Name = "Boiler Steam Pressure H.P")]
        public decimal boiler_steam_press_hp { get; set; }

        [Required]
        [Display(Name = "Boiler Feed Water pH")]
        public decimal ph_boiler_feed_water { get; set; }

        [Required]
        [Display(Name = "Boiler Feed Water Temp. °C")]
        public decimal Temp_Boiler_feed_water { get; set; }

        [Required]
        [Display(Name = "Bagasse Baed (Nos.)")]
        public decimal bagasse_baed { get; set; }

        [Required]
        [Display(Name = "Filter Water (Qtl.)")]
        public decimal filter_water { get; set; }

        [Required]
        [Display(Name = "Pan Water (Qtl.)")]
        public decimal pan_water { get; set; }

        [Required]
        [Display(Name = "C.F Water (Qtl.)")]
        public decimal cf_water { get; set; }

        [Required]
        [Display(Name = "Bagasses Sold (Qtls.)")]
        public decimal bagasse_sold { get; set; }

        [Required]
        [Display(Name = "Bagasses Stock  (Qtl.)")]
        public decimal bagasse_stock { get; set; }

        [Required]
        [Display(Name = "Bagasses Consumed  (Qtl.)")]
        public decimal bagasse_consumed_qtl { get; set; }

        [Required]
        [Display(Name = "Bagacillo to Boiling House (Qtl.)")]
        public decimal bagacillo_to_boiling_house { get; set; }

        [Required]
        [Display(Name = "Steam Fuel Ratio %")]
        public decimal Steam_Fuel_Ratio { get; set; }

        [Required]
        [Display(Name = "N.M Primary Index %")]
        public decimal nm_p_index { get; set; }
        [Required]
        [Display(Name = "N.M Primary Extraction %")]
        public decimal nm_pry_ext { get; set; }

        [Required]
        [Display(Name = "B. Heavy FM TRS %")]
        public decimal b_heavy_final_molasses_trs { get; set; }

        [Required]
        [Display(Name = "B.Heavy FM RS %")]
        public decimal b_heavy_final_molasses_rs { get; set; }

        [Required]
        [Display(Name = "C Heavy F.M TRS %.")]
        public decimal c_heavy_final_molasses_trs { get; set; }

        [Required]
        [Display(Name = "C Heavy F.M. RS %")]
        public decimal c_heavy_final_molasses_rs { get; set; }

        [Required]
        [Display(Name = "Max Temp. °C")]
        public decimal temp_max { get; set; }

        [Required]
        [Display(Name = "Min Temp. °C")]
        public decimal temp_min { get; set; }

        [Required]
        [Display(Name = "Humidity %")]
        public decimal humidity { get; set; }

        [Required]
        [Display(Name = "Rain fall (inch.)")]
        public decimal rain_fall { get; set; }

        [Required]
        [Display(Name = "IU Primary Juice")]
        public decimal iu_primary_juice { get; set; }

        [Required]
        [Display(Name = "IU Mixed Juice")]
        public decimal iu_mixed_juice { get; set; }

        [Required]
        [Display(Name = "IU Clear Juice")]
        public decimal iu_clear_juice { get; set; }

        // ph_mixed_juice is taken in two horuly form that is why this field is not required
        [Display(Name = "ph_mixed_juice")]
        public decimal ph_mixed_juice { get; set; }

        [Required]
        [Display(Name = "Live Steam Generation (Qtl.)")]
        public decimal live_steam_generation { get; set; }

        [Required]
        [Display(Name = "Live Steam Consumption  (Qtl.)")]
        public decimal live_steam_consumption { get; set; }


        [Required]
        [Display(Name = "Power Turbines (Qtl.)")]
        public decimal power_turbines { get; set; }

        [Required]
        [Display(Name = "Bleeding in Process (Qtl.)")]
        public decimal bleeding_in_process { get; set; }

        [Required]
        [Display(Name = "ATA-9 (Qtl.)")]
        public decimal bleeding_acf { get; set; }

        [Required]
        [Display(Name = "ATA-3 (Qtl.)")]
        public decimal ata3_cogen { get; set; }

        [Required]
        [Display(Name = "D Super Heating (Qtl.)")]
        public decimal d_sulpher_heating { get; set; }

        [Required]
        [Display(Name = "Drain Pipe Loss (Qtl.)")]
        public decimal drain_pipe_loss { get; set; }

        [Required]
        [Display(Name = "Exhaust Steam Generation (Qtl.)")]
        public decimal exhaust_steam_generation { get; set; }
        [Required]
        [Display(Name = "Exhaust Steam Consumption (Qtl.)")]
        public decimal exhaust_steam_consumption { get; set; }

        [Required]
        [Display(Name = "Steam Per 100 Ton Cane")]
        public decimal steam_per_ton_cane { get; set; }

        [Required]
        [Display(Name = "Steam Per 10 Ton Sugar")]
        public decimal steam_per_qtl_sugar { get; set; }

        [Required]
        [Display(Name = "Power From Grid (KWH)")]
        public decimal power_from_grid { get; set; }

        [Required]
        [Display(Name = "Total Power Export To Grid  (KWH)")]
        public decimal power_export_grid { get; set; }

        [Required]
        [Display(Name = "Power From DG Set  (KWH)")]
        public decimal power_dg_set { get; set; }

        [Required]
        [Display(Name = "Power From Sugar  (KWH)")]
        public decimal power_from_sugar { get; set; }

        [Required]
        [Display(Name = "Power Import from Co-Gen  (KWH)")]
        public decimal power_import_cogen { get; set; }

        [Required]
        [Display(Name = "Power Consumption @ Home (KWH)")]
        public decimal home_consumption { get; set; }

        [Required]
        [Display(Name = "Power Generation From Co-Gen")]
        public decimal power_generation_from_coGen { get; set; }


        [Required]
        [Display(Name = "Total Power Consumption (KWH)")]
        public decimal total_power { get; set; }

        [Required]
        [Display(Name = "Power Per Ton. Cane")]
        public decimal power_per_ton_cane { get; set; }

        [Required]
        [Display(Name = "Power per Qtl Sugar")]
        public decimal power_per_qtl_sugar { get; set; }

        [Required]
        [Display(Name = "OM Primary Index %")]
        public Nullable<decimal> om_p_index { get; set; }

        [Required]
        [Display(Name = "OM Primary Extraction %")]
        public Nullable<decimal> om_pry_ext { get; set; }

        [Required]
        [Display(Name = "Steam % Cane")]
        public Nullable<decimal> steam_percent_cane { get; set; }

        [Required]
        [Display(Name = "Cane Farm (Qtl)")]
        public Nullable<decimal> cane_farm { get; set; }

        [Required]
        [Display(Name = "Boiler Water pH")]
        public decimal boiler_water { get; set; }


        [Display(Name = "Daily Hower Worked (hrs.)")]
        public Nullable<byte> daily_hour_worked { get; set; }

        [Display(Name = "Created Date")]
        public Nullable<System.DateTime> crtd_dt { get; set; }


        [Display(Name = "Created By")]
        public string crtd_by { get; set; }


        [Display(Name = "Updated Date")]
        public Nullable<System.DateTime> updt_dt { get; set; }


        [Display(Name = "Updated By")]
        public string updt_by { get; set; }

        [Display(Name = "Power To Distillery")]
        public Nullable<decimal> PowerToDitillery { get; set; }

        [Display(Name = "Rec. Spirit")]
        public Nullable<decimal> RectifiedSpirit { get; set; }

        [Display(Name = "Abs. Alcohol")]
        public Nullable<decimal> AbsoluteAlcohol { get; set; }

        [Display(Name = "Ethanol")]
        public Nullable<decimal> Ethanol { get; set; }

        [Display(Name = "Rec. Recovery (Day)")]
        public Nullable<decimal> RectifiedSpiritDayRecover { get; set; }

        [Display(Name = "Rec. Recovery TD")]
        public Nullable<decimal> RectifiedSpiritToDateRecovery { get; set; }

        [Display(Name = "Abs Alco. Recovery Day")]
        public Nullable<decimal> AbsoluteAlcoholDayRecovery { get; set; }

        [Display(Name = "Abs. Aloc Recovery TD")]
        public Nullable<decimal> AbsoluteAlcoholToDateRecovery { get; set; }

        [Display(Name = "Ethanil Recovery Dat")]
        public Nullable<decimal> EthanolDayRecovery { get; set; }

        [Display(Name = "Ethanol Recovery TD")]
        public Nullable<decimal> EthanolToDateRecovery { get; set; }

        [Display(Name = "Gross BISS")]
        [Required]
        public Nullable<decimal> gross_biss_sugar { get; set; }

        [Display(Name = "Gross Scrap Sugar")]
        [Required]
        public Nullable<decimal> gross_scrap_sugar { get; set; }

        [Display(Name = "Gross Moist. Sugar")]
        [Required]
        public Nullable<decimal> gross_moist_sugar { get; set; }

        [Display(Name = "Gross Raw Sugar")]
        [Required]
        public Nullable<decimal> gross_raw_sugar { get; set; }

        [Display(Name = "Gross Oth. Sugar")]
        [Required]
        public Nullable<decimal> gross_other_sugar { get; set; }

        [Display(Name = "Water Pan-A")]
        [Required]
        public Nullable<decimal> water_pan_a { get; set; }

        [Display(Name = "Water Pan-B")]
        [Required]
        public Nullable<decimal> water_pan_b { get; set; }
        [Display(Name = "Water Pan-C")]
        [Required]
        public Nullable<decimal> water_pan_c { get; set; }

        [Display(Name = "Engineering Replacement")]
        [Required]
        public Nullable<decimal> OverTimeEnggReplacement { get; set; }
        [Display(Name = "Engineering Over-Time")]
        [Required]
        public Nullable<decimal> OverTimeEnggExtra { get; set; }

        [Display(Name = "Manufacturing Replacement")]
        [Required]
        public Nullable<decimal> OverTimeMfgReplacement { get; set; }
        [Display(Name = "Manufacturing Over Time")]
        [Required]
        public Nullable<decimal> OverTimeMfgExtra { get; set; }

        [Display(Name = "Mol. SentOut (B Heavy)")]
        [Required]
        public Nullable<decimal> MolassesSentOutBHeavy { get; set; }

        [Display(Name = "Mol. SentOut (C Heavy)")]
        [Required]
        public Nullable<decimal> MolassesSentOutCHeavy { get; set; }

        [Required]
        [Display(Name = "Proc. Chem. Qty")]
        public Nullable<decimal> StoreProcessChemicalQuantity { get; set; }
        [Required]
        [Display(Name = "Proc. Chem. Amt")]
        public Nullable<decimal> StoreProcessChemicalAmount { get; set; }

        [Required]
        [Display(Name = "Proc. Chem. Rate/Bag")]
        public Nullable<decimal> StoreProcessChemicalPerBagRate { get; set; }

        [Required]
        [Display(Name = "Proc. Chem. Rate/T.Cane")]
        public Nullable<decimal> StoreProcessChemicalPerTonCaneRate { get; set; }

        [Required]
        [Display(Name = "Boiler Chem. Qty")]
        public Nullable<decimal> StoreBoilerChemicalQuantity { get; set; }

        [Required]
        [Display(Name = "Boiler Chem. Amt.")]
        public Nullable<decimal> StoreBoilerChemicalAmount { get; set; }

        [Required]
        [Display(Name = "Boiler Chem. Rate/Bag")]
        public Nullable<decimal> StoreBoilerChemicalPerBagRate { get; set; }

        [Required]
        [Display(Name = "Boiler Chem. Rate/T.Cane")]
        public Nullable<decimal> StoreBoilerChemicalPerTonCaneRate { get; set; }

        [Required]
        [Display(Name = "Grease Oil Qty.")]
        public Nullable<decimal> StoreGreaseOilQuantity { get; set; }

        [Required]
        [Display(Name = "Grease Oil Amt.")]
        public Nullable<decimal> StoreGreaseOilAmount { get; set; }

        [Required]
        [Display(Name = "Grease Oil Rate/Bag.")]
        public Nullable<decimal> StoreGreaseOilPerBagRate { get; set; }

        [Required]
        [Display(Name = "Grease Oil Rate/T.Cane")]
        public Nullable<decimal> StoreGreaseOilPerTonCaneRate { get; set; }


        [Required]
        [Display(Name = "Syrup. Diversion (Qtl.)")]
        public Nullable<decimal> QuintalSyrupDiversion { get; set; }

        [Required]
        [Display(Name = "Sugar Pol")]
        public Nullable<decimal> SugarPol { get; set; }


        [Required]
        [Display(Name = "C Heavy Brix")]
        public decimal CHeavyBrix { get; set; }

        [Required]
        [Display(Name = "C Heavy Pol")]
        public decimal CHeavyPol { get; set; }

        [Required]
        [Display(Name = "C Heavy Purity")]
        public decimal CHeavyPurity { get; set; }

        [Required]
        [Display(Name = "R. Sugar Gain")]
        public decimal RawSugarGainDate { get; set; }

        [Required]
        [Display(Name = "Material Sent-out")]
        //[RegularExpression(@"^[1-9999]?$", ErrorMessage = "Material Sent-out type required!")]
        public int MaterialSentOut { get; set; }

        [Required]
        [Display(Name = "Sugar Type Produced")]
        //[RegularExpression(@"^[1-9999]?$", ErrorMessage = "Sugar type required!!")]
        public int SugarProduced { get; set; }

        [Required]
        [Display(Name ="Power Export From Disttillery (slop).")]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value only!")]
        public Nullable<decimal> PowerExportFromDistillery { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$",ErrorMessage ="Numeric value required!")]
        [Display(Name = "Power Export From Co-Gen")]

        public Nullable<decimal> PowerExportFromCogen { get; set; }

        [Required]
        [Display(Name = "Alcohol From Syrup")]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$", ErrorMessage = "Numeric value required!")]
        public Nullable<decimal> AlcoholFromCaneSyrup { get; set; }

        [Display(Name ="New Mill Crush")]
        public Nullable<decimal> NewMillCrush { get; set; }
        [Display(Name = "Old Mill Crush")]
        public Nullable<decimal> OldMillCrush { get; set; }

        [Required]
        [Display(Name = "Ash Sold")]
        public Nullable<decimal> AshSold { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
        public Nullable<decimal> AlcoholFromCHeavy { get; set; }
        public Nullable<decimal> AbsoluteAlcoholCHeavyDayRecovery { get; set; }
        public Nullable<decimal> AbsoluteAlcoholCHeavyToDateRecovery { get; set; }
        public Nullable<decimal> AbsoluteAlcoholFromBHeavy { get; set; }
        public Nullable<decimal> AbsoluteAlcoholBHeavyDayRecovery { get; set; }
        public Nullable<decimal> AbsoluteAlcoholBHeavyToDateRecovery { get; set; }
        public Nullable<decimal> AbsoluteAlcoholFromSyrupDayRecovery { get; set; }
        public Nullable<decimal> AbsoluteAlcoholFromSyrupToDateRecovery { get; set; }


        [Required]
        [Display(Name ="Mill House Trace")]
        public Nullable<bool> mill_house_trace { get; set; }

        [Required]
        [Display(Name ="Mill House pH")]
        public decimal mill_house_ph { get; set; }

        [Required]
        [Display(Name ="Mill House Pol")]
        public decimal mill_house_pol { get; set; }

        [Required]
        [Display(Name ="Under O.L Filter Trace")]
        public Nullable<bool> under_oliver_filter_trace { get; set; }

        [Required]
        [Display(Name = "Under O.L Filter pH")]
        public decimal under_oliver_filter_ph { get; set; }

        [Required]
        [Display(Name = "Under O.L Filter Pol")]
        public decimal under_oliver_filter_pol { get; set; }

        

        [Required]
        [Display(Name = "Syrup T.R.S")]
        public decimal syrup_trs { get; set; }

        [Required]
        [Display(Name = "Syrup R.S.")]
        public decimal syrup_rs { get; set; }

        [Required]
        [Display(Name = "Power Exp. From Sugar")]
        public decimal power_export_from_sugar { get; set; }
        [Required]
        [Display(Name ="Power Gen. From Sugar")]
        
        public decimal power_generate_from_sugar { get; set; }

        [Required]
        [Display(Name ="Refinery Water Consumption")]
        public int refinery_water_consumption { get; set; }

        [Required]
        [Display(Name = "Fiberized Pol in Cane")]
        public decimal fiberized_pol_in_cane { get; set; }

        [Required]
        [Display(Name = "Dextran in P.J. 100 Bx")]
        public decimal dextran_primary_juice { get; set; }
        [Required]
        [Display(Name = "Dectran in M.J. 100 Bx")]
        public decimal dextran_mixed_juice { get; set; }


    }
}