using Helper;
using Model;

namespace ViewModel.UsuarioVMs
{
    public static class UsuarioViewModelExtensions
    {
        public static FuncionarioOnline ToFuncionarioOnline(this UsuarioFuncionario model, string sessionId)
        {
            var vm = model.ConvertTo<FuncionarioOnline>();
            vm.Nome = model.Funcionario.Nome;
            vm.Latitude = model.Funcionario.Latitude;
            vm.Longitude = model.Funcionario.Longitude;
            vm.CargoId = model.Funcionario.CargoId;
            vm.Cargo = model.Funcionario.Cargo.Abreviacao;
            vm.SessionId = sessionId;
            vm.UnidadeId = model.Funcionario.UnidadeTrabalhoId;
            vm.TipoUnidadeId = model.Funcionario.UnidadeTrabalho?.TipoUnidadeId;
            vm.RegiaoUnidadeId = model.Funcionario.UnidadeTrabalho?.RegiaoUnidadeId;
            return vm;
        }

        public static AdminOnline ToAdminOnline(this UsuarioFuncionario model, string sessionId)
        {
            var vm = model.ConvertTo<AdminOnline>();
            vm.Nome = model.Funcionario.Nome;
            vm.Email = model.Email;
            vm.SessionId = sessionId;
            return vm;
        }
    }
}