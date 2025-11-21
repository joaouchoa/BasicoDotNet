## 🚀 Desafio Técnico: API de Gerenciamento de Avisos

Este repositório apresenta a solução para o desafio técnico focado na **modificação e extensão de uma API existente** para implementar o ciclo completo de gerenciamento de avisos (CRUD) e introduzir melhorias cruciais de arquitetura e regras de negócio.

---

### 💡 Design e Arquitetura

A solução foi desenvolvida seguindo os princípios da **Clean Architecture** (Arquitetura Limpa), utilizando o padrão **CQRS (Command Query Responsibility Segregation)** através do **MediatR**.

| Componente | Decisão de Design | Justificativa |
| :--- | :--- | :--- |
| **Persistência** | **PostgreSQL** e **Entity Framework Core**. | Flexibilidade, escalabilidade e robustez do banco de dados relacional. |
| **Versionamento** | **DBUP** para Migrações de Banco. | Garante que os *scripts* SQL sejam aplicados de forma determinística e rastreável, evitando problemas de migração manual. |
| **Controle de Fluxo** | **MediatR** (Padrão Handler/Request). | Separa a lógica de **Comando** (escrita) e **Query** (leitura), melhorando a coesão e a manutenibilidade. |
| **Validação** | **Fluent Validation**. | Previne que regras de negócio básicas (como campos nulos ou IDs inválidos) atinjam a Camada de Aplicação, retornando `400 Bad Request` na entrada. |

---

### ✨ Funcionalidades e Endpoints Implementados

Foi implementado o CRUD completo para a entidade `Aviso`, com atenção especial aos códigos de status HTTP semânticos.

| Verbo HTTP | Rota | Descrição | Status de Sucesso |
| :--- | :--- | :--- | :--- |
| **`GET`** | `/avisos` | Retorna todos os avisos **ativos**. | 200 OK / 204 No Content |
| **`GET`** | `/avisos/{id}` | Retorna um aviso específico pelo ID. | 200 OK / 404 Not Found |
| **`POST`** | `/avisos` | **Cria** um novo aviso. | 201 Created |
| **`PUT`** | `/avisos/{id}` | **Atualiza** completamente um aviso existente. | 200 OK / 204 No Content |
| **`DELETE`** | `/avisos/{id}` | **Remove** logicamente um aviso (Soft Delete). | 204 No Content |

---

### 🛡️ Regras de Negócio e Controles

Foram adicionadas melhorias críticas para atender aos requisitos de rastreabilidade e controle de dados:

#### 1. Rastreabilidade (Metadata)
* **`DataCriacao`** e **`DataEdicao`**: Adicionados campos na `AvisoEntity` para rastrear o ciclo de vida da entidade, garantindo que o negócio saiba exatamente quando cada aviso foi modificado.

#### 2. Soft Delete
* A exclusão de um aviso (`DELETE /avisos/{id}`) foi implementada como **exclusão lógica**, onde o status `Ativo` da entidade é marcado como falso.
* Todas as consultas (`GET`) foram ajustadas para filtrar e retornar **apenas avisos ativos**.

#### 3. Validações Essenciais (Fluent Validation)
* **IDs Válidos:** O Validador de busca previne que requisições com `Id <= 0` passem para o *Handler*.
* **Criação:** Títulos e Mensagens são obrigatórios (`NotEmpty()`).
* **Atualização:** O Validador garante que o campo **`Mensagem`** não seja nulo ou vazio durante a edição.
  
---

### ⚙️ Configuração e Execução Local

Para rodar o desafio, é necessário garantir que a infraestrutura do banco de dados esteja ativa via Docker antes de iniciar a aplicação principal.

#### 1. 🐳 Infraestrutura (Docker Compose)

O arquivo `docker-compose.yml` fornecido configura um container PostgreSQL com as seguintes credenciais de conexão: `POSTGRES_USER: admin`, `POSTGRES_PASSWORD: admin`, e `POSTGRES_DB: db_Aviso`.

**Comando para Iniciar o Banco:**

1.  Certifique-se de ter o **Docker** instalado e em execução.
2.  Navegue até o diretório onde o arquivo `docker-compose.yml` está localizado.
3.  Execute o comando para iniciar o serviço do banco de dados em segundo plano:

    ```bash
    docker-compose up -d
    ```
    *O banco de dados estará acessível em `localhost:5432`.*

#### 2. 🚀 Execução da Aplicação (API & DBUP)

O projeto de infraestrutura com o DBUP está configurado para ser executado **automaticamente** junto com a API. Ele aplicará as migrações e o versionamento do banco de dados assim que a aplicação for iniciada.

1.  **Garantir o Banco:** Confirme que o serviço `db` do Docker está rodando (Passo 1).
2.  **Rodar a API:** Inicie a solução no Visual Studio ou via CLI:
3.  **Teste:** Após o carregamento, acesse o **Swagger UI** em `https://localhost:[Porta]/swagger` para interagir com os *endpoints*.
