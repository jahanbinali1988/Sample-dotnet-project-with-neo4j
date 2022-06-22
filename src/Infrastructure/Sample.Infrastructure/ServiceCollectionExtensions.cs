using Sample.Domain.Currency;
using Sample.Domain.Currency.DomainServices;
using Sample.Infrastructure.Domain.Currency;
using Sample.Infrastructure.DomainService.Meeting;
using Sample.Infrastructure.EventProcessing;
using Sample.Infrastructure.Persistence;
using Sample.SharedKernel.EventProcessing.DomainEvent;
using Sample.SharedKernel.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Infrastructure.DomainService.Currency;
using Neo4jObjectMapper;
using Neo4j.Driver;
using Sample.Infrastructure.Setting;

namespace Sample.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        private static IServiceCollection RegisterDatabaseTools(this IServiceCollection services, string mssqlConnection, GraphDb graphDb)
        {
            services.AddDbContextPool<SampleDbContext>(options =>
            {
                options.UseSqlServer(mssqlConnection);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging(true);
            }, 1024);

            IDriver driver = GraphDatabase.Driver(graphDb.Url, AuthTokens.Basic(graphDb.Username, graphDb.Password));
            driver.VerifyConnectivityAsync();
            services.AddScoped<INeoContext, SampleNeoContext>(x =>new SampleNeoContext(driver));

            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ICurrencyGraphRepository, CurrencyGraphRepository>();

            services.AddScoped<ICheckCurrencyNameExistenceService, CheckCurrencyNameExistenceService>();
            services.AddScoped<ICheckCurrencyRateDuplicationService, CheckCurrencyRateDuplicationService>();
            services.AddScoped<ICheckCurrencyRateExistenceService, CheckCurrencyRateExistenceService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();

            return services;
        }

        public static IServiceCollection RegisterSampleDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlConnection = configuration.GetValue<string>("ConnectionStrings:MSSQL");
            var graphDb = new GraphDb();
            configuration.GetSection("GraphDb").Bind(graphDb);

            RegisterDatabaseTools(services, sqlConnection, graphDb);

            return services;
        }
    }
}
