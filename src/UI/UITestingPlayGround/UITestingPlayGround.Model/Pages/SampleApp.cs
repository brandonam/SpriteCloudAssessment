using OpenQA.Selenium;

namespace UITestingPlayGround.Model.Pages
{
    public static class SampleApp
    {
        public static readonly By UsernameTextInputElement = By.Name("UserName");
        public static readonly By PasswordTextInputElement = By.Name("Password");
        public static readonly By LoginButtonElement = By.Id("login");
        public static readonly By LoginStatusElement = By.XPath("//label[@id='loginstatus']");

    }
}
