using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace iTunesSearchMobileV3.Data
{
    public class TrackClickCountDbContext : DbContext
    {
        public TrackClickCountDbContext(DbContextOptions<TrackClickCountDbContext> options)
        : base(options)
        {
        }

        internal DbSet<TrackClickCount> TrackClickCount => Set<TrackClickCount>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("TrackClickCount");
        }
    }
}
