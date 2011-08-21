﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace OxyPlot.Pdf
{
    /// <summary>
    /// PDF Render context using PdfSharp (and SilverPDF for Silverlight)
    /// see http://pdfsharp.codeplex.com
    /// and http://silverpdf.codeplex.com
    /// </summary>
    internal class PdfRenderContext : RenderContextBase
    {
        private readonly PdfDocument doc;
        private readonly XGraphics g;
        private readonly PdfPage page;
        private const double FONTSIZE_FACTOR = 1.0;


        public PdfRenderContext(double width, double height)
        {
            Width = width;
            Height = height;
            doc = new PdfDocument();
            page = new PdfPage { Width = new XUnit(width), Height = new XUnit(height) };
            doc.AddPage(page);
            g = XGraphics.FromPdfPage(page);
        }

        #region IRenderContext Members

        public override void DrawLine(IList<ScreenPoint> points, OxyColor stroke, double thickness, double[] dashArray,
                             OxyPenLineJoin lineJoin, bool aliased)
        {
            if (stroke == null || thickness <= 0)
                return;


            g.SmoothingMode = aliased ? XSmoothingMode.None : XSmoothingMode.HighQuality;
            var pen = new XPen(ToColor(stroke), (float)thickness);

            if (dashArray != null)
                pen.DashPattern = dashArray;
            switch (lineJoin)
            {
                case OxyPenLineJoin.Round:
                    pen.LineJoin = XLineJoin.Round;
                    break;
                case OxyPenLineJoin.Bevel:
                    pen.LineJoin = XLineJoin.Bevel;
                    break;
                //  The default LineJoin is Miter
            }

            g.DrawLines(pen, ToPoints(points));
        }

        public override void DrawPolygon(IList<ScreenPoint> points, OxyColor fill, OxyColor stroke, double thickness,
                                double[] dashArray, OxyPenLineJoin lineJoin, bool aliased)
        {
            g.SmoothingMode = aliased ? XSmoothingMode.None : XSmoothingMode.HighQuality;

            var pts = ToPoints(points);

            if (fill != null)
                g.DrawPolygon(ToBrush(fill), pts, XFillMode.Alternate);

            if (stroke != null && thickness > 0)
            {
                var pen = new XPen(ToColor(stroke), (float)thickness);

                if (dashArray != null)
                    pen.DashPattern = dashArray;

                switch (lineJoin)
                {
                    case OxyPenLineJoin.Round:
                        pen.LineJoin = XLineJoin.Round;
                        break;
                    case OxyPenLineJoin.Bevel:
                        pen.LineJoin = XLineJoin.Bevel;
                        break;
                    //  The default LineJoin is Miter
                }

                g.DrawPolygon(pen, pts);
            }
        }
      
        public override void DrawText(ScreenPoint p, string text, OxyColor fill, string fontFamily, double fontSize,
                             double fontWeight, double rotate, HorizontalTextAlign halign, VerticalTextAlign valign)
        {
            if (text == null)
                return;
            var fs = XFontStyle.Regular;
            if (fontWeight > FontWeights.Normal)
                fs = XFontStyle.Bold;
            var font = new XFont(fontFamily, (float)fontSize * FONTSIZE_FACTOR, fs);

            var sf = new XStringFormat { Alignment = XStringAlignment.Near };

            var size = g.MeasureString(text, font);

            double dx = 0;
            if (halign == HorizontalTextAlign.Center)
            {
                dx = -size.Width / 2;
            }
            if (halign == HorizontalTextAlign.Right)
            {
                dx = -size.Width;
            }

            double dy = 0;
            sf.LineAlignment = XLineAlignment.Near;
            if (valign == VerticalTextAlign.Middle)
            {
                dy = -size.Height / 2;
            }
            if (valign == VerticalTextAlign.Bottom)
            {
                dy = -size.Height;
            }

            var state = g.Save();
            g.TranslateTransform(dx, dy);
            if (rotate != 0)
                g.RotateTransform((float)rotate);
            g.TranslateTransform((float)p.X, (float)p.Y);

            g.DrawString(text, font, ToBrush(fill), 0, 0, sf);
            g.Restore(state);
        }

        public override OxySize MeasureText(string text, string fontFamily, double fontSize, double fontWeight)
        {
            if (text == null)
                return OxySize.Empty;
            var fs = XFontStyle.Regular;
            if (fontWeight > FontWeights.Normal)
                fs = XFontStyle.Bold;
            var font = new XFont(fontFamily, (float)fontSize * FONTSIZE_FACTOR, fs);
            var size = g.MeasureString(text, font);
            return new OxySize(size.Width, size.Height);
        }

        public override void DrawRectangle(OxyRect rect, OxyColor fill, OxyColor stroke, double thickness)
        {
            if (fill != null)
                g.DrawRectangle(ToBrush(fill), rect.Left, rect.Top, rect.Width, rect.Height);

            if (stroke != null && thickness > 0)
            {
                var pen = new XPen(ToColor(stroke), (float)thickness);
                g.DrawRectangle(pen, rect.Left, rect.Top, rect.Width, rect.Height);
            }
        }

        public override void DrawEllipse(OxyRect rect, OxyColor fill, OxyColor stroke, double thickness)
        {
            if (fill != null)
                g.DrawEllipse(ToBrush(fill), rect.Left, rect.Top, rect.Width, rect.Height);

            if (stroke != null && thickness > 0)
            {
                var pen = new XPen(ToColor(stroke), (float)thickness);
                g.DrawEllipse(pen, rect.Left, rect.Top, rect.Width, rect.Height);
            }
        }


        #endregion

        private static XPoint[] ToPoints(IList<ScreenPoint> points)
        {
            if (points == null)
                return null;
            var r = new XPoint[points.Count()];
            int i = 0;
            foreach (ScreenPoint p in points)
                r[i++] = new XPoint((float)p.X, (float)p.Y);
            return r;
        }

        private static XColor ToColor(OxyColor c)
        {
            return XColor.FromArgb(c.A, c.R, c.G, c.B);
        }

        private static XBrush ToBrush(OxyColor fill)
        {
            if (fill != null)
                return new XSolidBrush(ToColor(fill));
            return null;
        }

        public void Save(Stream s)
        {
            doc.Save(s);
        }
    }
}