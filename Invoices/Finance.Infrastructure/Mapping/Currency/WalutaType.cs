using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using Invoices.Domain.Model.Product;

namespace Finance.Infrastructure.Mapping.Currency
{
    public class WalutaType : NHibernate.Type.EnumStringType
    {
        public WalutaType()
            : base(typeof(Waluta), 5)
        { }
    }
}
