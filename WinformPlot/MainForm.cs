using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlotNET;

namespace WinformPlot
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void webView21_Click(object sender, EventArgs e)
        {
        }

        public void ShowPlot(IPlotter plotter)
        {
            Task.WaitAll(Show(plotter.ShowAsHtml()));
        }
        private async Task Show(string html)
        {
            //await webView.EnsureCoreWebView2Async();
            //this.webView.NavigateToString(html);
        }
    }
}
