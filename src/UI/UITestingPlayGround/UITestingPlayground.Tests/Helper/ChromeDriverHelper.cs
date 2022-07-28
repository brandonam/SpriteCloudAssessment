using System.IO;
using System.Linq;

namespace UITestingPlayground.Tests.Helper;

public static class ChromeDriverHelper
{
    public static FileInfo? ChromeDriverFolderPath()
    {
        const string chromeDriverFile = "chromedriver.exe";
        var matchingFiles = Directory.EnumerateFiles(System.AppDomain.CurrentDomain.BaseDirectory, chromeDriverFile);
        if (matchingFiles.Any())
        {
            string? selectedChromeDriverFile = matchingFiles.FirstOrDefault();
            if (!string.IsNullOrEmpty(selectedChromeDriverFile))
            {
                return new FileInfo(selectedChromeDriverFile);
            }
            return null;
        }
        return null;
    }
}