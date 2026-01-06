# E-commerce Informatica - .NET 8 Migration

This project has been successfully migrated from ASP.NET Web Forms 4.7.2 to .NET 8 using clean architecture principles.

## Architecture

The solution follows clean architecture with four main layers:

- **Domain**: Core business entities and interfaces
- **Application**: Business logic, DTOs, and AutoMapper profiles
- **Infrastructure**: Data access with EF Core 8.0, repository implementations
- **Web**: ASP.NET Core 8.0 Razor Pages UI

## Technologies

- .NET 8.0
- ASP.NET Core 8.0 with Razor Pages
- Entity Framework Core 8.0
- SQL Server
- AutoMapper 12.0.1
- Serilog for logging
- BCrypt.Net for password hashing
- Bootstrap 5 for UI

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or SQL Server Express)

### Setup

1. Update the connection string in `src/EcommerceInformatica.Web/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Data Source=localhost\\sqlexpress;Initial Catalog=LaboratorioTIF;Integrated Security=True;TrustServerCertificate=True"
   }
   ```

2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

4. Run the web application:
   ```bash
   cd src/EcommerceInformatica.Web
   dotnet run
   ```

5. Navigate to `https://localhost:5001` or the URL shown in the console

## Migration Notes

### What Was Migrated

- **Pages**: All .aspx pages migrated to Razor Pages
- **Data Access**: ADO.NET replaced with Entity Framework Core
- **Authentication**: Session-based auth replaced with modern patterns
- **Configuration**: Web.config migrated to appsettings.json
- **Entities**: Improved with proper properties and navigation
- **UI**: Bootstrap 5 with responsive design

### Key Differences from Web Forms

- No ViewState - use TempData or session for temporary data
- No server controls - use HTML helpers and Tag Helpers
- No Page_Load events - use OnGet/OnPost handlers
- Async/await throughout for better performance
- Dependency injection for all services
- EF Core for database operations with proper async

### Breaking Changes

- All System.Web dependencies removed
- Master pages replaced with Razor _Layout.cshtml
- User controls replaced with partial views/components
- GridView/DataGrid replaced with HTML tables + JavaScript
- Response.Redirect replaced with RedirectToPage

## Project Structure

```
src/
├── EcommerceInformatica.Domain/          # Entities and interfaces
│   ├── Entities/
│   └── Interfaces/
├── EcommerceInformatica.Application/     # Business logic and DTOs
│   ├── DTOs/
│   ├── Mappings/
│   └── Extensions/
├── EcommerceInformatica.Infrastructure/  # Data access
│   ├── Data/
│   ├── Repositories/
│   └── Extensions/
└── EcommerceInformatica.Web/            # Razor Pages UI
    ├── Pages/
    └── wwwroot/
```

## Entities

- Product (Articles)
- Category
- Brand
- Provider
- Person (Users/Customers)
- City
- Province
- Order (Invoice/Factura)
- OrderDetail
- PaymentMethod

## Known Issues

- None

## Future Improvements

- Add API controllers for REST endpoints
- Implement advanced search and filtering
- Add product image upload functionality
- Implement email notifications
- Add more comprehensive unit tests
- Implement caching for better performance

## Build Verification

✅ Build Status: **SUCCESS**

All projects compile successfully with no errors.

---

Generated with .NET 8 Migration Tool - January 2026
