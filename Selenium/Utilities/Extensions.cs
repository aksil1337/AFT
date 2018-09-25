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

        /// <summary>
        /// Scrolls the element into the visible area of the browser window.
        /// </summary>
        /// <param name="source">The source element.</param>
        /// <param name="executor">A script executor.</param>
        /// <returns>An instance of the source element.</returns>
        public static IWebElement ScrollIntoView(this IWebElement source, IJavaScriptExecutor executor)
        {
            // The following script mimics the behavior of scrollIntoView({})
            // as it is considered experimental API and does not work in IE and Safari
            executor.ExecuteScript("window.scrollTo(0, arguments[0].offsetTop + (arguments[0].offsetHeight / 2) - (window.innerHeight / 2));", source);

            return source;
        }
    }
}
