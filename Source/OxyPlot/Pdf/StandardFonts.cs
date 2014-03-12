﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandardFonts.cs" company="OxyPlot">
//   The MIT License (MIT)
//   
//   Copyright (c) 2012 Oystein Bjorke
//   
//   Permission is hereby granted, free of charge, to any person obtaining a
//   copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//   
//   The above copyright notice and this permission notice shall be included
//   in all copies or substantial portions of the Software.
//   
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
//   OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//   IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
//   CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
//   TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
//   SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary>
//   Defines the standard fonts that can be used in a <see cref="PortableDocument"/>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot
{
    /// <summary>
    /// Defines the standard fonts that can be used in a <see cref="PortableDocument"/>.
    /// </summary>
    public static class StandardFonts
    {
        /// <summary>
        /// Initializes static members of the <see cref="StandardFonts"/> class.
        /// </summary>
        static StandardFonts()
        {
            //// TODO: Change to Type 1 fonts?

            var arialRegular = new PortableDocumentFont
                        {
                            Ascent = 905,
                            CapHeight = 716,
                            Descent = -212,
                            Flags = 32,
                            FontBoundingBox = new[] { -665, -325, 2046, 1040 },
                            ItalicAngle = 0,
                            StemV = 0,
                            XHeight = 519,
                            SubType = FontSubType.TrueType,
                            FontName = "Arial",
                            BaseFont = "Arial",
                            Encoding = FontEncoding.WinAnsiEncoding,
                            FirstChar = 0,
                            Widths = new[]
                                         {
                                             750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750,
                                             750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750,
                                             750, 750, 750, 750, 277, 277, 354, 556, 556, 889, 666, 190, 333, 333,
                                             389, 583, 277, 333, 277, 277, 556, 556, 556, 556, 556, 556, 556, 556,
                                             556, 556, 277, 277, 583, 583, 583, 556, 1015, 666, 666, 722, 722,
                                             666, 610, 777, 722, 277, 500, 666, 556, 833, 722, 777, 666, 777, 722,
                                             666, 610, 722, 666, 943, 666, 666, 610, 277, 277, 277, 469, 556, 333,
                                             556, 556, 500, 556, 556, 277, 556, 556, 222, 222, 500, 222, 833, 556,
                                             556, 556, 556, 333, 500, 277, 556, 500, 722, 500, 500, 500, 333, 259,
                                             333, 583, 750, 556, 750, 222, 556, 333, 1000, 556, 556, 333, 1000,
                                             666, 333, 1000, 750, 610, 750, 750, 222, 222, 333, 333, 350, 556,
                                             1000, 333, 1000, 500, 333, 943, 750, 500, 666, 277, 333, 556, 556,
                                             556, 556, 259, 556, 333, 736, 370, 556, 583, 333, 736, 552, 399, 548,
                                             333, 333, 333, 576, 537, 333, 333, 333, 365, 556, 833, 833, 833, 610,
                                             666, 666, 666, 666, 666, 666, 1000, 722, 666, 666, 666, 666, 277,
                                             277, 277, 277, 722, 722, 777, 777, 777, 777, 777, 583, 777, 722, 722,
                                             722, 722, 666, 666, 610, 556, 556, 556, 556, 556, 556, 889, 500, 556,
                                             556, 556, 556, 277, 277, 277, 277, 556, 556, 556, 556, 556, 556, 556,
                                             548, 610, 556, 556, 556, 556, 500, 556, 500
                                         }
                        };
            var arialItalic = new PortableDocumentFont
                              {
                                  Ascent = 905,
                                  CapHeight = 716,
                                  Descent = -212,
                                  Flags = 32,
                                  FontBoundingBox = new[] { -517, -325, 1359, 998 },
                                  ItalicAngle = -12,
                                  StemV = 0,
                                  XHeight = 519,
                                  SubType = FontSubType.TrueType,
                                  FontName = "Arial,Italic",
                                  BaseFont = "Arial,Italic",
                                  Encoding = FontEncoding.WinAnsiEncoding,
                                  FirstChar = 0,
                                  Widths =
                                      new[]
                                          {
                                              750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750,
                                              750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750,
                                              750, 750, 750, 750, 277, 277, 354, 556, 556, 889, 666, 190, 333, 333,
                                              389, 583, 277, 333, 277, 277, 556, 556, 556, 556, 556, 556, 556, 556,
                                              556, 556, 277, 277, 583, 583, 583, 556, 1015, 666, 666, 722, 722, 666,
                                              610, 777, 722, 277, 500, 666, 556, 833, 722, 777, 666, 777, 722, 666,
                                              610, 722, 666, 943, 666, 666, 610, 277, 277, 277, 469, 556, 333, 556,
                                              556, 500, 556, 556, 277, 556, 556, 222, 222, 500, 222, 833, 556, 556,
                                              556, 556, 333, 500, 277, 556, 500, 722, 500, 500, 500, 333, 259, 333,
                                              583, 750, 556, 750, 222, 556, 333, 1000, 556, 556, 333, 1000, 666, 333,
                                              1000, 750, 610, 750, 750, 222, 222, 333, 333, 350, 556, 1000, 333, 1000,
                                              500, 333, 943, 750, 500, 666, 277, 333, 556, 556, 556, 556, 259, 556,
                                              333, 736, 370, 556, 583, 333, 736, 552, 399, 548, 333, 333, 333, 576,
                                              537, 333, 333, 333, 365, 556, 833, 833, 833, 610, 666, 666, 666, 666,
                                              666, 666, 1000, 722, 666, 666, 666, 666, 277, 277, 277, 277, 722, 722,
                                              777, 777, 777, 777, 777, 583, 777, 722, 722, 722, 722, 666, 666, 610,
                                              556, 556, 556, 556, 556, 556, 889, 500, 556, 556, 556, 556, 277, 277,
                                              277, 277, 556, 556, 556, 556, 556, 556, 556, 548, 610, 556, 556, 556,
                                              556, 500, 556, 500
                                          }
                              };
            var arialBold = new PortableDocumentFont
                            {
                                Ascent = 905,
                                CapHeight = 716,
                                Descent = -212,
                                Flags = 32,
                                FontBoundingBox = new[] { -628, -376, 2000, 1056 },
                                ItalicAngle = 0,
                                StemV = 0,
                                XHeight = 519,
                                SubType = FontSubType.TrueType,
                                FontName = "Arial,Bold",
                                BaseFont = "Arial,Bold",
                                Encoding = FontEncoding.WinAnsiEncoding,
                                FirstChar = 0,
                                Widths =
                                    new[]
                                        {
                                            750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750,
                                            750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750,
                                            750, 750, 277, 333, 474, 556, 556, 889, 722, 237, 333, 333, 389, 583, 277,
                                            333, 277, 277, 556, 556, 556, 556, 556, 556, 556, 556, 556, 556, 333, 333,
                                            583, 583, 583, 610, 975, 722, 722, 722, 722, 666, 610, 777, 722, 277, 556,
                                            722, 610, 833, 722, 777, 666, 777, 722, 666, 610, 722, 666, 943, 666, 666,
                                            610, 333, 277, 333, 583, 556, 333, 556, 610, 556, 610, 556, 333, 610, 610,
                                            277, 277, 556, 277, 889, 610, 610, 610, 610, 389, 556, 333, 610, 556, 777,
                                            556, 556, 500, 389, 279, 389, 583, 750, 556, 750, 277, 556, 500, 1000, 556,
                                            556, 333, 1000, 666, 333, 1000, 750, 610, 750, 750, 277, 277, 500, 500,
                                            350, 556, 1000, 333, 1000, 556, 333, 943, 750, 500, 666, 277, 333, 556,
                                            556, 556, 556, 279, 556, 333, 736, 370, 556, 583, 333, 736, 552, 399, 548,
                                            333, 333, 333, 576, 556, 333, 333, 333, 365, 556, 833, 833, 833, 610, 722,
                                            722, 722, 722, 722, 722, 1000, 722, 666, 666, 666, 666, 277, 277, 277, 277,
                                            722, 722, 777, 777, 777, 777, 777, 583, 777, 722, 722, 722, 722, 666, 666,
                                            610, 556, 556, 556, 556, 556, 556, 889, 556, 556, 556, 556, 556, 277, 277,
                                            277, 277, 610, 610, 610, 610, 610, 610, 610, 548, 610, 610, 610, 610, 610,
                                            556, 610, 556
                                        }
                            };
            var arialBoldItalic = new PortableDocumentFont
                                  {
                                      Ascent = 905,
                                      CapHeight = 716,
                                      Descent = -212,
                                      Flags = 32,
                                      FontBoundingBox = new[] { -560, -376, 1390, 1018 },
                                      ItalicAngle = 0,
                                      StemV = 0,
                                      XHeight = 519,
                                      SubType = FontSubType.TrueType,
                                      FontName = "Arial,BoldItalic",
                                      BaseFont = "Arial,BoldItalic",
                                      Encoding = FontEncoding.WinAnsiEncoding,
                                      FirstChar = 0,
                                      Widths =
                                          new[]
                                              {
                                                  750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750,
                                                  750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750, 750,
                                                  750, 750, 750, 750, 277, 333, 474, 556, 556, 889, 722, 237, 333, 333,
                                                  389, 583, 277, 333, 277, 277, 556, 556, 556, 556, 556, 556, 556, 556,
                                                  556, 556, 333, 333, 583, 583, 583, 610, 975, 722, 722, 722, 722, 666,
                                                  610, 777, 722, 277, 556, 722, 610, 833, 722, 777, 666, 777, 722, 666,
                                                  610, 722, 666, 943, 666, 666, 610, 333, 277, 333, 583, 556, 333, 556,
                                                  610, 556, 610, 556, 333, 610, 610, 277, 277, 556, 277, 889, 610, 610,
                                                  610, 610, 389, 556, 333, 610, 556, 777, 556, 556, 500, 389, 279, 389,
                                                  583, 750, 556, 750, 277, 556, 500, 1000, 556, 556, 333, 1000, 666,
                                                  333, 1000, 750, 610, 750, 750, 277, 277, 500, 500, 350, 556, 1000,
                                                  333, 1000, 556, 333, 943, 750, 500, 666, 277, 333, 556, 556, 556,
                                                  556, 279, 556, 333, 736, 370, 556, 583, 333, 736, 552, 399, 548, 333,
                                                  333, 333, 576, 556, 333, 333, 333, 365, 556, 833, 833, 833, 610, 722,
                                                  722, 722, 722, 722, 722, 1000, 722, 666, 666, 666, 666, 277, 277,
                                                  277, 277, 722, 722, 777, 777, 777, 777, 777, 583, 777, 722, 722, 722,
                                                  722, 666, 666, 610, 556, 556, 556, 556, 556, 556, 889, 556, 556, 556,
                                                  556, 556, 277, 277, 277, 277, 610, 610, 610, 610, 610, 610, 610, 548,
                                                  610, 610, 610, 610, 610, 556, 610, 556
                                              }
                                  };

            Helvetica = new PortableDocumentFontFamily
            {
                RegularFont = arialRegular,
                BoldFont = arialBold,
                ItalicFont = arialItalic,
                BoldItalicFont = arialBoldItalic
            };

            var timesRegular  = new PortableDocumentFont
            {
                Ascent = 891,
                CapHeight = 662,
                Descent = -216,
                Flags = 32,
                FontBoundingBox = new[] { -568, -307, 2046, 1040 },
                ItalicAngle = 0,
                StemV = 0,
                XHeight = 447,
                SubType = FontSubType.TrueType,
                FontName = "TimesNewRoman",
                BaseFont = "TimesNewRoman",
                Encoding = FontEncoding.WinAnsiEncoding,
                FirstChar = 0,
                Widths =
                    new[]
                    {
                        777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777,
                        777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 250, 333, 408, 500, 500, 833, 777,
                        180, 333, 333, 500, 563, 250, 333, 250, 277, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500,
                        277, 277, 563, 563, 563, 443, 920, 722, 666, 666, 722, 610, 556, 722, 722, 333, 389, 722, 610,
                        889, 722, 722, 556, 722, 666, 556, 610, 722, 722, 943, 722, 722, 610, 333, 277, 333, 469, 500,
                        333, 443, 500, 443, 500, 443, 333, 500, 500, 277, 277, 500, 277, 777, 500, 500, 500, 500, 333,
                        389, 277, 500, 500, 722, 500, 500, 443, 479, 200, 479, 541, 777, 500, 777, 333, 500, 443, 1000,
                        500, 500, 333, 1000, 556, 333, 889, 777, 610, 777, 777, 333, 333, 443, 443, 350, 500, 1000, 333,
                        979, 389, 333, 722, 777, 443, 722, 250, 333, 500, 500, 500, 500, 200, 500, 333, 759, 275, 500,
                        563, 333, 759, 500, 399, 548, 299, 299, 333, 576, 453, 333, 333, 299, 310, 500, 750, 750, 750,
                        443, 722, 722, 722, 722, 722, 722, 889, 666, 610, 610, 610, 610, 333, 333, 333, 333, 722, 722,
                        722, 722, 722, 722, 722, 563, 722, 722, 722, 722, 722, 722, 556, 500, 443, 443, 443, 443, 443,
                        443, 666, 443, 443, 443, 443, 443, 277, 277, 277, 277, 500, 500, 500, 500, 500, 500, 500, 548,
                        500, 500, 500, 500, 500, 500, 500, 500
                    }
            };
            var timesItalic = new PortableDocumentFont
            {
                Ascent = 891,
                CapHeight = 662,
                Descent = -216,
                Flags = 32,
                FontBoundingBox = new[] { -498, -307, 1333, 1023 },
                ItalicAngle = -12,
                StemV = 0,
                XHeight = 430,
                SubType = FontSubType.TrueType,
                FontName = "TimesNewRoman,Italic",
                BaseFont = "TimesNewRoman,Italic",
                Encoding = FontEncoding.WinAnsiEncoding,
                FirstChar = 0,
                Widths =
                    new[]
                    {
                        777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777,
                        777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 250, 333, 419, 500, 500, 833, 777,
                        213, 333, 333, 500, 674, 250, 333, 250, 277, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500,
                        333, 333, 674, 674, 674, 500, 919, 610, 610, 666, 722, 610, 610, 722, 722, 333, 443, 666, 556,
                        833, 666, 722, 610, 722, 610, 500, 556, 722, 610, 833, 610, 556, 556, 389, 277, 389, 421, 500,
                        333, 500, 500, 443, 500, 443, 277, 500, 500, 277, 277, 443, 277, 722, 500, 500, 500, 500, 389,
                        389, 277, 500, 443, 666, 443, 443, 389, 399, 274, 399, 541, 777, 500, 777, 333, 500, 556, 889,
                        500, 500, 333, 1000, 500, 333, 943, 777, 556, 777, 777, 333, 333, 556, 556, 350, 500, 889, 333,
                        979, 389, 333, 666, 777, 389, 556, 250, 389, 500, 500, 500, 500, 274, 500, 333, 759, 275, 500,
                        674, 333, 759, 500, 399, 548, 299, 299, 333, 576, 522, 250, 333, 299, 310, 500, 750, 750, 750,
                        500, 610, 610, 610, 610, 610, 610, 889, 666, 610, 610, 610, 610, 333, 333, 333, 333, 722, 666,
                        722, 722, 722, 722, 722, 674, 722, 722, 722, 722, 722, 556, 610, 500, 500, 500, 500, 500, 500,
                        500, 666, 443, 443, 443, 443, 443, 277, 277, 277, 277, 500, 500, 500, 500, 500, 500, 500, 548,
                        500, 500, 500, 500, 500, 443, 500, 443
                    }
            };
            var timesBold = new PortableDocumentFont
            {
                Ascent = 891,
                CapHeight = 662,
                Descent = -216,
                Flags = 32,
                FontBoundingBox = new[] { -558, -328, 2000, 1056 },
                ItalicAngle = 0,
                StemV = 0,
                XHeight = 457,
                SubType = FontSubType.TrueType,
                FontName = "TimesNewRoman,Bold",
                BaseFont = "TimesNewRoman,Bold",
                Encoding = FontEncoding.WinAnsiEncoding,
                FirstChar = 0,
                Widths =
                    new[]
                    {
                        777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777,
                        777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 250, 333, 555, 500, 500, 1000, 833,
                        277, 333, 333, 500, 569, 250, 333, 250, 277, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500,
                        333, 333, 569, 569, 569, 500, 930, 722, 666, 722, 722, 666, 610, 777, 777, 389, 500, 777, 666,
                        943, 722, 777, 610, 777, 722, 556, 666, 722, 722, 1000, 722, 722, 666, 333, 277, 333, 581, 500,
                        333, 500, 556, 443, 556, 443, 333, 500, 556, 277, 333, 556, 277, 833, 556, 500, 556, 556, 443,
                        389, 333, 556, 500, 722, 500, 500, 443, 394, 220, 394, 520, 777, 500, 777, 333, 500, 500, 1000,
                        500, 500, 333, 1000, 556, 333, 1000, 777, 666, 777, 777, 333, 333, 500, 500, 350, 500, 1000, 333,
                        1000, 389, 333, 722, 777, 443, 722, 250, 333, 500, 500, 500, 500, 220, 500, 333, 747, 299, 500,
                        569, 333, 747, 500, 399, 548, 299, 299, 333, 576, 540, 333, 333, 299, 330, 500, 750, 750, 750,
                        500, 722, 722, 722, 722, 722, 722, 1000, 722, 666, 666, 666, 666, 389, 389, 389, 389, 722, 722,
                        777, 777, 777, 777, 777, 569, 777, 722, 722, 722, 722, 722, 610, 556, 500, 500, 500, 500, 500,
                        500, 722, 443, 443, 443, 443, 443, 277, 277, 277, 277, 500, 556, 500, 500, 500, 500, 500, 548,
                        500, 556, 556, 556, 556, 500, 556, 500
                    }
            };
            var timesBoldItalic = new PortableDocumentFont
            {
                Ascent = 891,
                CapHeight = 662,
                Descent = -216,
                Flags = 32,
                FontBoundingBox = new[] { -547, -307, 1401, 1032 },
                ItalicAngle = 0,
                StemV = 0,
                XHeight = 439,
                SubType = FontSubType.TrueType,
                FontName = "TimesNewRoman,BoldItalic",
                BaseFont = "TimesNewRoman,BoldItalic",
                Encoding = FontEncoding.WinAnsiEncoding,
                FirstChar = 0,
                Widths =
                    new[]
                    {
                        777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777,
                        777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 777, 250, 389, 555, 500, 500, 833, 777,
                        277, 333, 333, 500, 569, 250, 333, 250, 277, 500, 500, 500, 500, 500, 500, 500, 500, 500, 500,
                        333, 333, 569, 569, 569, 500, 832, 666, 666, 666, 722, 666, 666, 722, 777, 389, 500, 666, 610,
                        889, 722, 722, 610, 722, 666, 556, 610, 722, 666, 889, 666, 610, 610, 333, 277, 333, 569, 500,
                        333, 500, 500, 443, 500, 443, 333, 500, 556, 277, 277, 500, 277, 777, 556, 500, 500, 500, 389,
                        389, 277, 556, 443, 666, 500, 443, 389, 348, 220, 348, 569, 777, 500, 777, 333, 500, 500, 1000,
                        500, 500, 333, 1000, 556, 333, 943, 777, 610, 777, 777, 333, 333, 500, 500, 350, 500, 1000, 333,
                        1000, 389, 333, 722, 777, 389, 610, 250, 389, 500, 500, 500, 500, 220, 500, 333, 747, 266, 500,
                        605, 333, 747, 500, 399, 548, 299, 299, 333, 576, 500, 250, 333, 299, 299, 500, 750, 750, 750,
                        500, 666, 666, 666, 666, 666, 666, 943, 666, 666, 666, 666, 666, 389, 389, 389, 389, 722, 722,
                        722, 722, 722, 722, 722, 569, 722, 722, 722, 722, 722, 610, 610, 500, 500, 500, 500, 500, 500,
                        500, 722, 443, 443, 443, 443, 443, 277, 277, 277, 277, 500, 556, 500, 500, 500, 500, 500, 548,
                        500, 556, 556, 556, 556, 443, 500, 443
                    }
            };
            Times = new PortableDocumentFontFamily
            {
                RegularFont = timesRegular,
                BoldFont = timesBold,
                ItalicFont = timesItalic,
                BoldItalicFont = timesBoldItalic
            };

            var courierRegular = new PortableDocumentFont
            {
                Ascent = 833,
                CapHeight = 571,
                Descent = -300,
                Flags = 32,
                FontBoundingBox = new[] { -122, -680, 623, 1021 },
                ItalicAngle = 0,
                StemV = 0,
                XHeight = 423,
                SubType = FontSubType.TrueType,
                FontName = "Courier",
                BaseFont = "Courier",
                Encoding = FontEncoding.WinAnsiEncoding,
                FirstChar = 0,
                Widths =
                    new[]
                    {
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600
                    }
            };
            var courierItalic = new PortableDocumentFont
            {
                Ascent = 833,
                CapHeight = 571,
                Descent = -300,
                Flags = 32,
                FontBoundingBox = new[] { -67, -274, 800, 1000 },
                ItalicAngle = -12,
                StemV = 0,
                XHeight = 423,
                SubType = FontSubType.TrueType,
                FontName = "Courier,Italic",
                BaseFont = "Courier,Italic",
                Encoding = FontEncoding.WinAnsiEncoding,
                FirstChar = 0,
                Widths =
                    new[]
                    {
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600
                    }
            };
            var courierBold = new PortableDocumentFont
            {
                Ascent = 833,
                CapHeight = 592,
                Descent = -300,
                Flags = 32,
                FontBoundingBox = new[] { -192, -710, 702, 1221 },
                ItalicAngle = 0,
                StemV = 0,
                XHeight = 443,
                SubType = FontSubType.TrueType,
                FontName = "Courier,Bold",
                BaseFont = "Courier,Bold",
                Encoding = FontEncoding.WinAnsiEncoding,
                FirstChar = 0,
                Widths =
                    new[]
                    {
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600
                    }
            };
            var courierBoldItalic = new PortableDocumentFont
            {
                Ascent = 833,
                CapHeight = 592,
                Descent = -300,
                Flags = 32,
                FontBoundingBox = new[] { -103, -377, 839, 1004 },
                ItalicAngle = 0,
                StemV = 0,
                XHeight = 443,
                SubType = FontSubType.TrueType,
                FontName = "Courier,BoldItalic",
                BaseFont = "Courier,BoldItalic",
                Encoding = FontEncoding.WinAnsiEncoding,
                FirstChar = 0,
                Widths =
                    new[]
                    {
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600, 600,
                        600, 600, 600, 600, 600, 600, 600, 600
                    }
            };
            Courier = new PortableDocumentFontFamily
            {
                RegularFont = courierRegular,
                BoldFont = courierBold,
                ItalicFont = courierItalic,
                BoldItalicFont = courierBoldItalic
            };
        }

        /// <summary>
        /// Gets the Arial font family.
        /// </summary>
        public static PortableDocumentFontFamily Helvetica { get; private set; }

        /// <summary>
        /// Gets the Times font family.
        /// </summary>
        public static PortableDocumentFontFamily Times { get; private set; }

        /// <summary>
        /// Gets the Courier font family.
        /// </summary>
        public static PortableDocumentFontFamily Courier { get; private set; }
    }
}