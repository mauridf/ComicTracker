# ComicTracker API

![ComicTracker Logo](coloque_seu_logo_aqui.png)

API para gerenciamento de coleções de HQs (Scans) integrada com a Comic Vine.

## 📌 Visão Geral
O ComicTracker é uma API que permite:

- Cadastrar sua coleção de HQs
- Registrar quais edições você já leu
- Adicionar notas e observações às edições
- Integração com a API da Comic Vine para obter dados de:
  - **Editoras (Publishers)**
  - **Personagens (Characters)**
  - **Equipes (Teams)**
  - **Volumes (HQs)**
  - **Edições (Issues)**

## 🚀 Funcionalidades Principais

✅ Cadastro de editoras (DC, Marvel, etc.)  
✅ Registro de personagens e equipes  
✅ Gerenciamento de volumes (HQs) e edições  
✅ Marcação de edições lidas/não lidas  
✅ Adição de notas e avaliações  
🔍 Busca integrada com a Comic Vine API  

## 🛠 Tecnologias Utilizadas

- **Backend:** .NET 9
- **Banco de Dados:** PostgreSQL
- **ORM:** Entity Framework Core
- **Autenticação:** (Não requer cadastro de usuário)
- **Documentação:** Swagger/OpenAPI

## 📦 Estrutura do Projeto
```
ComicTracker/
├── ComicTracker.API/          # Camada de apresentação (Controllers)
├── ComicTracker.Application/  # Lógica de negócios (Services, DTOs)
├── ComicTracker.Domain/       # Entidades e interfaces
├── ComicTracker.Infrastructure/ # Implementações (Repositórios, DbContext)
└── ComicTracker.Tests/        # Testes unitários e de integração
```

## 🔧 Pré-requisitos

- .NET 9 SDK
- PostgreSQL (ou Docker)
- Comic Vine API Key

## 🚀 Como Executar

### Configuração do banco de dados:
```bash
docker run --name comic-tracker-db -e POSTGRES_PASSWORD=yourpassword -p 5432:5432 -d postgres
```

### Configuração da aplicação:
Crie um arquivo `appsettings.Development.json` em `ComicTracker.API`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ComicTracker;Username=postgres;Password=yourpassword"
  },
  "ComicVine": {
    "ApiKey": "sua_chave_da_comic_vine",
    "BaseUrl": "https://comicvine.gamespot.com/api/"
  }
}
```

### Aplicar migrações:
```bash
dotnet ef database update --project ComicTracker.Infrastructure --startup-project ComicTracker.API
```

### Executar a aplicação:
```bash
dotnet run --project ComicTracker.API
```
A API estará disponível em `https://localhost:5001` (ou porta configurada).

## 📚 Documentação da API
Acesse a documentação Swagger em:
```
https://localhost:5001/swagger
```

## 📋 Endpoints Principais

### **Editoras (Publishers)**
- `GET /api/publishers/search?name={name}` - Busca editoras na Comic Vine
- `POST /api/publishers` - Cadastra uma nova editora

### **Personagens (Characters)**
- `GET /api/characters/search?name={name}` - Busca personagens na Comic Vine
- `POST /api/characters` - Cadastra um novo personagem

### **Equipes (Teams)**
- `GET /api/teams/search?name={name}` - Busca equipes na Comic Vine
- `POST /api/teams` - Cadastra uma nova equipe

### **Volumes (HQs)**
- `GET /api/volumes/search?name={name}` - Busca volumes na Comic Vine
- `POST /api/volumes` - Cadastra um novo volume

### **Edições (Issues)**
- `GET /api/issues?volumeId={id}` - Lista edições de um volume
- `POST /api/issues` - Cadastra uma nova edição
- `PUT /api/issues/{id}/read` - Marca edição como lida
- `PUT /api/issues/{id}/rate` - Adiciona nota à edição

## 🤝 Contribuição

Contribuições são bem-vindas! Siga estes passos:

1. Faça um **fork** do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um **Pull Request**

## 📄 Licença
Distribuído sob a licença **MIT**. Veja `LICENSE` para mais informações.

## ✉️ Contato

Seu Nome - [@seu_twitter](https://twitter.com/seu_twitter) - seuemail@exemplo.com

Link do Projeto: [https://github.com/seuusuario/ComicTracker](https://github.com/seuusuario/ComicTracker)

---

Made with ❤️ by **Seu Nome**
