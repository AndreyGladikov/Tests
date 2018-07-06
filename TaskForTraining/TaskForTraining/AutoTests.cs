using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class Task_20
    {
        private static string URL = "https://192.168.100.26/";
        private static string FrameUrl = "https://the-internet.herokuapp.com/iframe";
        private static string AlertUrl = "https://the-internet.herokuapp.com/javascript_alerts";
        private static string KeyCode = Convert.ToString('\u0002');
        private static string AlertMSG = "I am a JS Alert";
        private static IWebDriver driver;
        private string ExpectedURL = "https://192.168.100.26/Home/Index";
        private string OfficeTabURL = "https://192.168.100.26/Office/Index";

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
            driver = new ChromeDriver(@"..\..\..\TaskForTraining\Drivers\"); // почему-то он ищет мой хромдрайвер в папке с деплоями, а его там нету, так что я придумал только такой путь.
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void LoginTest()
        {
            Login("EugenBorisik", "qwerty12345");
            string CurrentURL = driver.Url;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("sign-out")));
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

        [TestMethod]
        [TestCategory("Chrome")]
        public void LoginTestWithWaiter()
        {
            Login("EugenBorisik", "qwerty12345");
            string CurrentURL = driver.Url;
            Assert.AreEqual(ExpectedURL, CurrentURL);
            driver.FindElement(By.Id("officeMenu")).Click();
            string CurrentOfficeURL = driver.Url;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.PollingInterval = TimeSpan.FromMilliseconds(2700);
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("input-search")));
            IWebElement Search = driver.FindElement(By.Id("input-search"));
            Assert.IsTrue(Search.Displayed, "Search input is not displayed!");
        }

        [TestMethod]
        [DeploymentItem(@"TestData\TestData.csv")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "TestData.csv", "TestData#csv", DataAccessMethod.Sequential)]
        public void DDT_LoginTest()
        {
            string UName = TestContext.DataRow["UserName"].ToString();
            string PWD = TestContext.DataRow["Password"].ToString();
            Login(UName, PWD);
            string CurrentURL = driver.Url;
            Assert.AreEqual(ExpectedURL, CurrentURL);
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void FrameTest()
        {
            driver.Navigate().GoToUrl(FrameUrl);
            IWebElement FrameLoc = driver.FindElement(By.Id("mce_0_ifr"));
            driver.SwitchTo().Frame(FrameLoc);
            driver.FindElement(By.Id("tinymce")).Clear();
            driver.FindElement(By.Id("tinymce")).SendKeys("Hello ");
            driver.FindElement(By.Id("tinymce")).SendKeys(KeyCode);
            driver.FindElement(By.Id("tinymce")).SendKeys("World!");
            driver.FindElement(By.Id("tinymce")).SendKeys(KeyCode);
            string CurText = driver.FindElement(By.TagName("p")).Text;
            Assert.IsTrue(CurText.Contains("World!") && CurText.Contains("Hello"),"No current text on the page");
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void JSAlert()
        {
            driver.Navigate().GoToUrl(AlertUrl);
            driver.FindElement(By.XPath("//button[@onclick='jsAlert()']")).Click();
            string AlertText = driver.SwitchTo().Alert().Text;
            Assert.AreEqual(AlertText, AlertMSG);
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void JSConfirmCancel()
        {
            driver.Navigate().GoToUrl(AlertUrl);
            driver.FindElement(By.XPath("//button[@onclick='jsConfirm()']")).Click();
            driver.SwitchTo().Alert().Dismiss();
            string DismissResult = driver.FindElement(By.Id("result")).Text;
            Assert.AreEqual(DismissResult, "You clicked: Cancel");
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void JSConfirmOk()
        {
            driver.FindElement(By.XPath("//button[@onclick='jsConfirm()']")).Click();
            driver.SwitchTo().Alert().Accept();
            string AcceptResult = driver.FindElement(By.Id("result")).Text;
            Assert.AreEqual(AcceptResult, "You clicked: Ok");
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void JSPromptNull()
        {
            driver.Navigate().GoToUrl(AlertUrl);
            driver.FindElement(By.XPath("//button[@onclick='jsPrompt()']")).Click();
            driver.SwitchTo().Alert().SendKeys("Test Message");
            driver.SwitchTo().Alert().Dismiss();
            string DismissResult = driver.FindElement(By.Id("result")).Text;
            Assert.AreEqual(DismissResult, "You entered: null");
        }

        [TestMethod]
        [TestCategory("Chrome")]
        public void JSPromptMessage()
        {
            driver.FindElement(By.XPath("//button[@onclick='jsPrompt()']")).Click();
            driver.SwitchTo().Alert().SendKeys("Test Message");
            driver.SwitchTo().Alert().Accept();
            string AcceptResult = driver.FindElement(By.Id("result")).Text;
            Assert.AreEqual(AcceptResult, "You entered: Test Message");
        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.Quit();
        }
    }
}
