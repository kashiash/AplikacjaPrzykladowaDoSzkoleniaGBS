using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution2.Module.BusinessObjects
{
    [XafDefaultProperty(nameof(PelnyAdres))]
    public class Adres : XPObject
    {
        string miejscowosc;
        string kodPocztowy;
        string nrLokalu;
        string nrDomu;
        string ulica;
        public Adres(Session session) : base(session)
        { }

        [PersistentAlias("KodPocztowy + Miejscowosc + Ulica + NrDomu + NrLokalu")]
        public string PelnyAdres
        {
            get => $"{KodPocztowy} {Miejscowosc} {Ulica} {NrDomu} {NrLokalu}";
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Ulica
        {
            get => ulica;
            set => SetPropertyValue(nameof(Ulica), ref ulica, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string NrDomu
        {
            get => nrDomu;
            set => SetPropertyValue(nameof(NrDomu), ref nrDomu, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string NrLokalu
        {
            get => nrLokalu;
            set => SetPropertyValue(nameof(NrLokalu), ref nrLokalu, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string KodPocztowy
        {
            get => kodPocztowy;
            set => SetPropertyValue(nameof(KodPocztowy), ref kodPocztowy, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Miejscowosc
        {
            get => miejscowosc;
            set => SetPropertyValue(nameof(Miejscowosc), ref miejscowosc, value);
        }

    }


    public class AdresKontrahenta : Adres
    {
        public AdresKontrahenta(Session session) : base(session)
        { }


        Kontrahent kontrahent;


        //  [Association]
        public Kontrahent Kontrahent
        {
            get => kontrahent;
            set => SetPropertyValue(nameof(Kontrahent), ref kontrahent, value);
        }
    }
}
