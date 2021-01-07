using System.Windows.Controls;
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
        public async void PlotData(object data)
        {
            await this.webView.EnsureCoreWebView2Async();
            this.webView.NavigateToString(new Plotter().ShowAsHtml((int)ActualWidth, (int)ActualHeight, data));
        }
    }
}