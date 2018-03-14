using ViewModel.UsuarioVMs;

namespace Domain.Auth
{
    public interface IFuncionarioProvider
    {
        FuncionarioOnline User { get; }
    }
}
