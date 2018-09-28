// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using SeleniumExtras.WaitHelpers;
using System.Collections.Generic;
using System.Linq;

namespace Selenium.Utilities
{
    /// <summary>
    /// Provides a wrapper to extend the functionality of the UI web element.
    /// </summary>
    class UIElement : UIBase
    {
        // Constructors

        public UIElement(UIMap map, RemoteWebDriver driver) : base(map, driver) { }

        // Properties

        public IWebElement Element { get; internal set; }

        // Methods

        /// <summary>
        /// Clicks the element.
        /// </summary>
        /// <returns>An instance of this class.</returns>
        public UIElement Click()
        {
            Element.Click();

            return this;
        }

        /// <summary>
        /// Gets the class names of the element.
        /// </summary>
        /// <returns>A sequence of class names.</returns>
        public IEnumerable<string> GetClassNames()
        {
            return Element.GetAttribute("class").Split(' ').Where(text => !string.IsNullOrEmpty(text));
        }

        /// <summary>
        /// Hovers the cursor over the element.
        /// </summary>
        /// <returns>An instance of this class.</returns>
        public UIElement Hover()
        {
            new Actions(Driver).MoveToElement(Element).Build().Perform();

            return this;
        }

        /// <summary>
        /// Scrolls the element into the visible area of the browser window.
        /// </summary>
        /// <returns>An instance of this class.</returns>
        public UIElement ScrollIntoView()
        {
            // The following script mimics the behavior of scrollIntoView({})
            // as it is considered experimental API and does not work in IE and Safari
            Driver.ExecuteScript("window.scrollTo(0, arguments[0].offsetTop + (arguments[0].offsetHeight / 2) - (window.innerHeight / 2));", Element);

            return this;
        }

        /// <summary>
        /// Simulates typing text into the element.
        /// </summary>
        /// <param name="text">The text to type into the element.</param>
        /// <returns>An instance of this class.</returns>
        public UIElement SendKeys(string text)
        {
            Element.SendKeys(text);

            return this;
        }

        /// <summary>
        /// Sets the value of an attribute on the element.
        /// </summary>
        /// <remarks>
        /// If the attribute already exists, the value is updated; otherwise a new attribute is added with the specified name and value.
        /// </remarks>
        /// <param name="name">The name of the attribute whose value is to be set.</param>
        /// <param name="value">The value to assign to the attribute.</param>
        /// <returns>An instance of this class.</returns>
        public UIElement SetAttribute(string name, string value)
        {
            Driver.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", Element, name, value);

            return this;
        }

        /// <summary>
        /// Submits the element to the web server.
        /// </summary>
        /// <returns>An instance of this class.</returns>
        public UIElement Submit()
        {
            Element.Submit();

            return this;
        }

        /// <summary>
        /// Waits for the element to be invisible.
        /// </summary>
        /// <returns>An instance of this class.</returns>
        public UIElement WaitUntilInvisibile()
        {
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By));

            return this;
        }

        /// <summary>
        /// Waits for the element to be visible.
        /// </summary>
        /// <returns>An instance of this class.</returns>
        public UIElement WaitUntilVisible()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By));

            return this;
        }
    }
}
