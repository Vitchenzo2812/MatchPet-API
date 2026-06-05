# Spec: DeleteAnimal

## Contexto
Esse endpoint serve para deletar um animal no sistema

## Stack
- C# .NET
- FluentValidation
- Entity Framework Core
- Pattern: Feature Based - Mediatr
- Banco: MySQL
- Bucket S3 Amazon

## Contexto do Projeto
- As entidades e enums já existem em: `MatchPet.Shared/Models/` (não recriar)

## Endpoint

### Controller / Rota

**AnimalManagerController**

Delete /animal/{id}

### Response (200)
Apenas retorna status 204

### Response (Erros)
| Status | Quando                              |
|--------|-------------------------------------|
| 400 | Validação falhou (FluentValidation) |
| 404 | Animal não encontrado |
| 500 | Erro ao chamar no banco ou S3       |

## Ordem de Execução no Handler

1. Buscar animal no banco (lançar NotFoundError se não existir)
2. Remover imagem do S3 via `_fileManagerService.RemoveFile`
3. Deletar animal do banco via UnitOfWork

### Remoção de Imagem

Já existe um serviço implementado para S3. NÃO criar um novo.

**Interface:** `MatchPet.Infrastructure.Services.Contracts.IFileManagerService`
**Implementação:** `MatchPet.Infrastructure.Services.S3ManagerService`

Injetar via DI no Handler:
```csharp
private readonly IFileManagerService _fileManagerService;
```

Para remover a foto do animal:
- Chamar `_fileManagerService.RemoveFile(animal.Id.ToString())`
- Em caso de falha lança `InternalServerError` automaticamente — não precisa tratar 

## Regras de Negócio
- Você deve utilizar o padrão UnitOfWork, ele já existe atualmente no projeto, está em `MatchPet.Infrastructure.Database/`

## Validações
- Verificar se o animal existe na base de dados (se não existir, lançar um NotFoundError)

## Estrutura de Arquivos Esperada
src/
├── MatchPet.Features/
│   └── Animals/
│       └── DeleteAnimal/
│           ├──[Nome]Handler.cs
│           └──[Nome]Request.cs
│
├── MatchPet.Shared/
│   └── Models/[Entidade].cs
│
├── MatchPet.Infrastructure/
│   └── Repositories/
│       ├──Contracts/I[Nome]Repository.cs
│       └──[Nome]Repository.cs
│
├── MatchPet.Api/
│   └── Controllers/[Nome]Controller.cs
│
└── MatchPet.Tests/
├── Unit/
│   └──DeleteAnimal/
│
└── Integration/
    └──DeleteAnimal/

## Testes Esperados
- [ ] Teste unitário do Handler
- [ ] Teste de validação
- [ ] Teste de integração do endpoint

## O que NÃO fazer
- Não alterar entidades existentes
- Não adicionar Autenticação/Autorização JWT
- Não deixar nem tipo de Key exposta

## Obs
- Você deve utilizar o ID do animal para remover a imagem no S3