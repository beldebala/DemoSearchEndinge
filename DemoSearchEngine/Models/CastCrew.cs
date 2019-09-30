using DemoSearchEngine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSearchEngine.Models
{
    public class CastCrew
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public float Rating { get; set; }

        public List<Movie> Movies { get; set; }

        public SearchCategory Category { get; set; }
    }
}
