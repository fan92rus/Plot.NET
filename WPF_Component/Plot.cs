using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Wpf;
using PlotNET;

namespace WPF_Component
{
    public partial class Plot : WebView2
    {
        public Plot()
        {
            InitializeComponent();
        }

        public Plot(IContainer container)
        {
            this.components = container;
            InitializeComponent();
        }

        public async Task Show(IPlotter plotter)
        {
            try
            {
                await Show(plotter.ShowAsHtml());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public async Task Show(string html)
        {
            await EnsureCoreWebView2Async();
            this.NavigateToString(html);
        }
    }
}
