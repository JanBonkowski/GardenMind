using GardenMind.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddSystemsManager(ssm =>
{
    ssm.Path = $"/GardenMind/{builder.Environment.EnvironmentName}";
    ssm.AwsOptions = new Amazon.Extensions.NETCore.Setup.AWSOptions()
    {
        Profile = "default",
        Region = Amazon.RegionEndpoint.EUNorth1
    };
});

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.SectionName));

builder.Services.AddDbContext<GardenDbContext>((services, ctx) =>
{
    var config = services.GetRequiredService<IOptions<DatabaseOptions>>().Value;
    var connectionStringBuilder = new NpgsqlConnectionStringBuilder()
    {
        Host = config.Host,
        Database = config.Database,
        Username = config.Username,
        Password = config.Password,
        Port = config.Port
    };
    ctx.UseNpgsql(connectionStringBuilder.ConnectionString);
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapGet("/", async (GardenDbContext ctx) =>
{
    return await ctx.Species.ToListAsync();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();