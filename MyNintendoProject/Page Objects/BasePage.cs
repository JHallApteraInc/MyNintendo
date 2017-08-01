using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;

namespace MyNintendoProject.Page_Objects
{
    class BasePage
    {
        protected const int WAIT_SECONDS = 30;

        public IWebDriver Driver;

        protected BasePage(IWebDriver driver)
        {
            this.Driver = driver;
        }

        protected void Visit(string url)
        {
            if (url.Contains("http"))
            {
                Driver.Navigate().GoToUrl(url);
            }
            else
            {
                Driver.Navigate().GoToUrl(Step_Definitions.BaseSteps.ApplicationBaseUrl + url);
            }
        }

        protected IWebElement Find(By locator)
        {
            return Driver.FindElement(locator);
        }

        protected void Click(By locator)
        {
            Find(locator).Click();
        }

        protected void Type(string message, IWebElement element)
        {
            element.SendKeys(message);
        }

        protected int HowManyElements(By locator)
        {
            return Driver.FindElements(locator).Count;
        }

        protected bool IsDisplayed(By locator)
        {
            try
            {
                return Find(locator).Displayed;
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                return false;
            }
        }

        protected bool WaitFor(By locator, int maxWaitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, System.TimeSpan.
FromSeconds(maxWaitTime));
                wait.Until(ExpectedConditions.ElementExists(locator));
                return true;
            }
            catch (OpenQA.Selenium.WebDriverTimeoutException)
            {
                return false;
            }
        }

        protected bool WaitUntilClickable(By locator, int maxWaitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, System.TimeSpan.
FromSeconds(maxWaitTime));
                wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                return true;
            }
            catch (OpenQA.Selenium.WebDriverTimeoutException)
            {
                return false;
            }
        }

        protected bool WaitUntilInvisible(By locator, int maxWaitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, System.TimeSpan.
FromSeconds(maxWaitTime));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
                return true;
            }
            catch (OpenQA.Selenium.WebDriverTimeoutException)
            {
                return false;
            }
        }

        protected bool WaitUntilPageLoad()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, System.TimeSpan.FromSeconds(30.00));
                wait.Until(driver1 => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));
                return true;
            }
            catch (OpenQA.Selenium.WebDriverException)
            {
                return false;
            }
        }

        protected bool WaitUntilDisplayed(By locator, int maxWaitTime)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, System.TimeSpan.
FromSeconds(maxWaitTime));
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return true;
            }
            catch (OpenQA.Selenium.WebDriverTimeoutException)
            {
                return false;
            }
        }

        private bool IsSitefinityLicensePage()
        {
            try
            {
                By sitefinityTrialPageLocator = By.LinkText("Sitefinity 9.2");
                IWebElement element = Find(sitefinityTrialPageLocator);
                return true;
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                return false;
            }
        }

        protected void WaitForAjax(IWebDriver driver, int timeoutSecs = 10, bool throwException = false)
        {
            for (var i = 0; i < timeoutSecs; i++)
            {
                var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete) return;
                Thread.Sleep(1000);
            }
            if (throwException)
            {
                throw new Exception("WebDriver timed out waiting for AJAX call to complete");
            }
        }

        protected void PressEnter(IWebElement element)
        {
            element.SendKeys(Keys.Return);
        }

        protected void PressBackspace(IWebElement element)
        {
            element.SendKeys(Keys.Backspace);
        }

        protected string ReadElementText(IWebElement element)
        {
            return element.Text;
        }

        protected bool IsSelected(By locator)
        {
            return Driver.FindElement(locator).Selected;
        }

        protected void Hover(IWebElement element)
        {
            Actions builder = new Actions(Driver);
            builder.MoveToElement(element).Perform();
        }

        protected void AcceptPopup()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
                wait.Until(ExpectedConditions.AlertIsPresent());
                IAlert alert = Driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch (Exception e)
            {
                //fail quietly if no alert
            }
        }

        protected void DismissPopup()
        {
            IAlert popup = Driver.SwitchTo().Alert();
            popup.Dismiss();
        }

        protected void SwitchWindow()
        {
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
        }

        protected void SwitchWindow(string windowName)
        {
            Driver.SwitchTo().Window(windowName);
        }

        protected void ClearTextbox(IWebElement element)
        {
            element.Clear();
        }

        protected void SelectDropdown(By dropdownLocator, string textSelector)
        {
            SelectElement dropdown = new SelectElement(Find(dropdownLocator));
            WaitFor(dropdownLocator, WAIT_SECONDS);
            Click(dropdownLocator);
            dropdown.SelectByText(textSelector);
        }

        public object ExecuteJavaScript(string script)
        {
            return ((IJavaScriptExecutor)Driver).ExecuteScript(script);
        }

        public static By SelectorByAttributeValue(string p_strAttributeName, string p_strAttributeValue)
        {
            return (By.XPath(String.Format("//*[@{0} = '{1}']",
                                           p_strAttributeName,
                                           p_strAttributeValue)));
        }
    }
}