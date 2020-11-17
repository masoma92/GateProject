using GateProjectFrontend.Test.Pages;
using GateProjectFrontend.Test.Widgets.LoginPageWidgets;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateProjectFrontend.Test.Tests
{
    public class LoginPageTests : TestBase
    {
        [Test]
        public void LoginActionTest()
        {
            LoginWidget loginWidget = LoginPage.Navigate(Driver).GetLoginWidget();

            LoginPage.MaximizeWindowSize(Driver);

            loginWidget.EmailInputElement.SendKeys(LoginPage.AdminEmail);
            loginWidget.PasswordInputElement.SendKeys(LoginPage.AdminPassword);
            MainPage dashboardPage = loginWidget.Login();

            loginWidget.WaitForMainPage();

            Assert.AreEqual(dashboardPage.GetTitleWidget().TitleElement.Text, "Dashboard");
        }
    }
}
