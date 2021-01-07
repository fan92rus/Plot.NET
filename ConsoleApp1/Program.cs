using System;
using System.Runtime.CompilerServices;
using PlotNET;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var plotter = new Plotter();

            plotter.Plot(
                new[] { "giraffes", "orangutans", "monkeys" },
                new[] { 20f, 14f, 23f },
                "Test Chart", ChartType.Bar);
            
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
