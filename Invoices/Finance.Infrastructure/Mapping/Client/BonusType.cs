using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using Invoices.Domain.Model.Client;

namespace Finance.Infrastructure.Mapping.Client
{
    public class BonusType : NHibernate.Type.EnumStringType
    {
        public BonusType()
            : base(typeof(Bonus), 15)
        { }
    }
}
