// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="OxyPlot">
//   http://oxyplot.codeplex.com, license: Ms-PL
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ExampleBrowser
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constants and Fields

        /// <summary>
        /// The frame count.
        /// </summary>
        private int frameCount;

        /// <summary>
        /// The vm.
        /// </summary>
        private MainWindowViewModel vm = new MainWindowViewModel();

        /// <summary>
        /// The watch.
        /// </summary>
        private Stopwatch watch = new Stopwatch();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class. 
        ///   Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = this.vm;
            CompositionTarget.Rendering += this.CompositionTargetRendering;
            this.watch.Start();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the Rendering event of the CompositionTarget control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event. 
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data. 
        /// </param>
        private void CompositionTargetRendering(object sender, EventArgs e)
        {
            this.frameCount++;
            if (this.watch.ElapsedMilliseconds > 1000 && this.frameCount > 1)
            {
                this.vm.FrameRate = this.frameCount / (this.watch.ElapsedMilliseconds * 0.001);
                this.frameCount = 0;
                this.watch.Restart();
            }

            if (this.vm.MeasureFrameRate)
            {
                this.Plot1.RefreshPlot(true);
            }
        }

        #endregion
    }
}