using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlotNET;

namespace WinformPlot
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public static class PlotHelper
    {
        public static IPlotter CreatePlotter() => new Plotter();

        public static void Show(IPlotter plotter)
        {
            var thread = new Thread(() =>
            {
                try
                {
                    var window = new MainForm();
                    Application.Run(new MainForm());
                    window.ShowPlot(plotter);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }
    }
}
