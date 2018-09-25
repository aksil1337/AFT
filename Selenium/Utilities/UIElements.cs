// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Selenium.Utilities
{
    /// <summary>
    /// Exposes a locator in addition to the elements themselves.
    /// </summary>
    class UIElements
    {
        // Constructors

        public UIElements(IWebDriver driver, By by)
        {
            By = by;
            Elements = driver.FindElements(By);
        }

        // Properties

        public By By { get; } = null;

        public ReadOnlyCollection<IWebElement> Elements { get; }
    }
}
