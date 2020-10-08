using DevExpress.ExpressApp;
using Solution2.Module.BusinessObjects;
using System;

namespace Solution2.Module.Win.Controllers
{
    internal class InvoiceFactory
    {
        private IObjectSpace objectSpace;

        public InvoiceFactory(IObjectSpace objectSpace)
        {
            this.objectSpace = objectSpace;
        }

        internal FakturaKorygujaca WystawKorekte(Faktura faktura)
        {
            if (faktura == null)

            {

                return null;
            }

            var korekta = objectSpace.CreateObject<FakturaKorygujaca>();

            korekta.Kontrahent = faktura.Kontrahent;
            korekta.DataSprzedazy = faktura.DataSprzedazy;
            korekta.DataFaktury = DateTime.Now;
            korekta.KorygowanaFaktura = faktura;

            foreach (var pozycja in faktura.PozycjeFaktury) 
            {
                var pozycjaKorekty = objectSpace.CreateObject<PozycjaFaktury>();

                pozycja.PozycjaKorygujaca = pozycjaKorekty;
                pozycjaKorekty.PozycjaKorygowana = pozycja;

                pozycjaKorekty.Towar = pozycja.Towar;
                pozycjaKorekty.CenaJednostkowa = pozycja.CenaJednostkowa;
                pozycjaKorekty.Ilosc -= pozycja.Ilosc;
                pozycjaKorekty.WartoscNetto -= pozycja.WartoscNetto;
                pozycjaKorekty.WartoscVat -= pozycja.WartoscVat;
                pozycjaKorekty.WartoscBrutto -= pozycja.WartoscBrutto;
                korekta.PozycjeFaktury.Add(pozycjaKorekty);
            }

            return korekta;
        }
    }
}