namespace Helper.MailHelper
{
    public class Mail : IMail
    {
        public virtual string From { get; set; }
        public virtual string To { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }
    }
}
