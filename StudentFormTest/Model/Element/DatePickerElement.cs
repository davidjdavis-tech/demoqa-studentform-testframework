using OpenQA.Selenium.Chrome;

namespace NTT_Assessment.Model.Element
{
    public class DatePickerElement : AbstractElement
    {
        public DatePickerElement(ChromeDriver chromeDriver, string name, string cssSelector) : base(chromeDriver, name, cssSelector)
        {
        }

        public override void SetValue(string newValue)
        {
            var element = GetElement();
            element.SendKeys(newValue);

            // This is a hack. Wanted to get the text in the control, the length of that text and backspace by that amount.
            for (var i = 0; i < newValue.Length; i++)
            {
                element.SendKeys(OpenQA.Selenium.Keys.Left);
            }

            for (var i = 0; i < 11; i++)
            {
                element.SendKeys(OpenQA.Selenium.Keys.Backspace);
            }

            var numberElement = _chromeDriver.FindElementByCssSelector("input#userNumber");
            numberElement.Click();
        }
    }
}
