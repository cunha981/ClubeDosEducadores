using Helper;
using Model;
using ViewModel.UsuarioVMs;
using System.Linq;
using System.Web.Mvc;
using System;
using Helper.MailHelper;
using Model.Enums;

namespace Domain
{
    public class UsuarioDomain : ModelDomain<UsuarioFuncionario>
    {
        private readonly IMailProvider _mailProvider;

        public UsuarioDomain() : base()
        {
            _mailProvider = DependencyResolver.Current.GetService<IMailProvider>();
        }

        public UsuarioFuncionario Cadastrar(UsuarioCadastrarVM vm, string sessionId = null)
        {
            var model = Data.Create();
            model.Inject(vm);
            model.Funcionario = _context.Set<Funcionario>().Create();
            model.Funcionario.Inject(vm);
            model.Funcionario.CargoId = vm.CargoId.Value;
            model.SessionId = sessionId;
            model.Notificacoes = EnumExtensions.GetEnumerable<TipoEventoEnum>().Select(a => 
            new UsuarioNotificacao
            {
                Notificar = true,
                TipoEvento = a
            }).ToList();

            _mailProvider.SendAsync(MailTemplateEnum.Welcome, model.Email, null, model.Funcionario.Nome);
            return Save(model);
        }

        public UsuarioFuncionario Login(UsuarioLoginVM vm, string sessionId)
        {
            var user = Get().Single(a => a.Email == vm.Email && a.Senha == vm.Senha && a.Ativo);
            RefreshSessionId(sessionId, user.SessionId);

            user.SessionId = sessionId;
            user.DtAtividade = DateTime.Now;
            SaveChanges();
            return user;
        }

        public bool IsAuth(bool isAdmin = false)
        {
            string sessionId;

            if (isAdmin)
            {
                sessionId = _adminProvider.User?.SessionId;
            }
            else
            {
                 sessionId = _funcionarioProvider.User?.SessionId;
            }

            if (sessionId == null)
            {
                return false;
            }

            if (!_cacheManager.IsSet<string>(sessionId))
            {
                var user = Data.SingleOrDefault(a => a.SessionId == sessionId && a.Ativo);

                if (user == null)
                {
                    return false;
                }
            }

            RefreshSessionId(sessionId);
            LogActivity(sessionId);

            return true;
        }

        private void RefreshSessionId(string sessionId, string oldSessionId = null)
        {
            if(oldSessionId != null)
            {
                _cacheManager.Remove<string>(oldSessionId);
            }

            _cacheManager.Set(sessionId, sessionId, 30);
        }

        private void LogActivity(string sessionId)
        {
            _context.Database.ExecuteSqlCommand($"update UsuarioFuncionario set DtAtividade = '{DateTime.Now}' where SessionId = '{sessionId}';");
        }

        public bool RecoveryPassword(string email)
        {
            var user = Data.SingleOrDefault(a => a.Email == email && a.Ativo);
            if(user == null)
            {
                return false;
            }

            user.Senha = RandomHelper.RandomString(8);

            _mailProvider.SendAsync(MailTemplateEnum.ForgotPassword, user.Email, null, user.Funcionario.Nome, user.Senha);
            SaveChanges();
            return true;
        }
    }
}