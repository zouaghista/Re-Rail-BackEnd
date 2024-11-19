using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using ReRailBackEnd.Entities;

namespace ReRailBackEnd.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrackPoint>().HasOne(e => e.trackSnapShot);
        }
        public DbSet<TrackSnapShot> Images { get; set; }
        public DbSet<TrackPoint> TrackPoints { get; set; }
    }
}
