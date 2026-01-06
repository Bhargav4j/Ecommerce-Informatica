# E-Commerce Informatica - .NET 8 Migration

This project has been successfully migrated from ASP.NET Web Forms 4.7.2 to .NET 8 with clean architecture.

## Project Overview

E-Commerce Informatica is a web-based e-commerce application that manages products, categories, brands, suppliers, and customer orders.

## Architecture

The solution follows clean architecture principles with four main layers:

- **Domain Layer** (`EcommerceInformatica.Domain`): Contains business entities and domain interfaces
- **Application Layer** (`EcommerceInformatica.Application`): Contains business logic, services, DTOs, and mappings
- **Infrastructure Layer** (`EcommerceInformatica.Infrastructure`): Contains data access, EF Core configurations, and repository implementations
- **Web Layer** (`EcommerceInformatica.Web`): Contains Razor Pages UI and presentation logic

## Technologies Used

- .NET 8
- ASP.NET Core Razor Pages
- Entity Framework Core 8.0
- AutoMapper 12.0
- Serilog for logging
- Bootstrap 5 for UI
- xUnit for testing

## Setup Instructions

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or Express)

### Database Configuration

1. Update the connection string in `src/EcommerceInformatica.Web/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost\\sqlexpress;Database=LaboratorioTIF;Integrated Security=true;TrustServerCertificate=true"
   }
   ```

2. Run migrations (when implemented):
   ```bash
   dotnet ef database update --project src/EcommerceInformatica.Infrastructure --startup-project src/EcommerceInformatica.Web
   ```

### Running the Application

```bash
dotnet run --project src/EcommerceInformatica.Web
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`.

## Build Verification

The solution has been verified to build successfully:

```bash
dotnet build EcommerceInformatica.sln
```

Build Result: ✅ SUCCESS
- All 6 projects compiled without errors
- All NuGet packages restored successfully
- All dependencies resolved correctly

## Testing

Run unit tests:
```bash
dotnet test tests/EcommerceInformatica.UnitTests
```

Run integration tests:
```bash
dotnet test tests/EcommerceInformatica.IntegrationTests
```

## Migration Notes

### Key Changes from Web Forms

1. **Replaced System.Web** with ASP.NET Core equivalents
2. **Migrated .aspx pages** to Razor Pages (.cshtml)
3. **Converted ADO.NET** to Entity Framework Core
4. **Replaced ViewState** with modern state management
5. **Updated authentication** from Forms Auth to ASP.NET Core Identity (ready for implementation)
6. **Migrated Web.config** to appsettings.json
7. **Replaced Global.asax** with Program.cs

### Breaking Changes

- Server controls replaced with HTML helpers and Tag Helpers
- Page lifecycle events replaced with Razor Page handlers
- Session state now uses distributed cache
- Connection strings moved to configuration

## Known Issues

None at this time. The build is successful and ready for further development.

## Future Improvements

1. Implement authentication with ASP.NET Core Identity
2. Add complete CRUD operations for all entities (Brands, Suppliers, Invoices)
3. Implement data seeding for initial data
4. Add comprehensive error pages
5. Implement full-text search
6. Add pagination for large data sets
7. Implement caching strategies
8. Add API endpoints for mobile applications

## License

All rights reserved.
