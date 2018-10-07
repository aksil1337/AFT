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
    class UIBase
    {
        // Constructors

        public UIBase(UIMap map, RemoteWebDriver driver)
        {
            Map = map;
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Properties

        public By By { get; internal set; }

        public RemoteWebDriver Driver { get; }

        protected IWebElement HtmlElement { get; private set; }

        protected UIMap Map { get; }

        protected WebDriverWait Wait { get; }

        // Methods

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
