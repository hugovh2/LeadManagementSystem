# Sistema de Gerenciamento de Leads

Sistema profissional Full Stack para gerenciamento de leads desenvolvido com .NET 6, React e SQL Server, seguindo os princípios de DDD (Domain-Driven Design), padrão CQRS com MediatR e Event Sourcing.

## 🏗️ Arquitetura

Esta solução segue os princípios de Clean Architecture com clara separação de responsabilidades:

```
LeadsManager/
├── LeadsManager.Domain/          # Camada de Domínio (Entidades, Value Objects, Eventos)
├── LeadsManager.Application/     # Camada de Aplicação (CQRS, Commands, Queries, Handlers)
├── LeadsManager.Infrastructure/  # Camada de Infraestrutura (EF Core, Repositórios, Serviços)
├── LeadsManager.API/             # Camada de API (.NET 6 Web API)
└── leads-front-web/            # Frontend (React SPA)
```

## ✨ Funcionalidades

### Backend (.NET 6)
- ✅ **DDD (Domain-Driven Design)** - Modelo de domínio rico com entidades, value objects e agregados
- ✅ **CQRS com MediatR** - Separação de operações de leitura e escrita
- ✅ **Event Sourcing** - Trilha de auditoria completa de todos os eventos do domínio
- ✅ **EF Core 6** - ORM com SQL Server
- ✅ **API RESTful** - Endpoints de API limpos e documentados
- ✅ **Migrations Automáticas** - Migrations de banco de dados executadas na inicialização

### Frontend (React)
- ✅ **React 18 Moderno** - Componentes funcionais com hooks
- ✅ **TailwindCSS** - Interface bonita e responsiva
- ✅ **Ícones Lucide** - Biblioteca moderna de ícones
- ✅ **Arquitetura SPA** - Single Page Application com roteamento no cliente

### Lógica de Negócio
- ✅ **Gerenciamento de Leads** - Criar, aceitar e recusar leads
- ✅ **Rastreamento de Status** - Status: Novo, Aceito, Recusado
- ✅ **Descontos Automáticos** - 10% de desconto para leads acima de $500 ao aceitar
- ✅ **Notificações por Email** - Email enviado para vendas quando lead é aceito (salvo em arquivo)
- ✅ **Event Store** - Todos os eventos de domínio são persistidos para auditoria e replay

## 🚀 Como Começar

### Pré-requisitos

- .NET 6 SDK
- Node.js 18+
- SQL Server 2019+ ou SQL Server Express (gratuito)
- Docker (opcional, para implantação containerizada)

### Opção 1: Desenvolvimento Local

#### 1. Instalar Pré-requisitos

**Instalar .NET 6 SDK:**
1. Baixe de: https://dotnet.microsoft.com/download/dotnet/6.0
2. Execute o instalador
3. Verifique: abra PowerShell e digite `dotnet --version`

**Instalar Node.js:**
1. Baixe de: https://nodejs.org/ (versão 18 ou superior)
2. Execute o instalador
3. Verifique: abra PowerShell e digite `node --version`

**Instalar SQL Server:**
1. Baixe SQL Server Express de: https://www.microsoft.com/sql-server/sql-server-downloads
2. Escolha a instalação **"Basic"** (Básica)
3. Siga o assistente de instalação
4. Anote o nome da instância (geralmente `localhost` ou `localhost\SQLEXPRESS`)

#### 2. Configuração do Banco de Dados

Atualize a connection string em `LeadsManager.API/appsettings.json`:

**Para Autenticação do Windows:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=LeadsManagerDB;Integrated Security=True;TrustServerCertificate=True;Encrypt=False"
  }
}
```

**Para Autenticação SQL Server:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=LeadsManagerDB;User Id=seu_usuario;Password=sua_senha;TrustServerCertificate=True;Encrypt=False"
  }
}
```

#### 3. Instalar Ferramentas EF Core (Primeira vez)

Abra PowerShell e execute:

