using OpenQA.Selenium.Chrome;

namespace NTT_Assessment.Model.Element
{
    public class Element : AbstractElement
    {
        public Element(ChromeDriver chromeDriver, string name, string cssSelector) : base(chromeDriver, name, cssSelector)
        {
        }
    }
}
