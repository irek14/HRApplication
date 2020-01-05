using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Xunit;

namespace HRApplication.UITests
{
    public class UITests
    {
        public class SignInTest
        {
            private readonly string URI = "https://localhost:44351/Api/LogIn";
            private readonly IWebDriver _driver;

            public SignInTest()
            {
                _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            }

            [Fact]
            public void GoToSignInPage()
            {
                _driver.Navigate().GoToUrl(URI);
                Thread.Sleep(5000);
                var title = _driver.Title;
                Assert.Contains("Sign", title);

            }

            [Fact]
            public void TestLogIn()
            {
                _driver.Navigate().GoToUrl(URI);
                Thread.Sleep(5000);

                IWebElement Login = _driver.FindElement(By.Id("logonIdentifier"));
                IWebElement Password =  _driver.FindElement(By.Id("password"));
                IWebElement button = _driver.FindElement(By.Id("next"));

                Login.SendKeys("tasege1087@seomail.top");
                Password.SendKeys("Test123!");

                button.Click();
                Thread.Sleep(5000);

                var url = _driver.Url;
                Assert.Contains("Application/Index", url);

            }

            [Fact]
            public void ApplyAJob()
            {
                _driver.Navigate().GoToUrl(URI);
                Thread.Sleep(5000);

                IWebElement Login = _driver.FindElement(By.Id("logonIdentifier"));
                IWebElement Password = _driver.FindElement(By.Id("password"));
                IWebElement button = _driver.FindElement(By.Id("next"));

                Login.SendKeys("tasege1087@seomail.top");
                Password.SendKeys("Test123!");

                button.Click();
                Thread.Sleep(5000);

                var detailsButton = _driver.FindElement(By.ClassName("btn"));
                detailsButton.Click();

                var apply = _driver.FindElement(By.Id("applyButton"));
                apply.Click();

                var input = _driver.FindElement(By.Id("fileInput"));

                Assert.True(input != null);

            }
        }
    }
}
