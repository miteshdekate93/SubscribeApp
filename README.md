# SubscribeApp

![CI](https://github.com/miteshdekate93/SubscribeApp/actions/workflows/ci-cd.yml/badge.svg)
![.NET 8](https://img.shields.io/badge/.NET-8-purple)
![React](https://img.shields.io/badge/React-18-blue)
![Docker](https://img.shields.io/badge/Docker-ready-2496ED)

A subscriber management app — users can sign up with their email and name, view all subscribers, and remove them. Originally built with ASP.NET Framework + jQuery, modernised with a .NET 8 API and React frontend.

## Tech Stack

| Part | Technology |
|------|-----------|
| Backend | .NET 8 Web API (minimal API) |
| Database | SQL Server + Entity Framework Core |
| Frontend | React 18 + TypeScript + Tailwind CSS |
| Container | Docker + Docker Compose |
| CI | GitHub Actions |

## Run It

```bash
git clone https://github.com/miteshdekate93/SubscribeApp.git
cd SubscribeApp
docker-compose up --build
```
- App: http://localhost:3000
- API + Swagger: http://localhost:5000/swagger

## API

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/subscribers` | List all subscribers |
| POST | `/api/subscribers` | Add a subscriber |
| DELETE | `/api/subscribers/{id}` | Remove a subscriber |

## Project Structure

```
SubscribeApp/
├── SubscribeApp/   Original ASP.NET Framework app (legacy reference)
├── backend/        .NET 8 Web API (modernised)
├── frontend/       React 18 + TypeScript
└── docker-compose.yml
```
