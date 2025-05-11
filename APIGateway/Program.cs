using APIGateway.Middleware;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot().AddCacheManager(x =>
{
    x.WithDictionaryHandle();
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
});
var app = builder.Build();

app.UseCors();
app.UseHttpsRedirection();
app.UseMiddleware<TokenCheckerMiddleware>();
app.UseMiddleware<InterceptionMiddleware>();
app.UseAuthorization();
app.UseOcelot().Wait();
app.Run();
