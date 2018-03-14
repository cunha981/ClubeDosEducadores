using System;

namespace Helper.MailHelper
{
    public interface IMailTemplateProvider
    {
        /// <summary>
        /// Generate IMail by template
        /// </summary>
        /// <param name="key">Template Key to be found</param>
        /// /// <param name="to">Who will receive mail</param>
        /// <param name="parameters">Parameters of templte, should be exact else throw</param>
        /// <returns>IMail</returns>
        IMail Get(Enum key, string to, params object[] parameters);
    }
}
