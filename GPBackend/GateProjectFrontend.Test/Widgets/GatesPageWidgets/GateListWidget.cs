using GateProjectFrontend.Test.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateProjectFrontend.Test.Widgets.GatesPageWidgets
{
    public class GateListWidget : BasePage
    {
        readonly private WebDriverWait Wait;
        public GateListWidget(IWebDriver driver) : base(driver)
        {
            this.Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(25));
        }

        public IWebElement CreateGateElement => Driver.FindElement(By.CssSelector(".mat-mini-fab.mat-button-base.mat-primary.ng-star-inserted"));
        public IList<IWebElement> NameRows => Driver.FindElements(By.CssSelector(".mat-cell.cdk-column-name.mat-column-name.ng-star-inserted"));

        public void CreateGate()
        {
            CreateGateElement?.Click();
        }

        public IEnumerable<string> GetAllTableNameData()
        {
            foreach (var item in NameRows)
            {
                yield return item.Text.Trim();
            }
        }

        public void WaitCreateGateWidget()
        {
            this.Wait.Until(d => d.FindElement(By.Id("mat-input-5")) != null);
        }

    }
}
