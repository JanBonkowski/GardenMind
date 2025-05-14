using GardenMind.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GardenMind.API.Tests
{
    public abstract class IntegrationTestBase
    {
        private readonly GardenWebApplicationFactory _factory;
        private readonly WebApplicationFactoryClientOptions _options;

        protected IServiceScope ServiceScope;

        protected IntegrationTestBase()
        {
            _factory = new GardenWebApplicationFactory();
            _options = new WebApplicationFactoryClientOptions()
            {
                BaseAddress = new Uri("https://localhost"),
                AllowAutoRedirect = true
            };

            ServiceScope = _factory.Services.CreateScope();
        }

        public HttpClient Client => _factory.CreateClient(_options);

        protected GardenDbContext Context() => ServiceScope.ServiceProvider.GetService<GardenDbContext>()!;
    }
}
