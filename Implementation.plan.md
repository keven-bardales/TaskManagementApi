# Task Management API - Implementation Plan & Todo List

## 📋 Project Overview
Building a clean, maintainable Task Management API using vertical slice architecture with feature-based organization.

## 🏗️ Architecture Decisions

### Core Architecture Pattern: Vertical Slice Architecture
- **Features Folder**: Each feature is self-contained with its own Api, Application, and Domain layers
- **Common Folder**: Shared infrastructure, middleware, and cross-cutting concerns
- **Separation of Concerns**: Clear boundaries between layers within each feature

### Technology Stack
- **.NET 8.0**: Latest LTS version for performance and features
- **Entity Framework Core 8.0**: For data persistence (with SQLite for simplicity)
- **MediatR**: For CQRS pattern implementation
- **FluentValidation**: For request validation
- **JWT Bearer Authentication**: For bonus authentication requirement
- **Swagger/OpenAPI**: For API documentation
- **xUnit**: For unit testing

## 📝 Implementation Plan

### Phase 1: Project Setup (15 minutes)
1. Create solution and project structure
2. Install required NuGet packages
3. Set up basic folder structure
4. Configure Program.cs with minimal API setup

### Phase 2: Domain Layer (20 minutes)
1. Create Task entity with proper encapsulation
2. Define ITaskRepository interface
3. Add domain validation rules
4. Create task specifications for filtering

### Phase 3: Infrastructure Layer (25 minutes)
1. Set up EF Core with SQLite
2. Create ApplicationDbContext
3. Implement TaskRepository
4. Add database migrations
5. Configure dependency injection

### Phase 4: Application Layer (30 minutes)
1. Implement CQRS commands and queries
2. Create command/query handlers
3. Add FluentValidation validators
4. Implement mapping between domain and DTOs

### Phase 5: API Layer (25 minutes)
1. Create TasksController with all CRUD endpoints
2. Define request/response contracts
3. Implement query parameter filtering
4. Add Swagger documentation

### Phase 6: Middleware & Error Handling (20 minutes)
1. Create global error handling middleware
2. Add validation middleware
3. Implement consistent error responses
4. Add logging

### Phase 7: Authentication (Bonus - 20 minutes)
1. Implement JWT service
2. Add authentication middleware
3. Secure endpoints
4. Add user context

### Phase 8: Testing (20 minutes)
1. Write unit tests for handlers
2. Write integration tests for controllers
3. Test repository pattern
4. Validate error scenarios

### Phase 9: Documentation & Cleanup (15 minutes)
1. Write comprehensive README
2. Add XML documentation comments
3. Clean up code
4. Prepare submission

## ✅ Detailed Todo List

### 🚀 Initial Setup
- [ ] Create new solution in Visual Studio
- [ ] Create TaskManagementApi Web API project (.NET 8)
- [ ] Create TaskManagementApi.Tests xUnit project
- [ ] Add project reference from Tests to Api

### 📦 NuGet Packages Installation
- [ ] Install Entity Framework Core packages:
  - [ ] Microsoft.EntityFrameworkCore.Sqlite
  - [ ] Microsoft.EntityFrameworkCore.Design
  - [ ] Microsoft.EntityFrameworkCore.Tools
- [ ] Install MediatR:
  - [ ] MediatR
  - [ ] MediatR.Extensions.Microsoft.DependencyInjection
- [ ] Install FluentValidation:
  - [ ] FluentValidation
  - [ ] FluentValidation.DependencyInjectionExtensions
- [ ] Install Authentication packages:
  - [ ] Microsoft.AspNetCore.Authentication.JwtBearer
  - [ ] System.IdentityModel.Tokens.Jwt
- [ ] Install Swagger:
  - [ ] Swashbuckle.AspNetCore
- [ ] Install Testing packages (in test project):
  - [ ] Microsoft.AspNetCore.Mvc.Testing
  - [ ] Moq
  - [ ] FluentAssertions

### 📁 Folder Structure Creation
- [ ] Create Features folder
- [ ] Create Features/Tasks folder
- [ ] Create Features/Tasks/Api folder
- [ ] Create Features/Tasks/Api/Contracts folder
- [ ] Create Features/Tasks/Application folder
- [ ] Create Features/Tasks/Application/Commands folder
- [ ] Create Features/Tasks/Application/Queries folder
- [ ] Create Features/Tasks/Application/Handlers folder
- [ ] Create Features/Tasks/Application/Validators folder
- [ ] Create Features/Tasks/Domain folder
- [ ] Create Common folder
- [ ] Create Common/Infrastructure folder
- [ ] Create Common/Infrastructure/Persistence folder
- [ ] Create Common/Infrastructure/Repositories folder
- [ ] Create Common/Infrastructure/Authentication folder
- [ ] Create Common/Middleware folder
- [ ] Create Common/Extensions folder

### 🎯 Domain Layer Implementation
- [ ] Create Task.cs entity with properties:
  - [ ] Id (Guid)
  - [ ] Title (string, required)
  - [ ] Description (string, optional)
  - [ ] IsCompleted (bool)
  - [ ] DueDate (DateTime?, optional)
  - [ ] CreatedAt (DateTime)
  - [ ] UpdatedAt (DateTime)
