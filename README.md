# ??? Asset & Vulnerability Manager API

Uma API robusta desenvolvida em **.NET 8** para o gerenciamento de ativos de rede e monitoramento de vulnerabilidades. Este projeto foi focado em boas práticas de desenvolvimento e segurança de dados.

## ?? Tecnologias Utilizadas
* **C# / ASP.NET Core 8**
* **Entity Framework Core** (ORM)
* **SQLite** (Banco de Dados Local)
* **Swagger/OpenAPI** (Documentação e Testes)

## ?? Implementações de Segurança
Como estudante de **Segurança da Informação**, foquei em camadas de proteção essenciais:
* **Input Validation:** Uso de *Data Annotations* para impedir Injeção de Dados Inválidos e garantir formatos de IP corretos via Regex.
* **Sensitive Data Protection:** Configuração de `.gitignore` para impedir o vazamento de bancos de dados locais (`.db`) em repositórios públicos.
* **Architecture:** Separação clara entre Models, Data e Controllers para manter a integridade do código.

## ??? Como rodar o projeto
1. Clone o repositório:
   ```bash
   git clone [https://github.com/SEU_USUARIO/NOME_DO_REPO.git](https://github.com/SEU_USUARIO/NOME_DO_REPO.git)