# InvestControl 📈

Sistema Web completo para controle de investimentos de clientes, desenvolvido como parte de um desafio técnico para vaga de Engenheiro de software Júnior no Itaú.

Este projeto abrange o desenvolvimento de uma **API RESTful com ASP.NET Core**, um **front-end moderno com Blazor WebAssembly e MudBlazor**, e integração com um **banco de dados MySQL**. O sistema permite consulta detalhada das posições de investimento de um cliente, incluindo corretagens, preço médio, lucro/prejuízo e muito mais.

---

## 📊 Funcionalidades da Etapa 3 do Desafio

A partir do ID de um usuário, o sistema é capaz de:

- Consultar **Total Investido por Ativo**
- Consultar **Posição Global do Investidor** (valor investido, valor atual, lucro/prejuízo)
- Exibir **Total de Corretagem paga** pelo cliente
- Consultar **Preço Médio por Ativo**
- Exibir **dados do cliente** (nome, e-mail, porcentagem de corretagem)

Cada funcionalidade está implementada tanto na API quanto exibida de forma visual no front-end.

---

## 📚 Tecnologias Utilizadas

### Back-end (API REST)

- ASP.NET Core 8
- Entity Framework Core + Pomelo MySQL
- AutoMapper (Mapeamento de DTOs)
- Swagger para testes
- Configuração de CORS

### Front-end

- Blazor WebAssembly (.NET 8)
- MudBlazor (UI components)
- HttpClient para integração com API
- Tema personalizado com as **cores do Itaú** (laranja e azul)

### Banco de Dados

- MySQL
- Entidades: Usuário, Ativo, Operação, Posição

---

## 🗓️ Endpoints Implementados

| Recurso                 | Método | Endpoint                            |
| ----------------------- | ------ | ----------------------------------- |
| Obter usuário por ID    | GET    | `/api/usuarios/{id}`                |
| Total de corretagem     | GET    | `/api/operacoes/corretagem/{id}`    |
| Posições por cliente    | GET    | `/api/posicoes/usuario/{id}`        |
| Posição global          | GET    | `/api/posicoes/posicao-global/{id}` |
| Preços médios por ativo | GET    | `/api/posicoes/precos-medios/{id}`  |

---

## 🚀 Como Executar o Projeto Localmente

### ✍️ Clonar o repositório

```bash
git clone https://github.com/seu-usuario/InvestControl.git
cd InvestControl
```

### ⚙️ Configurar a string de conexão

No projeto `InvestControl.API`, arquivo `appsettings.json`:

```json
"ConnectionStrings": {
  "MySqlConnection": "server=localhost;port=3306;database=investcontrol;user=root;password=sua_senha"
}
```

### 📁 Criar banco de dados

Use o MySQL Workbench ou outro cliente para criar o schema `investcontrol`:

```sql
CREATE DATABASE investcontrol;
```


### ▶️ Executar a API

```bash
cd InvestControl.API
dotnet run
```

Acesse: [https://localhost:5024/swagger](https://localhost:5024/swagger)

### 🌐 Executar o Front-end

```bash
cd ../InvestControl.Web
dotnet run
```

Acesse: [https://localhost:7283](https://localhost:7283)

---

## 📅 Layout do Dashboard

- Input para buscar cliente por ID
- Cards com visual escuro e cores Itaú:
  - Azul escuro de fundo: `#004481`
  - Laranja borda: `#ff6600`
  - Letras brancas
- Todos os dados carregados são atualizados dinamicamente após a busca

---

## 👨‍💼 Autor

**Victor Hugo**Desenvolvedor .NET Jr.
