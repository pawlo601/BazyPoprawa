using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Domain.Model.Product
{
    public class Price
    {
        public virtual Money NetPrice { get; set; }
        public virtual float VAT { get; set; }

        public Price()
        {
            NetPrice = new Money();
            VAT = 0.19f;
        }
        public Price(Money a, float vat)
        {
            if (vat >= 0.0f && vat < 1.0f)
            {
                NetPrice = a;
                VAT = vat;
            }
            else
            {
                throw new Exception("Zła wartośc VAT.");
            }
        }
        public Price(float val, Waluta waluta, float vat)
            : this(new Money(val, waluta), vat)
        {

        }
        public Money GetGross()
        {
            return new Money(VAT * NetPrice.Value, NetPrice.NameOfCurrency);
        }
        public void ChangeCurrency(Waluta nowa)
        {
            NetPrice = new Money(NetPrice.GetValueIn(nowa), nowa);
        }
        public string FormatString()
        {
            return String.Format("Cena netto :{0}{3}Cena brutto :{1}{3}Vat: {2}%",
                                    NetPrice.ToString(), 
                                    this.GetGross().ToString(), 
                                    VAT, 
                                    Environment.NewLine
                                    );
        }
        public override string ToString()
        {
            return NetPrice.ToString() + " " + VAT.ToString();
        }
    }
}
