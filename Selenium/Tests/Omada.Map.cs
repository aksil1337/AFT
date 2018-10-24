// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using Selenium.Utilities;

namespace Selenium.Tests
{
    /// <summary>
    /// Provides the access to the UI elements.
    /// </summary>
    class OmadaMap
    {
        // UI Elements

        public UIPage Page => new UIPage();

        public UIElement Header => new UIElement("//header");
        public UIElement HeaderHomeLink => new UIElement("//header//a[@class='header__home']");
        public UIElement HeaderContactLink => new UIElement("//header//a[text()='Contact']");
        public UIElement HeaderNavMoreLink => new UIElement("//header//a[text()='More...']");
        public UIElement HeaderNavMoreNewsLink => new UIElement("//header//a[text()='News']");
        public UIElement HeaderSearchInput => new UIElement("//header//form[@class='header__search']/input");

        public UIElement MainH1 => new UIElement("//main//h1");
        public UIElements MainSearchResultsLinks => new UIElements("//main//div[@class='search-results__content']/section/a");
        public UIElements MainArticlesH1s => new UIElements("//main//div[@class='cases__items']//h1");
        public UIElement MainTabsUsWestSpan => new UIElement("//main//span[text()='U.S. West']");
        public UIElement MainTabsGermanySpan => new UIElement("//main//span[text()='Germany']");

        public UIElement CookieReadPrivacyPolicyLink => new UIElement("//div[@class='cookiebar__container']//a[text()='Read Privacy Policy']");
        public UIElement CookieCloseButtonSpan => new UIElement("//div[@class='cookiebar__container']//span[contains(@class,'cookiebar__button')]");
    }
}
