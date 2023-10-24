using System;
using System.Collections.Generic;

namespace LitmusWeb.Models
{
    public class CaneVarietyGroupModel
    {
        public CaneVarietyGroupModel()
        {
            this.CaneVarieties = new HashSet<CaneVarietyModel>();
        }

        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<CaneVarietyModel> CaneVarieties { get; set; }
    }
}