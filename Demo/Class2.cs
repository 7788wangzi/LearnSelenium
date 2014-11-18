using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using XWebDriver;

namespace Demo
{
    class Class2
    {
        OneDriver odriver = null;

        public Class2()
        {
            odriver = new OneDriver(Browsers.IE);
        }

        public void GoToMVA()
        {
            string url = @"http://www.microsoftvirtualacademy.com/";
            odriver.GoToUrl(url);

            // set location
            IWebElement spanLocation=odriver.FindElementByXPath(@"//span[@class='selected-country popupCountriesTrigger']");
            odriver.ClickElement(spanLocation);

            string idOfChinaText = "ctl00_ctl00_ucCountries_dlCountries_ctl33_LinkButton1";
            odriver.WaitForElementFound(idOfChinaText);
            IWebElement chinaLink = odriver.FindElementById(idOfChinaText);
            odriver.ClickElement(chinaLink);

            odriver.WaitForPageLoaded("微软虚拟学院");

            // Search by keywords
            string keyWords = "SharePoint";
            string idOfSearchBox = "ctl00_ctl00_SideBarContentPlaceHolder_ucQuickSearchBox_txtQuery";
            odriver.WaitForElementFound(idOfSearchBox);
            IWebElement searchBox = odriver.FindElementById(idOfSearchBox);
            odriver.SendKeysToElement(searchBox, keyWords);

            IWebElement searchBtn = odriver.FindElementByXPath(@"//div[@class='search-box-query']/div[@class='search-box-image']");
            odriver.ClickElement(searchBtn);

            odriver.WaitForPageLoaded("Windows");

            // verify keywords passed
            string idOfSearchNew = "ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder1_txtQuery";
            IWebElement searchBox1 = odriver.FindElementById(idOfSearchNew);
            string text = searchBox1.Text;
            if (text == keyWords) { 
            // pass
                var js = "alert('keywords passed to new page = True')";
                odriver.ExecuteJS(js);
                //odriver.AlertAccept();
            }

            // get results
            string expectedCourse = "Developing Microsoft SharePoint Server 2013 Core Solutions Jump Start";
            IList<IWebElement> coursesInResults = odriver.FindElementsByXPathName(@"//div[@class='title truncable']/a/h2");
            foreach (var item in coursesInResults)
            {
                if (item.Text.ToLower() == expectedCourse.ToLower())
                {
                    // pass
                    var js = "alert('Epected course is listed in results')";
                    odriver.ExecuteJS(js);

                    item.Click();
                    break;
                }
            }

            // logon
            odriver.WaitForPageLoaded("SharePoint");

            string idOfLogon = "ctl00_ucHeader_lnkLogin";
            odriver.WaitForElementFound(idOfLogon);
            IWebElement logonElement = odriver.FindElementById(idOfLogon);
            odriver.ClickElement(logonElement);

            odriver.WaitForPageLoaded("Sign in");
            // logon boxes

            //IWebElement form = odriver.FindElementByXPath("//div[@id='rightTD']/form");
            //odriver.GoToFrame(form);
            string nameOfEmail = "//input[@name='login']";
            string nameOfPwd = "//input[@name='passwd']";
            string nameOfSign = "//input[@name='SI']";
            IWebElement userBox = odriver.FindElementByXPath("//input[@name='login']");
            IWebElement pwdBox = odriver.FindElementByXPath("//input[@name='passwd']");
            IWebElement signBtn = odriver.FindElementByXPath(nameOfSign);

            odriver.SendKeysToElement(userBox, "skytest@outlook.com");
            odriver.SendKeysToElement(pwdBox, "");
            odriver.ClickElement(signBtn);

            //
            odriver.WaitForPageLoaded("SharePoint");


        }

        public void TestLogon()
        {
            string url = "https://login.live.com/login.srf?wa=wsignin1.0&rpsnv=12&ct=1411375017&rver=6.2.6289.0&wp=MBI&wreply=https:%2F%2Frps-mlxprod.microsoft.com:443%2FRpsSts%2FLogOn%3Fwa%3Dwsignin1.0%26wtrealm%3Dhttps%253a%252f%252fmlxprod.accesscontrol.windows.net%252f%26wreply%3Dhttps%253a%252f%252fmlxprod.accesscontrol.windows.net%252fv2%252fwsfederation%26wctx%3DcHI9d3NmZWRlcmF0aW9uJnJtPWh0dHBzJTNhJTJmJTJmd3d3Lm1pY3Jvc29mdHZpcnR1YWxhY2FkZW15LmNvbSUyZkF1dGhlbnRpY2F0aW9uSGFuZGxlci5hc3B4JnJ5PWh0dHBzJTNhJTJmJTJmd3d3Lm1pY3Jvc29mdHZpcnR1YWxhY2FkZW15LmNvbSUyZkF1dGhlbnRpY2F0aW9uSGFuZGxlci5hc3B4JmN4PQ2&lc=1033&id=291113";
            OpenQA.Selenium.IE.InternetExplorerOptions _ieOptions = new OpenQA.Selenium.IE.InternetExplorerOptions();
            _ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            OpenQA.Selenium.IE.InternetExplorerDriver _ie = new OpenQA.Selenium.IE.InternetExplorerDriver(_ieOptions);
            _ie.Navigate().GoToUrl(url);

            IWebElement name = _ie.FindElementByName("login");
            IWebElement pwd = _ie.FindElementByName("passwd");
            IWebElement signin = _ie.FindElementByName("SI");
            name.SendKeys("skytest@outlook.com");
            pwd.SendKeys("");
            signin.Click();
        }
    }
}
