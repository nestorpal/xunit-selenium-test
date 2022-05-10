using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace SeleniumTestProject.First
{
    public class DriverFixtureV1 : IDisposable
    {
        private const int WAIT_FOR_ELEMENT_TIMEOUT = 30;
        
        public WebDriverWait WebDriverWait { get; set; }
        public IWebDriver Driver { get; set; }
        public string[] TodoList { get; set; }

        public DriverFixtureV1()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            Driver = new ChromeDriver();
            WebDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT));

            TodoList =
                new string[]
                {
                    "Watch Kaguya Ep 5!",
                    "Run and push ups",
                    "Play Pokemon Unite with Espinas-kun"
                };
        }

        public void Dispose()
        {
            Driver.Quit();
        }
    }
}
