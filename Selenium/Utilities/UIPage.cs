// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
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

        public UIPage(UIMap map, RemoteWebDriver driver) : base(map, driver) => By = By.TagName("html");

        // Properties

        public IWebElement Element => HtmlElement;

        // Methods

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
    }
}
