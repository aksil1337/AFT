// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Collections.ObjectModel;

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
        
        public UIElement this[int i] => Map.FindElement($"({By.ToString().Substring(10)})[{i + 1}]");

        public ReadOnlyCollection<IWebElement> Elements { get; internal set; }
    }
}
