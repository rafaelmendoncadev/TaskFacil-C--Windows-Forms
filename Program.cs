using TaskFacil.Views;

namespace TaskFacil
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Para garantir alta qualidade visual no Windows
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Executar a aplicação
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro crítico na aplicação: {ex.Message}\n\nDetalhes: {ex}", 
                    "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
