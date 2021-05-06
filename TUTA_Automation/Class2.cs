using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TUTA_Automation
{
    public class Homework
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "http://automationpractice.com/index.php";
        }

        public void Login(string email, string password)
        {
            driver.FindElement(By.CssSelector("#header > div.nav > div > div > nav > div.header_user_info > a")).Click();
            driver.FindElement(By.Id("email")).SendKeys(email);
            driver.FindElement(By.Id("passwd")).SendKeys(password);
            driver.FindElement(By.Id("SubmitLogin")).Click();
        }

        [Test]
        public void TestSignIn()
        {
            // 1.	Sign in to the account
            Login("goda.bielskiene@gmail.com", "godagoda");

            //2.	Validate correct sign in
            IWebElement accountInfo = driver.FindElement(By.CssSelector("#header > div.nav > div > div > nav > div:nth-child(1) > a > span"));
            Assert.AreEqual("Goda Bielskiene", accountInfo.Text, "Account info is incorrect");
        }

        [Test]
        public void TestSearch()
        {
            // Log in
            Login("goda.bielskiene@gmail.com", "godagoda");

            // 3.	Search for an item in the shop
            driver.FindElement(By.Id("search_query_top")).SendKeys("t-shirt");
            driver.FindElement(By.Name("submit_search")).Click();

            // 4.	Validate that it finds the item
            IWebElement searchResult = driver.FindElement(By.CssSelector("#center_column > h1 > span.heading-counter"));
            Assert.AreEqual("1 result has been found.", searchResult.Text, "Search failed");
         }
        
        [Test]
        public void TestPurchase()
        {
            // Log in
            Login("goda.bielskiene@gmail.com", "godagoda");

            //Search for an item
            driver.FindElement(By.Id("search_query_top")).SendKeys("t-shirt");
            driver.FindElement(By.Name("submit_search")).Click();

            // 5.	Finish buying the item
            // More on an item
            IWebElement Image = driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div[2]/ul/li/div/div[1]/div/a[1]/img"));
            IWebElement MoreButton = driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div[2]/ul/li/div/div[2]/div[2]/a[2]/span"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(Image).MoveToElement(MoreButton).Click().Perform();

            //Select size 
            IWebElement Sizedropdown = driver.FindElement(By.XPath("//*[@id='group_1']"));
            SelectElement size = new SelectElement(Sizedropdown);
            size.SelectByText("M");

            //Select Color
            driver.FindElement(By.Id("color_14")).Click();

            //Click on add to cart
            driver.FindElement(By.XPath("//p[@id='add_to_cart']//span[.='Add to cart']")).Click();

            // Cart - Checkout         
            driver.FindElement(By.XPath("/html/body/div/div[1]/header/div[3]/div/div/div[3]/div/a/b")).Click();
            IWebElement Cart = driver.FindElement(By.XPath("/html/body/div/div[1]/header/div[3]/div/div/div[3]/div/a"));
            IWebElement Checkout = driver.FindElement(By.XPath("/html/body/div/div[1]/header/div[3]/div/div/div[3]/div/div/div/div/p[2]/a/span"));
            Actions act = new Actions(driver);
            act.MoveToElement(Cart).MoveToElement(Checkout).Click().Perform();

            // Proceed to Checkout 
            driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div/p[2]/a[1]/span")).Click();
            driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div/form/p/button/span")).Click();

            //Agree to Terms of service and Proceed to Checkout
            driver.FindElement(By.Id("cgv")).Click();
            driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div/div/form/p/button/span")).Click();

            //Select Pay by Check
            driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div/div/div[3]/div[2]/div/p/a")).Click();

            //Confirm my Order
            driver.FindElement(By.XPath("/html/body/div/div[2]/div/div[3]/div/form/p/button/span")).Click();

            //6.	Validate that the order is completed
            IWebElement searchResult = driver.FindElement(By.CssSelector("#center_column > p.alert.alert-success"));
            Assert.AreEqual("Your order on My Store is complete.", searchResult.Text, "Order is not completed");
        }
      
        [TearDown]
        public void CloseBrowser()
        {
            //driver.Close();
        }
    }
}
