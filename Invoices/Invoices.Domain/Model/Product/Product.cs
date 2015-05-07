using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Domain.Model.Product
{
    public enum TypProduktu { Przedmiot, Usługa };
    public class Product
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual TypProduktu Type { get; set; }
        public virtual Price Price { get; set; }
        public virtual string Comments
        {
            get
            {
                return this._comments;
            }
            set
            {
                this.SetComments(value);
            }
        }
        private string _comments;

        public Product()
        {
            ID = -1;
            Name = "Nazwa Produktu";
            Random rand = new Random();
            Name += rand.Next(1, 20000).ToString();
            switch (rand.Next(0, 2))
            {
                case 0:
                    Type = TypProduktu.Przedmiot;
                    break;
                case 1:
                    Type = TypProduktu.Usługa;
                    break;
                default:
                    Type = TypProduktu.Przedmiot;
                    break;
            }
            Price = new Price();
            Comments = "Brak komentarza";
        }
        public Product(string name, TypProduktu type, Price price)
        {
            this.ID = -1;
            this.Name = name;
            this.Type = type;
            this.Price = price;
            Comments = "Brak komentarza";
        }
        public virtual void SetComments(string comm)
        {
            if (comm.Length > 250)
                this._comments = comm.Substring(0, 250);
            else
                this._comments = comm;
        }
        public virtual Waluta GetCurrency()
        {
            return Price.NetPrice.NameOfCurrency;
        }
        public virtual void ChangeCurrency(Waluta a)
        {
            Price.ChangeCurrency(a);
        }
        public virtual string FormatString()
        {
            string przewwa = "====================================";
            return String.Format("Produkt: {0}{4}{5}{4}Typ: {1}{4}{5}{4}Cena:{4}{2}{4}{5}{4}Komentarz:{4}{3}{4}{5}",
                                        Name, 
                                        Type.ToString(), 
                                        Price.FormatString(), 
                                        Comments, 
                                        Environment.NewLine,
                                        przewwa
                                        );
        }
        public override string ToString()
        {
            return ID.ToString() + "\n" +
                   Name + "\n" +
                   Type.ToString() + "\n" +
                   Price.ToString() + "\n" +
                   Comments;
        }
    }
}
