# Perdin Blazor Client (Frontend)

This is the frontend client for the Perdin (Perjalanan Dinas) System, built on **Blazor WebAssembly** with **.NET 8.0** and styled with **Tailwind CSS**.

---

## Technical Stack

- **Framework**: Blazor WebAssembly (.NET 8.0)
- **Styling**: Tailwind CSS (compiled via npm scripts and integrated into the MSBuild pipeline)
- **Authentication**: Custom AuthenticationStateProvider using JWT Bearer Tokens
- **Alerts**: Blazored.Toast for popup notifications

---

## Folder Structure

- **Pages/**: Blazor pages / routable views (such as Dashboard, login pages, and feature CRUD layouts).
- **Components/**: Reusable UI components.
- **Models/**: Client-side data models and DTO representations matching Web API schemas.
- **Services/**: HTTP Client wrappers and authentication services interacting with the backend API.
- **Styles/**: Tailwind CSS input files (`app.css`) which compile to static CSS in `wwwroot/css`.

---

## Getting Started

### 1. Install Node Dependencies

Tailwind CSS requires Node.js to compile utility classes. Install the node packages first:

```bash
npm install
```

### 2. Configure Backend API Address

Check the API address configuration inside `Program.cs` to ensure it points to the correct backend host (usually `http://localhost:5266` or similar):

```csharp
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5266") });
```

### 3. Run the Client

Run the client app:

```bash
dotnet run
# or with hot reload:
dotnet watch
```

*Note: The project is configured to automatically build and compile Tailwind CSS before building the Blazor application (`npm run build:css` will trigger automatically as part of MSBuild).*
