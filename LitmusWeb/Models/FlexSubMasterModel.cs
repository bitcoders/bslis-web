namespace LitmusWeb.Models
{
    public class FlexSubMasterModel
    {
        public int Code { get; set; }
        public int FlexMasterCode { get; set; }
        public string Value { get; set; }
        public string DataTypeOfValue { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }

        public virtual FlexMasterModel FlexMasterModel { get; set; }
    }
}