using System.Data.SQLite;
using TaskFacil.Models;

namespace TaskFacil.Data
{
    public class DatabaseManager
    {
        private readonly string _connectionString;
        private const string DatabaseFileName = "taskfacil.db";

        public DatabaseManager()
        {
            string dbPath = Path.Combine(Environment.CurrentDirectory, DatabaseFileName);
            _connectionString = $"Data Source={dbPath};Version=3;";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string createTableQuery = @"
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
                );";

            using var command = new SQLiteCommand(createTableQuery, connection);
            command.ExecuteNonQuery();
        }

        public List<Tarefa> GetAllTarefas()
        {
            var tarefas = new List<Tarefa>();

            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM Tarefas ORDER BY DataVencimento";
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                tarefas.Add(CreateTarefaFromReader(reader));
            }

            return tarefas;
        }

        public Tarefa? GetTarefaById(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM Tarefas WHERE Id = @id";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return CreateTarefaFromReader(reader);
            }

            return null;
        }

        public int AddTarefa(Tarefa tarefa)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string query = @"
                INSERT INTO Tarefas (Titulo, Descricao, DataVencimento, Prioridade, Categoria, Status, DataCriacao, DataConclusao)
                VALUES (@titulo, @descricao, @dataVencimento, @prioridade, @categoria, @status, @dataCriacao, @dataConclusao);
                SELECT last_insert_rowid();";

            using var command = new SQLiteCommand(query, connection);
            AddParametersToCommand(command, tarefa);

            var result = command.ExecuteScalar();
            return Convert.ToInt32(result);
        }

        public bool UpdateTarefa(Tarefa tarefa)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string query = @"
                UPDATE Tarefas 
                SET Titulo = @titulo, 
                    Descricao = @descricao, 
                    DataVencimento = @dataVencimento, 
                    Prioridade = @prioridade, 
                    Categoria = @categoria, 
                    Status = @status, 
                    DataConclusao = @dataConclusao
                WHERE Id = @id";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", tarefa.Id);
            AddParametersToCommand(command, tarefa);

            return command.ExecuteNonQuery() > 0;
        }

        public bool DeleteTarefa(int id)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string query = "DELETE FROM Tarefas WHERE Id = @id";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            return command.ExecuteNonQuery() > 0;
        }

        public List<Tarefa> SearchTarefas(string searchTerm)
        {
            var tarefas = new List<Tarefa>();

            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string query = @"
                SELECT * FROM Tarefas 
                WHERE Titulo LIKE @searchTerm 
                   OR Descricao LIKE @searchTerm 
                   OR Categoria LIKE @searchTerm
                ORDER BY DataVencimento";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                tarefas.Add(CreateTarefaFromReader(reader));
            }

            return tarefas;
        }

        public List<Tarefa> GetTarefasByStatus(StatusTarefa status)
        {
            var tarefas = new List<Tarefa>();

            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM Tarefas WHERE Status = @status ORDER BY DataVencimento";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@status", (int)status);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                tarefas.Add(CreateTarefaFromReader(reader));
            }

            return tarefas;
        }

        public List<Tarefa> GetTarefasByCategoria(string categoria)
        {
            var tarefas = new List<Tarefa>();

            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM Tarefas WHERE Categoria = @categoria ORDER BY DataVencimento";
            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@categoria", categoria);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                tarefas.Add(CreateTarefaFromReader(reader));
            }

            return tarefas;
        }

        public List<Tarefa> GetTarefasVencendoEm(int dias)
        {
            var tarefas = new List<Tarefa>();
            var dataLimite = DateTime.Now.AddDays(dias).ToString("yyyy-MM-dd");

            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            string query = @"
                SELECT * FROM Tarefas 
                WHERE DATE(DataVencimento) <= @dataLimite 
                  AND Status != @statusConcluida
                ORDER BY DataVencimento";

            using var command = new SQLiteCommand(query, connection);
            command.Parameters.AddWithValue("@dataLimite", dataLimite);
            command.Parameters.AddWithValue("@statusConcluida", (int)StatusTarefa.Concluida);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                tarefas.Add(CreateTarefaFromReader(reader));
            }

            return tarefas;
        }

        private Tarefa CreateTarefaFromReader(SQLiteDataReader reader)
        {
            return new Tarefa
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                Descricao = reader.IsDBNull(reader.GetOrdinal("Descricao")) ? string.Empty : reader.GetString(reader.GetOrdinal("Descricao")),
                DataVencimento = DateTime.Parse(reader.GetString(reader.GetOrdinal("DataVencimento"))),
                Prioridade = (PrioridadeTarefa)reader.GetInt32(reader.GetOrdinal("Prioridade")),
                Categoria = reader.IsDBNull(reader.GetOrdinal("Categoria")) ? string.Empty : reader.GetString(reader.GetOrdinal("Categoria")),
                Status = (StatusTarefa)reader.GetInt32(reader.GetOrdinal("Status")),
                DataCriacao = DateTime.Parse(reader.GetString(reader.GetOrdinal("DataCriacao"))),
                DataConclusao = reader.IsDBNull(reader.GetOrdinal("DataConclusao")) ? null : DateTime.Parse(reader.GetString(reader.GetOrdinal("DataConclusao")))
            };
        }

        private void AddParametersToCommand(SQLiteCommand command, Tarefa tarefa)
        {
            command.Parameters.AddWithValue("@titulo", tarefa.Titulo);
            command.Parameters.AddWithValue("@descricao", tarefa.Descricao);
            command.Parameters.AddWithValue("@dataVencimento", tarefa.DataVencimento.ToString("yyyy-MM-dd HH:mm:ss"));
            command.Parameters.AddWithValue("@prioridade", (int)tarefa.Prioridade);
            command.Parameters.AddWithValue("@categoria", tarefa.Categoria);
            command.Parameters.AddWithValue("@status", (int)tarefa.Status);
            command.Parameters.AddWithValue("@dataCriacao", tarefa.DataCriacao.ToString("yyyy-MM-dd HH:mm:ss"));
            command.Parameters.AddWithValue("@dataConclusao", 
                tarefa.DataConclusao?.ToString("yyyy-MM-dd HH:mm:ss") ?? (object)DBNull.Value);
        }
    }
}
