using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace PlotNET
{
    public interface IPlotter
    {
        Plotter Plot(IEnumerable<float> xValues, IEnumerable<float> yValues, string name, ChartType type = ChartType.Bar, string mode = null);
        Plotter Plot(IEnumerable<string> labels, IEnumerable<float> yValues, string name, ChartType type = ChartType.Bar, string mode = null);
        Plotter Plot<T>(IEnumerable<string> labels, IEnumerable<T> yValues, string name, ChartType type = ChartType.Bar, string mode = null, params (string, object)[] @params);
        Plotter Plot<T>(IEnumerable<T> xValues, IEnumerable<T> yValues, string name, ChartType type = ChartType.Bar, string mode = null, params (string, object)[] @params);
        string ShowAsHtml(int width, int height);
    }

    public class Plotter : IPlotter
    {
        private string _jsUrl = "https://cdn.plot.ly/plotly-latest.min.js";

        private List<Trace> _traces = new List<Trace>();

        public Plotter Plot(IEnumerable<float> xValues, IEnumerable<float> yValues, string name, ChartType type = ChartType.Bar, string mode = null)
        {
            _traces.Add(new Trace(xValues.ToArray(), yValues.ToArray(), type)
            {
                Name = name,
                Mode = mode
            });
            return this;
        }

        public Plotter Plot(IEnumerable<string> labels, IEnumerable<float> yValues, string name, ChartType type = ChartType.Bar, string mode = null)
        {
            _traces.Add(new Trace(labels.ToArray(), yValues.ToArray(), type)
            {
                Name = name,
                Mode = mode
            });
            return this;
        }
        public Plotter Plot<T>(IEnumerable<string> labels, IEnumerable<T> yValues, string name, ChartType type = ChartType.Bar, string mode = null, params (string, object)[] @params)
        {
            _traces.Add(new Trace(labels.ToArray(), yValues.Cast<float>().ToArray(), type)
            {
                Name = name,
                Mode = mode,
                Params = @params
            });
            return this;
        }
        public Plotter Plot<T>(IEnumerable<T> xValues, IEnumerable<T> yValues, string name, ChartType type = ChartType.Bar, string mode = null, params (string, object)[] @params)
        {
            _traces.Add(new Trace(xValues.Cast<float>().ToArray(), yValues.Cast<float>().ToArray(), type)
            {
                Name = name,
                Mode = mode,
                Params = @params
            });
            return this;
        }
        public Plotter Plot(Trace trace)
        {
            _traces.Add(trace);
            return this;
        }

        private string RenderHeader()
        {
            return $"<head> <script src=\"{_jsUrl}\"></script></head>";
        }

        private string RenderBody(string divClientID)
        {
            return $"<div  style=\"border:0px;width:100%;height:100%;\"  id=\"{divClientID}\"></div>";
        }

        private string GetDataByTraces()
        {
            return "[" + string.Join(",", _traces.Select(t =>
            {
                var xTexts = t.XValues != null ? string.Join(",", t.XValues) : string.Join(",", t.Labels.Select(l => "'" + l + "'"));
                var nameNode = string.Empty;

                if (!string.IsNullOrEmpty(t.Name))
                {
                    nameNode = @",
                        name: '" + t.Name + "'";
                }

                var modeNode = string.Empty;

                if (!string.IsNullOrEmpty(t.Mode))
                {
                    modeNode = @",
                        mode: '" + t.Mode + "'";
                }


                return $@"{{
                    x: [{xTexts}],
                    y: [{string.Join(",", t.YValues)}],
                    type: '{t.Type.ToString().ToLower()}'{nameNode}{modeNode},
                    {string.Join(",\n", t.Params.Select(x => $"{x.name} : {JsonConvert.SerializeObject(x.value)}\t"))}}}";
            })) + "]";
        }

        private string RenderJS(string divClientID, object sourceData, int width, int height)
        {
            var data = sourceData != null ? JsonConvert.SerializeObject(sourceData) : GetDataByTraces();

            var strWidth = @",
                    width: " + (width - 20) + "," + @"
                    height: " + (height - 20);
            return @"<script>
                        var data = " + data + @";
                        Plotly.newPlot('" + divClientID + @"', data,
                            {
                                margin: { t: 5, l: 30, r: 5, b: 30 }" + strWidth + @"
                            });
                    </script>";
        }

        public string ShowAsHtml(int width, int height)
        {
            var html = GetHtml(width, height, null);
            _traces.Clear();
            return html;
        }
        public string ShowAsHtml(int width, int height, object data)
        {
            var html = GetHtml(width, height, data);
            _traces.Clear();
            return html;
        }
        private string GetHtml(int width, int height, object data)
        {
            var html = RenderHeader();
            var divClientID = "plot-" + Math.Abs(Guid.NewGuid().ToString().GetHashCode());
            return GetHtml(width, height, data, divClientID);
        }
        private string GetHtml(int width, int height, object data, string divClientID)
        {
            var html = RenderHeader();
            html += RenderBody(divClientID);
            html += RenderJS(divClientID, data, width, height);
            return html;
        }
    }
}
