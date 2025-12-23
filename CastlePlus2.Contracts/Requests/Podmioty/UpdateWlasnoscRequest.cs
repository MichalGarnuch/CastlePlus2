using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastlePlus2.Contracts.Requests.Podmioty
{
    public class UpdateWlasnoscRequest
    {
        public Guid IdEncji { get; set; }
        public long IdPodmiotu { get; set; }

        public decimal UdzialProcent { get; set; }
        public DateOnly OdDnia { get; set; }
        public DateOnly? DoDnia { get; set; }
    }
}
