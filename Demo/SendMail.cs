using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Se = OpenQA.Selenium;
using SIE = OpenQA.Selenium.IE;
using UI = OpenQA.Selenium.Support.UI;

namespace Demo
{
    public class SendMail
    {
        public SIE.InternetExplorerDriver IEDriver { get; set; }
        public string MsgTitle { get; set; }

        public SendMail()
        {
            SIE.InternetExplorerOptions _ieOptions = new SIE.InternetExplorerOptions();
            _ieOptions.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            IEDriver = new SIE.InternetExplorerDriver(_ieOptions);

            MsgTitle = "TEST - a new msg";
        }

        public bool Test()
        {
            Logon();
            SendMsg();
            return VerifySendBox();
        }

        private void Logon()
        {
            IEDriver.Navigate().GoToUrl("http://www.outlook.com");
            Se.IWebElement account = null;
            Se.IWebElement pwd = null;
            account = IEDriver.FindElement(Se.By.Name("login"));
            pwd = IEDriver.FindElement(Se.By.Name("passwd"));

            if (account != null && pwd != null)
            {
                account.SendKeys("skytest@outlook.com");
                pwd.SendKeys("");
            }

            Se.IWebElement signInBtn = null;
            signInBtn = IEDriver.FindElement(Se.By.Name("SI"));
            signInBtn.Click();
        }

        private void SendMsg()
        {
            //string body = "for test only, please ignore this msg.";
            Se.IWebElement newMsgBtn = null;
            newMsgBtn = IEDriver.FindElement(Se.By.Id("NewMessage"));
            newMsgBtn.Click();

            Se.IWebElement toBox = null;
            toBox = IEDriver.FindElement(Se.By.XPath(@"//div[@class='cp_inputContainer']/div/span/textarea"));
            toBox.SendKeys("xueqijie123@163.com");

            Se.IWebElement subjectBox = null;
            subjectBox = IEDriver.FindElement(Se.By.Name("fSubject"));
            subjectBox.SendKeys(MsgTitle);

            #region
            //Se.IWebElement msgBodyBox = null;
            //Se.IWebElement msgFrame = null;
            //IList<Se.IWebElement> frames = IEDriver.FindElements(Se.By.TagName("iframe"));
            //foreach (var frame in frames)
            //{
            //    if (frame.GetAttribute("title").ToLower() == "Message body".ToLower())
            //    {
            //        msgFrame = frame;
            //        break;
            //    }
            //}
            //if (msgFrame != null)
            //{
            //    IEDriver.SwitchTo().Frame(msgFrame);
            //    msgBodyBox = IEDriver.FindElement(Se.By.TagName("body"));
            //    msgBodyBox.SendKeys(body);
            //}
            #endregion
            IEDriver.SwitchTo().DefaultContent();

            Se.IWebElement sendBtn = null;
            sendBtn = IEDriver.FindElement(Se.By.Id("SendMessage"));
            sendBtn.Click();
        }

        private bool VerifySendBox()
        {
            bool pass = false;
            IEDriver.SwitchTo().DefaultContent();
            Se.IWebElement sentBox = null;

            UI.WebDriverWait _wait = new UI.WebDriverWait(IEDriver, TimeSpan.FromSeconds(10));
           sentBox= _wait.Until<Se.IWebElement>((d) => { return IEDriver.FindElement(Se.By.XPath(@"//li[@title='Sent']")); });
            //sentBox = IEDriver.FindElement(Se.By.XPath(@"//li[@title='Sent']"));
           
            if (sentBox != null)
            { sentBox.Click();
                
            }

            // list
            IList<Se.IWebElement> sends = IEDriver.FindElements(Se.By.XPath(@"//ul[@class='mailList InboxTableBody ']/li"));
            if (sends.Count > 0)
            {
                Se.IWebElement first = sends[0];
                string title = first.FindElement(Se.By.XPath(@"//span[@class='Sb']/a")).Text.Trim();

                if (title == MsgTitle)
                {
                    pass = true;
                }
            }

            return pass;
        }
    }
}
