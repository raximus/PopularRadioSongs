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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSeeding((context, _) =>
            {
                if (!context.Set<Artist>().Any() && !context.Set<Song>().Any())
                {
                    var dawidPodsiadlo = new Artist("Dawid Podsiadło");
                    var sylwiaGrzeszczak = new Artist("Sylwia Grzeszczak");
                    var duaLipa = new Artist("Dua Lipa");
                    var sanah = new Artist("Sanah");
                    var rihanna = new Artist("Rihanna");
                    var davidGuetta = new Artist("David Guetta");
                    var eminem = new Artist("Eminem");
                    var galantis = new Artist("Galantis");
                    var liber = new Artist("Liber");

                    context.Set<Artist>().Add(dawidPodsiadlo);
                    context.Set<Artist>().Add(sylwiaGrzeszczak);
                    context.Set<Artist>().Add(duaLipa);
                    context.Set<Artist>().Add(sanah);
                    context.Set<Artist>().Add(rihanna);
                    context.Set<Artist>().Add(davidGuetta);
                    context.Set<Artist>().Add(eminem);
                    context.Set<Artist>().Add(galantis);
                    context.Set<Artist>().Add(liber);

                    context.Set<Song>().Add(new Song("Małomiasteczkowy", [dawidPodsiadlo]));
                    context.Set<Song>().Add(new Song("Nie Ma Fal", [dawidPodsiadlo]));
                    context.Set<Song>().Add(new Song("To Co Masz Ty!", [dawidPodsiadlo]));
                    context.Set<Song>().Add(new Song("Ostatnia Nadzieja", [dawidPodsiadlo, sanah]));
                    context.Set<Song>().Add(new Song("Tamta Dziewczyna", [sylwiaGrzeszczak]));
                    context.Set<Song>().Add(new Song("Flirt", [sylwiaGrzeszczak]));
                    context.Set<Song>().Add(new Song("Bang Bang", [sylwiaGrzeszczak]));
                    context.Set<Song>().Add(new Song("Mijamy Się", [sylwiaGrzeszczak, liber]));
                    context.Set<Song>().Add(new Song("Be The One", [duaLipa]));
                    context.Set<Song>().Add(new Song("Don't Start Now", [duaLipa]));
                    context.Set<Song>().Add(new Song("Bang Bang", [duaLipa]));
                    context.Set<Song>().Add(new Song("Houdini", [duaLipa]));
                    context.Set<Song>().Add(new Song("Szampan", [sanah]));
                    context.Set<Song>().Add(new Song("No Sory", [sanah]));
                    context.Set<Song>().Add(new Song("Only Girl", [rihanna]));
                    context.Set<Song>().Add(new Song("Where Have You Been", [rihanna]));
                    context.Set<Song>().Add(new Song("Who's That Chick?", [rihanna, davidGuetta]));
                    context.Set<Song>().Add(new Song("Memories", [davidGuetta]));
                    context.Set<Song>().Add(new Song("Would I Lie To You", [davidGuetta]));
                    context.Set<Song>().Add(new Song("Lose Yourself", [eminem]));
                    context.Set<Song>().Add(new Song("Houdini", [eminem]));
                    context.Set<Song>().Add(new Song("Runaway", [galantis]));
                    context.Set<Song>().Add(new Song("Bang Bang!", [galantis]));
                    context.Set<Song>().Add(new Song("Skarby", [liber]));

                    context.SaveChanges();
                }
            });
        }
    }
}