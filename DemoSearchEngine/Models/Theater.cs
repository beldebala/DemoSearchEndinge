﻿using DemoSearchEngine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSearchEngine.Models
{
    public class Theater
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public List<TheaterMovies> TheaterMovies{ get; set; }

        public SearchCategory Category { get; set; }

        public Location Location { get; set; }
    }
}
