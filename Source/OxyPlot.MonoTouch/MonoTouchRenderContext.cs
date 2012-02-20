using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace OxyPlot.MonoTouch
{
	public class MonoTouchRenderContext : RenderContextBase
	{
		private CGContext gctx;
		
		public MonoTouchRenderContext (CGContext context, System.Drawing.RectangleF rect)
		{
			gctx = context;
			
			Height = rect.Height;
			Width = rect.Width;
			PaintBackground = true;
		}
		
		private UIColor ToColor(OxyColor c)
        {
            return UIColor.FromRGBA(c.R, c.G, c.B, c.A);
        }
		
		private RectangleF ToRectangle(OxyRect rect)
        {
            return new RectangleF((int)rect.Left, (int)rect.Top, (int)rect.Width, (int)rect.Height);
        }

		private void SetFill (OxyColor color)
		{
			if(color == null) return;
			ToColor(color).SetFill();
		}
		
		private void SetStroke (OxyColor color)
		{
			if(color == null) return;
			ToColor(color).SetStroke();
		}
		
		private void SetAttributes (OxyColor fill, OxyColor stroke, double thickness)
		{
			gctx.SetLineWidth ((float)thickness);
			SetFill(fill);
			SetStroke (stroke);
		}
		
		private PointF ToPoint (ScreenPoint p)
		{
			return new PointF((float)p.X, (float)p.Y);
		}
		
		#region IRenderContext implementation
		public override void DrawEllipse (OxyRect rect, OxyColor fill, OxyColor stroke, double thickness)
		{
			if (fill != null)
            {
				ToColor(fill).SetFill();
                var path = new CGPath ();
	            path.AddElipseInRect(ToRectangle(rect));
	            
	            gctx.AddPath (path);
	            gctx.DrawPath (CGPathDrawingMode.Fill);
            }

            if (stroke == null || thickness <= 0)
            {
                return;
            }
			
			SetAttributes (null, stroke, thickness);
			
			var path2 = new CGPath ();
            path2.AddElipseInRect(ToRectangle(rect));
            
            gctx.AddPath (path2);
            gctx.DrawPath (CGPathDrawingMode.Stroke);
		}

		public override void DrawLine (IList<ScreenPoint> points, OxyColor stroke, double thickness, double[] dashArray, OxyPenLineJoin lineJoin, bool aliased)
		{
			if (stroke == null || thickness <= 0)
            {
                return;
            }
			gctx.SetAllowsAntialiasing(aliased);
			gctx.SetLineCap(ToLine(lineJoin));
			
			SetAttributes (null, stroke, thickness);
            
            var path = new CGPath ();
			
            path.AddLines(points.Select(p => ToPoint(p)).ToArray());
			
            gctx.AddPath (path);
            gctx.DrawPath (CGPathDrawingMode.Stroke);
		}

		private CGLineCap ToLine (OxyPenLineJoin lineJoin)
		{
			switch(lineJoin)
			{
			case OxyPenLineJoin.Bevel:
				return CGLineCap.Butt;
			case OxyPenLineJoin.Miter:
				return CGLineCap.Square;
			case OxyPenLineJoin.Round:
				return CGLineCap.Round;
			}
			
			return CGLineCap.Square;
		}

		public override void DrawPolygon (IList<ScreenPoint> points, OxyColor fill, OxyColor stroke, double thickness, double[] dashArray, OxyPenLineJoin lineJoin, bool aliased)
		{
			gctx.SetAllowsAntialiasing(aliased);
			
            if (fill != null)
			{
				ToColor(fill).SetFill();
				var path = new CGPath ();
	            path.AddLines(points.Select(p => ToPoint(p)).ToArray());
				path.CloseSubpath();
				gctx.AddPath(path);
	            gctx.DrawPath (CGPathDrawingMode.Fill);
			}
			
            if (stroke != null && thickness > 0)
			{
				SetAttributes (null, stroke, thickness);
			
				gctx.SetLineCap(ToLine(lineJoin));
            
	            var path = new CGPath ();
	            path.AddLines(points.Select(p => ToPoint(p)).ToArray());
				path.CloseSubpath();
				gctx.AddPath(path);
	            gctx.DrawPath (CGPathDrawingMode.Stroke);
			}
		}

		public override void DrawRectangle (OxyRect rect, OxyColor fill, OxyColor stroke, double thickness)
		{
			if (fill != null)
			{
				ToColor(fill).SetFill();
				var path = new CGPath ();
	            path.AddRect(ToRectangle(rect));
	            gctx.AddPath (path);
	            gctx.DrawPath (CGPathDrawingMode.Fill);	
			}
            
			if (stroke == null || thickness <= 0)
            {
                return;
            }
			
			SetAttributes (null, stroke, thickness);
			
			var path2 = new CGPath ();
            path2.AddRect(ToRectangle(rect));
            gctx.AddPath (path2);
            gctx.DrawPath (CGPathDrawingMode.Stroke);
		}
		
		public override void DrawText (ScreenPoint p, string text, OxyColor fill, string fontFamily, double fontSize, double fontWeight, double rotate, HorizontalTextAlign halign, VerticalTextAlign valign, OxySize? maxSize)
		{
			//This method needs work not 100% around vertical alignment.
			if(string.IsNullOrEmpty(text))
			{
				return;
			}
			
			if (fontWeight >= 700)
            {
                //fs = FontStyle.Bold;
            }
			
			var textSize = MeasureText(text, fontFamily, fontSize, fontWeight);
			
			if (maxSize != null)
            {
//                if (size.Width > maxSize.Value.Width)
//                {
//                    size.Width = (float)maxSize.Value.Width;
//                }
//
//                if (size.Height > maxSize.Value.Height)
//                {
//                    size.Height = (float)maxSize.Value.Height;
//                }
            }
			
			gctx.SelectFont(fontFamily, (float)fontSize, CGTextEncoding.MacRoman);
			ToColor(fill).SetFill();
			
			gctx.SetTextDrawingMode(CGTextDrawingMode.Fill);
			
			gctx.SaveState();
			gctx.ScaleCTM(1, -1); 
			gctx.TranslateCTM(0, (float)-Height);
			
			float y = (float)(Height - p.Y);
			float x = 0;
			
			switch(halign)
			{
			case HorizontalTextAlign.Left:
				x = (float)(p.X);
				break;
			case HorizontalTextAlign.Right:
				x = (float)(p.X - textSize.Width);
				break;
			case HorizontalTextAlign.Center:
				x = (float)(p.X - (textSize.Width / 2));
				break;
			}
			
			switch(valign)
			{
			case VerticalTextAlign.Bottom:
				y -= (float)fontSize;
				break;
			case VerticalTextAlign.Top:
				//y += (float)fontSize;
				//break;
			case VerticalTextAlign.Middle:
				y -= (float)(fontSize / 2);
				break;
			}
			
			gctx.ShowTextAtPoint(x, y, text);
			gctx.RestoreState();
			//Console.WriteLine("X:{0:###} Y:{1:###} HA:{2}:{3:###} VA:{4}:{5:###} TW:{6:###} - {7}", p.X, p.Y, halign, x, valign, y, textSize.Width, text);
		}

		public override OxySize MeasureText (string text, string fontFamily, double fontSize, double fontWeight)
		{
			//This method needs work not 100% around calculating height.
			if(text == null)
			{
				return OxySize.Empty;
			}
			
			var currentPosition = gctx.TextPosition;
			
			gctx.SelectFont(fontFamily, (float)fontSize, CGTextEncoding.MacRoman);
			
			gctx.SetTextDrawingMode(CGTextDrawingMode.Invisible);
			gctx.ShowTextAtPoint(currentPosition.X, currentPosition.Y, text);
			
			var newPosition = gctx.TextPosition;
			
			var width = newPosition.X - currentPosition.X;
			
			return new OxySize(width, fontSize);
		}
		#endregion
	}
}