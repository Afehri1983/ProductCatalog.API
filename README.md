# Product Catalog API

A clean architecture implementation of a Product Catalog API using .NET 8.0.

## Features

- Clean Architecture implementation
- CQRS pattern with MediatR
- Entity Framework Core with In-Memory Database
- Swagger UI for API documentation and testing
- Unit tests with xUnit
- Repository pattern for data access abstraction

## Project Structure

- **Domain Layer** (`ProductCatalog.Domain`): Core business logic and entities
- **Application Layer** (`ProductCatalog.Application`): Use cases and business rules
- **Infrastructure Layer** (`ProductCatalog.Infrastructure`): Data access, external services
- **API Layer** (`ProductCatalog.API`): HTTP endpoints and controllers
- **Tests** (`ProductCatalog.Application.Tests`): Unit tests for application layer

## Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or VS Code with C# extensions

## Getting Started

1. Clone the repository
2. Navigate to the project directory
3. Run the application:
   ```bash
   dotnet run --project ProductCatalog.API
   ```
4. Access Swagger UI at: http://localhost:5136/swagger

## API Endpoints

- GET /api/Products - Get all products
- GET /api/Products/{id} - Get a product by ID
- POST /api/Products - Create a new product
- PUT /api/Products/{id} - Update an existing product
- DELETE /api/Products/{id} - Delete a product

## Testing

The project includes comprehensive unit tests using xUnit. Here's how to run and work with the tests:

### Running Tests

1. Run all tests:
   ```bash
   dotnet test
   ```

2. Run tests for a specific project:
   ```bash
   dotnet test ProductCatalog.Application.Tests/ProductCatalog.Application.Tests.csproj
   ```

3. Run a specific test:
   ```bash
   dotnet test --filter "FullyQualifiedName=ProductCatalog.Application.Tests.Commands.CreateProductCommandHandlerTests.Handle_ValidProduct_ReturnsId"
   ```

4. Run tests with detailed output:
   ```bash
   dotnet test -v n
   ```

### Test Coverage

To generate test coverage reports:
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov.info
```

### Test Categories

The tests cover various scenarios:

1. **CreateProductCommandHandler Tests**
   - Valid product creation
   - Empty name validation
   - Invalid price validation (<= 0)
   - Invalid stock validation (negative)
   - Null command validation

2. **DeleteProductCommandHandler Tests**
   - Valid product deletion
   - Invalid ID validation (<= 0)
   - Non-existent product handling
   - Null command validation

3. **UpdateProductCommandHandler Tests**
   - Valid product update
   - Non-existent product handling
   - Null command validation

4. **GetProductQueryHandler Tests**
   - Valid product retrieval
   - Non-existent product handling
   - Null query validation

5. **GetProductsQueryHandler Tests**
   - List all products
   - Empty product list handling
   - Null query validation

### Test Structure

Each test follows the Arrange-Act-Assert pattern:
```csharp
[Fact]
public async Task Handle_ValidProduct_ReturnsId()
{
    // Arrange
    var command = new CreateProductCommand("Test Product", "Description", 99.99m, 10);
    
    // Act
    var result = await _handler.Handle(command, CancellationToken.None);
    
    // Assert
    Assert.Equal(expectedId, result);
}
```

## Architecture

The project follows Clean Architecture principles:

1. **Domain Layer**
   - Contains business entities
   - Defines core business rules
   - No dependencies on other layers

2. **Application Layer**
   - Implements use cases
   - Contains interfaces for infrastructure
   - Depends only on Domain layer

3. **Infrastructure Layer**
   - Implements data access
   - Contains external service implementations
   - Depends on Application layer

4. **API Layer**
   - Handles HTTP requests
   - Maps DTOs to domain entities
   - Depends on Application layer

## Repository Pattern

The project uses the Repository pattern to abstract data access:

1. **Interface Definition** (Application Layer)
   ```csharp
   public interface IProductRepository
   {
       Task<IEnumerable<Product>> GetAllAsync();
       Task<Product?> GetByIdAsync(int id);
       Task<int> AddAsync(Product product);
       Task UpdateAsync(Product product);
       Task<bool> DeleteAsync(int id);
   }
   ```

2. **Implementation** (Infrastructure Layer)
   ```csharp
   public class ProductRepository : Repository<Product>, IProductRepository
   {
       public ProductRepository(ApplicationDbContext context) : base(context)
       {
       }
   }
   ```

## CQRS Pattern

The project implements CQRS using MediatR:

1. **Commands** (Write Operations)
   ```csharp
   public record CreateProductCommand : IRequest<int>
   {
       public string Name { get; init; }
       public string Description { get; init; }
       public decimal Price { get; init; }
       public int Stock { get; init; }
   }
   ```

2. **Queries** (Read Operations)
   ```csharp
   public record GetProductQuery : IRequest<Product>
   {
       public int Id { get; init; }
   }
   ```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details.
