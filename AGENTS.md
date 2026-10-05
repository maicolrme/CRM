# AGENTS.md

## Project

This repository contains a simple **multi-language CRM MVP** built with ASP.NET Core MVC.

The application is intentionally simple.

The objective is to create a professional CRM that is easy to understand, develop and extend without introducing unnecessary enterprise architecture.

---

# Version

Current version:

```text
v1
```

Use semantic project versions for significant changes.

```text
v1 → v1.1
v1.1 → v2
```

Do not increment the version for every small bug fix.

---

# Core Principles

Always prioritize:

1. Simplicity
2. Maintainability
3. Security
4. Readability
5. Consistent UI
6. Fast development

Do not introduce architectural complexity without a concrete requirement.

---

# Technology

Use:

* ASP.NET Core MVC
* C#
* Entity Framework Core
* SQLite
* Razor Views
* Tailwind CSS CDN
* Docker
* Docker Compose

Do not introduce another frontend framework.

---

# Architecture

Use:

```text
Controller
    ↓
Service
    ↓
DbContext
    ↓
SQLite
```

Controllers handle HTTP.

Services contain business logic.

DbContext handles persistence.

Views handle presentation.

---

# Controllers

Controllers should:

* Receive requests
* Validate ModelState
* Call Services
* Return Views
* Redirect after successful operations
* Handle HTTP concerns

Controllers should NOT contain business logic.

Avoid:

```csharp
public async Task<IActionResult> Create(...)
{
    // database queries
    // complex validation
    // customer creation
    // activity creation
    // business rules
}
```

Prefer:

```csharp
public async Task<IActionResult> Create(CreateCustomerViewModel model)
{
    if (!ModelState.IsValid)
        return View(model);

    await _customerService.CreateAsync(model);

    return RedirectToAction(nameof(Index));
}
```

---

# Services

Business logic belongs in Services.

Recommended:

```text
Services/
├── AuthService.cs
├── CustomerService.cs
├── CompanyService.cs
├── ContactService.cs
├── ActivityService.cs
├── TaskService.cs
└── AdminService.cs
```

Services should be responsible for business operations.

---

# CustomerService

Responsible for:

* Creating customers
* Updating customers
* Finding customers
* Searching customers
* Filtering customers
* Changing customer status
* Customer-related operations

---

# CompanyService

Responsible for:

* Creating companies
* Updating companies
* Finding companies
* Company search
* Related contacts

---

# ContactService

Responsible for:

* Creating contacts
* Updating contacts
* Finding contacts
* Linking contacts to companies

---

# ActivityService

Responsible for:

* Creating activities
* Listing activities
* Filtering activities
* Linking activities to customers
* Activity status

---

# TaskService

Responsible for:

* Creating tasks
* Updating tasks
* Completing tasks
* Assigning tasks
* Filtering tasks

---

# AdminService

Responsible for:

* User management
* Role management
* User activation/deactivation
* Administrative queries

---

# Dependency Injection

Use built-in ASP.NET Core Dependency Injection.

Example:

```csharp
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IAdminService, AdminService>();
```

Do not introduce a third-party DI framework.

---

# Interfaces

Use interfaces when they provide a practical benefit.

Example:

```text
ICustomerService
CustomerService
```

Do not create unnecessary abstractions.

Avoid:

```text
ICustomerRepository
CustomerRepository
ICustomerManager
CustomerManager
ICustomerProcessor
CustomerProcessor
```

unless there is a real requirement.

---

# ViewModels

Use ViewModels for forms and presentation-specific data.

Example:

```text
ViewModels/
├── LoginViewModel.cs
├── RegisterViewModel.cs
├── CustomerViewModel.cs
├── CreateCustomerViewModel.cs
├── CompanyViewModel.cs
├── ContactViewModel.cs
└── CreateTaskViewModel.cs
```

Do not expose database entities directly when a ViewModel is more appropriate.

---

# Database

Use:

```text
SQLite
```

Main entities:

```text
User
Customer
Company
Contact
Activity
Task
```

Keep relationships simple.

Do not create unnecessary entities.

---

# Entity Relationships

Basic structure:

```text
User
 │
 ├── Activities
 └── Tasks

Company
 │
 └── Contacts

Customer
 │
 ├── Activities
 └── Tasks
```

Do not create complex CRM relationship systems in v1.

---

# Authentication

Use ASP.NET Core Cookie Authentication.

Protected pages:

```csharp
[Authorize]
```

Admin:

```csharp
[Authorize(Roles = "Admin")]
```

Authorization must always be performed server-side.

Never rely only on hiding buttons in the UI.

---

# Passwords

Never store plain-text passwords.

Never log passwords.

Never expose passwords in:

* Views
* Models returned to clients
* Exceptions
* Logs
* Database queries

---

# Multilanguage

The application must use ASP.NET Core localization.

Initial cultures:

```text
en
es
it
```

Configure localization centrally.

Example:

```text
Resources/
├── SharedResource.en.resx
├── SharedResource.es.resx
└── SharedResource.it.resx
```

---

# Localization Rules

All user-facing text should be translatable.

Avoid:

```html
<h1>Customers</h1>
```

Prefer localized resources.

Controllers must not contain hardcoded user-facing text when it can be localized.

Avoid:

```csharp
return "Cliente creado correctamente";
```

Prefer resource-based messages.

---

# Language Persistence

The selected language should persist through the localization cookie.

The user should be able to switch between:

```text
English
Español
Italiano
```

without losing the current context whenever possible.

---

# Localization Naming

Use clear resource keys.

Example:

