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
        public void UserCanLogin()
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

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}