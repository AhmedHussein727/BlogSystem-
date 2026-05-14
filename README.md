# 🚀 Blog System

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-purple?style=for-the-badge&logo=.net)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-Web_API-blue?style=for-the-badge&logo=dotnet)
![MVC](https://img.shields.io/badge/MVC-Application-green?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL_Server-Database-red?style=for-the-badge&logo=microsoftsqlserver)
![JWT](https://img.shields.io/badge/JWT-Authentication-orange?style=for-the-badge&logo=jsonwebtokens)

</div>

---

# 📌 Overview

A modern Blog System built with:

- ASP.NET Core Web API
- ASP.NET Core MVC
- Clean Architecture
- Repository Pattern
- Unit Of Work
- JWT Authentication

The project demonstrates separation of concerns and scalable backend architecture using modern .NET practices.

---

# 🏗️ Architecture


BlogSystemSolution
│
├── Core
│   ├── Domain
│   ├── Service
│   └── Service.Abstraction
│
├── Infrastructure
│   └── Persistence
│
├── Presentation
│   ├── BlogSystem.Web   --> Web API
│   └── Blog.MVC         --> MVC Frontend
✨ Features
🔐 Authentication & Authorization
JWT Authentication
Role-Based Authorization
Admin / Editor / User Roles
📝 Blog Posts
Create Post
Update Post
Delete Post
Get All Posts
Pagination
Post Details
💬 Comments
Add Comment
Delete Comment
Get Post Comments
🌐 MVC Integration
MVC consuming API using HttpClient
Dynamic Details Pages
Comments Rendering
ViewModels Support
🛠️ Technologies
Technology	Usage
ASP.NET Core 8	Backend
ASP.NET MVC	Frontend
Entity Framework Core	ORM
SQL Server	Database
AutoMapper	Mapping
JWT	Authentication
LINQ	Querying
Repository Pattern	Data Access
Unit Of Work	Transaction Management
📂 Project Layers
🔹 Domain

Contains:

Entities
Interfaces
Enums
🔹 Persistence

Contains:

DbContext
Repositories
Configurations
Migrations
🔹 Services

Contains:

Business Logic
DTO Mapping
Validation
🔹 Presentation

Contains:

API Controllers
MVC Controllers
Views
ViewModels
⚙️ Getting Started
1️⃣ Clone Repository
git clone YOUR_REPOSITORY_LINK
2️⃣ Configure Database

Update:

appsettings.json

Connection String.

3️⃣ Apply Migrations
Update-Database
4️⃣ Run Projects

Run both:

BlogSystem.Web
Blog.MVC
🔑 API Authentication

Use JWT Token:

Authorization: Bearer YOUR_TOKEN
📸 Screenshots
🏠 Posts Page

Add Screenshot Here

📄 Post Details

Add Screenshot Here

🔐 Login Page

Add Screenshot Here

📈 Future Improvements
Like System
Rich Text Editor
Image Upload
Categories Dashboard
User Profiles
Admin Dashboard
Caching
SignalR Notifications
👨‍💻 Developer
Ahmed Hussein

ASP.NET Core Backend Developer

⭐ Support

If you like this project:

⭐ Star the repository
🍴 Fork the project
🛠️ Contribute

<div align="center">
🔥 Built with ASP.NET Core & Clean Architecture
</div> `
