using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XWebDriver;
using OpenQA.Selenium;

namespace Demo
{
    public class Class1
    {
        public void Test()
        {
            OpenQA.Selenium.Chrome.ChromeDriver _chrDriver = new OpenQA.Selenium.Chrome.ChromeDriver();
            _chrDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            _chrDriver.Navigate().GoToUrl("http://www.baidu.com");

            OpenQA.Selenium.IWebElement textBox = (new OpenQA.Selenium.Support.UI.WebDriverWait(_chrDriver,TimeSpan.FromSeconds(10))
                .Until<OpenQA.Selenium.IWebElement>((d)=>{return d.FindElement(OpenQA.Selenium.By.Id("kw"));}));
            textBox.SendKeys("Hello");


            OpenQA.Selenium.IWebElement btn = (new OpenQA.Selenium.Support.UI.WebDriverWait(_chrDriver, TimeSpan.FromSeconds(10))
                .Until<OpenQA.Selenium.IWebElement>((d) => { return d.FindElement(OpenQA.Selenium.By.Id("su")); }));
            btn.Click();

            string expectedText = "百度为您找到相关结果约100,000,000个";
            OpenQA.Selenium.IWebElement divnums = (new OpenQA.Selenium.Support.UI.WebDriverWait(_chrDriver, TimeSpan.FromSeconds(10))
                .Until<OpenQA.Selenium.IWebElement>((d) => { return d.FindElement(OpenQA.Selenium.By.XPath("//div[@class='nums']")); }));
            Console.WriteLine(string.Format(@"Search Result is correct?: {0}",divnums.Text ==expectedText));
            var js = "alert('Is the result correct?" + (divnums.Text == expectedText).ToString() + "')";
            _chrDriver.ExecuteScript(js, null);
            //_chrDriver.Quit();
        }

        public void Test2()
        {
            OneDriver oDriver = new OneDriver(Browsers.Chrome);            
            oDriver.GoToUrl("http://www.baidu.com");
            IWebElement we = oDriver.FindElementById("kw");
            we.SendKeys("Qijie Xue");
            IWebElement btn = oDriver.FindElementById("su");
            btn.Click();
            //oDriver.ExplicitlyWait("qijie");
            oDriver.WaitForPageLoaded("qijie");
            oDriver.PageScrollToBottom();
            oDriver.ExecuteJS("alert('Finish')");
            oDriver.AlertAccept();

            oDriver.TakeScreenshot(@"D:\baidu.jpg");


            oDriver.Cleanup();
        }

        public void Test3()
        {
            OpenQA.Selenium.IE.InternetExplorerDriver _ieDriver = new OpenQA.Selenium.IE.InternetExplorerDriver(new OpenQA.Selenium.IE.InternetExplorerOptions()
            {
                IntroduceInstabilityByIgnoringProtectedModeSettings=true
            });
            _ieDriver.Navigate().GoToUrl("http://www.baidu.com");
            OpenQA.Selenium.Support.UI.WebDriverWait _wbWait = new OpenQA.Selenium.Support.UI.WebDriverWait(_ieDriver, TimeSpan.FromSeconds(10));
            IWebElement box = _wbWait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("kw"));
            });
            box.SendKeys("QijieXue");

            IWebElement btn = _wbWait.Until<IWebElement>((d) =>
            {
                return d.FindElement(By.Id("su"));
            });

            btn.Click();

            _wbWait.Until((d) =>
            {
                return d.Title.ToLower().StartsWith("qijie");
            });

            var js = "var q = document.documentElement.scrollTop=10000";
            _ieDriver.ExecuteScript(js, null);

            js = "alert('test finished')";
            _ieDriver.ExecuteScript(js, null);
        }
        
    }
}
