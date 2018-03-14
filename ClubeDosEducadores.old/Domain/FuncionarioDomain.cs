using Helper;
using Model;
using System.Collections.Generic;
using System.Linq;
using ViewModel.FuncionarioVMs;

namespace Domain
{
    public class FuncionarioDomain : ModelDomain<Funcionario>
    {
        public Funcionario Save(FuncionarioEditVM vm)
        {
            Funcionario model;

            if(vm.Id > 0)
            {
                model = Get(vm.Id);
                model.Inject(vm);
            }
            else
            {
                model = vm.ConvertTo<Funcionario>();
            }

            model.Latitude = vm.Latitude;
            model.Longitude = vm.Longitude;

            model.Usuario.Inject(vm);

            if(model.Usuario.Notificacoes == null)
            {
                model.Usuario.Notificacoes = new List<UsuarioNotificacao>();
            }
            else
            {
                model.Usuario.Notificacoes.Foreach((notificacao) => notificacao.Notificar = false);
            }

            if(vm.Notificacoes != null)
            {
                vm.Notificacoes.Foreach((vmNotificacao) =>
                {
                    var notificacao = model.Usuario.Notificacoes.SingleOrDefault(a => a.TipoEvento == vmNotificacao);
                    if(notificacao == null)
                    {
                        notificacao = new UsuarioNotificacao();
                        model.Usuario.Notificacoes.Add(notificacao);
                    }

                    notificacao.Notificar = true;
                });
            }

            return Save(model);
        }

        public Funcionario GetByUsuarioId(int id)
        {
            return Get().Single(a => a.Usuario.Id == id);
        }
    }
}
