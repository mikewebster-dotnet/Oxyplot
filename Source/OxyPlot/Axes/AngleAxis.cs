﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AngleAxis.cs" company="OxyPlot">
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
// <summary>
//   Represents a angular axis for polar plots.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace OxyPlot
{
    using System;

    /// <summary>
    /// Represents a angular axis for polar plots.
    /// </summary>
    public class AngleAxis : LinearAxis
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AngleAxis"/> class. 
        ///   Initializes a new instance of the <see cref="AngleAxis"/> class.
        /// </summary>
        public AngleAxis()
        {
            this.IsPanEnabled = false;
            this.IsZoomEnabled = false;
            this.MajorGridlineStyle = LineStyle.Solid;
            this.MinorGridlineStyle = LineStyle.Solid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AngleAxis"/> class. 
        /// </summary>
        /// <param name="minimum">
        /// The minimum. 
        /// </param>
        /// <param name="maximum">
        /// The maximum. 
        /// </param>
        /// <param name="majorStep">
        /// The major step. 
        /// </param>
        /// <param name="minorStep">
        /// The minor step. 
        /// </param>
        /// <param name="title">
        /// The title. 
        /// </param>
        public AngleAxis(
            double minimum = double.NaN, 
            double maximum = double.NaN, 
            double majorStep = double.NaN, 
            double minorStep = double.NaN, 
            string title = null)
            : this()
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.MajorStep = majorStep;
            this.MinorStep = minorStep;
            this.Title = title;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Inverse transform the specified screen point.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="yaxis">The y-axis.</param>
        /// <returns>
        /// The data point.
        /// </returns>
        public override DataPoint InverseTransform(double x, double y, Axis yaxis)
        {
            throw new InvalidOperationException("Angle axis should always be the y-axis.");
        }

        /// <summary>
        /// Determines whether the axis is used for X/Y values.
        /// </summary>
        /// <returns>
        /// <c>true</c> if it is an XY axis; otherwise, <c>false</c> . 
        /// </returns>
        public override bool IsXyAxis()
        {
            return false;
        }

        /// <summary>
        /// Renders the axis on the specified render context.
        /// </summary>
        /// <param name="rc">
        /// The render context. 
        /// </param>
        /// <param name="model">
        /// The model. 
        /// </param>
        /// <param name="axisLayer">
        /// The rendering order. 
        /// </param>
        public override void Render(IRenderContext rc, PlotModel model, AxisLayer axisLayer)
        {
            if (this.Layer != axisLayer)
            {
                return;
            }

            var r = new AngleAxisRenderer(rc, model);
            r.Render(this);
        }

        /// <summary>
        /// Transforms the specified point to screen coordinates.
        /// </summary>
        /// <param name="x">
        /// The x value (for the current axis). 
        /// </param>
        /// <param name="y">
        /// The y value. 
        /// </param>
        /// <param name="yaxis">
        /// The y axis. 
        /// </param>
        /// <returns>
        /// The transformed point. 
        /// </returns>
        public override ScreenPoint Transform(double x, double y, Axis yaxis)
        {
            throw new InvalidOperationException("Angle axis should always be the y-axis.");
        }

        #endregion

        #region Methods

        /// <summary>
        /// The update transform.
        /// </summary>
        /// <param name="bounds">
        /// The bounds. 
        /// </param>
        internal override void UpdateTransform(OxyRect bounds)
        {
            double x0 = bounds.Left;
            double x1 = bounds.Right;
            double y0 = bounds.Bottom;
            double y1 = bounds.Top;

            this.ScreenMin = new ScreenPoint(x0, y1);
            this.ScreenMax = new ScreenPoint(x1, y0);

            this.Scale = 2 * Math.PI / (this.ActualMaximum - this.ActualMinimum);
            this.Offset = this.ActualMinimum;
        }

        #endregion
    }
}