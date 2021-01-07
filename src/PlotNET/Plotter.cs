using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Funny.PlotNET
{
    public interface IStockPlotter
    {
        IStockPlotter Plot(IEnumerable<Ohlc> values);
        IStockPlotter Plot(IEnumerable<LineItem> line);
        IStockPlotter Plot(IEnumerable<Marker> markers);
        string ShowAsHtml(int width, int height);
    }

    public class Ohlc
    {
        public DateTime Time { get; set; }
        [JsonProperty("time")] public long time => Time.Ticks;

        [JsonProperty("open")]
        public double Open { get; set; }

        [JsonProperty("close")]
        public double Close { get; set; }

        [JsonProperty("high")]
        public double High { get; set; }

        [JsonProperty("low")]
        public double Low { get; set; }
    }

    public class LineItem
    {
        [JsonProperty("time")] public long time => Time.Ticks;
        public DateTime Time { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }

    public class Marker
    {
        [JsonProperty("time")] public long time => Time.Ticks;

        public DateTime Time { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("position")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Type Position { get; set; }
        public enum Type
        {
            aboveBar,
            belowBar,
            inBar
        }
    }


    public class StockStockPlotter : IStockPlotter
    {
        private string _jsUrl = "https://unpkg.com/lightweight-charts/dist/lightweight-charts.standalone.production.js";
        private string Script { get; set; }
        private string RenderHeader()
        {
            return $"<head> <script src=\"{_jsUrl}\"></script></head>";
        }

        private string RenderBody(string divClientID)
        {
            return $"<div  style=\"border:0px;width:100%;height:100%;\"  id=\"{divClientID}\"></div>";
        }

        private string RenderJS(string divClientID, object sourceData, int width, int height)
        {

            var strWidth = @",
                    width: " + (width - 20) + "," + @"
                    height: " + (height - 20);
            return $@"<script>
                        const chart = LightweightCharts.createChart(document.getElementById('{divClientID}'));
                        {Script}
                   </script>";
        }

        private int csn = 0;
        public IStockPlotter Plot(IEnumerable<Ohlc> values)
        {
            csn++;

            Script += $@"
            var candleSeries_{csn} = chart.addCandlestickSeries();
            candleSeries_{csn}.setData({JsonConvert.SerializeObject(values)});
            ";
            return this;
        }

        public int lsn = 0;
        public IStockPlotter Plot(IEnumerable<LineItem> line)
        {
            lsn++;

            Script += $@"
            var lineSeries_{lsn} = chart.addLineSeries();
            lineSeries_{lsn}.setData({JsonConvert.SerializeObject(line)});
            ";
            return this;
        }

        public IStockPlotter Plot(IEnumerable<Marker> markers)
        {
            Script += $@"candleSeries_{csn}.setMarkers({JsonConvert.SerializeObject(markers)});";
            return this;
        }

        public string ShowAsHtml(int width, int height)
        {
            var html = GetHtml(width, height, null);
            Script = String.Empty;
            return html;
        }
        public string ShowAsHtml(int width, int height, object data)
        {
            var html = GetHtml(width, height, data);
            Script = String.Empty;
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
