# CacheDotNetAPI

## 📌 Project Overview

CacheDotNetAPI is a simple ASP.NET Core Web API demonstrating the usage of **Memory Cache** and **Distributed Cache (Redis)**.

It shows how caching can optimize database access by storing frequently requested data either in-memory or in a distributed cache system like Redis. This project is built for learning and showcasing performance comparison between the two caching strategies.

---

## ⚡ Technology Stack

- **.NET 9**
- **ASP.NET Core Web API**
- **Entity Framework Core** (Database ORM)
- **SQL Server 2022** (via Docker)
- **Redis** (via Docker)
- **MemoryCache** (In-Memory Caching)
- **DistributedCache** (Redis-based Caching)
- **Swagger UI** (API documentation)

---

## 🏛 Project Architecture

- **Controllers**: Handle incoming API requests.
- **Services**: Business logic for fetching, caching, and clearing data.
- **Entities**: Database models (e.g., Product).
- **DataAccess**: EF Core DbContext for database access.

Two services:
- `ProductMemoryCacheService` for **Memory Cache**
- `ProductDistributedCacheService` for **Redis Cache**

---

## 🚀 Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://docs.docker.com/get-docker/)

### Clone the Repository
```bash
git clone https://github.com/tanapoomjaisabay/CacheDotNetAPI.git
cd CacheDotNetAPI
```

### Setup Docker Containers
```bash
docker-compose up -d
```
This command will start both Redis and SQL Server.

### Apply Database Migrations (Optional)
If needed, apply EF Core migrations manually.

### Run the Application
```bash
dotnet run --project CacheDotNetAPI
```

Navigate to: `https://localhost:<port>/swagger`

---

## 🐳 Docker Compose Setup

Create a `docker-compose.yml` file:

```yaml
version: '3.8'
services:
  redis:
    image: redis:latest
    container_name: cache_redis
    ports:
      - "6379:6379"
    restart: always

  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: cache_mssql
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrong!Passw0rd
    ports:
      - "1433:1433"
    restart: always
```

Update your `appsettings.json` to match:
```json
"ConnectionStrings": {
  "DemoContext": "Server=localhost,1433;Database=DemoDb;User Id=sa;Password=YourStrong!Passw0rd;",
  "RedisConnection": "localhost:6379"
}
```

---

## 🔥 API Endpoints

| Endpoint | Method | Description |
|:---|:---|:---|
| `/api/memory/products/{id}` | GET | Fetch product using Memory Cache |
| `/api/memory/products/clear/{id}` | DELETE | Clear specific product from Memory Cache |
| `/api/distributed/products/{id}` | GET | Fetch product using Distributed Redis Cache |
| `/api/distributed/products/clear/{id}` | DELETE | Clear specific product from Distributed Redis Cache |

---

## 🛠️ Health Check

| Endpoint | Description |
|:---|:---|
| `/healthz` | Check application health |

---

## 📄 Sample Requests

**Get Product (Memory Cache)**
```bash
curl -X GET "https://localhost:<port>/api/memory/products/1"
```

**Clear Product Cache (Distributed Cache)**
```bash
curl -X DELETE "https://localhost:<port>/api/distributed/products/clear/1"
```

---

## 🙌🏻 Credit

Created by [Tanapoom Jaisabay](https://github.com/tanapoomjaisabay). 

This project is built for educational purposes to demonstrate caching strategies in modern .NET applications.

Happy Coding! 🌟
