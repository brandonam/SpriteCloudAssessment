using System.IO;
using System.Linq;

namespace UITestingPlayground.Tests.Helper;

public static class DriverHelper
{
    public static FileInfo? DriverFolderPath(string filename = "chromedriver.exe")
    {
        var matchingFiles = Directory.EnumerateFiles(System.AppDomain.CurrentDomain.BaseDirectory, filename);
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