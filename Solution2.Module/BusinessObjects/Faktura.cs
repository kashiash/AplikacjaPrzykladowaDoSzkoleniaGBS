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
    public class Faktura : XPObject
    {
        public Faktura(Session session) : base(session)
        { }


        decimal wartoscBrutto;
        decimal wartoscVat;
        decimal wartoscNetto;
        Kontrahent kontrahent;
        DateTime dataPlatnosci;
        DateTime dataSprzedazy;
        DateTime dataFaktury;
        string numer;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Numer
        {
            get => numer;
            set => SetPropertyValue(nameof(Numer), ref numer, value);
        }


        public DateTime DataFaktury
        {
            get => dataFaktury;
            set => SetPropertyValue(nameof(DataFaktury), ref dataFaktury, value);
        }

        public DateTime DataSprzedazy
        {
            get => dataSprzedazy;
            set => SetPropertyValue(nameof(DataSprzedazy), ref dataSprzedazy, value);
        }

        public DateTime DataPlatnosci
        {
            get => dataPlatnosci;
            set => SetPropertyValue(nameof(DataPlatnosci), ref dataPlatnosci, value);
        }


        public Kontrahent Kontrahent
        {
            get => kontrahent;
            set => SetPropertyValue(nameof(Kontrahent), ref kontrahent, value);
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

        [Association("Faktura-PozycjeFaktury"), Aggregated]
        public XPCollection<PozycjaFaktury> PozycjeFaktury
        {
            get
            {
                return GetCollection<PozycjaFaktury>(nameof(PozycjeFaktury));
            }
        }

        internal void PrzeliczSumy(bool forceChange = true)
        {
            decimal oldWartoscNetto = WartoscNetto;
            decimal oldWartoscVat = WartoscVat;
            decimal oldWartoscBrutto = WartoscBrutto;


            decimal tmpWartoscNetto = 0m;
            decimal tmpWartoscVat = 0m;
            decimal tmpWartoscBrutto = 0m;

            foreach (var pozycja in PozycjeFaktury)
            {
                tmpWartoscNetto += pozycja.WartoscNetto;
                tmpWartoscVat += pozycja.WartoscVat;
                tmpWartoscBrutto += pozycja.WartoscBrutto;
            }

            wartoscNetto = tmpWartoscNetto;
            wartoscVat = tmpWartoscVat;
            wartoscBrutto = tmpWartoscBrutto;

            if (forceChange)
            {
                OnChanged(nameof(WartoscNetto),oldWartoscNetto, wartoscNetto);
                OnChanged(nameof(WartoscVat), oldWartoscVat, wartoscVat);
                OnChanged(nameof(WartoscBrutto), oldWartoscBrutto, wartoscBrutto);
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            DataFaktury = DateTime.Now;
            DataSprzedazy = DateTime.Now;
            DataPlatnosci = dataFaktury.AddDays(14);
        }

        [Action(Caption = "Przelicz", ImageName = "BO_Skull")]
        public void PrzeliczFakture()
        {
            PrzeliczSumy();
        }
    }
}
