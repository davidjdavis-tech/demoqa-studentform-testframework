using System;
using OpenQA.Selenium.Chrome;

namespace NTT_Assessment.Model.Form
{
    public class PopupForm : AbstractForm
    {
        public PopupForm(ChromeDriver chromeDriver) : base(chromeDriver) {}

        public PopupStudentInfo WebTableToStudentInfo()
        {
            var info = new PopupStudentInfo
            {
                StudentName = _chromeDriver
                    .FindElementByXPath("/html/body/div[3]/div/div/div[2]/div/table/tbody/tr[1]/td[2]").Text,
                StudentEmail = _chromeDriver
                    .FindElementByXPath("/html/body/div[3]/div/div/div[2]/div/table/tbody/tr[2]/td[2]").Text,
                Gender = _chromeDriver
                    .FindElementByXPath("/html/body/div[3]/div/div/div[2]/div/table/tbody/tr[3]/td[2]").Text,
                Mobile = _chromeDriver
                    .FindElementByXPath("/html/body/div[3]/div/div/div[2]/div/table/tbody/tr[4]/td[2]").Text,
                DateOfBirth = _chromeDriver
                    .FindElementByXPath("/html/body/div[3]/div/div/div[2]/div/table/tbody/tr[5]/td[2]").Text,
                Subjects = _chromeDriver
                    .FindElementByXPath("/html/body/div[3]/div/div/div[2]/div/table/tbody/tr[6]/td[2]").Text,
                Hobbies = _chromeDriver
                    .FindElementByXPath("/html/body/div[3]/div/div/div[2]/div/table/tbody/tr[7]/td[2]").Text,
                Picture = _chromeDriver
                    .FindElementByXPath("/html/body/div[3]/div/div/div[2]/div/table/tbody/tr[8]/td[2]").Text,
                Address = _chromeDriver
                    .FindElementByXPath("/html/body/div[3]/div/div/div[2]/div/table/tbody/tr[9]/td[2]").Text,
                StateAndCity = _chromeDriver
                    .FindElementByXPath("/html/body/div[3]/div/div/div[2]/div/table/tbody/tr[10]/td[2]").Text
            };

            return info;
        }

        public bool IsDisplayed()
        {
            try
            {
                var modalPopupElement = _chromeDriver.FindElementByClassName("modal-header");
                return modalPopupElement.Displayed;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string GetTitleText()
        {
            return _chromeDriver.FindElementById("example-modal-sizes-title-lg").Text;
        }
        
    }
}
