using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoices.Domain.Model.Product;
using NHibernate;
using NHibernate.Cfg;

namespace Finance.Infrastructure.Mapping.Product
{
    public class TypProduktuType : NHibernate.Type.EnumStringType
    {
        public TypProduktuType()
            : base(typeof(TypProduktu), 10)
        { }
    }
}
