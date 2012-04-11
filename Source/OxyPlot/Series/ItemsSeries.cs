// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemsSeries.cs" company="OxyPlot">
//   http://oxyplot.codeplex.com, license: Ms-PL
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot
{
    using System.Collections;
    using System.Linq;

    /// <summary>
    /// Abstract base class for Series that can contains items.
    /// </summary>
    public abstract class ItemsSeries : Series
    {
        #region Public Properties

        /// <summary>
        ///   Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        [CodeGeneration(false)]
        public IEnumerable ItemsSource { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the item of the specified index.
        /// </summary>
        /// <param name="itemsSource">
        /// The items source.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The get item.
        /// </returns>
        /// <remarks>
        /// Returns null if ItemsSource is not set, or the index is outside the boundaries.
        /// </remarks>
        protected static object GetItem(IEnumerable itemsSource, int index)
        {
            if (itemsSource == null || index < 0)
            {
                return null;
            }

            var list = itemsSource as IList;
            if (list != null)
            {
                if (index < list.Count && index >= 0)
                {
                    return list[index];
                }

                return null;
            }

            int i = 0;
            return itemsSource.Cast<object>().FirstOrDefault(item => i++ == index);
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="i">
        /// The index. 
        /// </param>
        /// <returns>
        /// The item. 
        /// </returns>
        protected object GetItem(int i)
        {
            return GetItem(this.ItemsSource, i);
        }

        #endregion
    }
}