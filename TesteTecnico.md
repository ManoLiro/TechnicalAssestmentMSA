# Desafio Técnico: Desenvolvedor(a) .NET Pleno

👋 Olá, candidato(a)! Seja muito bem-vindo(a) ao nosso desafio técnico.

Ficamos felizes com o seu interesse em fazer parte do nosso time! Este desafio foi pensado para simular um dia de trabalho conosco, permitindo que você demonstre suas habilidades em um ambiente prático e sem a pressão de um "live coding".

Nossa filosofia é baseada em **Clean Architecture**, princípios **SOLID**, **Domain-Driven Design (DDD)** e uma forte cultura de **testes automatizados**. Queremos ver como você estrutura seu pensamento para resolver um problema real, escrevendo um código que seja legível, manutenível e, claro, funcional.

**O mais importante:** não existe uma única resposta "certa". Estamos mais interessados em entender suas decisões de design e a qualidade do seu código.

## 🚀 O Cenário

Você foi encarregado(a) de iniciar uma nova API para o nosso sistema de Gestão de Clientes. Sua primeira tarefa é implementar a funcionalidade principal: o cadastro e a consulta de clientes.

## 🎯 O Desafio

Sua missão é construir uma "feature slice" (uma fatia vertical da funcionalidade) que permita criar um novo cliente e consultá-lo por ID.

### Requisitos Funcionais:

1.  **Criar um Cliente:** A API deve ter um endpoint `POST` para criar um novo cliente.
2.  **Consultar um Cliente:** A API deve ter um endpoint `GET` para consultar um cliente existente pelo seu ID.

### Requisitos Técnicos:

1.  **Estrutura do Projeto:**
    *   Crie uma solução .NET 9.
    *   Estruture o projeto seguindo os princípios da **Clean Architecture**. Sugerimos a seguinte separação:
        *   `SuaSolucao.Domain`: Onde viverão suas entidades e regras de negócio (ex: `Cliente`).
        *   `SuaSolucao.Application`: Onde ficarão os casos de uso (Commands, Queries, Handlers).
        *   `SuaSolucao.Infrastructure`: Onde ficará a implementação da persistência.
        *   `SuaSolucao.API`: O projeto Web API que expõe os endpoints.
        *   `SuaSolucao.Tests`: O projeto de testes unitários.

2.  **Domínio (Domain):**
    *   Crie a entidade `Cliente` com propriedades como `Id`, `NomeFantasia`, `Cnpj` e `Ativo`.
    *   O CNPJ deve ser um **Value Object** para garantir sua validação e integridade.
    *   A entidade `Cliente` deve proteger suas próprias regras (invariantes). Por exemplo, o nome não pode ser vazio.

3.  **Aplicação (Application):**
    *   Implemente o padrão **CQRS**.
    *   Crie um `CriaClienteCommand` para o caso de uso de criação.
    *   Crie um `ObtemClientePorIdQuery` para o caso de uso de consulta.
    *   Implemente os respectivos `Handlers` para cada operação.

4.  **Infraestrutura (Infrastructure):**
    *   Implemente uma abstração de repositório (`IClienteRepository`).
    *   Crie uma implementação **em memória** para este repositório.
    *   **Nota sobre ORM:** Em nosso projeto, utilizamos **NHibernate**. Se você se sentir à vontade para usar o NHibernate com um banco de dados em memória (como o SQLite), será um grande diferencial! No entanto, para este desafio, uma simples lista estática na implementação do repositório em memória é **totalmente suficiente**. O foco aqui é avaliar o design do padrão de repositório e a inversão de dependência.

5.  **Testes (Tests):**
    *   Utilize **xUnit** para escrever testes unitários para os `Handlers` da camada de Aplicação.
    *   **Cenários mínimos a serem testados:**
        *   `CriaClienteCommandHandler`:
            *   Deve criar um cliente com sucesso quando os dados são válidos.
            *   Deve retornar um erro (ou lançar uma exceção) se o CNPJ já existir.
            *   Deve retornar um erro se dados essenciais (como o nome) forem inválidos.
        *   `ObtemClientePorIdQueryHandler`:
            *   Deve retornar o cliente correto quando o ID existe.
            *   Deve retornar nulo (ou um resultado indicando "não encontrado") quando o ID não existe.

### O que será avaliado (Critérios de Sucesso):

*   **Funcionalidade (Obrigatório):** A solução deve compilar, os testes devem passar e a API deve funcionar conforme os requisitos.
*   **Qualidade e Legibilidade do Código (Obrigatório):** O código deve ser limpo, bem estruturado e fácil de entender. **Todo o código, incluindo nomes de variáveis, métodos e comentários, deve estar em português do Brasil.**
*   **Arquitetura e Design:** Aderência à Clean Architecture e aos princípios SOLID. A correta separação de responsabilidades entre as camadas é fundamental.
*   **Modelagem de Domínio:** Como você modelou a entidade `Cliente` e o Value Object `Cnpj`.
*   **Qualidade dos Testes:** A clareza, a cobertura e a relevância dos testes unitários.
*   **Commits (Diferencial):** A clareza e a organização da sua cronologia de commits no Git.

### Como Entregar:

1.  Crie um repositório **público** no GitHub para sua solução.
2.  Faça seus commits à medida que avança no desafio.
3.  Ao finalizar, envie o link do seu repositório público para o time de recrutamento.

Boa sorte e divirta-se! Estamos ansiosos para ver sua solução. 🚀