//-----------------------------------------------------------------------
// <copyright file="LinearAxis.cs" company="OxyPlot">
//     http://oxyplot.codeplex.com, license: Ms-PL
// </copyright>
//-----------------------------------------------------------------------

namespace OxyPlot
{
    using System;

    /// <summary>
    /// Linear axis class.
    /// </summary>
    public class LinearAxis : AxisBase
    {
        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "LinearAxis" /> class.
        /// </summary>
        public LinearAxis()
        {
            this.FractionUnit = 1.0;
            this.FractionUnitSymbol = null;
            this.FormatAsFractions = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearAxis"/> class.
        /// </summary>
        /// <param name="pos">
        /// The pos.
        /// </param>
        /// <param name="title">
        /// The title.
        /// </param>
        public LinearAxis(AxisPosition pos, string title)
            : this()
        {
            this.Position = pos;
            this.Title = title;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearAxis"/> class.
        /// </summary>
        /// <param name="pos">
        /// The pos.
        /// </param>
        /// <param name="minimum">
        /// The minimum.
        /// </param>
        /// <param name="maximum">
        /// The maximum.
        /// </param>
        /// <param name="title">
        /// The title.
        /// </param>
        public LinearAxis(
            AxisPosition pos, double minimum = double.NaN, double maximum = double.NaN, string title = null)
            : this(pos, minimum, maximum, double.NaN, double.NaN, title)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearAxis"/> class.
        /// </summary>
        /// <param name="pos">
        /// The pos.
        /// </param>
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
        public LinearAxis(
            AxisPosition pos, double minimum, double maximum, double majorStep, double minorStep, string title = null)
            : this(pos, title)
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.MajorStep = majorStep;
            this.MinorStep = minorStep;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether to format numbers as fractions.
        /// </summary>
        public bool FormatAsFractions { get; set; }

        /// <summary>
        /// Gets or sets the fraction unit.
        /// Remember to set FormatAsFractions to true.
        /// </summary>
        /// <value>The fraction unit.</value>
        public double FractionUnit { get; set; }

        /// <summary>
        /// Gets or sets the fraction unit symbol.
        /// Use FractionUnit = Math.PI and FractionUnitSymbol = "π" if you want the axis to show "π/2,π,3π/2,2π" etc.
        /// Use FractionUnit = 1 and FractionUnitSymbol = "L" if you want the axis to show "0,L/2,L" etc.
        /// Remember to set FormatAsFractions to true.
        /// </summary>
        /// <value>The fraction unit symbol.</value>
        public string FractionUnitSymbol { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Formats the value to be used on the axis.
        /// </summary>
        /// <param name="x">
        /// The value.
        /// </param>
        /// <returns>
        /// The formatted value.
        /// </returns>
        public override string FormatValue(double x)
        {
            if (this.FormatAsFractions)
            {
                return FractionHelper.ConvertToFractionString(x, this.FractionUnit, this.FractionUnitSymbol, 1e-6, this.ActualCulture);
            }

            return base.FormatValue(x);
        }

        #endregion
    }
}
