# Phonebook API

API para gerenciamento de contatos com autenticação, criação de usuários e contatos. Utiliza armazenamento `InMemory` para persistência temporária e RabbitMQ com MassTransit para processamento em fila.

## Funcionalidades

- **Autenticação**: Fluxo de autorização para controle de acesso.
- **Gerenciamento de Usuários**: Criação e autenticação de usuários.
- **Gerenciamento de Contatos**: Criação e listagem de contatos por usuário.
- **Processamento em Fila**: Envio de dados de contatos para filas no RabbitMQ usando MassTransit.

## Tecnologias Utilizadas

- **ASP.NET Core** para construção da API
- **InMemory Database** para armazenamento temporário de dados
- **RabbitMQ** para filas de mensagens
- **MassTransit** como biblioteca de integração com RabbitMQ
