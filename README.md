# back-community-report

Back-end para o sistema Community Report desenvolvido com C# e MongoDB.

## Stack Tecnológica

* **Framework:** .NET Core 3.0
* **Linguagem:** C#
* **Banco de Dados:** MongoDB
* **Arquitetura:** API RESTful com padrão de 3 camadas (Controller, BLL - Business Logic Layer, DAL - Data Access Layer).
* **Autenticação:** JWT (JSON Web Tokens).
* **Documentação da API:** Swagger (OpenAPI).
* **Containerização:** Docker.

## Pré-requisitos

* [.NET Core 3.0 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.0)
* [Docker](https://www.docker.com/products/docker-desktop)
* Um cliente de API como [Postman](https://www.postman.com/) ou [Insomnia](https://insomnia.rest/) para testar os endpoints.

## Como Executar o Projeto

### Usando Docker (Recomendado)

A forma mais simples de executar o projeto é utilizando Docker, pois ele já encapsula toda a configuração necessária.

1.  **Construa a imagem Docker:**
    No diretório raiz do projeto (onde o `Dockerfile` está localizado), execute o seguinte comando para construir a imagem:

    ```bash
    docker build -t community-report-backend .
    ```

2.  **Execute o container:**
    Para executar a aplicação, você precisará de uma instância do MongoDB rodando. Você pode usar o Docker Compose para subir ambos os serviços ou executar o MongoDB em um container separado.

    **Opção A: Rodando apenas a aplicação (assumindo um MongoDB externo)**

    Se você já tem um MongoDB acessível, pode rodar o container da aplicação e configurar a string de conexão via variáveis de ambiente.

    ```bash
    docker run -p 4100:4100 --name community-report-app community-report-backend
    ```

    A aplicação estará acessível em `http://localhost:4100`.

### Executando Localmente

1.  **Configure o Banco de Dados:**
    * Certifique-se de ter uma instância do MongoDB rodando.
    * Atualize a string de conexão no arquivo `appsettings.Development.json` para apontar para o seu banco de dados local:

    ```json
    {
      "MongoDB": {
        "ConnectionString": "mongodb://localhost:27017",
        "Database": "CommunityReportDB"
      }
    }
    ```

2.  **Restaure as dependências:**
    Navegue até o diretório raiz do projeto e execute:

    ```bash
    dotnet restore
    ```

3.  **Execute a aplicação:**

    ```bash
    dotnet run
    ```

4.  A aplicação será iniciada e estará escutando nas URLs definidas em `Properties/launchSettings.json`, geralmente `http://localhost:5000` e `https://localhost:5001`.

## Acesso à API e Documentação

Com a aplicação em execução, você pode acessar a documentação da API gerada pelo Swagger no seguinte endereço:

* `http://localhost:4100/swagger` (se estiver usando Docker)
* `http://localhost:5000/swagger` ou `https://localhost:5001/swagger` (se estiver executando localmente)

## Pontos Importantes

* **Seeding de Dados:** A aplicação possui um serviço de *seeding* (`SeedingService`) que popula o banco de dados com dados iniciais (como Prefeitura, Setores, Categorias e usuários de teste) quando a aplicação é iniciada nos ambientes de desenvolvimento e produção.
* **Autenticação:** A maioria dos endpoints requer um token JWT para acesso. Utilize os endpoints em `TokenPublicoController` e `TokenRestritoController` para obter tokens de autenticação.
* **Tratamento de Exceções:** A aplicação utiliza um *middleware* customizado para tratar exceções de forma centralizada, retornando respostas de erro padronizadas.
* **Logs:** Os logs da aplicação são gerenciados com NLog e salvos em arquivos de texto, conforme configurado no `nlog.config`.

## TODO

* [ ] Criar o docker-compose pra subir o back junto do mongodb.
* [ ] Atualizar o projeto para uma versão mais recente do .NET (e.g., .NET 8 ou a futura .NET 10).
* [ ] Criar um pipeline de CI/CD (Integração Contínua/Entrega Contínua) utilizando GitHub Actions ou outra ferramenta para automatizar os builds, testes e deploys da aplicação.
