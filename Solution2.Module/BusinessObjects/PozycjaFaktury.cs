using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.XtraSpreadsheet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution2.Module.BusinessObjects
{
    //[VisibleInReports]
    //[VisibleInDashboards]
    [DefaultClassOptions]
    public class PozycjaFaktury : XPObject
    {
        public PozycjaFaktury(Session session) : base(session)
        { }


        PozycjaFaktury pozycjaKorygowana;
        PozycjaFaktury pozycjaKorygujaca;
        decimal wartoscBrutto;
        decimal wartoscVat;
        decimal wartoscNetto;
        decimal ilosc;
        decimal cenaJednostkowa;
        Towar towar;
        Faktura faktura;

        [Association("Faktura-PozycjeFaktury")]
        public Faktura Faktura
        {
            get => faktura;
            set
            {
                var oldFaktura = faktura;
                bool modified = SetPropertyValue(nameof(Faktura), ref faktura, value);
                if (modified && !IsLoading && !IsSaving && oldFaktura != faktura)
                {

                    oldFaktura = oldFaktura ?? faktura;
                    oldFaktura.PrzeliczSumy();
                }
            }
        }


        [ImmediatePostData]
        public Towar Towar
        {
            get => towar;
            set
            {
                bool modified = SetPropertyValue(nameof(Towar), ref towar, value);
                if (modified && !IsLoading && !IsSaving && Towar != null)
                {
                    CenaJednostkowa = Towar.CenaJednostkowa;
                    PrzeliczPozycje();
                }
            }
        }



        [ImmediatePostData]
        public decimal CenaJednostkowa
        {
            get => cenaJednostkowa;
            set
            {
                bool modified = SetPropertyValue(nameof(CenaJednostkowa), ref cenaJednostkowa, value);


                if (modified && !IsLoading && !IsSaving)
                {
                    PrzeliczPozycje();
                }

            }
        }


        [ImmediatePostData]
        public decimal Ilosc
        {
            get => ilosc;
            set
            {
                bool modified = SetPropertyValue(nameof(Ilosc), ref ilosc, value);
                if (modified && !IsLoading && !IsSaving)
                {
                    PrzeliczPozycje();
                }
            }
        }


        public decimal WartoscNetto
        {
            get => wartoscNetto;
            set => SetPropertyValue(nameof(WartoscNetto), ref wartoscNetto, value);
        }

        public decimal WartoscVat
        {
            get => wartoscVat;
            set => SetPropertyValue(nameof(WartoscVat), ref wartoscVat, value);
        }

        public decimal WartoscBrutto
        {
            get => wartoscBrutto;
            set => SetPropertyValue(nameof(WartoscBrutto), ref wartoscBrutto, value);
        }


        public PozycjaFaktury PozycjaKorygujaca
        {
            get => pozycjaKorygujaca;
            set => SetPropertyValue(nameof(PozycjaKorygujaca), ref pozycjaKorygujaca, value);
        }
        
        public PozycjaFaktury PozycjaKorygowana
        {
            get => pozycjaKorygowana;
            set => SetPropertyValue(nameof(PozycjaKorygowana), ref pozycjaKorygowana, value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Ilosc = 1;
        }

        private void PrzeliczPozycje()
        {
            if (pozycjaKorygowana != null)
                return;

            var stawka = Towar?.StawkaVat?.Wartosc ?? 0;
            WartoscNetto = Ilosc * CenaJednostkowa;
            WartoscBrutto = WartoscNetto * (100 + stawka) / 100;
            WartoscVat = WartoscBrutto - WartoscNetto;

            if (Faktura != null)
            {
                Faktura.PrzeliczSumy();
            }

        }
    }
}
