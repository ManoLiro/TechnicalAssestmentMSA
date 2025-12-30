# Sistema de Gestão de Clientes - API

## 📝 Descrição

API REST para gestão de clientes desenvolvida como parte de um desafio técnico. O projeto implementa os princípios de **Clean Architecture**, **SOLID**, **Domain-Driven Design (DDD)** e **CQRS**.

## 🏗️ Arquitetura

O projeto está estruturado em 4 camadas principais:

### 📦 Camadas

- **TechnicalAssestmentMSA.Domain**: Entidades, Value Objects e regras de negócio
- **TechnicalAssestmentMSA.Application**: Casos de uso (Commands, Queries e Handlers)
- **TechnicalAssestmentMSA.Infrastructure**: Persistência de dados com NHibernate + SQLite
- **TechnicalAssestmentMSA.API**: Web API REST com endpoints HTTP
- **TechnicalAssestmentMSA.Teste**: Testes unitários com xUnit

## 🚀 Tecnologias Utilizadas

- **.NET 9.0**
- **ASP.NET Core Web API**
- **MediatR** (implementação do padrão CQRS)
- **NHibernate** (ORM)
- **FluentNHibernate** (mapeamento)
- **SQLite** (banco de dados em memória)
- **xUnit** (framework de testes)
- **Moq** (biblioteca de mocking)
- **Scalar/OpenAPI** (documentação da API)

## 📋 Funcionalidades

### Endpoints Disponíveis

#### Criar Cliente
```
POST /api/clientes
```
**Payload:**
```json
{
  "nomeFantasia": "Empresa Teste Ltda",
  "cnpj": "11.222.333/0001-81",
  "ativo": true
}
```
**Resposta:** `201 Created` com o ID do cliente criado

#### Consultar Cliente por ID
```
GET /api/clientes/{id}
```
**Resposta:** `200 OK` com os dados do cliente ou `404 Not Found`

## 🎯 Destaques da Implementação

### Domain Layer
- ✅ **Entidade Cliente** com invariantes protegidas
- ✅ **Value Object CNPJ** com validação completa (incluindo dígitos verificadores)
- ✅ Setters protegidos e encapsulamento adequado
- ✅ Métodos de comportamento do domínio (Ativar/Desativar)

### Application Layer
- ✅ **Padrão CQRS** implementado com MediatR
- ✅ **CriaClienteCommand** e **CriaClienteCommandHandler**
- ✅ **ObtemClientePorIdQuery** e **ObtemClientePorIdQueryHandler**
- ✅ Separação clara entre operações de leitura e escrita

### Infrastructure Layer
- ✅ **Pattern Repository** com interface no Application
- ✅ Implementação com **NHibernate + SQLite**
- ✅ **Unit of Work** para controle de transações
- ✅ Inversão de dependências respeitada

### Testes
- ✅ **36 testes unitários** implementados
- ✅ Cobertura dos cenários principais:
  - Criação de cliente com sucesso
  - Validação de CNPJ duplicado
  - Validação de dados obrigatórios
  - Validação de CNPJ inválido
  - Consulta por ID existente e inexistente
- ✅ Uso de **Mocks** para isolamento dos testes
- ✅ Testes de entidade e value object

## 🔧 Como Executar

### Pré-requisitos
- .NET 9.0 SDK instalado

### Passos

1. **Clone o repositório**
```bash
git clone <url-do-repositorio>
cd TechnicalAssestmentMSA
```

2. **Restaure as dependências**
```bash
dotnet restore
```

3. **Execute a aplicação**
```bash
dotnet run --project TechnicalAssestmentMSA.API
```

4. **Acesse a documentação Swagger**
```
https://localhost:<porta>/swagger
```

## 🧪 Executar os Testes

Para executar todos os testes unitários:

```bash
dotnet test
```

Para executar com mais detalhes:

```bash
dotnet test --verbosity normal
```

Para ver a cobertura de testes:

```bash
dotnet test /p:CollectCoverage=true
```

## 📊 Resultados dos Testes

```
Test summary: total: 36, failed: 0, succeeded: 36, skipped: 0
```

### Distribuição dos Testes
- **CriaClienteCommandHandler**: 8 testes
- **ObtemClientePorIdQueryHandler**: 4 testes
- **Cliente (Entidade)**: 12 testes
- **Cnpj (Value Object)**: 12 testes

## 🎨 Princípios Aplicados

### Clean Architecture
- ✅ Separação clara de responsabilidades
- ✅ Dependências apontam para o centro (Domain)
- ✅ Camadas independentes e testáveis

### SOLID
- ✅ **S**ingle Responsibility: Cada classe tem uma única responsabilidade
- ✅ **O**pen/Closed: Extensível via interfaces
- ✅ **L**iskov Substitution: Implementações respeitam contratos
- ✅ **I**nterface Segregation: Interfaces específicas e enxutas
- ✅ **D**ependency Inversion: Dependência de abstrações, não de implementações

### DDD
- ✅ Value Objects imutáveis (Cnpj)
- ✅ Entidades com identidade (Cliente)
- ✅ Linguagem ubíqua no código
- ✅ Regras de negócio no domínio

## 📝 Observações

- O banco de dados SQLite é criado automaticamente na primeira execução
- As tabelas são criadas/atualizadas automaticamente via SchemaUpdate
- Todo o código está em português conforme requisito do desafio
- A validação de CNPJ implementa o algoritmo completo de dígitos verificadores

## 👨‍💻 Autor

Desenvolvido como parte do desafio técnico para a vaga de Desenvolvedor .NET Pleno.
