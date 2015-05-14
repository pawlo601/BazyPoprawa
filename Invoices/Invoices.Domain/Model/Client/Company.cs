using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
/*
drop table COMPANY
go

CREATE TABLE [dbo].[COMPANY](
	[id] [int] IDENTITY(2,2) NOT NULL,
	[name] [varchar](50) NULL,
    [surname] [varchar](50) NULL,
    [address] [int] NOT NULL,
    [NumberNIP] [varchar](20) NULL,
    [NumberRegon] [varchar](20) NULL
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
 */
namespace Invoices.Domain.Model.Client
{
    [HasSelfValidation]
    public class Company : Client
    {
        public virtual NIP Nip { get; set; }
        public virtual Regon Regon { get; set; }

        public Company()
            : base()
        {
            Random rand = new Random();
            this.Name = "Nazwa firmy";
            this.Name += rand.Next(1, 2000000).ToString();
            this.Surname = null;
            this.Nip = new NIP();
            this.Regon = new Regon();
        }
        public Company(string name, Address loc, NIP nip, Regon regon)
            : base(name, null, loc)
        {
            Nip = nip;
            Regon = regon;
        }
        public override string FormatString()
        {
            string text = String.Format("ID firmy: {1}{0}", Environment.NewLine, ID.ToString()) +
                                    "====================================" +
                                    String.Format("{0}Dane:{0}{1}{0}", Environment.NewLine, Name) +
                                    "====================================" +
                                    String.Format("{0}{1}{0}{2}{0}", Environment.NewLine, Nip.FormatString(), Regon.FormatString()) +
                                    "====================================" +
                                    String.Format("{0}Adres:{0}{1}{0}", Environment.NewLine, Localisation.FormatString()) +
                                    "====================================" +
                                    String.Format("{0}Kontakty:{0}", Environment.NewLine);
            foreach (Contact a in ListOfContact)
                text += String.Format("{0}------------------------------------{0}{1}",
                                       Environment.NewLine,
                                       a.FormatString());
            text += String.Format("{0}===================================={0}",
                    Environment.NewLine) +
                                    String.Format("Zniżki:{0}{0}", Environment.NewLine);
            foreach (Discount a in ListOfDiscount)
                text += String.Format("------------------------------------{0}{1}{0}", Environment.NewLine, a.FormatString());
            text += String.Format("===================================={0}",
                    Environment.NewLine);
            return text;
        }
        public override string ToString()
        {
            string text = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}",
                            Environment.NewLine,
                            ID.ToString(),
                            Name,
                            Nip.ToString(),
                            Regon.ToString(),
                            Localisation.ToString());
            foreach (Contact a in ListOfContact)
                text += a.ToString() + "\n";
            foreach (Discount a in ListOfDiscount)
                text += a.ToString() + "\n";
            return text;
        }
        [SelfValidation]
        public virtual void Validation(ValidationResults results)
        {
            if (Nip == null || Regon == null)
                results.AddResult(new ValidationResult("Nip ani regon nie powinny być null", this, "Validation", string.Empty, null));
        }
    }
}
