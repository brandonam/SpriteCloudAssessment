using OpenQA.Selenium;

namespace UITestingPlayGround.Model.Pages;

public static class TextInput
{
    public static readonly By TextInputElement = By.XPath("//input[@id='newButtonName']");
    public static readonly By UpdateButtonElement = By.XPath("//button[@id='updatingButton']");
}