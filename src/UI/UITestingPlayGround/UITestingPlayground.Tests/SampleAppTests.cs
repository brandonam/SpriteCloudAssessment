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
public class SampleAppTests : IDisposable
{
    private readonly ITestOutputHelper _testOutputHelper;
    private IWebDriver _webDriver;
    private const string RequestUrl = "http://www.uitestingplayground.com/SampleApp";
    private int _maxWaitTime = 10;

    // Setup
    public SampleAppTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        //ChromeOptions options = new ChromeOptions();
        //options.AddArgument("--headless");
        //options.AddArgument("--no-sandbox");
        //_webDriver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
        new DriverManager().SetUpDriver(new ChromeConfig());
        _webDriver = new ChromeDriver();
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
    public void Given_username_text_input_exists_Should_not_return_null()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        var dynamicButton = elementActions.GetElement(SampleApp.UsernameTextInputElement);

        // Assert
        Assert.NotNull(dynamicButton);
    }

    [Fact, TestPriority(2)]
    public void Given_password_text_input_exists_Should_not_return_null()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        var dynamicButton = elementActions.GetElement(SampleApp.PasswordTextInputElement);

        // Assert
        Assert.NotNull(dynamicButton);
    }

    [Fact, TestPriority(2)]
    public void Given_login_button_exists_Should_not_return_null()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        var dynamicButton = elementActions.GetElement(SampleApp.LoginButtonElement);

        // Assert
        Assert.NotNull(dynamicButton);
    }

    [Fact, TestPriority(2)]
    public void Given_login_status_exists_Should_not_return_null()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);

        // Act
        var dynamicButton = elementActions.GetElement(SampleApp.LoginStatusElement);

        // Assert
        Assert.NotNull(dynamicButton);
    }

    [Theory, TestPriority(3)]
    [InlineData("test", "pwd")]
    [InlineData("1234", "pwd")]
    [InlineData("!@#$", "pwd")]
    [InlineData("Test1234%", "pwd")]
    public void Given_valid_user_and_password_When_login_button_clicked_Should_update_login_status(string username, string password)
    {
        // Arrange
        string expectedLoginStatusMessage = $"Welcome, {username}!";
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);
        // wait at least _maxWaitTime seconds for the website to render the required elements
        WaitForRequiredElements();

        // Act
        elementActions.SetTextInput(SampleApp.UsernameTextInputElement, username);
        elementActions.SetTextInput(SampleApp.PasswordTextInputElement, password);
        elementActions.ButtonClick(SampleApp.LoginButtonElement);
        var loginStatusText = elementActions.GetElementText(SampleApp.LoginStatusElement);

        // Assert
        loginStatusText.Should().BeEquivalentTo(expectedLoginStatusMessage);
    }

    [Theory, TestPriority(3)]
    [InlineData("", "")]
    [InlineData(null, "p")]
    [InlineData("!@#$", "test")]
    [InlineData("!@#$", null)]
    public void Given_invalid_user_and_password_When_login_button_clicked_Should_display_login_status_invalid(string username, string password)
    {
        // Arrange
        const string expectedLoginStatusMessage = "Invalid username/password";
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);
        // wait at least _maxWaitTime seconds for the website to render the required elements
        WaitForRequiredElements();

        // Act
        elementActions.SetTextInput(SampleApp.UsernameTextInputElement, username);
        elementActions.SetTextInput(SampleApp.PasswordTextInputElement, password);
        elementActions.ButtonClick(SampleApp.LoginButtonElement);
        var loginStatusText = elementActions.GetElementText(SampleApp.LoginStatusElement);

        // Assert
        loginStatusText.Should().BeEquivalentTo(expectedLoginStatusMessage);
    }

    [Theory, TestPriority(3)]
    [InlineData("test", "pwd")]
    [InlineData("1234", "pwd")]
    [InlineData("!@#$", "pwd")]
    [InlineData("Test1234%", "pwd")]
    public void Given_valid_user_and_password_When_login_button_clicked_followedby_logout_clicked_Should_update_login_status(string username, string password)
    {
        // Arrange
        string expectedLoginStatusMessage = $"User logged out.";
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);
        // wait at least _maxWaitTime seconds for the website to render the required elements
        WaitForRequiredElements();

        // Act
        elementActions.SetTextInput(SampleApp.UsernameTextInputElement, username);
        elementActions.SetTextInput(SampleApp.PasswordTextInputElement, password);
        elementActions.ButtonClick(SampleApp.LoginButtonElement);
        elementActions.ButtonClick(SampleApp.LoginButtonElement);
        var loginStatusText = elementActions.GetElementText(SampleApp.LoginStatusElement);

        // Assert
        loginStatusText.Should().BeEquivalentTo(expectedLoginStatusMessage);
    }

    [Theory, TestPriority(3)]
    [InlineData("test", "pwd")]
    [InlineData("1234", "pwd")]
    [InlineData("!@#$", "pwd")]
    [InlineData("Test1234%", "pwd")]
    public void Given_valid_user_and_password_When_login_button_clicked_followedby_logout_clicked_and_then_login_again_Should_update_login_status(string username, string password)
    {
        // Arrange
        string expectedLoginStatusMessage = $"Welcome, {username}!";
        ElementActions elementActions = new ElementActions(_webDriver);
        // navigate to the page
        _webDriver.Navigate().GoToUrl(RequestUrl);
        // wait at least _maxWaitTime seconds for the website to render the required elements
        WaitForRequiredElements();

        // Act

        elementActions.SetTextInput(SampleApp.UsernameTextInputElement, username);
        elementActions.SetTextInput(SampleApp.PasswordTextInputElement, password);
        elementActions.ButtonClick(SampleApp.LoginButtonElement);
        // logout
        elementActions.ButtonClick(SampleApp.LoginButtonElement);
        // login again
        elementActions.SetTextInput(SampleApp.UsernameTextInputElement, username);
        elementActions.SetTextInput(SampleApp.PasswordTextInputElement, password);
        elementActions.ButtonClick(SampleApp.LoginButtonElement);
        var loginStatusText = elementActions.GetElementText(SampleApp.LoginStatusElement);

        // Assert
        loginStatusText.Should().BeEquivalentTo(expectedLoginStatusMessage);
    }

    private void WaitForRequiredElements()
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken = new CancellationToken();
        cancellationToken.ThrowIfCancellationRequested();

        WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(_maxWaitTime));
        IWebElement usernameInputExists = wait.Until(driver => driver.FindElement(SampleApp.UsernameTextInputElement), cancellationToken);
        if (usernameInputExists is null)
        {
            _testOutputHelper.WriteLine("usernameInput not found cancelling operation.");
            cancellationTokenSource.Cancel();
        }
        IWebElement passwordInputExists = wait.Until(driver => driver.FindElement(SampleApp.PasswordTextInputElement), cancellationToken);
        if (passwordInputExists is null)
        {
            _testOutputHelper.WriteLine("passwordInput not found cancelling operation.");
            cancellationTokenSource.Cancel();
        }
        IWebElement loginButtonExists = wait.Until(driver => driver.FindElement(SampleApp.LoginButtonElement), cancellationToken);
        if (loginButtonExists is null)
        {
            _testOutputHelper.WriteLine("loginButton not found cancelling operation.");
            cancellationTokenSource.Cancel();
        }
        IWebElement loginStatusExists = wait.Until(driver => driver.FindElement(SampleApp.LoginStatusElement), cancellationToken);
        if (loginStatusExists is null)
        {
            _testOutputHelper.WriteLine("loginStatus not found cancelling operation.");
            cancellationTokenSource.Cancel();
        }
    }
}