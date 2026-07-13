using System;
using System.Windows.Forms;
using ProcedGenV2.UI;

namespace ProcedGenV2;

internal static class Program
{
    /// <summary>Главная точка входа приложения.</summary>
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}
