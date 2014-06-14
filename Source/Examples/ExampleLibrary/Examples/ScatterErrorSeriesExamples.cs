﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScatterErrorSeriesExamples.cs" company="OxyPlot">
//   The MIT License (MIT)
//   
//   Copyright (c) 2014 OxyPlot contributors
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
// <summary>
//   Defines the ScatterErrorSeriesExamples examples.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ExampleLibrary
{
    using System;

    using OxyPlot;
    using OxyPlot.Series;

    [Examples("ScatterErrorSeries")]
    public class ScatterErrorSeriesExamples : ExamplesBase
    {
        [Example("Random points and errors")]
        public static PlotModel RandomPointsAndError()
        {
            const int N = 20;
            var model = new PlotModel { Title = string.Format("Random data (n={0})", N), LegendPosition = LegendPosition.LeftTop };

            var s1 = new ScatterErrorSeries { Title = "Measurements" };
            var random = new Random(27);
            double x = 0;
            double y = 0;
            for (int i = 0; i < N; i++)
            {
                x += 2 + random.NextDouble();
                y += 1 + random.NextDouble();

                var p = new ScatterErrorPoint(x, y, random.NextDouble(), random.NextDouble());
                s1.Points.Add(p);
            }

            model.Series.Add(s1);
            return model;
        }
    }
}
