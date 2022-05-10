using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SeleniumTestProject.Fixtures.V2
{
    public class CompiledJSTodoTestChrome : IClassFixture<ChromeDriverFixture>
    {
        private readonly ChromeDriverFixture _fixture;

        public CompiledJSTodoTestChrome(ChromeDriverFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [InlineData("Kotlin + React")]
        [InlineData("Spine")]
        [InlineData("Dart")]
        [InlineData("Closure")]
        [InlineData("Elm")]
        public void VerifyTodoListCreateSuccessfully(string technology)
        {
            _fixture.Driver.GoToUrl("https://todomvc.com/");
            SelectCategory("ctojs");
            OpenTechnologySample(technology);
            AddNewTodoItem(_fixture.TodoList[0]);
            AddNewTodoItem(_fixture.TodoList[1]);
            AddNewTodoItem(_fixture.TodoList[2]);
            GetItemCheckBox(_fixture.TodoList[0]).Click();
            AssertLeftItems(2);
        }

        private void SelectCategory(string category)
        {
            _fixture.Driver.WaitAndFindElement(By.XPath($"//paper-tab[@data-target='{category}']")).Click();
        }

        private void AssertLeftItems(int expectedCount)
        {
            var resultSpan = _fixture.Driver.WaitAndFindElement(By.XPath("//footer/span | //footer/div/span"));
            if (expectedCount == 0 || expectedCount > 1)
            {
                _fixture.Driver.ValidateInnerTextIs(resultSpan, $"{expectedCount} items left");
            }
            else
            {
                _fixture.Driver.ValidateInnerTextIs(resultSpan, $"{expectedCount} item left");
            }
        }

        private IWebElement GetItemCheckBox(string todoItem)
        {
            return _fixture.Driver.WaitAndFindElement(By.XPath($"//label[text()='{todoItem}']/preceding-sibling::input"));
        }

        private void AddNewTodoItem(string todoItem)
        {
            var todoInput = _fixture.Driver.WaitAndFindElement(By.XPath("//input[@placeholder='What needs to be done?']"));
            todoInput.SendKeys(todoItem);
            todoInput.SendKeys(Keys.Enter);
        }

        private void OpenTechnologySample(string name)
        {
            IWebElement techLink = _fixture.Driver.WaitAndFindElement(By.LinkText(name));
            techLink.Click();
        }
    }
}
