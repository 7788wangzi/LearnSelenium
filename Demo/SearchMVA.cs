using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SIE = OpenQA.Selenium.IE;
using Se = OpenQA.Selenium;
using UI = OpenQA.Selenium.Support.UI;

namespace Demo
{
    public class SearchMVA
    {
        public SearchMVA() { }
        public void Test()
        {
            SIE.InternetExplorerOptions _ieOptions = new SIE.InternetExplorerOptions();
            _ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            // open web page in IE
            OpenQA.Selenium.IE.InternetExplorerDriver _IEDriver = new OpenQA.Selenium.IE.InternetExplorerDriver(_ieOptions);
            _IEDriver.Navigate().GoToUrl(@"http://www.microsoftvirtualacademy.com");


            // span class ="selected-country popupCountriesTrigger"
            IWebElement selectedCountry = _IEDriver.FindElement(By.XPath(@"//span[@class='selected-country popupCountriesTrigger']"));
            if (selectedCountry != null)
            {
                selectedCountry.Click();
            }

            // select country of China
            IWebElement chinaLink = null;
            //IList<IWebElement> countryLinks = _IEDriver.FindElements(By.XPath(@"//a[@class='link']"));
            //foreach (var item in countryLinks)
            //{
            //    if (item.Text.Trim().ToLower() == "china")
            //    {
            //        chinaLink = item;
            //        break;
            //    }
            //}
            UI.WebDriverWait _wait = new UI.WebDriverWait(_IEDriver, TimeSpan.FromSeconds(60));
            chinaLink = _wait.Until<IWebElement>((d) =>
                {
                    return d.FindElement(By.LinkText("China"));
                });

            //chinaLink = _IEDriver.FindElement(By.LinkText("China"));
            if (chinaLink != null)
            {
                chinaLink.Click();
            }

            UI.WebDriverWait _webDriverWait = new UI.WebDriverWait(_IEDriver, TimeSpan.FromSeconds(100));
            _webDriverWait.Until((d) => { return d.Title.ToLower().StartsWith("微软虚拟学院"); });

            // find the search box
            IWebElement searchBox = null;
            IWebElement searchButton = null;
            searchBox = _IEDriver.FindElement(By.Name(@"ctl00$ctl00$SideBarContentPlaceHolder$ucQuickSearchBox$txtQuery"));
            searchButton = _IEDriver.FindElement(By.XPath(@"//div[@title='搜索']"));
            if (searchBox != null)
            {
                searchBox.SendKeys("SharePoint");
            }
            if (searchButton != null)
            {
                searchButton.Click();
            }

            // verify search result "Tuning SQL Server 2012 for SharePoint 2013 Jump Start"
            bool pass = false;
            string expectedCourse = "Tuning SQL Server 2012 for SharePoint 2013 Jump Start";
            IList<IWebElement> CourseLinks = _IEDriver.FindElements(By.XPath(@"//a/h2"));
            if (CourseLinks.Count != 0)
            {
                foreach (var item in CourseLinks)
                {
                    if (item.Text.Trim().ToLower() == expectedCourse.ToLower())
                    {
                        pass = true;
                        break;
                    }
                }
            }

            // scroll
            var js = "document.documentElement.scrollTop=10000";
            _IEDriver.ExecuteScript(js, null);

            Console.WriteLine(string.Format("Test Case status-{0}", pass));
            // cookie
            IList<Cookie> allCookies = _IEDriver.Manage().Cookies.AllCookies;
            int i = 0;
            StringBuilder stringTH = new StringBuilder();
            foreach (var item in allCookies)
            {
                string name = item.Name.ToString();
                string value = item.Value.ToString();
                stringTH.AppendLine(string.Format("{0} -{1}={2}", i++, name, value));
            }
            System.Windows.Forms.MessageBox.Show(stringTH.ToString());
            // clean up
            _IEDriver.Quit();
        }
    }
}
