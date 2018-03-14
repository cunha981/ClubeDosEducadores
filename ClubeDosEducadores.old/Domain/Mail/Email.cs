using System.Configuration;

namespace Domain.Mail
{
    public class Email : Helper.MailHelper.Mail
    {
        public override string From
        {
            get
            {
                return ConfigurationManager.AppSettings["MailFrom"] ?? base.From;
            }

            set
            {
                base.From = value;
            }
        }
    }
}
