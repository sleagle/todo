# Todo API

A simple ASP.NET core 10 Rest Api.

## Technologies
- ASP.NET Core 10
- Swagger
- JSON File Storage

## Project Structure

```
TodoApi
│
├── Controllers
│   └── TodoController.cs
│
├── Data
│   ├── DataFiles
│   │   └── todo.json
│   ├── Models
│   │   └── Todo.cs
│   └── Repositories
│       ├── ITodoRepository.cs
│       └── JsonTodoRepository.cs  
│
├── Services
│   ├── ITodoService.cs
│   └── TodoService.cs
│
├── TodoApi.http
├── Program.cs
└── README.md
```

## Architecture

```
HTTP Request
      │
      ▼
Controller
      │
      ▼
Service
      │
      ▼
Repository
      │
      ▼
JSON File
```

The controller is responsible for handling HTTP requests.
The service contains business logic.
The repository abstracts data access.
This separation makes the application easier to maintain and unit test.

Swagger

```
http://localhost:5275/swagger
```

---

## API Endpoints

| Method | Endpoint            | Description |
|---------|---------------------|-------------|
| GET | /api/Todo/all       | Get all todos |
| GET | /api/Todo/task/{id} | Get todo by ID |
| DELETE | /api/Todo/task/{id} | Delete a todo |
| POST | /api/Todo/task/add  | Create a todo |

---