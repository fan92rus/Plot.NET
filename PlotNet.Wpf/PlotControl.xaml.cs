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
using PlotNET;

namespace PlotNet.Wpf
{
    /// <summary>
    /// Interaction logic for PlotControl.xaml
    /// </summary>
    public partial class PlotControl : UserControl
    {
        public PlotControl()
        {
            InitializeComponent();
        }

        public async void Plot(IPlotter plotter)
        {
            await this.webView.EnsureCoreWebView2Async();
            this.webView.NavigateToString(plotter.ShowAsHtml((int)ActualWidth, (int)ActualHeight));
        }
    }
}