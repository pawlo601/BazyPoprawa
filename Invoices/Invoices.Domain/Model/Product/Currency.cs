using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
/*
drop table CURRENCY
go

CREATE TABLE [dbo].[CURRENCY](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](5) NULL,
	[exchange] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
 */
namespace Invoices.Domain.Model.Product
{
    public class Currency
    {
        public virtual int ID { get; set; }
        public virtual Waluta Name { get; set; }
        public virtual double ExchangeInTheRelationToPLN { get; set; }//X PLN=1 Waluta
        public Currency()
        {
            ID = -1;
            Name = Waluta.PLN;
            ExchangeInTheRelationToPLN = 1.0f;
        }
        public Currency(Waluta name, double exchange)
        {
            this.ID = -1;
            this.Name = name;
            this.ExchangeInTheRelationToPLN = exchange;
        }
        public override string ToString()
        {
            return ID.ToString() + " " + Name.ToString() + " " + ExchangeInTheRelationToPLN.ToString();
        }
        public virtual string FormatString()
        {
            return String.Format("ID: {0}\nWaluta: {1}\nExchange: {2}", ID.ToString(), Name.ToString(), ExchangeInTheRelationToPLN.ToString());
        }
        [SelfValidation]
        public virtual void ExchangeInTheRelationToPLNValidation(ValidationResults results)
        {
            if (ExchangeInTheRelationToPLN <= 0.0f)
                results.AddResult(new ValidationResult("Kurs wymiany powinien być większy od zera", this, "ExchangeInTheRelationToPLNValidation", string.Empty, null));
        }
    }
}
