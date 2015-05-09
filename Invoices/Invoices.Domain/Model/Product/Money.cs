using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Domain.Model.Product
{
    public enum Waluta {PLN,EUR,USD};

    public static class Extension
    {
        public static double GetExchange(this Waluta wal)
        {
            switch (wal)
            {
                case Waluta.PLN:
                    return 1.0f;
                case Waluta.EUR:
                    return 4.0547962f;
                case Waluta.USD:
                    return 3.6130953f;
                default:
                    return -1.0f;
            }
        }
    }
    public class Money
    {
        public virtual float Value { get; set; }
        public virtual Waluta NameOfCurrency { get; set; }
        public Currencies Curr { get; private set; }

        public Money()
        {
            Random rand = new Random();
            Value = (rand.Next(100, 100000) / 1000.0f);
            int a = rand.Next(0, 3);
            switch (a)
            {
                case 0:
                    NameOfCurrency = Waluta.PLN;
                    break;
                case 1:
                    NameOfCurrency = Waluta.EUR;
                    break;
                case 2:
                    NameOfCurrency = Waluta.USD;
                    break;
                default:
                    NameOfCurrency = Waluta.PLN;
                    break;
            }
            Curr = Currencies.GetInstance();
        }
        public Money(float val, Waluta waluta)
        {
   
            if (val < 0.0f)
                throw new Exception("Zła wartość.\n");
            else
            {
                Value = val;
                NameOfCurrency = waluta;
                Curr = Currencies.GetInstance();
            }
        }
        public virtual void RefreshCurrencies()
        {
            this.Curr.Refresh();
        }
        public virtual void ChengeCurrency(Waluta curr)
        {
            try
            {
                float b = GetValueIn(curr);
                Value = b;
                NameOfCurrency = curr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public virtual float GetValueIn(Waluta waluta)
        {
            if (waluta == NameOfCurrency)
                return Value;
            else
            {
                try
                {
                    return (float)(Curr.Swap(NameOfCurrency, waluta)) * Value;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public override string ToString()
        {
            return String.Format("{0} {1}", Value, NameOfCurrency);
        }
        public virtual string FormatString()
        {
            return "Wartość: " + Value.ToString() + NameOfCurrency.ToString();
        }

    }
}
