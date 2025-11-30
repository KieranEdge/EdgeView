📦 EdgeView Backend

EdgeView is a .NET 9 clean-architecture backend that:

Scrapes Barnsley Council bin collection dates using Playwright

Caches results in PostgreSQL for faster repeat calls

Exposes a clean REST API (/api/bin-day/next)

Uses a layered architecture: Domain → Application → Infrastructure → API

🧱 Architecture Overview
```
EdgeView.Domain
   └── Entities (BinCollection, etc.)

EdgeView.Application
   ├── Interfaces (IBinScraper, IBinDayService, ICacheRepository)
   └── Services (BinDayService)

EdgeView.Infrastructure
   ├── Scrapers (Playwright-based BarnsleyBinScraper)
   ├── Data (AppDbContext + Factory)
   ├── Repositories (CacheRepository → PostgreSQL)
   └── Migrations (EF Core)

EdgeView.Api
   └── REST endpoints + DI container
```
🚀 Running the API

Make sure PostgreSQL is running locally and your connection string is stored as a User Secret:

```
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=edgeview;Username=postgres;Password=YOURPASSWORD"
```

Then run the API:

```
dotnet run --project src/EdgeView.Api
```

Your API will be available at:
```
http://localhost:5181
```

Test endpoint:
```
curl "http://localhost:5181/api/bin-day/next?houseNumber=SOMENUMBER&postcode=SOMEPOSTCODE"
```
🗃 Database Migrations (EF Core)

Because migrations shouldn't run with the full DI graph, we use a dedicated EF environment.

Run migrations with:
```
dotnet ef migrations add <MigrationName> --project src/EdgeView.Infrastructure --startup-project src/EdgeView.Api
```

Apply them:
```
dotnet ef database update --project src/EdgeView.Infrastructure --startup-project src/EdgeView.Api
```

View existing migrations:
```
ls src/EdgeView.Infrastructure/Migrations
```
🧪 Scraping

The scraper lives in:
```
src/EdgeView.Infrastructure/Scrapers/BarnsleyBinScraper.cs
```

Playwright must be installed once:

playwright install

🧰 Common Development Commands
Build the whole solution
```
dotnet build
```
Run API with hot reload
```
dotnet watch --project src/EdgeView.Api
```
Fix Playwright if browsers break
```
playwright install chromium
```
Reset database (dangerous!)
```
dropdb edgeview
createdb edgeview
dotnet ef database update --project src/EdgeView.Infrastructure --startup-project src/EdgeView.Api
```
