using Microsoft.AspNetCore.Mvc.Testing;

namespace GardenMind.API.Tests;

public class GardenWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(Environments.Development);
        base.ConfigureWebHost(builder);
    }
}