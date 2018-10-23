// Copyright © Adrian Nowacki. All rights reserved.
// Licensed under the MIT License.

using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using Selenium.Utilities;
using System;
using System.IO;
using System.Reflection;

namespace Selenium.Tests
{
    /// <summary>
    /// Provides a base class for tests, which includes generic setup and teardown.
    /// </summary>
    [TestFixture]
    class TestBase
    {
        // Constructors

        public TestBase(string typeName) => TypeName = typeName;

        // Properties

        protected RemoteWebDriver Driver { get; private set; }

        protected string ExecutingPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        protected string TypeName { get; }

        // Setup and Teardown

        /// <summary>
        /// Runs the specified code before each test.
        /// </summary>
        [SetUp]
        protected void BeforeEach()
        {
            switch (TypeName)
            {
                case nameof(ChromeDriver):
                    Driver = new ChromeDriver(ExecutingPath);
                    break;
                case nameof(InternetExplorerDriver):
                    Driver = new InternetExplorerDriver(ExecutingPath, new InternetExplorerOptions() { EnsureCleanSession = true, RequireWindowFocus = true });
                    break;
            }

            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);

            UIBase.Initialize(Driver);
        }

        /// <summary>
        /// Runs the specified code after each test.
        /// </summary>
        [TearDown]
        protected void AfterEach()
        {
            Driver.Quit();
        }
    }
}
