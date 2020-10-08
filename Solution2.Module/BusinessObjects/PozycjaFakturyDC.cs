using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution2.Module.BusinessObjects
{
    [DomainComponent]
    public class PozycjaFakturyDC 
    {
        public Towar Towar { get; set; }

        [XafDisplayName("Cena")]
        public decimal? CenaJednostkowaNetto { get; set; }
        public decimal Ilosc { get; set; }
        public decimal? WartoscNetto { get; set; }
        public decimal? WartoscVAT { get; set; }
        public decimal? WartoscBrutto { get; set; }

        public PozycjaFaktury PozycjaKorygowana { get; set; }
        public PozycjaFaktury PozycjaKorygujaca { get; set; }
    }
}
