namespace CastlePlus2.Contracts.Requests.Auth
{
    public sealed class SetUserActiveRequest
    {
        public int IdUzytkownika { get; set; }
        public bool CzyAktywny { get; set; }
    }
}