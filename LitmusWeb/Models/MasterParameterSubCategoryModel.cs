using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LitmusWeb.Models
{

    public class MasterParameterSubCategoryModel
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MasterParameterSubCategoryModel()
        {
            this.KeySampleAnalyses = new HashSet<KeySampleAnalysisModel>();
            this.MassecuiteAnalyses = new HashSet<MassecuiteAnalysModel>();
            this.MolassesAnalyses = new HashSet<MolassesAnalysModel>();
            this.MeltingAnalyses = new HashSet<MeltingAnalysModel>();
        }

        [Required]
        [ForeignKey("Code")]
        [Display(Name = "Master Parameter Code", Description = "Provide code of Master Parameter, of which subset is the current parameter")]
        public int MasterCategory { get; set; }

        [Display(Name = "Master Parameter Name", Description = "Provide Name of Master Parameter , of which subset is the current parameter")]
        public string MasterCategoryName { get; set; }

        [Key]
        [Required]
        [Display(Name = "Code", Description = "A unique code for Parameter Category.")]
        public int Code { get; set; }

        [Required]
        [Display(Name = "Name", Description = "A unique name for Parameter Category.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description", Description = "Description of Parameter Category.")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Is Active", Prompt = "Is vehicle active?", Description = "Is vehicle active?")]
        public Nullable<bool> IsActive { get; set; }

        [Display(Name = "Created Date")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }


        public virtual MasterParameterCategoryModel MasterParameterCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KeySampleAnalysisModel> KeySampleAnalyses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MassecuiteAnalysModel> MassecuiteAnalyses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MolassesAnalysModel> MolassesAnalyses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeltingAnalysModel> MeltingAnalyses { get; set; }
    }

}