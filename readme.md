# CRM MVP

Simple y moderno **CRM (Customer Relationship Management)** desarrollado con ASP.NET Core MVC.

El proyecto está pensado como un MVP funcional para gestionar clientes, contactos, actividades y usuarios desde una interfaz corporativa, limpia y responsive.

La aplicación soporta múltiples idiomas desde el inicio.

---

## Stack

* ASP.NET Core MVC
* C#
* Entity Framework Core
* SQLite
* Razor Views
* Tailwind CSS CDN
* Docker
* Docker Compose

---

## Features

### Authentication

* Registro de usuarios
* Login
* Logout
* Roles
* Permisos básicos

### CRM

* Dashboard
* Clientes
* Contactos
* Empresas
* Actividades
* Notas
* Tareas
* Búsqueda
* Perfil de usuario

### Admin

* Gestión de usuarios
* Roles
* Activar/desactivar usuarios
* Gestión básica del sistema

### Multilenguaje

Idiomas iniciales:

* English
* Español
* Italiano

El idioma seleccionado debe mantenerse durante la navegación.

El sistema debe estar preparado para agregar nuevos idiomas fácilmente.

---

# UI / UX

La interfaz debe parecer un **CRM corporativo real**, no una plantilla generada automáticamente.

### Estilo

* Tema claro
* Fondo blanco
* Azul índigo como color principal
* Gris neutro para elementos secundarios
* Tipografía moderna
* Bordes discretos
* Sombras suaves
* Espaciado consistente
* Responsive

### Colores

```text
Primary:    #4F46E5
PrimaryDark: #3730A3
Background: #FFFFFF
Surface:    #F8FAFC
Border:     #E5E7EB
Text:       #111827
Muted:      #64748B
Success:    #16A34A
Warning:    #F59E0B
Danger:     #DC2626
```

El azul debe utilizarse principalmente para:

* Botones principales
* Links
* Navegación activa
* Elementos seleccionados
* Acciones importantes

No utilizar el azul en todos los elementos de la interfaz.

---

# Layout

Desktop:

```text
+-------------------------------------------------------+
| Logo                    Search       Language Profile |
+-------------------------------------------------------+
|                                                       |
| Sidebar              Main Content                     |
|                                                       |
| Dashboard            Customers                        |
| Customers            --------------------------------|
| Companies             Customer information             |
| Contacts                                               |
| Activities                                             |
| Tasks                                                  |
|                                                       |
+-------------------------------------------------------+
```

En móviles, el sidebar debe convertirse en navegación compacta.

---

# Pages

## Public

```text
/
 /login
 /register
```

## Authenticated

```text
/dashboard

/customers
/customers/create
/customers/{id}
/customers/{id}/edit

/companies
/contacts

/activities
/tasks

/profile
```

## Admin

```text
/admin
/admin/users
/admin/users/{id}
/admin/roles
```

---

# Dashboard

El dashboard debe mostrar información útil y sencilla.

Ejemplo:

```text
Good morning

Customers          Open tasks        Activities

 248                  12                36


Recent customers

Acme S.r.l.
John Smith
Tech Solutions


Upcoming tasks

Call Acme
Follow up with John
Send proposal
```

No utilizar gráficos complejos en el MVP.

Si se utiliza algún gráfico, debe ser sencillo y aportar información real.

---

# Customers

Los clientes son el elemento principal del CRM.

Información:

```text
Customer
- Id
- FirstName
- LastName
- Email
- Phone
- CompanyId
- Status
- Notes
- CreatedAt
- UpdatedAt
```

Estados:

```text
Lead
Prospect
Customer
Inactive
```

Funciones:

* Crear cliente
* Editar cliente
* Ver cliente
* Buscar cliente
* Filtrar por estado
* Actividades relacionadas
* Notas

---

# Companies

Permitir asociar contactos y clientes a empresas.

```text
Company
- Id
- Name
- Email
- Phone
- Website
- Address
- Notes
- CreatedAt
- UpdatedAt
```

Funciones:

* Crear empresa
* Editar empresa
* Ver empresa
* Buscar empresa
* Ver contactos relacionados

---

# Contacts

Un contacto puede pertenecer a una empresa.

```text
Contact
- Id
- CompanyId
- FirstName
- LastName
- Email
- Phone
- Position
- CreatedAt
- UpdatedAt
```

---

# Activities

Permitir registrar actividades relacionadas con clientes.

Tipos:

```text
Call
Meeting
Email
Note
Other
```

Información:

```text
Activity
- Id
- CustomerId
- UserId
- Type
- Title
- Description
- DueDate
- CreatedAt
```

---

# Tasks

Sistema sencillo de tareas.

```text
Task
- Id
- CustomerId
- UserId
- Title
- Description
- DueDate
- Status
- CreatedAt
```

