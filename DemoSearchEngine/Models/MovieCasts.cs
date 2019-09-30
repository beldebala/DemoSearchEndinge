using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSearchEngine.Models
{
    public class MovieCasts
    {
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public int CastCrewId { get; set; }

        public CastCrew CastCrew { get; set; }
    }
}
