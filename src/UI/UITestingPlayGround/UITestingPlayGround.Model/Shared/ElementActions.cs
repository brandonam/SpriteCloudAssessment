using OpenQA.Selenium;

namespace UITestingPlayGround.Model.Shared
{
    /// <summary>
    /// Reusable Element Actions
    /// </summary>
    public class ElementActions
    {
        private readonly IWebDriver _driver;

        public ElementActions(IWebDriver chromeDriver)
        {
            _driver = chromeDriver;
        }

        public IWebElement GetElement(By element)
        {
            return _driver.FindElement(element);
        }

        public IReadOnlyCollection<IWebElement> GetElementCollection(By element)
        {
            return _driver.FindElements(element);
        }


        /// <summary>
        /// Search Shadow DOM Elements.
        /// </summary>
        /// <param name="parentElement"></param>
        /// <param name="cssSelector">The type of css elements to gather.</param>
        /// <param name="attributeName">Use this attribute name to search the shadow dom elements.</param>
        /// <param name="attributeValue">Use this value to match to the attribute name used.</param>
        /// <returns></returns>
        public IWebElement? GetShadowDOMElement(By parentElement, string cssSelector, string? attributeName = null, string? attributeValue = null)
        {
            var webElement = GetElement(parentElement);
            var shadowElement = webElement.GetShadowRoot();
            var shadowElementCollection = shadowElement.FindElements(By.CssSelector(cssSelector));

            if(!shadowElementCollection.Any())
            {
                return null;
            }

            if(shadowElementCollection.Count == 1)
            {
               return shadowElementCollection.FirstOrDefault();
            }

            foreach (var button in shadowElementCollection)
            {
                if(!string.IsNullOrEmpty(attributeName) && !string.IsNullOrEmpty(attributeValue))
                {
                    var buttonAttributeValue = button.GetAttribute(attributeName);
                    if (buttonAttributeValue == attributeValue)
                    {
                        return button;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Identify if the element is visibile.
        /// </summary>
        /// <param name="element">The element to be checked.</param>
        /// <param name="byStyle"><c>false</c> by default to use seleniums definition, <c>true</c> to only check the style attribute.</param>
        /// <returns></returns>
        public bool ElementIsVisible(By element, bool byStyle = false)
        {
            const string styleVisibility = "display: none;";
            try
            {
                var inputElement = GetElement(element);
                if (inputElement != null)
                {
                    if(!byStyle)
                    {
                        return inputElement.Displayed;
                    }

                    var styleAttribute = inputElement.GetAttribute("style");
                    if(styleAttribute.Contains(styleVisibility, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
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

        public bool ButtonClick(IWebElement? element)
        {
            try
            {
                if (element != null)
                {
                    element.Click();
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
