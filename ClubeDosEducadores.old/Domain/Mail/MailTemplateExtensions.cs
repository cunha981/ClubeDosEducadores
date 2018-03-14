using Helper.MailHelper;
using Model;

namespace Domain.Mail
{
    public static class MailTemplateExtensions
    {
        public static Email GetRecoveryPasswordMail(this UsuarioFuncionario user, IMailTemplateProvider mailTemplateProvider)
        {
            var mail = new Email();
            mail.To = user.Email;

            mail.Subject = "Clube dos Educadores - Recuperação de senha";

            mail.Body = $@"Olá {user.Funcionario.Nome}, recebemos o seu pedido de recuperação de senha!
                        <br /><br />
                        Sua senha atual é: <b>{user.Senha}</b>";

            return mail;
        }
    }
}
