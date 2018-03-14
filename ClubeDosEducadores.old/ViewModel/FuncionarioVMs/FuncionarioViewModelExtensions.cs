using Helper;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace ViewModel.FuncionarioVMs
{
    public static class FuncionarioViewModelExtensions
    { 
        public static FuncionarioEditVM ToEditVM(this Funcionario model)
        {
            var vm = model.ConvertTo<FuncionarioEditVM>();
            vm.Inject(model.Usuario);
            vm.Notificacoes = model.Usuario.Notificacoes.Where(a => a.Notificar).Select(a => a.TipoEvento).ToArray();
            return vm;
        }

        public static IEnumerable<FuncionarioControlListVM> ToListControlVM(this IEnumerable<Funcionario> models)
        {
            return models.Select(a => new FuncionarioControlListVM
            {
                Id = a.Id,
                Nome = a.Nome,
                Telefone = a.Telefone,
                Celular = a.Celular,
                EnderecoPreenchido = a.Latitude.HasValue && a.Longitude.HasValue,
                Email = a.Usuario.Email, 
                Senha = a.Usuario.Senha,
                Ativo = a.Usuario.Ativo
            }).ToArray();
        }
    }
}
