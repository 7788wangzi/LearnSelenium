using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QA = OpenQA.Selenium;
using UI = OpenQA.Selenium.Support.UI;
using RM = OpenQA.Selenium.Remote;

namespace Demo
{
    public class SeleniumGrid
    {
        RM.DesiredCapabilities _capabilities = null;
        QA.IWebDriver _webDriver = null;
        public SeleniumGrid(Browser browser)
        {
            switch (browser)
            {
                case Browser.IE: { _capabilities = RM.DesiredCapabilities.InternetExplorer(); }; break;
                case Browser.Chrome: { _capabilities = RM.DesiredCapabilities.Chrome(); }; break;
                case Browser.Firefox: { _capabilities = RM.DesiredCapabilities.Firefox(); }; break;
                case Browser.Safari: { _capabilities = RM.DesiredCapabilities.Safari(); }; break;
                case Browser.PhantomJS: { _capabilities = RM.DesiredCapabilities.PhantomJS(); }; break;
            }
            _webDriver = new RM.RemoteWebDriver(new Uri("http://157.60.50.40:6666"),_capabilities,TimeSpan.FromSeconds(10));
        }

        public void Test()
        {
            _webDriver.Navigate().GoToUrl("http://www.baidu.com");
            UI.WebDriverWait _wait = new UI.WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            QA.IWebElement qBox = _wait.Until<QA.IWebElement>((d) => { return d.FindElement(QA.By.Id("kw")); });
            qBox.SendKeys("Qijie Xue");
            QA.IWebElement sBtn = _wait.Until<QA.IWebElement>((d) => { return d.FindElement(QA.By.Id("su")); });
            sBtn.Click();

            _wait.Until((d)=>{return d.Title.ToLower().StartsWith("qijie");});

        }

    }

    public enum Browser
    {
        IE,
        Firefox,
        Chrome,
        Safari,
        PhantomJS
    }
}
