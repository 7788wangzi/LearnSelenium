using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Se = OpenQA.Selenium;
using Chr = OpenQA.Selenium.Chrome;
using Fir = OpenQA.Selenium.Firefox;

namespace Demo
{
    class MutiBrowser
    {
        public static void TryChrome()
        {
            Chr.ChromeDriver _chromeDriver = new Chr.ChromeDriver();
            _chromeDriver.Navigate().GoToUrl("http://www.baidu.com");
            Se.Screenshot _screenShot = _chromeDriver.GetScreenshot();
            _screenShot.SaveAsFile(@"D:\chrome.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            _chromeDriver.Quit();
        }

        public static void TryFirefox()
        {
            Fir.FirefoxDriver _fireDriver = new Fir.FirefoxDriver();
            _fireDriver.Navigate().GoToUrl("http://www.baidu.com");
            Se.Screenshot _screenShot = _fireDriver.GetScreenshot();
            _screenShot.SaveAsFile(@"D:\Firefox.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            _fireDriver.Quit();
        }
    }
}
