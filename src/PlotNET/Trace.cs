using System.Collections.Generic;
using System.Linq;

namespace PlotNET
{
    public class Trace
    {
        public Trace(string[] labels, float[] yValues, ChartType type)
        {
            Labels = labels;
            YValues = yValues;
            Type = type;
        }

        public Trace(string[] labels, int[] yValues, ChartType type)
            : this(labels, yValues.Select(v => (float)v).ToArray(), type)
        {

        }

        public Trace(decimal[] xValues, decimal[] yValues, ChartType type)
            : this(xValues.Cast<float>().ToArray(), yValues.Cast<float>().ToArray(), type)
        {

        }

        public Trace(float[] xValues, float[] yValues, ChartType type)
        {
            XValues = xValues;
            YValues = yValues;
            Type = type;
        }

        public string Name { get; set; }

        public string Mode { get; set; }

        public float[] XValues { get; set; }

        public float[] YValues { get; set; }

        public string[] Labels { get; set; }

        public IEnumerable<(string name, object value)> Params { get; set; }

        public ChartType Type { get; set; }
    }
}
