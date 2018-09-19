using System;
using System.Collections.Generic;
using System.Text;

namespace NewsApp.Models
{
    public class QueryContext
    {
        public string OriginalQuery { get; set; }
        public bool AdultIntent { get; set; }
    }
}
