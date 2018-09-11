// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;

namespace Selenium.Tests
{
    [TestFixture]
    class Omada
    {
        protected RemoteWebDriver Driver { get; private set; }

        [SetUp]
        public void BeforeEach()
        {
            Driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
        }

        [TearDown]
        public void AfterEach()
        {
            Driver.Quit();
        }

        [Test]
        public void EndToEnd()
        {
            Driver.Navigate().GoToUrl("https://www.omada.net/");
        }
    }
}
