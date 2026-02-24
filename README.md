# 🎯 Lead Management System

> Sistema profissional Full Stack para gerenciamento de leads com arquitetura moderna e padrões avançados de desenvolvimento.

Sistema desenvolvido com **.NET 6**, **React 19** e **SQL Server**, implementando **Domain-Driven Design (DDD)**, **CQRS** com **MediatR** e **Event Sourcing** para criar uma solução robusta e escalável.

---

## 📑 Índice

- [Sobre o Projeto](#-sobre-o-projeto)
- [Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [Arquitetura](#-arquitetura)
- [Funcionalidades](#-funcionalidades)
- [Pré-requisitos](#-pré-requisitos)
- [Instalação e Configuração](#-instalação-e-configuração)
- [Executando o Projeto](#-executando-o-projeto)
- [API Endpoints](#-api-endpoints)
- [Regras de Negócio](#-regras-de-negócio)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Solução de Problemas](#-solução-de-problemas)
- [Autor](#-autor)

---

## 📖 Sobre o Projeto

O **Lead Management System** é uma aplicação completa para gerenciar leads de serviços. O sistema permite:

- **Visualizar** leads em tempo real organizados por status
- **Criar** novos leads com informações detalhadas de contato
- **Aceitar** leads com aplicação automática de descontos
- **Recusar** leads que não atendem aos critérios
- **Rastrear** todas as mudanças através de Event Sourcing
- **Notificar** vendas por email quando um lead é aceito

Este projeto foi desenvolvido como um **desafio técnico Full Stack** para demonstrar conhecimentos em arquitetura de software moderna, padrões de design e boas práticas de desenvolvimento.

---

## 🚀 Tecnologias Utilizadas

### **Backend**

| Tecnologia | Versão | Descrição |
|------------|--------|-----------|
| **.NET** | 6.0 | Framework principal do backend |
| **C#** | 10.0 | Linguagem de programação |
| **Entity Framework Core** | 6.0 | ORM para acesso ao banco de dados |
| **SQL Server** | 2019+ | Banco de dados relacional |
| **MediatR** | 11.0 | Implementação do padrão CQRS |
| **AutoMapper** | 12.0 | Mapeamento objeto-objeto |

### **Frontend**

| Tecnologia | Versão | Descrição |
|------------|--------|-----------|
| **React** | 19.0 | Biblioteca para interfaces de usuário |
| **JavaScript** | ES6+ | Linguagem de programação |
| **TailwindCSS** | 3.x | Framework CSS utilitário |
| **Axios** | 1.12 | Cliente HTTP |
| **Lucide React** | Latest | Biblioteca de ícones |

### **Padrões e Práticas**

- ✅ **Domain-Driven Design (DDD)** - Foco no domínio do negócio
- ✅ **CQRS** - Separação de Commands e Queries
- ✅ **Event Sourcing** - Armazenamento de eventos de domínio
- ✅ **Clean Architecture** - Separação clara de responsabilidades
- ✅ **Repository Pattern** - Abstração de acesso a dados
- ✅ **Dependency Injection** - Inversão de controle

---

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture**, organizando o código em camadas bem definidas:

```
┌─────────────────────────────────────────────────────────┐
│                    Presentation Layer                    │
│               (API Controllers + Frontend)               │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│                  Application Layer                       │
│        (CQRS: Commands, Queries, Handlers)              │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│                    Domain Layer                          │
│     (Entities, Value Objects, Domain Events)            │
└────────────────────┬────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────┐
│               Infrastructure Layer                       │
│   (EF Core, Repositories, Email Service, Event Store)   │
└─────────────────────────────────────────────────────────┘
```

### **Estrutura de Pastas**

```
LeadManagementSystem/
├── LeadsManager.Domain/          # Camada de Domínio
│   ├── Entities/                 # Entidades do domínio
│   ├── ValueObjects/             # Objetos de valor
│   ├── Events/                   # Eventos de domínio
│   └── Repositories/             # Interfaces de repositórios
│
├── LeadsManager.Application/     # Camada de Aplicação
│   ├── Commands/                 # Comandos CQRS
│   ├── Queries/                  # Consultas CQRS
│   ├── Handlers/                 # Handlers MediatR
│   └── DTOs/                     # Data Transfer Objects
│
├── LeadsManager.Infrastructure/  # Camada de Infraestrutura
│   ├── Persistence/              # Contexto EF Core
│   ├── Repositories/             # Implementação de repositórios
│   ├── EventSourcing/            # Event Store
│   └── Services/                 # Serviços (Email, etc.)
│
├── LeadsManager.API/             # API Web (.NET 6)
│   └── Controllers/              # Controllers REST
│
└── leads-front-web/              # Frontend React
    ├── src/
    │   ├── components/           # Componentes React
    │   ├── services/             # Serviços (API client)
    │   └── App.js                # Componente principal
    └── public/
```

---

## ✨ Funcionalidades

### **Gestão de Leads**

- 📝 **Criar Lead**: Adicionar novos leads com informações completas de contato
- ✅ **Aceitar Lead**: Aceitar leads e aplicar descontos automáticos
- ❌ **Recusar Lead**: Recusar leads que não atendem aos critérios
- 📊 **Visualizar Leads**: Ver leads organizados por status (Invited/Accepted)
- 🔍 **Filtrar**: Filtrar leads por status através da API

### **Sistema de Descontos**

- Desconto automático de **10%** aplicado quando:
  - Lead é aceito
  - Preço original é maior que **$500**

### **Notificações**

- 📧 Email simulado enviado para **vendas@test.com**
- Emails salvos como arquivos de texto em `EmailLogs/`
- Conteúdo detalhado com informações do lead e próximos passos

### **Event Sourcing**

- 📜 Todos os eventos de domínio são persistidos
- Auditoria completa de todas as mudanças
- Possibilidade de reconstruir estado a partir dos eventos
- Eventos armazenados: `LeadCreatedEvent`, `LeadAcceptedEvent`, `LeadDeclinedEvent`

---

## 📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

### **Obrigatórios**

- **.NET 6 SDK** - [Download aqui](https://dotnet.microsoft.com/download/dotnet/6.0)
  - Versão mínima: 6.0
  - Verificar instalação: `dotnet --version`

- **Node.js** - [Download aqui](https://nodejs.org/)
  - Versão mínima: 18.x
  - Verificar instalação: `node --version`

- **SQL Server** - [Download aqui](https://www.microsoft.com/sql-server/sql-server-downloads)
  - Versão: 2019+ ou SQL Server Express (gratuito)
  - Pode usar instância local ou remota

### **Opcionais**

- **Docker Desktop** - Para execução via containers
- **Visual Studio 2022** ou **VS Code** - IDE recomendada
- **SQL Server Management Studio (SSMS)** - Para gerenciar o banco de dados

---

## 🔧 Instalação e Configuração

### **1. Clonar o Repositório**

```powershell
git clone https://github.com/hugovh2/LeadManagementSystem.git
cd LeadManagementSystem
```

### **2. Configurar o Banco de Dados**

Edite o arquivo `LeadsManager.API/appsettings.json` e atualize a connection string:

**Autenticação Windows (Recomendado):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=LeadsManagerDB;Integrated Security=True;TrustServerCertificate=True;Encrypt=False"
  }
}
```

**Autenticação SQL Server:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=LeadsManagerDB;User Id=seu_usuario;Password=sua_senha;TrustServerCertificate=True;Encrypt=False"
  }
}
```

### **3. Instalar Ferramentas .NET**

```powershell
# Instalar Entity Framework Core Tools (apenas primeira vez)
dotnet tool install --global dotnet-ef --version 6.0.25

# Verificar instalação
dotnet ef --version
```

### **4. Restaurar Dependências do Backend**

```powershell
# Na pasta raiz do projeto
dotnet restore LeadsManager.sln
```

### **5. Criar o Banco de Dados**

```powershell
# Aplicar migrations e criar banco
dotnet ef database update --project LeadsManager.Infrastructure --startup-project LeadsManager.API
```

Isso criará:
- Banco de dados `LeadsManagerDB`
- Tabela `Leads` (armazena os leads)
- Tabela `Events` (armazena eventos de domínio)

### **6. Configurar o Frontend**

```powershell
cd leads-front-web

# Instalar dependências
npm install

# Criar arquivo .env (se não existir)
copy .env.example .env
```

O arquivo `.env` deve conter:
```env
REACT_APP_API_URL=http://localhost:5000/api
```

---

## 🚀 Executando o Projeto

### **Opção 1: Execução Local (Desenvolvimento)**

#### **Backend**

```powershell
# Na pasta raiz
cd LeadsManager.API
dotnet run
```

✅ **API estará disponível em:**
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger: `http://localhost:5000/swagger`

#### **Frontend**

```powershell
# Em outro terminal
cd leads-front-web
npm start
```

✅ **Aplicação React estará disponível em:**
- `http://localhost:3000`

### **Opção 2: Docker Compose**

```powershell
# Na pasta raiz
docker-compose up --build
```

Isso iniciará:
- 🗄️ SQL Server na porta `1433`
- 🔌 API na porta `5000`
- 🌐 Frontend na porta `3000`

Acesse: `http://localhost:3000`

---

## 📡 API Endpoints

### **Leads**

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/api/leads` | Listar todos os leads |
| `GET` | `/api/leads/status/{status}` | Filtrar leads por status (New, Accepted, Declined) |
| `POST` | `/api/leads` | Criar um novo lead |
| `PUT` | `/api/leads/{id}/accept` | Aceitar um lead |
| `PUT` | `/api/leads/{id}/decline` | Recusar um lead |

### **Exemplo de Requisição - Criar Lead**

```json
POST /api/leads
Content-Type: application/json

{
  "firstName": "João",
  "lastName": "Silva",
  "email": "joao.silva@example.com",
  "phoneNumber": "0412345678",
  "suburb": "São Paulo",
  "category": "Encanamento",
  "description": "Reparo urgente de encanamento",
  "price": 650.00
}
```

### **Exemplo de Resposta**

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "contact": {
    "fullName": "João Silva",
    "email": "joao.silva@example.com",
    "phoneNumber": "0412345678"
  },
  "location": {
    "suburb": "São Paulo"
  },
  "category": "Encanamento",
  "description": "Reparo urgente de encanamento",
  "price": 650.00,
  "status": 0,
  "createdAt": "2024-02-24T12:00:00Z"
}
```

### **Documentação Interativa**

Acesse `http://localhost:5000/swagger` para testar todos os endpoints de forma interativa.

---

## 🎯 Regras de Negócio

### **1. Criação de Lead**

- Todos os campos são obrigatórios
- Lead inicia com status `New` (0)
- Evento `LeadCreatedEvent` é gerado e armazenado

### **2. Aceitação de Lead**

- ✅ Apenas leads com status `New` podem ser aceitos
- 💰 Se preço > $500: desconto de 10% é aplicado automaticamente
- 📧 Email de notificação é enviado para `vendas@test.com`
- 📝 Status muda para `Accepted` (1)
- 📅 Campo `AcceptedAt` é preenchido com a data/hora atual
- 🎯 Evento `LeadAcceptedEvent` é gerado e armazenado

**Exemplo de Cálculo de Desconto:**
```
Preço Original: $650.00
Desconto (10%): -$65.00
Preço Final:    $585.00
```

### **3. Recusa de Lead**

- ❌ Apenas leads com status `New` podem ser recusados
- 📝 Status muda para `Declined` (2)
- 📅 Campo `DeclinedAt` é preenchido com a data/hora atual
- 🎯 Evento `LeadDeclinedEvent` é gerado e armazenado

### **4. Sistema de Email**

- 📧 Emails são **simulados** (não enviados de verdade)
- 💾 Salvos como arquivos `.txt` em `LeadsManager.API/EmailLogs/`
- 📄 Formato do arquivo: `Email_YYYYMMDD_HHMMSS_[GUID].txt`
- 📨 Destinatário: `vendas@test.com`
- ✉️ Conteúdo inclui: dados do lead, preço final, próximos passos

### **5. Status dos Leads**

| Status | Valor | Descrição |
|--------|-------|-----------|
| `New` | 0 | Lead recém-criado, aguardando ação |
| `Accepted` | 1 | Lead aceito pela equipe de vendas |
| `Declined` | 2 | Lead recusado |

---

## 📁 Estrutura do Projeto

### **Domain Layer** - Lógica de Negócio Pura

- **Entities**: `Lead` (raiz do agregado)
- **Value Objects**: `ContactInfo`, `Location`, `Money`
- **Events**: `LeadCreatedEvent`, `LeadAcceptedEvent`, `LeadDeclinedEvent`
- **Repositories**: `ILeadRepository` (interface)

### **Application Layer** - Casos de Uso

- **Commands**: `CreateLeadCommand`, `AcceptLeadCommand`, `DeclineLeadCommand`
- **Queries**: `GetAllLeadsQuery`, `GetLeadsByStatusQuery`
- **Handlers**: Implementações usando MediatR
- **DTOs**: `LeadDto` para transferência de dados

### **Infrastructure Layer** - Implementações Técnicas

- **Persistence**: `LeadsDbContext` (EF Core)
- **Repositories**: `LeadRepository`
- **Event Store**: `EventStore` (armazena eventos)
- **Services**: `EmailService` (email simulado)

### **API Layer** - Exposição HTTP

- **Controllers**: `LeadsController`
- **Startup**: Configuração de DI, CORS, Swagger

### **Frontend** - Interface do Usuário

- **Components**: `LeadCard`, `TabNav`, `Toast`, `AddLeadModal`
- **Services**: `api.js` (cliente HTTP)
- **Styling**: TailwindCSS

---

## 🛠️ Solução de Problemas

### **Erro: "The server was not found or was not accessible"**

**Causa**: SQL Server não está rodando ou connection string incorreta.

**Solução**:
1. Verifique se o SQL Server está rodando
2. Abra o "SQL Server Configuration Manager"
3. Inicie o serviço "SQL Server (MSSQLSERVER)"
4. Confirme a connection string em `appsettings.json`

---

### **Erro: "Invalid object name 'Leads'"**

**Causa**: Migrations não foram aplicadas ao banco de dados.

**Solução**:
```powershell
dotnet ef database update --project LeadsManager.Infrastructure --startup-project LeadsManager.API
```

---

### **Erro: CORS bloqueando requisições**

**Causa**: Política CORS bloqueando chamadas do frontend para a API.

**Solução**: O CORS já está configurado no projeto. Certifique-se de que:
- A API está rodando em `http://localhost:5000`
- O frontend está configurado com `REACT_APP_API_URL=http://localhost:5000/api`
- Reinicie ambos os serviços

---

### **Erro: "Port already in use"**

**Causa**: Outra instância da aplicação já está rodando na porta.

**Solução**:
```powershell
# Parar processos .NET
taskkill /F /IM dotnet.exe

# Parar processos Node
taskkill /F /IM node.exe
```

---

### **Swagger não aparece**

**Causa**: Tentando acessar URL incorreta.

**Solução**: Acesse `http://localhost:5000/swagger` (não precisa do `/index.html`)

---

## 👤 Autor

**Vitor Hugo Marcelino Santos**

Projeto desenvolvido como parte de um desafio técnico Full Stack, demonstrando:

- ✅ Arquitetura de software moderna (DDD, CQRS, Event Sourcing)
- ✅ Backend robusto com .NET 6 e padrões avançados
- ✅ Frontend responsivo com React e TailwindCSS
- ✅ Boas práticas de desenvolvimento e organização de código
- ✅ Documentação clara e completa

---

## 📄 Licença

Este projeto foi desenvolvido para fins educacionais e de avaliação técnica.

**Uso livre** para estudos, portfólio e demonstrações técnicas.

---

<div align="center">

**Desenvolvido por Vitor Hugo Marcelino Santos**

**.NET 6 • React • SQL Server**

`DDD` • `CQRS` • `Event Sourcing` • `Clean Architecture`

</div>
