using System;
using OpenQA.Selenium;

namespace MyNintendoProject.Page_Objects
{
    internal class HomePage : BasePage
    {
        public HomePage(IWebDriver Driver) : base(Driver)
        {

        }

        internal void VisitHomepage()
        {
            Visit("/");
        }

        internal void ClickSignIn()
        {
            By signInButton = By.ClassName("signInButton");
            WaitUntilDisplayed(signInButton, WAIT_SECONDS);
            Click(signInButton);
        }

        internal void ClickNintendoNetworkID()
        {
            By NNIDsignInButton = By.CssSelector(".SnsButtons_buttonNnid button");
            WaitUntilDisplayed(NNIDsignInButton, WAIT_SECONDS);
            Click(NNIDsignInButton);
        }

        internal void EnterLoginCredentials()
        {
            By usernameField = By.Name("username");
            By passwordField = By.Name("password");
            By signInButton = By.Id("btn_text");
            string username = "James3Hall";
            string password = "Bn9st6Cq!";
            WaitUntilDisplayed(usernameField, WAIT_SECONDS);
            Type(username, Find(usernameField));
            Type(password, Find(passwordField));
            Click(signInButton);
        }
    }
}