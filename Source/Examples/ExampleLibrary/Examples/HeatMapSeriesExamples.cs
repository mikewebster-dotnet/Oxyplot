﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContourSeriesExamples.cs" company="OxyPlot">
//   The MIT License (MIT)
//
//   Copyright (c) 2012 Oystein Bjorke
//
//   Permission is hereby granted, free of charge, to any person obtaining a
//   copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//
//   The above copyright notice and this permission notice shall be included
//   in all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
//   OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//   IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
//   CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//   TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
//   SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using OxyPlot;

namespace ExampleLibrary
{
    using System;

    using OxyPlot.Axes;
    using OxyPlot.Series;

    [Examples("HeatMapSeries")]
    public class HeatMapSeriesExamples : ExamplesBase
    {
        [Example("Peaks")]
        public static PlotModel Peaks()
        {
            double x0 = -3.1;
            double x1 = 3.1;
            double y0 = -3;
            double y1 = 3;
            Func<double, double, double> peaks = (x, y) => 3 * (1 - x) * (1 - x) * Math.Exp(-(x * x) - (y + 1) * (y + 1))
               - 10 * (x / 5 - x * x * x - y * y * y * y * y) * Math.Exp(-x * x - y * y)
               - 1.0 / 3 * Math.Exp(-(x + 1) * (x + 1) - y * y);
            var xvalues = ArrayHelper.CreateVector(x0, x1, 100);
            var yvalues = ArrayHelper.CreateVector(y0, y1, 100);
            var peaksData = ArrayHelper.Evaluate(peaks, xvalues, yvalues);

            var model = new PlotModel("Peaks");
            model.Axes.Add(new ColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(500), HighColor = OxyColors.Gray, LowColor = OxyColors.Black });

            var hms = new HeatMapSeries { X0 = x0, X1 = x1, Y0 = y0, Y1 = y1, Data = peaksData };
            model.Series.Add(hms);
            var cs = new ContourSeries
                {
                    Color = OxyColors.Black,
                    FontSize = 0,
                    ContourLevelStep = 1,
                    LabelBackground = null,
                    ColumnCoordinates = yvalues,
                    RowCoordinates = xvalues,
                    Data = peaksData
                };
            model.Series.Add(cs);
            return model;
        }

        [Example("Coordinates")]
        public static PlotModel Coordinates()
        {
            var data = new double[2, 3];
            data[0, 0] = 0;
            data[0, 1] = 0.2;
            data[0, 2] = 0.4;
            data[1, 0] = 0.1;
            data[1, 1] = 0.3;
            data[1, 2] = 0.2;

            var model = new PlotModel("Coordinates example", "Bounding box should be [0,2] and [0,3]");
            model.Axes.Add(new ColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(500), HighColor = OxyColors.Gray, LowColor = OxyColors.Black });

            // adding half cellwidth/cellheight to bounding box coordinates
            var hms = new HeatMapSeries { X0 = 0.5, X1 = 1.5, Y0 = 0.5, Y1 = 2.5, Data = data };
            model.Series.Add(hms);
            return model;
        }

        [Example("Not interpolated")]
        public static PlotModel NotInterpolated()
        {
            var model = Coordinates();
            model.Title = "Not interpolated values";
            var hms = (HeatMapSeries)model.Series[0];
            hms.Interpolate = false;
            return model;
        }
    }
}