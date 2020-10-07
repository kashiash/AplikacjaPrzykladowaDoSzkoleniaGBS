using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution2.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Kartoteki")]
    [DefaultProperty(nameof(PelnaNazwa))]
    public class Towar : XPObject
    {
        public Towar(Session session) : base(session)
        { }

        //Usługa, Produkt,Materiał




        TypTowaru typTowaru;
        RodzajProsty rodzajProsty;
        JednostkaMiary jednostkaMiary;
        string uwagi;
        StawkaVat stawkaVat;
        decimal cenaJednostkowa;
        string eAN;
        string kodProduktu;
        string nazwa;

        [PersistentAlias(" Nazwa + ' ' + KodProduktu")]
        public string PelnaNazwa { get => $" {Nazwa} {KodProduktu}"; }


        public string Typ{ get => GetType().Name; }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("Nazwa towaru")]
        public string Nazwa
        {
            get => nazwa;
            set => SetPropertyValue(nameof(Nazwa), ref nazwa, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string KodProduktu
        {
            get => kodProduktu;
            set => SetPropertyValue(nameof(KodProduktu), ref kodProduktu, value);
        }



        public RodzajProsty PelnyAdres
       
        {
            get => rodzajProsty;
            set => SetPropertyValue(nameof(PelnyAdres
       ), ref rodzajProsty, value);
        }


        
        public TypTowaru TypTowaru
        {
            get => typTowaru;
            set => SetPropertyValue(nameof(TypTowaru), ref typTowaru, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string EAN
        {
            get => eAN;
            set => SetPropertyValue(nameof(EAN), ref eAN, value);
        }


        public decimal CenaJednostkowa
        {
            get => cenaJednostkowa;
            set => SetPropertyValue(nameof(CenaJednostkowa), ref cenaJednostkowa, value);
        }


        public StawkaVat StawkaVat
        {
            get => stawkaVat;
            set => SetPropertyValue(nameof(StawkaVat), ref stawkaVat, value);
        }


        public JednostkaMiary JednostkaMiary
        {
            get => jednostkaMiary;
            set => SetPropertyValue(nameof(JednostkaMiary), ref jednostkaMiary, value);
        }

        [Size(SizeAttribute.Unlimited)]
        public string Uwagi
        {
            get => uwagi;
            set => SetPropertyValue(nameof(Uwagi), ref uwagi, value);
        }
    }

    public enum RodzajProsty  { 
        [XafDisplayName("Usługa")]
        Usluga = 1,
        [XafDisplayName("Produkt")]
        Produkt = 2,
        [XafDisplayName("Materiał")]
        Material =9};
}
