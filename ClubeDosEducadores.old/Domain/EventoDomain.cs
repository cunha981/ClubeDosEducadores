using Autofac;
using Domain.Mail;
using Helper;
using Helper.CacheHelper;
using Helper.IocHelper;
using Helper.MailHelper;
using Model;
using Model.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class EventoDomain : ModelDomain<Evento>
    {
        public override Evento Save(Evento model)
        {
            model = base.Save(model);

            if(model.EnviarEmail)
            {
                SendEmail(model);
            }

            return model;
        }

        public override Evento Save<TViewModel>(TViewModel vm)
        {
            var model = base.Save(vm);

            if (model.EnviarEmail)
            {
                SendEmail(model);
            }

            return model;
        }

        private void SendEmail(Evento model)
        {
            var mailTemplate = GetMailTemplate(model.TipoEvento);
            var emails = _context.Set<UsuarioFuncionario>()
                            .Where(a => a.Ativo && a.Notificacoes.Any(b => b.Notificar && b.TipoEvento == model.TipoEvento))
                            .Select(a => a.Email).ToArray();

            new System.Threading.Thread(() =>
            {
                using (var scope = ApplicationContainer.Container.BeginLifetimeScope())
                {
                    var mailProvider = scope.Resolve<IMailProvider>();
                    emails.Foreach(email =>
                    {
                        mailProvider.Send(new Email
                        {
                            To = email,
                            Subject = mailTemplate.Subject,
                            Body = string.Format(mailTemplate.Body,
                            model.Titulo,
                            model.Resumo,
                            model.DtEvento.ToString("dd/MM/yyyy HH:mm"),
                            !string.IsNullOrWhiteSpace(model.Url) ? $@"<strong>Link:</strong>&nbsp;<a href=""{model.Url}"" target=""_blank"">{model.Url}</a>" : "")

                        });
                    });
                }
            }).Start();
        }

        private MailTemplate GetMailTemplate(TipoEventoEnum tipoEvento)
        {
            switch (tipoEvento)
            {
                case TipoEventoEnum.Curso:
                    return _context.Set<MailTemplate>().Single(a => a.Key == MailTemplateEnum.NotifyCurso.ToString());
                case TipoEventoEnum.Noticia:
                    return _context.Set<MailTemplate>().Single(a => a.Key == MailTemplateEnum.NotifyNoticia.ToString());
                case TipoEventoEnum.Aviso:
                    return _context.Set<MailTemplate>().Single(a => a.Key == MailTemplateEnum.NotifyAviso.ToString());
                case TipoEventoEnum.Concurso:
                    return _context.Set<MailTemplate>().Single(a => a.Key == MailTemplateEnum.NotifyConcurso.ToString());
                default:
                    throw new System.ArgumentOutOfRangeException("TipoEvento");
            }
        }

        public IEnumerable<Evento> GetCache(TipoEventoEnum tipoEvento = TipoEventoEnum.Indefinido)
        {
            return _cacheManager.Get($"evento-{tipoEvento.ToString() ?? "all"}", 10, () =>
            {
                var query = tipoEvento == TipoEventoEnum.Indefinido ? Get() : Get().Where(a => a.TipoEvento == tipoEvento);

                return query.OrderByDescending(a => a.DtCadastro).Take(100).ToArray();
            });
        }
    }
}
