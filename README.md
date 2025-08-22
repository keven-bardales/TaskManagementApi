# Task Management API

A RESTful API for task management built with .NET 8, demonstrating clean architecture principles and modern development practices.

## 🚀 Quick Start

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022 or VS Code

### Setup Instructions

1. **Clone the repository**
   ```bash
   git clone [repository-url]
   cd TaskManagementApi
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

4. **Access the API**
   - Swagger UI: `https://localhost:[port]/` (port shown in console output)
   - The database (tasks.db) will be created automatically on first run

## 📋 API Documentation

### Authentication Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/register` | Register a new user |
| POST | `/api/auth/login` | Login and get JWT token |

### Task Endpoints (🔒 Authentication Required)
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/tasks` | Get all tasks with optional filters |
| GET | `/api/tasks/{id}` | Get a specific task |
| POST | `/api/tasks` | Create a new task |
| PUT | `/api/tasks/{id}` | Full update of a task |
| PATCH | `/api/tasks/{id}` | Partial update of a task |
| DELETE | `/api/tasks/{id}` | Delete a task |

### Query Parameters
- `GET /api/tasks?isCompleted=false` - Filter by completion status
- `GET /api/tasks?dueDate=2024-01-25` - Filter by due date
- Both parameters can be combined

### Sample Requests

**Register User:**
```json
POST /api/auth/register
{
  "username": "john_doe",
  "password": "password123"
}
```

**Login:**
```json
POST /api/auth/login
{
  "username": "john_doe",
  "password": "password123"
}
```

**Create Task (requires JWT token in Authorization header):**
```json
POST /api/tasks
Headers: Authorization: Bearer {jwt_token}
{
  "title": "Complete project",
  "description": "Finish the API implementation",
  "dueDate": "2024-01-25T10:00:00Z"
}
```

**Partial Update:**
```json
PATCH /api/tasks/{id}
Headers: Authorization: Bearer {jwt_token}
{
  "isCompleted": true
}
```

## 🏗️ Architecture

### Vertical Slice Architecture
The project uses vertical slice architecture for better feature cohesion and maintainability:
- **Feature Cohesion**: All code related to a specific feature is organized together
- **Reduced Coupling**: Each feature is self-contained with minimal dependencies
- **Scalability**: New features can be added without affecting existing ones
- **Team Collaboration**: Different teams can work on different features independently

### Design Patterns

#### CQRS Pattern with MediatR
- **Separation of Concerns**: Commands (writes) and Queries (reads) are handled separately
- **Testability**: Each handler can be unit tested in isolation
- **Flexibility**: Read and write operations can be optimized independently
- **Decoupling**: Controllers don't directly depend on business logic

#### Repository Pattern
- **Data Access Abstraction**: Domain layer is decoupled from Entity Framework
- **Testability**: Easy to mock for unit testing
- **Flexibility**: Data providers can be switched without changing business logic

#### Domain-Driven Design
- **Rich Domain Models**: TaskEntity and UserEntity encapsulate business logic
- **Data Integrity**: Private setters ensure controlled data modification
- **Domain Validation**: Business rules are enforced at the domain level
- **Feature-Specific Configurations**: Each feature manages its own entity configuration

## 🛠️ Technology Stack

| Library | Version | Purpose |
|---------|---------|---------|
| .NET | 8.0 | Framework |
| Entity Framework Core | 8.0.x | ORM for data persistence |
| MediatR | 13.0.0 | CQRS implementation |
| FluentValidation | 12.0.0 | Request validation |
| SQLite | 8.0.x | Database provider |
| Swashbuckle.AspNetCore | 6.x | Swagger/OpenAPI documentation |
| JWT Bearer Authentication | 8.0.x | JWT token authentication |
| BCrypt.Net-Next | 4.0.3 | Password hashing |

## 📁 Project Structure

```
TaskManagementApi/
├── Features/               # Feature-based organization
│   ├── Auth/              # Authentication feature
│   │   ├── Api/           # Auth controllers & DTOs  
│   │   └── Application/   # Auth commands & handlers
│   ├── Tasks/             # Task management feature
│   │   ├── Api/           # Controllers & DTOs
│   │   ├── Application/   # Business logic & handlers
│   │   ├── Domain/        # TaskEntity & interfaces
│   │   └── Infrastructure/
│   │       ├── Persistence/# TaskEntityConfiguration
│   │       └── Repositories/# TaskRepository
│   └── Users/             # User management feature
│       ├── Domain/        # UserEntity & interfaces
│       └── Infrastructure/
│           ├── Persistence/# UserEntityConfiguration
│           └── Repositories/# UserRepository
├── Common/                # Shared infrastructure
│   └── Infrastructure/
│       ├── Authentication/# JWT service
│       └── Persistence/   # ApplicationDbContext
└── Program.cs            # Application configuration
```

## 🚦 Getting Started

### Running the Application

1. **Start the API**
   ```bash
   dotnet run
   ```

2. **Access Swagger UI**
   Navigate to `https://localhost:[port]/` in your browser

3. **Register a User**
   Use the `/api/auth/register` endpoint to create an account

4. **Authenticate**
   - Login using `/api/auth/login` to receive a JWT token
   - Click "Authorize" in Swagger UI
   - Enter: `Bearer {your-jwt-token}`

5. **Manage Tasks**
   Use the authenticated task endpoints to create, read, update, and delete tasks

## ⚙️ Configuration

### Security Configuration
⚠️ **Important**: The current configuration stores JWT secrets in `appsettings.json` for demonstration purposes only.

For production deployments:
- Use `appsettings.Development.json` for local development (add to .gitignore)
- Store secrets in environment variables or secure vaults (Azure Key Vault, AWS Secrets Manager, etc.)
- Never commit sensitive configuration to source control

## 🔄 Future Enhancements

### Planned Improvements
- **Global Exception Handling**: Implement middleware for centralized error handling
- **Advanced Validation**: Complete FluentValidation implementation with custom validators
- **Result Pattern**: Replace exceptions with Result<T> pattern for better error handling
- **Pagination**: Add pagination support for large result sets
- **Structured Logging**: Implement comprehensive logging with Serilog or similar
- **Caching**: Add caching layer for frequently accessed data
- **API Versioning**: Implement versioning strategy for backward compatibility
- **Rate Limiting**: Add rate limiting to prevent API abuse
- **Health Checks**: Implement health check endpoints for monitoring

## 📝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🤝 Acknowledgments

- Built with clean architecture principles
- Follows SOLID principles and best practices
- Inspired by Domain-Driven Design concepts
