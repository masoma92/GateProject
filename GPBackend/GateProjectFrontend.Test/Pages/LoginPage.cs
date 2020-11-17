using GateProjectFrontend.Test.Widgets.LoginPageWidgets;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateProjectFrontend.Test.Pages
{
    public class LoginPage : BasePage
    {
        public static string AdminEmail = "soma.makai@gmail.com";
        public static string AdminPassword = "alma";
        public static string UserEmail = "user@andras.com";
        public static string UserPassword = "asd";

        public LoginPage(IWebDriver driver) : base(driver)
        {

        }

        public static LoginPage Navigate(IWebDriver webDriver)
        {
            webDriver.Url = "https://gateproject-79e4b.web.app/";

            return new LoginPage(webDriver);
        }

        public LoginWidget GetLoginWidget()
        {
            return new LoginWidget(Driver);
        }

        public static void MaximizeWindowSize(IWebDriver webDriver)
        {
            webDriver.Manage().Window.Maximize();
        }
    }
}
