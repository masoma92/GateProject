using GateProjectFrontend.Test.Widgets;
using GateProjectFrontend.Test.Widgets.DashboardPageWidgets;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateProjectFrontend.Test.Pages
{
    public class MainPage : BasePage
    {
        public MainPage(IWebDriver driver) : base(driver)
        {

        }

        public TitleWidget GetTitleWidget()
        {
            return new TitleWidget(Driver);
        }

        public NavbarWidget GetNavbarWidget()
        {
            return new NavbarWidget(Driver);
        }
    }
}
