// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using OpenQA.Selenium;

namespace Selenium.Utilities
{
    /// <summary>
    /// Exposes a locator in addition to the element itself.
    /// </summary>
    class UIElement
    {
        // Constructors

        public UIElement(IWebDriver driver, By by)
        {
            By = by;
            Element = driver.FindElement(By);
        }

        // Properties

        public By By { get; } = null;

        public IWebElement Element { get; }
    }
}
