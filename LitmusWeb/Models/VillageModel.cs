namespace LitmusWeb.Models
{
    public class VillageModel
    {
        public int Id { get; set; }
        public int UnitCode { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        //public virtual MasterUnitModel MasterUnitModel { get; set; }
    }
}