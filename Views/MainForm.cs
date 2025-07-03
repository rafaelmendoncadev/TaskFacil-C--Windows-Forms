using TaskFacil.Controllers;
using TaskFacil.Models;
using TaskFacil.Services;
using TaskFacil.Utilities;

namespace TaskFacil.Views
{
    public partial class MainForm : Form
    {
        private readonly TarefaController _tarefaController;
        private readonly NotificationService _notificationService;
        private readonly ExportService _exportService;
        
        private List<Tarefa> _currentTarefas;
        private bool _isLoading;

        public MainForm()
        {
            InitializeComponent();
            this.Icon = new Icon("imagens/icone.ico");
            _tarefaController = new TarefaController();
            _notificationService = new NotificationService(_tarefaController);
            _exportService = new ExportService();
            _currentTarefas = new List<Tarefa>();
            
            InitializeEvents();
            LoadData();
            StartNotificationService();
        }

        private void InitializeEvents()
        {
            _notificationService.NotificationTriggered += OnNotificationTriggered;
            
            // Eventos dos controles (serão implementados no Designer)
            btnNovatarefa.Click += BtnNovaTarefa_Click;
            btnEditarTarefa.Click += BtnEditarTarefa_Click;
            btnExcluirTarefa.Click += BtnExcluirTarefa_Click;
            btnMarcarConcluida.Click += BtnMarcarConcluida_Click;
            btnBuscar.Click += BtnBuscar_Click;
            btnExportar.Click += BtnExportar_Click;
            btnAtualizar.Click += BtnAtualizar_Click;
            
            txtBusca.TextChanged += TxtBusca_TextChanged;
            cmbFiltroStatus.SelectedIndexChanged += CmbFiltroStatus_SelectedIndexChanged;
            cmbFiltroCategoria.SelectedIndexChanged += CmbFiltroCategoria_SelectedIndexChanged;
            cmbOrdenacao.SelectedIndexChanged += CmbOrdenacao_SelectedIndexChanged;
            
            dgvTarefas.SelectionChanged += DgvTarefas_SelectionChanged;
            dgvTarefas.CellDoubleClick += DgvTarefas_CellDoubleClick;
            dgvTarefas.CellFormatting += DgvTarefas_CellFormatting;
        }

        private void LoadData()
        {
            try
            {
                _isLoading = true;
                
                // Carregar todas as tarefas
                _currentTarefas = _tarefaController.GetAllTarefas();
                
                // Atualizar DataGridView
                UpdateDataGridView();
                
                // Atualizar filtros
                UpdateFilters();
                
                // Atualizar estatísticas
                UpdateStatistics();
                
                _isLoading = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}", "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _isLoading = false;
            }
        }

        private void UpdateDataGridView()
        {
            dgvTarefas.DataSource = null;
            dgvTarefas.DataSource = _currentTarefas;
            
            // Configurar colunas
            if (dgvTarefas.Columns.Count > 0)
            {
                dgvTarefas.Columns["Id"].Visible = false;
                dgvTarefas.Columns["Titulo"].HeaderText = "Título";
                dgvTarefas.Columns["Titulo"].Width = 200;
                dgvTarefas.Columns["Descricao"].HeaderText = "Descrição";
                dgvTarefas.Columns["Descricao"].Width = 250;
                dgvTarefas.Columns["DataVencimento"].HeaderText = "Vencimento";
                dgvTarefas.Columns["DataVencimento"].Width = 120;
                dgvTarefas.Columns["PrioridadeTexto"].HeaderText = "Prioridade";
                dgvTarefas.Columns["PrioridadeTexto"].Width = 100;
                dgvTarefas.Columns["Categoria"].HeaderText = "Categoria";
                dgvTarefas.Columns["Categoria"].Width = 120;
                dgvTarefas.Columns["StatusTexto"].HeaderText = "Status";
                dgvTarefas.Columns["StatusTexto"].Width = 100;
                
                // Ocultar colunas desnecessárias
                if (dgvTarefas.Columns["Prioridade"] != null)
                    dgvTarefas.Columns["Prioridade"].Visible = false;
                if (dgvTarefas.Columns["Status"] != null)
                    dgvTarefas.Columns["Status"].Visible = false;
                if (dgvTarefas.Columns["DataCriacao"] != null)
                    dgvTarefas.Columns["DataCriacao"].Visible = false;
                if (dgvTarefas.Columns["DataConclusao"] != null)
                    dgvTarefas.Columns["DataConclusao"].Visible = false;
                if (dgvTarefas.Columns["EstaVencida"] != null)
                    dgvTarefas.Columns["EstaVencida"].Visible = false;
                if (dgvTarefas.Columns["EstaProximaDoVencimento"] != null)
                    dgvTarefas.Columns["EstaProximaDoVencimento"].Visible = false;
            }
            
            // Atualizar contadores
            lblTotalTarefas.Text = $"Total: {_currentTarefas.Count} tarefa(s)";
        }

