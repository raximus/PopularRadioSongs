using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Persistence.Repositories;

namespace PopularRadioSongs.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PopularRadioSongsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PopularRadioSongsConnection")));

            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<ISongRepository, SongRepository>();
            services.AddScoped<IImportRepository, ImportRepository>();

            return services;
        }
    }
}