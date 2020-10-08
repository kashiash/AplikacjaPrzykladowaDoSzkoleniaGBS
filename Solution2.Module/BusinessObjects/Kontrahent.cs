using DevExpress.ExpressApp.Editors;
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
    [DefaultProperty(nameof(Nazwa))]
    public class Kontrahent : XPObject
    {
        public Kontrahent(Session session) : base(session)
        { }



        string uwagi;
        string daneDoFaktury;
        Adres adresSiedziby;
        Adres adresKorespondencyjny;
        string regon;
        string nIP;
        string nazwa;

        [Size(200)]
        public string Nazwa
        {
            get => nazwa;
            set => SetPropertyValue(nameof(Nazwa), ref nazwa, value);
        }


        [Size(12)]
        public string NIP
        {
            get => nIP;
            set => SetPropertyValue(nameof(NIP), ref nIP, value);
        }


        [Size(14)]
        public string Regon
        {
            get => regon;
            set => SetPropertyValue(nameof(Regon), ref regon, value);
        }


        
        [Size(SizeAttribute.Unlimited)]
        public string Uwagi
        {
            get => uwagi;
            set => SetPropertyValue(nameof(Uwagi), ref uwagi, value);
        }

        //  [EditorAlias(EditorAliases.DetailPropertyEditor)]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        //  [Association]
        public Adres AdresSiedziby
        {
            get => adresSiedziby;
            set => SetPropertyValue(nameof(AdresSiedziby), ref adresSiedziby, value);
        }
        [EditorAlias(EditorAliases.DetailPropertyEditor)]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        // [Association]
        public Adres AdresKorespondencyjny
        {
            get => adresKorespondencyjny;
            set => SetPropertyValue(nameof(AdresKorespondencyjny), ref adresKorespondencyjny, value);
        }

        
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.RichTextPropertyEditor)]
        public string DaneDoFaktury
        {
            get => daneDoFaktury;
            set => SetPropertyValue(nameof(DaneDoFaktury), ref daneDoFaktury, value);
        }


        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (AdresSiedziby == null)
            {
                AdresSiedziby = new Adres(Session);
            }


            if (AdresKorespondencyjny == null)
            {
                AdresKorespondencyjny = new Adres(Session);
            }
        }
    }
}
