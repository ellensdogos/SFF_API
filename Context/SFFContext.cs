using System;
using SFF_API.Models;
using Microsoft.EntityFrameworkCore;

namespace SFF_API.Context
{
    public class SFFContext : DbContext
    {
        public SFFContext(DbContextOptions<SFFContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Trivia> Trivias { get; set; }
        public DbSet<MovieRental> MovieRentals { get; set; }

    }
}
