// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System.Collections.ObjectModel;

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

        public IWebElement Header => Driver.FindElementByXPath("//header");
        public IWebElement HeaderSearchInput => Driver.FindElementByXPath("//header//form[@class='header__search']/input");

        public IWebElement MainH1 => Driver.FindElementByXPath("//main//h1");
        public ReadOnlyCollection<IWebElement> MainSearchResultsLinks => Driver.FindElementsByXPath("//main//div[@class='search-results__content']/section/a");
    }
}
