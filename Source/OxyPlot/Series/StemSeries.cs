﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StemSeries.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Represents a series that plots discrete data in a stem plot.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Series
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a series that plots discrete data in a stem plot.
    /// </summary>
    /// <remarks>See <a href="http://en.wikipedia.org/wiki/Stemplot">Stem plot</a> and
    /// <a href="http://www.mathworks.com/help/techdoc/ref/stem.html">stem</a>.</remarks>
    public class StemSeries : LineSeries
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "StemSeries" /> class.
        /// </summary>
        public StemSeries()
        {
            this.Base = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StemSeries" /> class.
        /// </summary>
        /// <param name="title">The title.</param>
        [Obsolete]
        public StemSeries(string title)
            : base(title)
        {
            this.Title = title;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StemSeries" /> class.
        /// </summary>
        /// <param name="color">The color of the line stroke.</param>
        /// <param name="strokeThickness">The stroke thickness (optional).</param>
        /// <param name="title">The title (optional).</param>
        [Obsolete]
        public StemSeries(OxyColor color, double strokeThickness = 1, string title = null)
            : base(color, strokeThickness, title)
        {
        }

        /// <summary>
        /// Gets or sets Base.
        /// </summary>
        public double Base { get; set; }

        /// <summary>
        /// Gets the point on the series that is nearest the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="interpolate">Interpolate the series if this flag is set to <c>true</c>.</param>
        /// <returns>A TrackerHitResult for the current hit.</returns>
        public override TrackerHitResult GetNearestPoint(ScreenPoint point, bool interpolate)
        {
            if (this.XAxis == null || this.YAxis == null)
            {
                return null;
            }

            if (interpolate)
            {
                return null;
            }

            TrackerHitResult result = null;

            // http://paulbourke.net/geometry/pointlineplane/
            double minimumDistance = double.MaxValue;
            var points = this.ActualPoints;

            for (int i = 0; i < points.Count; i++)
            {
                var p1 = points[i];
                var basePoint = new DataPoint(p1.X, this.Base);
                var sp1 = this.Transform(p1);
                var sp2 = this.Transform(basePoint);
                var u = ScreenPointHelper.FindPositionOnLine(point, sp1, sp2);

                if (double.IsNaN(u))
                {
                    continue;
                }

                if (u < 0 || u > 1)
                {
                    continue; // outside line
                }

                var sp = sp1 + ((sp2 - sp1) * u);
                double distance = (point - sp).LengthSquared;

                if (distance < minimumDistance)
                {
                    result = new TrackerHitResult(
                        this, new DataPoint(p1.X, p1.Y), new ScreenPoint(sp1.x, sp1.y), this.GetItem(i));
                    minimumDistance = distance;
                }
            }

            return result;
        }

        /// <summary>
        /// Renders the LineSeries on the specified rendering context.
        /// </summary>
        /// <param name="rc">The rendering context.</param>
        /// <param name="model">The owner plot model.</param>
        public override void Render(IRenderContext rc, PlotModel model)
        {
            if (this.ActualPoints.Count == 0)
            {
                return;
            }

            this.VerifyAxes();

            double minDistSquared = this.MinimumSegmentLength * this.MinimumSegmentLength;

            var clippingRect = this.GetClippingRect();

            // Transform all points to screen coordinates
            // Render the line when invalid points occur
            var dashArray = this.ActualDashArray;
            var actualColor = this.GetSelectableColor(this.ActualColor);
            var points = new ScreenPoint[2];
            var markerPoints = this.MarkerType != MarkerType.None ? new List<ScreenPoint>(this.ActualPoints.Count) : null;
            foreach (var point in this.ActualPoints)
            {
                if (!this.IsValidPoint(point))
                {
                    continue;
                }

                points[0] = this.Transform(point.X, this.Base);
                points[1] = this.Transform(point.X, point.Y);

                if (this.StrokeThickness > 0 && this.ActualLineStyle != LineStyle.None)
                {
                    rc.DrawClippedLine(
                        clippingRect,
                        points,
                        minDistSquared,
                        actualColor,
                        this.StrokeThickness,
                        dashArray,
                        this.LineJoin,
                        false);
                }

                if (markerPoints != null)
                {
                    markerPoints.Add(points[1]);
                }
            }

            if (this.MarkerType != MarkerType.None)
            {
                rc.DrawMarkers(
                    clippingRect,
                    markerPoints,
                    this.MarkerType,
                    this.MarkerOutline,
                    new[] { this.MarkerSize },
                    this.MarkerFill,
                    this.MarkerStroke,
                    this.MarkerStrokeThickness);
            }
        }
    }
}