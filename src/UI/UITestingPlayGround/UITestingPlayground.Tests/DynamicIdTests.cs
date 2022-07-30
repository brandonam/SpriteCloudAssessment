using System;
using System.IO;
using Flurl.Http;
using Flurl.Http.Testing;
using Moq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using UITestingPlayGround.Model.Pages;
using Flurl;
using UITestingPlayground.Tests.Helper;
using OpenQA.Selenium.Support.UI;

namespace UITestingPlayground.Tests;

[TestCaseOrderer("UITestingPlayground.Tests.Helper.PriorityOrderer", "UITestingPlayground.Tests")]
public class DynamicIdTests : IDisposable
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ChromeDriver _chromeDriver;
    private const string RequestUrl = "http://www.uitestingplayground.com/dynamicid";

    // Setup
    public DynamicIdTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _chromeDriver = new ChromeDriver(System.AppDomain.CurrentDomain.BaseDirectory);
    }

    // Teardown
    public void Dispose()
    {
        _chromeDriver.Quit();
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

        DynamicIdButton uiTapHelper = new DynamicIdButton(_chromeDriver);
        // navigate to the page
        _chromeDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        var dynamicButton = uiTapHelper.GetDynamicIdButtonElement();

        // Assert
        Assert.NotNull(dynamicButton);
    }

    [Fact, TestPriority(2)]
    public void Given_Dynamic_Id_Button_Exists_When_Clicked_Should_Return_True()
    {
        // Arrange

        DynamicIdButton uiTapHelper = new DynamicIdButton(_chromeDriver);
        // navigate to the page
        _chromeDriver.Navigate().GoToUrl(RequestUrl);
        new WebDriverWait(_chromeDriver, TimeSpan.FromSeconds(10)).Until(driver => driver.FindElement(uiTapHelper.DynamicButton));

        // Act
        var dynamicButtonClicked = uiTapHelper.DynamicIdButtonClicked();

        // Assert
        Assert.True(dynamicButtonClicked);
    }
}