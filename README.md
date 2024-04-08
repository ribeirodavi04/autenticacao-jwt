# Autenticação JWT com ASP.NET Core WebAPI e Clean Architecture

Este é um projeto de API web ASP.NET Core que implementa autenticação JWT (JSON Web Token) para proteger os endpoints da API. O projeto segue os princípios da arquitetura limpa, que organiza a aplicação nas seguintes camadas:

#### Camada de Infraestrutura de Dados (Infra.Data):
- Responsável por implementar a lógica de acesso a dados e persistência no banco de dados.
- Inclui classes e interfaces para interagir com o banco de dados, como repositórios, contexto do Entity Framework, configuração de mapeamento de entidades e migrações do banco de dados.

#### Camada de Infraestrutura de IoC (Infra.IoC):
- Responsável por configurar e gerenciar a injeção de dependência (IoC - Inversion of Control) na aplicação.
- Contém classe de configurações relacionadas à injeção de dependência, como registros de serviços e respoitorios.

#### Camada de Domínio (Domain):
- Contém as entidades principais da aplicação.
- Inclui interfaces e tipos de valor que definem os contratos e comportamentos das entidades, como regras de negócio, validações, etc.

#### Camada de Aplicação (Application):
- Contém a lógica de negócio da aplicação e atua como uma camada intermediária entre as camadas de API e de Infraestrutura.
- Inclui serviços de aplicação, que coordenam as operações entre as entidades do domínio e os serviços de infraestrutura.

#### Camada de API (API):
- É a interface externa da aplicação, expondo os endpoints HTTP para interações com clientes externos.
- Inclui os controladores da Web API, que recebem solicitações HTTP, processam-nas e retornam respostas apropriadas.

Cada camada possui sua responsabilidade específica e contribui para a construção de um sistema robusto, escalável e de fácil manutenção.


## Tecnologias Utilizadas

Aqui estão as principais tecnologias e ferramentas que foram utilizadas no desenvolvimento deste projeto:

- [Entity Framework Core (Design, Tools)](https://docs.microsoft.com/en-us/ef/core/): Framework ORM (Object-Relational Mapper) utilizado para mapeamento objeto-relacional.
- [Npgsql.EntityFrameworkCore.PostgreSQL](https://www.npgsql.org/efcore/): Provedor de banco de dados PostgreSQL para o Entity Framework Core.
- [AutoMapper](https://automapper.org/): Biblioteca utilizada para mapear objetos automaticamente.
- [System.IdentityModel.Tokens.Jwt](https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt/): Biblioteca utilizada para a geração e validação de tokens JWT (JSON Web Tokens).
- [Authentication JWT Bearer](https://jwt.io/): Esquema de autenticação JWT utilizado para proteger os endpoints da API.

## Dependências e Versões Necessárias

Aqui estão as dependências necessárias para rodar o projeto e as versões que foram utilizadas:

- [Visual Studio 2022](https://visualstudio.microsoft.com/): Versão 2022
- [pgAdmin 4](https://www.pgadmin.org/): Versão 4
- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0): Versão 8.0
- [PostgreSQL 16.0](https://www.postgresql.org/about/news/postgresql-16-released-2715/): Versão 16.0

Certifique-se de instalar e configurar essas dependências de acordo com as versões especificadas para garantir o funcionamento adequado do projeto.

## Como rodar o projeto 

Para executar o projeto em sua máquina local, siga os passos abaixo:

#### 1 . Clonar o Repositório
```
git clone https://github.com/ribeirodavi04/AutenticacaoJWT.git
```

#### 2 . Configurar o Banco de Dados
- Certifique-se de ter o PostgreSQL instalado em sua máquina. Se não estiver instalado, você pode baixá-lo aqui.
- Utilize o pgAdmin 4 ou outra ferramenta de administração do PostgreSQL para criar um banco de dados com o nome especificado na string de conexão do arquivo appsettings.json.

#### 3. Abrir o Projeto no Visual Studio 2022:
- Navegue até o diretório onde o projeto foi clonado e abra o arquivo de solução .sln.

#### 4. Rodar as Migrações do Entity Framework Core:
- Abra o Console do Gerenciador de Pacotes no Visual Studio (View > Other Windows > Package Manager Console).
- Certifique-se de que o projeto de infraestrutura de dados (por exemplo, Infra.Data) esteja definido como o projeto padrão no Console do Gerenciador de Pacotes.
- Execute o comando Update-Database para aplicar as migrações e criar as tabelas no banco de dados.
```
Update-Database
```

#### 5. Configurar a String de Conexão:
- Abra o arquivo appsettings.json e substitua a string de conexão do banco de dados pelas credenciais do seu PostgreSQL, se necessário.

#### 6. Executar o Projeto:
- Pressione F5 ou clique em "Iniciar" no Visual Studio para executar o projeto.


## Endpoints da API

- Abaixo estão os endpoint que retorna um token JWT
```markdown
POST /api/Login/register - Cadastra um novo usuário e retorna um token JWT
POST /api/Login/login - Faz o login no sistema e retorna o token JWT
```

- Para executar estes endpoints é necessário estar autenticado no sistema 
```markdown
GET /api/User/Users - Retorna todos o usuários cadastrados
GET /api/User/{idUser} - Retorna um usuário especifico
PUT /api/User - Atualiza um usuário
DELETE /api/User/{idUser} - Apaga um usuário 
```
