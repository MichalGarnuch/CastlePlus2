namespace CastlePlus2.Application.Interfaces.Auth
{
    public interface IPasswordHashService
    {
        bool Verify(string password, string passwordHash);
        string Hash(string password);
    }
}
