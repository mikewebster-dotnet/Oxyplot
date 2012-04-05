// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DoubleExtensionsTests.cs" company="OxyPlot">
//   http://oxyplot.codeplex.com, license: Ms-PL
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Tests
{
    using System.Collections.Generic;

    using NUnit.Framework;

    [TestFixture]
    public class ScreenPointHelperTests
    {
        [Test]
        public void ResamplePoints()
        {
            var points = CreatePointList();
            var result = ScreenPointHelper.ResamplePoints(points, 1);
            Assert.AreEqual(4, result.Count);
        }

        [Test]
        public void GetCentroid()
        {
            var points = CreatePointList();
            var centroid = ScreenPointHelper.GetCentroid(points);
            Assert.AreEqual(0.041666, centroid.X, 1e-6);
            Assert.AreEqual(0.708333, centroid.Y, 1e-6);
        }

        private static IList<ScreenPoint> CreatePointList()
        {
            var points = new List<ScreenPoint>();
            points.Add(new ScreenPoint(-1, -1));
            points.Add(new ScreenPoint(1, -2));
            points.Add(new ScreenPoint(2, 2));
            points.Add(new ScreenPoint(-2, 3));
            return points;
        }
    }
}