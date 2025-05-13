using DotNet.Testcontainers.Containers;
using GardenMind.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace GardenMind.API.Tests;

public class GardenWebApplicationFactory : WebApplicationFactory<Program>
{
    private static readonly TaskFactory TASK_FACTORY = new(CancellationToken.None,
                                                           TaskCreationOptions.None,
                                                           TaskContinuationOptions.None,
                                                           TaskScheduler.Default);

    private static readonly string DATABASE_NAME = "Garden";
    private static readonly string DATABASE_USER = "postgres";
    private static readonly string DATABASE_PASSWORD = "admin";
    private static readonly short DATABASE_PORT = 5432;

    private static readonly PostgreSqlContainer POSTGRESQL_CONTAINER = new PostgreSqlBuilder()
        .WithDatabase(DATABASE_NAME)
        .WithPassword(DATABASE_USER)
        .WithUsername(DATABASE_PASSWORD)
        .WithPortBinding(DATABASE_PORT, assignRandomHostPort: true)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(Environments.Development);
        base.ConfigureWebHost(builder);
        builder.ConfigureTestServices((services) =>
        {
            services.RemoveAll(typeof(GardenDbContext));
            services.AddDbContext<GardenDbContext>(options =>
            {
                options.UseNpgsql(POSTGRESQL_CONTAINER.GetConnectionString());

            });
        });
    }

    static GardenWebApplicationFactory()
    {
        foreach (var container in CONTAINERS_TO_RUN())
        {
            TASK_FACTORY.StartNew(async () => await container.StartAsync())
                .Unwrap()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
    }

    static IEnumerable<DockerContainer> CONTAINERS_TO_RUN()
    {
        yield return POSTGRESQL_CONTAINER;
    }
}