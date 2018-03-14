using ViewModel.UsuarioVMs;

namespace Domain.Auth
{
    public interface IAdminProvider
    {
        AdminOnline User { get; }
    }
}
