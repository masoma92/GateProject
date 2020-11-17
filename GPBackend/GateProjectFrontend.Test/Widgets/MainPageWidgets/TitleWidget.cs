using GateProjectFrontend.Test.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateProjectFrontend.Test.Widgets.DashboardPageWidgets
{
    public class TitleWidget : BasePage
    {
        readonly private WebDriverWait Wait;
        public TitleWidget(IWebDriver driver) : base(driver)
        {
            this.Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
        }
        public IWebElement TitleElement => Driver.FindElement(By.CssSelector(".navbar-brand"));
        public IWebElement LogoutElement => Driver.FindElement(By.XPath("//a[. = 'Logout']"));
        public IWebElement UserElement => Driver.FindElement(By.CssSelector(".fas.fa-user-cog"));

        public MainPage Logout()
        {
            UserElement?.Click();
            this.Wait.Until(d => d.FindElement(By.XPath("//a[. = 'Logout']")).Displayed);

            LogoutElement?.Click();

            return new MainPage(Driver);
        }

    }
}
