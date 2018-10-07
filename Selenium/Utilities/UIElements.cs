// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Selenium.Utilities
{
    /// <summary>
    /// Provides a wrapper to extend the functionality of the UI web elements.
    /// </summary>
    class UIElements : UIBase
    {
        // Constructors

        public UIElements(UIMap map, RemoteWebDriver driver) : base(map, driver) { }

        // Properties

        /// <summary>
        /// Gets the underlying element at the specified index.
        /// </summary>
        /// <param name="i">The zero-based index of the element to get.</param>
        /// <returns>The element at the specified index.</returns>
        public UIElement this[int i] => Map.FindElement($"({By.ToString().Substring(10)})[{i + 1}]");

        /// <summary>
        /// Gets or sets the underlying collection of elements.
        /// </summary>
        public ReadOnlyCollection<IWebElement> Elements { get; internal set; }

        /// <summary>
        /// Gets the inner texts of all elements.
        /// </summary>
        /// <remarks>
        /// Any leading or trailing whitespace is removed; other whitespace is collapsed.
        /// </remarks>
        public IEnumerable<string> Texts => Elements.Select(element => element.Text);
    }
}
