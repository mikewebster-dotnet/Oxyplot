﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HighLowSeries.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// <summary>
//   Represents a series for high-low plots.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Series
{
	using System;
	using System.Collections.Generic;

	using OxyPlot.Axes;

	/// <summary>
	/// Represents a series for OHLC + volume plots
	/// </summary>
	/// <remarks>See <a href="http://www.mathworks.com/help/toolbox/finance/highlowfts.html">link</a></remarks>
	public class OHLCVSeries : XYAxisSeries
	{
		/// <summary>
		/// The default tracker format string
		/// </summary>
		public new const string DefaultTrackerFormatString = 
			"Time: {0}\nHigh: {1}\nLow: {2}\nOpen: {3}\nClose: {4}\nBuy Volume: {5}\nSell Volume: {6}";

		/// <summary>
		/// High/low items
		/// </summary>
		private readonly List<OHLCVItem> items = new List<OHLCVItem>();

		/// <summary>
		/// Initializes a new instance of the <see cref = "HighLowSeries" /> class.
		/// </summary>
		public OHLCVSeries()
		{
			this.YAxisKey = "Bars";
			this.PositiveColor = OxyColors.DarkGreen;
			this.NegativeColor= OxyColors.Red;
			this.SeparatorColor = OxyColors.Black;
			this.CandleWidth = 0;
			this.SeparatorStrokeThickness = 2;
			this.BarStrokeThickness = 1;
			this.VolumeStacked = true;

			this.TrackerFormatString = DefaultTrackerFormatString;
		}

		// Properties

		/// <summary>
		/// Gets the items of the series.
		/// </summary>
		/// <value>The items.</value>
		public List<OHLCVItem> Items
			{ get { return this.items; } }


		/// <summary>
		/// The portion of the Y axis associated with bars
		/// </summary>
		public LinearAxis BarAxis
			{ get { return (LinearAxis)XAxis; } }

		/// <summary>
		/// The portion of the Y axis associated with volume
		/// </summary>
		public LinearAxis VolumeAxis
			{ get; private set; }


		/// <summary>
		/// Gets or sets the thickness of the bar lines
		/// </summary>
		/// <value>The stroke thickness.</value>
		public double BarStrokeThickness { get; set; }

		/// <summary>
		/// Gets or sets the thickness of the volume / bar separator
		/// </summary>
		/// <value>The stroke thickness.</value>
		public double SeparatorStrokeThickness { get; set; }

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
		/// Gets or sets the color of the separtator line
		/// </summary>
		public OxyColor SeparatorColor { get; set; }

		/// <summary>
		/// Gets or sets the bar width in data units (for example if the X axis is datetime based, then should
		/// use the difference of DateTimeAxis.ToDouble(date) to indicate the width).  By default candlestick
		/// series will use 0.80 x the minimum difference in data points.
		/// </summary>
		public double CandleWidth { get; set; }

		/// <summary>
		/// Indicate whether shows dominant volume or semi-stacked
		/// </summary>
		public bool VolumeStacked { get; set; }


		// Functions



		/// <summary>
		/// Append a bar to the series (must be in X order)
		/// </summary>
		/// <param name="bar">Bar.</param>
		public void Append (OHLCVItem bar)
		{
			if (Items.Count > 0 && Items [Items.Count - 1].X > bar.X)
				throw new ArgumentException ("cannot append bar out of order, must be sequential in X");

			Items.Add (bar);
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

			return FindIndex (Items, x, Istarting);
		}


		/// <summary>
		/// Renders the series on the specified rendering context.
		/// </summary>
		/// <param name="rc">The rendering context.</param>
		/// <param name="model">The owner plot model.</param>
		public override void Render(IRenderContext rc, PlotModel model)
		{
			var nitems = this.Items.Count;
			var items = this.Items;

			if (nitems == 0)
				return;

			this.VerifyAxes();

			var clipping_bar = GetClippingRect (BarAxis);
			var dashes_bar = LineStyle.Solid.GetDashArray ();

			var datacandlewidth = (CandleWidth > 0) ? CandleWidth : _dx * 0.80;
			var candlewidth = 
				this.XAxis.Transform (items [0].X + datacandlewidth) -
				this.XAxis.Transform (items [0].X); 

			// colors
			var fill_up = this.GetSelectableFillColor (this.PositiveColor);
			var fill_down = this.GetSelectableFillColor (this.NegativeColor);
			var line_up = this.GetSelectableColor(this.PositiveColor.ChangeIntensity (0.70));
			var line_down = this.GetSelectableColor(this.NegativeColor.ChangeIntensity (0.70));


			// determine render range
			var xmin = this.XAxis.ActualMinimum;
			var xmax = this.XAxis.ActualMaximum;
			_pindex = FindIndex (items, xmin, _pindex);

			for (int i = _pindex ; i < nitems ; i++)
			{
				var bar = items [i];

				// if item beyond visible range, done
				if (bar.X > xmax)
					return;
				if (bar.X < xmin)
					continue;

				// check to see whether is valid
				if (!bar.IsValid())
					continue;

				var fillColor = bar.Close > bar.Open ? fill_up : fill_down;
				var lineColor = bar.Close > bar.Open ? line_up : line_down;

				var high = this.Transform(bar.X, bar.High);
				var low = this.Transform(bar.X, bar.Low);

				var open = this.Transform(bar.X, bar.Open);
				var close = this.Transform(bar.X, bar.Close);

				var max = new ScreenPoint(open.X, Math.Max(open.Y, close.Y));
				var min = new ScreenPoint(open.X, Math.Min(open.Y, close.Y));

				//
				// Bar part
				//

				rc.DrawClippedLine(
					clipping_bar,
					new[] { high, min },
					0,
					lineColor,
					this.BarStrokeThickness,
					dashes_bar,
					LineJoin.Miter,
					true);

				// Lower extent
				rc.DrawClippedLine(
					clipping_bar,
					new[] { max, low },
					0,
					lineColor,
					this.BarStrokeThickness,
					dashes_bar,
					LineJoin.Miter,
					true);

				// Body
				var openLeft = open + new ScreenVector(-candlewidth * 0.5, 0);
				var rect = new OxyRect(openLeft.X, min.Y, candlewidth, max.Y - min.Y);
				rc.DrawClippedRectangleAsPolygon(clipping_bar, rect, fillColor, lineColor, this.BarStrokeThickness);


				//
				// Volume Part
				//

				if (VolumeAxis == null)
					return;

				// draw separation line
				var clipping_sep = GetSeparationClippingRect ();
				var ysep = (clipping_sep.Bottom + clipping_sep.Top) / 2.0;
				rc.DrawClippedLine (
					clipping_sep,
					new[] { new ScreenPoint (clipping_sep.Left, ysep), new ScreenPoint (clipping_sep.Right, ysep) },
					0,
					SeparatorColor,
					SeparatorStrokeThickness,
					dashes_bar,
					LineJoin.Miter,
					true);

				var clipping_vol = GetClippingRect (VolumeAxis);

				var raw_buy = bar.BuyVolume;
				var raw_sell = bar.SellVolume;

				// adjust levels if not stacked
				if (!VolumeStacked)
				{
					var tot = raw_buy - raw_sell;
					if (tot < 0)
					{
						raw_sell = -tot;
						raw_buy = 0;
					}
					else
					{
						raw_sell = 0;
						raw_buy = tot;
					}
				}
					
				var Ybuy = VolumeAxis.Transform (raw_buy);
				var Ysell = VolumeAxis.Transform (raw_sell);

				if (Ybuy > Ysell)
				{
					var rect1 = new OxyRect (open.X - candlewidth * 0.5, Ybuy, candlewidth, Ybuy);
					rc.DrawClippedRectangleAsPolygon(clipping_vol, rect1, fill_up, line_up, this.BarStrokeThickness);
					var rect2 = new OxyRect (open.X - candlewidth * 0.5, Ysell, candlewidth, Ysell);
					rc.DrawClippedRectangleAsPolygon(clipping_vol, rect2, fill_down, line_down, this.BarStrokeThickness);
				}
				else
				{
					var rect1 = new OxyRect (open.X - candlewidth * 0.5, Ysell, candlewidth, Ysell);
					rc.DrawClippedRectangleAsPolygon(clipping_vol, rect1, fill_down, line_down, this.BarStrokeThickness);
					var rect2 = new OxyRect (open.X - candlewidth * 0.5, Ybuy, candlewidth, Ybuy);
					rc.DrawClippedRectangleAsPolygon(clipping_vol, rect2, fill_up, line_up, this.BarStrokeThickness);
				}
			}
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

			var datacandlewidth = (CandleWidth > 0) ? CandleWidth : _dx * 0.80;

			var fill_up = this.GetSelectableFillColor (this.PositiveColor);
			var line_up = this.GetSelectableColor(this.PositiveColor.ChangeIntensity (0.70));

			var candlewidth = 
				this.XAxis.Transform (this.Items [0].X + datacandlewidth) -
				this.XAxis.Transform (this.Items [0].X); 

			rc.DrawLine(
				new[] { new ScreenPoint(xmid, legendBox.Top), new ScreenPoint(xmid, legendBox.Bottom) },
				line_up,
				this.BarStrokeThickness,
				dashArray,
				LineJoin.Miter,
				true);

			rc.DrawRectangleAsPolygon(
				new OxyRect(xmid - (candlewidth * 0.5), yclose, candlewidth, yopen - yclose),
				fill_up,
				line_up,
				this.BarStrokeThickness);
		}


		/// <summary>
		/// Updates the data.
		/// </summary>
		protected internal override void UpdateData ()
		{
			base.UpdateData ();
			_pindex = 0;

			// determine minimum X gap between successive points
			var items = Items;
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
			if (this.XAxis == null || this.YAxis == null || interpolate || Items.Count == 0)
				return null;

			var nbars = Items.Count;
			var xy = InverseTransform (point);
			var targetX = xy.X;

			// punt if beyond start & end of series
			if (targetX > (Items [nbars - 1].X + _dx))
				return null;
			if (targetX < (Items [0].X - _dx))
				return null;

			var pidx = FindIndex (Items, targetX, _pindex);
			var nidx = ((pidx + 1) < Items.Count) ? pidx + 1 : pidx;

			Func<OHLCVItem,double> distance = (bar) =>
			{
				var dx = bar.X - xy.X;
				return dx*dx;
			};

			// determine closest point
			var midx = distance (Items [pidx]) <= distance (Items [nidx]) ? pidx : nidx; 
			var mbar = Items [midx];

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
		/// Ensures that the axes of the series is defined.
		/// </summary>
		protected internal override void EnsureAxes()
		{
			base.EnsureAxes ();
			this.VolumeAxis = (LinearAxis)this.PlotModel.GetAxisOrDefault("Volume", null);
		}


		/// <summary>
		/// Sets the default values.
		/// </summary>
		/// <param name="model">The model.</param>
		protected internal override void SetDefaultValues(PlotModel model)
		{
			this.PositiveColor = OxyColors.DarkGreen;
			this.NegativeColor= OxyColors.Red;
			this.SeparatorColor = OxyColors.Black;
			this.CandleWidth = 0;
			this.SeparatorStrokeThickness = 2;
			this.BarStrokeThickness = 1;
			this.VolumeStacked = true;

			this.TrackerFormatString = DefaultTrackerFormatString;
		}


		/// <summary>
		/// Gets the clipping rectangle for the given combination of existing XAxis and specific yaxis
		/// </summary>
		/// <returns>The clipping rectangle.</returns>
		protected OxyRect GetClippingRect(Axis yaxis)
		{
			double minX = Math.Min(this.XAxis.ScreenMin.X, this.XAxis.ScreenMax.X);
			double minY = Math.Min(yaxis.ScreenMin.Y, yaxis.ScreenMax.Y);
			double maxX = Math.Max(this.XAxis.ScreenMin.X, this.XAxis.ScreenMax.X);
			double maxY = Math.Max(yaxis.ScreenMin.Y, yaxis.ScreenMax.Y);

			return new OxyRect(minX, minY, maxX - minX, maxY - minY);
		}


		/// <summary>
		/// Gets the clipping rectangle between plots
		/// </summary>
		/// <returns>The clipping rectangle.</returns>
		protected OxyRect GetSeparationClippingRect()
		{
			double minX = Math.Min(this.XAxis.ScreenMin.X, this.XAxis.ScreenMax.X);
			double maxX = Math.Max(this.XAxis.ScreenMin.X, this.XAxis.ScreenMax.X);
			double minY = Math.Max (VolumeAxis.ScreenMin.Y, VolumeAxis.ScreenMax.Y);
			double maxY = Math.Min(BarAxis.ScreenMin.Y, BarAxis.ScreenMax.Y);

			return new OxyRect(minX, minY, maxX - minX, maxY - minY);
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

		private double		_dx;
		private int 		_pindex;
	}
}