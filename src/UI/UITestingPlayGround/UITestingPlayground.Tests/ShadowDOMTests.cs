using FluentAssertions;
using Flurl.Http;
using Flurl.Http.Testing;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading.Tasks;
using UITestingPlayground.Tests.Helper;
using UITestingPlayGround.Model.Pages;
using UITestingPlayGround.Model.Shared;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium.Remote;

namespace UITestingPlayground.Tests;

[TestCaseOrderer("UITestingPlayground.Tests.Helper.PriorityOrderer", "UITestingPlayground.Tests")]
public class ShadowDOMTests : IDisposable
{
    private readonly ITestOutputHelper _testOutputHelper;
    private IWebDriver _webDriver;
    private const string RequestUrl = "https://www.uitestingplayground.com/shadowdom";

    // Setup
    public ShadowDOMTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("start-maximized"); // open Browser in maximized mode
        options.AddArgument("disable-infobars"); // disabling infobars
        options.AddArgument("--disable-extensions"); // disabling extensions
        options.AddArgument("--disable-gpu"); // applicable to windows os only
        options.AddArgument("--disable-dev-shm-usage"); // overcome limited resource problems
        options.AddArgument("--no-sandbox"); // Bypass OS security model
        options.AddArgument("--headless"); // Bypass OS security model
        options.AddArgument("--ignore-certificate-errors"); // Bypass missing/invalid SSL Certificates

        new DriverManager().SetUpDriver(new ChromeConfig());
        _webDriver = new ChromeDriver(options);
    }

    // Teardown
    public void Dispose()
    {
        _webDriver.Quit();
        _webDriver.Dispose();
    }

    [Fact, TestPriority(1)]
    public void Given_Website_When_Navigated_Should_Return_Success_Response()
    {
        // Arrange
        using var httpFlurlTest = new HttpTest();

        // Act
        var result = new Uri(RequestUrl).GetAsync();

        // Assert
        httpFlurlTest.ShouldHaveCalled(RequestUrl);
        httpFlurlTest.RespondWith(It.IsAny<string>(), 304);
    }

    [Fact, TestPriority(2)]
    public void Given_guid_generate_button_exists_Should_not_return_null()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        var element = elementActions.GetShadowDOMElement(ShadowDOM.ShadowDOMParent, "button", "Id", ShadowDOM.GuidGenerateButtonElementId);

        // Assert
        Assert.NotNull(element);
    }

    [Fact, TestPriority(2)]
    public void Given_guid_copy_button_exists_Should_not_return_null()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        var element = elementActions.GetShadowDOMElement(ShadowDOM.ShadowDOMParent, "button", "Id", ShadowDOM.CopyGuidButtonElementId);

        // Assert
        Assert.NotNull(element);
    }

    [Fact, TestPriority(2)]
    public void Given_guid_input_field_exists_Should_not_return_null()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        var element = elementActions.GetShadowDOMElement(ShadowDOM.ShadowDOMParent, "input", "Id", ShadowDOM.GuidInputElemenId);

        // Assert
        Assert.NotNull(element);
    }

    [Fact, TestPriority(3)]
    public void Given_guid_input_field_exists_When_guid_generate_button_clicked_Should_not_be_empty()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        var generateGuidButtonElement = elementActions.GetShadowDOMElement(ShadowDOM.ShadowDOMParent, "button", "Id", ShadowDOM.GuidGenerateButtonElementId);
        var guidButtonClicked = elementActions.ButtonClick(generateGuidButtonElement);
        var inputFieldElement = elementActions.GetShadowDOMElement(ShadowDOM.ShadowDOMParent, "input", "Id", ShadowDOM.GuidInputElemenId);
        var inputText = inputFieldElement?.GetAttribute("value");

        // Assert
        inputText.Should().NotBeNullOrEmpty();
    }

    [Fact, TestPriority(3)]
    public async void Given_guid_input_field_exists_When_guid_generated_and_copy_button_clicked_Should_be_equal_clipboard_and_input_field()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        var generateGuidButtonElement = elementActions.GetShadowDOMElement(ShadowDOM.ShadowDOMParent, "button", "id", ShadowDOM.GuidGenerateButtonElementId);
        var guidButtonClicked = elementActions.ButtonClick(generateGuidButtonElement);
        var inputFieldElement = elementActions.GetShadowDOMElement(ShadowDOM.ShadowDOMParent, "input", "id", ShadowDOM.GuidInputElemenId);
        var inputTextFieldValue = inputFieldElement?.GetAttribute("value");
        _testOutputHelper.WriteLine($"InputField GUID = {inputTextFieldValue}");

        var copyGuidButtonElement = elementActions.GetShadowDOMElement(ShadowDOM.ShadowDOMParent, "button", "id", ShadowDOM.CopyGuidButtonElementId);
        var copyButtonClicked = elementActions.ButtonClick(copyGuidButtonElement);
        _testOutputHelper.WriteLine($"copyButtonClicked = {copyButtonClicked}");
        var clipboardValue = await GetClipboardDataAsync();
        _testOutputHelper.WriteLine($"Clipboard GUID = {clipboardValue}");

        // Assert
        clipboardValue.Should().BeEquivalentTo(inputTextFieldValue);
    }

    private async Task<string?> GetClipboardDataAsync()
    {
        return await TextCopy.ClipboardService.GetTextAsync();
    }
}