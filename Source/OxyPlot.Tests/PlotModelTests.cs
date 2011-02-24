﻿using System;
using NUnit.Framework;

namespace OxyPlot.Tests
{
    [TestFixture]
    public class PlotModelTests
    {
        [Test]
        public void Render_SimplePlot_ValidSvgFile()
        {
            var plot = new PlotModel();
            plot.Title = "Test1";
            plot.Subtitle = "subtitle";
            var ls = new LineSeries();
            for (double i = 0; i < 30; i += 0.1)
                ls.Points.Add(new DataPoint(i, Math.Sin(i) * 20));
            plot.Series.Add(ls);
            plot.Axes.Add(new LinearAxis { Title = "Y-axis1", Position = AxisPosition.Left });
            plot.Axes.Add(new LinearAxis { Title = "X-axis1", Position = AxisPosition.Bottom });
            plot.Axes.Add(new LinearAxis { Title = "Y-axis2", Position = AxisPosition.Right });
            plot.Axes.Add(new LinearAxis { Title = "X-axis2", Position = AxisPosition.Top });

            using (var svgrc = new SvgRenderContext("test.svg", 1200, 800))
            {
                plot.UpdateData();
                plot.Render(svgrc);
            }
            // todo: validate SVG
        }

        [Test]
        public void B11_Backgrounds()
        {
            var plot = new PlotModel("Backgrounds");
            plot.Axes.Add(new LinearAxis(AxisPosition.Bottom, "X-axis"));
            var yaxis1 = new LinearAxis(AxisPosition.Left, "Y1") { StartPosition = 0, EndPosition = 0.5 };
            var yaxis2 = new LinearAxis(AxisPosition.Left, "Y2") { StartPosition = 0.5, EndPosition = 1 };
            plot.Axes.Add(yaxis1);
            plot.Axes.Add(yaxis2);

            var ls = new LineSeries { Background = OxyColors.LightSeaGreen, YAxis = yaxis1 };
            AddExamplePoints(ls);
            plot.Series.Add(ls);

            var ls2 = new LineSeries { Background = OxyColors.LightSkyBlue, YAxis = yaxis2 };
            AddExamplePoints(ls2);
            plot.Series.Add(ls2);

            OxyAssert.AreEqual(plot);
        }

        private void AddExamplePoints(DataSeries ls)
        {
            ls.Points.Add(new DataPoint(3, 13));
            ls.Points.Add(new DataPoint(10, 47));
            ls.Points.Add(new DataPoint(30, 23));
            ls.Points.Add(new DataPoint(40, 65));
            ls.Points.Add(new DataPoint(80, 10));
        }
    }
}
