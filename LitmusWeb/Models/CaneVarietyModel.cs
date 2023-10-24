namespace LitmusWeb.Models
{
    public class CaneVarietyModel
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public int GroupCode { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }

        public virtual CaneVarietyGroupModel CaneVarietyGroupModel { get; set; }
    }
}