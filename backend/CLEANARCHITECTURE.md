# Modern .NET best practice follows Clean Architecture.

## Architecture Layers

- **API (Controllers)**
  - Handles incoming requests and delegates work to the application layer.
- **Infrastructure (DB, APIs, external stuff)**
  - Implements external dependencies like data access, external APIs, and other technical services.
- **Application (use cases, what the system does)**
  - Contains use cases, business logic orchestration, validation, and application-specific behavior.
- **Domain (core business)**
  - Contains core business entities and rules.

## Layer Responsibilities

### Domain (CORE)
- Entities such as `Order`, `User`
- Business rules
- No EF, HTTP, or framework-specific code

Example:
```csharp
public class Order
{
    // Domain logic here
}
```

### Application
- Business logic and abstractions (interfaces)
- Not DB-specific
- Not framework-specific
- Use cases, commands, queries, and DTOs
- Interfaces for repositories and services
- Coordinates domain and infrastructure

Example:
```csharp
public class PayOrderHandler
{
    private readonly IOrderRepository _repo;

    public async Task Handle(Guid orderId)
    {
        var order = await _repo.Get(orderId);
        order.Pay();
        await _repo.Save(order);
    }
}
```

### Infrastructure
- Technical implementation of external concerns
- Data access, APIs, external service integrations
- Implements application interfaces
- Examples: EF Core, external APIs, file system, email

Example:
```csharp
public class OrderRepository : IOrderRepository
{
    // Repository implementation here
}
```

### API (Presentation)
- Controllers only call the application layer
- Serves as the entry point for clients

## Summary
- **Domain Layer** → Core business rules and entities.
- **Application Layer** → Coordinates business rules, use cases, and workflows.
- **Infrastructure Layer** → Handles external dependencies like databases, APIs, and file systems.
- **Presentation** → API/UI entry point for users and clients.