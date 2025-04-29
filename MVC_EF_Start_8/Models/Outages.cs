namespace MVC_EF_Start_8.Models
{
    public class OutageRecord
    {
        public string period { get; set; }
        public string facility { get; set; }
        public string facilityName { get; set; }
        public string generator { get; set; }
        public string capacity { get; set; }
        public string outage { get; set; }
        public string percentOutage { get; set; }
    }

    public class Response
    {
        public List<OutageRecord> data { get; set; }
    }

    public class Root
    {
        public Response response { get; set; }
    }
}