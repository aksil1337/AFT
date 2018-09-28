// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Selenium.Utilities
{
    /// <summary>
    /// Provides the access to the UI elements.
    /// </summary>
    class UIMap
    {
        // Constructors

        public UIMap(RemoteWebDriver driver)
        {
            Element = new UIElement(this, driver);
            Elements = new UIElements(this, driver);
            Page = new UIPage(this, driver);
        }

        // Properties

        protected UIElement Element { get; }

        protected UIElements Elements { get; }

        public UIPage Page { get; }

        // Methods

        /// <summary>
        /// Finds the first element in the page that matches the XPath supplied.
        /// </summary>
        /// <param name="xpath">An XPath to the element.</param>
        /// <returns>The first element found, wrapped in the functionality extender.</returns>
        public UIElement FindElement(string xpath)
        {
            Element.By = By.XPath(xpath);
            Element.Element = Element.Driver.FindElement(Element.By);

            return Element;
        }

        /// <summary>
        /// Finds a list of elements that match the XPath supplied.
        /// </summary>
        /// <param name="xpath">An XPath to the elements.</param>
        /// <returns>The list of elements found, wrapped in the functionality extender.</returns>
        public UIElements FindElements(string xpath)
        {
            Elements.By = By.XPath(xpath);
            Elements.Elements = Elements.Driver.FindElements(Elements.By);

            return Elements;
        }

        // UI Elements

        public UIElement Header => FindElement("//header");
        public UIElement HeaderHomeLink => FindElement("//header//a[@class='header__home']");
        public UIElement HeaderContactLink => FindElement("//header//a[text()='Contact']");
        public UIElement HeaderNavMoreLink => FindElement("//header//a[text()='More...']");
        public UIElement HeaderNavMoreNewsLink => FindElement("//header//a[text()='News']");
        public UIElement HeaderSearchInput => FindElement("//header//form[@class='header__search']/input");

        public UIElement MainH1 => FindElement("//main//h1");
        public UIElements MainSearchResultsLinks => FindElements("//main//div[@class='search-results__content']/section/a");
        public UIElements MainArticlesH1s => FindElements("//main//div[@class='cases__items']//h1");
        public UIElement MainTabsUsWestSpan => FindElement("//main//span[text()='U.S. West']");
        public UIElement MainTabsGermanySpan => FindElement("//main//span[text()='Germany']");

        public UIElement CookieReadPrivacyPolicyLink => FindElement("//div[@class='cookiebar__container']//a[text()='Read Privacy Policy']");
        public UIElement CookieCloseButtonSpan => FindElement("//div[@class='cookiebar__container']//span[contains(@class,'cookiebar__button')]");
    }
}
