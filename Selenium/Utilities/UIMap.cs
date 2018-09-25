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

        public UIMap(RemoteWebDriver driver) => Driver = driver;

        // Properties

        public RemoteWebDriver Driver { get; }

        // UI Elements

        public UIElement Header => new UIElement(Driver, By.XPath("//header"));
        public UIElement HeaderNavMoreLink => new UIElement(Driver, By.XPath("//header//a[text()='More...']"));
        public UIElement HeaderNavMoreNewsLink => new UIElement(Driver, By.XPath("//header//a[text()='News']"));
        public UIElement HeaderSearchInput => new UIElement(Driver, By.XPath("//header//form[@class='header__search']/input"));

        public UIElement MainH1 => new UIElement(Driver, By.XPath("//main//h1"));
        public UIElements MainSearchResultsLinks => new UIElements(Driver, By.XPath("//main//div[@class='search-results__content']/section/a"));
        public UIElements MainArticlesH1s => new UIElements(Driver, By.XPath("//main//div[@class='cases__items']//h1"));
    }
}