        private void UpdateFilters()
        {
            // Atualizar combo de categorias
            var categorias = _tarefaController.GetCategorias();
            cmbFiltroCategoria.Items.Clear();
            cmbFiltroCategoria.Items.Add("Todas as Categorias");
            cmbFiltroCategoria.Items.AddRange(categorias.ToArray());
            cmbFiltroCategoria.SelectedIndex = 0;
        }

        private void UpdateStatistics()
        {
            var stats = _tarefaController.GetEstatisticasPorStatus();
            var summary = _notificationService.GetNotificationSummary();
            
            lblPendentes.Text = $"Pendentes: {stats[StatusTarefa.Pendente]}";
            lblEmAndamento.Text = $"Em Andamento: {stats[StatusTarefa.EmAndamento]}";
            lblConcluidas.Text = $"Concluídas: {stats[StatusTarefa.Concluida]}";
            lblVencidas.Text = $"Vencidas: {summary.TarefasVencidas}";
            
            // Destacar tarefas vencidas se houver
            lblVencidas.ForeColor = summary.TarefasVencidas > 0 ? Color.Red : Color.Black;
        }

        private void StartNotificationService()
        {
            _notificationService.StartNotificationService();
        }

        private void OnNotificationTriggered(object? sender, NotificationEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnNotificationTriggered(sender, e)));
                return;
            }

            var result = MessageBox.Show(
                $"{e.Message}\n\n{e.DetailedMessage}\n\nDeseja ver as tarefas?",
                e.Title,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                // Filtrar por tarefas da notificação
                ApplyNotificationFilter(e.NotificationType);
            }
        }

        private void ApplyNotificationFilter(NotificationType type)
        {
            switch (type)
            {
                case NotificationType.TarefasVencidas:
                    _currentTarefas = _tarefaController.GetTarefasVencidas();
                    break;
                case NotificationType.TarefasProximasVencimento:
                    _currentTarefas = _tarefaController.GetTarefasProximasDoVencimento();
                    break;
            }
            
            UpdateDataGridView();
        }

        // Event Handlers
        private void BtnNovaTarefa_Click(object? sender, EventArgs e)
        {
            using var form = new TarefaForm(_tarefaController);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void BtnEditarTarefa_Click(object? sender, EventArgs e)
        {
            if (GetSelectedTarefa() is Tarefa tarefa)
            {
                using var form = new TarefaForm(_tarefaController, tarefa);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void BtnExcluirTarefa_Click(object? sender, EventArgs e)
        {
            if (GetSelectedTarefa() is Tarefa tarefa)
            {
                var result = MessageBox.Show(
                    $"Deseja realmente excluir a tarefa '{tarefa.Titulo}'?",
                    "Confirmar Exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        _tarefaController.DeleteTarefa(tarefa.Id);
                        LoadData();
                        MessageBox.Show("Tarefa excluída com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir tarefa: {ex.Message}", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnMarcarConcluida_Click(object? sender, EventArgs e)
        {
            if (GetSelectedTarefa() is Tarefa tarefa)
            {
                try
                {
                    if (tarefa.Status == StatusTarefa.Concluida)
                    {
                        _tarefaController.MarcarTarefaComoPendente(tarefa.Id);
                        MessageBox.Show("Tarefa marcada como pendente!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        _tarefaController.MarcarTarefaComoConcluida(tarefa.Id);
                        MessageBox.Show("Tarefa marcada como concluída!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar status da tarefa: {ex.Message}", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void BtnExportar_Click(object? sender, EventArgs e)
        {
            using var saveDialog = new SaveFileDialog
            {
                Filter = "Arquivos CSV (*.csv)|*.csv|Arquivos de Texto (*.txt)|*.txt",
                DefaultExt = "csv",
                AddExtension = true
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (saveDialog.FilterIndex == 1) // CSV
                    {
                        _exportService.ExportTarefasToCSV(_currentTarefas, saveDialog.FileName);
                    }
                    else // TXT
                    {
                        _exportService.ExportTarefasToText(_currentTarefas, saveDialog.FileName);
                    }

                    MessageBox.Show("Tarefas exportadas com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao exportar tarefas: {ex.Message}", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnAtualizar_Click(object? sender, EventArgs e)
        {
            LoadData();
        }

        private void TxtBusca_TextChanged(object? sender, EventArgs e)
        {
            if (!_isLoading)
            {
                ApplyFilters();
            }
        }

        private void CmbFiltroStatus_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (!_isLoading)
            {
                ApplyFilters();
            }
        }

        private void CmbFiltroCategoria_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (!_isLoading)
            {
                ApplyFilters();
            }
        }

        private void CmbOrdenacao_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (!_isLoading)
            {
                ApplyFilters();
            }
        }

        private void DgvTarefas_SelectionChanged(object? sender, EventArgs e)
        {
            var tarefa = GetSelectedTarefa();
            btnEditarTarefa.Enabled = tarefa != null;
            btnExcluirTarefa.Enabled = tarefa != null;
            btnMarcarConcluida.Enabled = tarefa != null;
            
            if (tarefa != null)
            {
                btnMarcarConcluida.Text = tarefa.Status == StatusTarefa.Concluida 
                    ? "Marcar como Pendente" 
                    : "Marcar como Concluída";
            }
        }

        private void DgvTarefas_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BtnEditarTarefa_Click(sender, e);
            }
        }

        private void DgvTarefas_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _currentTarefas.Count)
                return;

            var tarefa = _currentTarefas[e.RowIndex];
            
            // Colorir linhas baseado no status e vencimento
            if (e.CellStyle != null)
            {
                if (tarefa.EstaVencida)
                {
                    e.CellStyle.BackColor = Color.MistyRose;
                    e.CellStyle.ForeColor = Color.DarkRed;
                }
                else if (tarefa.EstaProximaDoVencimento)
                {
                    e.CellStyle.BackColor = Color.LightYellow;
                    e.CellStyle.ForeColor = Color.DarkOrange;
                }
                else if (tarefa.Status == StatusTarefa.Concluida)
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                    e.CellStyle.ForeColor = Color.DarkGreen;
                }
            }
        }

        private void ApplyFilters()
        {
            try
            {
                var searchTerm = txtBusca.Text.Trim();
                var statusFilter = cmbFiltroStatus.SelectedIndex;
                var categoriaFilter = cmbFiltroCategoria.SelectedItem?.ToString();
                var ordenacao = cmbOrdenacao.SelectedItem?.ToString();

                // Começar com todas as tarefas
                _currentTarefas = _tarefaController.GetAllTarefas();

                // Aplicar filtro de busca
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    _currentTarefas = _tarefaController.SearchTarefas(searchTerm);
                }

                // Aplicar filtro de status
                if (statusFilter > 0)
                {
                    var status = (StatusTarefa)(statusFilter - 1);
                    _currentTarefas = _currentTarefas.Where(t => t.Status == status).ToList();
                }

                // Aplicar filtro de categoria
                if (!string.IsNullOrEmpty(categoriaFilter) && categoriaFilter != "Todas as Categorias")
                {
                    _currentTarefas = _currentTarefas.Where(t => t.Categoria == categoriaFilter).ToList();
                }

                // Aplicar ordenação
                if (!string.IsNullOrEmpty(ordenacao))
                {
                    _currentTarefas = ordenacao switch
                    {
                        "Título" => _currentTarefas.OrderBy(t => t.Titulo).ToList(),
                        "Data de Vencimento" => _currentTarefas.OrderBy(t => t.DataVencimento).ToList(),
                        "Prioridade" => _currentTarefas.OrderByDescending(t => t.Prioridade).ToList(),
                        "Status" => _currentTarefas.OrderBy(t => t.Status).ToList(),
                        "Categoria" => _currentTarefas.OrderBy(t => t.Categoria).ToList(),
                        _ => _currentTarefas.OrderBy(t => t.DataVencimento).ToList()
                    };
                }

                UpdateDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao aplicar filtros: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Tarefa? GetSelectedTarefa()
        {
            if (dgvTarefas.SelectedRows.Count > 0)
            {
                var index = dgvTarefas.SelectedRows[0].Index;
                if (index >= 0 && index < _currentTarefas.Count)
                {
                    return _currentTarefas[index];
                }
            }
            return null;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _notificationService.StopNotificationService();
            _notificationService.Dispose();
            base.OnFormClosing(e);
        }
    }
}
