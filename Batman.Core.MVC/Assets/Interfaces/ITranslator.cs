﻿/*
Copyright (c) 2013 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

#region Usings
using System;
using Batman.Core.Bootstrapper.Interfaces;
using System.Web.Mvc;
using System.Collections.Generic;
using Batman.Core.MVC.Assets.Enums;
#endregion

namespace Batman.Core.MVC.Assets.Interfaces
{
    /// <summary>
    /// Translator interface
    /// </summary>
    public interface ITranslator
    {
        /// <summary>
        /// Processes a list of assets
        /// </summary>
        /// <param name="Assets">Assets to process</param>
        /// <returns>The processed assets</returns>
        IList<IAsset> Translate(IList<IAsset> Assets);

        /// <summary>
        /// Processes an individual asset
        /// </summary>
        /// <param name="Asset">Asset to process</param>
        /// <returns>The processed asset</returns>
        IAsset Translate(IAsset Asset);

        /// <summary>
        /// The asset type that this translates to
        /// </summary>
        AssetType TranslatesTo { get; }

        /// <summary>
        /// Determines if it can translate the individual asset
        /// </summary>
        /// <param name="Asset">Asset</param>
        /// <returns>True if it can translate the asset, false otherwise</returns>
        bool CanTranslate(IAsset Asset);
    }
}
