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
    [NavigationItem("Słowniki")]
    [XafDefaultProperty(nameof(Symbol))]

    public class StawkaVat : XPObject
    {
        public StawkaVat(Session session) : base(session)
        { }


        decimal wartosc;
        string symbol;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
      //  [Key(false)]
        public string Symbol
        {
            get => symbol;
            set => SetPropertyValue(nameof(Symbol), ref symbol, value);
        }

        public decimal Wartosc
        {
            get => wartosc;
            set => SetPropertyValue(nameof(Wartosc), ref wartosc, value);
        }
    }
}
