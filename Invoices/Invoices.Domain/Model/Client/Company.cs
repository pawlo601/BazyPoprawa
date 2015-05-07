using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Domain.Model.Client
{
    public class Company:Client
    {
        public virtual NIP Nip { get; set; }
        public virtual Regon Regon { get; set; }

        public Company()
            :base()
        {
            Random rand = new Random();
            this.Name = "Nazwa firmy";
            this.Name += rand.Next(1, 2000000).ToString();
            this.Surname = null;
            this.Nip = new NIP();
            this.Regon = new Regon();
        }
        public Company(string name, Address loc, NIP nip, Regon regon)
            :base(name, null,loc)
        {
            Nip = nip;
            Regon = regon;
        }
        public override string FormatString()
        {
            string text = String.Format("ID firmy: {1}{0}", Environment.NewLine, ID.ToString()) +
                                    "====================================" +
                                    String.Format("Dane:{0}{1}", Environment.NewLine, Name ) +
                                    "====================================" +
                                    String.Format("{1}{0}{2}", Environment.NewLine, Nip.FormatString(), Regon.FormatString()) +
                                    "====================================" +
                                    String.Format("Adres:{0}{0}{1}", Environment.NewLine, Localisation.FormatString()) +
                                    "====================================" +
                                    String.Format("Kontakty:{0}", Environment.NewLine);
            foreach (Contact a in ListOfContact)
                text += a.FormatString() + "------------------------------------\n";
            text += "====================================" +
                  String.Format("Zniżki:{0}", Environment.NewLine);
            foreach (Discount a in ListOfDiscount)
                text += a.FormatString() + "------------------------------------";
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
    }
}