- [ ] Create ITaskRepository interface with methods:
  - [ ] GetAllAsync(bool? isCompleted, DateTime? dueDate)
  - [ ] GetByIdAsync(Guid id)
  - [ ] AddAsync(Task task)
  - [ ] UpdateAsync(Task task)
  - [ ] DeleteAsync(Guid id)
  - [ ] ExistsAsync(Guid id)
- [ ] Add domain validation methods to Task entity

### 🏗️ Infrastructure Implementation
- [ ] Create ApplicationDbContext
- [ ] Configure Task entity in DbContext
- [ ] Create TaskRepository implementation
- [ ] Add repository filtering logic
- [ ] Create initial migration
- [ ] Configure SQLite connection string

### 📋 Application Layer Implementation
- [ ] Create Commands:
  - [ ] CreateTaskCommand
  - [ ] UpdateTaskCommand
  - [ ] DeleteTaskCommand
- [ ] Create Queries:
  - [ ] GetTasksQuery
  - [ ] GetTaskByIdQuery
- [ ] Create Handlers:
  - [ ] CreateTaskHandler
  - [ ] UpdateTaskHandler
  - [ ] DeleteTaskHandler
  - [ ] GetTasksHandler
  - [ ] GetTaskByIdHandler
- [ ] Create Validators:
  - [ ] CreateTaskValidator
  - [ ] UpdateTaskValidator
- [ ] Add AutoMapper profiles or manual mapping

### 🌐 API Layer Implementation
- [ ] Create TasksController
- [ ] Implement endpoints:
  - [ ] POST /api/tasks (Create)
  - [ ] GET /api/tasks (Get all with filters)
  - [ ] GET /api/tasks/{id} (Get by ID)
  - [ ] PUT /api/tasks/{id} (Full update)
  - [ ] PATCH /api/tasks/{id} (Partial update)
  - [ ] DELETE /api/tasks/{id} (Delete)
- [ ] Create DTOs:
  - [ ] CreateTaskRequest
  - [ ] UpdateTaskRequest
  - [ ] TaskResponse
- [ ] Add Swagger annotations
- [ ] Implement query parameter binding

### 🛡️ Middleware & Error Handling
- [ ] Create ErrorHandlingMiddleware
- [ ] Create ValidationMiddleware
- [ ] Define error response format
- [ ] Add logging with ILogger
- [ ] Configure middleware pipeline

### 🔐 Authentication (Bonus)
- [ ] Create JwtService
- [ ] Configure JWT settings in appsettings
- [ ] Add authentication to Program.cs
- [ ] Create login endpoint
- [ ] Secure task endpoints with [Authorize]
- [ ] Add user context to requests

### 🧪 Testing Implementation
- [ ] Unit Tests:
  - [ ] Test Task entity validation
  - [ ] Test CreateTaskHandler
  - [ ] Test UpdateTaskHandler
  - [ ] Test GetTasksHandler
  - [ ] Test validators
- [ ] Integration Tests:
  - [ ] Test POST /api/tasks
  - [ ] Test GET /api/tasks with filters
  - [ ] Test PATCH /api/tasks/{id}
  - [ ] Test DELETE /api/tasks/{id}
  - [ ] Test error scenarios

### 📚 Documentation
- [ ] Write README.md with:
  - [ ] Project description
  - [ ] Architecture explanation
  - [ ] Setup instructions
  - [ ] API endpoints documentation
  - [ ] Design decisions
  - [ ] Known limitations
- [ ] Add XML documentation comments
- [ ] Configure Swagger documentation
- [ ] Add example requests/responses

### 🎁 Final Touches
- [ ] Code cleanup and formatting
- [ ] Remove unused usings
- [ ] Ensure consistent naming
- [ ] Add appropriate comments
- [ ] Test full application flow
- [ ] Create zip file for submission

## 🎨 Key Design Patterns Used

1. **Repository Pattern**: Abstraction over data access
2. **CQRS**: Separation of commands and queries
3. **Mediator Pattern**: Decoupling controllers from handlers
4. **Vertical Slice Architecture**: Feature-based organization
5. **Dependency Injection**: IoC container for loose coupling
6. **Unit of Work**: Through EF Core DbContext

## 📊 Success Metrics

- ✅ All CRUD operations working
- ✅ Query filtering implemented
- ✅ Partial updates supported
- ✅ Clean code with consistent formatting
- ✅ Proper error handling
- ✅ Comprehensive README
- ✅ Unit tests passing
- ✅ Bonus: Persistence with SQLite
- ✅ Bonus: JWT authentication

## 🚦 Implementation Order

1. Start with domain entity
2. Build infrastructure layer
3. Implement application logic
4. Create API endpoints
5. Add middleware
6. Implement authentication (if time permits)
7. Write tests
8. Document everything

This plan ensures we build from the inside out, following clean architecture principles while maintaining the feature-based organization you prefer.