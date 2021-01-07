using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using PlotNET;

namespace WPF_Component
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        public async Task Show(IPlotter plotter)
        {
            try
            {
                await this.Plot.Show(plotter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    public static class PlotHelper
    {
        public static IPlotter CreatePlotter() => new Plotter();
        
        public static async Task Show(IPlotter plotter)
        {
            var thread = new Thread(() =>
            {
                try
                {
                    var window = new MainWindow();
                    window.Show(plotter);
                    window.ShowDialog();
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
