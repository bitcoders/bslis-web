namespace LitmusWeb.Models
{
    public class NotificationTickerModel
    {
        public int id { get; set; }
        public string ValidUnits { get; set; }
        public string DisplayText { get; set; }
        public System.DateTime ValidFrom { get; set; }
        public System.DateTime ValidTill { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedAt { get; set; }
    }
}