using TaskFacil.Controllers;
using TaskFacil.Models;
using TaskFacil.Utilities;

namespace TaskFacil.Views
{
    public partial class TarefaForm : Form
    {
        private readonly TarefaController _tarefaController;
        private readonly Tarefa _tarefa;
        private readonly bool _isEditMode;

        public TarefaForm(TarefaController tarefaController, Tarefa? tarefa = null)
        {
            InitializeComponent();
            _tarefaController = tarefaController;
            _tarefa = tarefa ?? new Tarefa();
            _isEditMode = tarefa != null;
            
            ConfigureForm();
            LoadCategories();
            LoadData();
        }

        private void ConfigureForm()
        {
            this.Text = _isEditMode ? "Editar Tarefa" : "Nova Tarefa";
            
            // Configurar ComboBox de Prioridade
            cmbPrioridade.Items.Clear();
            foreach (PrioridadeTarefa prioridade in Enum.GetValues<PrioridadeTarefa>())
            {
                cmbPrioridade.Items.Add(new { Value = prioridade, Text = GetPrioridadeText(prioridade) });
            }
            cmbPrioridade.DisplayMember = "Text";
            cmbPrioridade.ValueMember = "Value";
            
            // Configurar ComboBox de Status
            cmbStatus.Items.Clear();
            foreach (StatusTarefa status in Enum.GetValues<StatusTarefa>())
            {
                cmbStatus.Items.Add(new { Value = status, Text = GetStatusText(status) });
            }
            cmbStatus.DisplayMember = "Text";
            cmbStatus.ValueMember = "Value";
            
            // Configurar DateTimePicker
            dtpVencimento.Format = DateTimePickerFormat.Custom;
            dtpVencimento.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpVencimento.ShowUpDown = false;
            
            // Configurar eventos
            btnSalvar.Click += BtnSalvar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            txtTitulo.TextChanged += ValidateForm;
            cmbCategoria.DropDown += CmbCategoria_DropDown;
        }

