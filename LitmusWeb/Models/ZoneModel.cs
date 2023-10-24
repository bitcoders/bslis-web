namespace LitmusWeb.Models
{
    public class ZoneModel
    {
        public int Id { get; set; }
        public int UnitCode { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual MasterUnitModel MasterUnitModel { get; set; }
    }
}