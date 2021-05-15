namespace NTT_Assessment.Model.Element
{
    public interface IElement
    {
        void SetValue(string newValue);

        string GetValue();

        string GetBorderColour();

        void Clear();

        void Click();

        string GetCssSelector();
    }
}
