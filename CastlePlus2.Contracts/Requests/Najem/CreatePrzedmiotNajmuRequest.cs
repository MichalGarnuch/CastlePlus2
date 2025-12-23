using System;

namespace CastlePlus2.Contracts.Requests.Najem
{
    public class CreatePrzedmiotNajmuRequest
    {
        public Guid IdUmowyNajmu { get; set; }
        public Guid IdEncji { get; set; }
        public decimal? UdzialProcent { get; set; }
        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }
    }
}