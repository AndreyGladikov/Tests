using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;


namespace UnitTestProject1
{
    [TestClass]
    public class Task_20
    {
        private static String URL = "https://192.168.100.26/";
        private static IWebDriver driver;
        private string ExpectedURL = "https://192.168.100.26/Home/Index";

        public void Login(string username, string password)
        {
           driver.Navigate().GoToUrl(URL);
           driver.FindElement(By.CssSelector("input[id=Username]")).SendKeys(username);
           driver.FindElement(By.Id("Password")).SendKeys(password);
           driver.FindElement(By.TagName("button")).Click();
        }

        [TestInitialize]
        public void initialize()
        {
           driver = new ChromeDriver();
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void LoginTest()
        {
            Login("EugenBorisik", "qwerty12345");
            string CurrentURL = driver.Url;
            Assert.AreEqual(ExpectedURL, CurrentURL);
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void LogoutTest()
        {
            Login("EugenBorisik", "qwerty12345");
            driver.FindElement(By.ClassName("sign-out")).Click();
            string CurrentURL = driver.Url;
            Assert.AreEqual(URL, CurrentURL);
        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.Quit();
        }
    }
}