```powershell
dotnet tool install --global dotnet-ef --version 6.0.25
```

#### 4. Restaurar Pacotes do Backend

Na pasta raiz do projeto:

```powershell
cd C:\Users\seu_usuario\CascadeProjects\LeadManagementSystem
dotnet restore LeadsManager.sln
```

#### 5. Criar e Aplicar Migrations do Banco de Dados

**Criar migrations:**
```powershell
dotnet ef migrations add InitialCreate --project LeadsManager.Infrastructure --startup-project LeadsManager.API
```

**Aplicar ao banco:**
```powershell
dotnet ef database update --project LeadsManager.Infrastructure --startup-project LeadsManager.API
```

Isso criará o banco `LeadsManagerDB` e todas as tabelas necessárias.

#### 6. Executar a API Backend

```powershell
cd LeadsManager.API
dotnet run
```

A API estará disponível em:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:5001`
- **Swagger**: `http://localhost:5000/swagger`

As migrations do banco de dados serão executadas automaticamente na inicialização.

#### 7. Configurar e Executar o Frontend

**Instalar dependências:**
```powershell
cd leads-front-web
npm install
```

**Criar arquivo de ambiente:**

O arquivo `.env` já existe. Se não existir, copie do exemplo:
```powershell
copy .env.example .env
```

Conteúdo do `.env`:
```
REACT_APP_API_URL=http://localhost:5000/api
```

**Iniciar servidor de desenvolvimento:**
```powershell
npm start
```

A aplicação abrirá automaticamente em `http://localhost:3000`

### Opção 2: Implantação com Docker

**Pré-requisito:** Docker Desktop instalado

Compile e execute tudo com Docker Compose:

```powershell
docker-compose up --build
```

Isso iniciará:
- SQL Server na porta 1433
- API na porta 5000
- Frontend na porta 3000

Acesse a aplicação em `http://localhost:3000`

## 📊 Esquema do Banco de Dados

A aplicação usa duas tabelas principais:

### Tabela Leads
- Id (PK) - Identificador único
- Informações de Contato (entidade proprietária)
  - FirstName - Primeiro nome
  - LastName - Sobrenome
  - Email - Email
  - PhoneNumber - Telefone
- Localização (entidade proprietária)
  - Suburb - Bairro/Cidade
  - PostalCode - Código postal
- Preço (entidade proprietária)
  - Amount - Valor
  - Currency - Moeda
- Category - Categoria do serviço
- Description - Descrição detalhada
- Status - Status (New, Accepted, Declined)
- CreatedAt - Data de criação
- AcceptedAt - Data de aceitação
- DeclinedAt - Data de recusa

### Tabela Events (Event Store)
- Id (PK) - Identificador único
- EventType - Tipo do evento
- AggregateId - ID do agregado relacionado
- Data - Dados do evento em JSON
- OccurredOn - Data/hora de ocorrência

## 🔌 Endpoints da API

### Leads

- `GET /api/leads` - Buscar todos os leads
- `GET /api/leads/status/{status}` - Buscar leads por status (New, Accepted, Declined)
- `POST /api/leads` - Criar um novo lead
- `PUT /api/leads/{id}/accept` - Aceitar um lead
- `PUT /api/leads/{id}/decline` - Recusar um lead

### Documentação Swagger

Acesse a documentação da API em: `http://localhost:5000/swagger`

## 🧪 Testando a Aplicação

### Opção 1: Usar o Swagger (Recomendado)

1. Acesse `http://localhost:5000/swagger`
2. Clique em **POST /api/leads**
3. Clique em **"Try it out"**
4. Cole este JSON de exemplo:

```json
{
  "firstName": "João",
  "lastName": "Silva",
  "email": "joao.silva@example.com",
  "phoneNumber": "0412345678",
  "suburb": "São Paulo",
  "category": "Encanamento",
  "description": "Preciso de reparo urgente de encanamento na cozinha",
  "price": 650.00
}
```

