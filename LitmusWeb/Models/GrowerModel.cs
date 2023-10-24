namespace LitmusWeb.Models
{
    public class GrowerModel
    {
        public int Id { get; set; }
        public int UnitCode { get; set; }
        public int VillageCode { get; set; }
        public int Code { get; set; }
        public string UniqueCode { get; set; }
        public string Name { get; set; }
        public string RelativeName { get; set; }
        public string FlexRelationCode { get; set; }
        public string MobileNo { get; set; }
        public string Uid { get; set; }
        public bool IsActive { get; set; }
    }
}