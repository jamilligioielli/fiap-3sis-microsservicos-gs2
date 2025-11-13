# Versionamento de Prompts

## Descrição do Projeto

Este projeto implementa um sistema de versionamento de prompts, permitindo o cadastro, atualização e consulta de prompts com controle de versões. Ele foi desenvolvido utilizando a linguagem C# e a plataforma .NET 9.0, com uma arquitetura modular que separa as responsabilidades em camadas distintas: **Domain**, **DTO**, **Repository**, **Service** e **Controllers**.

---

## Tecnologias Utilizadas

- **C# 13.0**: Linguagem de programação principal.
- **.NET 9.0**: Framework para desenvolvimento da aplicação.
- **Entity Framework Core (InMemory)**: Utilizado para persistência de dados em memória.
- **Newtonsoft.Json**: Biblioteca para manipulação de JSON.
- **ASP.NET Core**: Framework para construção de APIs RESTful.
- **Microsoft.Extensions.Logging**: Para gerenciamento de logs.

---

## Estrutura do Projeto

A solução está organizada em múltiplos projetos, cada um com uma responsabilidade específica:

1. **Domain**: Contém as entidades principais do sistema, como `Prompt` e `Versao`.
2. **DTO**: Define os objetos de transferência de dados, como `PromptDTO` e `PromptVersaoDTO`.
3. **Repository**: Responsável pela persistência e recuperação de dados. Interfaces como `IPromptRepository` e `IVersaoRepository` são implementadas aqui.
4. **Service**: Contém a lógica de negócios, como o gerenciamento de versões de prompts, implementado na classe `VersionamentoPromptsService`.
5. **Controllers**: Define os endpoints da API, como `PromptsController`, que expõe as operações de cadastro, atualização e consulta de prompts.

---

## Regras de Negócio

### 1. Cadastro de Prompts
- Um novo prompt pode ser cadastrado através do endpoint `POST /api/prompts`.
- Caso o autor ou título não sejam fornecidos, valores padrão são atribuídos:
  - **Autor**: "desconhecido".
  - **Título**: "prompt novo".
- Ao cadastrar um prompt, uma nova versão inicial (versão 1) é criada automaticamente.

### 2. Atualização de Prompts
- Um prompt existente pode ser atualizado através do endpoint `PUT /api/prompts/{id}`.
- Antes de atualizar, o sistema verifica:
  - Se o ID do prompt é válido.
  - Se os dados fornecidos não são nulos.
  - Se o campo `texto` não está vazio.
- Ao atualizar um prompt:
  - Uma nova versão é criada.
  - O número da versão anterior é registrado.
  - O número da versão atual é incrementado.
  - A data de criação da nova versão é registrada.

### 3. Listagem de Prompts
- Todos os prompts disponíveis podem ser consultados através do endpoint `GET /api/prompts`.
- Para cada prompt, é retornado:
  - Autor, título, texto e número da versão atual.

### 4. Controle de Versões
- Cada prompt possui um histórico de versões, armazenado na entidade `Versao`.
- A entidade `Versao` mantém informações como:
  - ID da versão.
  - Número da versão atual e anterior.
  - Data de criação da versão.
  - Referência ao prompt associado.

---

## Endpoints da API

### 1. **Cadastrar Prompt**
- **Rota**: `POST /api/prompts`
- **Descrição**: Cadastra um novo prompt.
- **Corpo da Requisição**: { "titulo": "string", "texto": "string", "autor": "string" }
### 2. **Atualizar Prompt**
- **Rota**: `PUT /api/prompts/{id}`
- **Descrição**: Atualiza um prompt existente e cria uma nova versão.
- **Corpo da Requisição**: { "titulo": "string", "texto": "string", "autor": "string" }


### 3. **Listar Prompts**
- **Rota**: `GET /api/prompts`
- **Descrição**: Retorna todos os prompts disponíveis com suas versões atuais.

---

## Estrutura das Entidades

### 1. **Prompt**
- Representa o conteúdo principal do sistema.
- Propriedades:
- `id`: Identificador único.
- `texto`: Conteúdo do prompt.
- `titulo`: Título do prompt.
- `autor`: Autor do prompt.
- `dataCriacao`: Data de criação.
- `dataAlteracao`: Data da última alteração (opcional).

### 2. **Versao**
- Representa uma versão de um prompt.
- Propriedades:
- `idVersao`: Identificador único da versão.
- `prompt`: Referência ao prompt associado.
- `dataCriacao`: Data de criação da versão.
- `numeroVersaoAtual`: Número da versão atual.
- `numeroVersaoAnterior`: Número da versão anterior (opcional).

---

## Fluxo de Operações

1. **Cadastro de Prompt**:
 - Um novo prompt é criado e armazenado.
 - Uma versão inicial (versão 1) é gerada automaticamente.

2. **Atualização de Prompt**:
 - O prompt existente é atualizado.
 - Uma nova versão é criada, mantendo o histórico da versão anterior.

3. **Consulta de Prompts**:
 - Retorna todos os prompts com suas informações e número da versão atual.

---

> Jamilli Vitoria Gioielli - RM 552414