//-----------------------------------------------------------------------
// <copyright file="SilverlightRenderContext.cs" company="OxyPlot">
//     http://oxyplot.codeplex.com, license: Ms-PL
// </copyright>
//-----------------------------------------------------------------------

namespace OxyPlot.Silverlight
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    using FontWeights = OxyPlot.FontWeights;

    /// <summary>
    /// Rendering Silverlight shapes to a Canvas
    /// </summary>
    public class SilverlightRenderContext : IRenderContext
    {
        #region Constants and Fields

        /// <summary>
        /// The brush cache.
        /// </summary>
        private readonly Dictionary<OxyColor, Brush> brushCache = new Dictionary<OxyColor, Brush>();

        /// <summary>
        /// The canvas.
        /// </summary>
        private readonly Canvas canvas;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SilverlightRenderContext"/> class.
        /// </summary>
        /// <param name="canvas">
        /// The canvas.
        /// </param>
        public SilverlightRenderContext(Canvas canvas)
        {
            this.canvas = canvas;
            this.Width = canvas.ActualWidth;
            this.Height = canvas.ActualHeight;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>The height.</value>
        public double Height { get; private set; }

        /// <summary>
        /// Gets a value indicating whether to paint the background.
        /// </summary>
        /// <value>
        /// <c>true</c> if the background should be painted; otherwise, <c>false</c>.
        /// </value>
        public bool PaintBackground
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>The width.</value>
        public double Width { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// The draw ellipse.
        /// </summary>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <param name="fill">
        /// The fill.
        /// </param>
        /// <param name="stroke">
        /// The stroke.
        /// </param>
        /// <param name="thickness">
        /// The thickness.
        /// </param>
        public void DrawEllipse(OxyRect rect, OxyColor fill, OxyColor stroke, double thickness)
        {
            var el = new Ellipse();
            if (stroke != null)
            {
                el.Stroke = new SolidColorBrush(stroke.ToColor());
                el.StrokeThickness = thickness;
            }

            if (fill != null)
            {
                el.Fill = new SolidColorBrush(fill.ToColor());
            }

            el.Width = rect.Width;
            el.Height = rect.Height;
            Canvas.SetLeft(el, rect.Left);
            Canvas.SetTop(el, rect.Top);
            this.canvas.Children.Add(el);
        }

        /// <summary>
        /// The draw ellipses.
        /// </summary>
        /// <param name="rectangles">
        /// The rectangles.
        /// </param>
        /// <param name="fill">
        /// The fill.
        /// </param>
        /// <param name="stroke">
        /// The stroke.
        /// </param>
        /// <param name="thickness">
        /// The thickness.
        /// </param>
        public void DrawEllipses(IList<OxyRect> rectangles, OxyColor fill, OxyColor stroke, double thickness)
        {
            var path = new Path();
            this.SetStroke(path, stroke, thickness);
            if (fill != null)
            {
                path.Fill = this.GetCachedBrush(fill);
            }

            var gg = new GeometryGroup { FillRule = FillRule.Nonzero };
            foreach (OxyRect rect in rectangles)
            {
                gg.Children.Add(
                    new EllipseGeometry
                        {
                            Center = new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2), 
                            RadiusX = rect.Width / 2, 
                            RadiusY = rect.Height / 2
                        });
            }

            path.Data = gg;
            this.Add(path);
        }

        /// <summary>
        /// The draw line.
        /// </summary>
        /// <param name="points">
        /// The points.
        /// </param>
        /// <param name="stroke">
        /// The stroke.
        /// </param>
        /// <param name="thickness">
        /// The thickness.
        /// </param>
        /// <param name="dashArray">
        /// The dash array.
        /// </param>
        /// <param name="lineJoin">
        /// The line join.
        /// </param>
        /// <param name="aliased">
        /// The aliased.
        /// </param>
        public void DrawLine(
            IList<ScreenPoint> points, 
            OxyColor stroke, 
            double thickness, 
            double[] dashArray, 
            OxyPenLineJoin lineJoin, 
            bool aliased)
        {
            var e = new Polyline();
            this.SetStroke(e, stroke, thickness, lineJoin, dashArray, aliased);

            var pc = new PointCollection();
            foreach (ScreenPoint p in points)
            {
                pc.Add(p.ToPoint(aliased));
            }

            e.Points = pc;

            this.canvas.Children.Add(e);
        }

        /// <summary>
        /// The draw line segments.
        /// </summary>
        /// <param name="points">
        /// The points.
        /// </param>
        /// <param name="stroke">
        /// The stroke.
        /// </param>
        /// <param name="thickness">
        /// The thickness.
        /// </param>
        /// <param name="dashArray">
        /// The dash array.
        /// </param>
        /// <param name="lineJoin">
        /// The line join.
        /// </param>
        /// <param name="aliased">
        /// The aliased.
        /// </param>
        public void DrawLineSegments(
            IList<ScreenPoint> points, 
            OxyColor stroke, 
            double thickness, 
            double[] dashArray, 
            OxyPenLineJoin lineJoin, 
            bool aliased)
        {
            var path = new Path();
            this.SetStroke(path, stroke, thickness, lineJoin, dashArray, aliased);
            var pg = new PathGeometry();
            for (int i = 0; i + 1 < points.Count; i += 2)
            {
                // if (points[i].Y==points[i+1].Y)
                // {
                // var line = new Line();

                // line.X1 = 0.5+(int)points[i].X;
                // line.X2 = 0.5+(int)points[i+1].X;
                // line.Y1 = 0.5+(int)points[i].Y;
                // line.Y2 = 0.5+(int)points[i+1].Y;
                // SetStroke(line, OxyColors.DarkRed, thickness, lineJoin, dashArray, aliased);
                // Add(line);
                // continue;
                // }
                var figure = new PathFigure { StartPoint = points[i].ToPoint(aliased), IsClosed = false };
                figure.Segments.Add(new LineSegment { Point = points[i + 1].ToPoint(aliased) });
                pg.Figures.Add(figure);
            }

            path.Data = pg;
            this.Add(path);
        }

        /// <summary>
        /// The draw polygon.
        /// </summary>
        /// <param name="points">
        /// The points.
        /// </param>
        /// <param name="fill">
        /// The fill.
        /// </param>
        /// <param name="stroke">
        /// The stroke.
        /// </param>
        /// <param name="thickness">
        /// The thickness.
        /// </param>
        /// <param name="dashArray">
        /// The dash array.
        /// </param>
        /// <param name="lineJoin">
        /// The line join.
        /// </param>
        /// <param name="aliased">
        /// The aliased.
        /// </param>
        public void DrawPolygon(
            IList<ScreenPoint> points, 
            OxyColor fill, 
            OxyColor stroke, 
            double thickness, 
            double[] dashArray, 
            OxyPenLineJoin lineJoin, 
            bool aliased)
        {
            var po = new Polygon();
            this.SetStroke(po, stroke, thickness, lineJoin, dashArray, aliased);

            if (fill != null)
            {
                po.Fill = this.GetCachedBrush(fill);
            }

            var pc = new PointCollection();
            foreach (ScreenPoint p in points)
            {
                pc.Add(p.ToPoint(aliased));
            }

            po.Points = pc;

            this.canvas.Children.Add(po);
        }

        /// <summary>
        /// The draw polygons.
        /// </summary>
        /// <param name="polygons">
        /// The polygons.
        /// </param>
        /// <param name="fill">
        /// The fill.
        /// </param>
        /// <param name="stroke">
        /// The stroke.
        /// </param>
        /// <param name="thickness">
        /// The thickness.
        /// </param>
        /// <param name="dashArray">
        /// The dash array.
        /// </param>
        /// <param name="lineJoin">
        /// The line join.
        /// </param>
        /// <param name="aliased">
        /// The aliased.
        /// </param>
        public void DrawPolygons(
            IList<IList<ScreenPoint>> polygons, 
            OxyColor fill, 
            OxyColor stroke, 
            double thickness, 
            double[] dashArray, 
            OxyPenLineJoin lineJoin, 
            bool aliased)
        {
            var path = new Path();
            this.SetStroke(path, stroke, thickness, lineJoin, dashArray, aliased);
            if (fill != null)
            {
                path.Fill = this.GetCachedBrush(fill);
            }

            var pg = new PathGeometry { FillRule = FillRule.Nonzero };
            foreach (var polygon in polygons)
            {
                var figure = new PathFigure { IsClosed = true };
                bool first = true;
                foreach (ScreenPoint p in polygon)
                {
                    if (first)
                    {
                        figure.StartPoint = p.ToPoint(aliased);
                        first = false;
                    }
                    else
                    {
                        figure.Segments.Add(new LineSegment { Point = p.ToPoint(aliased) });
                    }
                }

                pg.Figures.Add(figure);
            }

            path.Data = pg;
            this.Add(path);
        }

        /// <summary>
        /// Draws a rectangle.
        /// </summary>
        /// <param name="rect">
        /// The rect.
        /// </param>
        /// <param name="fill">
        /// The fill.
        /// </param>
        /// <param name="stroke">
        /// The stroke.
        /// </param>
        /// <param name="thickness">
        /// The thickness.
        /// </param>
        public void DrawRectangle(OxyRect rect, OxyColor fill, OxyColor stroke, double thickness)
        {
            var el = new Rectangle();
            if (stroke != null)
            {
                el.Stroke = new SolidColorBrush(stroke.ToColor());
                el.StrokeThickness = thickness;
            }

            if (fill != null)
            {
                el.Fill = new SolidColorBrush(fill.ToColor());
            }

            el.Width = rect.Width;
            el.Height = rect.Height;
            Canvas.SetLeft(el, rect.Left);
            Canvas.SetTop(el, rect.Top);
            this.canvas.Children.Add(el);
        }

        /// <summary>
        /// Draws a collection of rectangles, where all have the same stroke and fill.
        /// This performs better than calling DrawRectangle multiple times.
        /// </summary>
        /// <param name="rectangles">
        /// The rectangles.
        /// </param>
        /// <param name="fill">
        /// The fill.
        /// </param>
        /// <param name="stroke">
        /// The stroke.
        /// </param>
        /// <param name="thickness">
        /// The thickness.
        /// </param>
        public void DrawRectangles(IList<OxyRect> rectangles, OxyColor fill, OxyColor stroke, double thickness)
        {
            var path = new Path();
            this.SetStroke(path, stroke, thickness);
            if (fill != null)
            {
                path.Fill = this.GetCachedBrush(fill);
            }

            var gg = new GeometryGroup { FillRule = FillRule.Nonzero };
            foreach (OxyRect rect in rectangles)
            {
                gg.Children.Add(new RectangleGeometry { Rect = rect.ToRect(true) });
            }

            path.Data = gg;
            this.Add(path);
        }

        /// <summary>
        /// The draw text.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="fill">
        /// The fill.
        /// </param>
        /// <param name="fontFamily">
        /// The font family.
        /// </param>
        /// <param name="fontSize">
        /// The font size.
        /// </param>
        /// <param name="fontWeight">
        /// The font weight.
        /// </param>
        /// <param name="rotate">
        /// The rotate.
        /// </param>
        /// <param name="halign">
        /// The halign.
        /// </param>
        /// <param name="valign">
        /// The valign.
        /// </param>
        public void DrawText(
            ScreenPoint p, 
            string text, 
            OxyColor fill, 
            string fontFamily, 
            double fontSize, 
            double fontWeight, 
            double rotate, 
            HorizontalTextAlign halign, 
            VerticalTextAlign valign)
        {
            var tb = new TextBlock { Text = text, Foreground = new SolidColorBrush(fill.ToColor()) };

            // tb.SetValue(TextOptions.TextHintingModeProperty, TextHintingMode.Animated);
            if (fontFamily != null)
            {
                tb.FontFamily = new FontFamily(fontFamily);
            }

            if (fontSize > 0)
            {
                tb.FontSize = fontSize;
            }

            tb.FontWeight = GetFontWeight(fontWeight);

            tb.Measure(new Size(1000, 1000));

            double dx = 0;
            if (halign == HorizontalTextAlign.Center)
            {
                dx = -tb.ActualWidth / 2;
            }

            if (halign == HorizontalTextAlign.Right)
            {
                dx = -tb.ActualWidth;
            }

            double dy = 0;
            if (valign == VerticalTextAlign.Middle)
            {
                dy = -tb.ActualHeight / 2;
            }

            if (valign == VerticalTextAlign.Bottom)
            {
                dy = -tb.ActualHeight;
            }

            var transform = new TransformGroup();
            transform.Children.Add(new TranslateTransform { X = (int)dx, Y = (int)dy });
            if (rotate != 0)
            {
                transform.Children.Add(new RotateTransform { Angle = rotate });
            }

            transform.Children.Add(new TranslateTransform { X = (int)p.X, Y = (int)p.Y });
            tb.RenderTransform = transform;

            this.canvas.Children.Add(tb);
        }

        /// <summary>
        /// The measure text.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="fontFamily">
        /// The font family.
        /// </param>
        /// <param name="fontSize">
        /// The font size.
        /// </param>
        /// <param name="fontWeight">
        /// The font weight.
        /// </param>
        /// <returns>
        /// </returns>
        public OxySize MeasureText(string text, string fontFamily, double fontSize, double fontWeight)
        {
            if (string.IsNullOrEmpty(text))
            {
                return OxySize.Empty;
            }

            var tb = new TextBlock { Text = text };

            if (fontFamily != null)
            {
                tb.FontFamily = new FontFamily(fontFamily);
            }

            if (fontSize > 0)
            {
                tb.FontSize = fontSize;
            }

            tb.FontWeight = GetFontWeight(fontWeight);

            tb.Measure(new Size(1000, 1000));

            return new OxySize(tb.ActualWidth, tb.ActualHeight);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The create dash array collection.
        /// </summary>
        /// <param name="dashArray">
        /// The dash array.
        /// </param>
        /// <returns>
        /// </returns>
        private static DoubleCollection CreateDashArrayCollection(IList<double> dashArray)
        {
            var dac = new DoubleCollection();
            foreach (double v in dashArray)
            {
                dac.Add(v);
            }

            return dac;
        }

        /// <summary>
        /// The get font weight.
        /// </summary>
        /// <param name="fontWeight">
        /// The font weight.
        /// </param>
        /// <returns>
        /// </returns>
        private static FontWeight GetFontWeight(double fontWeight)
        {
            return fontWeight > FontWeights.Normal ? System.Windows.FontWeights.Bold : System.Windows.FontWeights.Normal;
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="shape">
        /// The shape.
        /// </param>
        private void Add(Shape shape)
        {
            this.canvas.Children.Add(shape);
        }

        /// <summary>
        /// The get cached brush.
        /// </summary>
        /// <param name="stroke">
        /// The stroke.
        /// </param>
        /// <returns>
        /// </returns>
        private Brush GetCachedBrush(OxyColor stroke)
        {
            Brush brush;
            if (!this.brushCache.TryGetValue(stroke, out brush))
            {
                brush = new SolidColorBrush(stroke.ToColor());
                this.brushCache.Add(stroke, brush);
            }

            return brush;
        }

        /// <summary>
        /// The set stroke.
        /// </summary>
        /// <param name="shape">
        /// The shape.
        /// </param>
        /// <param name="stroke">
        /// The stroke.
        /// </param>
        /// <param name="thickness">
        /// The thickness.
        /// </param>
        /// <param name="lineJoin">
        /// The line join.
        /// </param>
        /// <param name="dashArray">
        /// The dash array.
        /// </param>
        /// <param name="aliased">
        /// The aliased.
        /// </param>
        private void SetStroke(
            Shape shape, 
            OxyColor stroke, 
            double thickness, 
            OxyPenLineJoin lineJoin = OxyPenLineJoin.Miter, 
            double[] dashArray = null, 
            bool aliased = false)
        {
            if (stroke != null && thickness > 0)
            {
                shape.Stroke = this.GetCachedBrush(stroke);

                switch (lineJoin)
                {
                    case OxyPenLineJoin.Round:
                        shape.StrokeLineJoin = PenLineJoin.Round;
                        break;
                    case OxyPenLineJoin.Bevel:
                        shape.StrokeLineJoin = PenLineJoin.Bevel;
                        break;

                        // The default StrokeLineJoin is Miter
                }

                if (thickness != 1)
                {
                    // default values is 1
                    shape.StrokeThickness = thickness;
                }

                if (dashArray != null)
                {
                    shape.StrokeDashArray = CreateDashArrayCollection(dashArray);
                }
            }

            // shape.UseLayoutRounding = aliased;
        }

        #endregion
    }
}
