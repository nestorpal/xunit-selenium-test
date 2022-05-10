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

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, MaxParallelThreads = 4)]
namespace SeleniumTestProject.Fixtures.V1
{
    public class TodoTest : IClassFixture<DriverFixture>
    {
        private readonly DriverFixture _fixture;
        private readonly Actions _actions;

        public TodoTest(DriverFixture fixture)
        {
            _fixture = fixture;
            _actions = new Actions(_fixture.Driver);
        }

        [Theory]
        [InlineData("Backbone.js")]
        [InlineData("AngularJS")]
        [InlineData("Ember.js")]
        [InlineData("KnockoutJS")]
        [InlineData("Dojo")]
        [InlineData("Knockback.js")]
        [InlineData("CanJS")]
        [InlineData("Polymer")]
        [InlineData("React")]
        [InlineData("Mithril")]
        [InlineData("Vue.js")]
        [InlineData("Marionette.js")]
        public void VerifyTodoListCreateSuccessfully(string technology)
        {
            _fixture.Driver.Navigate().GoToUrl("https://todomvc.com/");
            SelectCategory("js");
            OpenTechnologySample(technology);
            AddNewTodoItem(_fixture.TodoList[0]);
            AddNewTodoItem(_fixture.TodoList[1]);
            AddNewTodoItem(_fixture.TodoList[2]);
            GetItemCheckBox(_fixture.TodoList[1]).Click();
            AssertLeftItems(2);
        }

        private void SelectCategory(string category)
        {
            WaitAndFindElement(By.XPath($"//paper-tab[@data-target='{category}']")).Click();
        }

        private void AssertLeftItems(int expectedCount)
        {
            var resultSpan = WaitAndFindElement(By.XPath("//footer/span | //footer/div/span"));
            if (expectedCount == 0 || expectedCount > 1)
            {
                ValidateInnerTextIs(resultSpan, $"{expectedCount} items left");
            }
            else
            {
                ValidateInnerTextIs(resultSpan, $"{expectedCount} item left");
            }
        }

        private void ValidateInnerTextIs(IWebElement resultSpan, string expectedText)
        {
            _fixture.WebDriverWait.Until(
                ExpectedConditions.TextToBePresentInElement(resultSpan, expectedText)
            );
        }

        private IWebElement GetItemCheckBox(string todoItem)
        {
            return WaitAndFindElement(By.XPath($"//label[text()='{todoItem}']/preceding-sibling::input"));
        }

        private void AddNewTodoItem(string todoItem)
        {
            var todoInput = WaitAndFindElement(By.XPath("//input[@placeholder='What needs to be done?']"));
            todoInput.SendKeys(todoItem);
            _actions.MoveToElement(todoInput).SendKeys(Keys.Enter).Perform();
        }

        private void OpenTechnologySample(string name)
        {
            IWebElement techLink = WaitAndFindElement(By.LinkText(name));
            techLink.Click();
        }

        private IWebElement WaitAndFindElement(By locator)
        {
            return _fixture.WebDriverWait.Until(ExpectedConditions.ElementExists(locator));
        }
    }
}
