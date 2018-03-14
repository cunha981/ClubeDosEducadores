namespace Helper.MailHelper
{
    public interface IMailTemplate
    {
        string Key { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
    }
}
