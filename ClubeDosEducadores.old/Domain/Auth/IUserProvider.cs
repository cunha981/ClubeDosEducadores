using Model;

namespace Domain.Auth
{
    public interface IUserProvider
    {
        IUser User { get; }
    }
}
