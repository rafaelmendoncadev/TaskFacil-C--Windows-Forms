# Exemplos de Extensões - TaskFácil

## Adicionando Novos Campos à Tarefa

### 1. Modificar o Model
```csharp
// Em Models/Tarefa.cs
public class Tarefa : INotifyPropertyChanged
{
    // Campos existentes...
    
    private string _tags;
    private int _tempoEstimado; // em minutos
    private string _responsavel;
    
    public string Tags
    {
        get => _tags;
        set
        {
            _tags = value ?? string.Empty;
            OnPropertyChanged(nameof(Tags));
        }
    }
    
    public int TempoEstimado
    {
        get => _tempoEstimado;
        set
        {
            _tempoEstimado = value;
            OnPropertyChanged(nameof(TempoEstimado));
        }
    }
    
    public string Responsavel
    {
        get => _responsavel;
        set
        {
            _responsavel = value ?? string.Empty;
            OnPropertyChanged(nameof(Responsavel));
        }
    }
}
```

### 2. Atualizar o Banco de Dados
```csharp
// Em Data/DatabaseManager.cs - método InitializeDatabase()
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
        DataConclusao TEXT,
        Tags TEXT,
        TempoEstimado INTEGER DEFAULT 0,
        Responsavel TEXT
    );";

// Adicionar migration para tabelas existentes
string addColumnsQuery = @"
    ALTER TABLE Tarefas ADD COLUMN Tags TEXT;
    ALTER TABLE Tarefas ADD COLUMN TempoEstimado INTEGER DEFAULT 0;
    ALTER TABLE Tarefas ADD COLUMN Responsavel TEXT;";
```

## Adicionando Relatórios Avançados

### 1. Novo Service para Relatórios
```csharp
// Services/ReportService.cs
public class ReportService
{
    private readonly TarefaController _tarefaController;

    public ReportService(TarefaController tarefaController)
    {
        _tarefaController = tarefaController;
    }

    public ProductivityReport GetProductivityReport(DateTime startDate, DateTime endDate)
    {
        var tarefas = _tarefaController.GetAllTarefas()
            .Where(t => t.DataCriacao >= startDate && t.DataCriacao <= endDate)
            .ToList();

        return new ProductivityReport
        {
            TotalTarefas = tarefas.Count,
            TarefasConcluidas = tarefas.Count(t => t.Status == StatusTarefa.Concluida),
            TarefasVencidas = tarefas.Count(t => t.EstaVencida),
            TempoPorCategoria = tarefas.GroupBy(t => t.Categoria)
                .ToDictionary(g => g.Key, g => g.Sum(t => t.TempoEstimado)),
            TaxaConclusao = tarefas.Count > 0 ? 
                (double)tarefas.Count(t => t.Status == StatusTarefa.Concluida) / tarefas.Count * 100 : 0
        };
    }
}

public class ProductivityReport
{
    public int TotalTarefas { get; set; }
    public int TarefasConcluidas { get; set; }
    public int TarefasVencidas { get; set; }
    public Dictionary<string, int> TempoPorCategoria { get; set; } = new();
    public double TaxaConclusao { get; set; }
}
```

### 2. Formulário de Relatórios
```csharp
// Views/RelatorioForm.cs
public partial class RelatorioForm : Form
{
    private readonly ReportService _reportService;
    
    public RelatorioForm(ReportService reportService)
    {
        InitializeComponent();
        _reportService = reportService;
    }
    
    private void BtnGerarRelatorio_Click(object sender, EventArgs e)
    {
        var report = _reportService.GetProductivityReport(
            dtpInicio.Value, dtpFim.Value);
            
        // Atualizar controles com dados do relatório
        lblTotalTarefas.Text = $"Total: {report.TotalTarefas}";
        lblConcluidas.Text = $"Concluídas: {report.TarefasConcluidas}";
        lblTaxaConclusao.Text = $"Taxa: {report.TaxaConclusao:F1}%";
        
        // Gráfico simples
        chartProductivity.Series["Tarefas"].Points.Clear();
        chartProductivity.Series["Tarefas"].Points.AddXY("Concluídas", report.TarefasConcluidas);
        chartProductivity.Series["Tarefas"].Points.AddXY("Pendentes", report.TotalTarefas - report.TarefasConcluidas);
    }
}
```

## Integrações com APIs Externas

