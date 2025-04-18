using GardenMind.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GardenMind.API.Tests
{
    public abstract class IntegrationTestBase
    {
        private readonly GardenWebApplicationFactory _factory;
        private readonly WebApplicationFactoryClientOptions _options;

        protected IntegrationTestBase()
        {
            _factory = new GardenWebApplicationFactory();
            _options = new WebApplicationFactoryClientOptions()
            {
                BaseAddress = new Uri("https://localhost"),
                AllowAutoRedirect = true
            };

            using var scope = _factory.Services.CreateScope();
            using var ctx = scope.ServiceProvider.GetService<GardenDbContext>();
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
        }

        public HttpClient Client => _factory.CreateClient(_options);

        
    }
}
