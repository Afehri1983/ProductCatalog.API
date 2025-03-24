# Product Catalog API

A .NET Core Web API implementing Clean Architecture and CQRS pattern for managing a product catalog.

## Architecture

This project follows Clean Architecture principles and SOLID design patterns:

- **API Layer** (`ProductCatalog.API`): Controllers and API endpoints
- **Application Layer** (`ProductCatalog.Application`): Business logic, CQRS handlers
- **Domain Layer** (`ProductCatalog.Domain`): Entities and business rules
- **Infrastructure Layer** (`ProductCatalog.Infrastructure`): Data access, external services

### Key Features

- CQRS pattern with MediatR
- Entity Framework Core with SQLite
- Swagger/OpenAPI documentation
- Unit tests for command handlers
- Rich domain model with encapsulation
- Validation and error handling

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or VS Code

### Installation

1. Clone the repository
2. Navigate to the project directory
```bash
cd ProductCatalog.API
```
3. Restore dependencies
```bash
dotnet restore
```
4. Run the application
```bash
dotnet run --project ./ProductCatalog.API/ProductCatalog.API.csproj
```

The API will be available at `http://localhost:5136/swagger`

## API Endpoints

### Products

- **GET /api/Products**: Get all products
- **GET /api/Products/{id}**: Get product by ID
- **POST /api/Products**: Create a new product
- **PUT /api/Products/{id}**: Update an existing product
- **DELETE /api/Products/{id}**: Delete a product

### Example Request (Create Product)

```json
POST /api/Products
{
  "name": "Gaming Laptop",
  "description": "High-performance gaming laptop",
  "price": 1299.99,
  "stock": 50
}
```

## Testing

The project includes comprehensive unit tests for the command handlers:

- `CreateProductCommandHandler`
- `UpdateProductCommandHandler`
- `DeleteProductCommandHandler`

Run the tests using:
```bash
dotnet test
```

## Project Structure

```
ProductCatalog.API/
├── ProductCatalog.API/
│   ├── Controllers/
│   │   └── ProductsController.cs
│   └── Program.cs
├── ProductCatalog.Application/
│   ├── Commands/
│   │   ├── CreateProduct/
│   │   ├── UpdateProduct/
│   │   └── DeleteProduct/
│   └── Queries/
│       ├── GetProducts/
│       └── GetProductById/
├── ProductCatalog.Domain/
│   └── Entities/
└── ProductCatalog.Infrastructure/
    └── Repositories/
```

## Design Decisions

1. **Rich Domain Model**: Products are implemented as rich entities with encapsulated business rules
2. **CQRS**: Separate command and query responsibilities for better scalability
3. **Repository Pattern**: Abstract data access through repositories
4. **Unit Tests**: Focus on testing business logic in command handlers
5. **Validation**: Input validation at both API and domain levels

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License. 