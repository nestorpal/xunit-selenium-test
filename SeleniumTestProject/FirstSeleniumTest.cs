using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumTestProject
{
    public class FirstSeleniumTest : IDisposable
    {
        private readonly IWebDriver _driver;

        public FirstSeleniumTest()
        {
            new DriverManager().SetUpDriver(
                new ChromeConfig(),
                VersionResolveStrategy.MatchingBrowser
            );

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless");

            _driver = new ChromeDriver(chromeOptions);
            _driver.Manage().Window.Maximize();
        }

        [Fact]
        public void CorrectTitleDisplayed_When_NavigateHomePage()
        {
            _driver.Navigate().GoToUrl("https://lambdatest.github.io/sample-todo-app/");

            Assert.Equal("Sample page - lambdatest.com", _driver.Title);
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
