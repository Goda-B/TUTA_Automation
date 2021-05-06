using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TUTA_Automation
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "http://automationpractice.com/index.php";
        }

        [Test]
        public void TestLogin()
        {
            /*
            driver.FindElement(By.CssSelector("#header > div.nav > div > div > nav > div.header_user_info > a")).Click();

            driver.FindElement(By.Id("email")).SendKeys("goda.bielskiene@gmail.com");
            driver.FindElement(By.Id("passwd")).SendKeys("godagoda");
            driver.FindElement(By.Id("SubmitLogin")).Click();
            */
            Login("goda.bielskiene@gmail.com", "godagoda");

            IWebElement accountInfo = driver.FindElement(By.CssSelector("#header > div.nav > div > div > nav > div:nth-child(1) > a > span"));

            Assert.AreEqual("Goda Bielskiene", accountInfo.Text, "Account info is incorrect");
        }

        [Test]
        public void TestIncorrectLogin()
        {
            /*driver.FindElement(By.CssSelector("#header > div.nav > div > div > nav > div.header_user_info > a")).Click();

            driver.FindElement(By.Id("email")).SendKeys("goda.bielskiene@gmail.com");
            driver.FindElement(By.Id("passwd")).SendKeys("goda");
            driver.FindElement(By.Id("SubmitLogin")).Click();
            */

            Login("goda.bielskiene@gmail.com", "goda");

            IWebElement accountInfo = driver.FindElement(By.CssSelector("#center_column > div.alert.alert-danger > p"));

            Assert.AreEqual("There is 1 error", accountInfo.Text, "Account info is incorrect");
        }

        public void Login(string email, string password)
        {
            driver.FindElement(By.CssSelector("#header > div.nav > div > div > nav > div.header_user_info > a")).Click();

            driver.FindElement(By.Id("email")).SendKeys(email);
            driver.FindElement(By.Id("passwd")).SendKeys(password);
            driver.FindElement(By.Id("SubmitLogin")).Click();
        }

        [TearDown]
        public void CloseBrowser()
        {
            //driver.Close();
        }
    }
}