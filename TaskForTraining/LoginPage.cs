using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TaskForTraining.Pages
{
    class LoginPage
    {
        public const string MailLogURL = "https://www.tut.by/?trnd=47188";
        public const string MailURL = "https://www.tut.by/";
        public const string MailLog = "AutoTest92";
        public const string MailPWD = "Inq2020327";

        private IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
           this.driver = driver;
           PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//a[@data-target-popup='authorize-form']")]
        public IWebElement LoginPopUp { get; set; }
        [FindsBy(How = How.XPath, Using = "//div[@class='i-holder']//input[@type='text']")]
        public IWebElement LoginField { get; set; }
        [FindsBy(How = How.XPath, Using = "//input[@type='password']")]
        public IWebElement PasswordField { get; set; }
        [FindsBy(How = How.XPath, Using = "//input[@value='Войти']")]
        public IWebElement LoginButton { get; set; }

        public void goToPage()
        {
            driver.Navigate().GoToUrl(MailURL);
        }

        public void clickOnLoginPopUp()
        {
            LoginPopUp.Click();
        }

        public void enterUserName(string username)
        {
            LoginField.SendKeys(username);
        }

        public void enterPassword(string password)
        {
            PasswordField.SendKeys(password);
        }

        public void clickOnLoginButton()
        {
            LoginButton.Click();
        }
    }  
}

