﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputCommandBinding.cs" company="OxyPlot">
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
//   Represents an binding by an input gesture and a command binding.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace OxyPlot
{
    /// <summary>
    /// Represents an binding by an input gesture and a command binding.
    /// </summary>
    public class InputCommandBinding
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputCommandBinding" /> class by a gesture.
        /// </summary>
        /// <param name="gesture">The gesture.</param>
        /// <param name="command">The command.</param>
        public InputCommandBinding(OxyInputGesture gesture, IViewCommand command)
        {
            this.Gesture = gesture;
            this.Command = command;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputCommandBinding" /> class by a key gesture.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="modifiers">The modifiers.</param>
        /// <param name="command">The command.</param>
        public InputCommandBinding(OxyKey key, OxyModifierKeys modifiers, IViewCommand command)
            : this(new OxyKeyGesture(key, modifiers), command)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputCommandBinding" /> class by a mouse gesture.
        /// </summary>
        /// <param name="mouseButton">The mouse button.</param>
        /// <param name="modifiers">The modifiers.</param>
        /// <param name="command">The command.</param>
        public InputCommandBinding(OxyMouseButton mouseButton, OxyModifierKeys modifiers, IViewCommand command)
            : this(new OxyMouseDownGesture(mouseButton, modifiers), command)
        {
        }

        /// <summary>
        /// Gets the gesture.
        /// </summary>
        public OxyInputGesture Gesture { get; private set; }

        /// <summary>
        /// Gets the command.
        /// </summary>
        public IViewCommand Command { get; private set; }
    }
}