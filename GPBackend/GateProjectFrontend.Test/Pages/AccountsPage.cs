using GateProjectFrontend.Test.Widgets.AccountsPageWidgets;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateProjectFrontend.Test.Pages
{
    public class AccountsPage : BasePage
    {
        public AccountsPage(IWebDriver driver) : base(driver)
        {

        }

        public AccountListWidget GetAccountListWidget()
        {
            return new AccountListWidget(Driver);
        }

        public CreateAccountWidget GetCreateAccountWidget()
        {
            return new CreateAccountWidget(Driver);
        }
    }
}
