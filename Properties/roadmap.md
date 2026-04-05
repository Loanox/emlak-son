# 🏢 Real Estate Portal - Midterm API Roadmap

[cite_start]**Target:** Midterm Evaluation (April 6, 2026) 
[cite_start]**Tech Stack:** .NET 7.0 Core Web API, Entity Framework Core, SQL Server (Code-First)[cite: 4, 19].

---

## 🚀 Phase 1: Project Initialization & Package Management
[cite_start]**Goal:** Create the project and install required NuGet packages[cite: 23].
- [ ] [cite_start]Create a new `.NET 7.0 Core Web API` project[cite: 4].
- [ ] Install required NuGet packages:
  - `Microsoft.EntityFrameworkCore.SqlServer`
  - `Microsoft.EntityFrameworkCore.Tools` (for Migrations)
  - `Microsoft.AspNetCore.Identity.EntityFrameworkCore` (for Identity)
  - `Microsoft.AspNetCore.Authentication.JwtBearer` (for JWT Authentication)

## 📦 Phase 2: Domain Layer (Models / Entities)
[cite_start]**Goal:** Design the database structure for a Real Estate Portal[cite: 23].
- [ ] **AppUser & AppRole:** - Create `AppUser` inheriting from `IdentityUser<int>`.
  - Create `AppRole` inheriting from `IdentityRole<int>`.
- [ ] **Category Entity:** `Id`, `CategoryName` (e.g., For Sale, For Rent), `Status`.
- [ ] **Property (Listing) Entity:** `Id`, `Title`, `Description`, `Price`, `SquareMeters`, `RoomCount`, `City`, `District`, `Address`, `CreatedDate`, `CategoryId`, `AppUserId`.
- [ ] **Establish Relationships:** - 1-to-Many between `Category` and `Property`.
  - 1-to-Many between `AppUser` and `Property`.

## 🔄 Phase 3: Data Transfer Objects (DTOs)
[cite_start]**Goal:** Create DTOs to abstract database entities from API responses/requests[cite: 23].
- [ ] **Auth DTOs:** `UserRegisterDTO`, `UserLoginDTO`.
- [ ] **Category DTOs:** `CreateCategoryDTO`, `UpdateCategoryDTO`, `ResultCategoryDTO`.
- [ ] **Property DTOs:** `CreatePropertyDTO`, `UpdatePropertyDTO`, `ResultPropertyDTO`.

## 🗄️ Phase 4: Data Access Layer & Migrations
[cite_start]**Goal:** Configure SQL Server connection and Code-First migrations[cite: 19, 23].
- [ ] **DbContext Setup:** Create `ApplicationDbContext` inheriting from `IdentityDbContext<AppUser, AppRole, int>`.
- [ ] **DbSets:** Add `DbSet<Category> Categories` and `DbSet<Property> Properties`.
- [ ] **Connection String:** Define `DefaultConnection` in `appsettings.json`.
- [ ] **Service Registration:** Add DbContext to `Program.cs` services.
- [ ] [cite_start]**Migrations:** Run `Add-Migration InitialCreate` and `Update-Database` in Package Manager Console[cite: 23].

## 🏗️ Phase 5: Repository Pattern Implementation
[cite_start]**Goal:** Abstract database operations using the Repository Pattern[cite: 23].
- [ ] **Interfaces:** Create `IGenericRepository<T>` with standard CRUD methods (`GetAll`, `GetById`, `Insert`, `Update`, `Delete`).
- [ ] **Concrete:** Create `GenericRepository<T>` implementing the interface using Entity Framework.
- [ ] **Dependency Injection:** Register repositories in `Program.cs` (`builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));`).

## 🔐 Phase 6: Identity, JWT & Role Management
[cite_start]**Goal:** Secure the API with ASP.NET Identity and JSON Web Tokens[cite: 23].
- [ ] **Identity Setup:** Configure `AddIdentity` in `Program.cs` with password and user requirements.
- [ ] [cite_start]**Role Management:** Seed default roles (`Admin`, `User`) into the database[cite: 23].
- [ ] **JWT Configuration:** - Define `JwtSettings` (Issuer, Audience, Key) in `appsettings.json`.
  - Add `AddAuthentication` and `AddJwtBearer` to `Program.cs` to validate tokens.
- [ ] **Token Service:** Create a helper class/service to generate JWTs containing `ClaimTypes.NameIdentifier`, `ClaimTypes.Email`, and `ClaimTypes.Role`.

## 📡 Phase 7: API Endpoints (Controllers)
[cite_start]**Goal:** Code all API methods to handle HTTP requests[cite: 23].
- [ ] **AuthController:** Endpoints for `[HttpPost("register")]` and `[HttpPost("login")]` returning JWTs.
- [ ] **CategoriesController:** Standard CRUD endpoints using Repositories. Add `[Authorize(Roles = "Admin")]` to Insert/Update/Delete.
- [ ] **PropertiesController:** - Standard CRUD endpoints.
  - Endpoint to get properties by Category.
  - Endpoint for a user to see only their own properties (`[Authorize(Roles = "User")]`).

## 🌐 Phase 8: Source Control
[cite_start]**Goal:** Push to GitHub[cite: 23].
- [ ] Initialize Git repository.
- [ ] Add `.gitignore` (specifically for Visual Studio and .NET).
- [ ] Commit all code with a message: `feat: completed midterm api requirements`.
- [ ] [cite_start]Push to the remote GitHub repository[cite: 23].