//using GameDashboardProject.Application.Repositories;
//using GameDashboardProject.Application.UnitOfWorks;
//using GameDashboardProject.Domain.Identities;
//using GameDashboardProject.Infrastructure.Mongo;
//using GameDashboardProject.Infrastructure.MongoServices;
//using GameDashboardProject.Persistence.Context;
//using GameDashboardProject.Persistence.Repositories;
//using GameDashboardProject.Persistence.UnitOfWorks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using MongoDB.Driver;
//using Microsoft.Extensions.Configuration;

//namespace GameDashboardProject.Persistence
//{
//    public static class ServiceRegistration
//    {
//        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
//        {
//            // MySQL Configuration
//            services.AddDbContext<GameDashboardDbContext>(opt =>
//                opt.UseMySql(configuration.GetConnectionString("DefaultConnection"),
//                    new MySqlServerVersion(new Version(8, 0, 2)),
//                    mySqlOptions => mySqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null)));

//            //// MongoDB Configuration
//            var mongoSettings = configuration.GetSection("ConnectionStrings:MongoConnection").Get<MongoSettings>();
//            services.AddSingleton(mongoSettings);
//            services.AddSingleton<IMongoClient>(s =>
//                new MongoClient(mongoSettings.ConnectionString));

//            //// MongoDB Configuration
//            //var mongoSettings = configuration.GetSection("MongoSettings").Get<MongoSettings>();
//            //services.AddSingleton(mongoSettings);
//            //services.AddSingleton<IMongoClient>(s =>
//            //    new MongoClient(mongoSettings.ConnectionString));

//            // Register BuildingConfigurationRepository for IBuildingConfigurationRepository
//            services.AddScoped<IBuildingRepository, BuildingRepository>(s =>
//                new BuildingRepository(
//                    s.GetRequiredService<IMongoClient>(),
//                    mongoSettings.DatabaseName));

//            // Identity Configuration
//            services.AddIdentity<AppUser, AppRole>()
//                .AddEntityFrameworkStores<GameDashboardDbContext>()
//                .AddDefaultTokenProviders();

//            // Data Protection
//            services.AddDataProtection();

//            // Dependency Injection for MySQL Repositories and UnitOfWork
//            services.AddScoped(typeof(IMySqlReadRepository<>), typeof(MySqlReadRepository<>));
//            //services.AddScoped(typeof(IBuildingRepository), typeof(BuildingRepository));
//            services.AddScoped(typeof(IMySqlWriteRepository<>), typeof(MySqlWriteRepository<>));
//            services.AddScoped<IMySqlUnitOfWork, MySqlUnitOfWork>();

//            // Dependency Injection for MongoDB Repositories and UnitOfWork
//            services.AddScoped(typeof(IMongoReadRepository<>), typeof(MongoReadRepository<>));
//            services.AddScoped(typeof(IMongoWriteRepository<>), typeof(MongoWriteRepository<>));
//            services.AddScoped<IMongoUnitOfWork>(s => new MongoUnitOfWork(
//                s.GetRequiredService<IMongoClient>(),
//                mongoSettings.DatabaseName,
//                s.GetRequiredService<IBuildingRepository>()));
//        }
//    }
//}
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using MongoDB.Driver;
//using GameDashboardProject.Application.Repositories;
//using GameDashboardProject.Persistence.Repositories;
//using GameDashboardProject.Infrastructure.Mongo;
//using GameDashboardProject.Infrastructure.MongoServices;
//using GameDashboardProject.Application.UnitOfWorks;
//using GameDashboardProject.Domain.Identities;
//using GameDashboardProject.Persistence.Context;
//using GameDashboardProject.Persistence.UnitOfWorks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace GameDashboardProject.Persistence
//{
//    public static class ServiceRegistration
//    {
//        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
//        {
//            // MySQL Configuration
//            services.AddDbContext<GameDashboardDbContext>(opt =>
//                opt.UseMySql(configuration.GetConnectionString("DefaultConnection"),
//                    new MySqlServerVersion(new Version(8, 0, 2)),
//                    mySqlOptions => mySqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null)));

//            // MongoDB Configuration
//            var mongoSettings = configuration.GetSection("ConnectionStrings:MongoConnection").Get<MongoSettings>();
//            services.AddSingleton(mongoSettings);
//            services.AddSingleton<IMongoClient>(s => new MongoClient(mongoSettings.ConnectionString));

//            // Register BuildingRepository for IBuildingRepository
//            services.AddScoped<IMongoUnitofWork, BuildingRepository>(s =>
//                new BuildingRepository(
//                    s.GetRequiredService<IMongoClient>(),
//                    mongoSettings.DatabaseName));

//            // Register BuildingTypeRepository for IBuildingTypeRepository
//            services.AddScoped<IBuildingTypeRepository, BuildingTypeRepository>(s =>
//                new BuildingTypeRepository(
//                    s.GetRequiredService<IMongoClient>(),
//                    mongoSettings.DatabaseName));

//            // Identity Configuration
//            services.AddIdentity<AppUser, AppRole>()
//                .AddEntityFrameworkStores<GameDashboardDbContext>()
//                .AddDefaultTokenProviders();

