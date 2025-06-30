# TaskFácil - Sistema de Controle de Tarefas

Um sistema completo de gerenciamento de tarefas desenvolvido em C# com Windows Forms, seguindo o padrão arquitetural MVC (Model-View-Controller) e utilizando SQLite como banco de dados.

## 🚀 Funcionalidades

### Gerenciamento de Tarefas
- ✅ **Criar** novas tarefas com informações completas
- ✅ **Editar** tarefas existentes
- ✅ **Excluir** tarefas desnecessárias
- ✅ **Marcar** tarefas como concluídas ou pendentes

### Informações da Tarefa
- **Título** (obrigatório)
- **Descrição** detalhada
- **Data de vencimento** com hora
- **Prioridade** (Baixa, Média, Alta, Urgente)
- **Categoria** personalizável
- **Status** (Pendente, Em Andamento, Concluída, Cancelada)

### Filtros e Busca
- 🔍 **Busca** por palavra-chave (título, descrição, categoria)
- 📊 **Filtros** por status, categoria
- 📋 **Ordenação** por data, título, prioridade, status, categoria
- 🎯 **Visualização** de tarefas vencidas e próximas do vencimento

### Notificações e Alertas
- 🔔 **Notificações automáticas** para tarefas vencidas
- ⏰ **Alertas** para tarefas próximas do vencimento (2 dias)
- 📈 **Estatísticas** em tempo real (pendentes, concluídas, vencidas)

### Exportação
- 📄 **Exportar para CSV** para análise em planilhas
- 📝 **Exportar para TXT** como relatório formatado

## 🏗️ Arquitetura

O projeto segue o padrão **MVC (Model-View-Controller)**:

```
TaskFacil/
├── Models/           # Modelos de dados
│   └── Tarefa.cs    # Modelo da tarefa
├── Views/           # Interface do usuário
│   ├── MainForm.cs  # Tela principal
│   └── TarefaForm.cs # Formulário de tarefa
├── Controllers/     # Lógica de negócio
│   └── TarefaController.cs
├── Data/           # Acesso a dados
│   └── DatabaseManager.cs
├── Services/       # Serviços auxiliares
│   ├── ExportService.cs
│   └── NotificationService.cs
└── Utilities/      # Utilitários
    └── Extensions.cs
```

## 🗄️ Banco de Dados

O sistema utiliza **SQLite** com a seguinte estrutura:

```sql
CREATE TABLE Tarefas (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Titulo TEXT NOT NULL,
    Descricao TEXT,
    DataVencimento TEXT NOT NULL,
    Prioridade INTEGER NOT NULL,
    Categoria TEXT,
    Status INTEGER NOT NULL,
    DataCriacao TEXT NOT NULL,
    DataConclusao TEXT
);
```

## 🛠️ Tecnologias Utilizadas

- **C# .NET 8.0** - Linguagem e framework
- **Windows Forms** - Interface gráfica
- **SQLite** - Banco de dados local
- **System.Data.SQLite** - Provider para SQLite
- **CsvHelper** - Biblioteca para exportação CSV

## 📦 Instalação e Execução

### Pré-requisitos
- .NET 8.0 SDK ou superior
- Windows 10/11

### Passos para executar:

1. **Clone ou baixe o projeto**
2. **Navegue até o diretório do projeto**
   ```bash
   cd TaskFacil
   ```

3. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

4. **Compile o projeto**
   ```bash
   dotnet build
   ```

5. **Execute a aplicação**
   ```bash
   dotnet run
   ```

## 🎯 Como Usar

### 1. Tela Principal
- **Visualize** todas as suas tarefas em uma tabela organizada
- **Use os filtros** para encontrar tarefas específicas
- **Monitore estatísticas** na parte inferior da tela

### 2. Criando uma Nova Tarefa
- Clique em **"Nova Tarefa"** ou pressione `Ctrl+N`
- Preencha as informações obrigatórias (título, data de vencimento, prioridade)
- Adicione descrição e categoria se desejar
- Clique em **"Salvar"**

### 3. Editando uma Tarefa
- **Selecione** uma tarefa na lista
- Clique em **"Editar"** ou **duplo-clique** na tarefa
- Modifique as informações desejadas
- Clique em **"Salvar"**

### 4. Gerenciando Status
- **Selecione** uma tarefa
- Clique em **"Concluir"** para marcar como concluída
- Ou **"Marcar como Pendente"** para reverter

### 5. Exportando Dados
- Clique em **"Exportar"** ou pressione `Ctrl+E`
- Escolha o formato (CSV ou TXT)
- Selecione o local para salvar

## 🔧 Recursos Avançados

### Notificações Inteligentes
- O sistema verifica automaticamente a cada 5 minutos
- Notifica sobre tarefas vencidas
- Alerta sobre tarefas próximas do vencimento

### Filtros Dinâmicos
- **Busca em tempo real** enquanto você digita
- **Filtros combinados** (status + categoria + busca)
- **Ordenação flexível** por qualquer campo

### Interface Responsiva
- **Cores indicativas** para status e prioridades
- **Destaque visual** para tarefas vencidas
- **Estatísticas em tempo real**

## 🎨 Personalização

### Adicionando Novas Categorias
- As categorias são criadas automaticamente quando você digita
- Categorias existentes aparecem na lista suspensa
- Categorias padrão: Trabalho, Pessoal, Estudos, Casa, Saúde, Compras

### Modificando Prioridades
- Edite o enum `PrioridadeTarefa` em `Models/Tarefa.cs`
- Atualize o método `GetPrioridadeText()` em `TarefaForm.cs`
- Ajuste as cores em `ColorHelper.GetPriorityColor()`

## 🔒 Segurança e Backup

- **Banco de dados local**: `taskfacil.db` no diretório da aplicação
- **Backup manual**: Copie o arquivo `taskfacil.db`
- **Exportação regular**: Use a função de exportar para backup em CSV/TXT

## 🐛 Solução de Problemas

### Banco de dados não encontrado
- O banco é criado automaticamente na primeira execução
- Verifique as permissões do diretório da aplicação

### Erro ao salvar tarefa
- Verifique se o título está preenchido
- Confirme se a data de vencimento é válida

### Notificações não funcionam
- As notificações são exibidas apenas quando a aplicação está em execução
- Verifique se há tarefas vencidas ou próximas do vencimento

## 📝 Licença

Este projeto é de código aberto e está disponível para uso educacional e pessoal.

## 🤝 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para:
- Reportar bugs
- Sugerir melhorias
- Adicionar novas funcionalidades
- Melhorar a documentação

---

**TaskFácil** - Mantenha suas tarefas organizadas e nunca mais perca um prazo! 🎯
