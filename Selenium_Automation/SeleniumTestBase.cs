using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace SeleniumTestBase
{
    public class TestBase
    {
        protected IWebDriver Driver;
        private string _browserType;
        private string _deviceName;

        public TestBase(string browserType, string deviceName = null)
        {
            _browserType = browserType;
            _deviceName = deviceName;
        }

        [SetUp]
        public void SetUp()
        {
            switch (_browserType.ToLower())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();

                    if (_deviceName != null)
                    {
                        chromeOptions.EnableMobileEmulation(_deviceName);
                    }

                    chromeOptions.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.9999.99 Safari/537.36");

                    Driver = new ChromeDriver(chromeOptions);
                    break;

                case "firefox":
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.SetPreference("general.useragent.override", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:99.0) Gecko/20100101 Firefox/99.0");

                    Driver = new FirefoxDriver(firefoxOptions);
                    break;

                case "edge":
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.9999.99 Safari/537.36 Edg/99.0.9999.99");
                    
                    Driver = new EdgeDriver(edgeOptions);
                    break;

                case "internetexplorer":
                case "ie":
                    var ieOptions = new InternetExplorerOptions();
                    ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    ieOptions.IgnoreZoomLevel = true;

                    Driver = new InternetExplorerDriver(ieOptions);
                    break;

                default:
                    throw new ArgumentException($"Browser type '{_browserType}' is not supported.");
            }

            Driver.Manage().Window.Maximize();
            Console.WriteLine($"Starting test on {_browserType}.");
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                var screenshotFilePath = $"{TestContext.CurrentContext.WorkDirectory}\\{TestContext.CurrentContext.Test.Name}.png";
                screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);
                TestContext.AddTestAttachment(screenshotFilePath);
                Console.WriteLine($"Test failed, screenshot saved to: {screenshotFilePath}");
            }

            Driver.Quit();
            Console.WriteLine($"Test finished with result: {TestContext.CurrentContext.Result.Outcome.Status}");
        }
    }
}