### 1. Sincronização com Google Calendar
```csharp
// Services/GoogleCalendarService.cs
public class GoogleCalendarService
{
    public async Task<bool> SincronizarTarefa(Tarefa tarefa)
    {
        try
        {
            var evento = new Event
            {
                Summary = tarefa.Titulo,
                Description = tarefa.Descricao,
                Start = new EventDateTime
                {
                    DateTime = tarefa.DataVencimento,
                    TimeZone = "America/Sao_Paulo"
                },
                End = new EventDateTime
                {
                    DateTime = tarefa.DataVencimento.AddHours(1),
                    TimeZone = "America/Sao_Paulo"
                }
            };
            
            // Lógica de autenticação e inserção no Google Calendar
            // Requer Google.Apis.Calendar NuGet package
            
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao sincronizar com Google Calendar: {ex.Message}");
        }
    }
}
```

### 2. Envio de Email com SendGrid
```csharp
// Services/EmailService.cs
public class EmailService
{
    private readonly string _apiKey;
    
    public EmailService(string apiKey)
    {
        _apiKey = apiKey;
    }
    
    public async Task EnviarLembreteAsync(Tarefa tarefa, string emailDestino)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress("noreply@taskfacil.com", "TaskFácil");
        var to = new EmailAddress(emailDestino);
        
        var subject = $"Lembrete: {tarefa.Titulo}";
        var htmlContent = $@"
            <h2>Lembrete de Tarefa</h2>
            <p><strong>Título:</strong> {tarefa.Titulo}</p>
            <p><strong>Descrição:</strong> {tarefa.Descricao}</p>
            <p><strong>Vencimento:</strong> {tarefa.DataVencimento:dd/MM/yyyy HH:mm}</p>
            <p><strong>Prioridade:</strong> {tarefa.PrioridadeTexto}</p>";
            
        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
        await client.SendEmailAsync(msg);
    }
}
```

## Melhorias na Interface

### 1. Drag and Drop para Prioridades
```csharp
// Views/MainForm.cs
private void DgvTarefas_MouseDown(object sender, MouseEventArgs e)
{
    if (e.Button == MouseButtons.Left)
    {
        var hitTest = dgvTarefas.HitTest(e.X, e.Y);
        if (hitTest.RowIndex >= 0)
        {
            var tarefa = _currentTarefas[hitTest.RowIndex];
            dgvTarefas.DoDragDrop(tarefa, DragDropEffects.Move);
        }
    }
}

private void PanelPrioridades_DragEnter(object sender, DragEventArgs e)
{
    if (e.Data.GetDataPresent(typeof(Tarefa)))
    {
        e.Effect = DragDropEffects.Move;
    }
}

private void PanelPrioridadeAlta_DragDrop(object sender, DragEventArgs e)
{
    if (e.Data.GetData(typeof(Tarefa)) is Tarefa tarefa)
    {
        tarefa.Prioridade = PrioridadeTarefa.Alta;
        _tarefaController.UpdateTarefa(tarefa);
        LoadData();
    }
}
```

### 2. Gráficos com Chart Control
```csharp
// Views/DashboardForm.cs
public partial class DashboardForm : Form
{
    public void AtualizarGraficos()
    {
        var stats = _tarefaController.GetEstatisticasPorStatus();
        
        // Gráfico de Pizza - Status
        chartStatus.Series["Status"].Points.Clear();
        foreach (var stat in stats)
        {
            chartStatus.Series["Status"].Points.AddXY(
                stat.Key.ToString(), stat.Value);
        }
        
        // Gráfico de Barras - Tarefas por Mês
        var tarefasPorMes = _tarefaController.GetAllTarefas()
            .Where(t => t.Status == StatusTarefa.Concluida)
            .GroupBy(t => t.DataConclusao?.ToString("yyyy-MM"))
            .OrderBy(g => g.Key)
            .Take(12);
            
        chartTendencia.Series["Monthly"].Points.Clear();
        foreach (var grupo in tarefasPorMes)
        {
            chartTendencia.Series["Monthly"].Points.AddXY(
                grupo.Key, grupo.Count());
        }
    }
}
```

## Plugins e Extensibilidade

