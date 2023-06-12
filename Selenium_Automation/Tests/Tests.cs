using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTestBase;
using Selenium_Automation;

namespace Tests
{
    [TestFixture("chrome")]
    [TestFixture("firefox")]
    [TestFixture("edge")]
    public class Tests : TestBase
    {
        private Helper Helper;

        public Tests(string browserType) : base(browserType)
        {
        }

        [SetUp]
        public void TestSetup()
        {
            Helper = new Helper(Driver);
        }

        [Test]
        [Description("Test that clicks an element")]
        public void Test_ClickElement()
        {
            Driver.Navigate().GoToUrl("https://www.example.com");
            Helper.ClickElement(By.Id("some-id"));
            // Add assertions or further actions based on the result of clicking the element
        }

        [Test]
        [Description("Test that enters text into an input field")]
        public void Test_EnterText()
        {
            Driver.Navigate().GoToUrl("https://www.example.com");
            Helper.EnterText(By.Id("input-id"), "test text");
            // Add assertions or further actions based on entering text
        }

        [Test]
        [Description("Test that reads text from an element")]
        public void Test_ReadText()
        {
            Driver.Navigate().GoToUrl("https://www.example.com");
            string text = Helper.ReadText(By.Id("text-id"));
            Assert.AreEqual("expected text", text);
            // Add assertions or further actions based on the read text
        }

        [Test]
        [Retry(3)]
        [MaxTime(5000)]
        [Description("Test that demonstrates retrying the test up to 3 times and setting a maximum execution time of 5 seconds")]
        public void Test_RetryAndMaxTime()
        {
            // Test logic here
        }

        // Add more tests as needed
    }
}
