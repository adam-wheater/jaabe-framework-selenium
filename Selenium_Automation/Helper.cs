using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Selenium_Automation
{
    public class Helper
    {
        private IWebDriver Driver;

        public Helper(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebElement FindElement(By locator, int timeoutInSeconds = 10)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(driver =>
                {
                    try
                    {
                        var element = driver.FindElement(locator);
                        return element.Displayed && element.Enabled ? element : null;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                }) ?? throw new NotFoundException();
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception($"Element with locator: '{locator}' was not found in current context page within {timeoutInSeconds} seconds.");
            }
        }

        public IReadOnlyCollection<IWebElement> FindElements(By locator, int timeoutInSeconds = 10)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(driver =>
                {
                    try
                    {
                        var elements = driver.FindElements(locator);
                        return elements.Count > 0 ? elements : null;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                }) ?? throw new NotFoundException();
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception($"No elements with locator: '{locator}' were found in current context page within {timeoutInSeconds} seconds.");
            }
        }


        public void ClickElement(By locator)
        {
            try
            {
                FindElement(locator).Click();
            }
            catch (Exception)
            {
                throw new Exception($"Failed to click on element with locator: '{locator}'");
            }
        }

        public void EnterText(By locator, string text)
        {
            try
            {
                FindElement(locator).SendKeys(text);
            }
            catch (Exception)
            {
                throw new Exception($"Failed to enter text into element with locator: '{locator}'");
            }
        }

        public string ReadText(By locator)
        {
            try
            {
                return FindElement(locator).Text;
            }
            catch (Exception)
            {
                throw new Exception($"Failed to read text from element with locator: '{locator}'");
            }
        }

        public IWebElement WaitForElementToBeVisible(By locator, int timeoutInSeconds)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(driver =>
                {
                    var element = driver.FindElement(locator);
                    return (element.Displayed && element.Enabled) ? element : throw new NotFoundException();
                });
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception($"Element with locator: '{locator}' was not visible in current context page after waiting for {timeoutInSeconds} seconds.");
            }
        }

        public IWebElement WaitForElementsToBeVisible(By locator, int timeoutInSeconds)
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutInSeconds));
                var elements = wait.Until(driver => driver.FindElements(locator));

                return elements.FirstOrDefault(element => element.Displayed && element.Enabled) ?? throw new NotFoundException();
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception($"Element with locator: '{locator}' was not visible in current context page after waiting for {timeoutInSeconds} seconds.");
            }
        }
    }
}
