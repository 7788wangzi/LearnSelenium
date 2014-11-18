using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Se = OpenQA.Selenium;
using SIE = OpenQA.Selenium.IE;

namespace Demo
{
    class ScrollBar
    {
        public SIE.InternetExplorerDriver IEDriver { get; set; }

        public ScrollBar()
        {
            SIE.InternetExplorerOptions _ieOptions = new SIE.InternetExplorerOptions();
            _ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            IEDriver = new SIE.InternetExplorerDriver(_ieOptions);
        }

        public void Test()
        {
            IEDriver.Navigate().GoToUrl("http://www.163.com");
            var js = "document.documentElement.scrollTop=10000";
            IEDriver.ExecuteScript(js,null);

            js = "document.documentElement.scrollLeft=10000";
            IEDriver.ExecuteScript(js, null);
        }
    }
}
