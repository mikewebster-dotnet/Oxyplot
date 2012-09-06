// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Plot.Properties.cs" company="OxyPlot">
//   http://oxyplot.codeplex.com, license: Ms-PL
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Metro
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    
    //public static class DependencyProperty
    //{
    //    public static Windows.UI.Xaml.DependencyProperty Register(string name, Type propertyType, Type ownerType, PropertyMetadata typeMetadata)
    //    {
    //        string propertyTypeName = propertyType.FullName;
    //        string ownerTypeName = ownerType.FullName;
    //        return Windows.UI.Xaml.DependencyProperty.Register(name, propertyTypeName, ownerTypeName, typeMetadata);
    //    }
    //}

    /// <summary>
    /// The Metro Plot control.
    /// </summary>
    public partial class Plot
    {
        #region Constants and Fields

        /// <summary>
        ///   The default tracker property.
        /// </summary>
        public static readonly DependencyProperty DefaultTrackerTemplateProperty =
            DependencyProperty.Register(
                "DefaultTrackerTemplate", typeof(ControlTemplate), typeof(Plot), new PropertyMetadata(null));

        /// <summary>
        ///   The handle right clicks property.
        /// </summary>
        public static readonly DependencyProperty HandleRightClicksProperty =
            DependencyProperty.Register("HandleRightClicks", typeof(bool), typeof(Plot), new PropertyMetadata(true));

        /// <summary>
        ///   The is mouse wheel enabled property.
        /// </summary>
        public static readonly DependencyProperty IsMouseWheelEnabledProperty =
            DependencyProperty.Register("IsMouseWheelEnabled", typeof(bool), typeof(Plot), new PropertyMetadata(true));

        /// <summary>
        ///   The keyboard pan horizontal step property.
        /// </summary>
        public static readonly DependencyProperty KeyboardPanHorizontalStepProperty =
            DependencyProperty.Register(
                "KeyboardPanHorizontalStep", typeof(double), typeof(Plot), new PropertyMetadata(0.1));

        /// <summary>
        ///   The keyboard pan vertical step property.
        /// </summary>
        public static readonly DependencyProperty KeyboardPanVerticalStepProperty =
            DependencyProperty.Register(
                "KeyboardPanVerticalStep", typeof(double), typeof(Plot), new PropertyMetadata(0.1));

        /// <summary>
        ///   The model property.
        /// </summary>
        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(
            "Model", typeof(PlotModel), typeof(Plot), new PropertyMetadata(null, ModelChanged));

        /// <summary>
        ///   The zoom rectangle template property.
        /// </summary>
        public static readonly DependencyProperty ZoomRectangleTemplateProperty =
            DependencyProperty.Register(
                "ZoomRectangleTemplate", typeof(ControlTemplate), typeof(Plot), new PropertyMetadata(null));

        #endregion

        #region Public Properties

        /// <summary>
        ///   Gets or sets the default tracker template.
        /// </summary>
        public ControlTemplate DefaultTrackerTemplate
        {
            get
            {
                return (ControlTemplate)this.GetValue(DefaultTrackerTemplateProperty);
            }

            set
            {
                this.SetValue(DefaultTrackerTemplateProperty, value);
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether to handle right clicks.
        /// </summary>
        public bool HandleRightClicks
        {
            get
            {
                return (bool)this.GetValue(HandleRightClicksProperty);
            }

            set
            {
                this.SetValue(HandleRightClicksProperty, value);
            }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether IsMouseWheelEnabled.
        /// </summary>
        public bool IsMouseWheelEnabled
        {
            get
            {
                return (bool)this.GetValue(IsMouseWheelEnabledProperty);
            }

            set
            {
                this.SetValue(IsMouseWheelEnabledProperty, value);
            }
        }

        /// <summary>
        ///   Gets or sets the keyboard pan horizontal step (fraction of plot area width).
        /// </summary>
        /// <value>The keyboard pan horizontal step.</value>
        public double KeyboardPanHorizontalStep
        {
            get
            {
                return (double)this.GetValue(KeyboardPanHorizontalStepProperty);
            }

            set
            {
                this.SetValue(KeyboardPanHorizontalStepProperty, value);
            }
        }

        /// <summary>
        ///   Gets or sets the keyboard pan vertical step size (fraction of plot area height).
        /// </summary>
        /// <value>The keyboard pan vertical step.</value>
        public double KeyboardPanVerticalStep
        {
            get
            {
                return (double)this.GetValue(KeyboardPanVerticalStepProperty);
            }

            set
            {
                this.SetValue(KeyboardPanVerticalStepProperty, value);
            }
        }

        /// <summary>
        ///   Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public PlotModel Model
        {
            get
            {
                return (PlotModel)this.GetValue(ModelProperty);
            }

            set
            {
                this.SetValue(ModelProperty, value);
            }
        }

        /// <summary>
        ///   Gets or sets the zoom rectangle template.
        /// </summary>
        /// <value>The zoom rectangle template.</value>
        public ControlTemplate ZoomRectangleTemplate
        {
            get
            {
                return (ControlTemplate)this.GetValue(ZoomRectangleTemplateProperty);
            }

            set
            {
                this.SetValue(ZoomRectangleTemplateProperty, value);
            }
        }

        #endregion
    }
}