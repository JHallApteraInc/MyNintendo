using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Edge;
using System.Configuration;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Remote;
using System;
using System.Drawing;
using System.Linq;
using System.IO;

namespace MyNintendoProject.Step_Definitions
{
    class BaseSteps : Steps
    {
        protected IWebDriver Driver;
        public static string BrowserName;
        public static string ApplicationBaseUrl;

        protected void LoadConfigValues()
        {
            var configReader = new AppSettingsReader();
            BrowserName = (string)configReader.GetValue("BrowserName", typeof(string));
            ApplicationBaseUrl = (string)configReader.GetValue("ApplicationBaseUrl", typeof(string));
        }

        protected void CheckBrowser()
        {
            switch (BrowserName.ToLower())
            {
                case "firefox":
                    FirefoxProfile profile = new FirefoxProfile();
                    //FirefoxBinary binary = new FirefoxBinary(VendorDirectory + "\\geckodriver.exe");
                    //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(VendorDirectory);
                    //profile.SetPreference("network.http.phishy-userpass-length", 255);
                    profile.SetPreference("network.automatic-ntlm-auth.trusted-uris", "mytenet.com");
                    profile.EnableNativeEvents = false;
                    Driver = new FirefoxDriver(profile);
                    //Adding driver to PATH requires a restart of VS
                    Driver.Manage().Window.Maximize();
                    ScenarioContext.Current["Driver"] = Driver;
                    break;
                case "chrome":
                    Driver = new ChromeDriver();
                    Driver.Manage().Window.Maximize();
                    Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(45);
                    ScenarioContext.Current["Driver"] = Driver;
                    break;
                case "ie":
                    InternetExplorerOptions cap = new InternetExplorerOptions();
                    cap.IgnoreZoomLevel = true;
                    cap.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    cap.EnableNativeEvents = false;
                    cap.UnexpectedAlertBehavior = InternetExplorerUnexpectedAlertBehavior.Dismiss;
                    Driver = new InternetExplorerDriver(cap);
                    Driver.Manage().Window.Maximize();
                    Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(45);
                    ScenarioContext.Current["Driver"] = Driver;
                    break;
                case "ie-mobile":
                    InternetExplorerOptions cape = new InternetExplorerOptions();
                    cape.IgnoreZoomLevel = true;
                    cape.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    cape.EnableNativeEvents = false;
                    Driver = new InternetExplorerDriver(cape);
                    Driver.Manage().Window.Size = new Size(1280, 850);
                    Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(45);
                    ScenarioContext.Current["Driver"] = Driver;
                    break;
            }
        }

        private void SetWebDriver(IWebDriver passDriver)
        {
            ScenarioContext.Current["Driver"] = passDriver;
        }

        private IWebDriver GetWebDriver()
        {
            return ScenarioContext.Current["Driver"] as IWebDriver;
        }

        protected virtual void Drive(Action<IWebDriver> test)
        {
            var passDriver = GetWebDriver();
            test(passDriver);
            SetWebDriver(passDriver);
        }

        protected void LogoutOfSitefinity()
        {
            By logoutLink = By.LinkText("Logout");
            var sitefinityWindow = Driver.WindowHandles.FirstOrDefault(h => h != Driver.CurrentWindowHandle);
            if (sitefinityWindow == null)
            {
                return;
            }
            Driver.SwitchTo().Window(sitefinityWindow);

            IWebElement people = Driver.FindElement(logoutLink);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("arguments[0].click();", people);
        }

        protected void Teardown()
        {
            Driver.Quit();
        }
    }
}