5. Clique em **"Execute"**
6. Vá para `http://localhost:3000` e veja o lead aparecer!

### Opção 2: Usar o Script PowerShell

Na raiz do projeto, execute:
```powershell
.\seed-data.ps1
```

Isso criará 6 leads de exemplo automaticamente.

### Opção 3: Via linha de comando

**Criar um lead:**
```powershell
Invoke-RestMethod -Uri "http://localhost:5000/api/leads" -Method POST -ContentType "application/json" -Body '{
  "firstName": "Maria",
  "lastName": "Santos",
  "email": "maria@example.com",
  "phoneNumber": "0412345678",
  "suburb": "Rio de Janeiro",
  "category": "Pintura",
  "description": "Pintura de 2 janelas",
  "price": 450.00
}'
```

**Aceitar um lead:**
```powershell
Invoke-RestMethod -Uri "http://localhost:5000/api/leads/{lead-id}/accept" -Method PUT
```

**Buscar leads por status:**
```powershell
# Buscar leads novos (convidados)
Invoke-RestMethod -Uri "http://localhost:5000/api/leads/status/New"

# Buscar leads aceitos
Invoke-RestMethod -Uri "http://localhost:5000/api/leads/status/Accepted"
```

## 🏛️ Padrões de Design e Princípios

### Domain-Driven Design (DDD)
- **Entidades**: Lead (raiz agregada)
- **Value Objects**: ContactInfo, Location, Money
- **Eventos de Domínio**: LeadCreatedEvent, LeadAcceptedEvent, LeadDeclinedEvent
- **Repositórios**: Abstração ILeadRepository
- **Agregados**: Lead é o agregado principal com invariantes

### CQRS (Command Query Responsibility Segregation)
- **Commands**: CreateLeadCommand, AcceptLeadCommand, DeclineLeadCommand
- **Queries**: GetAllLeadsQuery, GetLeadsByStatusQuery
- **Handlers**: Handlers separados para cada comando e consulta usando MediatR

### Event Sourcing
- Todos os eventos de domínio são persistidos na tabela Events
- Eventos contêm mudanças de estado completas em formato JSON
- Possibilidade de reconstruir estado do agregado a partir dos eventos
- Trilha de auditoria completa de todas as mudanças

## 📁 Project Structure

### Domain Layer
```
LeadsManager.Domain/
├── Common/
│   ├── BaseEntity.cs
│   └── DomainEvent.cs
├── Entities/
│   └── Lead.cs
├── Enums/
│   └── LeadStatus.cs
├── Events/
│   ├── LeadCreatedEvent.cs
│   ├── LeadAcceptedEvent.cs
│   └── LeadDeclinedEvent.cs
├── Repositories/
│   └── ILeadRepository.cs
└── ValueObjects/
    ├── ContactInfo.cs
    ├── Location.cs
    └── Money.cs
```

### Application Layer
```
LeadsManager.Application/
├── Commands/
│   ├── CreateLeadCommand.cs
│   ├── AcceptLeadCommand.cs
│   └── DeclineLeadCommand.cs
├── Queries/
│   ├── GetAllLeadsQuery.cs
│   └── GetLeadsByStatusQuery.cs
├── Handlers/
│   ├── CreateLeadCommandHandler.cs
│   ├── AcceptLeadCommandHandler.cs
│   ├── DeclineLeadCommandHandler.cs
│   ├── GetAllLeadsQueryHandler.cs
│   └── GetLeadsByStatusQueryHandler.cs
├── DTOs/
│   └── LeadDto.cs
├── Mappings/
│   └── MappingProfile.cs
└── Common/
    └── Interfaces/
        ├── IEmailService.cs
        └── IEventStore.cs
```

### Infrastructure Layer
```
LeadsManager.Infrastructure/
├── Persistence/
│   ├── LeadsDbContext.cs
│   └── StoredEvent.cs
├── Repositories/
│   └── LeadRepository.cs
├── EventSourcing/
│   └── EventStore.cs
├── Services/
│   └── EmailService.cs
└── DependencyInjection.cs
```

