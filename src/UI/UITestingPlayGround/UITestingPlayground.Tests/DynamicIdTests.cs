using System;
using System.IO;
using OpenQA.Selenium.Chrome;
using Xunit;
using Xunit.Abstractions;

namespace UITestingPlayground.Tests;

public class DynamicIdTests : IDisposable
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ChromeDriver _chromeDriver;
    private const string BaseUrl = "http://www.uitestingplayground.com/";

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

    [Fact]
    public void Test1()
    {
        Console.WriteLine("First test");
        _testOutputHelper.WriteLine("First test");
        _chromeDriver.Navigate().GoToUrl(BaseUrl);
    }
}