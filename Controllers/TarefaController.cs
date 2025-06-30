using TaskFacil.Data;
using TaskFacil.Models;

namespace TaskFacil.Controllers
{
    public class TarefaController
    {
        private readonly DatabaseManager _databaseManager;

        public TarefaController()
        {
            _databaseManager = new DatabaseManager();
        }

        public List<Tarefa> GetAllTarefas()
        {
            return _databaseManager.GetAllTarefas();
        }

        public Tarefa? GetTarefaById(int id)
        {
            return _databaseManager.GetTarefaById(id);
        }

        public bool CreateTarefa(Tarefa tarefa)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tarefa.Titulo))
                {
                    throw new ArgumentException("O título da tarefa é obrigatório.");
                }

                if (tarefa.DataVencimento < DateTime.Now.Date)
                {
                    throw new ArgumentException("A data de vencimento não pode ser anterior à data atual.");
                }

                tarefa.DataCriacao = DateTime.Now;
                var id = _databaseManager.AddTarefa(tarefa);
                tarefa.Id = id;
                return id > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar tarefa: {ex.Message}", ex);
            }
        }

        public bool UpdateTarefa(Tarefa tarefa)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tarefa.Titulo))
                {
                    throw new ArgumentException("O título da tarefa é obrigatório.");
                }

                if (tarefa.DataVencimento < DateTime.Now.Date && tarefa.Status != StatusTarefa.Concluida)
                {
                    throw new ArgumentException("A data de vencimento não pode ser anterior à data atual para tarefas não concluídas.");
                }

                return _databaseManager.UpdateTarefa(tarefa);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar tarefa: {ex.Message}", ex);
            }
        }

        public bool DeleteTarefa(int id)
        {
            try
            {
                return _databaseManager.DeleteTarefa(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir tarefa: {ex.Message}", ex);
            }
        }

        public bool MarcarTarefaComoConcluida(int id)
        {
            try
            {
                var tarefa = _databaseManager.GetTarefaById(id);
                if (tarefa == null)
                {
                    throw new ArgumentException("Tarefa não encontrada.");
                }

                tarefa.Status = StatusTarefa.Concluida;
                tarefa.DataConclusao = DateTime.Now;
                return _databaseManager.UpdateTarefa(tarefa);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao marcar tarefa como concluída: {ex.Message}", ex);
            }
        }

        public bool MarcarTarefaComoPendente(int id)
        {
            try
            {
                var tarefa = _databaseManager.GetTarefaById(id);
                if (tarefa == null)
                {
                    throw new ArgumentException("Tarefa não encontrada.");
                }

                tarefa.Status = StatusTarefa.Pendente;
                tarefa.DataConclusao = null;
                return _databaseManager.UpdateTarefa(tarefa);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao marcar tarefa como pendente: {ex.Message}", ex);
            }
        }

        public List<Tarefa> SearchTarefas(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return GetAllTarefas();
                }

                return _databaseManager.SearchTarefas(searchTerm);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar tarefas: {ex.Message}", ex);
            }
        }

        public List<Tarefa> GetTarefasByStatus(StatusTarefa status)
        {
            try
            {
                return _databaseManager.GetTarefasByStatus(status);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar tarefas por status: {ex.Message}", ex);
            }
        }

        public List<Tarefa> GetTarefasByCategoria(string categoria)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(categoria))
                {
                    return GetAllTarefas();
                }

                return _databaseManager.GetTarefasByCategoria(categoria);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar tarefas por categoria: {ex.Message}", ex);
            }
        }

        public List<Tarefa> GetTarefasVencidas()
        {
            try
            {
                return GetAllTarefas()
                    .Where(t => t.EstaVencida)
                    .OrderBy(t => t.DataVencimento)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar tarefas vencidas: {ex.Message}", ex);
            }
        }

        public List<Tarefa> GetTarefasProximasDoVencimento()
        {
            try
            {
                return GetAllTarefas()
                    .Where(t => t.EstaProximaDoVencimento)
                    .OrderBy(t => t.DataVencimento)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar tarefas próximas do vencimento: {ex.Message}", ex);
            }
        }

        public List<Tarefa> GetTarefasVencendoEm(int dias)
        {
            try
            {
                return _databaseManager.GetTarefasVencendoEm(dias);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar tarefas vencendo em {dias} dias: {ex.Message}", ex);
            }
        }

        public List<Tarefa> GetTarefasOrdenadas(string orderBy, bool ascending = true)
        {
            try
            {
                var tarefas = GetAllTarefas();

                return orderBy.ToLower() switch
                {
                    "titulo" => ascending ? tarefas.OrderBy(t => t.Titulo).ToList() : tarefas.OrderByDescending(t => t.Titulo).ToList(),
                    "datavencimento" => ascending ? tarefas.OrderBy(t => t.DataVencimento).ToList() : tarefas.OrderByDescending(t => t.DataVencimento).ToList(),
                    "prioridade" => ascending ? tarefas.OrderBy(t => t.Prioridade).ToList() : tarefas.OrderByDescending(t => t.Prioridade).ToList(),
                    "status" => ascending ? tarefas.OrderBy(t => t.Status).ToList() : tarefas.OrderByDescending(t => t.Status).ToList(),
                    "categoria" => ascending ? tarefas.OrderBy(t => t.Categoria).ToList() : tarefas.OrderByDescending(t => t.Categoria).ToList(),
                    "datacriacao" => ascending ? tarefas.OrderBy(t => t.DataCriacao).ToList() : tarefas.OrderByDescending(t => t.DataCriacao).ToList(),
                    _ => tarefas.OrderBy(t => t.DataVencimento).ToList()
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao ordenar tarefas: {ex.Message}", ex);
            }
        }

        public List<string> GetCategorias()
        {
            try
            {
                return GetAllTarefas()
                    .Where(t => !string.IsNullOrWhiteSpace(t.Categoria))
                    .Select(t => t.Categoria)
                    .Distinct()
                    .OrderBy(c => c)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar categorias: {ex.Message}", ex);
            }
        }

        public Dictionary<StatusTarefa, int> GetEstatisticasPorStatus()
        {
            try
            {
                var tarefas = GetAllTarefas();
                return Enum.GetValues<StatusTarefa>()
                    .ToDictionary(status => status, status => tarefas.Count(t => t.Status == status));
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao gerar estatísticas: {ex.Message}", ex);
            }
        }
    }
}
