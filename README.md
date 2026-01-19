🎮  Catalog API – FIAP Cloud Games 

.NET 8 | Entity Framework Core | SQL Server | Swagger | JWT

📌 Sobre o Projeto

A Catalog API é responsável por gerenciar o catálogo de jogos da plataforma FIAP Cloud Games (FCG).
Ela centraliza todas as informações dos jogos disponíveis para compra, servindo como base para outros microsserviços, como Payment API e Users API.

🎯 Funcionalidades

Cadastro de jogos (Administrador)

Atualização e remoção de jogos

Listagem de jogos disponíveis

Consulta de detalhes de um jogo

Ativação e desativação de jogos

Persistência com Entity Framework Core

Documentação com Swagger

🏗️ Arquitetura

Backend: .NET 8 (Web API)

Banco de Dados: SQL Server

ORM: Entity Framework Core

Autenticação: JWT

Documentação: Swagger / OpenAPI

📂 Estrutura do Projeto
Catalogs.API
├── Controllers
├── Domain
│   ├── Entities
│   ├── Interfaces
│   └── Enums
├── Application
│   ├── DTOs
│   ├── Services
│   └── UseCases
├── Infrastructure
│   ├── Context
│   └── Repositories
├── Migrations
├── Program.cs
└── appsettings.json

🐳 Docker – Banco de Dados

Imagem utilizada:

mcr.microsoft.com/mssql/server:2022-latest


Arquivo .env:

ACCEPT_EULA=Y
MSSQL_SA_PASSWORD=YourStrong!Passw0rd
TZ=America/Sao_Paulo
MSSQL_PID=Developer


Subir o container:

docker compose up -d

🗄️ Entity Framework

Aplicar migrations:

dotnet ef database update

🔑 JWT Authentication

Configure a chave JWT:

dotnet user-secrets set "Jwt:Key" "sua-chave-256-bits"

▶️ Executando a API
dotnet run


Swagger:

https://localhost:{porta}/swagger

🔐 Autorização

Operações administrativas exigem perfil Administrador

Autenticação via JWT

Token deve ser informado no Swagger usando:

Bearer {seu_token}

🔗 Integrações

Payment API: consulta de jogos para validação de compras

Users API: validação de permissões administrativas