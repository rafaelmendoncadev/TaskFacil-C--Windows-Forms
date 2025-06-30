namespace TaskFacil.Views
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        
        private MenuStrip menuStrip1;
        private ToolStripMenuItem arquivoToolStripMenuItem;
        private ToolStripMenuItem novoToolStripMenuItem;
        private ToolStripMenuItem exportarToolStripMenuItem;
        private ToolStripMenuItem sairToolStripMenuItem;
        private ToolStripMenuItem ajudaToolStripMenuItem;
        private ToolStripMenuItem sobreToolStripMenuItem;
        
        private Panel panelTop;
        private Panel panelSearch;
        private Panel panelButtons;
        private Panel panelStatus;
        private Panel panelMain;
        
        private GroupBox gbFiltros;
        private Label lblBusca;
        private TextBox txtBusca;
        private Button btnBuscar;
        private Label lblStatus;
        private ComboBox cmbFiltroStatus;
        private Label lblCategoria;
        private ComboBox cmbFiltroCategoria;
        private Label lblOrdenacao;
        private ComboBox cmbOrdenacao;
        
        private GroupBox gbAcoes;
        private Button btnNovatarefa;
        private Button btnEditarTarefa;
        private Button btnExcluirTarefa;
        private Button btnMarcarConcluida;
        private Button btnExportar;
        private Button btnAtualizar;
        
        private GroupBox gbEstatisticas;
        private Label lblPendentes;
        private Label lblEmAndamento;
        private Label lblConcluidas;
        private Label lblVencidas;
        private Label lblTotalTarefas;
        
        private DataGridView dgvTarefas;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            // Form
            this.AutoScaleDimensions = new SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 800);
            this.MinimumSize = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "TaskFácil - Controle de Tarefas";
            this.WindowState = FormWindowState.Maximized;
            
            // MenuStrip
            this.menuStrip1 = new MenuStrip();
            this.arquivoToolStripMenuItem = new ToolStripMenuItem();
            this.novoToolStripMenuItem = new ToolStripMenuItem();
            this.exportarToolStripMenuItem = new ToolStripMenuItem();
            this.sairToolStripMenuItem = new ToolStripMenuItem();
            this.ajudaToolStripMenuItem = new ToolStripMenuItem();
            this.sobreToolStripMenuItem = new ToolStripMenuItem();
            
            // Panels
            this.panelTop = new Panel();
            this.panelSearch = new Panel();
            this.panelButtons = new Panel();
            this.panelStatus = new Panel();
            this.panelMain = new Panel();
            
            // Controls
            this.gbFiltros = new GroupBox();
            this.lblBusca = new Label();
            this.txtBusca = new TextBox();
            this.btnBuscar = new Button();
            this.lblStatus = new Label();
            this.cmbFiltroStatus = new ComboBox();
            this.lblCategoria = new Label();
            this.cmbFiltroCategoria = new ComboBox();
            this.lblOrdenacao = new Label();
            this.cmbOrdenacao = new ComboBox();
            
            this.gbAcoes = new GroupBox();
            this.btnNovatarefa = new Button();
            this.btnEditarTarefa = new Button();
            this.btnExcluirTarefa = new Button();
            this.btnMarcarConcluida = new Button();
            this.btnExportar = new Button();
            this.btnAtualizar = new Button();
            
            this.gbEstatisticas = new GroupBox();
            this.lblPendentes = new Label();
            this.lblEmAndamento = new Label();
            this.lblConcluidas = new Label();
            this.lblVencidas = new Label();
            this.lblTotalTarefas = new Label();
            
            this.dgvTarefas = new DataGridView();
            
            this.SuspendLayout();
            
            // MenuStrip Configuration
            this.menuStrip1.ImageScalingSize = new Size(20, 20);
            this.menuStrip1.Items.AddRange(new ToolStripItem[] {
                this.arquivoToolStripMenuItem,
                this.ajudaToolStripMenuItem
            });
            this.menuStrip1.Location = new Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new Size(1200, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            
            // Arquivo Menu
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.novoToolStripMenuItem,
                new ToolStripSeparator(),
                this.exportarToolStripMenuItem,
                new ToolStripSeparator(),
                this.sairToolStripMenuItem
            });
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new Size(75, 24);
            this.arquivoToolStripMenuItem.Text = "&Arquivo";
            
            this.novoToolStripMenuItem.Name = "novoToolStripMenuItem";
            this.novoToolStripMenuItem.Size = new Size(224, 26);
            this.novoToolStripMenuItem.Text = "&Nova Tarefa";
            this.novoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            this.novoToolStripMenuItem.Click += (s, e) => BtnNovaTarefa_Click(s, e);
            
            this.exportarToolStripMenuItem.Name = "exportarToolStripMenuItem";
            this.exportarToolStripMenuItem.Size = new Size(224, 26);
            this.exportarToolStripMenuItem.Text = "&Exportar";
            this.exportarToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.E;
            this.exportarToolStripMenuItem.Click += (s, e) => BtnExportar_Click(s, e);
            
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new Size(224, 26);
            this.sairToolStripMenuItem.Text = "&Sair";
            this.sairToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.F4;
            this.sairToolStripMenuItem.Click += (s, e) => this.Close();
            
            // Ajuda Menu
            this.ajudaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.sobreToolStripMenuItem
            });
            this.ajudaToolStripMenuItem.Name = "ajudaToolStripMenuItem";
            this.ajudaToolStripMenuItem.Size = new Size(62, 24);
            this.ajudaToolStripMenuItem.Text = "&Ajuda";
            
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new Size(224, 26);
            this.sobreToolStripMenuItem.Text = "&Sobre";
            this.sobreToolStripMenuItem.Click += (s, e) => MessageBox.Show(
                "TaskFácil v1.0\n\nSistema de Controle de Tarefas\nDesenvolvido em C# com Windows Forms",
                "Sobre", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            // Panel Top Configuration
            this.panelTop.Dock = DockStyle.Top;
            this.panelTop.Height = 140;
            this.panelTop.Controls.Add(this.gbFiltros);
            this.panelTop.Controls.Add(this.gbAcoes);
            
            // Panel Status Configuration
            this.panelStatus.Dock = DockStyle.Bottom;
            this.panelStatus.Height = 90;
            this.panelStatus.Controls.Add(this.gbEstatisticas);
            
            // Panel Main Configuration
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Controls.Add(this.dgvTarefas);
            
            // GroupBox Filtros Configuration
            this.gbFiltros.Text = "Filtros e Busca";
            this.gbFiltros.Dock = DockStyle.Left;
            this.gbFiltros.Width = 750;
            this.gbFiltros.Padding = new Padding(10);
            
            // Busca Controls
            this.lblBusca.Text = "Buscar:";
            this.lblBusca.Location = new Point(15, 25);
            this.lblBusca.Size = new Size(60, 23);
            
            this.txtBusca.Location = new Point(80, 22);
            this.txtBusca.Size = new Size(200, 27);
            this.txtBusca.PlaceholderText = "Digite para buscar...";
            
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.Location = new Point(290, 21);
            this.btnBuscar.Size = new Size(80, 29);
            
            // Status Filter
            this.lblStatus.Text = "Status:";
            this.lblStatus.Location = new Point(15, 60);
            this.lblStatus.Size = new Size(60, 23);
            
            this.cmbFiltroStatus.Location = new Point(80, 57);
            this.cmbFiltroStatus.Size = new Size(150, 28);
            this.cmbFiltroStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFiltroStatus.Items.AddRange(new object[] {
                "Todos os Status",
                "Pendente",
                "Em Andamento", 
                "Concluída",
                "Cancelada"
            });
            this.cmbFiltroStatus.SelectedIndex = 0;
            
            // Categoria Filter
            this.lblCategoria.Text = "Categoria:";
            this.lblCategoria.Location = new Point(250, 60);
            this.lblCategoria.Size = new Size(80, 23);
            
            this.cmbFiltroCategoria.Location = new Point(335, 57);
            this.cmbFiltroCategoria.Size = new Size(150, 28);
            this.cmbFiltroCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            
            // Ordenação
            this.lblOrdenacao.Text = "Ordenar por:";
            this.lblOrdenacao.Location = new Point(500, 60);
            this.lblOrdenacao.Size = new Size(100, 23);
            
            this.cmbOrdenacao.Location = new Point(605, 57);
            this.cmbOrdenacao.Size = new Size(130, 28);
            this.cmbOrdenacao.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbOrdenacao.Items.AddRange(new object[] {
                "Data de Vencimento",
                "Título",
                "Prioridade",
                "Status",
                "Categoria"
            });
            this.cmbOrdenacao.SelectedIndex = 0;
            
            this.gbFiltros.Controls.AddRange(new Control[] {
                this.lblBusca, this.txtBusca, this.btnBuscar,
                this.lblStatus, this.cmbFiltroStatus,
                this.lblCategoria, this.cmbFiltroCategoria,
                this.lblOrdenacao, this.cmbOrdenacao
            });
            
            // GroupBox Ações Configuration
            this.gbAcoes.Text = "Ações";
            this.gbAcoes.Dock = DockStyle.Right;
            this.gbAcoes.Width = 180;
            this.gbAcoes.Padding = new Padding(10);
            
            this.btnNovatarefa.Text = "Nova Tarefa";
            this.btnNovatarefa.Location = new Point(15, 25);
            this.btnNovatarefa.Size = new Size(150, 35);
            this.btnNovatarefa.BackColor = Color.LightGreen;
            this.btnNovatarefa.UseVisualStyleBackColor = false;
            
            this.btnEditarTarefa.Text = "Editar";
            this.btnEditarTarefa.Location = new Point(15, 70);
            this.btnEditarTarefa.Size = new Size(70, 30);
            this.btnEditarTarefa.Enabled = false;
            
            this.btnExcluirTarefa.Text = "Excluir";
            this.btnExcluirTarefa.Location = new Point(95, 70);
            this.btnExcluirTarefa.Size = new Size(70, 30);
            this.btnExcluirTarefa.Enabled = false;
            this.btnExcluirTarefa.BackColor = Color.LightCoral;
            this.btnExcluirTarefa.UseVisualStyleBackColor = false;
            
            this.btnMarcarConcluida.Text = "Concluir";
            this.btnMarcarConcluida.Location = new Point(15, 105);
            this.btnMarcarConcluida.Size = new Size(70, 30);
            this.btnMarcarConcluida.Enabled = false;
            this.btnMarcarConcluida.BackColor = Color.LightBlue;
            this.btnMarcarConcluida.UseVisualStyleBackColor = false;
            
            this.btnExportar.Text = "Exportar";
            this.btnExportar.Location = new Point(95, 105);
            this.btnExportar.Size = new Size(70, 30);
            
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.Location = new Point(55, 140);
            this.btnAtualizar.Size = new Size(100, 30);
            
            this.gbAcoes.Controls.AddRange(new Control[] {
                this.btnNovatarefa, this.btnEditarTarefa, this.btnExcluirTarefa,
                this.btnMarcarConcluida, this.btnExportar, this.btnAtualizar
            });
            
            // GroupBox Estatísticas Configuration
            this.gbEstatisticas.Text = "Estatísticas";
            this.gbEstatisticas.Dock = DockStyle.Fill;
            this.gbEstatisticas.Padding = new Padding(10);
            
            this.lblPendentes.Text = "Pendentes: 0";
            this.lblPendentes.Location = new Point(15, 25);
            this.lblPendentes.Size = new Size(120, 23);
            this.lblPendentes.ForeColor = Color.Orange;
            
            this.lblEmAndamento.Text = "Em Andamento: 0";
            this.lblEmAndamento.Location = new Point(150, 25);
            this.lblEmAndamento.Size = new Size(150, 23);
            this.lblEmAndamento.ForeColor = Color.Blue;
            
            this.lblConcluidas.Text = "Concluídas: 0";
            this.lblConcluidas.Location = new Point(320, 25);
            this.lblConcluidas.Size = new Size(120, 23);
            this.lblConcluidas.ForeColor = Color.Green;
            
            this.lblVencidas.Text = "Vencidas: 0";
            this.lblVencidas.Location = new Point(460, 25);
            this.lblVencidas.Size = new Size(120, 23);
            this.lblVencidas.ForeColor = Color.Red;
            
            this.lblTotalTarefas.Text = "Total: 0 tarefa(s)";
            this.lblTotalTarefas.Location = new Point(15, 55);
            this.lblTotalTarefas.Size = new Size(200, 23);
            this.lblTotalTarefas.Font = new Font(this.Font, FontStyle.Bold);
            
            this.gbEstatisticas.Controls.AddRange(new Control[] {
                this.lblPendentes, this.lblEmAndamento, this.lblConcluidas,
                this.lblVencidas, this.lblTotalTarefas
            });
            
            // DataGridView Configuration
            this.dgvTarefas.Dock = DockStyle.Fill;
            this.dgvTarefas.AutoGenerateColumns = true;
            this.dgvTarefas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvTarefas.MultiSelect = false;
            this.dgvTarefas.ReadOnly = true;
            this.dgvTarefas.AllowUserToAddRows = false;
            this.dgvTarefas.AllowUserToDeleteRows = false;
            this.dgvTarefas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTarefas.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            this.dgvTarefas.GridColor = Color.DarkGray;
            this.dgvTarefas.BorderStyle = BorderStyle.Fixed3D;
            
            // Form Controls
            this.MainMenuStrip = this.menuStrip1;
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.menuStrip1);
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
