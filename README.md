# Ecommerce Management System

A comprehensive e-commerce management platform built with .NET 8.0, following Clean Architecture principles and modern design patterns.

## Project Description

This Ecommerce Management System is a web-based application that provides complete functionality for managing an online retail business. The system allows users to manage products, categories, brands, suppliers, and user accounts through an intuitive web interface.

## Architecture Overview

The project follows **Clean Architecture** principles with clear separation of concerns across multiple layers:

### Layers

1. **Domain Layer** (`Ecommerce.Domain`)
   - Contains core business entities (Product, Category, Brand, Supplier, Person)
   - Defines repository interfaces
   - No dependencies on other layers
   - Pure business logic and domain models

2. **Application Layer** (`Ecommerce.Application`)
   - Contains application business logic
   - Implements service interfaces (ProductService, CategoryService, etc.)
   - Defines DTOs (Data Transfer Objects) for data exchange
   - Uses AutoMapper for entity-to-DTO mapping
   - Depends only on Domain layer

3. **Infrastructure Layer** (`Ecommerce.Infrastructure`)
   - Implements data access using Entity Framework Core
   - Contains repository implementations
   - Manages database configurations and migrations
   - Implements the Unit of Work pattern
   - Depends on Domain and Application layers

4. **Web/Presentation Layer** (`Ecommerce.Web`)
   - ASP.NET Core Razor Pages application
   - Handles HTTP requests and responses
   - Provides user interface for all CRUD operations
   - Implements authentication and authorization
   - Depends on Application and Infrastructure layers

5. **Test Projects**
   - `Ecommerce.UnitTests`: Unit tests for services using Moq and xUnit
   - `Ecommerce.IntegrationTests`: Integration tests for repositories and end-to-end scenarios

### Key Design Patterns

- **Repository Pattern**: Abstracts data access logic
- **Service Layer Pattern**: Encapsulates business logic
- **Dependency Injection**: Used throughout for loose coupling
- **DTO Pattern**: Separates domain models from data transfer objects
- **AutoMapper**: Automatic object-to-object mapping

## Technology Stack

- **.NET 8.0**: Latest LTS version of .NET
- **ASP.NET Core**: Web framework for building modern web applications
- **Entity Framework Core 8.0**: ORM for data access
- **SQL Server**: Primary database (configurable)
- **Bootstrap 5**: Frontend CSS framework
- **xUnit**: Testing framework
- **Moq**: Mocking framework for unit tests
- **FluentAssertions**: Assertion library for tests
- **AutoMapper**: Object-to-object mapping

## Project Structure

```
ecommerce1001/
├── src/
│   ├── Ecommerce.Domain/           # Domain entities and interfaces
│   ├── Ecommerce.Application/      # Business logic and services
│   ├── Ecommerce.Infrastructure/   # Data access and EF Core
│   └── Ecommerce.Web/              # Web UI (Razor Pages)
├── tests/
│   ├── Ecommerce.UnitTests/        # Unit tests
│   └── Ecommerce.IntegrationTests/ # Integration tests
└── Ecommerce.sln                   # Solution file
```

## Features

### Product Management
- Create, read, update, and delete products
- Track product details (name, description, price, stock)
- Associate products with categories, brands, and suppliers
- Manage product availability status

### Category Management
- Organize products into categories
- Create hierarchical category structures
- Manage category descriptions and metadata

### Brand Management
- Maintain brand information
- Associate products with specific brands
- Track brand descriptions

### Supplier Management
- Manage supplier contact information
- Track supplier business details
- Associate products with suppliers
- Maintain supplier location data (province, city)

### Person/User Management
- User authentication and authorization
- Admin and regular user roles
- Secure password storage
- User profile management

## Setup Instructions

### Prerequisites

