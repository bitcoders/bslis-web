using System.Collections.Generic;

namespace LitmusWeb.Models
{
    public class ErrorViewModel
    {
        public string ErrorTitle { get; set; }
        //public List<string> ErrorMessage { get; set; }
        public List<Errors> ErrorMessage { get; set; }
        //public string ErrorMessage { get; set; }
        
    }
    public class Errors
    {
        public string ErrorMessage { get; set; }
    }
}