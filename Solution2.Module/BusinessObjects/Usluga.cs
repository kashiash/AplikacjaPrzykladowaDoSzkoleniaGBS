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
    public class Usluga : Towar
    {
        public Usluga(Session session) : base(session)
        { }


        string opisUslugi;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string OpisUslugi
        {
            get => opisUslugi;
            set => SetPropertyValue(nameof(OpisUslugi), ref opisUslugi, value);
        }
    }
}
