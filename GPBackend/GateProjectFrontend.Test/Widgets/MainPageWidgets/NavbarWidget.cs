using GateProjectFrontend.Test.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateProjectFrontend.Test.Widgets
{
    public class NavbarWidget : BasePage
    {
        readonly private WebDriverWait Wait;
        public NavbarWidget(IWebDriver driver) : base(driver)
        {
            this.Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
        }

        public IWebElement AccountsNavigateElement => Driver.FindElement(By.XPath("//a[@href='/main/accounts']"));
        public IWebElement GatesNavigateElement => Driver.FindElement(By.XPath("//a[@href='/main/gates']"));

        public GatesPage NavigateToGates()
        {
            GatesNavigateElement?.Click();

            return new GatesPage(Driver);
        }

        public void WaitForGatePage()
        {
            this.Wait.Until(d => d.Url.Contains("gates"));
        }

        public AccountsPage NavigateToAccount()
        {
            AccountsNavigateElement?.Click();

            return new AccountsPage(Driver);
        }

        public void WaitForAccountPage()
        {
            this.Wait.Until(d => d.Url.Contains("accounts"));
        }
    }
}
