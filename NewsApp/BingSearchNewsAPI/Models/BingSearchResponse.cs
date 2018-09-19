using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Models
{
    public class BingSearchResponse
    {
        public string Type { get; set; }
        public string ReadLink { get; set; }
        public QueryContext QueryContext { get; set; }
        public int TotalEstimatedMatches { get; set; }
        public List<Sort> Sort { get; set; }
        public List<Value> Value { get; set; }
    }
}
