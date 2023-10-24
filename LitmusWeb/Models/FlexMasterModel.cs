using System.Collections.Generic;

namespace LitmusWeb.Models
{
    public class FlexMasterModel
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public virtual ICollection<FlexSubMasterModel> FlexSubMasters { get; set; }
    }
}