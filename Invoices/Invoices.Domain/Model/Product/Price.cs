using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Invoices.Domain.Model.Product
{
    [HasSelfValidation]
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
        public virtual Money GetGross()
        {
            return new Money((VAT+1.0f) * NetPrice.Value, NetPrice.NameOfCurrency);
        }
        public virtual void ChangeCurrency(Waluta nowa)
        {
            NetPrice = new Money(NetPrice.GetValueIn(nowa), nowa);
        }
        public virtual string FormatString()
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
        [SelfValidation]
        public void VATValidation(ValidationResults results)
        {
            if (VAT <= 0.0f&&VAT>=1.0f)
                results.AddResult(new ValidationResult("Vat powinien być wiekszy od 0 i mniejszy od 1", this, "VATValidation", string.Empty, null));
        }
        [SelfValidation]
        public virtual void NetPriceValidation(ValidationResults results)
        {
            if (NetPrice==null)
                results.AddResult(new ValidationResult("Cena nie może być null-em", this, "NetPriceValidation", string.Empty, null));
        }
    }
}
