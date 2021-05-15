using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NTT_Assessment.Model.Element
{
    public class RadioButtonElement : AbstractElement
    {
        private List<string> _true = new List<string> {"true", "TRUE", "True", "Yes", "YES", "1"};
        private List<string> _false = new List<string> {"false", "FALSE", "False", "No", "NO", "0"};

        public RadioButtonElement(ChromeDriver chromeDriver, string name, string cssSelector) : base(chromeDriver, name, cssSelector)
        {
        }

        public override void SetValue(string newValue)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_chromeDriver;
            if (_true.Contains(newValue)) js.ExecuteScript("arguments[0].checked = true;", GetElement());
            if (_false.Contains(newValue)) js.ExecuteScript("arguments[0].checked = false;", GetElement());
        }

        public bool IsSelected()
        {
            return GetElement().Selected;
        }
    }
}
