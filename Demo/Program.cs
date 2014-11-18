using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SIE = OpenQA.Selenium.IE;
using UI = OpenQA.Selenium.Support.UI;
using SChr = OpenQA.Selenium.Chrome;
using SFire = OpenQA.Selenium.Firefox;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            SendMail mailTest = new SendMail();
            mailTest.Test();

            //ScrollBar barTest = new ScrollBar();
            //barTest.Test();

            //SearchMVA mvaTest = new SearchMVA();
            //mvaTest.Test();

            //MutiBrowser.TryFirefox();

            //Class1 cls = new Class1();
            //cls.Test2();


            //SeleniumGrid grid = new SeleniumGrid(Browser.Chrome);
            //grid.Test();

            //Class2 cls2 = new Class2();
            //cls2.TestLogon();
            Console.WriteLine("Stopped");
            Console.Read();
        }
    }
}
