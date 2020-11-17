using GateProjectFrontend.Test.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateProjectFrontend.Test.Widgets.GatesPageWidgets
{
    public class CreateGateWidget : BasePage
    {
        readonly private WebDriverWait Wait;
        public CreateGateWidget(IWebDriver driver) : base(driver)
        {
            this.Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
        }

        public IWebElement NameInputElement => Driver.FindElement(By.Id("mat-input-5"));
        public IWebElement AccountInputElement => Driver.FindElement(By.Id("mat-input-6"));
        public IWebElement CreateButtonElement => Driver.FindElement(By.CssSelector(".btn.btn-info.w-100.mb-2"));

        public void Create()
        {
            //this.Wait.Until(d => d.FindElement(By.Id("mat-input-6")).Displayed);
            this.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
            CreateButtonElement?.Click();
            this.Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("mat-input-5")));
        }
    }
}
