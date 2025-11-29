 **Overview**

A Library Management System built with **C#** and **.NET 7 Web API** using **Onion Architecture**.

Supports user roles, book management, loans, categories, authors, and file uploads.

---

 **Architecture**

**Onion Architecture Layers:**

- **Core:** Entities, Enums, Interfaces, DTOs
- **Business Logic (Services):** Handles application logic (Loans, Books, Users, etc.)
- **Data Access (Repositories):** EF Core repositories for database operations
- **Presentation (API Controllers):** Exposes endpoints for frontend / clients

---

**Authentication & Authorization**

- **ASP.NET Identity** for user management
- **JWT Authentication**
- Roles: `Admin`, `Librarian`, `Member`

---

 **Database**

- **Provider:** SQL Server
- **Entities:** Users, Books, Authors, Categories, Loans, FileUploads
- Relationships managed via EF Core

---

**Features**

- User registration, login, role management
- CRUD for Books, Categories, Authors
- Loan workflow: Pending → Approved/Rejected → Returned
- Automatic late fee calculation
- File uploads (book images)
- Search & filtering for books, authors, categories