- .NET 8.0 SDK or later
- SQL Server (2019 or later) or SQL Server Express
- Visual Studio 2022, VS Code, or JetBrains Rider (optional)
- Git (for version control)

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd ecommerce1001
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Configure the database connection**

   Update the connection string in `src/Ecommerce.Web/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=EcommerceDb;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

4. **Apply database migrations**
   ```bash
   cd src/Ecommerce.Infrastructure
   dotnet ef database update --startup-project ../Ecommerce.Web
   ```

5. **Build the solution**
   ```bash
   cd ../..
   dotnet build
   ```

## Running the Application

### Run the Web Application

```bash
cd src/Ecommerce.Web
dotnet run
```

The application will start and be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

### Default Admin Credentials

After seeding the database, you can log in with:
- Username: `admin` (or configure in seed data)
- Password: (set in your seed configuration)

## Testing Instructions

### Run All Tests

```bash
dotnet test
```

### Run Unit Tests Only

```bash
dotnet test tests/Ecommerce.UnitTests/Ecommerce.UnitTests.csproj
```

### Run Integration Tests Only

```bash
dotnet test tests/Ecommerce.IntegrationTests/Ecommerce.IntegrationTests.csproj
```

### Run Tests with Coverage

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## Migration Notes

This project was migrated from a legacy codebase to modern .NET 8.0 with Clean Architecture. Key migration changes:

### Architecture Changes

1. **Layered Architecture**: Restructured from monolithic to Clean Architecture with distinct layers
2. **Service Layer Introduction**: Added service layer to separate business logic from controllers/pages
3. **Repository Pattern**: Implemented repository pattern for data access abstraction
4. **DTO Pattern**: Introduced DTOs to separate domain models from API contracts

### Technology Updates

1. **Framework**: Migrated from older .NET Framework to .NET 8.0
2. **Entity Framework**: Updated to EF Core 8.0
3. **Web Framework**: Transitioned to ASP.NET Core Razor Pages
4. **Dependency Injection**: Implemented built-in DI container throughout

### Database Schema Updates

1. **Entity Changes**:
   - Standardized entity properties with `Id` as primary key
   - Added `IsActive`, `CreatedDate`, `CreatedBy`, `ModifiedDate`, `ModifiedBy` audit fields
   - Introduced string-based alternate keys (e.g., `ProductId`, `CategoryId`) alongside integer primary keys

2. **Relationships**:
   - Maintained foreign key relationships
   - Added navigation properties for Entity Framework
   - Configured cascade behaviors appropriately

### Build and Compilation Fixes

During migration, the following issues were addressed:

1. **Circular Dependencies**: Moved service interfaces from Domain to Application layer
2. **Property Name Mismatches**: Corrected entity property references throughout the codebase
3. **Interface Signatures**: Aligned repository and service interface method signatures
4. **Configuration Mismatches**: Updated EF Core configurations to match actual entity properties
5. **Razor Pages**: Added fully qualified namespace in @model directives
6. **Missing Dependencies**: Added required NuGet packages

### Test Structure

1. **Unit Tests**: Created unit tests for services using Moq for dependency isolation
2. **Integration Tests**: Established integration test framework with in-memory database
3. **Test Coverage**: Focused on core CRUD operations and business logic

## Development Guidelines

### Adding New Entities

1. Create the entity class in `Ecommerce.Domain/Entities`
2. Define the repository interface in `Ecommerce.Domain/Interfaces/Repositories`
3. Implement the repository in `Ecommerce.Infrastructure/Repositories`
4. Create the DTO in `Ecommerce.Application/DTOs`
5. Add AutoMapper mappings in `Ecommerce.Application/Mappings/MappingProfile.cs`
6. Implement the service interface and class in `Ecommerce.Application`
7. Register services in dependency injection
8. Create Razor Pages in `Ecommerce.Web/Pages`
9. Write unit and integration tests

### Database Migrations

To create a new migration:

```bash
cd src/Ecommerce.Infrastructure
dotnet ef migrations add <MigrationName> --startup-project ../Ecommerce.Web
```

To apply migrations:

```bash
dotnet ef database update --startup-project ../Ecommerce.Web
```

## Contributing

1. Follow Clean Architecture principles
2. Write unit tests for all business logic
3. Use consistent naming conventions
4. Document public APIs and complex logic
5. Run all tests before committing

## License

[Specify your license here]

## Support

For issues, questions, or contributions, please contact the development team or create an issue in the repository.

---

**Version**: 1.0.0
**Last Updated**: January 2026
**Framework**: .NET 8.0
