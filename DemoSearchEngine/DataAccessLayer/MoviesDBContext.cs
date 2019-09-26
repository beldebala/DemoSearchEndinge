using DemoSearchEngine.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace DemoSearchEngine.DataAccessLayer
{
    public class MoviesDBContext : DbContext
    {
        public MoviesDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Theater> Theaters { get; set; }

        public DbSet<Location> Locations {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TheaterMovies>()
                .HasKey(s => new { s.MovieId, s.TheaterId });            
        }
    }
}
