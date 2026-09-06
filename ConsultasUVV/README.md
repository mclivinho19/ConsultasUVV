# UVV Consultas - Sistema de Gestão de Consultas

Aplicação web desenvolvida em **ASP.NET Core MVC** com **Entity Framework Core** e **SQL Server** para gerenciamento de usuários e consultas médicas/profissionais.

### Desenvolvido por:

- GUIlherme Marques Rezende

---

## 🎥 Vídeo Demonstrativo

https://youtu.be/rZQX1RbzJ2o?is=vXvyglUiB7auTLDZ

---


## 🎯 Funcionalidades

- Cadastro de usuários com validação (Data Annotations)
- Login e logout com autenticação por cookies
- CRUD completo de consultas (listar, criar, editar, excluir)
- Proteção de rotas com `[Authorize]`
- Persistência com EF Core (Code First + Migrations)

---

## 🛠️ Pré-requisitos

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server) (ou Docker)

---

## ⚙️ Configuração do Banco de Dados

1. No arquivo `appsettings.json`, ajuste a string de conexão:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=ConsultasUVV;User Id=sa;Password=SenhaForte123!;TrustServerCertificate=True;"
}
```

2. Execute as migrations para criar o banco e as tabelas:

```bash
dotnet ef database update
```

Se ainda não tiver as migrations, gere-as:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```
---

## 🚀 Executando o Projeto
``` bash
dotnet run
```
Acesse: http://localhost:5239 (ou a porta exibida no console).

---

## 📚 Estrutura do Projeto
```text
ConsultasUVV/
├── Controllers/
│   ├── AccountController.cs
│   ├── ConsultasController.cs
│   └── HomeController.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Migrations/
├── Models/
│   ├── Usuario.cs
│   └── Consulta.cs
├── Views/
│   ├── Account/
│   ├── Consultas/
│   ├── Home/
│   └── Shared/
├── wwwroot/
├── Program.cs
├── ConsultasUVV.postman_collection.json
├── appsettings.json
└── README.md
```

## 🧪 Testes com Postman

Foi incluído na raíz do projeto a coleção Postman (`ConsultasUVV.postman_collection.json`) utilizada para testar os principais endpoints antes da proteção das rotas com `[ValidateAntiForgeryToken]`.
