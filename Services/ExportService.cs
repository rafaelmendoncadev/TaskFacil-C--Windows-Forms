using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using TaskFacil.Models;

namespace TaskFacil.Services
{
    public class ExportService
    {
        public bool ExportTarefasToCSV(List<Tarefa> tarefas, string filePath)
        {
            try
            {
                using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
                using var csv = new CsvWriter(writer, CultureInfo.CurrentCulture);
                
                // Configurar o CSV para usar o separador ponto e vírgula
                csv.Context.Configuration.Delimiter = ";";
                
                // Escrever os dados
                csv.WriteRecords(tarefas.Select(t => new TarefaExportModel
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descricao = t.Descricao,
                    DataVencimento = t.DataVencimento.ToString("dd/MM/yyyy HH:mm"),
                    Prioridade = t.PrioridadeTexto,
                    Categoria = t.Categoria,
                    Status = t.StatusTexto,
                    DataCriacao = t.DataCriacao.ToString("dd/MM/yyyy HH:mm"),
                    DataConclusao = t.DataConclusao?.ToString("dd/MM/yyyy HH:mm") ?? "",
                    EstaVencida = t.EstaVencida ? "Sim" : "Não"
                }));

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao exportar tarefas para CSV: {ex.Message}", ex);
            }
        }

        public bool ExportTarefasToText(List<Tarefa> tarefas, string filePath)
        {
            try
            {
                using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
                
                writer.WriteLine("=== RELATÓRIO DE TAREFAS ===");
                writer.WriteLine($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm}");
                writer.WriteLine($"Total de tarefas: {tarefas.Count}");
                writer.WriteLine();

                var statusGroups = tarefas.GroupBy(t => t.Status);
                
                foreach (var group in statusGroups)
                {
                    writer.WriteLine($"=== {group.Key.ToString().ToUpper()} ({group.Count()}) ===");
                    writer.WriteLine();

                    foreach (var tarefa in group.OrderBy(t => t.DataVencimento))
                    {
                        writer.WriteLine($"ID: {tarefa.Id}");
                        writer.WriteLine($"Título: {tarefa.Titulo}");
                        writer.WriteLine($"Descrição: {tarefa.Descricao}");
                        writer.WriteLine($"Categoria: {tarefa.Categoria}");
                        writer.WriteLine($"Prioridade: {tarefa.PrioridadeTexto}");
                        writer.WriteLine($"Data de Vencimento: {tarefa.DataVencimento:dd/MM/yyyy HH:mm}");
                        writer.WriteLine($"Data de Criação: {tarefa.DataCriacao:dd/MM/yyyy HH:mm}");
                        
                        if (tarefa.DataConclusao.HasValue)
                        {
                            writer.WriteLine($"Data de Conclusão: {tarefa.DataConclusao:dd/MM/yyyy HH:mm}");
                        }
                        
                        if (tarefa.EstaVencida)
                        {
                            writer.WriteLine("*** TAREFA VENCIDA ***");
                        }
                        
                        writer.WriteLine(new string('-', 50));
                        writer.WriteLine();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao exportar tarefas para texto: {ex.Message}", ex);
            }
        }
    }

    public class TarefaExportModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string DataVencimento { get; set; } = string.Empty;
        public string Prioridade { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string DataCriacao { get; set; } = string.Empty;
        public string DataConclusao { get; set; } = string.Empty;
        public string EstaVencida { get; set; } = string.Empty;
    }
}
