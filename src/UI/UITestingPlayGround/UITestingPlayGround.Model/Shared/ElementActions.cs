using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace UITestingPlayGround.Model.Shared
{
    /// <summary>
    /// Reusable Element Actions
    /// </summary>
    public class ElementActions
    {
        private readonly ChromeDriver _chromeDriver;

        public ElementActions(ChromeDriver chromeDriver)
        {
            _chromeDriver = chromeDriver;
        }

        public IWebElement GetElement(By element)
        {
            return _chromeDriver.FindElement(element);
        }

        public bool SetTextInput(By element, string text)
        {
            try
            {
                var inputElement = GetElement(element);
                if (inputElement != null)
                {
                    inputElement.SendKeys(text);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ClearTextInput(By element)
        {
            try
            {
                var inputElement = GetElement(element);
                if (inputElement != null)
                {
                    inputElement.Clear();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ButtonClick(By element)
        {
            try
            {
                var buttonElement = GetElement(element);
                if (buttonElement != null)
                {
                    buttonElement.Click();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string? GetElementText(By element)
        {
            try
            {
                var selectedElement = GetElement(element);
                if (selectedElement != null)
                {
                    var currentText = selectedElement.Text;
                    return currentText;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
