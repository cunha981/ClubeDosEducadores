using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Helper.MailHelper
{
    public class MailProvider : IMailProvider
    {
        private IMailTemplateProvider _mailTemplateProvider;

        public MailProvider(IMailTemplateProvider mailTemplateProvider)
        {
            _mailTemplateProvider = mailTemplateProvider;
        }

        public bool Send(IMail iMail)
        {
            using (var mail = new MailMessage())
            {
                mail.To.Add(iMail.To);
                mail.From = new MailAddress(iMail.From);
                mail.Subject = iMail.Subject;
                mail.Body = iMail.Body;
                mail.IsBodyHtml = true;
                mail.Headers.Add("List-Unsubscribe", $"<mailto:{iMail.From}?subject=unsubscribe>");

                using (var smtp = new SmtpClient())
                {
                    try
                    {
                        smtp.Send(mail);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return true;
        }

        public async Task SendAsync(IMail iMail, Action<IMail, bool> action = null)
        {
            await Task.Run(() =>
            {
                var result = Send(iMail);
                action?.Invoke(iMail, result);
            });
        }

        public async Task SendAsync(Func<IMail> get, Action<IMail, bool> action = null)
        {
            await SendAsync(get(), action);
        }

        public async Task SendAsync(Enum key, string to, Action<IMail, bool> action = null, params object[] parameters)
        {
            var mail = _mailTemplateProvider.Get(key, to, parameters);
            await SendAsync(mail, action);
        }
    }
}
