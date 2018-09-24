// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Selenium.Utilities
{
    /// <summary>
    /// Provides a set of extension methods for test related classes and interfaces.
    /// </summary>
    static class Extensions
    {
        /// <summary>
        /// Gets the class names of the element.
        /// </summary>
        /// <param name="source">The source element.</param>
        /// <returns>A sequence of class names.</returns>
        public static IEnumerable<string> GetClassNames(this IWebElement source)
        {
            return source.GetAttribute("class").Split(' ').Where(text => !string.IsNullOrEmpty(text));
        }
    }
}
