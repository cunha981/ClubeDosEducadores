using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Helper.MailHelper;
using System.Threading.Tasks;
using System.Net.Mail;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var email = new Mail();
            email.To = "jeanjnx@gmail.com";
            email.From = "contato@clubedoseducadores.com.br";
            email.Subject = "Teste 2";
            email.Body = "Testando o envio de E-mail";

            Send(email);

        }

        private bool Send(IMail iMail)
        {
            using (var mail = new MailMessage())
            {
                mail.To.Add(iMail.To);
                mail.From = new MailAddress(iMail.From);
                mail.Subject = iMail.Subject;
                mail.Body = iMail.Body;
                mail.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    smtp.Send(mail);
                }
            }
            return true;
        }
    }
}
