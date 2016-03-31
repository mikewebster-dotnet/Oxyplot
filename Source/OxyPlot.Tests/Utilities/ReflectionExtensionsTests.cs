﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionExtensionsTests.cs" company="OxyPlot">
//   Copyright (c) 2014 OxyPlot contributors
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using NUnit.Framework;

    // ReSharper disable InconsistentNaming
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class ReflectionExtensionsTests
    {
        [Test]
        public void AddRangeWithSimpleProperties()
        {
            var items = new List<Item> { new Item { A = 1, B = 2 } };
            var result = new List<double>();
            result.AddRange(items, "A");
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo(1));
        }

        [Test]
        public void AddRangeWithPath()
        {
            var items = new List<Item> { new Item { Point = new DataPoint(1.1, 2.2) } };
            var result = new List<double>();
            result.AddRange(items, "Point.X");
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo(1.1));
        }

        [Test]
        public void ListOfDataPointAddRangeWithSimpleProperties()
        {
            var items = new List<Item> { new Item { A = 1.1, B = 2.2 } };
            var result = new List<DataPoint>();
            result.AddRange(items, "A", "B");
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].X, Is.EqualTo(1.1));
            Assert.That(result[0].Y, Is.EqualTo(2.2));
        }

        [Test]
        public void ListOfDataPointAddRangeWithPath()
        {
            var items = new List<Item> { new Item { Point = new DataPoint(1.1, 2.2) } };
            var result = new List<DataPoint>();
            result.AddRange(items, "Point.X", "Point.Y");
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].X, Is.EqualTo(1.1));
            Assert.That(result[0].Y, Is.EqualTo(2.2));
        }

        [Test]
        public void FormatIntegers()
        {
            var items = new[] { 1, 2, 3 };
            CollectionAssert.AreEqual(new[] { "1", "2", "3" }, items.Format(null, null, CultureInfo.InvariantCulture));
            CollectionAssert.AreEqual(new[] { "01", "02", "03" }, items.Format("", "00", CultureInfo.InvariantCulture));
            CollectionAssert.AreEqual(new[] { "Item 1", "Item 2", "Item 3" }, items.Format(null, "Item {0}", CultureInfo.InvariantCulture));
        }

        [Test]
        public void FormatStrings()
        {
            var items = new[] { "One", "Two", "Three" };
            CollectionAssert.AreEqual(new[] { "3", "3", "5" }, items.Format("Length", null, CultureInfo.InvariantCulture));
            CollectionAssert.AreEqual(new[] { "Item One", "Item Two", "Item Three" }, items.Format(null, "Item {0}", CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Represents a test item.
        /// </summary>
        public class Item
        {
            /// <summary>
            /// Gets or sets a value.
            /// </summary>
            public double A { get; set; }

            /// <summary>
            /// Gets or sets the b value.
            /// </summary>
            public double B { get; set; }

            /// <summary>
            /// Gets or sets the point.
            /// </summary>
            /// <value>
            /// The point.
            /// </value>
            public DataPoint Point { get; set; }

            /// <summary>
            /// Gets or sets the sub item.
            /// </summary>
            /// <value>
            /// The sub item.
            /// </value>
            public ReflectionPathTests.Item SubItem { get; set; }
        }
    }
}