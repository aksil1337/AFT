// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using Selenium.Utilities;
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

        protected RemoteWebDriver Driver { get; private set; }

        protected string TypeName { get; }

        protected UIMap UI { get; private set; }

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
            UI.Header.WaitForPageLoad();

            CollectionAssert.Contains(UI.Header.GetClassNames(), "is-medium");

            // Step 2

            UI.HeaderSearchInput.SendKeys("gartner").Submit().WaitForPageLoad();

            var searchResultTexts = UI.MainSearchResultsLinks.Elements.Select(element => Regex.Match(element.Text, @"(?<=\d\. )(.*)").Value).ToList();

            Assert.Greater(searchResultTexts.Count(), 1);
            CollectionAssert.Contains(searchResultTexts, "Gartner IAM Summit 2016 - London");

            // Step 3

            UI.MainSearchResultsLinks[searchResultTexts.IndexOf("Gartner IAM Summit 2016 - London")].ScrollIntoView().Click().WaitForPageLoad();

            Assert.AreEqual("Gartner IAM Summit 2016 - London", UI.MainH1.Element.Text);

            // Step 4

            UI.HeaderNavMoreLink.Hover();
            UI.HeaderNavMoreNewsLink.WaitUntilVisible().Click().WaitForPageLoad();

            CollectionAssert.Contains(UI.MainArticlesH1s.Elements.Select(element => element.Text), "Gartner IAM Summit 2016 - London");

            // Step 5

            UI.HeaderHomeLink.Click().WaitForPageLoad();
            UI.HeaderContactLink.Click().WaitForPageLoad();

            var previousClasses = UI.MainTabsUsWestSpan.GetClassNames();
            UI.MainTabsUsWestSpan.Click();
            var currentClasses = UI.MainTabsUsWestSpan.GetClassNames();

            CollectionAssert.IsSupersetOf(currentClasses, previousClasses);
        }
    }
}
