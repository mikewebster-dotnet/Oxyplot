﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright filename="GraphicsRenderContext.cs" company="OxyPlot">
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
namespace OxyPlot.Core.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// The graphics render context.
    /// </summary>
    internal class GraphicsRenderContext : RenderContextBase, IDisposable
    {
        private readonly HashSet<OxyImage> imagesInUse = new HashSet<OxyImage>();
        private readonly Dictionary<OxyImage, Image> imageCache = new Dictionary<OxyImage, Image>();

        /// <summary>
        /// The font size factor.
        /// </summary>
        private const float FontsizeFactor = 0.8f;

        /// <summary>
        /// The GDI+ drawing surface.
        /// </summary>
        private Graphics g;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicsRenderContext"/> class.
        /// </summary>
        public GraphicsRenderContext(Graphics g)
        {
            this.g = g;
        }

        /// <summary>
        /// Sets the graphics target.
        /// </summary>
        /// <param name="graphics">The graphics surface.</param>
        public void SetGraphicsTarget(Graphics graphics)
        {
            this.g = graphics;
        }

        /// <inheritdoc/>
        public override void DrawEllipse(OxyRect rect, OxyColor fill, OxyColor stroke, double thickness, EdgeRenderingMode edgeRenderingMode)
        {
            this.SetSmoothingMode(this.ShouldUseAntiAliasingForEllipse(edgeRenderingMode));
            if (fill != null)
            {
                this.g.FillEllipse(fill.ToBrush(), (float)rect.Left, (float)rect.Top, (float)rect.Width, (float)rect.Height);
            }

            if (stroke == null || thickness <= 0)
            {
                return;
            }

            using (var pen = new Pen(stroke.ToColor(), (float)thickness))
            {
                this.g.SmoothingMode = SmoothingMode.HighQuality;
                this.g.DrawEllipse(pen, (float)rect.Left, (float)rect.Top, (float)rect.Width, (float)rect.Height);
            }
        }

        /// <inheritdoc/>
        public override void DrawLine(
           IList<ScreenPoint> points,
           OxyColor stroke,
           double thickness,
           EdgeRenderingMode edgeRenderingMode,
           double[] dashArray,
           OxyPlot.LineJoin lineJoin)
        {
            if (stroke == null || thickness <= 0 || points.Count < 2)
            {
                return;
            }

            this.SetSmoothingMode(this.ShouldUseAntiAliasingForLine(edgeRenderingMode, points));
            using (var pen = new Pen(stroke.ToColor(), (float)thickness))
            {

                if (dashArray != null)
                {
                    pen.DashPattern = this.ToFloatArray(dashArray);
                }

                switch (lineJoin)
                {
                    case OxyPlot.LineJoin.Round:
                        pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                        break;
                    case OxyPlot.LineJoin.Bevel:
                        pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;
                        break;

                        // The default LineJoin is Miter
                }

                this.g.DrawLines(pen, this.ToPoints(points));
            }
        }

        /// <inheritdoc/>
        public override void DrawPolygon(
           IList<ScreenPoint> points,
           OxyColor fill,
           OxyColor stroke,
           double thickness,
           EdgeRenderingMode edgeRenderingMode,
           double[] dashArray,
           OxyPlot.LineJoin lineJoin)
        {
            if (points.Count < 2)
            {
                return;
            }

            this.SetSmoothingMode(this.ShouldUseAntiAliasingForLine(edgeRenderingMode, points));

            var pts = this.ToPoints(points);
            if (fill != null)
            {
                this.g.FillPolygon(fill.ToBrush(), pts);
            }

            if (stroke != null && thickness > 0)
            {
                using (var pen = new Pen(stroke.ToColor(), (float)thickness))
                {

                    if (dashArray != null)
                    {
                        pen.DashPattern = this.ToFloatArray(dashArray);
                    }

                    switch (lineJoin)
                    {
                        case OxyPlot.LineJoin.Round:
                            pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                            break;
                        case OxyPlot.LineJoin.Bevel:
                            pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;
                            break;

                            // The default LineJoin is Miter
                    }

                    this.g.DrawPolygon(pen, pts);
                }
            }
        }

        /// <inheritdoc/>
        public override void DrawRectangle(OxyRect rect, OxyColor fill, OxyColor stroke, double thickness, EdgeRenderingMode edgeRenderingMode)
        {
            this.SetSmoothingMode(this.ShouldUseAntiAliasingForRect(edgeRenderingMode));
            if (fill != null)
            {
                this.g.FillRectangle(
               fill.ToBrush(), (float)rect.Left, (float)rect.Top, (float)rect.Width, (float)rect.Height);
            }

            if (stroke == null || thickness <= 0)
            {
                return;
            }

            using (var pen = new Pen(stroke.ToColor(), (float)thickness))
            {
                this.g.DrawRectangle(pen, (float)rect.Left, (float)rect.Top, (float)rect.Width, (float)rect.Height);
            }
        }

        /// <summary>
        /// Draws the text.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="text">The text.</param>
        /// <param name="fill">The fill color.</param>
        /// <param name="fontFamily">The font family.</param>
        /// <param name="fontSize">Size of the font.</param>
        /// <param name="fontWeight">The font weight.</param>
        /// <param name="rotate">The rotation angle.</param>
        /// <param name="halign">The horizontal alignment.</param>
        /// <param name="valign">The vertical alignment.</param>
        /// <param name="maxSize">The maximum size of the text.</param>
        public override void DrawText(
           ScreenPoint p,
           string text,
           OxyColor fill,
           string fontFamily,
           double fontSize,
           double fontWeight,
           double rotate,
           HorizontalAlignment halign,
           VerticalAlignment valign,
           OxySize? maxSize)
        {
            var fs = FontStyle.Regular;
            if (fontWeight >= 700)
            {
                fs = FontStyle.Bold;
            }

            using (var font = new Font(fontFamily, (float)fontSize * FontsizeFactor, fs))
            {
                using (var sf = new StringFormat { Alignment = StringAlignment.Near })
                {
                    var size = this.g.MeasureString(text, font);
                    if (maxSize != null)
                    {
                        if (size.Width > maxSize.Value.Width)
                        {
                            size.Width = (float)maxSize.Value.Width;
                        }

                        if (size.Height > maxSize.Value.Height)
                        {
                            size.Height = (float)maxSize.Value.Height;
                        }
                    }

                    float dx = 0;
                    if (halign == HorizontalAlignment.Center)
                    {
                        dx = -size.Width / 2;
                    }

                    if (halign == HorizontalAlignment.Right)
                    {
                        dx = -size.Width;
                    }

                    float dy = 0;
                    sf.LineAlignment = StringAlignment.Near;
                    if (valign == VerticalAlignment.Middle)
                    {
                        dy = -size.Height / 2;
                    }

                    if (valign == VerticalAlignment.Bottom)
                    {
                        dy = -size.Height;
                    }

                    this.g.TranslateTransform((float)p.X, (float)p.Y);
                    if (Math.Abs(rotate) > double.Epsilon)
                    {
                        this.g.RotateTransform((float)rotate);
                    }

                    this.g.TranslateTransform(dx, dy);

                    var layoutRectangle = new RectangleF(0, 0, size.Width, size.Height);
                    this.g.DrawString(text, font, fill.ToBrush(), layoutRectangle, sf);

                    this.g.ResetTransform();
                }
            }
        }

        /// <summary>
        /// Measures the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fontFamily">The font family.</param>
        /// <param name="fontSize">Size of the font.</param>
        /// <param name="fontWeight">The font weight.</param>
        /// <returns>The text size.</returns>
        public override OxySize MeasureText(string text, string fontFamily, double fontSize, double fontWeight)
        {
            if (text == null)
            {
                return OxySize.Empty;
            }

            var fs = FontStyle.Regular;
            if (fontWeight >= 700)
            {
                fs = FontStyle.Bold;
            }

            using (var font = new Font(fontFamily, (float)fontSize * FontsizeFactor, fs))
            {
                var size = this.g.MeasureString(text, font);
                return new OxySize(size.Width, size.Height);
            }
        }

        /// <inheritdoc />
        public override bool SetClip(OxyRect rect)
        {
            this.g.SetClip(rect.ToRect());
            return true;
        }

        /// <inheritdoc />
        public override void ResetClip()
        {
            this.g.ResetClip();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // dispose images
            foreach (var i in this.imageCache)
            {
                i.Value.Dispose();
            }
        }

        /// <summary>
        /// Converts a double array to a float array.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <returns>
        /// The float array.
        /// </returns>
        private float[] ToFloatArray(double[] a)
        {
            if (a == null)
            {
                return null;
            }

            var r = new float[a.Length];
            for (var i = 0; i < a.Length; i++)
            {
                r[i] = (float)a[i];
            }

            return r;
        }

        /// <summary>
        /// Converts a list of point to an array of PointF.
        /// </summary>
        /// <param name="points">
        /// The points.
        /// </param>
        /// <returns>
        /// An array of points.
        /// </returns>
        private PointF[] ToPoints(IList<ScreenPoint> points)
        {
            if (points == null)
            {
                return null;
            }

            var r = new PointF[points.Count()];
            var i = 0;
            foreach (var p in points)
            {
                r[i++] = new PointF((float)p.X, (float)p.Y);
            }

            return r;
        }

        /// <inheritdoc />
        public override void CleanUp()
        {
            var imagesToRelease = this.imageCache.Keys.Where(i => !this.imagesInUse.Contains(i));
            foreach (var i in imagesToRelease)
            {
                var image = this.GetImage(i);
                image.Dispose();
                this.imageCache.Remove(i);
            }

            this.imagesInUse.Clear();
        }

        private Image GetImage(OxyImage source)
        {
            if (source == null)
            {
                return null;
            }

            if (!this.imagesInUse.Contains(source))
            {
                this.imagesInUse.Add(source);
            }

            if (this.imageCache.TryGetValue(source, out var src))
            {
                return src;
            }

            if (source != null)
            {
                Image btm;
                using (var ms = new MemoryStream(source.GetData()))
                {
                    btm = Image.FromStream(ms);
                }

                this.imageCache.Add(source, btm);
                return btm;
            }

            return null;
        }

        /// <summary>
        /// Sets the smoothing mode.
        /// </summary>
        /// <param name="useAntiAliasing">A value indicating whether to use Anti-Aliasing.</param>
        private void SetSmoothingMode(bool useAntiAliasing)
        {
            this.g.SmoothingMode = useAntiAliasing ? SmoothingMode.HighQuality : SmoothingMode.None;
        }

    }
}
