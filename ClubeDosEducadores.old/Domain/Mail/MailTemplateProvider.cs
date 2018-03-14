using DataAccess;
using Helper;
using Helper.MailHelper;
using Model;
using System;
using System.Linq;

namespace Domain.Mail
{
    public class MailTemplateProvider : IMailTemplateProvider
    {
        private Context _context;

        public MailTemplateProvider(Context context)
        {
            _context = context;
        }

        public IMail Get(Enum key, string to, params object[] parameters)
        {
            var keyDescription = key.GetDescription();

            var template = _context.Set<MailTemplate>().Single(a => a.Key == keyDescription);

            return new Email
            {
                Subject = template.Subject,
                Body = string.Format(template.Body, parameters),
                To = to
            };
        }
    }
}
