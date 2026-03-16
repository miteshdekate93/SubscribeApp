# SubscribeApp

![Build Status](https://github.com/miteshdekate93/SubscribeApp/actions/workflows/ci-cd.yml/badge.svg)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?logo=docker)
![React](https://img.shields.io/badge/React-18-61DAFB?logo=react)
![TypeScript](https://img.shields.io/badge/TypeScript-5.x-3178C6?logo=typescript)

A full-stack subscriber management application. Users can add, view, and delete email subscribers. Built with a .NET 8 Web API backend, React 18 + TypeScript frontend, SQL Server database, and fully containerized with Docker.

---

## Architecture

```
                    +-------------------+
                    |   Browser / User  |
                    +--------+----------+
                             |
                    +--------v----------+
                    |  React Frontend   |
                    |  (Vite + Tailwind)|
                    |   localhost:3000  |
                    +--------+----------+
                             |  HTTP (REST)
                    +--------v----------+
                    |  .NET 8 Web API   |
                    |  (Minimal API +   |
                    |   EF Core + Swagger)
                    |   localhost:5000  |
                    +--------+----------+
                             |  EF Core
                    +--------v----------+
                    |    SQL Server     |
                    |  2022 (Docker)    |
                    |   port 1433       |
                    +-------------------+
```

---

## Tech Stack

| Layer      | Technology                                      |
|------------|-------------------------------------------------|
| Frontend   | React 18, TypeScript, Vite, Tailwind CSS, Axios |
| Backend    | .NET 8, ASP.NET Core Minimal API, EF Core        |
| Database   | Microsoft SQL Server 2022                        |
| ORM        | Entity Framework Core 8 (Code-First)             |
| Docs       | Swagger / OpenAPI (Swashbuckle)                  |
| Container  | Docker, Docker Compose                           |
| CI/CD      | GitHub Actions                                   |

### Legacy Stack (original)
- ASP.NET Framework 4.x Web API
- jQuery, Bootstrap
- Entity Framework 6 (Code-First)
- SQL Server LocalDB

---

## Getting Started

### Option 1 — Docker Compose (recommended)

**Prerequisites:** Docker Desktop installed and running.

```bash
git clone https://github.com/miteshdekate93/SubscribeApp.git
cd SubscribeApp

# Start all services (API, frontend, database)
docker compose up --build
```

- Frontend: http://localhost:3000
- API + Swagger: http://localhost:5000/swagger

### Option 2 — Manual Setup

**Prerequisites:** .NET 8 SDK, Node.js 20+, SQL Server instance.

#### Backend

```bash
cd backend

# Set your connection string
export ConnectionStrings__DefaultConnection="Server=localhost;Database=SubscribeApp;User Id=sa;Password=YourPassword;TrustServerCertificate=True"

dotnet restore
dotnet ef database update
dotnet run
# API available at http://localhost:5000
```

#### Frontend

```bash
cd frontend
npm install
npm run dev
# App available at http://localhost:3000
```

---

## API Endpoints

| Method | Endpoint               | Description               | Request Body                      |
|--------|------------------------|---------------------------|-----------------------------------|
| GET    | `/api/subscribers`     | List all subscribers      | —                                 |
| POST   | `/api/subscribers`     | Add a new subscriber      | `{ "name": "...", "email": "..." }` |
| DELETE | `/api/subscribers/{id}`| Delete a subscriber by ID | —                                 |

Full interactive documentation available at `/swagger` when the API is running.

---

## Project Structure

```
SubscribeApp/
├── SubscribeApp/          # Original ASP.NET Framework app (legacy)
├── backend/               # .NET 8 Web API (modernized)
│   ├── Data/
│   ├── Models/
│   ├── Program.cs
│   ├── SubscribeApp.csproj
│   └── Dockerfile
├── frontend/              # React 18 + TypeScript (modernized)
│   ├── src/
│   │   ├── App.tsx
│   │   └── types/
│   ├── index.html
│   ├── package.json
│   ├── vite.config.ts
│   ├── tsconfig.json
│   └── Dockerfile
├── docker-compose.yml
├── .github/
│   └── workflows/
│       └── ci-cd.yml
└── README.md
```

---

## Contributing

1. Fork the repository
2. Create a feature branch: `git checkout -b feat/your-feature`
3. Commit your changes: `git commit -m "feat: describe your change"`
4. Push to your branch: `git push origin feat/your-feature`
5. Open a Pull Request — CI will run automatically

Please follow conventional commits (`feat:`, `fix:`, `docs:`, `chore:`) and ensure all CI checks pass before requesting review.

---

## License

This project is open source. See [LICENSE](LICENSE) for details.
