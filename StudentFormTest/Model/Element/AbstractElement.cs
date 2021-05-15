using System;
using System.Collections.Generic;
using System.Text;
using NTT_Assessment.Model.Element;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NTT_Assessment.Model
{
    public abstract class AbstractElement : IElement
    {
        protected readonly ChromeDriver _chromeDriver;
        public string Name { get; }
        private string _cssSelector { get; }

        protected AbstractElement(ChromeDriver chromeDriver, string name, string cssSelector)
        {
            _chromeDriver = chromeDriver;
            Name = name;
            _cssSelector = cssSelector;
        }

        public virtual void SetValue(string newValue)
        {
            GetElement().SendKeys(newValue);
        }

        public virtual string GetValue()
        {
            return GetElement().GetAttribute("value");
        }

        public string GetBorderColour()
        {
            return GetElement().GetCssValue("border-color");
        }

        protected IWebElement GetElement()
        {
            return _chromeDriver.FindElementByCssSelector(_cssSelector);
        }

        public void Clear()
        {
            GetElement().Clear();
        }

        public void Click()
        {
            GetElement().Click();
        }

        public string GetCssSelector()
        {
            return _cssSelector;
        }
    }
}
