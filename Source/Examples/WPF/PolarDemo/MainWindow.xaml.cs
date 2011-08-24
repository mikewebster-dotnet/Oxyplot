﻿namespace PolarDemo
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    using OxyPlot;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = this;
            this.SpiralPoints = new List<IDataPoint>();
            this.MaxAngle = Math.PI * 2;
            this.MajorStep = Math.PI / 4;
            this.MinorStep = Math.PI / 16;
            this.PI = Math.PI;

            foreach (IDataPoint pt in FunctionSeries.GetPoints(t => t, t => t, 0, Math.PI * 6, 0.01))
            {
                this.SpiralPoints.Add(pt);
            }

            this.MyModel = this.CreateModel();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets MajorStep.
        /// </summary>
        public double MajorStep { get; set; }

        /// <summary>
        /// Gets or sets MaxAngle.
        /// </summary>
        public double MaxAngle { get; set; }

        /// <summary>
        /// Gets or sets MinorStep.
        /// </summary>
        public double MinorStep { get; set; }

        /// <summary>
        /// Gets or sets MyModel.
        /// </summary>
        public PlotModel MyModel { get; set; }

        /// <summary>
        /// Gets or sets PI.
        /// </summary>
        public double PI { get; set; }

        /// <summary>
        /// Gets or sets SpiralPoints.
        /// </summary>
        public List<IDataPoint> SpiralPoints { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// The create model.
        /// </summary>
        /// <returns>
        /// A PlotModel.
        /// </returns>
        private PlotModel CreateModel()
        {
            var model = new PlotModel("Polar plot", "Archimedean spiral with equation r(θ) = θ for 0 < θ < 6π")
                {
                   PlotType = PlotType.Polar, 
                   PlotMargins = new OxyThickness(20, 20, 4, 40), 
                   BoxThickness = 0 
                };
            model.Axes.Add(
                new AngleAxis(0, this.MaxAngle, this.MajorStep, this.MinorStep)
                    {
                       FormatAsFractions = true, 
                       FractionUnit = Math.PI, 
                       FractionUnitSymbol = "π" 
                    });
            model.Axes.Add(new MagnitudeAxis());
            model.Series.Add(new LineSeries { ItemsSource = this.SpiralPoints });
            return model;
        }

        #endregion
    }
}