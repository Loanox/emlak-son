# 🏢 Real Estate Portal - Midterm API Roadmap

**Target:** Midterm Evaluation (April 6, 2026)
**Tech Stack:** .NET 8 Core Web API, Entity Framework Core, SQL Server (Code-First)

---

## 🚀 Phase 1: Project Initialization & Package Management
**Goal:** Create the project and install required NuGet packages.
- [x] Create a new `.NET Core Web API` project.
- [x] Install required NuGet packages:
  - `Microsoft.EntityFrameworkCore.SqlServer`
  - `Microsoft.EntityFrameworkCore.Tools` (for Migrations)
  - `Microsoft.AspNetCore.Identity.EntityFrameworkCore` (for Identity)
  - `Microsoft.AspNetCore.Authentication.JwtBearer` (for JWT Authentication)

## 📦 Phase 2: Domain Layer (Models / Entities)
**Goal:** Design the database structure for a Real Estate Portal.
- [x] **AppUser & AppRole:**
  - Create `AppUser` inheriting from `IdentityUser<int>`.
  - Create `AppRole` inheriting from `IdentityRole<int>`.
- [x] **Category Entity:** `Id`, `CategoryName` (e.g., For Sale, For Rent), `Status`.
- [x] **Property (Listing) Entity:** `Id`, `Title`, `Description`, `Price`, `SquareMeters`, `RoomCount`, `City`, `District`, `Address`, `CreatedDate`, `CategoryId`, `AppUserId`.
- [x] **Establish Relationships:**
  - 1-to-Many between `Category` and `Property`.
  - 1-to-Many between `AppUser` and `Property`.

## 🔄 Phase 3: Data Transfer Objects (DTOs)
**Goal:** Create DTOs to abstract database entities from API responses/requests.
- [x] **Auth DTOs:** `UserRegisterDTO`, `UserLoginDTO`.
- [x] **Category DTOs:** `CreateCategoryDTO`, `UpdateCategoryDTO`, `ResultCategoryDTO`.
- [x] **Property DTOs:** `CreatePropertyDTO`, `UpdatePropertyDTO`, `ResultPropertyDTO`.

## 🗄️ Phase 4: Data Access Layer & Migrations
**Goal:** Configure SQL Server connection and Code-First migrations.
- [x] **DbContext Setup:** Create `ApplicationDbContext` inheriting from `IdentityDbContext<AppUser, AppRole, int>`.
- [x] **DbSets:** Add `DbSet<Category> Categories` and `DbSet<Property> Properties`.
- [x] **Connection String:** Define `DefaultConnection` in `appsettings.json`.
- [x] **Service Registration:** Add DbContext to `Program.cs` services.
- [x] **Migrations:** Run `Add-Migration InitialCreate` and `Update-Database`.

## 🏗️ Phase 5: Repository Pattern Implementation
**Goal:** Abstract database operations using the Repository Pattern.
- [x] **Interfaces:** Create `IGenericRepository<T>` with standard CRUD methods (`GetAll`, `GetById`, `Insert`, `Update`, `Delete`).
- [x] **Concrete:** Create `GenericRepository<T>` implementing the interface using Entity Framework.
- [x] **Dependency Injection:** Register repositories in `Program.cs` (`builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));`).

## 🔐 Phase 6: Identity, JWT & Role Management
**Goal:** Secure the API with ASP.NET Identity and JSON Web Tokens.
- [x] **Identity Setup:** Configure `AddIdentity` in `Program.cs` with password and user requirements.
- [x] **Role Management:** Seed default roles (`Admin`, `User`) into the database.
- [x] **JWT Configuration:**
  - Define `JwtSettings` (Issuer, Audience, Key) in `appsettings.json`.
  - Add `AddAuthentication` and `AddJwtBearer` to `Program.cs` to validate tokens.
- [x] **Token Service:** Create a helper class/service to generate JWTs containing `ClaimTypes.NameIdentifier`, `ClaimTypes.Email`, and `ClaimTypes.Role`.

## 📡 Phase 7: API Endpoints (Controllers)
**Goal:** Code all API methods to handle HTTP requests.
- [x] **AuthController:** Endpoints for `[HttpPost("register")]` and `[HttpPost("login")]` returning JWTs.
- [x] **CategoriesController:** Standard CRUD endpoints using Repositories. Add `[Authorize(Roles = "Admin")]` to Insert/Update/Delete.
- [x] **PropertiesController:**
  - Standard CRUD endpoints.
  - Endpoint to get properties by Category.
  - Endpoint for a user to see only their own properties (`[Authorize(Roles = "User")]`).

## 🌐 Phase 8: Source Control
**Goal:** Push to GitHub.
- [x] Initialize Git repository.
- [x] Add `.gitignore` (specifically for Visual Studio and .NET).
- [x] Commit all code with a message: `feat: completed midterm api requirements`.
- [ ] Push to the remote GitHub repository.