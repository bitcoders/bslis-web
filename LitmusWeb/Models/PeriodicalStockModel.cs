using System;
using System.ComponentModel.DataAnnotations;

namespace LitmusWeb.Models
{
    public class PeriodicalStockModel
    {
        public System.Guid Id { get; set; }
        public int UnitCode { get; set; }
        public int SeasonCode { get; set; }

        [Display(Name = "Entry Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public System.DateTime EntryDate { get; set; }

        [Display(Name = "M.J HL")]
        public decimal MixedJuiceJuiceHl { get; set; }

        [Display(Name = "M.J Brix")]
        public decimal MixedJuiceJuiceBrix { get; set; }

        [Display(Name = "M.J Pol")]
        public decimal MixedJuiceJuicePol { get; set; }

        [Display(Name = "M.J Purity")]
        public decimal MixedJuiceJuicePurity { get; set; }

        [Display(Name = "M.J Avl. Sugar")]
        public decimal MixedJuiceJuiceAvailableSugar { get; set; }

        [Display(Name = "M.J Avl. Molasses")]
        public decimal MixedJuiceJuiceAvailableMolasses { get; set; }

        [Display(Name = "C.Juice HL")]
        public decimal ClearJuiceHl { get; set; }

        [Display(Name = "C.Juice Brix")]
        public decimal ClearJuiceBrix { get; set; }

        [Display(Name = "C.Juice PolL")]
        public decimal ClearJuicePol { get; set; }

        [Display(Name = "C.Juice Purity")]
        public decimal ClearJuicePurity { get; set; }

        [Display(Name = "C.J. Avl Sugar")]
        public decimal ClearJuiceAvailableSugar { get; set; }

        [Display(Name = "C.J Avl. Molasses")]
        public decimal ClearJuiceAvailableMolasses { get; set; }

        [Display(Name = "Syrup Juice HL")]
        public decimal SyrupJuiceHl { get; set; }

        [Display(Name = "Syrup Juice Brix")]
        public decimal SyrupJuiceBrix { get; set; }

        [Display(Name = "Syrup Juice Pol")]
        public decimal SyrupJuicePol { get; set; }

        [Display(Name = "Syrup Juice Purity")]
        public decimal SyrupJuicePurity { get; set; }

        [Display(Name = "S.J Avabl. Sugar")]
        public decimal SyrupJuiceAvailableSugar { get; set; }

        [Display(Name = "S.J Avabl. Molasses")]
        public decimal SyrupJuiceAvailableMolasses { get; set; }

        [Display(Name = "Seed HL")]
        public decimal SeedJuiceHl { get; set; }

        [Display(Name = "Seed Brix")]
        public decimal SeedJuiceBrix { get; set; }

        [Display(Name = "Seed Pol")]
        public decimal SeedJuicePol { get; set; }

        [Display(Name = "Seed Purity")]
        public decimal SeedJuicePurity { get; set; }

        [Display(Name = "Seed Avl. Sugar")]
        public decimal SeedJuiceAvailableSugar { get; set; }

        [Display(Name = "Seed Avl. Molasses")]
        public decimal SeedJuiceAvailableMolasses { get; set; }

        [Display(Name = "A. HL")]
        public decimal MassecuiteAJuiceHl { get; set; }

        [Display(Name = "A. Brix")]
        public decimal MassecuiteAJuiceBrix { get; set; }

        [Display(Name = "A. Pol")]
        public decimal MassecuiteAJuicePol { get; set; }

        [Display(Name = "A. Purity")]
        public decimal MassecuiteAJuicePurity { get; set; }

        [Display(Name = "A. Avl Sugar")]
        public decimal MassecuiteAJuiceAvailableSugar { get; set; }

        [Display(Name = "A. Avl. Molasses")]
        public decimal MassecuiteAJuiceAvailableMolasses { get; set; }

        [Display(Name = "C. HL")]
        public decimal MassecuiteCJuiceHl { get; set; }

        [Display(Name = "C. Brix")]
        public decimal MassecuiteCJuiceBrix { get; set; }

        [Display(Name = "C. Pol")]
        public decimal MassecuiteCJuicePol { get; set; }

        [Display(Name = "C. Purity")]
        public decimal MassecuiteCJuicePurity { get; set; }

        [Display(Name = "C. Avl. Sugar")]
        public decimal MassecuiteCJuiceAvailableSugar { get; set; }

        [Display(Name = "C. Avl. Molassess")]
        public decimal MassecuiteCJuiceAvailableMolasses { get; set; }

        [Display(Name = "C1 H.L")]
        public decimal MassecuiteCOneJuiceHl { get; set; }

        [Display(Name = "C1 Brix")]
        public decimal MassecuiteCOneJuiceBrix { get; set; }

        [Display(Name = "C1 Pol")]
        public decimal MassecuiteCOneJuicePol { get; set; }

        [Display(Name = "C1 Purity")]
        public decimal MassecuiteCOneJuicePurity { get; set; }

        [Display(Name = "C1 Avl. Sugar")]
        public decimal MassecuiteCOneJuiceAvailableSugar { get; set; }

        [Display(Name = "C1 Avl. Molasses")]
        public decimal MassecuiteCOneJuiceAvailableMolasses { get; set; }

        [Display(Name = "R1 H.L")]
        public decimal MassecuiteROneJuiceHl { get; set; }

        [Display(Name = "R1 Brix")]
        public decimal MassecuiteROneJuiceBrix { get; set; }

        [Display(Name = "R1 Pol")]
        public decimal MassecuiteROneJuicePol { get; set; }

        [Display(Name = "R1 Purity")]
        public decimal MassecuiteROneJuicePurity { get; set; }

        [Display(Name = "R1 Avl. sugar")]
        public decimal MassecuiteROneJuiceAvailableSugar { get; set; }

        [Display(Name = "R1 Avl. Molasses")]
        public decimal MassecuiteROneJuiceAvailableMolasses { get; set; }

        [Display(Name = "B H.L")]
        public decimal MassecuiteBJuiceHl { get; set; }

        [Display(Name = "B Brix")]
        public decimal MassecuiteBJuiceBrix { get; set; }

        [Display(Name = "B Pol")]
        public decimal MassecuiteBJuicePol { get; set; }

        [Display(Name = "B Purity")]
        public decimal MassecuiteBJuicePurity { get; set; }

        [Display(Name = "B Avl. Sugar")]
        public decimal MassecuiteBJuiceAvailableSugar { get; set; }

        [Display(Name = "B Avl. Molasses")]
        public decimal MassecuiteBJuiceAvailableMolasses { get; set; }

        [Display(Name = "R2 H.L.")]
        public decimal MassecuiteRTwoJuiceHl { get; set; }

        [Display(Name = "R2 Brix")]
        public decimal MassecuiteRTwoJuiceBrix { get; set; }

        [Display(Name = "R2 Pol")]
        public decimal MassecuiteRTwoJuicePol { get; set; }

        [Display(Name = "R2 Purity")]
        public decimal MassecuiteRTwoJuicePurity { get; set; }

        [Display(Name = "R2 Avl. Sugar")]
        public decimal MassecuiteRTwoJuiceAvailableSugar { get; set; }

        [Display(Name = "R2 Avl. Molasses")]
        public decimal MassecuiteRTwoJuiceAvailableMolasses { get; set; }

        [Display(Name = "R3 H.L.")]
        public decimal MassecuiteRThreeJuiceHl { get; set; }

        [Display(Name = "R3 Brix")]
        public decimal MassecuiteRThreeJuiceBrix { get; set; }


        [Display(Name = "R3 Pol")]
        public decimal MassecuiteRThreeJuicePol { get; set; }

        [Display(Name = "R3 Purity")]
        public decimal MassecuiteRThreeJuicePurity { get; set; }

        [Display(Name = "R3 Avl. Sugar")]
        public decimal MassecuiteRThreeJuiceAvailableSugar { get; set; }


        [Display(Name = "R3 Avl. Molasses")]
        public decimal MassecuiteRThreeJuiceAvailableMolasses { get; set; }

        [Display(Name = "A Heavy H.L")]
        public decimal MolassesAHeavyJuiceHl { get; set; }

        [Display(Name = "A Heavy Brix")]
        public decimal MolassesAHeavyJuiceBrix { get; set; }

        [Display(Name = "A Heavy Pol")]
        public decimal MolassesAHeavyJuicePol { get; set; }

        [Display(Name = "A Heavy Purity")]
        public decimal MolassesAHeavyJuicePurity { get; set; }

        [Display(Name = "A Heavy Avl. Sugar")]
        public decimal MolassesAHeavyJuiceAvailableSugar { get; set; }

        [Display(Name = "A Heavy Avl. Molasses")]
        public decimal MolassesAHeavyJuiceAvailableMolasses { get; set; }

        [Display(Name = "A Light H.L")]
        public decimal MolassesALightJuiceHl { get; set; }

        [Display(Name = "A Light Brix")]
        public decimal MolassesALightJuiceBrix { get; set; }

        [Display(Name = "A Light Pol")]
        public decimal MolassesALightJuicePol { get; set; }

        [Display(Name = "A Light Purity")]
        public decimal MolassesALightJuicePurity { get; set; }

        [Display(Name = "A Light Avl. Sugar")]
        public decimal MolassesALightJuiceAvailableSugar { get; set; }

        [Display(Name = "A Light Avl. Molasses")]
        public decimal MolassesALightJuiceAvailableMolasses { get; set; }

        [Display(Name = "B Heavy H.L")]
        public decimal MolassesBHeavyJuiceHl { get; set; }

        [Display(Name = "B Heavy Brix")]
        public decimal MolassesBHeavyJuiceBrix { get; set; }

        [Display(Name = "B Heavy Pol")]
        public decimal MolassesBHeavyJuicePol { get; set; }

        [Display(Name = "B Heavy Purity")]
        public decimal MolassesBHeavyJuicePurity { get; set; }

        [Display(Name = "B Heavy Avl. Sugar")]
        public decimal MolassesBHeavyJuiceAvailableSugar { get; set; }

        [Display(Name = "B Heavy Avl. Molasses")]
        public decimal MolassesBHeavyJuiceAvailableMolasses { get; set; }

        [Display(Name = "C Light H.L")]
        public decimal MolassesCLightJuiceHl { get; set; }

        [Display(Name = "C Light Brix")]
        public decimal MolassesCLightJuiceBrix { get; set; }

        [Display(Name = "C Light Pol")]
        public decimal MolassesCLightJuicePol { get; set; }


        [Display(Name = "C Light Purity")]
        public decimal MolassesCLightJuicePurity { get; set; }


        [Display(Name = "C Light Avl Sugar")]
        public decimal MolassesCLightJuiceAvailableSugar { get; set; }

        [Display(Name = "C Light Avl Molasses")]
        public decimal MolassesCLightJuiceAvailableMolasses { get; set; }


        [Display(Name = "C1 H.L")]
        public decimal MolassesCOneJuiceHl { get; set; }

        [Display(Name = "C1 Birx")]
        public decimal MolassesCOneJuiceBrix { get; set; }

        [Display(Name = "C1 Pol")]
        public decimal MolassesCOneJuicePol { get; set; }

        [Display(Name = "C1 Purity")]
        public decimal MolassesCOneJuicePurity { get; set; }

        [Display(Name = "C1 Avl. Sugar")]
        public decimal MolassesCOneJuiceAvailableSugar { get; set; }

        [Display(Name = "C1 Avl. Molasses")]
        public decimal MolassesCOneJuiceAvailableMolasses { get; set; }

        [Display(Name = "R1 Heavy H.L")]
        public decimal MolassesROneHeavyJuiceHl { get; set; }

        [Display(Name = "R1 Heavy Brix")]
        public decimal MolassesROneHeavyJuiceBrix { get; set; }

        [Display(Name = "R1 Heavy Pol")]
        public decimal MolassesROneHeavyJuicePol { get; set; }

        [Display(Name = "R1 Heavy Purity")]
        public decimal MolassesROneHeavyJuicePurity { get; set; }


        [Display(Name = "R1 Heavy Avl. Sugar")]
        public decimal MolassesROneHeavyJuiceAvailableSugar { get; set; }

        [Display(Name = "R1 Heavy Avl Molasses")]
        public decimal MolassesROneHeavyJuiceAvailableMolasses { get; set; }


        [Display(Name = "R2 Heavy H.L")]
        public decimal MolassesRTwoJuiceHl { get; set; }

        [Display(Name = "R2 Heavy Brix")]
        public decimal MolassesRTwoJuiceBrix { get; set; }

        [Display(Name = "R2 Heavy Pol")]
        public decimal MolassesRTwoJuicePol { get; set; }


        [Display(Name = "R2 Heavy Purity")]
        public decimal MolassesRTwoJuicePurity { get; set; }

        [Display(Name = "R2 Heavy Avl. Sugar")]
        public decimal MolassesRTwoJuiceAvailableSugar { get; set; }

        [Display(Name = "R2 Heavy Avl. Purity")]
        public decimal MolassesRTwoJuiceAvailableMolasses { get; set; }

        [Display(Name = "R3 Heavy H.L")]
        public decimal MolassesRThreeHeavyJuiceHl { get; set; }

        [Display(Name = "R3 Heavy Brix")]
        public decimal MolassesRThreeHeavyJuiceBrix { get; set; }

        [Display(Name = "R3 Heavy Pol")]
        public decimal MolassesRThreeHeavyJuicePol { get; set; }

        [Display(Name = "R3 Heavy Purity")]
        public decimal MolassesRThreeHeavyJuicePurity { get; set; }

        [Display(Name = "R3 Heavy Avl. Sugar")]
        public decimal MolassesRThreeHeavyJuiceAvailableSugar { get; set; }


        [Display(Name = "R3 Heavy Avl. Mol.")]
        public decimal MolassesRThreeHeavyJuiceAvailableMolasses { get; set; }

        [Display(Name = "Unweighted Sugar H.L")]
        public decimal SugarUnweightedJuiceHl { get; set; }

        [Display(Name = "Unweighted Sugar Brix")]
        public decimal SugarUnweightedJuiceBrix { get; set; }

        [Display(Name = "Unweighted Sugar Pol")]
        public decimal SugarUnweightedJuicePol { get; set; }

        [Display(Name = "Unweighted Sugar Purity")]
        public decimal SugarUnweightedJuicePurity { get; set; }

        [Display(Name = "Unweighted Sugar Avl. Sugar")]
        public decimal SugarUnweightedJuiceAvailableSugar { get; set; }

        [Display(Name = "Unweighted Sugar Avl. Mol")]
        public decimal SugarUnweightedJuiceAvailableMolasses { get; set; }

        [Display(Name = "Fine Liquor H.L")]
        public decimal FineLiqorJuiceHl { get; set; }

        [Display(Name = "Fine Liquor Brix")]
        public decimal FineLiqorJuiceBrix { get; set; }

        [Display(Name = "Fine Liquor Pol")]
        public decimal FineLiqorJuicePol { get; set; }

        [Display(Name = "Fine Liquor Purity")]
        public decimal FineLiqorJuicePurity { get; set; }

        [Display(Name = "Fine Liquor Avl. Sugar")]
        public decimal FineLiqorJuiceAvailableSugar { get; set; }

        [Display(Name = "Fine Liquor Avl. Molasses")]
        public decimal FineLiqorJuiceAvailableMolasses { get; set; }

        [Display(Name = "Total Juice H.L")]
        public decimal TotalJuiceHl { get; set; }

        [Display(Name = "Total Juice Brix")]
        public decimal TotalJuiceBrix { get; set; }

        [Display(Name = "Total Juice Pol")]
        public decimal TotalJuicePol { get; set; }

        [Display(Name = "Total Juice Purity")]
        public decimal TotalJuicePurity { get; set; }

        [Display(Name = "Total Juice H.L")]
        public decimal TotalJuiceAvailableSugar { get; set; }

        [Display(Name = "Total Juice Avl. Molasses")]
        public decimal TotalJuiceAvailableMolasses { get; set; }

        [Display(Name = "Bagasse Saved")]
        public decimal BagasseSaved { get; set; }

        [Display(Name = "Bagasses To Distillery")]
        public decimal BagasseToDistillery { get; set; }

        [Display(Name = "Bagasse Purchased")]
        public decimal BagassePurchased { get; set; }

        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "CreatedBy")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created At")]
        public Nullable<System.DateTime> CreatedAt { get; set; }
    }
}