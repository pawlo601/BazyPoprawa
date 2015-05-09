using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Domain.Model.Product
{
    public class Currencies
    {
        public List<Currency> ListOfCurrency { get; private set; }
        private static Currencies instance = new Currencies();
        public static Currencies GetInstance()
        {
            return instance;
        }
        private Currencies()
        {
            Refresh();
        }
        public void Refresh()
        {//from Internet
            this.GetCurrencyFromDataBase(new List<Currency>());

            Currency dol = new Currency(Waluta.USD, Waluta.USD.GetExchange());
            ListOfCurrency.Add(dol);

            Currency eur = new Currency(Waluta.EUR, Waluta.EUR.GetExchange());
            ListOfCurrency.Add(eur);

            Currency pln = new Currency(Waluta.PLN, Waluta.PLN.GetExchange());
            ListOfCurrency.Add(pln);
        }
        public void GetCurrencyFromDataBase(List<Currency> a)
        {
            ListOfCurrency = a;
        }
        public double GetCourse(Waluta name)
        {
            double pomocnicza = Find(name);
            if (pomocnicza == -1.0f)
                throw new Exception("Nie znaleziono waluty.\n");
            else
                return pomocnicza;
        }
        private double Find(Waluta Name)
        {
            foreach (Currency a in ListOfCurrency)
            {
                if (a.Name == Name)
                    return a.ExchangeInTheRelationToPLN;
            }
            return -1.0f;
        }
        public double Swap(Waluta from, Waluta to)
        {
            double przelicznik = 0.0f;
            double xfrom = Find(from);
            double xto = Find(to);
            if (xfrom == -1.0f || xto == -1.0f)
                throw new Exception("Nie ma informacji to tych walutach.\n");
            przelicznik = xfrom / xto;
            return przelicznik;
        }
        public override string ToString()
        {
            string text = "";
            foreach (Currency a in ListOfCurrency)
                text += a.ToString();
            return text;
        }
        public string FormatString()
        {
            string text = "Dostępne waluty\n";
            foreach (Currency a in ListOfCurrency)
                text += a.FormatString() + "\n";
            return text;
        }
    }
}
