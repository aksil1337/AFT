// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using Selenium.Utilities;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Selenium.Tests
{
    [TestFixture(nameof(ChromeDriver))]
    [TestFixture(nameof(InternetExplorerDriver))]
    class Omada
    {
        // Constructors

        public Omada(string typeName) => TypeName = typeName;

        // Properties

        public Actions Actions { get; private set; }

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
                    Driver = new InternetExplorerDriver(executingPath, new InternetExplorerOptions() { RequireWindowFocus = true });
                    break;
            }

            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);

            Actions = new Actions(Driver);
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

            CollectionAssert.Contains(UI.Header.Element.GetClassNames(), "is-medium");

            // Step 2

            UI.HeaderSearchInput.Element.SendKeys("gartner");
            UI.HeaderSearchInput.Element.Submit();
            WaitForPageLoad();

            var searchResultTexts = UI.MainSearchResultsLinks.Elements.Select(element => Regex.Match(element.Text, @"(?<=\d\. )(.*)").Value).ToList();

            Assert.Greater(searchResultTexts.Count(), 1);
            CollectionAssert.Contains(searchResultTexts, "Gartner IAM Summit 2016 - London");

            // Step 3

            UI.MainSearchResultsLinks.Elements[searchResultTexts.IndexOf("Gartner IAM Summit 2016 - London")].ScrollIntoView(Driver).Click();
            WaitForPageLoad();

            Assert.AreEqual("Gartner IAM Summit 2016 - London", UI.MainH1.Element.Text);

            // Step 4

            UI.HeaderNavMoreLink.Element.Hover(Actions);
            Wait.Until(ExpectedConditions.ElementIsVisible(UI.HeaderNavMoreNewsLink.By));
            UI.HeaderNavMoreNewsLink.Element.Click();
            WaitForPageLoad();

            CollectionAssert.Contains(UI.MainArticlesH1s.Elements.Select(element => element.Text), "Gartner IAM Summit 2016 - London");
        }
    }
}
