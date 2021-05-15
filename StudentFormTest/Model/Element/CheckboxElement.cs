using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NTT_Assessment.Model.Element
{
    public class CheckboxElement : AbstractElement
    {
        public CheckboxElement(ChromeDriver chromeDriver, string name, string cssSelector) : base(chromeDriver, name, cssSelector)
        {
        }

        public override void SetValue(string newValue)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_chromeDriver;
            js.ExecuteScript("arguments[0].checked = true;", GetElement());
        }
    }
}