```text
Navigation.Dashboard
Navigation.Customers
Navigation.Companies
Navigation.Contacts
Navigation.Tasks

Customer.Create
Customer.Edit
Customer.Delete

Common.Save
Common.Cancel
Common.Search
Common.Delete
```

Do not create random or duplicated resource keys.

---

# UI

The CRM uses a light corporate theme.

Primary:

```text
#4F46E5
```

Dark:

```text
#3730A3
```

Background:

```text
#FFFFFF
```

Surface:

```text
#F8FAFC
```

Border:

```text
#E5E7EB
```

Text:

```text
#111827
```

Muted:

```text
#64748B
```

Success:

```text
#16A34A
```

Warning:

```text
#F59E0B
```

Danger:

```text
#DC2626
```

---

# UI Rules

The application must look like a real corporate CRM.

Avoid:

* Excessive gradients
* Glassmorphism
* Huge cards
* Excessive animations
* Decorative elements
* AI-generated-looking dashboards
* Excessive colors
* Excessive rounded components

Prefer:

* Tables
* Forms
* Simple cards
* Clear navigation
* Consistent spacing
* Strong typography
* Subtle borders
* Simple icons

---

# Tailwind

Use Tailwind CSS through CDN.

```html
<script src="https://cdn.tailwindcss.com"></script>
```

Do not introduce a frontend build system.

Do not add:

* React
* Vue
* Angular
* Next.js

---

# Dashboard

Keep dashboard simple.

Show:

* Total customers
* Open tasks
* Recent activities
* Recent customers
* Upcoming tasks

Do not add complex analytics unless explicitly requested.

---

# Customer Rules

Customer status:

```text
Lead
Prospect
Customer
Inactive
```

Customer creation should validate:

* Required name
* Valid email when provided
* Valid phone when provided
* Valid company relationship

---

# Task Rules

Task status:

```text
Pending
In Progress
Completed
```

Tasks can optionally belong to:

* Customer
* User

Do not build a complete project-management system.

---

# Activity Rules

Activity types:

```text
Call
Meeting
Email
Note
Other
```

Keep activity management simple.

---

# Search

Search should initially support simple text matching.

Example:

```text
Customer name
Email
Company
Phone
```

Do not introduce Elasticsearch or another search engine for v1.

---

# Security

Always validate:

* Authentication
* Authorization
* Form input
* ModelState
* User ownership
* Admin permissions

Use:

* Antiforgery protection
* Password hashing
* Cookie authentication
* Server-side authorization

---

# Error Handling

Never expose sensitive internal errors to users.

Do not expose:

* Stack traces
* Database connection strings
* File paths
* Secrets

Use a simple error page in production.

---

# Logging

Use ASP.NET Core built-in logging.

Do not log:

* Passwords
* Cookies
* Secrets
* Sensitive personal information unnecessarily

---

# Async

Use asynchronous Entity Framework operations:

```csharp
ToListAsync()
FirstOrDefaultAsync()
FindAsync()
SaveChangesAsync()
```

Avoid unnecessary synchronous database access.

---

# Docker

Application must run using:

```bash
docker compose up -d
```

Stop:

```bash
docker compose down
```

Application:

```text
http://localhost:8080
```

SQLite must persist using a Docker volume.

---

# Seed Data

Development environment should create:

```text
Admin
admin@example.com
Admin123!
```

and:

```text
User
user@example.com
User123!
```

Also create example:

* Companies
* Customers
* Contacts
* Activities
* Tasks

These are development credentials only.

---

# What NOT to Add

Unless explicitly requested, do NOT add:

* Clean Architecture
* DDD
* CQRS
* MediatR
* Event Sourcing
* Microservices
* Repository Pattern everywhere
* Unit of Work abstraction
* Redis
* Kafka
* RabbitMQ
* PostgreSQL
* Kubernetes
* Elasticsearch
* GraphQL
* React
* Vue
* Angular
* AutoMapper
* FluentValidation
* Complex permission frameworks

Keep the project simple.

---

# Development Workflow

When implementing a feature:

1. Inspect the existing structure.
2. Reuse existing Services.
3. Create or update a ViewModel if necessary.
4. Implement business logic in a Service.
5. Keep Controller logic minimal.
6. Update the View.
7. Add/update localization resources.
8. Test the feature.
9. Verify the application builds.

---

# Feature Flow

Example customer creation:

```text
CreateCustomer.cshtml
        ↓
CustomersController
        ↓
CustomerService
        ↓
AppDbContext
        ↓
SQLite
```

Example customer activity:

```text
Activity View
      ↓
ActivitiesController
      ↓
ActivityService
      ↓
AppDbContext
      ↓
SQLite
```

---

# Code Style

Use standard modern C# conventions.

Classes and methods:

```text
PascalCase
```

Local variables and parameters:

```text
camelCase
```

Use nullable reference types where appropriate.

Prefer clear code over clever code.

---

# Git

Use clear commit messages.

Examples:

```text
feat: add customer management
feat: add multilingual support
feat: add company contacts
feat: add activity management
feat: add task management
fix: prevent unauthorized customer access
style: improve CRM dashboard
refactor: move customer logic to service
```

---

# Versioning

The project currently starts at:

```text
v1
```

When adding a significant feature, update the version.

Example:

```text
v1
v1.1
v1.2
v2
```

Keep version information consistent with the README when relevant.

---

# Final Rule

Before introducing complexity, ask:

> Can this feature be implemented cleanly using MVC + Services + EF Core?

If yes, use that approach.

The preferred architecture is:

```text
MVC
 ↓
Services
 ↓
EF Core
 ↓
SQLite
```

The project should remain understandable to a developer opening the repository for the first time.

**Simple, professional and maintainable.**
