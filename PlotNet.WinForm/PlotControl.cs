using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlotNET;

namespace WinformPlot
{
    public partial class PlotControl : UserControl
    {
        public PlotControl()
        {
            InitializeComponent();
        }

        public async void Plot(IPlotter plotter)
        {
            await this.webView.EnsureCoreWebView2Async();
            this.webView.NavigateToString(plotter.ShowAsHtml(this.Width, this.Height));
        }
    }
}
