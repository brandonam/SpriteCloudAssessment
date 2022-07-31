using OpenQA.Selenium;

namespace UITestingPlayGround.Model.Pages;

public static class DynamicIdButton
{
    public static readonly By DynamicIdButtonElement = By.XPath("//button[@class='btn btn-primary']");
}