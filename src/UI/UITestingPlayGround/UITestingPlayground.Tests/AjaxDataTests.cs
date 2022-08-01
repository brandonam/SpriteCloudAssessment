using System;
using Flurl.Http;
using Flurl.Http.Testing;
using Moq;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Xunit;
using Xunit.Abstractions;
using UITestingPlayGround.Model.Pages;
using UITestingPlayGround.Model.Shared;
using UITestingPlayground.Tests.Helper;
using OpenQA.Selenium.Support.UI;
using FluentAssertions;
using System.Threading;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace UITestingPlayground.Tests;

[TestCaseOrderer("UITestingPlayground.Tests.Helper.PriorityOrderer", "UITestingPlayground.Tests")]
public class AjaxDataTests : IDisposable
{
    private readonly ITestOutputHelper _testOutputHelper;
    private IWebDriver _webDriver;
    private const string RequestUrl = "http://www.uitestingplayground.com/ajax";
    private int _maxWaitTime = 20;

    // Setup
    public AjaxDataTests(ITestOutputHelper testOutputHelper)
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
    public void Given_fetch_ajax_button_exists_Should_not_return_null()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(_maxWaitTime));
        IWebElement ajaxDataButtonElement = wait.Until(driver => driver.FindElement(AjaxData.AjaxDataButtonElement));

        // Assert
        Assert.NotNull(ajaxDataButtonElement);
    }

    [Fact, TestPriority(2)]
    public void Given_loading_daisy_hidden_but_exists_Should_not_return_null()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(_maxWaitTime));
        IWebElement loadingDaisyInvisibleElement = wait.Until(driver => driver.FindElement(AjaxData.LoadingDaisyInitializedInvisibleElement));

        // Assert
        Assert.NotNull(loadingDaisyInvisibleElement);
    }

    [Fact, TestPriority(3)]
    public void Given_fetch_ajax_button_exists_When_button_clicked_Should_display_loading_daisy()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        var fetchAjaxButtonClicked = elementActions.ButtonClick(AjaxData.AjaxDataButtonElement);
        WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(_maxWaitTime));
        IWebElement loadingDaisyVisibleElement = wait.Until(driver => driver.FindElement(AjaxData.LoadingDaisyVisibleElement));

        // Assert
        Assert.NotNull(loadingDaisyVisibleElement);
    }

    [Fact, TestPriority(3)]
    public void Given_fetch_ajax_button_exists_When_button_clicked_Should_load_data_after_max_15_seconds()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        var fetchAjaxButtonClicked = elementActions.ButtonClick(AjaxData.AjaxDataButtonElement);
        WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(_maxWaitTime));
        IWebElement dataRecievedElement = wait.Until(driver => driver.FindElement(AjaxData.AjaxDataRecievedElement));

        // Assert
        Assert.NotNull(dataRecievedElement);
    }

    [Fact, TestPriority(3)]
    public void Given_fetch_ajax_button_exists_When_button_clicked_twice_Should_load_two_elements_with_data_after_30_seconds()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        var fetchAjaxButtonClicked = elementActions.ButtonClick(AjaxData.AjaxDataButtonElement);
        WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(_maxWaitTime));
        IWebElement dataRecievedElement = wait.Until(driver => driver.FindElement(AjaxData.AjaxDataRecievedElement));
        if(dataRecievedElement is not null)
        {
            _testOutputHelper.WriteLine("First AJAX data payload recieved.");
        }
        // Wait until the loading daisy is hidden
        wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(_maxWaitTime));
        wait.Until(driver => driver.FindElement(AjaxData.LoadingDaisyInvisibleElement));
        // Safe to interact with the button again
        fetchAjaxButtonClicked = elementActions.ButtonClick(AjaxData.AjaxDataButtonElement);
        // Wait until the loading daisy is visible
        wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(_maxWaitTime));
        wait.Until(driver => driver.FindElement(AjaxData.LoadingDaisyVisibleElement));
        // Wait until the loading daisy is hidden
        wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(_maxWaitTime));
        var test = wait.Until(driver => driver.FindElement(AjaxData.LoadingDaisyInvisibleElement));

        var recievedAjaxCollection = elementActions.GetElementCollection(AjaxData.AjaxDataRecievedElement);

        // Assert
        recievedAjaxCollection.Should().HaveCountGreaterThan(1);
    }
}