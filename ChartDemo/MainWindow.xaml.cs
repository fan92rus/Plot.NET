using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Funny.PlotNET;

namespace ChartDemo
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

        private void Plotter_Loaded(object sender, RoutedEventArgs e)
        {
            this.Plotter.Add(new List<Ohlc>()
            {
                new Ohlc(){Time = DateTime.Now - TimeSpan.FromHours(2), Close = 12.3, Open = 12.5, High = 12.7, Low = 12},
                new Ohlc(){Time = DateTime.Now - TimeSpan.FromHours(1), Close = 12, Open = 12.2, High = 12.8, Low = 11.7},
            });
            this.Plotter.Plot();
        }
    }
}
