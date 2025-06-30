namespace TaskFacil.Utilities
{
    public static class DateTimeExtensions
    {
        public static string ToFriendlyString(this DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan.Days > 0)
            {
                return timeSpan.Days == 1 ? "há 1 dia" : $"há {timeSpan.Days} dias";
            }

            if (timeSpan.Hours > 0)
            {
                return timeSpan.Hours == 1 ? "há 1 hora" : $"há {timeSpan.Hours} horas";
            }

            if (timeSpan.Minutes > 0)
            {
                return timeSpan.Minutes == 1 ? "há 1 minuto" : $"há {timeSpan.Minutes} minutos";
            }

            return "agora mesmo";
        }

        public static string ToRemainingTimeString(this DateTime dateTime)
        {
            var timeSpan = dateTime - DateTime.Now;

            if (timeSpan.TotalDays < 0)
            {
                var pastTimeSpan = DateTime.Now - dateTime;
                if (pastTimeSpan.Days > 0)
                {
                    return pastTimeSpan.Days == 1 ? "venceu há 1 dia" : $"venceu há {pastTimeSpan.Days} dias";
                }
                return "venceu hoje";
            }

            if (timeSpan.Days > 0)
            {
                return timeSpan.Days == 1 ? "vence em 1 dia" : $"vence em {timeSpan.Days} dias";
            }

            if (timeSpan.Hours > 0)
            {
                return timeSpan.Hours == 1 ? "vence em 1 hora" : $"vence em {timeSpan.Hours} horas";
            }

            if (timeSpan.Minutes > 0)
            {
                return timeSpan.Minutes == 1 ? "vence em 1 minuto" : $"vence em {timeSpan.Minutes} minutos";
            }

            return "vence agora";
        }

        public static bool IsToday(this DateTime dateTime)
        {
            return dateTime.Date == DateTime.Now.Date;
        }

        public static bool IsTomorrow(this DateTime dateTime)
        {
            return dateTime.Date == DateTime.Now.AddDays(1).Date;
        }

        public static bool IsThisWeek(this DateTime dateTime)
        {
            var startOfWeek = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);
            return dateTime.Date >= startOfWeek.Date && dateTime.Date < endOfWeek.Date;
        }
    }

    public static class ColorHelper
    {
        public static Color GetPriorityColor(Models.PrioridadeTarefa prioridade)
        {
            return prioridade switch
            {
                Models.PrioridadeTarefa.Baixa => Color.Green,
                Models.PrioridadeTarefa.Media => Color.Orange,
                Models.PrioridadeTarefa.Alta => Color.Red,
                Models.PrioridadeTarefa.Urgente => Color.DarkRed,
                _ => Color.Gray
            };
        }

        public static Color GetStatusColor(Models.StatusTarefa status)
        {
            return status switch
            {
                Models.StatusTarefa.Pendente => Color.Orange,
                Models.StatusTarefa.EmAndamento => Color.Blue,
                Models.StatusTarefa.Concluida => Color.Green,
                Models.StatusTarefa.Cancelada => Color.Gray,
                _ => Color.Black
            };
        }

        public static Color GetOverdueColor()
        {
            return Color.Red;
        }

        public static Color GetDueSoonColor()
        {
            return Color.Orange;
        }
    }

    public static class StringExtensions
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value[..maxLength] + "...";
        }

        public static bool ContainsIgnoreCase(this string source, string searchTerm)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(searchTerm))
                return false;

            return source.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
        }
    }

    public static class ValidationHelper
    {
        public static bool IsValidTarefa(Models.Tarefa tarefa, out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrWhiteSpace(tarefa.Titulo))
            {
                errors.Add("O título da tarefa é obrigatório.");
            }

            if (tarefa.Titulo.Length > 200)
            {
                errors.Add("O título da tarefa não pode ter mais de 200 caracteres.");
            }

            if (tarefa.Descricao.Length > 1000)
            {
                errors.Add("A descrição da tarefa não pode ter mais de 1000 caracteres.");
            }

            if (tarefa.Categoria.Length > 100)
            {
                errors.Add("A categoria da tarefa não pode ter mais de 100 caracteres.");
            }

            return errors.Count == 0;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
