# Clean Architecture Overview

Modern .NET best practice follows Clean Architecture.

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

## Links
- https://sharpskill.dev/en/blog/dotnet/clean-architecture-dotnet-practical-guide
- https://dev.to/ravivis13370227/clean-architecture-in-net-application-step-by-step-2ol0



use docker with sql server
steps:
1. create docker-compose.yml
2. > docker-compose up -d
3. run 
> docker inspect <container_id> | grep IPAddress 
and get the ip adress
4. in appsettings.json add: "ConnectionStrings": {
    "Default": "server=<ip_adress>,1433;Database=TaskDb;User Id=sa;Password=\"5ZI6=q;A0ni=\";TrustServerCertificate=True"
  },
5. in Infra project add ef core packages:
> dotnet tool install dotnet-ef
> dotnet add package Microsoft.EntityFrameworkCore.SqlServer
> dotnet add package Microsoft.EntityFrameworkCore.Tool
> dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.17

7.add migration, see AutoAddDbMigration method
8. run only once in Infra project: 
> dotnet ef migrations add InitialCreate
> dotnet ef database update

9. debug sql:
> docker exec -it sqlserver "bash"
> /opt/mssql-tools18/bin/sqlcmd -S localhost -U SA -C
> select name from sys.databases
> GO