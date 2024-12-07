using Microsoft.EntityFrameworkCore;
using PopularRadioSongs.Core.Entities;

namespace PopularRadioSongs.Persistence
{
    public class PopularRadioSongsDbContext : DbContext
    {
        public DbSet<Artist> Artists => Set<Artist>();
        public DbSet<Song> Songs => Set<Song>();
        public DbSet<Playback> Playbacks => Set<Playback>();

        public PopularRadioSongsDbContext(DbContextOptions<PopularRadioSongsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PopularRadioSongsDbContext).Assembly);
        }
    }
}