### 1. Interface para Plugins
```csharp
// Interfaces/ITaskPlugin.cs
public interface ITaskPlugin
{
    string Name { get; }
    string Version { get; }
    string Description { get; }
    
    void Initialize(TarefaController controller);
    void Execute(Tarefa tarefa);
    bool CanExecute(Tarefa tarefa);
}

// Exemplo de Plugin
public class TimerPlugin : ITaskPlugin
{
    public string Name => "Timer Plugin";
    public string Version => "1.0.0";
    public string Description => "Cronometra o tempo gasto em tarefas";
    
    private TarefaController _controller;
    private Dictionary<int, DateTime> _timers = new();
    
    public void Initialize(TarefaController controller)
    {
        _controller = controller;
    }
    
    public void Execute(Tarefa tarefa)
    {
        if (_timers.ContainsKey(tarefa.Id))
        {
            // Parar timer
            var tempoGasto = DateTime.Now - _timers[tarefa.Id];
            _timers.Remove(tarefa.Id);
            
            MessageBox.Show($"Tempo gasto: {tempoGasto:hh\\:mm\\:ss}");
        }
        else
        {
            // Iniciar timer
            _timers[tarefa.Id] = DateTime.Now;
            MessageBox.Show("Timer iniciado!");
        }
    }
    
    public bool CanExecute(Tarefa tarefa)
    {
        return tarefa.Status == StatusTarefa.EmAndamento;
    }
}
```

### 2. Gerenciador de Plugins
```csharp
// Services/PluginManager.cs
public class PluginManager
{
    private readonly List<ITaskPlugin> _plugins = new();
    
    public void LoadPlugins(string pluginDirectory)
    {
        if (!Directory.Exists(pluginDirectory))
            return;
            
        var dllFiles = Directory.GetFiles(pluginDirectory, "*.dll");
        
        foreach (var dllFile in dllFiles)
        {
            try
            {
                var assembly = Assembly.LoadFrom(dllFile);
                var pluginTypes = assembly.GetTypes()
                    .Where(t => typeof(ITaskPlugin).IsAssignableFrom(t) && !t.IsInterface);
                    
                foreach (var pluginType in pluginTypes)
                {
                    if (Activator.CreateInstance(pluginType) is ITaskPlugin plugin)
                    {
                        _plugins.Add(plugin);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log erro ao carregar plugin
                Console.WriteLine($"Erro ao carregar plugin {dllFile}: {ex.Message}");
            }
        }
    }
    
    public void ExecutePlugin(string pluginName, Tarefa tarefa)
    {
        var plugin = _plugins.FirstOrDefault(p => p.Name == pluginName);
        if (plugin != null && plugin.CanExecute(tarefa))
        {
            plugin.Execute(tarefa);
        }
    }
    
    public IEnumerable<ITaskPlugin> GetAvailablePlugins(Tarefa tarefa)
    {
        return _plugins.Where(p => p.CanExecute(tarefa));
    }
}
```

## Configurações Avançadas

### 1. Sistema de Configurações
```csharp
// Models/AppSettings.cs
public class AppSettings
{
    public bool NotificacoesHabilitadas { get; set; } = true;
    public int IntervaloNotificacoes { get; set; } = 300000; // 5 minutos
    public string FormatoDataExportacao { get; set; } = "dd/MM/yyyy";
    public bool IniciarComWindows { get; set; } = false;
    public string EmailPadrao { get; set; } = string.Empty;
    public Dictionary<string, string> CoresPersonalizadas { get; set; } = new();
    
    public void Save()
    {
        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions 
        { 
            WriteIndented = true 
        });
        File.WriteAllText("appsettings.json", json);
    }
    
    public static AppSettings Load()
    {
        if (File.Exists("appsettings.json"))
        {
            var json = File.ReadAllText("appsettings.json");
            return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
        }
        return new AppSettings();
    }
}
```

### 2. Formulário de Configurações
```csharp
// Views/ConfiguracoesForm.cs
public partial class ConfiguracoesForm : Form
{
    private readonly AppSettings _settings;
    
    public ConfiguracoesForm()
    {
        InitializeComponent();
        _settings = AppSettings.Load();
        CarregarConfiguracoes();
    }
    
    private void CarregarConfiguracoes()
    {
        chkNotificacoes.Checked = _settings.NotificacoesHabilitadas;
        numIntervalo.Value = _settings.IntervaloNotificacoes / 60000; // converter para minutos
        txtEmail.Text = _settings.EmailPadrao;
        chkIniciarWindows.Checked = _settings.IniciarComWindows;
    }
    
    private void BtnSalvar_Click(object sender, EventArgs e)
    {
        _settings.NotificacoesHabilitadas = chkNotificacoes.Checked;
        _settings.IntervaloNotificacoes = (int)numIntervalo.Value * 60000;
        _settings.EmailPadrao = txtEmail.Text;
        _settings.IniciarComWindows = chkIniciarWindows.Checked;
        
        _settings.Save();
        
        MessageBox.Show("Configurações salvas com sucesso!", "Sucesso",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        
        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}
```

Essas extensões podem ser implementadas gradualmente para expandir as funcionalidades do TaskFácil!
