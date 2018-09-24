// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Selenium.Utilities;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using System.Reflection;

namespace Selenium.Tests
{
    [TestFixture(nameof(ChromeDriver))]
    [TestFixture(nameof(InternetExplorerDriver))]
    class Omada
    {
        // Constructors

        public Omada(string typeName) => TypeName = typeName;

        // Properties

        protected RemoteWebDriver Driver { get; private set; }

        protected IWebElement HtmlElement { get; private set; }

        protected string TypeName { get; }

        protected UIMap UI { get; private set; }

        protected WebDriverWait Wait { get; private set; }

        // Helper methods

        /// <summary>
        /// Waits for the page to be loaded propely.
        /// </summary>
        public void WaitForPageLoad()
        {
            Wait.Until(ExpectedConditions.StalenessOf(HtmlElement));
            Wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            HtmlElement = Driver.FindElement(By.TagName("html"));
        }

        // Setup and Teardown

        /// <summary>
        /// Runs the specified code before each test.
        /// </summary>
        [SetUp]
        public void BeforeEach()
        {
            var executingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            switch (TypeName)
            {
                case nameof(ChromeDriver):
                    Driver = new ChromeDriver(executingPath);
                    break;
                case nameof(InternetExplorerDriver):
                    Driver = new InternetExplorerDriver(executingPath);
                    break;
            }

            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);

            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

            UI = new UIMap(Driver);
        }

        /// <summary>
        /// Runs the specified code after each test.
        /// </summary>
        [TearDown]
        public void AfterEach()
        {
            Driver.Quit();
        }

        // Tests

        [Test]
        public void EndToEnd()
        {
            // Step 1

            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl("https://www.omada.net/");
            WaitForPageLoad();

            CollectionAssert.Contains(UI.Header.GetClassNames(), "is-medium");

            // Step 2

            UI.HeaderSearchInput.SendKeys("gartner");
            UI.HeaderSearchInput.Submit();
            WaitForPageLoad();
        }
    }
}
