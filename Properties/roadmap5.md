# ?? Real Estate Portal - Final Evaluation Roadmap

**Target:** Final Evaluation
**Tech Stack:** .NET 8 Core Web API, ASP.NET Core MVC, jQuery AJAX, Bootstrap 5

---

## ?? Phase 1-8: Midterm API Requirements (Completed)
- [x] Initial Project Setup & Packages
- [x] Domain Models (AppUser, AppRole, Category, Property)
- [x] DTOs & Mapping
- [x] DbContext & Migrations
- [x] Generic Repository Pattern
- [x] Identity, JWT & Role Management
- [x] Basic API Endpoints (Auth, Categories, Properties)
- [x] Initial GitHub Push

---

## ??? Phase 9: Advanced Property Features (API Enhancement)
**Goal:** Make the property listings realistic and fully featured.
- [x] **Property Updates:** Add `Status` (Active, Sold, Inactive), `Latitude`, and `Longitude` to `Property` entity.
- [x] **Image Upload Service:** Create a `PropertyImage` model (1-to-Many with Property).
- [x] **File Storage:** Write an API endpoint to accept `IFormFile` and save property images locally or via cloud.
- [x] **Migrations:** Apply changes to the database.

## ?? Phase 10: Search, Filtering & Pagination (API Enhancement)
**Goal:** Allow users to find properties efficiently.
- [x] Add Pagination support to `PropertiesController` (e.g., `PageNumber`, `PageSize`).
- [x] Create an Advanced Search endpoint `[HttpGet("search")]` filtering by:
  - Minimum and Maximum Price
  - City and District
  - Room Count
  - Square Meters

## ?? Phase 11: User Interaction (API Enhancement)
**Goal:** Add core user engagement features to the API.
- [x] **Favorites:** Create `FavoriteProperty` entity (Many-to-Many between User and Property). Create endpoints to Add/Remove/List favorites.
- [x] **Comments/Reviews:** Create `Comment` entity for properties. Add endpoints for creating and approving comments.

## ?? Phase 12: Frontend - MVC Application Setup
**Goal:** Create the consumer-facing web application as required by the project rules.
- [x] Create a new `.NET Core MVC` project within the solution.
- [x] Configure `HttpClient` in the MVC project to consume the Web API.
- [x] Implement Bootstrap 5 (or higher) for responsive design and visual consistency.

## ?? Phase 13: Frontend - User Interface (Jquery AJAX)
**Goal:** Build the public-facing pages and user dashboard.
- [x] **Auth Pages:** Create Login and Register views using Jquery AJAX to call API. Store JWT in LocalStorage/Cookies.
- [x] **Home Page:** Display latest/featured properties dynamically.
- [x] **Property Listing & Details:** Pages to search, filter, and view full details (including images and map) of a property.
- [ ] **User Dashboard:** A protected area for standard users to manage their listings (`GetMine`) and favorites.

## ??? Phase 14: Frontend - Admin Panel (Jquery AJAX)
**Goal:** Build the control center for site administrators.
- [x] **Admin Layout:** Create a separate, secure layout/dashboard for users with the "Admin" role.
- [x] **Category Management UI:** CRUD operations for categories via AJAX.
- [ ] **User Management UI:** View registered users and manage their roles via API (`ChangeRole`).
- [ ] **Global Property Management:** Ability to delete or approve any property on the site.

## ?? Phase 15: Final Submission & Polish
**Goal:** Prepare the project for final grading.
- [ ] Test the entire flow (Register -> Login -> Create Listing -> Search Listing).
- [ ] **GitHub Update:** Push all API and MVC code to the GitHub repository.
- [ ] **Demonstration Video:** Record a <5-minute video presenting the code structure and frontend UI functionality.
- [ ] Upload video to YouTube and prepare submission links.