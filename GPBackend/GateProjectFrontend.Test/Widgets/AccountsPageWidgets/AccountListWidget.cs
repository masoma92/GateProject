using GateProjectFrontend.Test.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateProjectFrontend.Test.Widgets.AccountsPageWidgets
{
    public class AccountListWidget : BasePage
    {
        readonly private WebDriverWait Wait;
        public AccountListWidget(IWebDriver driver) : base(driver)
        {
            this.Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
        }

        public IWebElement CreateAccountElement => Driver.FindElement(By.CssSelector(".mat-mini-fab.mat-button-base.mat-primary.ng-star-inserted"));

        public void CreateAccount()
        {
            CreateAccountElement?.Click();
        }

        public void WaitCreateAccountWidget()
        {
            this.Wait.Until(d => d.FindElement(By.Id("mat-input-5")) != null);
        }
    }
}
