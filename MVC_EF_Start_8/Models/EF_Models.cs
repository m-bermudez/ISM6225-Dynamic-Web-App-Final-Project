using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_EF_Start_8.Models
{
    public class Company
    {
        public string Id { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public bool isEnabled { get; set; }
        public string type { get; set; }
        public string iexId { get; set; }
        public List<Quote> Quotes { get; set; }
    }

    public class Quote
    {
        public int Id { get; set; }
        public string date { get; set; }
        public float open { get; set; }
        public float high { get; set; }
        public float low { get; set; }
        public float close { get; set; }
        public int volume { get; set; }
        public int unadjustedVolume { get; set; }
        public float change { get; set; }
        public float changePercent { get; set; }
        public float vwap { get; set; }
        public string label { get; set; }
        public float changeOverTime { get; set; }
        public string ClassDemo { get; set; }
        public Company Company { get; set; }
    }

    public class ChartRoot
    {
        public Quote[] chart { get; set; }
    }
}