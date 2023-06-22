using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumCalculatorTests
{
    public class CalculatorTests
    {
        private AppiumLocalService appiumLocalService;
        private const string appiumServer = "http://127.0.0.1:4723/wd/hub";
        private const string applocation = @"C:\Users\NSD\Desktop\QA Automation Front-End - март 2023\7.Appium Desktop Testing\SummatorDesktopApp.exe";
        private WindowsDriver<WindowsElement> driver;
        private AppiumOptions appiumOptions;

        [OneTimeSetUp]
        public void OpenApplication()
        {
            /////////Start Appium using Desctop Appium Server
            // appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            // appiumLocalService.Start();
            //// this.appiumOptions = new AppiumOptions() { PlatformName= "Windows" };
            //  appiumOptions.AddAdditionalCapability("app", applocation);
            // appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, applocation);
            //appiumOptions.AddAdditionalCapability("PlatformName", "Windows");
            //this.driver = new WindowsDriver<WindowsElement> (appiumLocalService, appiumOptions);


            //////Start Appium using headless mode
            this.appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            appiumLocalService.Start(); 
            this.appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", applocation);
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.App, applocation);
            this.driver = new WindowsDriver<WindowsElement> (new Uri(appiumServer), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }
        [OneTimeTearDown]
        public void CloseApplication()
        {
           // driver.CloseApp();
            driver.Quit();
            appiumLocalService.Dispose();
        }
        [Test]
        public void Test_Sum_TwoPossitiveNumbers()
        {
            var firstField = driver.FindElementByAccessibilityId("textBoxFirstNum");
            var secondField = driver.FindElementByAccessibilityId("textBoxSecondNum");
            var resultField = driver.FindElementByAccessibilityId("textBoxSum");
            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");

            firstField.Clear();
            secondField.Clear();
            firstField.SendKeys("5");
            secondField.SendKeys("15");
            calcButton.Click();

            Assert.That(resultField.Text, Is.EqualTo("20"));
        }
        [Test]
        public void Test_Sum_TwoInvalidNumbers()
        {
            var firstField = driver.FindElementByAccessibilityId("textBoxFirstNum");
            var secondField = driver.FindElementByAccessibilityId("textBoxSecondNum");
            var resultField = driver.FindElementByAccessibilityId("textBoxSum");
            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");

            firstField.Clear();
            secondField.Clear();
            firstField.SendKeys("alabala");
            secondField.SendKeys("15");
            calcButton.Click();

            Assert.That(resultField.Text, Is.EqualTo("error"));
        }
        [TestCase("5","15","20")]
        [TestCase("5","10","15")]
        [TestCase("5","alabala","error")]
        public void Test_Sum_TwoValidNumbers(string firstValue, string secondValue, string result)
        {
            var firstField = driver.FindElementByAccessibilityId("textBoxFirstNum");
            var secondField = driver.FindElementByAccessibilityId("textBoxSecondNum");
            var resultField = driver.FindElementByAccessibilityId("textBoxSum");
            var calcButton = driver.FindElementByAccessibilityId("buttonCalc");

            firstField.Clear();
            secondField.Clear();
            firstField.SendKeys(firstValue);
            secondField.SendKeys(secondValue);
            calcButton.Click();

            Assert.That(resultField.Text, Is.EqualTo(result));
        }
    }
}