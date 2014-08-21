﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Decimator.cs" company="OxyPlot">
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
//   Provides functionality to decimate lines.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides functionality to decimate lines.
    /// </summary>
    public class Decimator
    {
        /// <summary>
        /// Decimates lines by reducing all points that have the same integer x value to a maximum of 4 points (first, min, max, last).
        /// </summary>
        /// <param name="input">The input points.</param>
        /// <param name="output">The decimated points.</param>
        public static void Decimate(List<ScreenPoint> input, List<ScreenPoint> output)
        {
            if (input == null || input.Count == 0)
            {
                return;
            }

            var point = input[0];
            var currentX = Math.Round(point.X);
            var currentMinY = Math.Round(point.Y);
            var currentMaxY = currentMinY;
            var currentFirstY = currentMinY;
            var currentLastY = currentMinY;
            for (var i = 1; i < input.Count; ++i)
            {
                point = input[i];
                var newX = Math.Round(point.X);
                var newY = Math.Round(point.Y);
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (newX != currentX)
                {
                    AddVerticalPoints(output, currentX, currentFirstY, currentLastY, currentMinY, currentMaxY);
                    currentFirstY = currentLastY = currentMinY = currentMaxY = newY;
                    currentX = newX;
                    continue;
                }

                if (newY < currentMinY)
                {
                    currentMinY = newY;
                }

                if (newY > currentMaxY)
                {
                    currentMaxY = newY;
                }

                currentLastY = newY;
            }

            // Keep from adding an extra point for last
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            currentLastY = currentFirstY == currentMinY ? currentMaxY : currentMinY;
            AddVerticalPoints(output, currentX, currentFirstY, currentLastY, currentMinY, currentMaxY);
        }

        /// <summary>
        /// Adds vertical points to the <paramref name="result" /> list.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="firstY">The first y.</param>
        /// <param name="lastY">The last y.</param>
        /// <param name="minY">The minimum y.</param>
        /// <param name="maxY">The maximum y.</param>
        private static void AddVerticalPoints(
            // ReSharper disable SuggestBaseTypeForParameter
            List<ScreenPoint> result,
            // ReSharper restore SuggestBaseTypeForParameter
            double x,
            double firstY,
            double lastY,
            double minY,
            double maxY)
        {
            result.Add(new ScreenPoint(x, firstY));
            // ReSharper disable CompareOfFloatsByEqualityOperator
            if (firstY == minY)
            {
                if (minY != maxY)
                {
                    result.Add(new ScreenPoint(x, maxY));
                }

                if (maxY != lastY)
                {
                    result.Add(new ScreenPoint(x, lastY));
                }

                return;
            }

            if (firstY == maxY)
            {
                if (maxY != minY)
                {
                    result.Add(new ScreenPoint(x, minY));
                }

                if (minY != lastY)
                {
                    result.Add(new ScreenPoint(x, lastY));
                }

                return;
            }

            if (lastY == minY)
            {
                if (minY != maxY)
                {
                    result.Add(new ScreenPoint(x, maxY));
                }
            }
            else if (lastY == maxY)
            {
                if (maxY != minY)
                {
                    result.Add(new ScreenPoint(x, minY));
                }
            }
            else
            {
                result.Add(new ScreenPoint(x, minY));
                result.Add(new ScreenPoint(x, maxY));
            }
            // ReSharper restore CompareOfFloatsByEqualityOperator
            result.Add(new ScreenPoint(x, lastY));
        }
    }
}