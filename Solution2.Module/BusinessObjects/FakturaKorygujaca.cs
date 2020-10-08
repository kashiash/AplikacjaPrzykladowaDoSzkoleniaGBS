using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.DC;
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
        XPCollection<PozycjaFaktury> pozycjePrzedKorekta;
        [XafDisplayName("Przed korektą")]
        public XPCollection<PozycjaFaktury> PozycjePrzedKorekta
        {
            get
            {
                if (pozycjePrzedKorekta == null)
                {
                    PrzygotujPozycjePrzedKorekta();
                    OnChanged(nameof(PozycjePrzedKorekta));
                }
                return pozycjePrzedKorekta;
            }
        }
        private void PrzygotujPozycjePrzedKorekta()
        {
            pozycjePrzedKorekta = new XPCollection<PozycjaFaktury>(Session, CriteriaOperator.Parse("Faktura = ?", KorygowanaFaktura));
        }

        // pozycje po korekcie
        IList<PozycjaFakturyDC> pozycjePoKorekcie;
        [XafDisplayName("Po korekcie")]
        public IList<PozycjaFakturyDC> PozycjePoKorekcie
        {
            get
            {

                if (pozycjePoKorekcie == null)
                {
                    PrzygotujPozycjePoKorekcie();
                    OnChanged(nameof(PozycjePoKorekcie));
                }
                return pozycjePoKorekcie;

            }
        }

        private void PrzygotujPozycjePoKorekcie()
        {
            if (pozycjePoKorekcie == null)
            {
                pozycjePoKorekcie = new List<PozycjaFakturyDC>();
            }
            foreach (var korygowana in PozycjePrzedKorekta)
            {
                PozycjaFakturyDC oczekiwana = pozycjePoKorekcie
                    .Where(p => p.PozycjaKorygowana == korygowana)
                    .FirstOrDefault();
                if (oczekiwana == null)
                {
                    oczekiwana = new PozycjaFakturyDC()
                    {
                        Towar = korygowana.Towar,
                        Ilosc = korygowana.Ilosc,
                        CenaJednostkowaNetto = korygowana.CenaJednostkowa,
                        WartoscBrutto = korygowana.WartoscBrutto,
                        WartoscNetto = korygowana.WartoscNetto,
                        WartoscVAT = korygowana.WartoscVat,
                        PozycjaKorygowana = korygowana,
                    };
                }
                pozycjePoKorekcie.Add(oczekiwana);

            }
            foreach (var korygujaca in PozycjeFaktury)
            {
                if (korygujaca != null)
                {
                    var oczekiwana = pozycjePoKorekcie
                        .Where(p => p.PozycjaKorygowana == korygujaca.PozycjaKorygowana)
                        .FirstOrDefault();
                    if (oczekiwana == null)
                    {
                        oczekiwana = new PozycjaFakturyDC()
                        {
                            Towar = korygujaca.Towar,
                            Ilosc = korygujaca.Ilosc,
                            CenaJednostkowaNetto = korygujaca.CenaJednostkowa,
                            WartoscBrutto = korygujaca.WartoscBrutto,
                            WartoscNetto = korygujaca.WartoscNetto,
                            WartoscVAT = korygujaca.WartoscVat,
                            PozycjaKorygujaca = korygujaca,
                        };
                        pozycjePoKorekcie.Add(oczekiwana);
                    }
                    else
                    {
                        oczekiwana.Ilosc += korygujaca.Ilosc;
                        oczekiwana.CenaJednostkowaNetto += korygujaca.CenaJednostkowa;
                        oczekiwana.WartoscBrutto += korygujaca.WartoscBrutto;
                        oczekiwana.WartoscNetto += korygujaca.WartoscNetto;
                        oczekiwana.WartoscVAT += korygujaca.WartoscVat;
                        oczekiwana.PozycjaKorygujaca = korygujaca;
                    }
                }
            }
        }
    }
}
