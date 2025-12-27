namespace CastlePlus2.Contracts.Requests.Slownik
{
    public class CreateJednostkaMiaryRequest
    {
        public string KodJednostki { get; set; } = default!;
        public string Nazwa { get; set; } = default!;
    }
}
