# Guia de Instalação e Uso - TaskFácil

## 🚀 Instalação Rápida

### Pré-requisitos
- Windows 10/11
- .NET 8.0 SDK ([Download aqui](https://dotnet.microsoft.com/download/dotnet/8.0))

### Passos para Executar

1. **Abra o PowerShell** como administrador
2. **Navegue até o diretório** do projeto:
   ```powershell
   cd "c:\Users\Rafael\OneDrive\Projetos em C#\TaskFacil"
   ```

3. **Execute o projeto**:
   ```powershell
   dotnet run
   ```

**Ou simplesmente**:
```powershell
start dotnet run
```

## 🎯 Primeiro Uso

### 1. Tela Principal
Quando abrir o TaskFácil, você verá:
- **Área de filtros** no topo
- **Botões de ação** à direita
- **Lista de tarefas** no centro
- **Estatísticas** na parte inferior

### 2. Criando sua Primeira Tarefa
1. Clique em **"Nova Tarefa"** (botão verde)
2. Preencha:
   - **Título**: Nome da tarefa (obrigatório)
   - **Descrição**: Detalhes da tarefa
   - **Data de Vencimento**: Quando deve ser concluída
   - **Prioridade**: Baixa, Média, Alta ou Urgente
   - **Categoria**: Ex: Trabalho, Pessoal, Estudos
   - **Status**: Pendente, Em Andamento, etc.
3. Clique em **"Salvar"**

### 3. Gerenciando Tarefas
- **Editar**: Selecione uma tarefa e clique em "Editar"
- **Excluir**: Selecione uma tarefa e clique em "Excluir"
- **Concluir**: Selecione uma tarefa e clique em "Concluir"
- **Buscar**: Digite no campo de busca
- **Filtrar**: Use os filtros de Status e Categoria

## 🔍 Recursos Avançados

### Filtros e Busca
- **Busca por texto**: Procura em título, descrição e categoria
- **Filtro por Status**: Mostra apenas tarefas do status selecionado
- **Filtro por Categoria**: Mostra apenas tarefas da categoria selecionada
- **Ordenação**: Organize por data, título, prioridade, etc.

### Notificações
O sistema automaticamente notifica sobre:
- ⚠️ **Tarefas vencidas**
- 🔔 **Tarefas próximas do vencimento** (2 dias)

### Exportação
1. Clique em **"Exportar"**
2. Escolha o formato:
   - **CSV**: Para Excel/planilhas
   - **TXT**: Relatório formatado
3. Salve onde desejar

## 🎨 Interface Visual

### Cores dos Status
- 🟡 **Amarelo**: Tarefas próximas do vencimento
- 🔴 **Vermelho**: Tarefas vencidas
- 🟢 **Verde**: Tarefas concluídas
- ⚪ **Branco**: Tarefas normais

### Prioridades
- 🟢 **Verde**: Baixa
- 🟡 **Amarelo**: Média
- 🟠 **Laranja**: Alta
- 🔴 **Vermelho**: Urgente

## ⌨️ Atalhos do Teclado

- **Ctrl + N**: Nova tarefa
- **Ctrl + E**: Exportar
- **Alt + F4**: Sair
- **Enter**: Salvar (quando em formulário)
- **Escape**: Cancelar (quando em formulário)

## 📁 Estrutura de Arquivos

```
TaskFacil/
├── taskfacil.db          # Banco de dados (criado automaticamente)
├── TaskFacil.exe         # Executável (após build)
├── bin/                  # Arquivos compilados
├── Documentation/        # Documentação adicional
└── README.md            # Este arquivo
```

### Localização do Banco de Dados
O arquivo `taskfacil.db` é criado no mesmo diretório do executável.

**Para backup**: Copie o arquivo `taskfacil.db`
**Para restaurar**: Substitua o arquivo `taskfacil.db`

## 🔧 Solução de Problemas

### Erro: "dotnet não é reconhecido"
**Solução**: Instale o .NET 8.0 SDK

### Erro: "Acesso negado ao banco"
**Solução**: Execute como administrador ou mude as permissões da pasta

### Tarefas não aparecem
**Solução**: 
1. Clique em "Atualizar"
2. Verifique se há filtros aplicados
3. Limpe o campo de busca

### Notificações não funcionam
**Solução**: 
- Mantenha o programa aberto
- Verifique se há tarefas vencidas ou próximas do vencimento

## 📊 Dicas de Uso

### Para Máxima Produtividade
1. **Use categorias consistentes**: Trabalho, Pessoal, Estudos, etc.
2. **Defina prioridades reais**: Reserve "Urgente" para emergências
3. **Seja específico nos títulos**: "Relatório mensal - Janeiro" em vez de "Relatório"
4. **Use descrições detalhadas**: Inclua contexto e próximos passos
5. **Revise semanalmente**: Exporte relatórios para análise

### Categorias Sugeridas
- 💼 **Trabalho**: Tarefas profissionais
- 👨‍👩‍👧‍👦 **Pessoal**: Família e amigos
- 📚 **Estudos**: Cursos e aprendizado
- 🏠 **Casa**: Tarefas domésticas
- 🏥 **Saúde**: Consultas e exercícios
- 🛒 **Compras**: Itens para comprar
- 💰 **Financeiro**: Contas e pagamentos

## 🆘 Suporte

### Problemas Comuns
- **Erro de compilação**: Verifique se o .NET 8.0 está instalado
- **Banco corrompido**: Delete `taskfacil.db` (perderá dados) ou restaure backup
- **Interface lenta**: Reinicie o programa

### Backup Recomendado
- **Frequência**: Semanal
- **Método**: Copiar arquivo `taskfacil.db`
- **Alternativa**: Exportar para CSV regularmente

---

## 🎉 Pronto para Usar!

O TaskFácil está pronto para ajudar você a organizar suas tarefas e aumentar sua produtividade!

**Comando para executar**:
```powershell
cd "c:\Users\Rafael\OneDrive\Projetos em C#\TaskFacil"
dotnet run
```

Ou execute o arquivo .exe gerado na pasta `bin\Debug\net8.0-windows\`
