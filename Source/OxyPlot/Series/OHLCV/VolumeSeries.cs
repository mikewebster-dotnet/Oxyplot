﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HighLowSeries.cs" company="OxyPlot">
//   Copyright (c) 2015 OxyPlot contributors
// </copyright>
// <summary>
//   Represents a volume view on OHLCV bars
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Series
{
	using System;
	using System.Collections.Generic;

	using OxyPlot.Axes;

	/// <summary>
	/// Represents a dual view (candlestick & volume) series for OHLCV bars
	/// </summary>
	/// <remarks>See <a href="http://www.mathworks.com/help/toolbox/finance/highlowfts.html">link</a></remarks>
	public class VolumeSeries : XYAxisSeries
	{
		/// <summary>
		/// The default tracker format string
		/// </summary>
		public new const string DefaultTrackerFormatString = 
			"Time: {0}\nHigh: {1}\nLow: {2}\nOpen: {3}\nClose: {4}\nBuy Volume: {5}\nSell Volume: {6}";


		/// <summary>
		/// Initializes a new instance of the <see cref = "HighLowSeries" /> class.
		/// </summary>
		public VolumeSeries()
		{
			this.PositiveColor = OxyColors.DarkGreen;
			this.NegativeColor= OxyColors.Red;
			this.BarWidth = 0;
			this.StrokeThickness = 1;
			this.NegativeHollow = false;
			this.PositiveHollow = true;
			this.StrokeIntensity = 0.80;

			this.InterceptColor = OxyColors.Gray;
			this.InterceptLineStyle = LineStyle.Dash;
			this.InterceptStrokeThickness = 1;

			this.TrackerFormatString = DefaultTrackerFormatString;
		}

		// Properties

		/// <summary>
		/// Gets the items of the series.
		/// </summary>
		/// <value>The items.</value>
		public List<OHLCVItem> Items
		{ 
			get 
				{ return (_data != null) ? _data : (_data = new List<OHLCVItem> ()); } 
			set
				{ _data = value; }
		}

		/// <summary>
		/// The style of volume rendering
		/// </summary>
		public VolumeStyle VolumeStyle { get; set; }

		/// <summary>
		/// Gets or sets the thickness of the bar lines
		/// </summary>
		/// <value>The stroke thickness.</value>
		public double StrokeThickness { get; set; }

		/// <summary>
		/// Gets or sets the stroke intensity scale (used to generate stroke color from positive or negative color).
		/// For example, 1.0 = same color and 0.5 is 1/2 of the intensity of the source fill color.
		/// </summary>
		public double StrokeIntensity { get; set; }

		/// <summary>
		/// Gets or sets the color used when the closing value is greater than opening value or
		/// for buying volume.
		/// </summary>
		public OxyColor PositiveColor { get; set; }

		/// <summary>
		/// Gets or sets the fill color used when the closing value is less than opening value or
		/// for selling volume
		/// </summary>
		public OxyColor NegativeColor { get; set; }

		/// <summary>
		/// Gets or sets the stroke color of the Y=0 intercept
		/// </summary>
		public OxyColor InterceptColor { get; set; }

		/// <summary>
		/// Gets or sets the thickness of the Y=0 intercept
		/// </summary>
		public double InterceptStrokeThickness { get; set; }

		/// <summary>
		/// Gets or sets the line style of the Y=0 intercept
		/// </summary>
		public LineStyle InterceptLineStyle { get; set; }

		/// <summary>
		/// Indicates whether positive bars are shown as filled (false) or hollow (true) candlesticks
		/// </summary>
		public bool PositiveHollow { get; set; }

		/// <summary>
		/// Indicates whether negative bars are shown as filled (false) or hollow (true) candlesticks
		/// </summary>
		public bool NegativeHollow { get; set; }

		/// <summary>
		/// Gets or sets the bar width in data units (for example if the X axis is datetime based, then should
		/// use the difference of DateTimeAxis.ToDouble(date) to indicate the width).  By default candlestick
		/// series will use 0.80 x the minimum difference in data points.
		/// </summary>
		public double BarWidth { get; set; }


		// Functions



		/// <summary>
		/// Append a bar to the series (must be in X order)
		/// </summary>
		/// <param name="bar">Bar.</param>
		public void Append (OHLCVItem bar)
		{
			if (_data == null)
				_data = new List<OHLCVItem> ();

			if (_data.Count > 0 && _data [_data.Count - 1].X > bar.X)
				throw new ArgumentException ("cannot append bar out of order, must be sequential in X");

			_data.Add (bar);
		}


		/// <summary>
		/// Fast index of bar where max(bar[i].X) <= x 
		/// </summary>
		/// <returns>The index of the bar closest to X, where max(bar[i].X) <= x.</returns>
		/// <param name="x">The x coordinate.</param>
		/// <param name="Istarting">starting index</param> 
		public int FindByX (double x, int Istarting = -1)
		{
			if (Istarting < 0)
				Istarting = _pindex;

			return FindIndex (_data, x, Istarting);
		}


		/// <summary>
		/// Renders the series on the specified rendering context.
		/// </summary>
		/// <param name="rc">The rendering context.</param>
		/// <param name="model">The owner plot model.</param>
		public override void Render(IRenderContext rc, PlotModel model)
		{
			if (_data == null || _data.Count == 0)
				return;

			var items = _data;
			var nitems = _data.Count;

			this.VerifyAxes();

			var clipping = this.GetClippingRect();

			var datacandlewidth = (BarWidth > 0) ? BarWidth : _dx * 0.80;
			var candlewidth = 
				this.XAxis.Transform (items [0].X + datacandlewidth) -
				this.XAxis.Transform (items [0].X) - StrokeThickness; 

			// colors
			var fill_up = this.GetSelectableFillColor (this.PositiveColor);
			var fill_down = this.GetSelectableFillColor (this.NegativeColor);

			var barfill_up = this.PositiveHollow ? 
				OxyColors.Transparent : fill_up;
			var barfill_down = this.NegativeHollow ? 
				OxyColors.Transparent : fill_down;

			var line_up = this.GetSelectableColor(this.PositiveColor.ChangeIntensity (StrokeIntensity));
			var line_down = this.GetSelectableColor(this.NegativeColor.ChangeIntensity (StrokeIntensity));


			// determine render range
			var xmin = this.XAxis.ActualMinimum;
			var xmax = this.XAxis.ActualMaximum;
			_pindex = FindIndex (items, xmin, _pindex);

			for (int i = _pindex ; i < nitems ; i++)
			{
				var bar = items [i];

				// if item beyond visible range, done
				if (bar.X > xmax)
					break;
				if (bar.X < xmin)
					continue;

				// check to see whether is valid
				if (!bar.IsValid())
					continue;

				var Xleft = XAxis.Transform(bar.X) - BarWidth / 2.0;
				var Y0 = YAxis.Transform (0); 

				switch (VolumeStyle)
				{
					case VolumeStyle.Combined:
						{
							var adj = YAxis.Transform (Math.Abs(bar.BuyVolume - bar.SellVolume));
							var fillcolor = (bar.BuyVolume > bar.SellVolume) ? barfill_up : barfill_down;
							var linecolor = (bar.BuyVolume > bar.SellVolume) ? line_up : line_down;
							var rect1 = new OxyRect (Xleft, adj, candlewidth, Math.Abs(adj-Y0));
							rc.DrawClippedRectangleAsPolygon(clipping, rect1, fillcolor, linecolor, this.StrokeThickness);
						}
						break;
					
					case VolumeStyle.PositiveNegative:
						{
							var Ybuy = YAxis.Transform (bar.BuyVolume);
							var Ysell = YAxis.Transform (-bar.SellVolume);
							var rect1 = new OxyRect (Xleft, Ybuy, candlewidth, Math.Abs(Ybuy-Y0));
							rc.DrawClippedRectangleAsPolygon(clipping, rect1, fill_up, line_up, this.StrokeThickness);
							var rect2 = new OxyRect (Xleft, Y0, candlewidth, Math.Abs(Ysell-Y0));
							rc.DrawClippedRectangleAsPolygon(clipping, rect2, fill_down, line_down, this.StrokeThickness);
						}
						break;

					case VolumeStyle.Stacked:
						if (bar.BuyVolume > bar.SellVolume)
						{
							var Ybuy = YAxis.Transform (bar.BuyVolume);
							var Ysell = YAxis.Transform (bar.SellVolume);
							var dyoffset = Ysell - Y0;
							var rect2 = new OxyRect (Xleft, Ysell, candlewidth, Math.Abs (Ysell - Y0));
							rc.DrawClippedRectangleAsPolygon (clipping, rect2, fill_down, line_down, this.StrokeThickness);
							var rect1 = new OxyRect (Xleft, Ybuy + dyoffset, candlewidth, Math.Abs (Ybuy - Y0));
							rc.DrawClippedRectangleAsPolygon (clipping, rect1, fill_up, line_up, this.StrokeThickness);
						}
						else
						{
							var Ybuy = YAxis.Transform (bar.BuyVolume);
							var Ysell = YAxis.Transform (bar.SellVolume);
							var dyoffset = Ybuy - Y0;
							var rect1 = new OxyRect (Xleft, Ybuy, candlewidth, Math.Abs (Ybuy - Y0));
							rc.DrawClippedRectangleAsPolygon (clipping, rect1, fill_up, line_up, this.StrokeThickness);
							var rect2 = new OxyRect (Xleft, Ysell + dyoffset, candlewidth, Math.Abs (Ysell - Y0));
							rc.DrawClippedRectangleAsPolygon (clipping, rect2, fill_down, line_down, this.StrokeThickness);
						}
						break;

				}
			}

			// draw volume y=0 line
			var intercept = YAxis.Transform (0); 
			rc.DrawClippedLine (
				clipping,
				new[] { new ScreenPoint (clipping.Left, intercept), new ScreenPoint (clipping.Right, intercept) },
				0,
				InterceptColor,
				InterceptStrokeThickness,
				InterceptLineStyle.GetDashArray (),
				LineJoin.Miter,
				true);
		}


		/// <summary>
		/// Renders the legend symbol for the series on the specified rendering context.
		/// </summary>
		/// <param name="rc">The rendering context.</param>
		/// <param name="legendBox">The bounding rectangle of the legend box.</param>
		public override void RenderLegend(IRenderContext rc, OxyRect legendBox)
		{
			double xmid = (legendBox.Left + legendBox.Right) / 2;
			double yopen = legendBox.Top + ((legendBox.Bottom - legendBox.Top) * 0.7);
			double yclose = legendBox.Top + ((legendBox.Bottom - legendBox.Top) * 0.3);
			double[] dashArray = LineStyle.Solid.GetDashArray();

			var datacandlewidth = (BarWidth > 0) ? BarWidth : _dx * 0.80;

			var fill_up = this.GetSelectableFillColor (this.PositiveColor);
			var line_up = this.GetSelectableColor(this.PositiveColor.ChangeIntensity (0.70));

			var candlewidth = 
				this.XAxis.Transform (this._data [0].X + datacandlewidth) -
				this.XAxis.Transform (this._data [0].X); 

			rc.DrawLine(
				new[] { new ScreenPoint(xmid, legendBox.Top), new ScreenPoint(xmid, legendBox.Bottom) },
				line_up,
				this.StrokeThickness,
				dashArray,
				LineJoin.Miter,
				true);

			rc.DrawRectangleAsPolygon(
				new OxyRect(xmid - (candlewidth * 0.5), yclose, candlewidth, yopen - yclose),
				fill_up,
				line_up,
				this.StrokeThickness);
		}


		/// <summary>
		/// Updates the data.
		/// </summary>
		protected internal override void UpdateData ()
		{
			base.UpdateData ();
			_pindex = 0;

			// determine minimum X gap between successive points
			var items = _data;
			var nitems = items.Count;
			_dx = double.MaxValue;

			for (int i = 1; i < nitems; i++)
			{
				_dx = Math.Min (_dx, items [i].X - items [i - 1].X);
				if (_dx < 0)
					throw new ArgumentException ("bars are out of order, must be sequential in x");
			}

			if (nitems <= 1)
				_dx = 1;
		}


		/// <summary>
		/// Gets the point on the series that is nearest the specified point.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <param name="interpolate">Interpolate the series if this flag is set to <c>true</c>.</param>
		/// <returns>A TrackerHitResult for the current hit.</returns>
		public override TrackerHitResult GetNearestPoint(ScreenPoint point, bool interpolate)
		{
			if (this.XAxis == null || this.YAxis == null || interpolate || _data.Count == 0)
				return null;

			var nbars = _data.Count;
			var xy = InverseTransform (point);
			var targetX = xy.X;

			// punt if beyond start & end of series
			if (targetX > (_data [nbars - 1].X + _dx))
				return null;
			if (targetX < (_data [0].X - _dx))
				return null;

			var pidx = FindIndex (_data, targetX, _pindex);
			var nidx = ((pidx + 1) < _data.Count) ? pidx + 1 : pidx;

			Func<OHLCVItem,double> distance = (bar) =>
			{
				var dx = bar.X - xy.X;
				return dx*dx;
			};

			// determine closest point
			var midx = distance (_data [pidx]) <= distance (_data [nidx]) ? pidx : nidx; 
			var mbar = _data [midx];

			var hit = new DataPoint(mbar.X, mbar.Close);
			return new TrackerHitResult
			{
				Series = this,
				DataPoint = hit,
				Position = Transform(hit),
				Item = mbar,
				Index = midx,
				Text = StringHelper.Format(
					this.ActualCulture,
					this.TrackerFormatString,
					mbar,
					this.XAxis.GetValue(mbar.X),
					this.YAxis.GetValue(mbar.High),
					this.YAxis.GetValue(mbar.Low),
					this.YAxis.GetValue(mbar.Open),
					this.YAxis.GetValue(mbar.Close),
					this.YAxis.GetValue(mbar.BuyVolume),
					this.YAxis.GetValue(mbar.SellVolume))
			};
		}
			

		/// <summary>
		/// Find index of max(x) <= target x
		/// </summary>
		/// <param name='items'>
		/// vector of bars
		/// </param>
		/// <param name='targetX'>
		/// target x.
		/// </param>
		/// <param name='Iguess'>
		/// initial guess.
		/// </param>
		/// <returns>
		/// index of x with max(x) <= target x or -1 if cannot find
		/// </returns>
		private static int FindIndex (List<OHLCVItem> items, double targetX, int Iguess)
		{
			int Ilastguess = 0;
			int Istart = 0;
			int Iend = items.Count - 1;

			while (Istart <= Iend)
			{
				if (Iguess < Istart)
					return Ilastguess;
				if (Iguess > Iend)
					return Iend;

				var Xguess = items[Iguess].X;
				if (Xguess == targetX)
					return Iguess;

				if (Xguess > targetX)
				{
					Iend = Iguess-1;
					if (Iend < Istart)
						return Ilastguess;
					if (Iend == Istart)
						return Iend;
				} 
				else
				{ 
					Istart = Iguess+1; 
					Ilastguess = Iguess; 
				}

				if (Istart >= Iend)
					return Ilastguess;

				var Xend = items[Iend].X;
				var Xstart = items[Istart].X;

				var m = (double)(Iend-Istart+1) / (Xend - Xstart);
				Iguess = Istart + (int)((targetX - Xstart) * m);
			}

			return Ilastguess;
		}


		// Variables

		private List<OHLCVItem>		_data;
		private double				_dx;
		private int 				_pindex;
	}
}