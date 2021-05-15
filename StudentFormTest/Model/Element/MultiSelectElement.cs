using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NTT_Assessment.Model.Element
{
    public class MultiSelectElement : AbstractElement
    {
        public MultiSelectElement(ChromeDriver chromeDriver, string name, string cssSelector) : base(chromeDriver, name, cssSelector)
        {
        }

        public override void SetValue(string newValue)
        {
            var items = newValue.Split(",");
            var element = GetElement();

            // type and select each subject in turn
            foreach (var item in items)
            {
                element.SendKeys(item);
                ICollection<IWebElement> elements = new List<IWebElement>();

                try
                {
                    elements = _chromeDriver.FindElementsByClassName(
                        "subjects-auto-complete__menu-list");
                }
                catch (Exception e)
                {
                    Assert.Fail($"No Subjects with the name '{item}'");
                }

                if (elements.Any()) elements.First().Click();
                element.SendKeys(Keys.Enter);
            }
        }
    }
}
