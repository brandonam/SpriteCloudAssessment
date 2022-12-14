using Flurl.Http;
using Flurl.Http.Testing;
using Moq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using UITestingPlayground.Tests.Helper;
using UITestingPlayGround.Model.Pages;
using UITestingPlayGround.Model.Shared;
using Xunit;
using Xunit.Abstractions;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium;

namespace UITestingPlayground.Tests;

[TestCaseOrderer("UITestingPlayground.Tests.Helper.PriorityOrderer", "UITestingPlayground.Tests")]
public class DynamicIdTests : IDisposable
{
    private readonly ITestOutputHelper _testOutputHelper;
    private IWebDriver _webDriver;
    private const string RequestUrl = "http://www.uitestingplayground.com/dynamicid";

    // Setup
    public DynamicIdTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        ChromeOptions options = new ChromeOptions();
        options.AddArguments("start-maximized"); // open Browser in maximized mode
        options.AddArguments("disable-infobars"); // disabling infobars
        options.AddArguments("--disable-extensions"); // disabling extensions
        options.AddArguments("--disable-gpu"); // applicable to windows os only
        options.AddArguments("--disable-dev-shm-usage"); // overcome limited resource problems
        options.AddArguments("--no-sandbox"); // Bypass OS security model
        options.AddArguments("--headless");
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
    public void Given_Dynamic_Id_Button_Exists_Should_Not_Return_Null()
    {
        // Arrange

        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        var dynamicButton = elementActions.GetElement(DynamicIdButton.DynamicIdButtonElement);

        // Assert
        Assert.NotNull(dynamicButton);
    }

    [Fact, TestPriority(2)]
    public void Given_Dynamic_Id_Button_Exists_When_Clicked_Should_Return_True()
    {
        // Arrange

        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);
        // wait at least 10 seconds for the website to render
        new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10)).Until(driver => driver.FindElement(DynamicIdButton.DynamicIdButtonElement));

        // Act
        var dynamicButtonClicked = elementActions.ButtonClick(DynamicIdButton.DynamicIdButtonElement);

        // Assert
        Assert.True(dynamicButtonClicked);
    }
}