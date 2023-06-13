using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;


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
        public void RefreshPage()
        {
            try
            {
                Driver.Navigate().Refresh();
                Console.WriteLine("Refreshed the page.");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to refresh the page.");
                throw;
            }
        }

        public void NavigateBack()
        {
            try
            {
                Driver.Navigate().Back();
                Console.WriteLine("Navigated back to the previous page.");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to navigate back.");
                throw;
            }
        }

        public void NavigateForward()
        {
            try
            {
                Driver.Navigate().Forward();
                Console.WriteLine("Navigated forward to the next page.");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to navigate forward.");
                throw;
            }
        }

        public void ClearText(By locator)
        {
            try
            {
                FindElement(locator).Clear();
                Console.WriteLine($"Cleared text from element with locator: '{locator}'");
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to clear text from element with locator: '{locator}'");
                throw;
            }
        }

        public void Submit(By locator)
        {
            try
            {
                FindElement(locator).Submit();
                Console.WriteLine($"Submitted form starting from element with locator: '{locator}'");
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to submit form starting from element with locator: '{locator}'");
                throw;
            }
        }

        public void DragAndDrop(By sourceLocator, By destinationLocator)
        {
            try
            {
                var sourceElement = FindElement(sourceLocator);
                var destinationElement = FindElement(destinationLocator);
                new Actions(Driver).DragAndDrop(sourceElement, destinationElement).Perform();
                Console.WriteLine($"Performed drag and drop from '{sourceLocator}' to '{destinationLocator}'");
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to perform drag and drop from '{sourceLocator}' to '{destinationLocator}'");
                throw;
            }
        }

        public void SwitchToFrame(By locator)
        {
            try
            {
                Driver.SwitchTo().Frame(FindElement(locator));
                Console.WriteLine($"Switched to frame with locator: '{locator}'");
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to switch to frame with locator: '{locator}'");
                throw;
            }
        }

        public void SwitchToDefaultContent()
        {
            try
            {
                Driver.SwitchTo().DefaultContent();
                Console.WriteLine("Switched back to the default content from an iframe.");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to switch back to the default content from an iframe.");
                throw;
            }
        }

        public void SwitchToNewWindow()
        {
            try
            {
                foreach (string handle in Driver.WindowHandles)
                {
                    Driver.SwitchTo().Window(handle);
                }
                Console.WriteLine("Switched to the new window.");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to switch to the new window.");
                throw;
            }
        }

        public void SwitchToMainWindow()
        {
            try
            {
                Driver.SwitchTo().Window(Driver.WindowHandles.First());
                Console.WriteLine("Switched back to the main window.");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to switch back to the main window.");
                throw;
            }
        }

        public void CloseCurrentWindow()
        {
            try
            {
                Driver.Close();
                Console.WriteLine("Closed the current window.");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to close the current window.");
                throw;
            }
        }

        public void DoubleClick(By locator)
        {
            try
            {
                new Actions(Driver).DoubleClick(FindElement(locator)).Perform();
                Console.WriteLine($"Performed double click on element with locator: '{locator}'");
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to perform double click on element with locator: '{locator}'");
                throw;
            }
        }

        public void RightClick(By locator)
        {
            try
            {
                new Actions(Driver).ContextClick(FindElement(locator)).Perform();
                Console.WriteLine($"Performed right click on element with locator: '{locator}'");
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to perform right click on element with locator: '{locator}'");
                throw;
            }
        }

        public void HoverOverElement(By locator)
        {
            try
            {
                new Actions(Driver).MoveToElement(FindElement(locator)).Perform();
                Console.WriteLine($"Hovered over element with locator: '{locator}'");
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to hover over element with locator: '{locator}'");
                throw;
            }
        }

        public void ScrollToBottom()
        {
            try
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                Console.WriteLine("Scrolled to the bottom of the page.");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to scroll to the bottom of the page.");
                throw;
            }
        }

        public void ScrollToTop()
        {
            try
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, 0)");
                Console.WriteLine("Scrolled to the top of the page.");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to scroll to the top of the page.");
                throw;
            }
        }

        public void NavigateToUrl(string url)
        {
            try
            {
                Driver.Navigate().GoToUrl(url);
                Console.WriteLine($"Navigated to URL: {url}");
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to navigate to URL: {url}");
                throw;
            }
        }

        public bool IsElementPresent(By locator)
        {
            try
            {
                Driver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void SelectDropdownByValue(By locator, string value)
        {
            try
            {
                new SelectElement(FindElement(locator)).SelectByValue(value);
                Console.WriteLine($"Selected '{value}' from dropdown with locator: '{locator}'");
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to select '{value}' from dropdown with locator: '{locator}'");
                throw;
            }
        }

        public void SelectDropdownByText(By locator, string text)
        {
            try
            {
                new SelectElement(FindElement(locator)).SelectByText(text);
                Console.WriteLine($"Selected '{text}' from dropdown with locator: '{locator}'");
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to select '{text}' from dropdown with locator: '{locator}'");
                throw;
            }
        }

        public string GetAttributeValue(By locator, string attributeName)
        {
            try
            {
                string value = FindElement(locator).GetAttribute(attributeName);
                Console.WriteLine($"Retrieved value '{value}' of attribute '{attributeName}' from element with locator: '{locator}'");
                return value;
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to retrieve value of attribute '{attributeName}' from element with locator: '{locator}'");
                throw;
            }
        }

        public void ScrollToElement(By locator)
        {
            try
            {
                ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", FindElement(locator));
                Console.WriteLine($"Scrolled to element with locator: '{locator}'");
            }
            catch (Exception)
            {
                Console.WriteLine($"Failed to scroll to element with locator: '{locator}'");
                throw;
            }
        }


    }
}