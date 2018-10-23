// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using OpenQA.Selenium;
using System.IO;
using System.Reflection;

namespace Selenium.Utilities
{
    /// <summary>
    /// Provides a wrapper to extend the functionality of the web page element.
    /// </summary>
    class UIPage : UIBase
    {
        // Constructors

        public UIPage()
        {
            By = By.TagName("html");
            TabIndex = Driver.WindowHandles.IndexOf(Driver.CurrentWindowHandle);
        }

        // Properties

        /// <summary>
        /// Gets the underlying HTML element.
        /// </summary>
        public IWebElement Element => HtmlElement;
        
        protected int TabIndex { get; private set; } = 0;

        // Methods

        /// <summary>
        /// Closes the current tab.
        /// </summary>
        /// <returns>An instance of this class.</returns>
        public UIPage CloseCurrentTab()
        {
            Driver.Close();
            SwitchToTab(Index.Previous);

            return this;
        }

        /// <summary>
        /// Closes the tab specified by the index.
        /// </summary>
        /// <param name="tab">An enumeration value that specifies the tab index.</param>
        /// <returns>An instance of this class.</returns>
        public UIPage CloseTab(Index tab)
        {
            SwitchToTab(tab);
            CloseCurrentTab();

            return this;
        }

        /// <summary>
        /// Load a new page in the current tab.
        /// </summary>
        /// <param name="url">The URL to load.</param>
        /// <returns>An instance of this class.</returns>
        public UIPage GoToUrl(string url)
        {
            Driver.Navigate().GoToUrl(url);

            return this;
        }

        /// <summary>
        /// Maximizes the current browser window.
        /// </summary>
        /// <returns>An instance of this class.</returns>
        public UIPage Maximize()
        {
            Driver.Manage().Window.Maximize();

            return this;
        }

        /// <summary>
        /// Refreshes the page in the current tab.
        /// </summary>
        /// <returns>An instance of this class.</returns>
        public UIPage Refresh()
        {
            Driver.Navigate().Refresh();

            return this;
        }

        /// <summary>
        /// Saves a screenshot to a file. Overwrites the already existing file and creates non-existent directories.
        /// </summary>
        /// <param name="fileName">The file name to save the screenshot to. May include relative or absolute path.</param>
        /// <param name="format">A format to save the image to.</param>
        /// <returns>An instance of this class.</returns>
        public UIPage SaveScreenshot(string fileName, ScreenshotImageFormat format = ScreenshotImageFormat.Png)
        {
            if (!Path.HasExtension(fileName)) { fileName = Path.ChangeExtension(fileName, format.ToString().ToLower()); }
            if (!Path.IsPathRooted(fileName)) { fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), fileName); }
            Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            Driver.GetScreenshot().SaveAsFile(fileName, format);

            return this;
        }

        /// <summary>
        /// Switches the current tab to another one specified by the enumerated position.
        /// </summary>
        /// <param name="tab">The position of the tab.</param>
        /// <returns>An instance of this class.</returns>
        public UIPage SwitchToTab(Index tab)
        {
            switch (tab)
            {
                case Index.Previous:
                    return SwitchToTab(--TabIndex);
                case Index.Next:
                    return SwitchToTab(++TabIndex);
                default:
                    return SwitchToTab((int)tab);
            }
        }

        /// <summary>
        /// Switches the current tab to another one specified by the index.
        /// </summary>
        /// <param name="index">The zero-based index of the tab.</param>
        /// <returns>An instance of this class.</returns>
        public UIPage SwitchToTab(int index)
        {
            index = index % Driver.WindowHandles.Count;
            if (index < 0) { index += Driver.WindowHandles.Count; }

            Driver.SwitchTo().Window(Driver.WindowHandles[index]);
            TabIndex = index;

            return this;
        }
    }
}