Estados:

```text
Pending
In Progress
Completed
```

No crear un sistema avanzado de project management.

---

# Profile

El usuario puede:

* Ver perfil
* Editar nombre
* Editar apellido
* Cambiar email
* Cambiar contraseña
* Seleccionar idioma

---

# Multilanguage

El sistema debe utilizar el sistema de localización de ASP.NET Core.

Idiomas:

```text
en
es
it
```

Las traducciones deben estar separadas del código.

Ejemplo conceptual:

```text
Resources/
├── SharedResource.en.resx
├── SharedResource.es.resx
└── SharedResource.it.resx
```

Nunca escribir textos traducibles directamente dentro de Controllers o lógica de negocio.

Evitar:

```csharp
return View("Cliente");
```

Preferir recursos localizados.

---

# Language Selector

La interfaz debe tener un selector de idioma.

Ejemplo:

```text
English
Español
Italiano
```

El usuario debe poder cambiar el idioma sin perder la página actual cuando sea posible.

El idioma seleccionado debe persistir mediante cookie de localización.

---

# Architecture

Mantener una arquitectura MVC + Services sencilla.

```text
Request
   ↓
Controller
   ↓
Service
   ↓
Entity Framework Core
   ↓
SQLite
```

Estructura:

```text
CrmApp/
│
├── Controllers/
│
├── Models/
│
├── ViewModels/
│
├── Services/
│
├── Data/
│
├── Resources/
│
├── Views/
│
├── wwwroot/
│
├── Migrations/
│
├── Program.cs
├── appsettings.json
├── Dockerfile
├── docker-compose.yml
├── AGENTS.md
└── CrmApp.csproj
```

No utilizar Clean Architecture, CQRS, DDD ni microservicios.

---

# Services

La lógica de negocio debe vivir en Services.

Ejemplo:

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

Los Controllers deben mantenerse pequeños.

---

# Database

Utilizar SQLite.

Archivo:

```text
crm.db
```

Entity Framework Core administra la persistencia.

Principales entidades:

```text
User
Customer
Company
Contact
Activity
Task
```

No crear entidades innecesarias.

---

# Docker

Ejecutar:

```bash
docker compose up -d
```

Detener:

```bash
docker compose down
```

Aplicación:

```text
http://localhost:8080
```

La base de datos debe persistir mediante Docker volume.

---

# Security

Implementar seguridad básica:

* Password hashing
* Cookie Authentication
* Authorization
* Role checks
* CSRF protection
* Model validation
* Server-side authorization
* No passwords in logs
* No sensitive information in views

---

# Roles

Roles iniciales:

```text
Admin
User
```

El administrador puede:

* Gestionar usuarios
* Activar/desactivar usuarios
* Gestionar roles

Los usuarios normales pueden trabajar con el CRM según sus permisos.

---

# Seed

Crear datos de desarrollo automáticamente.

### Admin

```text
Email:
admin@example.com

Password:
Admin123!
```

### User

```text
Email:
user@example.com

Password:
User123!
```

Crear algunos clientes y empresas de ejemplo para que el dashboard no aparezca vacío.

Estas credenciales son únicamente para desarrollo.

---

# Tailwind

Utilizar Tailwind mediante CDN.

```html
<script src="https://cdn.tailwindcss.com"></script>
```

No crear un pipeline frontend complejo.

No utilizar React, Vue o Angular.

---

# UI Philosophy

El CRM debe parecer desarrollado por un equipo de software real.

Evitar:

* Gradientes excesivos
* Glassmorphism
* Animaciones innecesarias
* Dashboards saturados
* Muchas cards
* Ilustraciones genéricas
* Colores excesivos
* Iconos decorativos sin función

Priorizar:

* Tablas claras
* Formularios sencillos
* Navegación intuitiva
* Buen espaciado
* Tipografía profesional
* Estados visuales claros
* Responsive design

---

# MVP Scope

### Included

* Authentication
* Users
* Roles
* Customers
* Companies
* Contacts
* Activities
* Tasks
* Notes
* Search
* Profile
* Multilanguage
* Admin panel
* SQLite
* Docker
* Tailwind CDN

### Not Included

* Email provider
* WhatsApp API
* SMS
* Payment processing
* Marketing automation
* Advanced reporting
* AI assistant
* External CRM integrations
* Elasticsearch
* Redis
* PostgreSQL
* Microservices
* Kubernetes

---

# Goal

Crear un CRM pequeño, funcional y profesional que pueda utilizarse como:

* MVP
* Portfolio project
* Demo técnica
* Base para un SaaS
* Ejemplo de ASP.NET Core MVC

La prioridad es:

**Simple > Complejo**

**Funcional > Sobrearquitecturado**

**Profesional > Visualmente espectacular**

**Fácil de mantener > Muchas abstracciones**
