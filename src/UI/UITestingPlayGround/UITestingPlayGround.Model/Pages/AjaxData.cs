using OpenQA.Selenium;

namespace UITestingPlayGround.Model.Pages;

public static class AjaxData
{
    public static readonly By AjaxDataButtonElement = By.XPath("//button[@id='ajaxButton']");
    public static readonly By LoadingDaisyInitializedInvisibleElement = By.XPath("//i[contains(@style,'display:none')]");
    public static readonly By LoadingDaisyInvisibleElement = By.XPath("//i[contains(@style,'display: none;')]");
    public static readonly By LoadingDaisyVisibleElement = By.XPath("//i[contains(@style,'')]");
    public static readonly By AjaxDataRecievedElement = By.XPath("//p[@class='bg-success']");
}