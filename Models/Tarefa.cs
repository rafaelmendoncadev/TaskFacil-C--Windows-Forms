using System.ComponentModel;

namespace TaskFacil.Models
{
    public enum PrioridadeTarefa
    {
        Baixa = 1,
        Media = 2,
        Alta = 3,
        Urgente = 4
    }

    public enum StatusTarefa
    {
        Pendente = 1,
        EmAndamento = 2,
        Concluida = 3,
        Cancelada = 4
    }

    public class Tarefa : INotifyPropertyChanged
    {
        private int _id;
        private string _titulo;
        private string _descricao;
        private DateTime _dataVencimento;
        private PrioridadeTarefa _prioridade;
        private string _categoria;
        private StatusTarefa _status;
        private DateTime _dataCriacao;
        private DateTime? _dataConclusao;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Titulo
        {
            get => _titulo;
            set
            {
                _titulo = value ?? string.Empty;
                OnPropertyChanged(nameof(Titulo));
            }
        }

        public string Descricao
        {
            get => _descricao;
            set
            {
                _descricao = value ?? string.Empty;
                OnPropertyChanged(nameof(Descricao));
            }
        }

        public DateTime DataVencimento
        {
            get => _dataVencimento;
            set
            {
                _dataVencimento = value;
                OnPropertyChanged(nameof(DataVencimento));
            }
        }

        public PrioridadeTarefa Prioridade
        {
            get => _prioridade;
            set
            {
                _prioridade = value;
                OnPropertyChanged(nameof(Prioridade));
            }
        }

        public string Categoria
        {
            get => _categoria;
            set
            {
                _categoria = value ?? string.Empty;
                OnPropertyChanged(nameof(Categoria));
            }
        }

        public StatusTarefa Status
        {
            get => _status;
            set
            {
                _status = value;
                if (value == StatusTarefa.Concluida && _dataConclusao == null)
                {
                    _dataConclusao = DateTime.Now;
                }
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(DataConclusao));
            }
        }

        public DateTime DataCriacao
        {
            get => _dataCriacao;
            set
            {
                _dataCriacao = value;
                OnPropertyChanged(nameof(DataCriacao));
            }
        }

        public DateTime? DataConclusao
        {
            get => _dataConclusao;
            set
            {
                _dataConclusao = value;
                OnPropertyChanged(nameof(DataConclusao));
            }
        }

        public bool EstaVencida => DataVencimento < DateTime.Now && Status != StatusTarefa.Concluida;
        
        public bool EstaProximaDoVencimento => 
            DataVencimento.Date <= DateTime.Now.AddDays(2).Date && 
            DataVencimento.Date >= DateTime.Now.Date && 
            Status != StatusTarefa.Concluida;

        public string PrioridadeTexto => Prioridade switch
        {
            PrioridadeTarefa.Baixa => "Baixa",
            PrioridadeTarefa.Media => "Média",
            PrioridadeTarefa.Alta => "Alta",
            PrioridadeTarefa.Urgente => "Urgente",
            _ => "Não definida"
        };

        public string StatusTexto => Status switch
        {
            StatusTarefa.Pendente => "Pendente",
            StatusTarefa.EmAndamento => "Em Andamento",
            StatusTarefa.Concluida => "Concluída",
            StatusTarefa.Cancelada => "Cancelada",
            _ => "Não definido"
        };

        public Tarefa()
        {
            _titulo = string.Empty;
            _descricao = string.Empty;
            _categoria = string.Empty;
            _dataVencimento = DateTime.Now.AddDays(1);
            _prioridade = PrioridadeTarefa.Media;
            _status = StatusTarefa.Pendente;
            _dataCriacao = DateTime.Now;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
