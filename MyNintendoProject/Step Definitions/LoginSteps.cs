using MyNintendoProject.Page_Objects;
using TechTalk.SpecFlow;

namespace MyNintendoProject.Step_Definitions
{
    [Binding]
    class LoginSteps : BaseSteps
    {
        [BeforeScenario("Login")]
        public void BeforeLoginScenario()
        {
            LoadConfigValues();
            CheckBrowser();
        }

        [AfterScenario("Login")]
        public void AfterLoginScenario()
        {
            Teardown();
        }

        [Given(@"I am on the My Nintendo site")]
        public void GivenIAmOnTheMyNintendoSite()
        {
            HomePage homePage = new HomePage(Driver);
            homePage.VisitHomepage();
        }

        [When(@"I sign in")]
        public void WhenISignIn()
        {
            HomePage homePage = new HomePage(Driver);
            homePage.ClickSignIn();
            homePage.ClickNintendoNetworkID();
            homePage.EnterLoginCredentials();
        }

        [Then(@"I am logged into my account")]
        public void ThenIAmLoggedIntoMyAccount()
        {
            ProfilePage profilePage = new ProfilePage(Driver);
            profilePage.ValidateLogin();
        }

        [Then(@"I can collect my Rewards")]
        public void ThenICanCollectMyRewards()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
