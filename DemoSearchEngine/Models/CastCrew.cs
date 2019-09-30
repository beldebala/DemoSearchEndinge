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

        public string Name { get { return string.Format("{0} {1}", FirstName, LastName); } }

        public float Rating { get; set; }

        public List<Movie> Movies { get; set; }

        public SearchCategory Category { get; set; }
    }
}
