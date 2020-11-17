using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateProjectFrontend.Test.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver;

        public BasePage(IWebDriver webDriver)
        {
            Driver = webDriver;
        }
    }
}