//            // Data Protection
//            services.AddDataProtection();

//            // Dependency Injection for MySQL Repositories and UnitOfWork
//            services.AddScoped(typeof(IMySqlReadRepository<>), typeof(MySqlReadRepository<>));
//            services.AddScoped(typeof(IMySqlWriteRepository<>), typeof(MySqlWriteRepository<>));
//            services.AddScoped<IMySqlUnitOfWork, MySqlUnitOfWork>();

//            // Dependency Injection for MongoDB Repositories and UnitOfWork
//            services.AddScoped(typeof(IMongoReadRepository<>), typeof(MongoReadRepository<>));
//            services.AddScoped(typeof(IMongoWriteRepository<>), typeof(MongoWriteRepository<>));
//            services.AddScoped<IMongoUnitOfWorkk>(s => new MongoUnitOfWork(
//                s.GetRequiredService<IMongoClient>(),
//                mongoSettings.DatabaseName,
//                s.GetRequiredService<IMongoUnitofWork>(),
//                s.GetRequiredService<IBuildingTypeRepository>()));
//        }
//    }
//}

//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using MongoDB.Driver;
//using GameDashboardProject.Application.Repositories;
//using GameDashboardProject.Persistence.Repositories;
//using GameDashboardProject.Infrastructure.Mongo;
//using GameDashboardProject.Infrastructure.MongoServices;
//using GameDashboardProject.Application.UnitOfWorks;
//using GameDashboardProject.Domain.Identities;
//using GameDashboardProject.Persistence.Context;
//using GameDashboardProject.Persistence.UnitOfWorks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace GameDashboardProject.Persistence
//{
//    public static class ServiceRegistration
//    {
//        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
//        {
//            // MySQL Configuration
//            services.AddDbContext<GameDashboardDbContext>(opt =>
//                opt.UseMySql(configuration.GetConnectionString("DefaultConnection"),
//                    new MySqlServerVersion(new Version(8, 0, 2)),
//                    mySqlOptions => mySqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null)));

//            // MongoDB Configuration
//            var mongoSettings = configuration.GetSection("ConnectionStrings:MongoConnection").Get<MongoSettings>();
//            services.AddSingleton(mongoSettings);
//            services.AddSingleton<IMongoClient>(s => new MongoClient(mongoSettings.ConnectionString));

//            // Register MongoDB Repositories
//            services.AddScoped(typeof(IMongoReadRepository<>), typeof(MongoReadRepository<>));
//            services.AddScoped(typeof(IMongoWriteRepository<>), typeof(MongoWriteRepository<>));

//            // Register MongoDB Unit of Work
//            services.AddScoped<IMongoUnitOfWorkk>(s => new MongoUnitOfWork(
//                s.GetRequiredService<IMongoClient>(),
//                mongoSettings.DatabaseName));

//            // Identity Configuration
//            services.AddIdentity<AppUser, AppRole>()
//                .AddEntityFrameworkStores<GameDashboardDbContext>()
//                .AddDefaultTokenProviders();

//            // Data Protection
//            services.AddDataProtection();

//            // Dependency Injection for MySQL Repositories and UnitOfWork
//            services.AddScoped(typeof(IMySqlReadRepository<>), typeof(MySqlReadRepository<>));
//            services.AddScoped(typeof(IMySqlWriteRepository<>), typeof(MySqlWriteRepository<>));
//            services.AddScoped<IMySqlUnitOfWork, MySqlUnitOfWork>();
//        }
//    }
//}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using GameDashboardProject.Application.Repositories;
using GameDashboardProject.Persistence.Repositories;
using GameDashboardProject.Infrastructure.MongoServices;
using GameDashboardProject.Application.UnitOfWorks;
using GameDashboardProject.Domain.Identities;
using GameDashboardProject.Persistence.Context;
using GameDashboardProject.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GameDashboardProject.Infrastructure.Mongo;

namespace GameDashboardProject.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GameDashboardDbContext>(opt =>
                opt.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 2)),
                    mySqlOptions => mySqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null)));

            var mongoSettings = configuration.GetSection("ConnectionStrings:MongoConnection").Get<MongoSettings>();
            services.AddSingleton(mongoSettings);
            services.AddSingleton<IMongoClient>(s => new MongoClient(mongoSettings.ConnectionString));      

            services.AddScoped(typeof(IMongoReadRepository<>), typeof(MongoReadRepository<>));
            services.AddScoped(typeof(IMongoWriteRepository<>), typeof(MongoWriteRepository<>));

            services.AddScoped<IMongoUnitOfWorkk, MongoUnitOfWork>();

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<GameDashboardDbContext>()
                .AddDefaultTokenProviders();

            services.AddDataProtection();

            services.AddScoped(typeof(IMySqlReadRepository<>), typeof(MySqlReadRepository<>));
            services.AddScoped(typeof(IMySqlWriteRepository<>), typeof(MySqlWriteRepository<>));
            services.AddScoped<IMySqlUnitOfWork, MySqlUnitOfWork>();
        }
    }
}
