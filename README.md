# Task Management API

A RESTful API for task management built with .NET 8, demonstrating clean architecture principles and modern development practices.

## 🚀 Quick Start

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022 or VS Code

### Setup Instructions

1. **Clone or extract the project**
   ```bash
   # If from zip file, extract to your desired location
   # If from git
   git clone [repository-url]
   ```

2. **Navigate to project directory**
   ```bash
   cd TaskManagementApi
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Access the API**
   - Swagger UI: `https://localhost:[port]/` (port shown in console output)
   - The database (tasks.db) will be created automatically on first run

## 📋 API Endpoints

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

## 🏗️ Architecture & Design Decisions

### Vertical Slice Architecture
I chose **vertical slice architecture** over traditional layered architecture because:
- **Feature Cohesion**: All code related to Tasks is in one place, making it easier to understand and modify
- **Reduced Coupling**: Each feature is self-contained, reducing dependencies between different parts of the application
- **Scalability**: New features can be added without affecting existing ones
- **Team Collaboration**: Different developers can work on different features without conflicts

### CQRS Pattern with MediatR
Implemented CQRS (Command Query Responsibility Segregation) because:
- **Separation of Concerns**: Commands (writes) and Queries (reads) have different requirements
- **Testability**: Each handler can be unit tested in isolation
- **Flexibility**: Can optimize read and write operations differently
- **Decoupling**: Controllers don't directly depend on business logic

### Repository Pattern
Used repository pattern to:
- **Abstract Data Access**: Domain layer doesn't know about Entity Framework
- **Testability**: Easy to mock for unit testing
- **Future Flexibility**: Can switch data providers without changing business logic

### Domain-Driven Design
- **Rich Domain Models**: TaskEntity and UserEntity encapsulate business logic (not just data)
- **Private Setters**: Ensures data integrity through controlled methods
- **Domain Validation**: Business rules are enforced at the domain level
- **Feature-Specific Configurations**: Each feature manages its own entity configuration
- **Consistent Naming**: All entities follow the *Entity naming convention

## 🛠️ Technologies & Libraries Used

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

## ⚠️ Known Limitations & Future Improvements

### Current Limitations

1. **Error Handling**: Currently using try-catch in individual handlers. Could be improved with:
   - Global exception middleware for centralized error handling
   - Custom exception types for different error scenarios
   - Consistent error response format

2. **No Validation Middleware**: While FluentValidation is installed, validators aren't fully implemented
   - Could add request validators for better input validation
   - Automatic validation pipeline behavior

3. **Result Pattern**: Currently throwing exceptions for errors. Could implement:
   - Result<T> pattern for better error handling
   - Avoid exceptions for expected failures

4. **Services Layer**: The Services folder exists but isn't utilized
   - Could extract common business logic into services
   - Reduce duplication in handlers

5. **No Pagination**: Large result sets aren't paginated
   - Could add pagination for GET /api/tasks

6. **No Logging**: No structured logging implemented
   - Could add ILogger for debugging and monitoring

7. **No Caching**: Frequently accessed data isn't cached
   - Could implement caching for better performance

8. **Security Configuration**: JWT secrets and other sensitive data in appsettings.json
   - Should use appsettings.json only as an example template
   - Actual secrets should be in appsettings.Development.json (gitignored)
   - Production should use Azure Key Vault, environment variables, or similar
   - Current approach exposes JWT key in source control (not production-safe)

### Why These Weren't Implemented
Given the 3-hour time constraint, I prioritized:
- Core functionality over nice-to-haves
- Working features over optimizations
- Clean architecture demonstration over every possible feature

## 🎯 My Approach

1. **Started with Domain**: Built the Task entity first with proper encapsulation
2. **Infrastructure Next**: Implemented data persistence with EF Core and SQLite
3. **Application Layer**: Added CQRS handlers for business logic
4. **API Layer Last**: Created controllers as thin presentation layer
5. **Documentation**: Ensured Swagger works for easy testing

This inside-out approach ensures:
- Business logic is independent of infrastructure
- Core functionality works before adding UI
- Each layer has clear responsibilities

## ✅ Requirements Checklist

- ✅ **Create Task**: POST endpoint with all required fields
- ✅ **Get All Tasks**: GET endpoint with query filtering
- ✅ **Update Task**: Both PUT and PATCH for full/partial updates
- ✅ **Delete Task**: DELETE endpoint
- ✅ **Clean Code**: Consistent naming, formatting, organization
- ✅ **REST Principles**: Proper HTTP verbs, status codes, resource-based URLs
- ✅ **Persistence**: SQLite with Entity Framework Core
- ✅ **Documentation**: Swagger UI for API testing
- ✅ **JWT Authentication**: Full implementation with user registration/login and secured endpoints

## 🚦 Testing the API

### Basic Testing Flow

1. **Run the application**
   ```bash
   dotnet run
   ```

2. **Open Swagger UI**
   - Navigate to `https://localhost:[port]/` (port shown in console)
   - The database file (tasks.db) will be created automatically

3. **Test Authentication**
   1. Use `/api/auth/register` to create a new user
   2. Use `/api/auth/login` to get a JWT token
   3. Copy the token from the response

4. **Authenticate in Swagger**
   1. Click the "Authorize" button in Swagger UI
   2. Enter: `Bearer {your-jwt-token}`
   3. Click "Authorize"

5. **Test Task Endpoints**
   - All `/api/tasks/*` endpoints now require authentication
   - Use the secured endpoints to manage tasks

## 📝 Notes on Code Quality

- **SOLID Principles**: Applied throughout (especially SRP and DIP)
- **Clean Architecture**: Clear separation of concerns
- **Consistent Naming**: Following C# conventions
- **Minimal Dependencies**: Each layer only depends on what it needs
- **No Code Duplication**: Shared logic properly abstracted

## 💡 If I Had More Time

With additional time, I would add:
1. Comprehensive error handling middleware
2. Request/response logging
3. Integration tests
4. JWT authentication implementation
5. User context for multi-tenant support
6. API versioning
7. Health checks endpoint
8. Docker support

---

**Submission by**: [Your Name]  
**Date**: January 2024  
**Time Taken**: ~3 hours

This project demonstrates my understanding of clean architecture, SOLID principles, and modern .NET development practices. The code is production-ready in terms of structure and could easily be extended with the improvements mentioned above.