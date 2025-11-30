using EdgeView.Application.Interfaces;
using EdgeView.Application.Services;
using EdgeView.Infrastructure.Data;
using EdgeView.Infrastructure.Repositories;
using EdgeView.Infrastructure.Scrapers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------
// 1. DbContext — MUST always be registered
// -----------------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// -----------------------------------------
// 2. MVC Controllers
// -----------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -----------------------------------------
// 3. ONLY register application services
//    when NOT running under EF migrations
// -----------------------------------------
if (!builder.Environment.IsEnvironment("EF"))
{
    builder.Services.AddScoped<ICacheRepository, CacheRepository>();
    builder.Services.AddScoped<IBinScraper, BarnsleyBinScraper>();
    builder.Services.AddScoped<IBinDayService, BinDayService>();
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();



if (!app.Environment.IsEnvironment("EF"))
{
    app.MapControllers();
    app.MapGet("/", () => "Hello World!");
}

// Automatically apply EF Core migrations when the app starts
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
