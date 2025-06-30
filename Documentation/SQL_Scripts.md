# Scripts SQL e Exemplos - TaskFácil

## Estrutura do Banco de Dados

### Tabela Tarefas
```sql
CREATE TABLE IF NOT EXISTS Tarefas (
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

### Índices para Performance
```sql
-- Índice para busca por status
CREATE INDEX IF NOT EXISTS idx_tarefas_status ON Tarefas(Status);

-- Índice para busca por data de vencimento
CREATE INDEX IF NOT EXISTS idx_tarefas_vencimento ON Tarefas(DataVencimento);

-- Índice para busca por categoria
CREATE INDEX IF NOT EXISTS idx_tarefas_categoria ON Tarefas(Categoria);

-- Índice composto para filtros combinados
CREATE INDEX IF NOT EXISTS idx_tarefas_status_vencimento ON Tarefas(Status, DataVencimento);
```

## Consultas Úteis

### Buscar tarefas vencidas
```sql
SELECT * FROM Tarefas 
WHERE DATE(DataVencimento) < DATE('now') 
  AND Status != 3  -- 3 = Concluída
ORDER BY DataVencimento;
```

### Buscar tarefas próximas do vencimento (próximos 2 dias)
```sql
SELECT * FROM Tarefas 
WHERE DATE(DataVencimento) BETWEEN DATE('now') AND DATE('now', '+2 days')
  AND Status != 3  -- 3 = Concluída
ORDER BY DataVencimento;
```

### Estatísticas por status
```sql
SELECT 
    CASE Status
        WHEN 1 THEN 'Pendente'
        WHEN 2 THEN 'Em Andamento'
        WHEN 3 THEN 'Concluída'
        WHEN 4 THEN 'Cancelada'
    END as StatusNome,
    COUNT(*) as Quantidade
FROM Tarefas 
GROUP BY Status;
```

### Tarefas por categoria
```sql
SELECT 
    COALESCE(Categoria, 'Sem Categoria') as Categoria,
    COUNT(*) as Total,
    SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) as Concluidas,
    SUM(CASE WHEN Status != 3 THEN 1 ELSE 0 END) as Pendentes
FROM Tarefas 
GROUP BY Categoria
ORDER BY Total DESC;
```

### Produtividade mensal
```sql
SELECT 
    strftime('%Y-%m', DataConclusao) as Mes,
    COUNT(*) as TarefasConcluidas
FROM Tarefas 
WHERE Status = 3 
  AND DataConclusao IS NOT NULL
GROUP BY strftime('%Y-%m', DataConclusao)
ORDER BY Mes DESC;
```

## Dados de Exemplo

### Inserir tarefas de exemplo
```sql
-- Tarefas de Trabalho
INSERT INTO Tarefas (Titulo, Descricao, DataVencimento, Prioridade, Categoria, Status, DataCriacao)
VALUES 
('Finalizar relatório mensal', 'Preparar relatório de vendas do mês atual', '2024-01-15 17:00:00', 3, 'Trabalho', 1, '2024-01-10 09:00:00'),
('Reunião com cliente', 'Apresentar proposta de novo projeto', '2024-01-12 14:30:00', 4, 'Trabalho', 2, '2024-01-08 10:00:00'),
('Atualizar sistema', 'Deploy da nova versão do sistema', '2024-01-20 09:00:00', 2, 'Trabalho', 1, '2024-01-05 08:30:00');

-- Tarefas Pessoais
INSERT INTO Tarefas (Titulo, Descricao, DataVencimento, Prioridade, Categoria, Status, DataCriacao)
VALUES 
('Consulta médica', 'Consulta de rotina com cardiologista', '2024-01-18 10:00:00', 3, 'Saúde', 1, '2024-01-03 12:00:00'),
('Comprar presente', 'Aniversário da Maria na próxima semana', '2024-01-25 18:00:00', 2, 'Pessoal', 1, '2024-01-11 15:00:00'),
('Lavar o carro', 'Carro precisa de uma boa lavagem', '2024-01-14 10:00:00', 1, 'Casa', 1, '2024-01-12 07:00:00');

-- Tarefas de Estudos
INSERT INTO Tarefas (Titulo, Descricao, DataVencimento, Prioridade, Categoria, Status, DataCriacao)
VALUES 
('Estudar para prova', 'Prova de C# na próxima semana', '2024-01-22 19:00:00', 4, 'Estudos', 1, '2024-01-10 20:00:00'),
('Fazer exercícios', 'Lista de exercícios de algoritmos', '2024-01-16 23:59:00', 2, 'Estudos', 1, '2024-01-11 14:00:00');

-- Tarefa já concluída
INSERT INTO Tarefas (Titulo, Descricao, DataVencimento, Prioridade, Categoria, Status, DataCriacao, DataConclusao)
VALUES 
('Backup do sistema', 'Fazer backup completo dos dados', '2024-01-05 18:00:00', 3, 'Trabalho', 3, '2024-01-04 09:00:00', '2024-01-05 16:30:00');
```

## Manutenção do Banco

### Limpar tarefas antigas concluídas (mais de 6 meses)
```sql
DELETE FROM Tarefas 
WHERE Status = 3 
  AND DataConclusao < DATE('now', '-6 months');
```

### Vacuum para otimizar o banco
```sql
VACUUM;
```

### Verificar integridade
```sql
PRAGMA integrity_check;
```

## Backup e Restauração

### Backup via linha de comando
```bash
# Criar backup
sqlite3 taskfacil.db ".backup backup_taskfacil.db"

# Restaurar backup
sqlite3 taskfacil_restored.db ".restore backup_taskfacil.db"
```

### Export para SQL
```bash
sqlite3 taskfacil.db ".dump" > taskfacil_backup.sql
```

### Import de SQL
```bash
sqlite3 taskfacil_new.db < taskfacil_backup.sql
```

## Configurações Recomendadas

### Pragmas para performance
```sql
PRAGMA journal_mode = WAL;
PRAGMA synchronous = NORMAL;
PRAGMA cache_size = 1000;
PRAGMA temp_store = memory;
```

### Configurar auto-vacuum
```sql
PRAGMA auto_vacuum = INCREMENTAL;
```

## Relatórios Úteis

### Relatório de produtividade semanal
```sql
SELECT 
    strftime('%Y-%W', DataConclusao) as Semana,
    COUNT(*) as TarefasConcluidas,
    AVG(julianday(DataConclusao) - julianday(DataCriacao)) as TempoMedio
FROM Tarefas 
WHERE Status = 3 
  AND DataConclusao IS NOT NULL
GROUP BY strftime('%Y-%W', DataConclusao)
ORDER BY Semana DESC
LIMIT 10;
```

### Tarefas mais atrasadas
```sql
SELECT 
    Titulo,
    Categoria,
    DataVencimento,
    (julianday('now') - julianday(DataVencimento)) as DiasAtraso
FROM Tarefas 
WHERE Status != 3 
  AND DATE(DataVencimento) < DATE('now')
ORDER BY DiasAtraso DESC;
```

### Análise de categorias mais produtivas
```sql
SELECT 
    Categoria,
    COUNT(*) as TotalTarefas,
    SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) as Concluidas,
    ROUND(
        (SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) * 100.0 / COUNT(*)), 2
    ) as TaxaConclusao
FROM Tarefas 
WHERE Categoria IS NOT NULL
GROUP BY Categoria
HAVING COUNT(*) >= 3
ORDER BY TaxaConclusao DESC;
```
