# Spec: GetAnimalById

## Contexto
Esse endpoint serve para buscar um animal específico pelo seu ID no sistema.

## Stack
- C# .NET
- FluentValidation
- Entity Framework Core
- Pattern: Feature Based - Mediatr
- Banco: MySQL

## Contexto do Projeto
- As entidades e enums já existem em: `MatchPet.Shared/Models/` (não recriar)

## Endpoint

### Controller / Rota

**AnimalManagerController**

GET /animal/{id}

### Response (200)
```json
{
  "id": "guid - identificador único do animal",
  "name": "string - nome do animal",
  "description": "string (html) - descrição do animal",
  "gender": "enum (string) AnimalGender - gênero do animal",
  "type": "enum (string) AnimalType - tipo do animal",
  "size": "enum (string) AnimalSize - tamanho do animal",
  "supportType": "enum (string) AnimalSupportType - tipo de suporte disponível",
  "photo": "string - URL da foto do animal",
  "age": "int - idade do animal",
  "breed": "string | null - raça do animal",
  "shelterSince": "Datetime - data de quando o animal chegou no abrigo",
  "isVaccinated": "bool - se o animal é ou não vacinado",
  "isSterilized": "bool - se o animal é ou não castrado",
  "isUnderTreatment": "bool - se o animal está ou não em tratamento",
  "isReadyTo": "bool - se o animal está ou não pronto para ser adotado/apadrinhado"
}
```

### Response (Erros)
| Status | Quando |
|--------|--------|
| 404 | Animal não encontrado |
| 500 | Erro ao buscar no banco |

## Regras de Negócio
- As Enums devem ser retornadas sempre como string (nameof), nunca como número

## Validações
- Verificar se o animal existe na base de dados (se não existir, lançar um NotFoundError)

## Estrutura de Arquivos Esperada
src/
├── MatchPet.Features/
│   └── Animals/
│       └── GetAnimalById/
│           ├──[Nome]Handler.cs
│           ├──[Nome]Request.cs
│           └──[Nome]Response.cs
│
├── MatchPet.Infrastructure/
│   └── Repositories/
│       ├──Contracts/I[Nome]Repository.cs
│       └──[Nome]Repository.cs
│
└── MatchPet.Api/
    └── Controllers/[Nome]Controller.cs

## Testes Esperados
- [ ] Teste unitário do Handler
- [ ] Teste de integração do endpoint

## O que NÃO fazer
- Não alterar entidades existentes
- Não adicionar Autenticação/Autorização JWT
- Não deixar nenhum tipo de Key exposta
- Não incluir campos internos desnecessários na response (ex: campos de auditoria)