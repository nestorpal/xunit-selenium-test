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

namespace SeleniumTestProject.Fixtures.V2
{
    public abstract class DriverFixture : IDisposable
    {
        private const int WAIT_FOR_ELEMENT_TIMEOOUT = 30;

        public DriverAdapter Driver { get; set; }
        public virtual int WaitForElementTimeout { get; set; } = WAIT_FOR_ELEMENT_TIMEOOUT;
        public string[] TodoList { get; set; }

        protected abstract void InitializeDriver();

        public DriverFixture()
        {
            Driver = new DriverAdapter();
            InitializeDriver();

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
            Driver.Dispose();
        }
    }
}
