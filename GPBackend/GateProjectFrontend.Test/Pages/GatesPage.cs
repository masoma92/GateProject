using GateProjectFrontend.Test.Widgets.GatesPageWidgets;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateProjectFrontend.Test.Pages
{
    public class GatesPage : BasePage
    {
        public GatesPage(IWebDriver driver) : base(driver)
        {

        }

        public GateListWidget GetGateListWidget()
        {
            return new GateListWidget(Driver);
        }

        public CreateGateWidget GetCreateGateWidget()
        {
            return new CreateGateWidget(Driver);
        }
    }
}
