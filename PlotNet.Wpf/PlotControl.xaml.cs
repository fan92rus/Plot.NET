using System.Collections.Generic;
using System.Windows.Controls;
using Funny.PlotNET;

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
            this.Plotter = new StockStockPlotter();
        }

        private IStockPlotter Plotter { get; set; }

        public void Add(IEnumerable<Ohlc> ohlc) => Plotter.Plot(ohlc);

        public void Add(IEnumerable<Marker> markers) => Plotter.Plot(markers);

        public void Add(IEnumerable<LineItem> line) => Plotter.Plot(line);

        public async void Plot()
        {
            await this.webView.EnsureCoreWebView2Async();
            var html = this.Plotter.ShowAsHtml((int)ActualWidth, (int)ActualHeight);
            this.webView.NavigateToString(html);
        }

        public async void Clear()
        {
            await this.webView.EnsureCoreWebView2Async();
            this.webView.NavigateToString("");
        }
    }
}