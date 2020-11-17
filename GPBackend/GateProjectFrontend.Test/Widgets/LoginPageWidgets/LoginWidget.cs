using GateProjectFrontend.Test.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateProjectFrontend.Test.Widgets.LoginPageWidgets
{
    public class LoginWidget : BasePage
    {
        readonly private WebDriverWait Wait;
        public LoginWidget(IWebDriver driver) : base(driver)
        {
            this.Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
        }

        public IWebElement EmailInputElement => Driver.FindElement(By.Id("mat-input-0"));
        public IWebElement PasswordInputElement => Driver.FindElement(By.Id("mat-input-1"));
        public IWebElement LoginButtonElement => Driver.FindElement(By.CssSelector(".btn.btn-info.w-100.mb-2"));

        public MainPage Login()
        {
            LoginButtonElement?.Click();

            return new MainPage(Driver);
        }

        public void WaitForMainPage()
        {
            this.Wait.Until(d => d.Url.Contains("main"));
        }
    }
}
