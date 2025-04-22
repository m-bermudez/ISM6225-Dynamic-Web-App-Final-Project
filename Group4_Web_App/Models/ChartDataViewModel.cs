using System.Collections.Generic;

namespace Group4_Web_App.Models
{
    public class ChartDataViewModel
    {
        public List<string> Labels { get; set; }
        public List<int> Values { get; set; }
        public string DataSetLabel { get; set; }
        public List<Dataset> MultiDatasetValues { get; set; }
    }

    public class Dataset
    {
        public string Label { get; set; }
        public List<int> Data { get; set; }
        public string BackgroundColor { get; set; }
    }
}