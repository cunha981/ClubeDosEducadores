using System;
using System.Threading.Tasks;

namespace Helper.MailHelper
{
    public interface IMailProvider
    {
        bool Send(IMail iMail);

        /// <summary>
        /// Send email by async mode
        /// </summary>
        /// <param name="iMail">email</param>
        /// <param name="action">action to be executed after email send (email, true if sended or false if not)</param>
        Task SendAsync(IMail iMail, Action<IMail, bool> action = null);

        /// <summary>
        /// Send email by async mode
        /// </summary>
        /// <param name="get">function to generate the mail</param>
        /// <param name="action">action to be executed after email send (email, true if sended or false if not)</param>
        /// <returns></returns>
        Task SendAsync(Func<IMail> get, Action<IMail, bool> action = null);

        /// <summary>
        /// Send email by async mode using IEmailTemplateProvider to get the mail
        /// </summary>
        /// <param name="key">template key</param>
        /// <param name="to">to</param>
        /// <param name="action">callback</param>
        /// <param name="parameters">template parameters</param>
        /// <returns></returns>
        Task SendAsync(Enum key, string to, Action<IMail, bool> action = null, params object[] parameters);
    }
}
