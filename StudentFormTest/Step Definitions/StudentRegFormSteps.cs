using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using NTT_Assessment.Helpers;
using NTT_Assessment.Model;
using NTT_Assessment.Model.Element;
using NTT_Assessment.Model.Enums;
using NTT_Assessment.Model.Form;
using NTT_Assessment.Repository;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace NTT_Assessment.Step_Definitions
{
    [Binding]
    public sealed class StudentRegFormSteps : IDisposable
    {
        #region Context Keys

        private const string UrlKey = "url";

        private const string ClosePageOnTestEnd = "closeontestend";

        private const string StudentInfoData = "sid";

        #endregion

        #region Private Fields

        private ChromeDriver _chromeDriver;
        private readonly ScenarioContext _scenarioContext;
        private const int LoadTimeoutSecs = 15;

        private readonly List<string> _mandatoryFields = new List<string>
            {"First Name", "Last Name", "Email", "Mobile Number"};

        #endregion

        #region Constructors/Deconstructors

        public StudentRegFormSteps()
        {
            _chromeDriver = new ChromeDriver();
        }

        public StudentRegFormSteps(ScenarioContext scenarioContext)
        {
            _chromeDriver = new ChromeDriver();
            _scenarioContext = scenarioContext;
        }

        public void Dispose()
        {
            if (_chromeDriver == null) return;
            var closePage = !_scenarioContext.ContainsKey(ClosePageOnTestEnd) ||
                            _scenarioContext.Get<bool>(ClosePageOnTestEnd);

            if (closePage)
            {
                _chromeDriver.Dispose();
                _chromeDriver = null;
            }
        }

        #endregion

        #region Step Definitions

        [Given(@"I have navigated to ""(.*)""")]
        public void GivenIHaveNavigatedTo(string url)
        {
            _scenarioContext.Add(UrlKey, url);
            _chromeDriver.Navigate().GoToUrl(url);

            // Tried to use this but didn't work. For time sake, using Thread.Sleep although this isn't recommended
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("section#text-10")));
            Thread.Sleep(2000);
        }

        [When(@"I enter student details as follows")]
        public void WhenIEnterStudentDetailsAsFollows(Table table)
        {
            CheckVerticalTable(table);
            var studentInfo = table.CreateInstance<StudentInfo>();

            AssertOrAssignStudentInfo(studentInfo, AssertOrAssign.Assign);
        }

        [Then(@"the student details on the form are as follows")]
        public void ThenTheStudentDetailsOnTheFormAreAsFollows(Table table)
        {
            var studentInfo = table.CreateInstance<StudentInfo>();
            AssertOrAssignStudentInfo(studentInfo, AssertOrAssign.Assert);
        }

        [Then(@"the current address field is as follows")]
        public void ThenTheCurrentAddressFieldIsAsFollows(Table table)
        {
            var element = _chromeDriver.FindElementById("currentAddress");
            var actual = element.GetAttribute("value");
            var addressList = table.ToStringList();
            var expectedAddress = string.Join(Environment.NewLine, addressList);

            Assert.AreEqual(expectedAddress, actual);
        }


        [Then(@"the popup appears")]
        public void ThenThePopupAppears()
        {
            Assert.IsTrue(new PopupForm(_chromeDriver).IsDisplayed(), "The popup dialog window has not been displayed");
        }

        [Then(@"the popup does not appear")]
        public void ThenThePopupDoesNotAppear()
        {
            Assert.IsFalse(new PopupForm(_chromeDriver).IsDisplayed());
        }

        [Then(@"the field border colours are as follows")]
        public void ThenTheFieldBorderColoursAreAsFollows(Table table)
        {
            CheckVerticalTable(table);
            var fieldColours = table.CreateInstance<StudentInfo>();

            AssertFormFieldBorderColoursAreEqual(fieldColours);
        }

        

        [When(@"I upload a picture from file ""(.*)""")]
        public void WhenIUploadAPictureFromFile(string filePath)
        {
            var element = _chromeDriver.FindElementById("uploadPicture");
            var path = Path.Join(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filePath);
            element.SendKeys(path);
        }

        [When(@"I load student test data")]
        public void WhenILoadStudentTestData()
        {
            var studentInfo = new CsvDataRepository().GetStudentInfo();
            _scenarioContext.Add(StudentInfoData, studentInfo);
        }

        [When(@"I fill in the screen with test data")]
        public void WhenIFillInTheScreenWithTestData()
        {
            if (!_scenarioContext.ContainsKey(StudentInfoData))
                Assert.Fail("Attempt to run step without prior load of student data");

            AssertOrAssignStudentInfo(_scenarioContext.Get<StudentInfo>(StudentInfoData), AssertOrAssign.Assign);
        }

        [When(@"I enter a current address as follows")]
        public void WhenIEnterACurrentAddressAsFollows(Table table)
        {
            var addressList = table.ToStringList();
            var address = string.Join(Environment.NewLine, addressList);

            var element = _chromeDriver.FindElementById("currentAddress");
            element.SendKeys(address);
        }

        [Then(@"the popup screen table is as follows")]
        public void ThenThePopupScreenTableIsAsFollows(Table table)
        {
            ThenThePopupAppears();
            var expected = table.CreateInstance<PopupStudentInfo>();
            var actual = new PopupForm(_chromeDriver).WebTableToStudentInfo();

            if (expected.StudentName != null) Assert.AreEqual(expected.StudentName, actual.StudentName);
            if (expected.StudentEmail != null) Assert.AreEqual(expected.StudentEmail, actual.StudentEmail);
            if (expected.Gender != null) Assert.AreEqual(expected.Gender, actual.Gender);
            if (expected.Mobile != null) Assert.AreEqual(expected.Mobile, actual.Mobile);
            if (expected.DateOfBirth != null) Assert.AreEqual(expected.DateOfBirth, actual.DateOfBirth);
            if (expected.Subjects != null) Assert.AreEqual(expected.Subjects, actual.Subjects);
            if (expected.Hobbies != null) Assert.AreEqual(expected.Hobbies, actual.Hobbies);
            if (expected.Picture != null) Assert.AreEqual(expected.Picture, actual.Picture);
            if (expected.Address != null) Assert.AreEqual(expected.Address, actual.Address);
            if (expected.StateAndCity != null) Assert.AreEqual(expected.StateAndCity, actual.StateAndCity);
        }

        [When(@"I press the submit button")]
        public void WhenIPressTheSubmitButton()
        {
            var errorMsg = "Could not find the Submit button using CSS Selector 'button#submit'";
            try
            {
                var submitButton = _chromeDriver.FindElementByCssSelector("button#submit");
                Assert.IsNotNull(submitButton, errorMsg);
                ScrollToElement(submitButton);
                submitButton.Click();
            }
            catch (Exception e)
            {
                Assert.Fail($"Couldn't click button:\n'{e.Message}'");
            }
        }

        [When(@"I leave the page open")]
        [Then(@"I leave the page open")]
        public void WhenILeaveThePageOpen()
        {
            _scenarioContext.Add(ClosePageOnTestEnd, false);
        }

        [Then(@"the popup title is ""(.*)""")]
        public void ThenThePopupTitleIs(string modalPageTitle)
        {
            ThenThePopupAppears();
            Assert.AreEqual(modalPageTitle, new PopupForm(_chromeDriver).GetTitleText());
        }

        [When(@"I close the page")]
        public void WhenICloseThePage()
        {
            _scenarioContext.Add(ClosePageOnTestEnd, true);
        }

        #endregion

        #region Private Methods

        private void AssertOrAssignStudentInfo(StudentInfo info, AssertOrAssign mode)
        {
            var form = new StudentInfoForm(_chromeDriver);
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(_chromeDriver, new TimeSpan(0, 0, LoadTimeoutSecs));

            if (info.FirstName != null)
            {
                if (mode == AssertOrAssign.Assign)
                    form.FirstNameField.SetValue(info.FirstName);
                else
                    Assert.AreEqual(info.FirstName, form.FirstNameField.GetValue());
            }

            if (info.LastName != null)
            {
                if (mode == AssertOrAssign.Assign)
                    form.LastNameField.SetValue(info.LastName);
                else
                    Assert.AreEqual(info.LastName, form.LastNameField.GetValue());
            }

            if (info.Email != null)
            {
                if (mode == AssertOrAssign.Assign)
                    form.EmailField.SetValue(info.Email);
                else
                    Assert.AreEqual(info.Email, form.EmailField.GetValue());
            }

            if (info.Gender != null)
            {
                switch (info.Gender)
                {
                    case "Male":
                        if (mode == AssertOrAssign.Assign)
                            form.MaleGenderField.SetValue("1");
                        else
                            Assert.IsTrue(((RadioButtonElement)form.MaleGenderField).IsSelected());

                        break;

                    case "Female":
                        if (mode == AssertOrAssign.Assign)
                            form.FemaleGenderField.SetValue("1");
                        else
                            Assert.IsTrue(((RadioButtonElement)form.FemaleGenderField).IsSelected());

                        break;

                    case "Other":
                        if (mode == AssertOrAssign.Assign)
                            form.OtherGenderField.SetValue("1");
                        else
                            Assert.IsTrue(((RadioButtonElement)form.OtherGenderField).IsSelected());

                        break;

                    default:
                        Assert.Fail($"Invalid Gender '{info.Gender}' supplied. Must be 'Male', 'Female' or 'Other'");
                        break;
                }
            }

            if (info.MobileNumber != null)
            {
                if (mode == AssertOrAssign.Assign)
                {
                    form.MobileNumberField.Clear();
                    form.MobileNumberField.SetValue(info.MobileNumber);
                }
                else
                {
                    Assert.AreEqual(info.MobileNumber, form.MobileNumberField.GetValue());
                }
            }

            if (info.DateOfBirth != null)
            {
                if (mode == AssertOrAssign.Assign)
                {
                    form.DateOfBirthField.SetValue(info.DateOfBirth);

                    // hack to close the date picker
                    form.MobileNumberField.Click();
                }
                else
                {
                    Assert.AreEqual(info.DateOfBirth, form.DateOfBirthField.GetValue());
                }
            }

            if (!string.IsNullOrEmpty(info.Subjects)) 
            {
                form.SubjectsField.SetValue(info.Subjects); 
            }

            if (!string.IsNullOrEmpty(info.Hobbies))
            {
                var hobbies = info.Hobbies.Split(",").Distinct().ToList();

                if (hobbies.Contains("Sports")) form.SportsField.SetValue("1");

                if (hobbies.Contains("Reading")) form.ReadingField.SetValue("1");

                if (hobbies.Contains("Music")) form.MusicField.SetValue("1");
            }

            if (!string.IsNullOrEmpty(info.State)) form.StateField.SetValue(info.State);

            if (!string.IsNullOrEmpty(info.City)) form.CityField.SetValue(info.City);
        }

        private void AssertFormFieldBorderColoursAreEqual(StudentInfo fieldColours)
        {
            if (fieldColours.FirstName != null)
            {
                var element = _chromeDriver.FindElementByCssSelector("input#firstName");
                CheckBorderColour(fieldColours.FirstName, element);
            }

            if (fieldColours.LastName != null)
            {
                var element = _chromeDriver.FindElementByCssSelector("input#lastName");
                CheckBorderColour(fieldColours.LastName, element);
            }

            if (fieldColours.Email != null)
            {
                var element = _chromeDriver.FindElementByCssSelector("input#userEmail");
                CheckBorderColour(fieldColours.Email, element);
            }

            if (fieldColours.Gender != null)
            {
                var maleRadioButton =
                    _chromeDriver.FindElementByXPath("//*[@id=\"genterWrapper\"]/div[2]/div[1]/label");
                CheckBorderColour(fieldColours.Gender, maleRadioButton);
            }

            if (fieldColours.MobileNumber != null)
            {
                var element = _chromeDriver.FindElementByCssSelector("input#userNumber");
                CheckBorderColour(fieldColours.MobileNumber, element);
            }


            if (fieldColours.DateOfBirth != null)
            {
                var element = _chromeDriver.FindElementByCssSelector("input#dateOfBirthInput");
                CheckBorderColour(fieldColours.DateOfBirth, element);
            }

            if (!string.IsNullOrEmpty(fieldColours.Hobbies))
            {
                var sportsCheckbox =
                    _chromeDriver.FindElementByXPath("//*[@id=\"hobbiesWrapper\"]/div[2]/div[1]/label");
                CheckBorderColour(fieldColours.DateOfBirth, sportsCheckbox);
            }

            if (fieldColours.Address != null)
            {
                var element = _chromeDriver.FindElementById("currentAddress");
                CheckBorderColour(fieldColours.DateOfBirth, element);
            }
        }

        private static void CheckBorderColour(string colour, IWebElement element)
        {
            var borderColour = element.GetCssValue("border-color");
            Assert.AreEqual(StudentInfoForm.ColourDictionary[colour], borderColour);
        }
        private void CheckVerticalTable(Table table)
        {
            var headers = table.Rows.Select(r => r[0]).ToList();
            CheckTableHeaders(headers);
        }

        private void CheckHorizontalTable(Table table)
        {
            CheckTableHeaders(table.Header);
        }

        private void CheckTableHeaders(ICollection<string> headers)
        {
            var errors = new List<string>();

            foreach (var mandatoryField in _mandatoryFields)
                if (!headers.Contains(mandatoryField))
                    errors.Add($"Table is missing mandatory field '{mandatoryField}'");

            if (errors.Any()) Assert.Fail(string.Join(Environment.NewLine, errors));
        }

        private void ScrollToElement(IWebElement element)
        {
            IJavaScriptExecutor js = _chromeDriver;
            js.ExecuteScript("arguments[0].scrollIntoView();", element);
        }

        #endregion
    }
}