using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Services;
using System.Reflection;

namespace PopularRadioSongs.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(options => options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient<IPlaybacksImporterService, PlaybacksImporterService>();
            services.AddSingleton<IRadioNamesService, RadioNamesService>();

            return services;
        }
    }
}