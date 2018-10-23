// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Selenium.Utilities
{
    /// <summary>
    /// Provides a base class for UI web element wrappers. Exposes the locator and the driver used to find the element.
    /// </summary>
    abstract class UIBase
    {
        // Properties

        public static RemoteWebDriver Driver { get; private set; }

        protected static IWebElement HtmlElement { get; private set; }

        protected static WebDriverWait Wait { get; private set; }

        public By By { get; protected set; }

        // Methods

        /// <summary>
        /// Initializes the static properties used in the derived classes.
        /// </summary>
        /// <param name="driver">A web driver instance.</param>
        public static void Initialize(RemoteWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        /// Waits for the new page to be loaded propely.
        /// </summary>
        public void WaitForNewPage()
        {
            Wait.Until(ExpectedConditions.StalenessOf(HtmlElement));
            Wait.Until(_ => Driver.ExecuteScript("return document.readyState").Equals("complete"));
            HtmlElement = Driver.FindElement(By.TagName("html"));
        }
    }
}
