using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium;
using AppiumCalculatorTestsPOM.Pages;
using NUnit.Framework;

namespace AppiumCalculatorTestsPOM.Tests
{
    public class BaseTest
    {
        private const string appiumServer = "http://127.0.0.1:4723/wd/hub";
        private const string applocation = @"C:\Users\NSD\Desktop\QA Automation Front-End - март 2023\7.Appium Desktop Testing\SummatorDesktopApp.exe";
        protected WindowsDriver<WindowsElement> driver;
        private AppiumOptions appiumOptions;

        [SetUp]
        public void Setup()
        {
            this.appiumOptions = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptions.AddAdditionalCapability("app", applocation);
            this.driver = new WindowsDriver<WindowsElement>(new Uri(appiumServer), appiumOptions);
        }

        [TearDown]
        public void CloseApp()
        {
            driver.Quit();
        }
    }
}
