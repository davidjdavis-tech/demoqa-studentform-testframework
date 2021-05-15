using OpenQA.Selenium.Chrome;

namespace NTT_Assessment.Model.Form
{
    public abstract class AbstractForm
    {
        protected readonly ChromeDriver _chromeDriver;

        protected AbstractForm(ChromeDriver chromeDriver)
        {
            _chromeDriver = chromeDriver;
        }
    }
}
