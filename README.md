# todo
 A Full stack Todo application demonstrating modern software engineering practices using ASP.NET Core 10 and Angular 21.

## Backend
- ASP.NET Core 10
- Swagger
- JSON File Storage

## Frontend
- Angular 21
- TypeScript
- RxJS
- Angular Signals
- Tailwind CSS
- Lucide Angular icons

## Design Principles

This project demonstrates:

- Layered architecture
- Separation of concerns
- Dependency Injection
- Repository Pattern
- Service Layer
- RESTful API design
- Swagger documentation
- Asynchronous programming using `async` / `await`
- Interface driven development for unit testing (Not done in the current version)

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/sleagle/todo.git
cd todo
```

### 2. Start the API

```bash
cd TodoApi
dotnet restore
dotnet run
```

Swagger:

```
http://localhost:5275/swagger
```

---

### 3. Start the Angular application

```bash
cd TodoApp
npm install
ng serve
```

Browse to:

```
http://localhost:4200
```