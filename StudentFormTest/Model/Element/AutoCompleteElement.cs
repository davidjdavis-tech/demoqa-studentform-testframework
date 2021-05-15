using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NTT_Assessment.Model.Element
{
    public class AutoCompleteElement : AbstractElement
    {
        private readonly string _listClassName;

        public AutoCompleteElement(ChromeDriver chromeDriver, string name, string cssSelector, string listClassName) : base(chromeDriver, name, cssSelector)
        {
            _listClassName = listClassName;
        }

        public override void SetValue(string newValue)
        {
            var items = newValue.Split(",");
            var element = GetElement();

            foreach (var item in items)
            {
                element.SendKeys(item);
                ICollection<IWebElement> listElements = new List<IWebElement>();

                try
                {
                    listElements = _chromeDriver.FindElementsByClassName(
                        _listClassName);
                }
                catch (Exception e)
                {
                    Assert.Fail($"No Subjects with the name '{item}'");
                }

                if (listElements.Any()) listElements.First().Click();
            }
        }
    }
}
