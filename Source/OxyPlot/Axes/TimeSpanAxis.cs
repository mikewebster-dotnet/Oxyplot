//-----------------------------------------------------------------------
// <copyright file="TimeSpanAxis.cs" company="OxyPlot">
//     http://oxyplot.codeplex.com, license: Ms-PL
// </copyright>
//-----------------------------------------------------------------------

namespace OxyPlot
{
    using System;
    using System.Linq;

    /// <summary>
    /// Time axis.
    /// </summary>
    /// <remarks>
    /// The values should be in seconds.
    ///   The StringFormat value can be used to force formatting of the axis values
    ///   "h:mm" shows hours and minutes
    ///   "m:ss" shows minutes and seconds
    /// </remarks>
    public class TimeSpanAxis : LinearAxis
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanAxis"/> class.
        /// </summary>
        /// <param name="pos">
        /// The position.
        /// </param>
        /// <param name="title">
        /// The axis title.
        /// </param>
        /// <param name="format">
        /// The string format for the axis values.
        /// </param>
        public TimeSpanAxis(AxisPosition pos, string title = null, string format = "m:ss")
            : base(pos, title)
        {
            this.StringFormat = format;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanAxis"/> class.
        /// </summary>
        /// <param name="pos">
        /// The position.
        /// </param>
        /// <param name="min">
        /// The min.
        /// </param>
        /// <param name="max">
        /// The max.
        /// </param>
        /// <param name="title">
        /// The axis title.
        /// </param>
        /// <param name="format">
        /// The string format for the axis values.
        /// </param>
        public TimeSpanAxis(
            AxisPosition pos = AxisPosition.Bottom, 
            double min = double.NaN, 
            double max = double.NaN, 
            string title = null, 
            string format = "m:ss")
            : base(pos, min, max, title)
        {
            this.StringFormat = format;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The to double.
        /// </summary>
        /// <param name="s">
        /// The s.
        /// </param>
        /// <returns>
        /// The to double.
        /// </returns>
        public static double ToDouble(TimeSpan s)
        {
            return s.TotalSeconds;
        }

        /// <summary>
        /// The to time span.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// </returns>
        public static TimeSpan ToTimeSpan(double value)
        {
            return TimeSpan.FromSeconds(value);
        }

        /// <summary>
        /// Formats the value.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <returns>
        /// The format value.
        /// </returns>
        public override string FormatValue(double x)
        {
            TimeSpan span = TimeSpan.FromSeconds(x);
            string s = this.ActualStringFormat ?? "h:mm:ss";

            s = s.Replace("mm", span.Minutes.ToString("00"));
            s = s.Replace("ss", span.Seconds.ToString("00"));
            s = s.Replace("hh", span.Hours.ToString("00"));
            s = s.Replace("msec", span.Milliseconds.ToString("000"));
            s = s.Replace("m", ((int)span.TotalMinutes).ToString("0"));
            s = s.Replace("s", ((int)span.TotalSeconds).ToString("0"));
            s = s.Replace("h", ((int)span.TotalHours).ToString("0"));
            return s;
        }

        /// <summary>
        /// The get value.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <returns>
        /// The get value.
        /// </returns>
        public override object GetValue(double x)
        {
            return TimeSpan.FromSeconds(x);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The calculate actual interval.
        /// </summary>
        /// <param name="availableSize">
        /// The available size.
        /// </param>
        /// <param name="maxIntervalSize">
        /// The max interval size.
        /// </param>
        /// <returns>
        /// The calculate actual interval.
        /// </returns>
        protected override double CalculateActualInterval(double availableSize, double maxIntervalSize)
        {
            double range = Math.Abs(this.ActualMinimum - this.ActualMaximum);
            double interval = 1;
            var goodIntervals = new[] { 1.0, 5, 10, 30, 60, 120, 300, 600, 900, 1200, 1800, 3600 };

            int maxNumberOfIntervals = Math.Max((int)(availableSize / maxIntervalSize), 2);

            while (true)
            {
                if (range / interval < maxNumberOfIntervals)
                {
                    return interval;
                }

                double nextInterval = goodIntervals.FirstOrDefault(i => i > interval);
                if (nextInterval == 0)
                {
                    nextInterval = interval * 2;
                }

                interval = nextInterval;
            }
        }

        #endregion
    }
}