### API Layer
```
LeadsManager.API/
├── Controllers/
│   └── LeadsController.cs
├── Program.cs
├── appsettings.json
└── Dockerfile
```

### Frontend
```
leads-front-web/
├── public/
│   ├── index.html
│   └── manifest.json
├── src/
│   ├── components/
│   │   ├── LeadCard.js
│   │   └── TabNav.js
│   ├── services/
│   │   └── api.js
│   ├── App.js
│   ├── index.js
│   └── index.css
├── package.json
├── tailwind.config.js
└── Dockerfile
```

## 🎯 Regras de Negócio

1. **Criação de Lead**: Leads são criados com status "New" (Novo)
2. **Aceitação de Lead**:
   - Somente leads com status "New" podem ser aceitos
   - Se preço > $500, desconto automático de 10% é aplicado
   - Notificação por email enviada para vendas@test.com (email simulado salvo em arquivo)
   - Status do lead alterado para "Accepted" (Aceito)
3. **Recusa de Lead**:
   - Somente leads com status "New" podem ser recusados
   - Status do lead alterado para "Declined" (Recusado)
4. **Serviço de Email**: 
   - Email simulado enviado para **vendas@test.com**
   - Emails são salvos na pasta `LeadsManager.API/EmailLogs/` como arquivos de texto
   - Cada email contém: informações do lead, preço final, próximos passos
   - Formato: `Email_YYYYMMDD_HHMMSS_[GUID].txt`

## 🔐 Considerações de Segurança

Para implantação em produção, considere:
- Adicionar autenticação (JWT, OAuth)
- Adicionar autorização baseada em roles
- Usar HTTPS
- Proteger strings de conexão do banco de dados
- Adicionar rate limiting
- Implementar validação de entrada
- Adicionar restrições CORS
- Usar variáveis de ambiente para dados sensíveis

## 🛠️ Solução de Problemas

### Erro: "The server was not found or was not accessible"
**Solução**: Verifique se o SQL Server está rodando. Reinicie o serviço SQL Server.

### Erro: "Invalid object name 'Leads'"
**Solução**: Execute as migrations:
```powershell
dotnet ef database update --project LeadsManager.Infrastructure --startup-project LeadsManager.API
```

### Erro: CORS policy bloqueando requisições
**Solução**: O CORS já está configurado no código. Certifique-se de que a API está rodando e reinicie o frontend.

### Swagger não aparece
**Solução**: Acesse `http://localhost:5000/swagger` (sem /index.html). O Swagger já está habilitado para todos os ambientes.

### Porta já em uso
**Solução**: Pare os processos anteriores:
```powershell
# Parar API
taskkill /F /IM LeadsManager.API.exe

# Parar Frontend
taskkill /F /IM node.exe
```

## 📝 Licença

Este projeto foi desenvolvido como um desafio técnico Full Stack para demonstração de habilidades em .NET, React e arquitetura de software.

**Uso**: Livre para fins educacionais e avaliação técnica.

## 👤 Autor

**Vitor Hugo Marcelino Santos**

Projeto desenvolvido como parte de um desafio técnico Full Stack, demonstrando conhecimentos em:
- .NET 6 Web API
- React com TailwindCSS
- Domain-Driven Design (DDD)
- CQRS com MediatR
- Event Sourcing
- SQL Server com Entity Framework Core

## 🛠️ Tecnologias Utilizadas

- **.NET 6** - Framework backend
- **React 19** - Framework frontend
- **SQL Server** - Banco de dados
- **Entity Framework Core 6** - ORM
- **MediatR** - Implementação CQRS
- **TailwindCSS** - Estilização
- **Lucide React** - Ícones

---

**Desenvolvido por Vitor Hugo Marcelino Santos** | Desafio Técnico Full Stack .NET
