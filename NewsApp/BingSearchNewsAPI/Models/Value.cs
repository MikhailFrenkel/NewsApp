using System;
using System.Collections.Generic;

namespace SearchNewsAPI.Models
{
    public class Value
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public List<Provider> Provider { get; set; }
        public DateTime DatePublished { get; set; }
        public Image Image { get; set; }
        public List<About> About { get; set; }
    }
}
