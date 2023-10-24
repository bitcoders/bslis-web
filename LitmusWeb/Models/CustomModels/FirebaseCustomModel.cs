namespace LitmusWeb.Models.CustomModels
{
    public class FirebaseCustomModel
    {
        public string message { get; set; }
    }
    public class FirebaseMessageParameterModel
    {
        public int MessageType { get; set; }
        public int UnitCode { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}