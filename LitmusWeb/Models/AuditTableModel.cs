namespace LitmusWeb.Models
{
    public class AuditTableModel
    {
        public int ID { get; set; }
        public string KeyFieldID { get; set; }
        public int AuditActionTypeENUM { get; set; }
        public System.DateTime DateTimeStamp { get; set; }
        public string DataModel { get; set; }
        public string Changes { get; set; }
        public string ValueBefore { get; set; }
        public string ValueAfter { get; set; }
    }

    public enum AuditActionType
    {
        Create = 1,
        Update,
        Delete
    }

}