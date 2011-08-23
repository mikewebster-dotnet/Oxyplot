﻿using System.Collections.ObjectModel;
using System.Linq;

namespace OxyPlot
{
    using System;

    /// <summary>
    /// Annotation base class.
    /// </summary>
    [Serializable]
    public abstract class Annotation : IAnnotation
    {
        /// <summary>
        /// Annotation text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///   Gets or sets the X axis.
        /// </summary>
        /// <value>The X axis.</value>
        public IAxis XAxis { get; set; }

        /// <summary>
        ///   Gets or sets the Y axis.
        /// </summary>
        /// <value>The Y axis.</value>
        public IAxis YAxis { get; set; }

        /// <summary>
        ///   Gets or sets the X axis key.
        /// </summary>
        /// <value>The X axis key.</value>
        public string XAxisKey { get; set; }

        /// <summary>
        ///   Gets or sets the Y axis key.
        /// </summary>
        /// <value>The Y axis key.</value>
        public string YAxisKey { get; set; }

        #region IAnnotation Members

        public AnnotationLayer Layer { get; set; }

        public virtual void Render(IRenderContext rc, PlotModel model)
        {
        }


        public void EnsureAxes(Collection<Axis> axes, IAxis defaultXAxis, IAxis defaultYAxis)
        {
            // todo: refactor - this code is shared with DataPointSeries
            if (XAxisKey != null)
            {
                XAxis = axes.FirstOrDefault(a => a.Key == XAxisKey);
            }
            if (YAxisKey != null)
            {
                YAxis = axes.FirstOrDefault(a => a.Key == YAxisKey);
            }

            // If axes are not found, use the default axes
            if (XAxis == null)
            {
                XAxis = defaultXAxis;
            }
            if (YAxis == null)
            {
                YAxis = defaultYAxis;
            }
        }

        #endregion
    }
}