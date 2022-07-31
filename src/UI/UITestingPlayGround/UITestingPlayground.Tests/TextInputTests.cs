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

namespace UITestingPlayground.Tests;

[TestCaseOrderer("UITestingPlayground.Tests.Helper.PriorityOrderer", "UITestingPlayground.Tests")]
public class TextInputTests : IDisposable
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ChromeDriver _driver;
    private const string RequestUrl = "http://www.uitestingplayground.com/textinput";

    // Setup
    public TextInputTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);
    }

    // Teardown
    public void Dispose()
    {
        _driver.Quit();
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
        httpFlurlTest.ShouldHaveCalled(RequestUrl);
        httpFlurlTest.RespondWith(It.IsAny<string>(), 304);
    }

    [Fact, TestPriority(2)]
    public void Given_Text_Input_Exists_Should_Not_Return_Null()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_driver);
        // navigate to the page
        _driver.Navigate().GoToUrl(RequestUrl);

        // Act
        var dynamicButton = elementActions.GetElement(TextInput.TextInputElement);

        // Assert
        Assert.NotNull(dynamicButton);
    }

    [Fact, TestPriority(2)]
    public void Given_Update_Button_Exists_Should_Not_Return_Null()
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_driver);
        // navigate to the page
        _driver.Navigate().GoToUrl(RequestUrl);

        // Act
        var dynamicButton = elementActions.GetElement(TextInput.UpdateButtonElement);

        // Assert
        Assert.NotNull(dynamicButton);
    }

    [Theory, TestPriority(3)]
    [InlineData("New Text")]
    [InlineData("BUTTONUPDATED")]
    [InlineData("button_!#$%^&")]
    [InlineData("1")]
    [InlineData(".")]
    [InlineData(". .")]
    public void Given_input_text_and_update_button_Exists_When_input_added_and_update_button_clicked_Should_update_button_text(string text)
    {
        // Arrange
        ElementActions elementActions = new ElementActions(_driver);
        // navigate to the page
        _driver.Navigate().GoToUrl(RequestUrl);
        // wait at least 10 seconds for the website to render the required elements
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        IWebElement textInputExists = wait.Until(driver => driver.FindElement(TextInput.TextInputElement));
        IWebElement updateButtonExists = wait.Until(driver => driver.FindElement(TextInput.UpdateButtonElement));

        // Act
        var updateTextInput = elementActions.SetTextInput(TextInput.TextInputElement, text);
        var updateButtonClicked = elementActions.ButtonClick(TextInput.UpdateButtonElement);
        var readUpdateButtonText = elementActions.GetElementText(TextInput.UpdateButtonElement);

        // Assert
        readUpdateButtonText.Should().BeEquivalentTo(text);
    }

    [Theory, TestPriority(3)]
    [InlineData("")]
    [InlineData(null)]
    public void Given_input_text_and_update_button_Exists_When_no_input_and_update_button_clicked_Should_not_update_button_text(string text)
    {
        // Arrange
        const string expectedButtonText = "Button That Should Change it's Name Based on Input Value";
        ElementActions elementActions = new ElementActions(_driver);
        // navigate to the page
        _driver.Navigate().GoToUrl(RequestUrl);
        // wait at least 10 seconds for the website to render the required elements
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        IWebElement textInputExists = wait.Until(driver => driver.FindElement(TextInput.TextInputElement));
        IWebElement updateButtonExists = wait.Until(driver => driver.FindElement(TextInput.UpdateButtonElement));

        // Act
        var updateTextInput = elementActions.SetTextInput(TextInput.TextInputElement, text);
        var updateButtonClicked = elementActions.ButtonClick(TextInput.UpdateButtonElement);
        var readUpdateButtonText = elementActions.GetElementText(TextInput.UpdateButtonElement);

        // Assert
        readUpdateButtonText.Should().BeEquivalentTo(expectedButtonText);
    }


    [Fact, TestPriority(3)]
    public void Given_input_text_and_update_button_Exists_When_input_and_update_button_clicked_more_than_once_Should_find_each_update()
    {
        // Arrange
        const string firstExpectedButtonText = "Button Change One";
        const string secondExpectedButtonText = "Button Change Two";
        const string finalExpectedButtonText = "Button Change Three";
        ElementActions elementActions = new ElementActions(_driver);
        // navigate to the page
        _driver.Navigate().GoToUrl(RequestUrl);
        // wait at least 10 seconds for the website to render the required elements
        WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        IWebElement textInputExists = wait.Until(driver => driver.FindElement(TextInput.TextInputElement));
        IWebElement updateButtonExists = wait.Until(driver => driver.FindElement(TextInput.UpdateButtonElement));

        // Act
        // first update
        _testOutputHelper.WriteLine($"Changing text to - {firstExpectedButtonText}");
        var updateTextInput = elementActions.SetTextInput(TextInput.TextInputElement, firstExpectedButtonText);
        _testOutputHelper.WriteLine($"Update Result - {updateTextInput}");
        var updateButtonClicked = elementActions.ButtonClick(TextInput.UpdateButtonElement);
        _testOutputHelper.WriteLine($"Button Click Result - {updateButtonClicked}");
        elementActions.ClearTextInput(TextInput.TextInputElement);
        // second update
        _testOutputHelper.WriteLine($"Changing text to - {firstExpectedButtonText}");
        updateTextInput = elementActions.SetTextInput(TextInput.TextInputElement, secondExpectedButtonText);
        _testOutputHelper.WriteLine($"Update Result - {updateTextInput}");
        updateButtonClicked = elementActions.ButtonClick(TextInput.UpdateButtonElement);
        _testOutputHelper.WriteLine($"Button Click Result - {updateButtonClicked}");
        elementActions.ClearTextInput(TextInput.TextInputElement);
        // final update
        _testOutputHelper.WriteLine($"Changing text to - {firstExpectedButtonText}");
        updateTextInput = elementActions.SetTextInput(TextInput.TextInputElement, finalExpectedButtonText);
        _testOutputHelper.WriteLine($"Update Result - {updateTextInput}");
        updateButtonClicked = elementActions.ButtonClick(TextInput.UpdateButtonElement);
        _testOutputHelper.WriteLine($"Button Click Result - {updateButtonClicked}");
        var readUpdateButtonText = elementActions.GetElementText(TextInput.UpdateButtonElement);

        // Assert
        readUpdateButtonText.Should().BeEquivalentTo(finalExpectedButtonText);
    }
}