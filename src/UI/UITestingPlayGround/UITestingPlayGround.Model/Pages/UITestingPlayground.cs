
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace UITestingPlayGround.Model.Pages;

public class DynamicIdButton
{
    private readonly ChromeDriver _chromeDriver;

    public DynamicIdButton(ChromeDriver chromeDriver)
    {
        _chromeDriver = chromeDriver;
    }

    public readonly By DynamicButton = By.XPath("//button[@class='btn btn-primary']");

    public IWebElement GetDynamicIdButtonElement()
    {
        return _chromeDriver.FindElement(DynamicButton);
    }

    public bool DynamicIdButtonClicked() {
        try
        {
            var buttonElement = GetDynamicIdButtonElement();
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

    public string GetDynamicIdButtonAttribute(string attribute)
    {
        return _chromeDriver.FindElement(DynamicButton).GetAttribute(attribute);
    }

}