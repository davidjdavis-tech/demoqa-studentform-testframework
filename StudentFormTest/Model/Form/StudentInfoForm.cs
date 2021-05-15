using System.Collections.Generic;
using NTT_Assessment.Model.Element;
using OpenQA.Selenium.Chrome;

namespace NTT_Assessment.Model.Form
{
    public class StudentInfoForm : AbstractForm
    {
        public static readonly Dictionary<string, string> ColourDictionary = new Dictionary<string, string>
        {
            // I'm aware these colour may not be the same on all machines
            {"Red", "rgb(220, 53, 69)"},
            {"Grey", "rgb(204, 204, 204)"},
            {"Green", "rgb(40, 167, 69)"}
        };

        public IElement FirstNameField => new Element.Element(_chromeDriver, "FirstName", "input#firstName");
        public IElement LastNameField => new Element.Element(_chromeDriver, "LastName", "input#lastName");
        public IElement EmailField => new Element.Element(_chromeDriver, "Email", "input#userEmail");
        public IElement MobileNumberField => new Element.Element(_chromeDriver, "MobileNumber", "input#userNumber");
        public IElement MaleGenderField => new RadioButtonElement(_chromeDriver,"Male", "input#gender-radio-1");
        public IElement FemaleGenderField => new Element.Element(_chromeDriver, "Female", "input#gender-radio-2");
        public IElement OtherGenderField => new Element.Element(_chromeDriver, "Other", "input#gender-radio-3");
        public IElement DateOfBirthField => new DatePickerElement(_chromeDriver, "DateOfBirth", "input#dateOfBirthInput");
        public IElement SubjectsField => new AutoCompleteElement(_chromeDriver, "Subjects", "input#subjectsInput", "subjects-auto-complete__menu-list");
        public IElement SportsField => new CheckboxElement(_chromeDriver, "Sports", "input#hobbies-checkbox-1");
        public IElement ReadingField => new CheckboxElement(_chromeDriver, "Reading", "input#hobbies-checkbox-2");
        public IElement MusicField => new CheckboxElement(_chromeDriver, "Music", "input#hobbies-checkbox-3");
        public IElement StateField => new MultiSelectElement(_chromeDriver, "State", "input#react-select-3-input");
        public IElement CityField => new MultiSelectElement(_chromeDriver, "City", "input#react-select-4-input");

        public StudentInfoForm(ChromeDriver chromeDriver) : base(chromeDriver) {}
    }
}
