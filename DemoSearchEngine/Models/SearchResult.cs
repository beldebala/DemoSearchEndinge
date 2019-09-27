using DemoSearchEngine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSearchEngine.Models
{
    public class SearchResult
    {
        public int ID { get; set; }
        public SearchCategory Category { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

        public string Genre { get; set; }
    }
}
