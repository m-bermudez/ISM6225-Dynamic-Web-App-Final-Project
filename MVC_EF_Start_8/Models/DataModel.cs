using System.Collections.Generic;

namespace Group4MVC_EF_Start_8_Web_App.Models
{
    public class Park
    {
        public string id { get; set; }
        public string fullName { get; set; }
        public string parkCode { get; set; }
        public string description { get; set; }
        public string latLong { get; set; }
    }

    public class Parks
    {
        public List<Park> data { get; set; }
    }
}

