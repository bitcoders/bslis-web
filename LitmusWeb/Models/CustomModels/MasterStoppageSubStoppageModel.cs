using System.Collections.Generic;
using System.Web.Mvc;


namespace LitmusWeb.Models.CustomModels
{
    public class MasterStoppageSubStoppageModel
    {
        public List<SelectListItem> StoppageMasterTypes;
        public List<SelectListItem> StoppageSubTypes;

        public int StoppageMasterId { get; set; }
        public int StoppageSubId { get; set; }

        public MasterStoppageSubStoppageModel()
        {
            this.StoppageMasterTypes = new List<SelectListItem>();
            this.StoppageSubTypes = new List<SelectListItem>();
        }

    }
}