﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BmpDecoder.cs" company="OxyPlot">
//   The MIT License (MIT)
//   
//   Copyright (c) 2014 OxyPlot contributors
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
//   Implements support for decoding bmp images.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot
{
    using System;
    using System.IO;

    /// <summary>
    /// Implements support for decoding bmp images.
    /// </summary>
    public class BmpDecoder : IImageDecoder
    {
        /// <summary>
        /// Gets information about the image in the specified byte array.
        /// </summary>
        /// <param name="bytes">The image data.</param>
        /// <returns>
        /// An <see cref="OxyImageInfo" /> structure.
        /// </returns>
        public OxyImageInfo GetImageInfo(byte[] bytes)
        {
            var ms = new MemoryStream(bytes);
            var r = new BinaryReader(ms);

            // bitmap file header
            r.ReadBytes(2); // headerField
            r.ReadUInt32(); // size
            r.ReadBytes(4); // reserved
            r.ReadUInt32(); // imageDataOffset

            // BITMAPINFOHEADER 
            r.ReadUInt32(); // headerSize
            var width = r.ReadInt32();
            var height = r.ReadInt32();
            r.ReadInt16(); // colorPlane
            var bitsPerPixel = r.ReadInt16();
            r.ReadInt32(); // compressionMethod
            r.ReadInt32(); // imageSize
            var horizontalResolution = r.ReadInt32();
            var verticalResolution = r.ReadInt32();
            r.ReadInt32(); // numberOfColors
            r.ReadInt32(); // importantColors

            return new OxyImageInfo
            {
                Width = width,
                Height = height,
                DpiX = horizontalResolution * 0.0254,
                DpiY = verticalResolution * 0.0254,
                BitsPerPixel = bitsPerPixel
            };
        }

        /// <summary>
        /// Decodes an image from the specified byte array.
        /// </summary>
        /// <param name="bytes">The image data.</param>
        /// <returns>
        /// The 32-bit pixel data.
        /// </returns>
        public OxyColor[,] Decode(byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}