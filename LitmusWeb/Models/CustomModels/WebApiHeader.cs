using System.Net;

namespace LitmusWeb.Models.CustomModels
{
    public class WebApiHeader
    {
        public HttpStatusCode statusCode { get; set; }

        public string statusMessage { get; set; }
    }
}