using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TaskForTraining.Pages
{
    class HomePage
    {
        private IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public const string UName = "Test Auto";

        [FindsBy(How = How.XPath, Using = "//span[@class='uname']")]
        public IWebElement UserName { get; set; }
        [FindsBy(How = How.XPath, Using = "//a[@class='button wide auth__reg']")]
        public IWebElement LogOutBTN { get; set; }

        public void clickOnUserName()
        {
            UserName.Click();
        }

        public void clickOnLogOutBTN()
        {
            LogOutBTN.Click();
        }
    }
}
