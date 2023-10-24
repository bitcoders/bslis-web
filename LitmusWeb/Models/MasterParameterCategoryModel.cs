using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace LitmusWeb.Models
{
    public class MasterParameterCategoryModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MasterParameterCategoryModel()
        {
            this.MasterParameterSubCategories = new HashSet<MasterParameterSubCategoryModel>();
        }

        [Key]
        [Required]
        [ForeignKey("Code")]
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dd-MMM-yyyy}")]
        public System.DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MasterParameterSubCategoryModel> MasterParameterSubCategories { get; set; }
    }

}