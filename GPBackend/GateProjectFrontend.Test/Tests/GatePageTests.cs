

using GateProjectFrontend.Test.Pages;
using GateProjectFrontend.Test.Widgets;
using GateProjectFrontend.Test.Widgets.DashboardPageWidgets;
using GateProjectFrontend.Test.Widgets.GatesPageWidgets;
using GateProjectFrontend.Test.Widgets.LoginPageWidgets;
using NUnit.Framework;

namespace GateProjectFrontend.Test.Tests
{
    public class GatePageTests : TestBase
    {
        [Test]
        public void GateCreatedByAdminIsNotVisibleByUser()
        {
            LoginWidget loginWidget = LoginPage.Navigate(Driver).GetLoginWidget();

            LoginPage.MaximizeWindowSize(Driver);

            loginWidget.EmailInputElement.SendKeys(LoginPage.AdminEmail);
            loginWidget.PasswordInputElement.SendKeys(LoginPage.AdminPassword);
            MainPage mainPage = loginWidget.Login();

            loginWidget.WaitForMainPage();

            NavbarWidget navbarWidget = mainPage.GetNavbarWidget();

            GatesPage gatesPage = navbarWidget.NavigateToGates();
            navbarWidget.WaitForGatePage();

            GateListWidget gateListWidget = gatesPage.GetGateListWidget();

            gateListWidget.CreateGate();
            gateListWidget.WaitCreateGateWidget();

            CreateGateWidget createGateWidget = gatesPage.GetCreateGateWidget();

            createGateWidget.NameInputElement.SendKeys("test");
            createGateWidget.Create();

            TitleWidget titleWidget = mainPage.GetTitleWidget();
            titleWidget.Logout();

            loginWidget.EmailInputElement.SendKeys(LoginPage.UserEmail);
            loginWidget.PasswordInputElement.SendKeys(LoginPage.UserPassword);
            loginWidget.Login();
            loginWidget.WaitForMainPage();

            navbarWidget.NavigateToGates();

            var nameList = gateListWidget.GetAllTableNameData();

            CollectionAssert.DoesNotContain(nameList, "test");

        }
    }
}
