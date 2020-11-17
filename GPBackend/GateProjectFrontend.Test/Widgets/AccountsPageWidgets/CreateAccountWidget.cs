using GateProjectFrontend.Test.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateProjectFrontend.Test.Widgets.AccountsPageWidgets
{
    public class CreateAccountWidget : BasePage
    {
        readonly private WebDriverWait Wait;
        public CreateAccountWidget(IWebDriver driver) : base(driver)
        {
            this.Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
        }
        public IWebElement NameInputElement => Driver.FindElement(By.Id("mat-input-5"));
        public IWebElement ZipInputElement => Driver.FindElement(By.Id("mat-input-6"));
        public IWebElement CountryInputElement => Driver.FindElement(By.Id("mat-input-7"));
        public IWebElement CityInputElement => Driver.FindElement(By.Id("mat-input-8"));
        public IWebElement StreetInputElement => Driver.FindElement(By.Id("mat-input-9"));
        public IWebElement StreetNoInputElement => Driver.FindElement(By.Id("mat-input-10"));
        public IWebElement ContactEmailInputElement => Driver.FindElement(By.Id("mat-input-11")); 
        public IWebElement CreateButtonElement => Driver.FindElement(By.CssSelector(".btn.btn-info.w-100.mb-2"));

        public void Create()
        {
            CreateButtonElement?.Click();
            this.Wait.Until(d => d.FindElement(By.Id("mat-input-5")) == null);
        }
    }
}
