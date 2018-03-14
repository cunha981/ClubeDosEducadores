using Domain.Auth;
using Domain.Mail;
using Helper;
using Helper.MailHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ViewModel.FaleConoscoVMs;

namespace Domain
{
    public class ContactDomain
    {
        private readonly IFuncionarioProvider _userProvider;
        private IMailProvider _mailProvider;

        public ContactDomain()
        {
            _userProvider = DependencyResolver.Current.GetService<IFuncionarioProvider>();
            _mailProvider = DependencyResolver.Current.GetService<IMailProvider>();
        }

        public void Send(FaleConoscoVM vm)
        {
            var mail = new Email
            {
                Body = $@"Usuário: {_userProvider.User.Email}<br />
                        Nome: {_userProvider.User.Nome}<br />
                        {vm.Mensagem}",
                From = _userProvider.User.Email,
                Subject = vm.Assunto,
                To = ConfigurationManager.AppSettings["MailFrom"]
            };

            _mailProvider.SendAsync(mail);

            if (vm.ReceberCopia)
            {
                var copyMail = mail.Clone();
                copyMail.To = _userProvider.User.Email;
                _mailProvider.SendAsync(copyMail);
            }
        }

        public void Send(FaleConoscoPortfolioVM vm)
        {
            var mail = new Email
            {
                Body = $@"E-mail: {vm.Email}<br />
                        Nome: {vm.Nome}<br />
                        Celular: {vm.Celular}<br />
                        {vm.Mensagem}",
                From = vm.Email,
                Subject = "[Clube dos Educadores] - Contato",
                To = ConfigurationManager.AppSettings["MailFrom"]
            };

            _mailProvider.SendAsync(mail);
        }
    }
}
