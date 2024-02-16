using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CustomModels
{
    public class ResponseStatusModel
    {
        public int status_code { get; set; }
        public string status_message { get; set; }
    }
}
