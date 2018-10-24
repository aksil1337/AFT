// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using Selenium.Utilities;
using System.Linq;
using System.Text.RegularExpressions;

namespace Selenium.Tests
{
    [TestFixture(nameof(ChromeDriver))]
    [TestFixture(nameof(InternetExplorerDriver))]
    class Omada : TestBase
    {
        // Constructors

        public Omada(string typeName) : base(typeName) { }

        // Properties

        protected OmadaMap UI { get; } = new OmadaMap();

        // Tests

        [Test]
        public void EndToEnd()
        {
            var searchText = "gartner";
            var articleTitle = "Gartner IAM Summit 2016 - London";

            // Step 1

            UI.Page.Maximize().GoToUrl("https://www.omada.net/").WaitForNewPage();

            CollectionAssert.Contains(UI.Header.ClassNames, "is-medium");

            // Step 2

            UI.HeaderSearchInput.SendKeys(searchText).Submit().WaitForNewPage();

            var searchResultTexts = UI.MainSearchResultsLinks.Elements.Select(element => Regex.Match(element.Text, @"(?<=\d\. )(.*)").Value).ToList();

            Assert.Greater(searchResultTexts.Count(), 1);
            CollectionAssert.Contains(searchResultTexts, articleTitle);

            // Step 3

            UI.MainSearchResultsLinks[searchResultTexts.IndexOf(articleTitle)].ScrollIntoView().Click().WaitForNewPage();

            Assert.AreEqual(articleTitle, UI.MainH1.Text);

            // Step 4

            UI.HeaderNavMoreLink.Hover();
            UI.HeaderNavMoreNewsLink.WaitUntilVisible().Click().WaitForNewPage();

            CollectionAssert.Contains(UI.MainArticlesH1s.Texts, articleTitle);

            // Step 5

            UI.HeaderHomeLink.Click().WaitForNewPage();
            UI.HeaderContactLink.Click().WaitForNewPage();

            var previousClasses = UI.MainTabsUsWestSpan.ClassNames;
            UI.MainTabsUsWestSpan.Click();
            var currentClasses = UI.MainTabsUsWestSpan.ClassNames;

            CollectionAssert.IsSupersetOf(currentClasses, previousClasses);

            UI.Page.SaveScreenshot("screenshots/0-before-mouse-hover-on-tab");
            UI.MainTabsGermanySpan.Hover();
            UI.Page.SaveScreenshot("screenshots/1-after-mouse-hover-on-tab");

            // Step 6

            UI.CookieReadPrivacyPolicyLink.SetAttribute("target", "_blank").Click();
            UI.Page.SwitchToTab(Index.Last).WaitForNewPage();

            // Step 7

            UI.Page.SwitchToTab(Index.First);
            UI.CookieCloseButtonSpan.Click();

            UI.Page.CloseTab(Index.Last);
            UI.Page.Refresh().WaitForNewPage();

            UI.CookieReadPrivacyPolicyLink.WaitUntilInvisibile();
        }
    }
}
