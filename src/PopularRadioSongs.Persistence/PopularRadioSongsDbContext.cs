using Microsoft.EntityFrameworkCore;

namespace PopularRadioSongs.Persistence
{
    public class PopularRadioSongsDbContext : DbContext
    {
        public PopularRadioSongsDbContext(DbContextOptions<PopularRadioSongsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PopularRadioSongsDbContext).Assembly);
        }
    }
}