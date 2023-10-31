using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Travel9
{
    [TestClass]
    public class TravelTest
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void SetUp()
        {
            _driver = new ChromeDriver();
        }

        [TestMethod]
        public void BlogCreation()
        {
            _driver.Navigate().GoToUrl("https://localhost:7146/Blog/Create");

            var Blog_Title = _driver.FindElement(By.Id("Blog_Title"));
            var Blog_Content = _driver.FindElement(By.Id("Blog_Content"));
            var Destination_Country = _driver.FindElement(By.Id("Destination_Country"));
            var Destination_City = _driver.FindElement(By.Id("Destination_City"));
            var ImageFile = _driver.FindElement(By.Id("ImageFile"));
            var Button = _driver.FindElement(By.Id("Create"));

            Blog_Title.SendKeys("trxt");
            Blog_Content.SendKeys("trxt");
            Destination_Country.SendKeys("trxt");
            Destination_City.SendKeys("trxt");
            ImageFile.SendKeys(@"Z:\ddd.jpg");

            Button.Click();
            var Link = _driver.FindElement(By.Id("Details"));
            Assert.IsNotNull(Link);
        }
        [TestMethod]
        public void TestLogin()
        {
            _driver.Navigate().GoToUrl("https://localhost:7146/Identity/Account/Login");

            var Blog_Email = _driver.FindElement(By.Id("Input_Email"));
            var Blog_Password = _driver.FindElement(By.Id("Input_Password"));
            var Blog_login = _driver.FindElement(By.Id("login-submit"));

            Blog_Email.SendKeys("fd@gmail.com");
            Blog_Password.SendKeys("Qwerty1!");
            Blog_login.Click();

            Assert.AreEqual("https://localhost:7146/", _driver.Url);
        }
        [TestCleanup]
        public void CleanUp()
        {
            _driver.Quit();
        }
    }
}