# Perdin Web API (Backend)

This is the backend API service for the Perdin (Perjalanan Dinas) System, built on **ASP.NET Core Web API** with **.NET 8.0**.

---

## Technical Stack

- **Framework**: .NET 8.0 / ASP.NET Core
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core 8.x
- **Pattern**: CQRS (Command Query Responsibility Segregation) using **MediatR**
- **Authentication**: JWT Bearer Authentication and BCrypt.Net-Next (for hashing passwords)
- **API Specs**: Swagger / OpenAPI (via Swashbuckle)

---

## Folder Structure

- **Controllers/**: Defines REST API endpoints mapping to MediatR requests.
- **Features/**: Contains CQRS vertical slices (grouped by domain like `Users`, `Provinces`, `Cities`, `BusinessTripRequests`). Each feature contains its respective Request, Response, Command/Query, and CommandHandler/QueryHandler classes.
- **Models/**: Database entities (e.g., `User`, `Province`, `City`, `Country`).
- **Data/**: Contains `AppDbContext` for EF Core data access.
- **Services/**: Application services (such as `JwtService` for access token generation).
- **Helpers/**: Math/business helper utilities (e.g., distance calculations).

---

## Getting Started

### 1. Configure Database Connection

Open [appsettings.json](file:///c:/projects/perdin/Perdin.WebApi/appsettings.json) and set your database connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=PerdinDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 2. Apply Migrations

Verify you have `dotnet-ef` tool installed, then apply database migrations:

```bash
dotnet ef database update
```

### 3. Run the API

Start the backend in development mode:

```bash
dotnet run
# or with hot reload:
dotnet watch
```

Once running, you can access the Swagger UI at `http://localhost:5266/swagger` (or port configured in your local environment) to test API endpoints.
