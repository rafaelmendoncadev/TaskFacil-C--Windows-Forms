namespace TaskFacil.Views
{
    partial class TarefaForm
    {
        private System.ComponentModel.IContainer components = null;
        
        private Label lblTitulo;
        private TextBox txtTitulo;
        private Label lblDescricao;
        private TextBox txtDescricao;
        private Label lblDataVencimento;
        private DateTimePicker dtpVencimento;
        private Label lblPrioridade;
        private ComboBox cmbPrioridade;
        private Label lblCategoria;
        private ComboBox cmbCategoria;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private Label lblDataCriacao;
        private Label lblDataConclusao;
        private Label lblPrioridadePreview;
        private Button btnSalvar;
        private Button btnCancelar;
        private GroupBox gbDadosBasicos;
        private GroupBox gbDetalhes;
        private GroupBox gbInformacoes;

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
            this.ClientSize = new Size(600, 700);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Tarefa";
            
            // Controls
            this.gbDadosBasicos = new GroupBox();
            this.lblTitulo = new Label();
            this.txtTitulo = new TextBox();
            this.lblDescricao = new Label();
            this.txtDescricao = new TextBox();
            
            this.gbDetalhes = new GroupBox();
            this.lblDataVencimento = new Label();
            this.dtpVencimento = new DateTimePicker();
            this.lblPrioridade = new Label();
            this.cmbPrioridade = new ComboBox();
            this.lblPrioridadePreview = new Label();
            this.lblCategoria = new Label();
            this.cmbCategoria = new ComboBox();
            this.lblStatus = new Label();
            this.cmbStatus = new ComboBox();
            
            this.gbInformacoes = new GroupBox();
            this.lblDataCriacao = new Label();
            this.lblDataConclusao = new Label();
            
            this.btnSalvar = new Button();
            this.btnCancelar = new Button();
            
            this.SuspendLayout();
            
            // GroupBox Dados Básicos
            this.gbDadosBasicos.Text = "Dados Básicos";
            this.gbDadosBasicos.Location = new Point(12, 12);
            this.gbDadosBasicos.Size = new Size(576, 200);
            this.gbDadosBasicos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            
            // Título
            this.lblTitulo.Text = "Título *:";
            this.lblTitulo.Location = new Point(15, 30);
            this.lblTitulo.Size = new Size(80, 23);
            
            this.txtTitulo.Location = new Point(15, 55);
            this.txtTitulo.Size = new Size(545, 27);
            this.txtTitulo.MaxLength = 200;
            this.txtTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            
            // Descrição
            this.lblDescricao.Text = "Descrição:";
            this.lblDescricao.Location = new Point(15, 95);
            this.lblDescricao.Size = new Size(80, 23);
            
            this.txtDescricao.Location = new Point(15, 120);
            this.txtDescricao.Size = new Size(545, 60);
            this.txtDescricao.MaxLength = 1000;
            this.txtDescricao.Multiline = true;
            this.txtDescricao.ScrollBars = ScrollBars.Vertical;
            this.txtDescricao.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            
            this.gbDadosBasicos.Controls.AddRange(new Control[] {
                this.lblTitulo, this.txtTitulo,
                this.lblDescricao, this.txtDescricao
            });
            
            // GroupBox Detalhes
            this.gbDetalhes.Text = "Detalhes";
            this.gbDetalhes.Location = new Point(12, 230);
            this.gbDetalhes.Size = new Size(576, 280);
            this.gbDetalhes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            
            // Data de Vencimento
            this.lblDataVencimento.Text = "Data de Vencimento *:";
            this.lblDataVencimento.Location = new Point(15, 30);
            this.lblDataVencimento.Size = new Size(150, 23);
            
            this.dtpVencimento.Location = new Point(15, 55);
            this.dtpVencimento.Size = new Size(200, 27);
            this.dtpVencimento.Format = DateTimePickerFormat.Custom;
            this.dtpVencimento.CustomFormat = "dd/MM/yyyy HH:mm";
            
            // Prioridade
            this.lblPrioridade.Text = "Prioridade *:";
            this.lblPrioridade.Location = new Point(230, 30);
            this.lblPrioridade.Size = new Size(100, 23);
            
            this.cmbPrioridade.Location = new Point(230, 55);
            this.cmbPrioridade.Size = new Size(150, 28);
            this.cmbPrioridade.DropDownStyle = ComboBoxStyle.DropDownList;
            
            this.lblPrioridadePreview.Text = "Prioridade: -";
            this.lblPrioridadePreview.Location = new Point(230, 90);
            this.lblPrioridadePreview.Size = new Size(150, 23);
            this.lblPrioridadePreview.Font = new Font(this.Font, FontStyle.Bold);
            
            // Categoria
            this.lblCategoria.Text = "Categoria:";
            this.lblCategoria.Location = new Point(15, 125);
            this.lblCategoria.Size = new Size(80, 23);
            
            this.cmbCategoria.Location = new Point(15, 150);
            this.cmbCategoria.Size = new Size(200, 28);
            this.cmbCategoria.MaxLength = 100;
            
            // Status
            this.lblStatus.Text = "Status *:";
            this.lblStatus.Location = new Point(230, 125);
            this.lblStatus.Size = new Size(80, 23);
            
            this.cmbStatus.Location = new Point(230, 150);
            this.cmbStatus.Size = new Size(150, 28);
            this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            
            this.gbDetalhes.Controls.AddRange(new Control[] {
                this.lblDataVencimento, this.dtpVencimento,
                this.lblPrioridade, this.cmbPrioridade, this.lblPrioridadePreview,
                this.lblCategoria, this.cmbCategoria,
                this.lblStatus, this.cmbStatus
            });
            
            // GroupBox Informações
            this.gbInformacoes.Text = "Informações";
            this.gbInformacoes.Location = new Point(12, 530);
            this.gbInformacoes.Size = new Size(576, 80);
            this.gbInformacoes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            
            this.lblDataCriacao.Text = "Data de Criação: -";
            this.lblDataCriacao.Location = new Point(15, 25);
            this.lblDataCriacao.Size = new Size(250, 23);
            this.lblDataCriacao.ForeColor = Color.Gray;
            
            this.lblDataConclusao.Text = "Data de Conclusão: -";
            this.lblDataConclusao.Location = new Point(280, 25);
            this.lblDataConclusao.Size = new Size(250, 23);
            this.lblDataConclusao.ForeColor = Color.Gray;
            
            this.gbInformacoes.Controls.AddRange(new Control[] {
                this.lblDataCriacao, this.lblDataConclusao
            });
            
            // Botões
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Location = new Point(433, 630);
            this.btnSalvar.Size = new Size(75, 35);
            this.btnSalvar.BackColor = Color.LightGreen;
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnSalvar.Enabled = false;
            
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Location = new Point(513, 630);
            this.btnCancelar.Size = new Size(75, 35);
            this.btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnCancelar.DialogResult = DialogResult.Cancel;
            
            // Form Controls
            this.AcceptButton = this.btnSalvar;
            this.CancelButton = this.btnCancelar;
            
            this.Controls.AddRange(new Control[] {
                this.gbDadosBasicos, this.gbDetalhes, this.gbInformacoes,
                this.btnSalvar, this.btnCancelar
            });
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
