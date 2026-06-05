## Contexto do Projeto

Esse é um projeto Backend voltado a uma ONG de adoção e apadrinhamento de 
animais chamada MatchPet. 

O projeto em si é basicamente um CRUD onde será possível adicionar,
remover, editar e visualizar animais, além disso, também será possível salvar
formulários de adoção e apadrinhamento que serão enviados para que os administradores
avaliem os casos e entrem em contato com quem preencheu o formulário.

O projeto será algo simples, utilizaremos C# .NET, Entity Framework, MySQL,
estrutura Feature Based (utilizando Mediatr) e sem nenhum tipo de Autenticação/
Autorização nos endpoints. Também vamos utilizar um Bucket da Amazon S3 para salvar 
a imagem dos animais.

Alguns modelos de negócio já estão criados dentro da pasta MatchPet.Shared/Models/
