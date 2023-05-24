using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TDTProject.Tests
{
    public class LoginTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://regatuljocurilor.ro/ro/autentificare");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void ValidEmailAndPassword_LoginSuccessful()
        {
            string email = "validemail@example.com";
            string password = "validpassword";

            Login(email, password);

            // Assert that login was successful
            Assert.That(GetIsLoggedIn(), Is.False, "Login was not successful.");
        }

        [Test]
        public void ValidEmailAndInvalidPassword_LoginFailed()
        {
            string email = "validemail@example.com";
            string password = "invalidpassword";

            Login(email, password);

            // Assert that login failed
            Assert.That(GetIsLoggedIn(), Is.False, "Login was expected to fail.");
        }

        [Test]
        public void InvalidEmailAndValidPassword_LoginFailed()
        {
            string email = "invalidemail";
            string password = "validpassword";

            Login(email, password);

            // Assert that login failed
            Assert.That(GetIsLoggedIn(), Is.False, "Login was expected to fail.");
        }

        [Test]
        public void InvalidEmailAndInvalidPassword_LoginFailed()
        {
            string email = "invalidemail";
            string password = "invalidpassword";

            Login(email, password);

            // Assert that login failed
            Assert.That(GetIsLoggedIn(), Is.False, "Login was expected to fail.");
        }

        private void Login(string email, string password)
        {
            IWebElement emailField = driver.FindElement(By.CssSelector("input[name='email']"));
            emailField.SendKeys(email);

            IWebElement passwordField = driver.FindElement(By.CssSelector("input[name='password']"));
            passwordField.SendKeys(password);

            IWebElement loginButton = driver.FindElement(By.CssSelector("button[data-link-action='sign-in']"));
            loginButton.Click();
        }


        private bool GetIsLoggedIn()
        {
            string expectedUrl = "https://regatuljocurilor.ro/ro/contul-meu";
            string currentUrl = driver.Url;

            return currentUrl.StartsWith(expectedUrl);
        }
    }
}
