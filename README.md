# Lead Management System

Sistema Full Stack para gerenciamento de leads desenvolvido com .NET 6, React e SQL Server.

## 🚀 Tecnologias

**Backend:**
- .NET 6 Web API
- SQL Server + Entity Framework Core
- CQRS com MediatR
- Domain-Driven Design (DDD)
- Event Sourcing

**Frontend:**
- React 19
- TailwindCSS
- Lucide Icons

## ✨ Funcionalidades

- Criar, aceitar e recusar leads
- Desconto automático de 10% para leads > $500
- Notificação por email simulado (vendas@test.com)
- Sistema de status (New, Accepted, Declined)
- Event Store para auditoria completa

## 📋 Pré-requisitos

- .NET 6 SDK ([Download](https://dotnet.microsoft.com/download/dotnet/6.0))
- Node.js 18+ ([Download](https://nodejs.org/))
- SQL Server ([Download](https://www.microsoft.com/sql-server/sql-server-downloads))

## ⚙️ Instalação e Execução

### 1. Configurar Banco de Dados

Atualize `LeadsManager.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=LeadsManagerDB;Integrated Security=True;TrustServerCertificate=True;Encrypt=False"
  }
}
```

### 2. Executar Backend

```powershell
# Instalar EF Core Tools (primeira vez)
dotnet tool install --global dotnet-ef --version 6.0.25

# Restaurar pacotes
dotnet restore LeadsManager.sln

# Aplicar migrations
dotnet ef database update --project LeadsManager.Infrastructure --startup-project LeadsManager.API

# Executar API
cd LeadsManager.API
dotnet run
```

API disponível em: `http://localhost:5000` | Swagger: `http://localhost:5000/swagger`

### 3. Executar Frontend

```powershell
cd leads-front-web
npm install
npm start
```

Aplicação disponível em: `http://localhost:3000`

## 🐳 Docker (Opcional)

```powershell
docker-compose up --build
```

## � API Endpoints

- `GET /api/leads` - Listar todos os leads
- `GET /api/leads/status/{status}` - Filtrar por status
- `POST /api/leads` - Criar lead
- `PUT /api/leads/{id}/accept` - Aceitar lead
- `PUT /api/leads/{id}/decline` - Recusar lead

## 🎯 Regras de Negócio

- **Criação**: Leads iniciam com status "New"
- **Aceitação**: Aplica 10% desconto se preço > $500
- **Email**: Notificação salva em `LeadsManager.API/EmailLogs/`

##  Autor

**Vitor Hugo Marcelino Santos**

Desafio Técnico Full Stack - .NET 6 + React + SQL Server

---

**DDD | CQRS | Event Sourcing | Clean Architecture**
