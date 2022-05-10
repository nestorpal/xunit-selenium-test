using System;
using System.Linq;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumTestProject
{
    public class AttributeSeleniumTest : IDisposable
    {
        private readonly IWebDriver _driver;

        public AttributeSeleniumTest()
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
        [Trait("Category", "C1")]
        [UseCulture("es-PE")]
        public void ProperCheckboxSelected_When_AddNewItem()
        {
            _driver.Navigate().GoToUrl("https://lambdatest.github.io/sample-todo-app/");

            IWebElement todoInput = _driver.FindElement(By.Id("sampletodotext"));
            DateTime birthDay = new DateTime(1992, 10, 3);
            todoInput.SendKeys(birthDay.ToString("d"));

            var addButton = _driver.FindElement(By.Id("addbutton"));
            addButton.Click();

            var todoCheckBoxes = _driver.FindElements(By.XPath("//li[@ng-repeat]/input"));
            todoCheckBoxes.Last().Click();

            var todoInfos = _driver.FindElements(By.XPath("//li[@ng-repeat]/span"));

            Assert.Equal("3/10/1992", todoInfos.Last().Text);
        }

        //[Fact]
        [Trait("Category", "C2")]
        [RetryFact(MaxRetries = 5)]
        public void TraitTest_When_AddNewItem()
        {
            string testString = "Trait Test";

            _driver.Navigate().GoToUrl("https://lambdatest.github.io/sample-todo-app/");

            IWebElement todoInput = _driver.FindElement(By.Id("sampletodotext"));
            todoInput.SendKeys(testString);

            var addButton = _driver.FindElement(By.Id("addbutton"));
            addButton.Click();

            var todoCheckBoxes = _driver.FindElements(By.XPath("//li[@ng-repeat]/input"));
            todoCheckBoxes.Last().Click();

            var todoInfos = _driver.FindElements(By.XPath("//li[@ng-repeat]/span"));

            Assert.Equal(testString, todoInfos.Last().Text);

            Console.WriteLine($"Test finished at {DateTime.Now.ToString("ffff")}");
        }

        [SkippableFact(typeof(DriverServiceNotFoundException))]
        public void FailTest()
        {
            
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
