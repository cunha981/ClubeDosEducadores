namespace Model
{
    public interface IUser : IKey
    {
        string Email { get; set; }
    }
}
