using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ReturneeManager.Application.Interfaces.Repositories;
using ReturneeManager.Application.Interfaces.Services.Storage;
using ReturneeManager.Application.Interfaces.Services.Storage.Provider;
using ReturneeManager.Application.Interfaces.Serialization.Serializers;
using ReturneeManager.Application.Serialization.JsonConverters;
using ReturneeManager.Infrastructure.Repositories;
using ReturneeManager.Infrastructure.Services.Storage;
using ReturneeManager.Application.Serialization.Options;
using ReturneeManager.Infrastructure.Services.Storage.Provider;
using ReturneeManager.Application.Serialization.Serializers;

namespace ReturneeManager.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
                .AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<IPersonRepository, PersonRepository>()
                .AddTransient<IBrandRepository, BrandRepository>()
                .AddTransient<IIdTypeRepository, IdTypeRepository>()
                .AddTransient<IGenderRepository, GenderRepository>()
                .AddTransient<IDistrictRepository, DistrictRepository>()
                .AddTransient<IDivisionRepository, DivisionRepository>()
                .AddTransient<IUpazilaRepository, UpazilaRepository>()
                .AddTransient<IWardRepository, WardRepository>()
                .AddTransient<IFromCountryRepository, FromCountryRepository>()
                .AddTransient<IDocumentRepository, DocumentRepository>()
                .AddTransient<IDocumentTypeRepository, DocumentTypeRepository>()
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        public static IServiceCollection AddExtendedAttributesUnitOfWork(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IExtendedAttributeUnitOfWork<,,>), typeof(ExtendedAttributeUnitOfWork<,,>));
        }

        public static IServiceCollection AddServerStorage(this IServiceCollection services)
            => AddServerStorage(services, null);

        public static IServiceCollection AddServerStorage(this IServiceCollection services, Action<SystemTextJsonOptions> configure)
        {
            return services
                .AddScoped<IJsonSerializer, SystemTextJsonSerializer>()
                .AddScoped<IStorageProvider, ServerStorageProvider>()
                .AddScoped<IServerStorageService, ServerStorageService>()
                .AddScoped<ISyncServerStorageService, ServerStorageService>()
                .Configure<SystemTextJsonOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
                        configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
        }
    }
}