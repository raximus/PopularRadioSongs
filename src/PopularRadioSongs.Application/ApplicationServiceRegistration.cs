using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PopularRadioSongs.Application.Behaviors;
using PopularRadioSongs.Application.Contracts;
using PopularRadioSongs.Application.Options;
using PopularRadioSongs.Application.Services;
using System.Reflection;

namespace PopularRadioSongs.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient<IPlaybacksImporterService, PlaybacksImporterService>();
            services.AddSingleton<IRadioNamesService, RadioNamesService>();

            services.AddOptions<AppOptions>().BindConfiguration(nameof(AppOptions)).ValidateDataAnnotations().ValidateOnStart();

            return services;
        }
    }
}