        private void LoadCategories()
        {
            try
            {
                var categorias = _tarefaController.GetCategorias();
                cmbCategoria.Items.Clear();
                cmbCategoria.Items.AddRange(categorias.ToArray());
                
                // Adicionar categorias padrão se não existirem
                var categoriasComuns = new[] { "Trabalho", "Pessoal", "Estudos", "Casa", "Saúde", "Compras" };
                foreach (var categoria in categoriasComuns)
                {
                    if (!categorias.Contains(categoria))
                    {
                        cmbCategoria.Items.Add(categoria);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar categorias: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadData()
        {
            try
            {
                txtTitulo.Text = _tarefa.Titulo;
                txtDescricao.Text = _tarefa.Descricao;
                dtpVencimento.Value = _tarefa.DataVencimento;
                cmbCategoria.Text = _tarefa.Categoria;
                
                // Selecionar prioridade
                for (int i = 0; i < cmbPrioridade.Items.Count; i++)
                {
                    var itemObj = cmbPrioridade.Items[i];
                    if (itemObj != null)
                    {
                        dynamic item = itemObj;
                        if (item?.Value != null && item.Value!.Equals(_tarefa.Prioridade))
                        {
                            cmbPrioridade.SelectedIndex = i;
                            break;
                        }
                    }
                }
                
                // Selecionar status (apenas em modo de edição)
                if (_isEditMode)
                {
                    for (int i = 0; i < cmbStatus.Items.Count; i++)
                    {
                        var itemObj = cmbStatus.Items[i];
                        if (itemObj != null)
                        {
                            dynamic item = itemObj;
                            if (item?.Value != null && item.Value!.Equals(_tarefa.Status))
                            {
                                cmbStatus.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    // Para nova tarefa, definir como Pendente
                    cmbStatus.SelectedIndex = 0; // Pendente
                }
                
                // Exibir informações adicionais em modo de edição
                if (_isEditMode)
                {
                    lblDataCriacao.Text = $"Data de Criação: {_tarefa.DataCriacao:dd/MM/yyyy HH:mm}";
                    lblDataConclusao.Text = _tarefa.DataConclusao.HasValue 
                        ? $"Data de Conclusão: {_tarefa.DataConclusao:dd/MM/yyyy HH:mm}"
                        : "Data de Conclusão: -";
                    
                    lblDataCriacao.Visible = true;
                    lblDataConclusao.Visible = true;
                }
                else
                {
                    lblDataCriacao.Visible = false;
                    lblDataConclusao.Visible = false;
                }
                
                ValidateForm(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados da tarefa: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSalvar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!ValidateTarefa())
                    return;

                // Atualizar dados da tarefa
                _tarefa.Titulo = txtTitulo.Text.Trim();
                _tarefa.Descricao = txtDescricao.Text.Trim();
                _tarefa.DataVencimento = dtpVencimento.Value;
                _tarefa.Categoria = cmbCategoria.Text.Trim();
                
                if (cmbPrioridade.SelectedItem != null)
                {
                    dynamic prioridadeItem = cmbPrioridade.SelectedItem;
                    _tarefa.Prioridade = prioridadeItem.Value;
                }
                
                if (cmbStatus.SelectedItem != null)
                {
                    dynamic statusItem = cmbStatus.SelectedItem;
                    _tarefa.Status = statusItem.Value;
                }

                bool success;
                if (_isEditMode)
                {
                    success = _tarefaController.UpdateTarefa(_tarefa);
                    if (success)
                    {
                        MessageBox.Show("Tarefa atualizada com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    success = _tarefaController.CreateTarefa(_tarefa);
                    if (success)
                    {
                        MessageBox.Show("Tarefa criada com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Erro ao salvar a tarefa. Tente novamente.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar tarefa: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ValidateForm(object? sender, EventArgs e)
        {
            bool isValid = !string.IsNullOrWhiteSpace(txtTitulo.Text);
            btnSalvar.Enabled = isValid;
            
            // Atualizar preview de prioridade
            if (cmbPrioridade.SelectedItem != null)
            {
                dynamic item = cmbPrioridade.SelectedItem;
                lblPrioridadePreview.Text = $"Prioridade: {item.Text}";
                lblPrioridadePreview.ForeColor = ColorHelper.GetPriorityColor(item.Value);
            }
        }

        private void CmbCategoria_DropDown(object? sender, EventArgs e)
        {
            // Recarregar categorias quando abrir o dropdown
            LoadCategories();
        }

        private bool ValidateTarefa()
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                errors.Add("O título é obrigatório.");
            }

            if (txtTitulo.Text.Length > 200)
            {
                errors.Add("O título não pode ter mais de 200 caracteres.");
            }

            if (txtDescricao.Text.Length > 1000)
            {
                errors.Add("A descrição não pode ter mais de 1000 caracteres.");
            }

            if (cmbCategoria.Text.Length > 100)
            {
                errors.Add("A categoria não pode ter mais de 100 caracteres.");
            }

            if (dtpVencimento.Value < DateTime.Now.Date && !_isEditMode)
            {
                errors.Add("A data de vencimento não pode ser anterior à data atual.");
            }

            if (cmbPrioridade.SelectedItem == null)
            {
                errors.Add("Selecione uma prioridade.");
            }

            if (cmbStatus.SelectedItem == null)
            {
                errors.Add("Selecione um status.");
            }

            if (errors.Any())
            {
                MessageBox.Show(string.Join("\n", errors), "Dados Inválidos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private string GetPrioridadeText(PrioridadeTarefa prioridade)
        {
            return prioridade switch
            {
                PrioridadeTarefa.Baixa => "Baixa",
                PrioridadeTarefa.Media => "Média",
                PrioridadeTarefa.Alta => "Alta",
                PrioridadeTarefa.Urgente => "Urgente",
                _ => "Não definida"
            };
        }

        private string GetStatusText(StatusTarefa status)
        {
            return status switch
            {
                StatusTarefa.Pendente => "Pendente",
                StatusTarefa.EmAndamento => "Em Andamento",
                StatusTarefa.Concluida => "Concluída",
                StatusTarefa.Cancelada => "Cancelada",
                _ => "Não definido"
            };
        }
    }
}
