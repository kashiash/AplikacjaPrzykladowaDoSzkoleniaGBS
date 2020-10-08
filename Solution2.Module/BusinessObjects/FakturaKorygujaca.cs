using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution2.Module.BusinessObjects
{
    public class FakturaKorygujaca : Faktura
    {
        public FakturaKorygujaca(Session session) : base(session)
        { }


        string powodKorekty;
        Faktura korygowanaFaktura;

        public Faktura KorygowanaFaktura
        {
            get => korygowanaFaktura;
            set => SetPropertyValue(nameof(KorygowanaFaktura), ref korygowanaFaktura, value);
        }
        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string PowodKorekty
        {
            get => powodKorekty;
            set => SetPropertyValue(nameof(PowodKorekty), ref powodKorekty, value);
        }

        // Pozycje przed korekta
        // pozycje po korekcie

    }
}
