using TaskFacil.Controllers;
using TaskFacil.Models;

namespace TaskFacil.Services
{
    public class NotificationService
    {
        private readonly TarefaController _tarefaController;
        private readonly System.Windows.Forms.Timer _notificationTimer;

        public event EventHandler<NotificationEventArgs>? NotificationTriggered;

        public NotificationService(TarefaController tarefaController)
        {
            _tarefaController = tarefaController;
            _notificationTimer = new System.Windows.Forms.Timer();
            _notificationTimer.Interval = 300000; // 5 minutos
            _notificationTimer.Tick += CheckForNotifications;
        }

        public void StartNotificationService()
        {
            _notificationTimer.Start();
        }

        public void StopNotificationService()
        {
            _notificationTimer.Stop();
        }

        private void CheckForNotifications(object? sender, EventArgs e)
        {
            try
            {
                // Verificar tarefas vencidas
                var tarefasVencidas = _tarefaController.GetTarefasVencidas();
                
                if (tarefasVencidas.Any())
                {
                    var message = $"Você tem {tarefasVencidas.Count} tarefa(s) vencida(s)!";
                    var detailedMessage = string.Join("\n", tarefasVencidas.Take(3).Select(t => 
                        $"• {t.Titulo} (Vencimento: {t.DataVencimento:dd/MM/yyyy})"));
                    
                    if (tarefasVencidas.Count > 3)
                    {
                        detailedMessage += $"\n... e mais {tarefasVencidas.Count - 3} tarefa(s).";
                    }

                    NotificationTriggered?.Invoke(this, new NotificationEventArgs
                    {
                        Title = "Tarefas Vencidas",
                        Message = message,
                        DetailedMessage = detailedMessage,
                        NotificationType = NotificationType.TarefasVencidas,
                        Tarefas = tarefasVencidas
                    });
                }

                // Verificar tarefas próximas do vencimento
                var tarefasProximas = _tarefaController.GetTarefasProximasDoVencimento();
                
                if (tarefasProximas.Any())
                {
                    var message = $"Você tem {tarefasProximas.Count} tarefa(s) vencendo nos próximos 2 dias!";
                    var detailedMessage = string.Join("\n", tarefasProximas.Take(3).Select(t => 
                        $"• {t.Titulo} (Vencimento: {t.DataVencimento:dd/MM/yyyy HH:mm})"));
                    
                    if (tarefasProximas.Count > 3)
                    {
                        detailedMessage += $"\n... e mais {tarefasProximas.Count - 3} tarefa(s).";
                    }

                    NotificationTriggered?.Invoke(this, new NotificationEventArgs
                    {
                        Title = "Tarefas Próximas do Vencimento",
                        Message = message,
                        DetailedMessage = detailedMessage,
                        NotificationType = NotificationType.TarefasProximasVencimento,
                        Tarefas = tarefasProximas
                    });
                }
            }
            catch (Exception ex)
            {
                // Log do erro ou tratamento silencioso
                Console.WriteLine($"Erro no serviço de notificações: {ex.Message}");
            }
        }

        public NotificationSummary GetNotificationSummary()
        {
            try
            {
                var tarefasVencidas = _tarefaController.GetTarefasVencidas();
                var tarefasProximas = _tarefaController.GetTarefasProximasDoVencimento();
                var tarefasPendentes = _tarefaController.GetTarefasByStatus(StatusTarefa.Pendente);
                var tarefasEmAndamento = _tarefaController.GetTarefasByStatus(StatusTarefa.EmAndamento);

                return new NotificationSummary
                {
                    TarefasVencidas = tarefasVencidas.Count,
                    TarefasProximasVencimento = tarefasProximas.Count,
                    TarefasPendentes = tarefasPendentes.Count,
                    TarefasEmAndamento = tarefasEmAndamento.Count,
                    TotalTarefasAtivas = tarefasPendentes.Count + tarefasEmAndamento.Count
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao gerar resumo de notificações: {ex.Message}", ex);
            }
        }

        public void Dispose()
        {
            _notificationTimer?.Stop();
            _notificationTimer?.Dispose();
        }
    }

    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string DetailedMessage { get; set; } = string.Empty;
        public NotificationType NotificationType { get; set; }
        public List<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    }

    public enum NotificationType
    {
        TarefasVencidas,
        TarefasProximasVencimento,
        TarefaCriada,
        TarefaConcluida,
        TarefaAtualizada
    }

    public class NotificationSummary
    {
        public int TarefasVencidas { get; set; }
        public int TarefasProximasVencimento { get; set; }
        public int TarefasPendentes { get; set; }
        public int TarefasEmAndamento { get; set; }
        public int TotalTarefasAtivas { get; set; }
    }
}
