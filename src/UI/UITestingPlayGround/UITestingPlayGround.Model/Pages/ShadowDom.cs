using OpenQA.Selenium;

namespace UITestingPlayGround.Model.Pages;

public static class ShadowDOM
{
    public static readonly By ShadowDOMParent = By.XPath("//guid-generator");

    public const string GuidInputElemenId = "editField";
    public const string GuidGenerateButtonElementId = "buttonGenerate";
    public const string CopyGuidButtonElementId = "buttonCopy";
}