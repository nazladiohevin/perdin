# Perdin (Perjalanan Dinas) System

Perdin is a web-based system designed to manage business trip requests (perjalanan dinas). The system is built using .NET 8.0, featuring a decoupled client-server architecture:

1. **Backend**: ASP.NET Core Web API ([Perdin.WebApi](file:///c:/projects/perdin/Perdin.WebApi/README.md))
2. **Frontend**: Blazor WebAssembly ([Perdin.BlazorClient](file:///c:/projects/perdin/Perdin.BlazorClient/README.md))

---

## Workspace Structure

```
perdin/
├── Perdin.WebApi/        # ASP.NET Core Web API (Backend)
├── Perdin.BlazorClient/  # Blazor WebAssembly Client (Frontend)
└── README.md             # This file
```

---

## Prerequisites

Ensure you have the following installed on your local machine:
- **.NET 8.0 SDK** or later
- **Node.js** (v18+ recommended) & npm (for compiling Tailwind CSS in the Blazor project)
- **SQL Server** (LocalDB, Express, or full instance)

---

## Getting Started

### 1. Database Setup

1. Open [appsettings.json](file:///c:/projects/perdin/Perdin.WebApi/appsettings.json) in the `Perdin.WebApi` project.
2. Update the `ConnectionStrings:DefaultConnection` to match your local SQL Server instance.
3. Open a terminal in the `Perdin.WebApi` directory and run the Entity Framework migrations to create the database:
   ```bash
   dotnet ef database update
   ```

### 2. Running the Application

You can run both projects simultaneously using C# CLI or your favorite IDE.

#### Option A: Running from Terminal

**Start Backend (WebApi):**
```bash
cd Perdin.WebApi
dotnet run
```
*Note: The API will be available at `http://localhost:5266` (or the configured launch settings URL).*

**Start Frontend (Blazor Client):**
```bash
cd Perdin.BlazorClient
npm install
dotnet run
```
*Note: The frontend will be available at `http://localhost:5247` or `https://localhost:7196` depending on your profiles.*

#### Option B: Running with `dotnet watch` (Development)

Run this to enable Hot Reload for development:
```bash
# In Perdin.WebApi/
dotnet watch

# In Perdin.BlazorClient/
dotnet watch
```

---

## Technical Stack Summary

### Backend
- **Core**: .NET 8.0, C# 12
- **Database Access**: Entity Framework Core with SQL Server
- **Architecture**: CQRS (Command Query Responsibility Segregation) pattern powered by **MediatR**
- **Authentication**: JWT Bearer Tokens & BCrypt for password hashing
- **API Documentation**: OpenAPI / Swagger

### Frontend
- **Core**: Blazor WebAssembly (.NET 8.0)
- **Styling**: Tailwind CSS (integrated with standard npm scripts and MSBuild compilation)
- **State/Auth**: Microsoft ASP.NET Core Components Authorization
- **Notifications**: Blazored.Toast
