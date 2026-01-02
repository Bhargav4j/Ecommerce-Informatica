# Ecommerce Application - .NET 8 Migration

This project has been successfully migrated from ASP.NET Web Forms 4.7.2 to .NET 8 with clean architecture.

## Architecture

The application follows clean architecture principles with four main layers:

### Domain Layer (`EcommerceApp.Domain`)
- Contains domain entities (Person, Category, Brand, Product, Supplier, Invoice, etc.)
- Defines repository interfaces
- No external dependencies

### Application Layer (`EcommerceApp.Application`)
- Contains business logic and services
- Defines service interfaces
- Contains DTOs (Data Transfer Objects)
- Uses AutoMapper for entity-DTO mapping
- Depends on Domain layer only

### Infrastructure Layer (`EcommerceApp.Infrastructure`)
- Implements data access using Entity Framework Core 8
- Repository implementations
- Database context and configurations
- Depends on both Domain and Application layers

### Web Layer (`EcommerceApp.Web`)
- ASP.NET Core 8 Razor Pages application
- User interface
- Depends on Infrastructure and Application layers

## Technologies Used

- **.NET 8**: Latest version of .NET
- **ASP.NET Core 8**: Web framework
- **Entity Framework Core 8**: ORM for data access
- **Razor Pages**: UI framework
- **AutoMapper 12**: Object-to-object mapping
- **Serilog**: Structured logging
- **SQL Server**: Database
- **Bootstrap 5**: UI styling

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or Express)
- Visual Studio 2022 or VS Code

### Setup

1. **Update Connection String**:
   Edit `src/EcommerceApp.Web/appsettings.json` and update the connection string:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost\\sqlexpress;Database=LaboratorioTIF;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
   }
   ```

2. **Create Database**:
   The database schema from the original application should be present. If not, use the SQL script:
   ```bash
   sqlcmd -S localhost\sqlexpress -i "Base de datos E-Commerce.sql"
   ```

3. **Build the Solution**:
   ```bash
   dotnet build
   ```

4. **Run the Application**:
   ```bash
   cd src/EcommerceApp.Web
   dotnet run
   ```

5. **Access the Application**:
   Open your browser and navigate to `https://localhost:5001`

## Project Structure

```
Ecommerce-component/
├── src/
│   ├── EcommerceApp.Domain/
│   │   ├── Entities/
│   │   └── Interfaces/
│   ├── EcommerceApp.Application/
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   ├── Mappings/
│   │   └── Services/
│   ├── EcommerceApp.Infrastructure/
│   │   ├── Data/
│   │   │   ├── Configurations/
│   │   │   └── EcommerceDbContext.cs
│   │   └── Repositories/
│   └── EcommerceApp.Web/
│       ├── Pages/
│       │   ├── Categories/
│       │   ├── Products/
│       │   └── Shared/
│       ├── wwwroot/
│       ├── Program.cs
│       └── appsettings.json
├── EcommerceApp.sln
└── README.md
```

## Features Migrated

- ✅ Category management (CRUD operations)
- ✅ Product browsing and management
- ✅ Clean architecture implementation
- ✅ Entity Framework Core 8
- ✅ Async/await patterns
- ✅ Proper dependency injection
- ✅ Structured logging with Serilog
- ✅ Modern UI with Bootstrap 5

## Database Schema

The application uses the existing `LaboratorioTIF` database with the following main tables:
- `Personas` - Users and customers
- `Categorias` - Product categories
- `Marcas` - Brands
- `Productos` - Products
- `Proveedores` - Suppliers
- `Facturas` - Invoices
- `DetalleFactura` - Invoice details
- `FormaPago` - Payment methods
- `Provincias` - Provinces
- `Ciudades` - Cities

## Migration Notes

### Key Changes from Web Forms

1. **UI Framework**: Migrated from ASPX pages to Razor Pages
2. **Data Access**: Replaced ADO.NET with Entity Framework Core
3. **Configuration**: Migrated Web.config to appsettings.json
4. **Session Management**: Using ASP.NET Core session middleware
5. **Dependency Injection**: Built-in DI container instead of manual instantiation
6. **Logging**: Serilog instead of no formal logging
7. **Security**: Modern authentication patterns (ready for ASP.NET Core Identity)

### Breaking Changes

- All ASPX pages have been converted to Razor Pages
- ViewState is no longer used
- Server controls replaced with HTML helpers and tag helpers
- Session state is now managed differently

## Next Steps

Consider implementing:
- ASP.NET Core Identity for authentication
- API controllers for mobile/SPA support
- Unit and integration tests
- Docker containerization
- CI/CD pipeline

## Support

For issues or questions, refer to the migration documentation in the `docs/` folder (if available).

## License

[Your License Here]
