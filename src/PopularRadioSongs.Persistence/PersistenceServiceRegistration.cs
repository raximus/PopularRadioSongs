using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PopularRadioSongs.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PopularRadioSongsDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("PopularRadioSongsConnection")));

            return services;
        }
    }
}