using DemoSearchEngine.Enums;
using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSearchEngine.Models
{
    public class Movie
    {
        public int ID { get; set; } 
        
        [Required(ErrorMessage ="Please enter valid movie name")]        
        public string Name { get; set; }

        public string Link { get; set; }        
        public DateTime ReleaseDate { get; set; }        
        public string Genre { get; set; }
        public int Rating { get; set; }
        public List<TheaterMovies> TheaterMovies{ get; set; }
        public SearchCategory Category { get; set; }        
    }
}
