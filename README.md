# Blog System

A full-stack blog management system built with ASP.NET Core following Clean Architecture principles.

## Features

### Authentication & Authorization

* User Registration
* User Login
* JWT Authentication
* Role-Based Authorization
* Admin Role
* Editor Role
* User Role

### Blog Posts

* Create Posts
* Edit Posts
* Delete Posts
* View Posts
* Pagination Support
* Category Assignment

### Categories

* Create Categories
* Update Categories
* Delete Categories
* View Categories

### Comments

* Add Comments
* Delete Comments
* View Comments

### Admin Dashboard

* Total Posts Count
* Total Comments Count
* Total Categories Count
* Total Users Count

---

## Technologies Used

### Backend

* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* JWT Authentication
* AutoMapper

### Frontend

* ASP.NET MVC
* Razor Views
* Bootstrap

### Database

* PostgreSQL (Neon)

### Design Patterns

* Repository Pattern
* Unit Of Work Pattern
* Dependency Injection

---

## Architecture

The solution consists of two main applications:

### BlogSystem.Web

RESTful API responsible for:

* Authentication
* Authorization
* Business Logic
* Database Access

### Blog.MVC

MVC Frontend responsible for:

* User Interface
* Consuming API Endpoints
* Authentication Flow

Flow:

User → MVC Application → Web API → PostgreSQL Database

---

## Roles

### Admin

* Manage Posts
* Manage Categories
* Delete Comments
* Access Dashboard

### Editor

* Create Posts
* Edit Posts

### User

* View Posts
* Add Comments

---

## Database

The application uses PostgreSQL hosted on Neon.

Main Entities:

* Users
* Roles
* Posts
* Categories
* Comments

---

## Getting Started

### Clone Repository

```bash
git clone <https://github.com/AhmedHussein727/BlogSystem->
```

### Configure Database

Update Connection String in:

```json
appsettings.json
```

### Apply Migrations

```bash
dotnet ef database update
```

### Run API

```bash
dotnet run
```

### Run MVC Application

```bash
dotnet run
```

---

## Future Improvements

* Email Confirmation
* Docker Deployment
* Cloud Hosting

---

## Author

Ahmed Hussein

ASP.NET Core Backend Developer
