using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution2.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Kartoteki")]
    [Persistent("ProduktNew")]
    public class Produkt : Towar
    {
        public Produkt(Session session) : base(session)
        { }



        DateTime terminPrzydatnosci;
        string numer;
        string seria;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Seria
        {
            get => seria;
            set => SetPropertyValue(nameof(Seria), ref seria, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Numer
        {
            get => numer;
            set => SetPropertyValue(nameof(Numer), ref numer, value);
        }
        
        [XafDisplayName("Termin przydatności")]
        public DateTime TerminPrzydatnosci
        {
            get => terminPrzydatnosci;
            set => SetPropertyValue(nameof(TerminPrzydatnosci), ref terminPrzydatnosci, value);
        }
    }
}
