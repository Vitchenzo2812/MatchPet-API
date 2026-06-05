# Spec: CreateAnimal

## Contexto
Esse endpoint serve para adicionar um animal ao sistema

## Stack
- C# .NET
- FluentValidation
- Entity Framework Core
- Pattern: Feature Based - Mediatr
- Banco: MySQL
- Bucket S3 Amazon

## Modelo de domínio

**AnimalGender**
```csharp
enum AnimalGender 
{
    MALE,
    FEMALE
}
```

**AnimalType**
```csharp
enum AnimalType 
{
    DOG,
    CAT,
    BIRD,
    RODENTS,
    REPTILES,
    EXOTIC
}
```

**AnimalSize**
```csharp
enum AnimalSize 
{
    SMALL,
    MEDIUM,
    LARGE
}
```

**Animal**
```csharp
class Animal 
{
    string Name
    string Description
    AnimalGender Gender
    AnimalType Type
    AnimalSize Size
    AnimalSupportType SupportType
    string Photo
    int Age
    string? Breed
    Datetime ShelterSince
    bool IsVaccinated
    bool IsSterilized
    bool IsUnderTreatment
    bool IsReadyTo
}
```

## Endpoint

### Controller / Rota

**AnimalManagerController**

POST /animal

### Request
```json
{
  "name": "string - nome do animal",
  "description": "string (html) - descrição do animal",
  "gender": "enum (string) AnimalGender - gênero do animal",
  "type": "enum (string) AnimalType - tipo do animal",
  "size": "enum (string) AnimalSize - tamanho do animal",
  "supportType": "enum (string) SupportType - Se o animal está disponível para adoção ou apadrinhamento",
  "photo": "string - base64 da foto do animal",
  "age": "int - idade do animal",
  "breed": "string | undefined - raça do animal",
  "shelterSince": "Datetime - data de quando o animal chegou no abrigo",
  "isVaccinated": "bool - se o animal é ou não vacinado",
  "isSterilized": "bool - se o animal é ou não castrado",
  "isUnderTreatment": "bool - se o animal está ou não em tratamento",
  "IsReadyTo": "bool - se o animal está ou não pronto para ser adotado/apadrinhado"
}
```

### Response (200)
Apenas retorna status 201

### Response (Erros)
| Status | Quando |
|--------|--------|
| 400 | Validação falhou (FluentValidation) |
| 500 | Erro ao salvar no banco ou S3 |

## Contexto do Projeto
- `AnimalSupportType` já existe em: `MatchPet.Shared/Models/AnimalSupportType.cs` (não recriar)

### Serviço de Upload de Imagem

Já existe um serviço implementado para S3. NÃO criar um novo.

**Interface:** `MatchPet.Infrastructure.Services.Contracts.IFileManagerService`
**Implementação:** `MatchPet.Infrastructure.Services.S3ManagerService`

Injetar via DI no Handler:
```csharp
private readonly IFileManagerService _fileManagerService;
```

Para salvar a foto do animal:
- Chamar `_fileManagerService.SaveFile(request.Photo, Guid.NewGuid().ToString())`
- O retorno é a URL pública da imagem — salvar no campo `Photo` da entidade
- O método já valida se é base64 válido internamente
- Em caso de falha lança `InternalServerError` automaticamente — não precisa tratar

## Regras de Negócio
- Você deve utilizar o padrão UnitOfWork, ele já existe atualmente no projeto, está em `MatchPet.Infrastructure.Database/`
- As Enums devem ser utilizadas sempre como o nameof nas mesmas, nunca os números atribuidos a elas (pode salvar no banco como um número, podem deve ocorrer a conversão para string quando puxar os dados do banco)

## Validações
- Verificar se os campos não são vazios/nulos
- Caso um animal esteja em tratamento, não deve ser possível adotar/apadrinhar o mesmo
- Verificar se idade do animal é válida (não pode ser um número negativo)
- Verificar se a data "shelterSince" é valida (não deve ser válido selecionar uma data após o dia atual, nem mais de 120 anos atrás)

## Estrutura de Arquivos Esperada
src/
├── MatchPet.Features/
│   └── Animals/
│       └── CreateAnimal/
│           ├──[Nome]Handler.cs
│           ├──[Nome]Validator.cs
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
    │   └──CreateAnimal/
    │
    └── Integration/
        └──CreateAnimal/

## Testes Esperados
- [ ] Teste unitário do Handler
- [ ] Teste de validação
- [ ] Teste de integração do endpoint

## O que NÃO fazer
- Não alterar entidades existentes
- Não adicionar Autenticação/Autorização JWT
- Não deixar nem tipo de Key exposta

## Obs
- Você deve utilizar o ID do animal para salvar a imagem no S3
- `IsReadyTo` (bool): indica se o animal está pronto para o processo definido em `SupportType`.
  Ex: se `SupportType = ADOPTION`, significa que está pronto para adoção.