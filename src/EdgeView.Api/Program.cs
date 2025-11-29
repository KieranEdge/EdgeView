using EdgeView.Application.Interfaces;
using EdgeView.Application.Services;
using EdgeView.Infrastructure.Scrapers;

var builder = WebApplication.CreateBuilder(args);

// 1. Register services BEFORE Build()
builder.Services.AddScoped<IBinScraper, BarnsleyBinScraper>();
builder.Services.AddScoped<IBinDayService, BinDayService>();
// 2. Add controllers (if using attribute routing)
builder.Services.AddControllers();

// 3. Build the app
var app = builder.Build();

// 4. Map endpoints
app.MapControllers();  // enables attribute-routed controllers

app.MapGet("/", () => "Hello World!");

// 5. Run
app.Run();
